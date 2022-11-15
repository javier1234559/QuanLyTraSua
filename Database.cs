﻿using ConsoleTables;
using LINQtoCSV;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using DataRow = System.Data.DataRow;

namespace MilkTeaStore
{
    //Generic
    public  class Database<T> where T : class ,new()
    {
        public static string DiscountFilePath { get; set; } = "DataBase//dicount.csv";
        public static string CustomerFilePath { get; set; } = "DataBase//customer.csv";
        public static string StaffFilePath { get; set; } = "DataBase//staff.csv";
        public static string BillFilePath { get; set; } = "DataBase//bill.csv";
        public static string OderFilePath { get; set; } = "DataBase//oder.csv";
        public static string ProductFilePath { get; set; } = "DataBase//product.csv";
        public static string SupplyFilePath { get; set; } = "DataBase//supply.csv";
        public static string IngredientFilePath { get; set; } = "DataBase//ingredient.csv";
       
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
                new Customer(2, "Hung", "0123456789", "40/104fdfsdf"),
                new Customer(3, "Hang", "09422323", "40/104fdfsdf")
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
                new Staff(1,"Ngoc","1234567","20/3 duong hang tre","Fulltime","NhanVien",1200,0),
                new Staff(2,"Ngoc","1234567","20/3 duong hang tre","Fulltime","NhanVien",1200,0),
                new Staff(3,"Ngoc","1234567","20/3 duong hang tre","Fulltime","NhanVien",1200,0),
            };
            var DiscountList = new List<Discount>
            {
                new Discount(1,"25/12",0.5,"Giam gia giang sinh"),
                new Discount(2,"15/11",0.2,"Giam gia sinh nhat"),
            };
            var IngredientList = new List<Ingredient> {
                new Ingredient(1,"Sugar",3000,"Cat Linh"),
                new Ingredient(2,"Chan Chau",1000,"Cat Linh"),
                new Ingredient(3,"Sugar",200,"Cat Linh")
            };
            var BillList = new List<Bill> {
                new Bill(1,1,1,"20/10/2002",1,20000),
                new Bill(2,1,1,"20/10/2002",1,20000),
                new Bill(3,1,1,"20/10/2002",1,20000),
            };
            var SupplyList = new List<Supply> {
                new Supply(1,1,"15/11",30),
                new Supply(1,2,"15/11",30),
                new Supply(1,3,"15/11",30),
                new Supply(4,1,"15/11",30),
                new Supply(7,1,"15/11",30),
            };

            /*var oders = new List<Oder> {
                new Oder(9,2,30),
                new Oder(9,1,30),
                new Oder(9,1,30),
                new Oder(9,5,30),
                new Oder(11,5,30),
            };*/
            Database<Customer>.writeFile(customerList, Database<T>.CustomerFilePath);
            Database<Product>.writeFile(ProductList, Database<T>.ProductFilePath);
            Database<Staff>.writeFile(StaffList, Database<T>.StaffFilePath);
            Database<Discount>.writeFile(DiscountList, Database<T>.DiscountFilePath);
            Database<Bill>.writeFile(BillList, Database<T>.BillFilePath);
            Database<Ingredient>.writeFile(IngredientList, Database<T>.IngredientFilePath);
            Database<Supply>.writeFile(SupplyList, Database<T>.SupplyFilePath);
            //Database<Oder>.writeFile(oders, Database<Oder>.OderFilePath); //add to database


        }
        public static void Table(IEnumerable<T> enumerable) {
            if (!enumerable.Any()) return;

            List<T> oblist = enumerable.ToList();

            DataTable data = TableDraw.ToDataTable(oblist);

            string[] columnNames = data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            DataRow[] rows = data.Select();

            var table = new ConsoleTable(columnNames);

            foreach (DataRow row in rows)
            {
                table.AddRow(row.ItemArray);
            }
            table.Write(Format.Alternative);
        }
        public static void QueryTable(IEnumerable<T> enumerable,string[] props )
        {
            List<T> oblist = enumerable.ToList(); // convert to List<string>

            DataTable data = TableDraw.ToDataTable(oblist);

            var tb = new DataTable(typeof(T).Name);
            foreach (var prop in props)
            {
                tb.Columns.Add(prop, typeof(string));
            }
            foreach (var item in oblist)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = TableDraw.GetPropValue(item, props[i]);
                }
                tb.Rows.Add(values);
            }

            string[] columnNames = tb.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            DataRow[] rows = tb.Select();
            var table = new ConsoleTable(props);

            foreach (DataRow row in rows)
            {
                table.AddRow(row.ItemArray);
            }
            table.Write(Format.Alternative);

        }
        
    }
    public static class TableDraw
    {
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
