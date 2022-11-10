using System.Net;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace MilkTeaStore
{
    class Customer:Person
    {
        public  string CusId { get; set; }

        public Customer()
        {
           
        }
        public Customer(string name , string numberphone):base(name,numberphone)
        {

        }
        public Customer(string id, string name, string numberphone, string address) : base( name, numberphone, address)
        {
            this.CusId = id;
        }

        public void oderThucUong(string code)
        {
            Console.WriteLine("Hay nhap danh sach");
            //dsOder = new Oder();
            //dsOder.addOder();

            /*if(this == IStrongBox s)
            od = new Order(ds thuc uong);
            
            od.capnhatMoidat();*/
            //od.luu(); //list 
        }

        public void HistoryBuying (){
            List<Bill> billList = Database<Bill>.readFile(Database<Bill>.BillFilePath);
            List<Customer> customerlist = Database<Customer>.readFile(Database<Customer>.CustomerFilePath);

            var query = from b in billList
                        join cus in customerlist on b.CusID equals cus.CusId
                        where cus.Numberphone == this.Numberphone && cus.Name == this.Name
                        select new
                        {
                            Ten = cus.Name,
                            NgayDat = b.Date,
                            DiaChi = cus.Address,
                            TongTien = b.Total
                        };
           /* List<object> list = (List<object>)query;
            
            Database<object>.Table(list);*/

            Console.ReadLine(); // stop screen


        }
        public void CreateNewOder()
        {
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

            Console.Write("Tiep tuc oder ---> ");
            Console.ReadLine();//Stop screen
            Menu.OderMenu();
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
        override public string ToString() => $"[{this.CusId},{this.Name},{this.Address}]";

    }
}