namespace TeaStorel
{
    class Discount
    {
        public int DiscountID { get; set; }
        public string DiscountVal { get; set; }
        public double PercentDiscount { get; set; }
        public string DiscountName { get; set; }


        public Discount() { }

        public Discount(int discountID, string discountVal, double percentDiscount, string discountName)
        {
            PercentDiscount = percentDiscount;
            DiscountID = discountID;
            DiscountVal = discountVal;
            DiscountName = discountName;
        }


        //Ham xu ly 1 discount
        public int CheckDiscount(string day)
        {
            CacheData.discounts = Database<Discount>.readFile(Database<Discount>.DiscountFilePath);

            var query = from d in CacheData.discounts
                        where d.DiscountVal == day
                        select new { id = d.DiscountID };
            int id = 0;
            foreach (var item in query)
            {
                id = item.id;
            }
            return id;
        }

        
        //Ham xu ly CRUD manager lien quan den Discount
        public void AddManagerDiscount()
        {
            CacheData.discounts = Database<Discount>.readFile(Database<Discount>.DiscountFilePath);

            //Add discount
            Console.WriteLine("Nhap ten giam gia de them :");
            this.DiscountName = Console.ReadLine();
            Console.WriteLine("Nhap gia tri giam gia de them :");
            this.PercentDiscount = Double.Parse(Console.ReadLine());
            Console.WriteLine("Nhap ngay ap dung ma giam gia :");
            this.DiscountVal = Console.ReadLine();
            this.DiscountID = CacheData.discounts.Any() ? CacheData.discounts.Max(x => x.DiscountID) + 1 : 1; // tang id cua discount len 1

            //Add discount to CacheData.discounts
            CacheData.discounts.Add(this);
            Console.WriteLine("Them thanh cong !");

            //Print Table
            TableDraw.Table(CacheData.discounts);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
        }

        public bool DeleteManageDiscount()
        {
            CacheData.discounts = Database<Discount>.readFile(Database<Discount>.DiscountFilePath);

            //Check id discount
            int id;
            while (true)
            {
                Console.Write("Nhap id giam gia muon xoa :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in CacheData.discounts
                           where o.DiscountID == id
                           select o;
                if (list.Any(oder => oder.DiscountID == id))
                    break;
                Console.WriteLine("Ma giam gia khong hop le vui long nhap lai ! ");
            };

            //Delete discount
            CacheData.discounts.RemoveAll(x => x.DiscountID == id);
            Console.WriteLine("Xoa thanh cong !");

            //Print Table
            TableDraw.Table(CacheData.discounts);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
            return true;
        }

        public bool EditManageDiscount()
        {
            CacheData.discounts = Database<Discount>.readFile(Database<Discount>.DiscountFilePath);

            //Check id discount
            int id;
            while (true)
            {
                Console.Write("Nhap id giam gia muon sua :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in CacheData.discounts
                           where o.DiscountID == id
                           select o;
                if (list.Any(oder => oder.DiscountID == id))
                    break;
                Console.WriteLine("Ma giam gia khong hop le vui long nhap lai ! ");
            };
            CacheData.discounts.RemoveAll(x => x.DiscountID == id);
            this.DiscountID = id;

            //Edit discount
            Console.WriteLine("Nhap ten giam gia de sua :");
            this.DiscountName = Console.ReadLine();
            Console.WriteLine("Nhap gia tri giam gia de sua :");
            this.PercentDiscount = Double.Parse(Console.ReadLine());
            Console.WriteLine("Nhap ngay ap dung ma giam gia :");
            this.DiscountVal = Console.ReadLine();

            //Add discount to CacheData.discounts
            CacheData.discounts.Add(this);
            Console.WriteLine("Sua thanh cong !");

            //Print Table
            TableDraw.Table(CacheData.discounts);
            Console.WriteLine("<---- Back");
            Console.ReadLine();

            return true;
        }

        public bool AddManageDiscounttoDataBase()
        {
            try
            {
                var enumerable = from o in CacheData.discounts
                                 orderby o.DiscountID ascending
                                 select o;
                List<Discount> oderByList = enumerable.ToList();
                Database<Discount>.writeFile(oderByList, Database<Discount>.DiscountFilePath); //add to database
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }

    }
}
