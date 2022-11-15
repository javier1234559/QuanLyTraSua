using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace MilkTeaStore
{
    class Menu
    {
        public static bool statusMenu = true; //Thoat ct

        //Thuoc tinh luu tru de thao tac Oder
        public Customer cus ;
        public Oder oder;
        public Staff staff = new Staff(1, "Ngoc", "1234567", "20/3 duong hang tre", "Fulltime", "NhanVien", 1200,0);
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
        public  bool WelcomeMenu()
        {
            Console.Clear();
            Console.WriteLine(String.Format("{0}{1,-55}", "", "Welcome to TeaStore"));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "-----------------------"));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "Vui long nhap vai tro :"));
            Console.WriteLine(String.Format("{0}{1,-55}", "1.", "Khach Hang"));
            Console.WriteLine(String.Format("{0}{1,-55}", "2.", "Nhan Vien"));
            Console.WriteLine(String.Format("{0}{1,-55}", "3.", "Quan Ly"));
            Console.WriteLine(String.Format("{0}{1,-55}", "4.", "Exit"));
            Console.Write("Select an option : ");
            switch (Console.ReadLine())
            {
                case "1":
                    CustomerMenu();
                    return true;
                case "2":
                    //NhanvienMenu()
                    Console.WriteLine("Chua hoan tat !");
                    Console.ReadLine();
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
        public  bool CustomerMenu()
        {
            Console.Clear();
            Console.WriteLine(String.Format("{0}{1,-55}", "", "Khach hang"));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "-----------------------"));
            Console.WriteLine(String.Format("{0}{1,-55}", "1.", "Dat Hang"));
            Console.WriteLine(String.Format("{0}{1,-55}", "2.", "Lich su hoa don"));
            Console.WriteLine(String.Format("{0}{1,-55}", "3.", "Quay Lai"));
            Console.WriteLine(String.Format("{0}{1,-55}", "4.", "Thoat"));
            Console.Write("Select an option : ");
            switch (Console.ReadLine())
            {
                case "1":
                    this.cus = new Customer();
                    this.cus.CreateNewOder();
                    this.OderMenu();
                    return true;
                case "2":
                    string name, numberphone;
                    Console.WriteLine("Vui long nhap ten va so dien thoai lan dat truoc: ");
                    Console.Write("Ten khach hang : ");
                    name = Console.ReadLine();
                    Console.Write("So dien thoai : ");
                    numberphone = Console.ReadLine();

                    this.cus = new Customer(name, numberphone);
                    cus.HistoryBuying();
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
            List<Product> products = null;
            products = Database<Product>.readFile(Database<Product>.ProductFilePath);
            oder = new Oder(); // Oder khai bao toan cuc

            while (true)
            {
                Console.Clear();
                Console.WriteLine(String.Format("{0}", "Danh sach cac thuc uong"));
                Console.WriteLine(String.Format("{0}", "-----------------------"));
                //Xuat bang
                string[] labels = { "ProductID", "Name", "Size", "Price" };
                Database<Product>.QueryTable(products, labels);

                Console.WriteLine("1.Them Oder");
                Console.WriteLine("2.Xoa Oder");
                Console.WriteLine("3.Hoan Tat Oder");
                Console.WriteLine("4. Exit ");
                Console.WriteLine();
                Console.Write("Select an option : ");
                switch (Console.ReadLine())
                {
                    case "1": 
                        this.oder.addOder();
                        break;
                    case "2":
                        oder.printOderList();
                        oder.deleteOder() ;
                        break;
                    case "3":
                        oder.addOderToDatabase();
                        Console.Write("Hay nhap ngay hom nay de biet giam gia: ");
                        string date = Console.ReadLine(); 
                        this.discount.DiscountID = discount.CheckDiscount(date);
                        this.bill = new Bill(cus.CusId, staff.StaffId, date,this.discount.DiscountID);
                        bill.addBill();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Ket Thuc Chuong Trinh !");
                        statusMenu = false;
                        return false;
                    default:
                        return true;
                }
            }


        }
        public bool ManagerMenu() {
            Console.Clear();
            Console.WriteLine(String.Format("{0}{1,-55}", "", "Quan Ly"));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "-----------------------"));
            Console.WriteLine(String.Format("{0}{1,-55}", "1.", "Xem danh sach"));
            Console.WriteLine(String.Format("{0}{1,-55}", "2.", "Xem chi phi hang thang"));
            Console.WriteLine(String.Format("{0}{1,-55}", "3.", "Quay Lai"));
            Console.WriteLine(String.Format("{0}{1,-55}", "4.", "Thoat"));
            Console.WriteLine();
            Console.Write("Select an option : ");
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
            Console.WriteLine(String.Format("{0}{1,-55}", "", "Danh sach quan ly"));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "-----------------------"));
            Console.WriteLine(String.Format("{0}{1,-55}", "1.", "Quan ly danh sach san pham"));
            Console.WriteLine(String.Format("{0}{1,-55}", "2.", "Quan ly danh sach nhan vien"));
            Console.WriteLine(String.Format("{0}{1,-55}", "3.", "Quan ly danh sach khach hang"));
            Console.WriteLine(String.Format("{0}{1,-55}", "4.", "Quan ly danh sach oder"));
            Console.WriteLine(String.Format("{0}{1,-55}", "5.", "Quan ly danh sach hoa don"));
            Console.WriteLine(String.Format("{0}{1,-55}", "6.", "Quan ly danh sach giam gia"));
            Console.WriteLine(String.Format("{0}{1,-55}", "7.", "Quan ly danh sach nguyen lieu"));
            Console.WriteLine(String.Format("{0}{1,-55}", "8.", "Quan ly danh sach cung cap"));
            Console.WriteLine(String.Format("{0}{1,-55}", "9.", "Lich su hoa don"));
            Console.WriteLine(String.Format("{0}{1,-55}", "10.", "Quay Lai"));
            Console.WriteLine(String.Format("{0}{1,-55}", "11.", "Thoat"));
            Console.WriteLine();
            Console.Write("Select an option : ");
            switch (Console.ReadLine())
            {
                case "1":
                    ListProductManager();
                    return true;
                case "2":
                    ListStaffManager();
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
                
                Console.WriteLine(String.Format("{0}", "Quan ly Danh sach san pham"));
                
                //Product table 
                Database<Product>.Table(products);
                Console.WriteLine("1.Them san pham");
                Console.WriteLine("2.Xoa san pham");
                Console.WriteLine("3.Sua san pham");
                Console.WriteLine("4.Luu tat ca chinh sua");
                Console.WriteLine("5.Quay Lai ");
                Console.WriteLine("6.Thoat");
                Console.Write("Select an option : ");
                switch (Console.ReadLine())
                {
                    case "1":
                        manageProduct.addProduct();
                        break;
                    case "2":
                        manageProduct.deleteProduct();
                        break;
                    case "3":
                        manageProduct.editProduct();
                        break;
                    case "4":
                        if (manageProduct.addProducttoDataBase())
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

                Console.WriteLine(String.Format("{0}", "Quan ly Danh sach nhan vien"));
                Console.WriteLine(String.Format("{0}", "-----------------------"));
                //Staff table 
                Database<Staff>.Table(staffs);
                Console.WriteLine("1.Them nhan vien");
                Console.WriteLine("2.Xoa nhan vien");
                Console.WriteLine("3.Sua nhan vien");
                Console.WriteLine("4.Luu tat ca chinh sua");
                Console.WriteLine("5.Quay Lai ");
                Console.WriteLine("6.Thoat");
                Console.WriteLine();
                Console.Write("Select an option : ");
                switch (Console.ReadLine())
                {
                    case "1":
                        manageStaff.addStaff();
                        break;
                    case "2":
                        manageStaff.deleteStaff();
                        break;
                    case "3":
                        manageStaff.editStaff();
                        break;
                    case "4":
                        if (manageStaff.addStafftoDataBase())
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


    }
}