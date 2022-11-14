using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace MilkTeaStore
{
    class Menu
    {
        public static bool statusMenu = true;

        public  Customer cus ;
        public  Oder oder;
        public Discount discount = new Discount();
        public Staff staff = new Staff(1, "Ngoc", "1234567", "20/3 duong hang tre", "Fulltime", "NhanVien", 1200,0);
        public Bill bill ;

        public Menu() { }
        public  bool WelcomeMenu()
        {
            Console.Clear();
            Console.WriteLine(String.Format("{0}{1,-55}", "", "Welcome to TeaStore"));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "-----------------------"));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "Vui long nhap vai tro :"));
            Console.WriteLine(String.Format("{0}{1,-55}", "1.", "Khach Hang"));
            Console.WriteLine(String.Format("{0}{1,-55}", "2.", "Quan Ly"));
            Console.WriteLine(String.Format("{0}{1,-55}", "3.", "Exit"));
            Console.WriteLine();
            Console.Write("Select an option : ");
            switch (Console.ReadLine())
            {
                case "1":
                    CustomerMenu();
                    return true;
                case "2":
                    Console.WriteLine("1");
                    return true;
                case "3":
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
            Console.WriteLine();
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

    }
}