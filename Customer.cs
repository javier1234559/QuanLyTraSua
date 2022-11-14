using System.Net;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace MilkTeaStore
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
        public void HistoryBuying (){
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
        public void CreateNewOder()
        {
            //Get data from database
            List<Customer> customers = Database<Customer>.readFile(Database<Customer>.CustomerFilePath);
            
            CreateNewOderMenu();
            Console.Write("Ten khach hang : ");
            this.Name = Console.ReadLine();
            CreateNewOderMenu();
            Console.Write("So dien thoai : ");
            this.Numberphone = Console.ReadLine();
            CreateNewOderMenu();
            Console.Write("Dia chi dat hang : ");
            this.Address = Console.ReadLine();
            CreateNewOderMenu();

            this.CusId = customers.Any() ? customers.Max(x => x.CusId) + 1 : 1; // tang id cua Bill len 1
            customers.Add(new Customer(this.CusId,this.Name,this.Numberphone,this.Address));

            //Debug
            foreach(var cus in customers)
            {
                Console.WriteLine(cus.Name);
            }
            //Them vao database
            Database<Customer>.writeFile(customers, Database<Customer>.CustomerFilePath); //add to database
            Console.Write("Tiep tuc oder ---> ");
            Console.ReadLine();//Stop screen
        }
        public void CreateNewOderMenu() // khong biet co nen bo o Customer ko
        {
            Console.Clear();
            Console.WriteLine(String.Format("{0}{1,-55}", "", "Nhap thong tin dat hang"));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "-----------------------"));
            Console.WriteLine(String.Format("{0}{1,-15}", "1.", this.Name == null ? "Nhap ten khach hang" : String.Format("Ten khach hang:{0,11}{1,2}", "|", Name)));
            Console.WriteLine(String.Format("{0}{1,-15}", "2.", this.Numberphone == null ? "Nhap so dien thoai" : String.Format("So dien thoai:{0,12}{1,2}", "|", Numberphone)));
            Console.WriteLine(String.Format("{0}{1,-15}", "3.", this.Address == null  ? "Nhap dia chi giao hang " : String.Format("Dia chi giao hang:{0,8}{1,2}", "|",Address)));
            Console.WriteLine("---------------------");
        }

    }
}