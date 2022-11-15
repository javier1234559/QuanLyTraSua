using System;
using System.Collections.Generic;
using System.Linq;
using LINQtoCSV;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using System.Xml.Linq;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using ConsoleTables;
using System.Data;
using System.Threading;
using DataRow = System.Data.DataRow;

namespace MilkTeaStore
{
    public static class Table
    {
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            //Database<Bill>.CreateDatabase();
            Menu menu = new Menu();
            while (Menu.statusMenu)
            {
                menu.WelcomeMenu();
            }
            /*List<Customer> customers = Database<Customer>.readFile(Database<Customer>.CustomerFilePath);
            DataTable data = Table.ToDataTable(customers);

            string[] columnNames = data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            DataRow[] rows = data.Select();

            var table = new ConsoleTable(columnNames);

            foreach(DataRow row in rows)
            {
                table.AddRow(row.ItemArray);
            }
            table.Write(Format.Alternative);
            Console.Read();*/
        }

        
    }

}
