using System;
using System.Collections.Generic;
using System.Linq;
using LINQtoCSV;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace MilkTeaStore
{
    class Program
    {
        static void Main(string[] args)
        {
            Database<Bill>.CreateDatabase();
            while (Menu.statusMenu)
            {
                Menu.WelcomeMenu();
            }

        }
    }

}
