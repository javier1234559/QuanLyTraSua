using System;
using System.Collections.Generic;
using System.Linq;
using LINQtoCSV;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MilkTeaStore
{
    partial class Program
    {

        static void Main(string[] args)
        {
            var customerList = new List<Customer>
            {   
                new Customer("123","Nhat","09422323","40/104fdfsdf"),
                new Customer("123", "Nhat", "09422323", "40/104fdfsdf"),
                new Customer("123", "Nhat", "09422323", "40/104fdfsdf") 
            };
            writeToFile(customerList);

            var list = readFromFile();

            foreach(var customer in list)
            {
                Console.WriteLine($"{customer.Id} ,{customer.Name},{customer.Numberphone},{customer.Address}");
            }



        }

        static void writeToFile(List<Customer> customers)
        {
            var csvFileConfig = new CsvFileDescription
            {
                FirstLineHasColumnNames = true,
                SeparatorChar = ',',
            };
            var csvContext  = new CsvContext();
            /*var eperson = csvContext.Read<Customer>("cus.csv", csvFileConfig);*/

            csvContext.Write(customers, "cus.csv", csvFileConfig);

        }
        static List<Customer> readFromFile()
        {
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };
            CsvContext cc = new CsvContext();
            var customerslist = cc.Read<Customer>("cus.csv", inputFileDescription);

            return customerslist.ToList<Customer>();
        }

    }
}
