using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace TeaStorel
{
    class Menu
    {
        public static bool statusMenu = true; //Thoat ct

        //Thuoc tinh luu tru de thao tac Oder
        public Customer cus ;
        public Oder oder;
        public Staff staff = new Staff();
        public Discount discount = new Discount();
        public Bill bill ;

        //Thuoc tinh luu tru de thao tac quan ly
        public Product manageProduct = new Product();
        public Staff manageStaff = new Staff();
        public Customer manageCustomer = new Customer();
        public Oder manageOder = new Oder();
        public Bill manageBill = new Bill();
        public Discount manageDiscount = new Discount();
        public Ingredient manageIngredient = new Ingredient();
        public Supply manageSupply = new Supply();

        public Menu() { }
        public void printOptionConsole(string title,string[] str)
        {
            Console.WriteLine(String.Format("{0}{1,-55}", "", title));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "-----------------------"));
            for (int i = 0; i  < str.Length; i++)
            {
                Console.WriteLine(String.Format("{0}.{1,-55}", i + 1, str[i]));
            }
            Console.WriteLine();
            Console.Write("Select an option : ");
        }
        public bool WelcomeMenu()
        {
            Console.Clear();
            string title = "WELCOME TO TEASTORE";
            string[] listoption = { "Khach Hang", "Nhan Vien", "Quan Ly" , "Exit" };
            printOptionConsole(title, listoption);

            switch (Console.ReadLine())
            {
                case "1":
                    this.cus = new Customer();
                    CreateNewCustomer();
                    return true;
                case "2":
                    LoginNhanVien();
                    return true;
                case "3":
                    ManagerMenu();
                    return true;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Ket Thuc Chuong Trinh !");
                    statusMenu = false;
                    return false;
                default:
                    return true;
            }
        
        }
        //Nhan Vien
        public void LoginNhanVien()
        {
            staff.loginMenu();

            while (true) { 

                Console.Write("Ten nhan vien : ");
                staff.Name = Console.ReadLine();
                staff.loginMenu();
                Console.Write("So dien thoai : ");
                staff.Numberphone = Console.ReadLine();
                staff.loginMenu();

                if (staff.checkAndLogin()) break;

                Console.WriteLine("Ten hoac so dien thoai khong dung !");
                Console.WriteLine("Thoat ? (y/n)");
                string s = Console.ReadLine();
                if (s == "y") break;
            }
            Console.WriteLine("Dang Nhap thanh cong ! " );
            Console.WriteLine("------> Next");
            Console.ReadLine();
            StaffMenu();
        }
        public bool StaffMenu()
        {
            Console.Clear();
            string title = "Xin chao nhan vien  " + this.staff.Name;
            string[] listoption = { "Xem thong tin ", "Xem lich su oder ", "Quay Lai", "Thoat" };
            printOptionConsole(title, listoption);

            switch (Console.ReadLine())
            {
                case "1":
                    //OderMenu();
                    return true;
                case "2":
                    //HistoryOderStaff();
                    return true;
                case "3":
                    WelcomeMenu();
                    return true;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Ket Thuc Chuong Trinh !");
                    statusMenu = false;
                    return false;
                default:
                    return true;
            }
        }

        //Khach Hang

        public void CreateNewCustomer()
        {
            //Get data from database
            List<Customer> customers = Database<Customer>.readFile(Database<Customer>.CustomerFilePath);

            this.cus.CreateNewOderMenu();
            Console.Write("Ten khach hang : ");
            this.cus.Name = Console.ReadLine();
            this.cus.CreateNewOderMenu();
            Console.Write("So dien thoai : ");
            this.cus.Numberphone = Console.ReadLine();
            this.cus.CreateNewOderMenu();
            Console.Write("Dia chi dat hang : ");
            this.cus.Address = Console.ReadLine();
            this.cus.CreateNewOderMenu();

            this.cus.CusId = customers.Any() ? customers.Max(x => x.CusId) + 1 : 1; // tang id cua Bill len 1
            customers.Add(this.cus);


            //Them vao database
            Database<Customer>.writeFile(customers, Database<Customer>.CustomerFilePath); //add to database
            Console.Write("Tiep tuc oder ---> ");
            Console.ReadLine();//Stop screen
            CustomerMenu();
        }
        public bool CustomerMenu()
        {
            Console.Clear();
            string title = "Xin chao khach hang " + this.cus.Name;
            string[] listoption = { "Dat Hang", "Lich Su Hoa Don", "Quay Lai", "Thoat" };
            printOptionConsole(title, listoption);

            switch (Console.ReadLine())
            {
                case "1":
                    OderMenu();
                    return true;
                case "2":
                    cus.HistoryBuying();
                    CustomerMenu();
                    return true;
                case "3":
                    WelcomeMenu();
                    return true;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Ket Thuc Chuong Trinh !");
                    statusMenu = false;
                    return false;
                default:
                    return true;
            }
        }
        public  bool OderMenu()
        {
            Console.Clear();
            List<Product> products = null;
            products = Database<Product>.readFile(Database<Product>.ProductFilePath);
            oder = new Oder(); // Oder khai bao toan cuc

            while (true)
            {
                Console.Clear();
                //Xuat bang
                string[] labels = { "ProductID", "Name", "Size", "Price" };
                Database<Product>.QueryTable(products, labels);
                
                //Xuat option
                string title = "DANH SACH CAC THUC UONG";
                string[] listoption = { "Them Oder", "Xoa Oder", "Hoan Tat Oder","Quay lai", "Exit" };
                printOptionConsole(title, listoption);

                switch (Console.ReadLine())
                {
                    case "1": 
                        oder.addOder();
                        break;
                    case "2":
                        oder.printOderList();
                        oder.deleteProductInOder() ;
                        break;
                    case "3":
                        oder.addOderToDatabase();
                        Console.Write("Hay nhap ngay hom nay de biet giam gia (vd: 25/12): ");
                        string date = Console.ReadLine();
                        Console.WriteLine("Nhap ma nhan vien neu co (y/n)");
                        string ma = Console.ReadLine();
                        if( ma == "y"){
                            int staffOderId = Int32.Parse(Console.ReadLine());
                            this.discount.DiscountID = discount.CheckDiscount(date);
                            this.bill = new Bill(cus.CusId, staffOderId, date,this.discount.DiscountID);
                            bill.addBill();
                        }
                        else {
                            this.bill = new Bill(cus.CusId, date, this.discount.DiscountID);
                            bill.addBill();
                        }
                        break;
                    case "4":
                        CustomerMenu();
                        return false;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("Ket Thuc Chuong Trinh !");
                        statusMenu = false;
                        return false;
                    default:
                        return true;
                }
            }
        }
       
        //Quan Ly 
        public bool ManagerMenu() {
            Console.Clear();
            string title = "Quan Ly";
            string[] listOption = { "Xem danh sach", "Xem chi phi hang thang", "Quay Lai", "Thoat" };
            printOptionConsole(title, listOption);

            switch (Console.ReadLine())
            {
                case "1":
                    ListManage();
                    return true;
                case "2":
                    Console.WriteLine("Chua hoan tat ");
                    Console.ReadLine(); // STOP sreen
                    return true;
                case "3":
                    WelcomeMenu();
                    return true;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Ket Thuc Chuong Trinh !");
                    statusMenu = false;
                    return false;
                default:
                    return true;
            }
        }
        public bool ListManage()
        {
            Console.Clear();
            string title = "Danh Sach Quan Ly";
            string[] listoption = { "Quan ly danh sach san pham", "Quan ly danh sach nhan vien",
                "Quan ly danh sach khach hang", "Quan ly danh sach oder","Quan ly danh sach hoa don",
                "Quan ly danh sach giam gia","Quan ly danh sach nguyen lieu","Quan ly danh sach cung cap",
                "Lich su hoa don", "Quay Lai","Thoat"};
            printOptionConsole(title, listoption);

            switch (Console.ReadLine())
            {
                case "1":
                    ListProductManager();
                    return true;
                case "2":
                    ListStaffManager();
                    return true;
                case "3":
                    ListCustomerManager();
                    return true;
                case "10":
                    WelcomeMenu();
                    return true;
                case "11":
                    Console.Clear();
                    Console.WriteLine("Ket Thuc Chuong Trinh !");
                    statusMenu = false;
                    return false;
                default:
                    return true;
            }
        }
        public bool ListProductManager()
        {
            List<Product> products =  Database<Product>.readFile(Database<Product>.ProductFilePath);

            while (true)
            {
                Console.Clear();
                Database<Product>.Table(products);
                string title = "Quan ly Danh sach san pham";
                string[] listOption = { "Them San Pham","Xoa San Pham","Sua San Pham","Luu Va Lam Moi", "Quay Lai", "Thoat" };
                printOptionConsole(title, listOption);
                
                switch (Console.ReadLine())
                {
                    case "1":
                        manageProduct.addManageProduct();
                        break;
                    case "2":
                        manageProduct.deleteManageProduct();
                        break;
                    case "3":
                        manageProduct.editManageProduct();
                        break;
                    case "4":
                        if (manageProduct.addManageProducttoDataBase())
                            Console.WriteLine("Cap nhat thay doi vao database thanh cong !");
                        Console.ReadLine();
                        ListProductManager();
                        break;
                    case "5":
                        ListManage();
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Ket Thuc Chuong Trinh !");
                        statusMenu = false;
                        return false;
                    default:
                        return true;
                }
            }


        }
        public bool ListStaffManager()
        {
            List<Staff> staffs = Database<Staff>.readFile(Database<Staff>.StaffFilePath);

            while (true)
            {
                Console.Clear();
                Database<Staff>.Table(staffs);
                string title = "Quan ly Danh Sach Nhan Vien";
                string[] listOption = {"Them Nhan Vien","Xoa Nhan Vien","Sua Nhan Vien","Luu Va Lam Moi", "Quay Lai", "Thoat" };
                printOptionConsole(title, listOption);

                switch (Console.ReadLine())
                {
                    case "1":
                        manageStaff.addManageStaff();
                        break;
                    case "2":
                        manageStaff.deleteManageStaff();
                        break;
                    case "3":
                        manageStaff.editManageStaff();
                        break;
                    case "4":
                        if (manageStaff.addManageStafftoDataBase())
                            Console.WriteLine("Cap nhat thay doi vao database thanh cong !");
                        Console.ReadLine();
                        ListStaffManager();
                        break;
                    case "5":
                        ListManage();
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Ket Thuc Chuong Trinh !");
                        statusMenu = false;
                        WelcomeMenu();
                        return false;
                    default:
                        return true;
                }
            }


        }
        public bool ListCustomerManager()
        {
            List<Customer> customers = Database<Customer>.readFile(Database<Customer>.CustomerFilePath);

            while (true)
            {
                Console.Clear();
                Database<Customer>.Table(customers);
                string title = "Quan ly Danh Sach Khach Hang";
                string[] listOption = { "Them Khach Hang", "Xoa Khach Hang", "Sua Khach Hang", "Luu Va Lam Moi", "Quay Lai", "Thoat" };
                printOptionConsole(title, listOption);

                switch (Console.ReadLine())
                {
                    case "1":
                        manageCustomer.addManageCustomer();
                        break;
                    case "2":
                        manageCustomer.deleteManageCustomer();
                        break;
                    case "3":
                        manageCustomer.editManageCustomer();
                        break;
                    case "4":
                        if (manageCustomer.addManageCustomertoDataBase())
                            Console.WriteLine("Cap nhat thay doi vao database thanh cong !");
                        Console.ReadLine();
                        ListCustomerManager();
                        break;
                    case "5":
                        ListManage();
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Ket Thuc Chuong Trinh !");
                        statusMenu = false;
                        WelcomeMenu();
                        return false;
                    default:
                        return true;
                }
            }


        }

      
    }
}