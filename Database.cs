using LINQtoCSV;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MilkTeaStore
{
    //Generic
    public class Database<T> where T : class ,new()
    {
        public static string DiscountFilePath { get; set; } = "DataBase//dicount.csv";
        public static string CustomerFilePath { get; set; } = "DataBase//customer.csv";
        public static string StaffFilePath { get; set; } = "DataBase//staff.csv";
        public static string BillFilePath { get; set; } = "DataBase//bill.csv";
        public static string OderFilePath { get; set; } = "DataBase//oder.csv";
        public static string ProductFilePath { get; set; } = "DataBase//product.csv";
        public static string SupplyFilePath { get; set; } = "DataBase//supply.csv";
        public static string IngredientFilePath { get; set; } = "DataBase//ingredient.csv";

        public Database()
        {
               
        }
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
                new Customer("cus1","Nhat","09422323","40/104fdfsdf"),
                new Customer("cus2", "Hung", "09422323", "40/104fdfsdf"),
                new Customer("cus3", "Hang", "09422323", "40/104fdfsdf")
            };
            var ProductList = new List<Product>
            {
                new Product("item1","Tra Sua",20000,SIZE.S,10,0,0,190),
                new Product("item2","Tra Sua",20000,SIZE.L,10,0,0,20),
                new Product("item3","Tra Sua",20000,SIZE.M,10,0,0,40),
                new Product("item4","Tra Dao",10000,SIZE.S,10,0,0,20),
                new Product("item5","Tra Dao",10000,SIZE.L,10,0,0,20),
                new Product("item6","Tra Dao",10000,SIZE.M,10,0,0,20),
                new Product("item7","Chanh Leo",15000,SIZE.M,10,0,0,50),
                new Product("item8","Chanh Leo",15000,SIZE.M,10,0,0,20),
                new Product("item9","Chanh Leo",15000,SIZE.M,10,0,0,30),
            };
            var StaffList = new List<Staff>
            {
                new Staff("staff1","Ngoc","1234567","20/3 duong hang tre","Fulltime","NhanVien",1200,0),
                new Staff("staff2","Ngoc","1234567","20/3 duong hang tre","Fulltime","NhanVien",1200,0),
                new Staff("staff3","Ngoc","1234567","20/3 duong hang tre","Fulltime","NhanVien",1200,0),
            };
            var DiscountList = new List<Discount>
            {
                new Discount("discount1",0.2f,"Giam gia giang sinh"),
                new Discount("discount2",0.2f,"Giam gia sinh nhat"),
                new Discount("discount3",0.2f,"Giam gia mua 10 ly"),
            };
            var IngredientList = new List<Ingredient> {
                new Ingredient("ingre1","Sugar",3000,"Cat Linh"),
                new Ingredient("ingre2","Chan Chau",1000,"Cat Linh"),
                new Ingredient("ingre3","Sugar",200,"Cat Linh")
            };
            var BillList = new List<Bill> {
                new Bill("bill1","cus1","staff1","20/10/2002","discount1",20000),
                new Bill("bill2","cus2","staff1","20/10/2002","discount1",40000),
                new Bill("bill3","cus1","staff1","20/12/2002","discount1",20000),
                new Bill("bill4","cus1","staff1","20/12/2002","discount1",20000),
                new Bill("bill5","cus1","staff1","20/12/2002","discount1",20000),
            };

            Database<Customer>.writeFile(customerList, Database<T>.CustomerFilePath);
            Database<Product>.writeFile(ProductList, Database<T>.ProductFilePath);
            Database<Staff>.writeFile(StaffList, Database<T>.StaffFilePath);
            Database<Discount>.writeFile(DiscountList, Database<T>.DiscountFilePath);
            Database<Bill>.writeFile(BillList, Database<T>.BillFilePath);
            Database<Ingredient>.writeFile(IngredientList, Database<T>.IngredientFilePath);


        }
        public static void Table(List<T> oblist) {
            var label = oblist[0];
            foreach (PropertyDescriptor d in TypeDescriptor.GetProperties(label))
            {
                string name = d.Name;
                Console.Write("{0,-14}", name);
            }
            foreach (var o in oblist) {
               
                Console.WriteLine();
                /*foreach (PropertyDescriptor d in TypeDescriptor.GetProperties(o))
                {
                    Console.Write(" {0,-10}", "-------------");
                }
                Console.WriteLine();*/
                foreach (PropertyDescriptor d in TypeDescriptor.GetProperties(o))
                {
                    object value = d.GetValue(o);
                    Console.Write("|{0,-13}", value);
                }
            }
            Console.WriteLine();
        }
        

    }
}
