using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using TeaStorel;

namespace TeaStorel
{
    class Customer:Person
    {
        public  int CusId { get; set; }
        public Customer()
        {
           
        }
        public Customer(string name , string numberphone):base(name,numberphone)
        {

        }
        public Customer(int id, string name, string numberphone, string address) : base( name, numberphone, address)
        {
            this.CusId = id;
        }
        public void HistoryBuying(){
            List<Bill> billList = Database<Bill>.readFile(Database<Bill>.BillFilePath);
            List<Customer> customerlist = Database<Customer>.readFile(Database<Customer>.CustomerFilePath);

            var query = from b in billList
                        join cus in customerlist on b.CusID equals cus.CusId
                        where cus.Numberphone == this.Numberphone && cus.Name == this.Name
                        select b;
            
            Console.Clear();
            Console.WriteLine("\t\t\tLich su hoa don cua " + this.Name +"\n");
            Database<Bill>.Table(query);
            Console.WriteLine("<------ Back");
            Console.ReadLine();

        }
        
        public void CreateNewOderMenu() 
        {
            Console.Clear();
            Console.WriteLine(String.Format("{0}{1,-55}", "", "Nhap thong tin dat hang"));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "-----------------------"));
            Console.WriteLine(String.Format("{0}{1,-15}", "1.", this.Name == null ? "Nhap ten khach hang" : String.Format("Ten khach hang:{0,11}{1,2}", "|", Name)));
            Console.WriteLine(String.Format("{0}{1,-15}", "2.", this.Numberphone == null ? "Nhap so dien thoai" : String.Format("So dien thoai:{0,12}{1,2}", "|", Numberphone)));
            Console.WriteLine(String.Format("{0}{1,-15}", "3.", this.Address == null  ? "Nhap dia chi giao hang " : String.Format("Dia chi giao hang:{0,8}{1,2}", "|",Address)));
            Console.WriteLine("---------------------");
        }

        public void printCustomer()
        {
            CacheData.customers = Database<Customer>.readFile(Database<Customer>.CustomerFilePath);
            var list = CacheData.customers.Where(o => o.CusId == this.CusId);
            Database<Customer>.Table(CacheData.customers);
        }
        public void addManageCustomer()
        {
            CacheData.customers = Database<Customer>.readFile(Database<Customer>.CustomerFilePath);
            //--Check add Customer
            Console.WriteLine("Nhap ten khach hang de them :");
            this.Name = Console.ReadLine();
            Console.WriteLine("Nhap so dien thoai khach hang de them :");
            this.Numberphone = Console.ReadLine();
            Console.WriteLine("Nhap dia chi khach hang de them :");
            this.Address = Console.ReadLine();

            this.CusId = CacheData.customers.Any() ? CacheData.customers.Max(x => x.CusId) + 1 : 1; // tang id cua Customer len 1

            //--Add Customer to Customers
            CacheData.customers.Add(this);

            Console.WriteLine("Them thanh cong !");
            Database<Customer>.Table(CacheData.customers);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
        }
        public bool deleteManageCustomer()
        {
            //--Check id sp
            CacheData.customers = Database<Customer>.readFile(Database<Customer>.CustomerFilePath);
            int id;
            while (true)
            {
                Console.Write("Nhap id nhan vien muon xoa :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in CacheData.customers
                           where o.CusId == id
                           select o;
                if (list.Any(oder => oder.CusId == id))
                    break;
                Console.WriteLine("Ma nhan vien khong hop le vui long nhap lai ! ");
            };

            CacheData.customers.RemoveAll(x => x.CusId == id);

            Console.WriteLine("Xoa thanh cong !");
            Database<Customer>.Table(CacheData.customers);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
            return true;
        }
        public bool editManageCustomer()
        {
            CacheData.customers = Database<Customer>.readFile(Database<Customer>.CustomerFilePath);
            //--Check id sp
            int id;
            while (true)
            {
                Console.Write("Nhap id nhan vien muon sua :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in CacheData.customers
                           where o.CusId == id
                           select o;
                if (list.Any(oder => oder.CusId == id))
                    break;
                Console.WriteLine("Ma nhan vien khong hop le vui long nhap lai ! ");
            };
            CacheData.customers.RemoveAll(x => x.CusId == id);

            this.CusId = id;
            //--Check add Customer
            Console.WriteLine("Nhap ten khach hang de sua :");
            this.Name = Console.ReadLine();
            Console.WriteLine("Nhap so dien thoai khach hang de sua :");
            this.Numberphone = Console.ReadLine();
            Console.WriteLine("Nhap dia chi khach hang de sua :");
            this.Address = Console.ReadLine();

            //--Add Customer to Customers
            CacheData.customers.Add(this);

            Console.WriteLine("Sua thanh cong !");
            Database<Customer>.Table(CacheData.customers);
            Console.WriteLine("<---- Back");
            Console.ReadLine();

            return true;
        }
        public bool addManageCustomertoDataBase()
        {
            try
            {
                var enumerable = from o in CacheData.customers
                                 orderby o.CusId descending
                                 select o;
                List<Customer> oderByList = enumerable.ToList();

                Database<Customer>.writeFile(oderByList, Database<Customer>.CustomerFilePath); //add to database
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }


    }
}