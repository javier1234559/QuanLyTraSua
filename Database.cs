using ConsoleTables;
using LINQtoCSV;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using DataRow = System.Data.DataRow;

namespace TeaStorel
{
    //Generic
    public class Database<T> where T : class, new()
    {
        public static string DiscountFilePath { get; set; } = "DataBase//discount.csv";
        public static string CustomerFilePath { get; set; } = "DataBase//customer.csv";
        public static string StaffFilePath { get; set; } = "DataBase//staff.csv";
        public static string BillFilePath { get; set; } = "DataBase//bill.csv";
        public static string OrderFilePath { get; set; } = "DataBase//order.csv";
        public static string ProductFilePath { get; set; } = "DataBase//product.csv";
        public static string OperateCostFilePath { get; set; } = "DataBase//operatecost.csv";
        public static string IncomeFilePath { get; set; } = "DataBase//income.csv";

        public static void writeFile(List<T> listobjects, string filepath)
        {
            var csvFileConfig = new CsvFileDescription
            {
                FirstLineHasColumnNames = true,
                SeparatorChar = ',',
            };

            var csvContext = new CsvContext();
            ProductFilePath = filepath;
            csvContext.Write(listobjects, ProductFilePath, csvFileConfig);
        }

        public static List<T> readFile(string filepath)
        {
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };
            CsvContext cc = new CsvContext();
            var list = cc.Read<T>(filepath, inputFileDescription);

            return list.ToList<T>();

        }

        public static void CreateDatabase()
        {
            var customerList = new List<Customer>
            {
                new Customer(1,"Nhat","0962472106","40/104fdfsdf"),
                new Customer(2, "Nhat", "123", "40/104fdfsdf"),
                new Customer(3, "Hang", "123", "40/104fdfsdf")
            };
            var ProductList = new List<Product>
            {
                new Product(1,"Tra Sua",SIZE.S,10000,10,0,0,190),
                new Product(2,"Tra Sua",SIZE.L,10000,10,0,0,20),
                new Product(3,"Tra Sua",SIZE.M,10000,10,0,0,40),
                new Product(4,"Tra Dao",SIZE.S,10000,10,0,0,20),
                new Product(5,"Tra Dao",SIZE.L,10000,10,0,0,20),
                new Product(6,"Tra Dao",SIZE.M,10000,10,0,0,20),
                new Product(7,"Chanh Leo",SIZE.M,10000,10,0,0,50),
                new Product(8,"Chanh Leo",SIZE.M,10000,10,0,0,20),
                new Product(9,"Chanh Leo",SIZE.M,10000,10,0,0,30),
            };
            var StaffList = new List<Staff>
            {
                new Staff(1,"Nhat","123","20/3 duong hang tre","Fulltime","NhanVien",1200,0),
                new Staff(2,"Hung","123","20/3 duong hang tre","Fulltime","NhanVien",1200,0),
                new Staff(3,"Ngoc","123","20/3 duong hang tre","Fulltime","NhanVien",1200,0),
            };
            var DiscountList = new List<Discount>
            {
                new Discount(1,"25/12",0.5,"Giam gia giang sinh"),
                new Discount(2,"15/11",0.2,"Giam gia sinh nhat"),
            };
            var billList = new List<Bill> {
                new Bill(1,1,1,"20/10",1,20000),
                new Bill(2,1,1,"30/10",1,20000),
                new Bill(3,1,1,"1/11",1,20000),
                new Bill(4,2,2,"13/11",1,20000),
                new Bill(5,2,2,"2/11",1,20000),
                new Bill(6,2,2,"4/11",1,20000),
                new Bill(7,3,3,"5/12",2,20000),
                new Bill(8,3,3,"5/12",2,20000),
                new Bill(9,3,3,"5/12",2,20000),
            };
            var operateCosts = new List<OperateCost>
            {
                new OperateCost(1,"Dien",300),
                new OperateCost(2,"Mat Bang",2300),
                new OperateCost(3,"Nuoc",10)
            };
            var incomes = new List<Income>
            {
                new Income(1,"9",3000)
            };
            var oders = new List<Order>
            {
                new Order(3,5,2)
            };


            //Add to DataBase
            Database<Customer>.writeFile(customerList, Database<T>.CustomerFilePath);
            Database<Product>.writeFile(ProductList, Database<T>.ProductFilePath);
            Database<Staff>.writeFile(StaffList, Database<T>.StaffFilePath);
            Database<Discount>.writeFile(DiscountList, Database<T>.DiscountFilePath);
            Database<Bill>.writeFile(billList, Database<T>.BillFilePath);
            Database<OperateCost>.writeFile(operateCosts, Database<OperateCost>.OperateCostFilePath);
            Database<Income>.writeFile(incomes, Database<Income>.IncomeFilePath);
            Database<Order>.writeFile(oders, Database<Order>.OrderFilePath);

        }

    }
}
