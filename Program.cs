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
using ConsoleTables;
using System.Data;
using System.Threading;
using DataRow = System.Data.DataRow;

namespace TeaStorel
{
    class Program
    {
        static void Main(string[] args)
        {
            Database<Bill>.CreateDatabase(); //tao san Database
            Menu menu = new Menu();
            while (Menu.statusMenu)
            {
                menu.MainMenu();
            }

            /* List<Product> products = Database<Product>.readFile(Database<Product>.ProductFilePath);
             string[] labels = { "ProductID", "Name", "Size", "Price" };
             TableDraw.TableHavePropSelected(products, labels);*/

        }


    }

}
