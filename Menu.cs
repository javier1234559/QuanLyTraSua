using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace MilkTeaStore
{
    class Menu
    {
        public static bool statusMenu = true;
        public Menu() { }
        public static bool WelcomeMenu()
        {
            Console.Clear();
            Console.WriteLine(String.Format("{0}{1,-55}", "", "Welcome to TeaStore"));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "-----------------------"));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "Vui long nhap vai tro :"));
            Console.WriteLine(String.Format("{0}{1,-55}", "1.", "Khach Hang"));
            Console.WriteLine(String.Format("{0}{1,-55}", "2.", "Quan Ly"));
            Console.WriteLine("\nEnter q to exit ");
            Console.Write("Select an option : ");
            switch (Console.ReadLine())
            {
                case "1":
                    CustomerMenu();
                    return true;
                case "2":
                    Console.WriteLine("1");
                    return true;
                case "q":
                    Console.Clear();
                    Console.WriteLine("Ket Thuc Chuong Trinh !");
                    statusMenu = false;
                    return false;
                default:
                    return true;
            }
        
        }
        public static bool CustomerMenu()
        {
            Console.Clear();
            Console.WriteLine(String.Format("{0}{1,-55}", "", "Khach hang"));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "-----------------------"));
            Console.WriteLine(String.Format("{0}{1,-55}", "1.", "Dat Hang"));
            Console.WriteLine(String.Format("{0}{1,-55}", "2.", "Lich su hoa don"));
            Console.WriteLine("\nEnter -1 to back ");
            Console.WriteLine("Enter q to end ");
            Console.Write("Select an option : ");
            switch (Console.ReadLine())
            {
                case "1":
                    Customer cus1 = new Customer();
                    cus1.CreateNewOder();
                    return true;
                case "2":
                    string name, numberphone;
                    Console.WriteLine("Vui long nhap ten va so dien thoai lan dat truoc: ");
                    Console.Write("Ten khach hang : ");
                    name = Console.ReadLine();
                    Console.Write("So dien thoai : ");
                    numberphone = Console.ReadLine();
                    Customer cus = new Customer(name, numberphone);

                    cus.HistoryBuying();
                    return true;
                case "-1":
                    WelcomeMenu();
                    return true;
                case "q":
                    Console.Clear();
                    Console.WriteLine("Ket Thuc Chuong Trinh !");
                    statusMenu = false;
                    return false;
                default:
                    return true;
            }
        }
        public static bool OderMenu()
        {
            List<Product> products = null;
            products = Database<Product>.readFile(Database<Product>.ProductFilePath);
            Oder oder = new Oder(); // Oder khai bao toan cuc

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
                Console.WriteLine("3. Exit ");
                Console.Write("Select an option : ");
                switch (Console.ReadLine())
                {
                    case "1": // Chua hoan tat ham
                        oder.addOder();
                        break;
                    case "2":
                        oder.printOderList();
                        oder.deleteOder() ;
                        break;
                    case "3":
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