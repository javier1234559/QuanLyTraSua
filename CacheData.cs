﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaStorel
{
    static class CacheData
    {
        public static  List<Oder> oders { get; set; }
        public static List<Customer> customers { get; set; }
        public static List<Product> products { get; set; }
        public static List<Staff> staffs { get; set; }

        public static List<Bill> bills { get; set; }

    }
}