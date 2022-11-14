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

namespace MilkTeaStore
{
    class kqquery{


    }
    class Program
    {
        static void Main(string[] args)
        {
           /* Database<Bill>.CreateDatabase();*/
            /*while (Menu.statusMenu)
            {
                Menu.WelcomeMenu();
            }*/
            Bill bill = new Bill(1);

            long a =bill.TotalBill();
            Console.WriteLine(a);
        }
    }

}
