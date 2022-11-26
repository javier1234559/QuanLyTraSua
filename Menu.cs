using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace TeaStorel
{
    class Menu
    {
        public static bool statusMenu = true; //Thoat ct

        //Thuoc tinh luu tru de thao tac Oder
        public Customer cus;
        public Order order;
        public Staff staff = new Staff();
        public Discount discount = new Discount();
        public Bill bill;

        //Thuoc tinh luu tru de thao tac quan ly
        public Product manageProduct = new Product();
        public Staff manageStaff = new Staff();
        public Customer manageCustomer = new Customer();
        public Order manageOder = new Order();
        public Bill manageBill = new Bill();
        public Discount manageDiscount = new Discount();
        public Ingredient manageIngredient = new Ingredient();
        public Supply manageSupply = new Supply();

        public Menu() { }

        //Menu Chinh
        public void PrintOptionConsole(string title, string[] str)
        {
            Console.WriteLine(String.Format("{0}{1,-55}", "", title));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "-----------------------"));
            for (int i = 0; i < str.Length; i++)
            {
                Console.WriteLine(String.Format("{0}.{1,-55}", i + 1, str[i]));
            }
            Console.WriteLine();
            Console.Write("Select an option : ");
        }

        public bool MainMenu()
        {
            Console.Clear();
            string title = "WELCOME TO TEASTORE";
            string[] listoption = { "Khach Hang", "Nhan Vien", "Quan Ly", "Exit" };
            PrintOptionConsole(title, listoption);

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


        //Menu Khach Hang
        public void CreateNewCustomer() //Tao moi Customer nhung neu cung 1 Ten,Sdt thi xem nhu la Dang Nhap
        {
            List<Customer> customers = Database<Customer>.readFile(Database<Customer>.CustomerFilePath);

            //Add Customer
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

            //Add to DataBase
            Database<Customer>.writeFile(customers, Database<Customer>.CustomerFilePath); //add to database
            Console.Write("Tiep tuc order ---> ");
            Console.ReadLine();//Stop screen
            CustomerMenu();
        }

        public bool CustomerMenu()
        {
            Console.Clear();
            string title = "Xin chao khach hang " + this.cus.Name;
            string[] listoption = { "Dat Hang", "Lich Su Hoa Don", "Quay Lai", "Thoat" };
            PrintOptionConsole(title, listoption);

            switch (Console.ReadLine())
            {
                case "1":
                    OderMenu();
                    return true;
                case "2":
                    cus.HistoryBuying(); // Se co lich su neu trung ten,sdt
                    CustomerMenu();
                    return true;
                case "3":
                    MainMenu();
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

        public bool OderMenu()
        {
            Console.Clear();
            List<Product> products = null;
            products = Database<Product>.readFile(Database<Product>.ProductFilePath);
            order = new Order(); // Tao 1 Oder

            while (true)
            {
                //Xuat Danh sach Product
                Console.Clear();
                string[] labels = { "ProductID", "Name", "Size", "Price" };
                TableDraw.TableHavePropSelected(products, labels);

                //Xuat Option
                string title = "DANH SACH CAC THUC UONG";
                string[] listoption = { "Them Oder", "Xoa Oder", "Hoan Tat Oder", "Quay lai", "Exit" };
                PrintOptionConsole(title, listoption);
                switch (Console.ReadLine())
                {
                    case "1":
                        order.AddThisOrder();
                        break;
                    case "2":
                        order.PrintThisOrderList();
                        order.DeleteProductInOrder();
                        break;
                    case "3":
                        order.AddThisOrderToDatabase();
                        Console.Write("Hay nhap ngay hom nay de biet giam gia (vd: 25/12): ");
                        string date = Console.ReadLine();
                        Console.WriteLine("Nhap ma nhan vien neu co (y/n)");
                        string ma = Console.ReadLine();
                        if (ma == "y")
                        {
                            int staffOderId = Int32.Parse(Console.ReadLine());
                            this.discount.DiscountID = discount.CheckDiscount(date);
                            this.bill = new Bill(cus.CusId, staffOderId, date, this.discount.DiscountID);
                            bill.AddThisBillToCache();
                        }
                        else
                        {
                            this.discount.DiscountID = discount.CheckDiscount(date);
                            this.bill = new Bill(cus.CusId, date, this.discount.DiscountID);
                            bill.AddThisBillToCache();
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


        //Menu Nhan Vien
        public void LoginNhanVien()
        {
            //Login Staff
            staff.LoginMenuThisStaff();
            while (true)
            {
                //Input Login
                Console.Write("Ten nhan vien : ");
                staff.Name = Console.ReadLine();
                staff.LoginMenuThisStaff();      // Ham Login Menu lap lai de xem gia tri vua nhap tren menu
                Console.Write("So dien thoai : ");
                staff.Numberphone = Console.ReadLine();
                staff.LoginMenuThisStaff();

                //Check Login
                if (staff.CheckAndLoginThisStaff()) break;
                Console.WriteLine("Ten hoac so dien thoai khong dung !");
                Console.WriteLine("Thoat ? (y/n)");
                string s = Console.ReadLine();
                if (s == "y") break;
            }

            //Login thanh cong va chuyen sang Staff Menu
            Console.WriteLine("Dang Nhap thanh cong ! ");
            Console.WriteLine("------> Next");
            Console.ReadLine();
            StaffMenu();
        }

        public bool StaffMenu()
        {
            Console.Clear();
            string title = "Xin chao nhan vien  " + this.staff.Name;
            string[] listoption = { "Xem thong tin ", "Xem lich su order ", "Quay Lai", "Thoat" };
            PrintOptionConsole(title, listoption);

            switch (Console.ReadLine())
            {
                case "1":
                    //OderMenu();
                    return true;
                case "2":
                    //HistoryOderStaff();
                    return true;
                case "3":
                    MainMenu();
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


        //Menu Quan Ly 
        public bool ManagerMenu()
        {
            Console.Clear();
            string title = "Quan Ly";
            string[] listOption = { "Tao Them Xoa Sua Danh Sach", "Xem Chi Phi Hang Thang", "Quay Lai", "Thoat" };
            PrintOptionConsole(title, listOption);
            switch (Console.ReadLine())
            {
                case "1":
                    CrudManagerList();
                    return true;
                case "2":
                    Console.WriteLine("Chua hoan tat ");
                    Console.ReadLine(); // STOP sreen
                    return true;
                case "3":
                    MainMenu();
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

        public bool CrudManagerList()
        {
            Console.Clear();
            string title = "Danh Sach Quan Ly";
            string[] listoption = { "Quan ly danh sach san pham", "Quan ly danh sach nhan vien",
                "Quan ly danh sach khach hang", "Quan ly danh sach order","Quan ly danh sach hoa don",
                "Quan ly danh sach giam gia","Quan ly danh sach nguyen lieu","Quan ly danh sach cung cap",
                "Lich su hoa don", "Quay Lai","Thoat"};
            PrintOptionConsole(title, listoption);
            switch (Console.ReadLine())
            {
                case "1":
                    CrudProduct();
                    return true;
                case "2":
                    CrudStaff();
                    return true;
                case "3":
                    CrudCustomer();
                    return true;
                case "10":
                    MainMenu();
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

        public bool CrudProduct()
        {
            List<Product> products = Database<Product>.readFile(Database<Product>.ProductFilePath);

            while (true)
            {
                Console.Clear();
                TableDraw.Table(products);
                string title = "Quan ly Danh sach san pham";
                string[] listOption = { "Them San Pham", "Xoa San Pham", "Sua San Pham", "Luu Va Lam Moi", "Quay Lai", "Thoat" };
                PrintOptionConsole(title, listOption);

                switch (Console.ReadLine())
                {
                    case "1":
                        manageProduct.AddProductManager();
                        break;
                    case "2":
                        manageProduct.DeleteManageProduct();
                        break;
                    case "3":
                        manageProduct.EditManageProduct();
                        break;
                    case "4":
                        if (manageProduct.AddManageProducttoDataBase())
                            Console.WriteLine("Cap nhat thay doi vao database thanh cong !");
                        Console.ReadLine();
                        CrudProduct();
                        break;
                    case "5":
                        CrudManagerList();
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

        public bool CrudStaff()
        {
            List<Staff> staffs = Database<Staff>.readFile(Database<Staff>.StaffFilePath);

            while (true)
            {
                Console.Clear();
                TableDraw.Table(staffs);
                string title = "Quan ly Danh Sach Nhan Vien";
                string[] listOption = { "Them Nhan Vien", "Xoa Nhan Vien", "Sua Nhan Vien", "Luu Va Lam Moi", "Quay Lai", "Thoat" };
                PrintOptionConsole(title, listOption);

                switch (Console.ReadLine())
                {
                    case "1":
                        manageStaff.AddManageStaff();
                        break;
                    case "2":
                        manageStaff.DeleteManageStaff();
                        break;
                    case "3":
                        manageStaff.EditManageStaff();
                        break;
                    case "4":
                        if (manageStaff.AddManageStafftoDataBase())
                            Console.WriteLine("Cap nhat thay doi vao database thanh cong !");
                        Console.ReadLine();
                        CrudStaff();
                        break;
                    case "5":
                        CrudManagerList();
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Ket Thuc Chuong Trinh !");
                        statusMenu = false;
                        MainMenu();
                        return false;
                    default:
                        return true;
                }
            }


        }

        public bool CrudCustomer()
        {
            List<Customer> customers = Database<Customer>.readFile(Database<Customer>.CustomerFilePath);

            while (true)
            {
                Console.Clear();
                TableDraw.Table(customers);
                string title = "Quan ly Danh Sach Khach Hang";
                string[] listOption = { "Them Khach Hang", "Xoa Khach Hang", "Sua Khach Hang", "Luu Va Lam Moi", "Quay Lai", "Thoat" };
                PrintOptionConsole(title, listOption);

                switch (Console.ReadLine())
                {
                    case "1":
                        manageCustomer.AddManageCustomer();
                        break;
                    case "2":
                        manageCustomer.DeleteManageCustomer();
                        break;
                    case "3":
                        manageCustomer.EditManageCustomer();
                        break;
                    case "4":
                        if (manageCustomer.AddManageCustomertoDataBase())
                            Console.WriteLine("Cap nhat thay doi vao database thanh cong !");
                        Console.ReadLine();
                        CrudCustomer();
                        break;
                    case "5":
                        CrudManagerList();
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Ket Thuc Chuong Trinh !");
                        statusMenu = false;
                        MainMenu();
                        return false;
                    default:
                        return true;
                }
            }


        }


    }
}