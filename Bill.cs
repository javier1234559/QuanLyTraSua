namespace TeaStorel
{
    class Bill
    {
        public int BillID { get; set; }
        public int CusID { get; set; }
        public int StaffID { get; set; }
        public int DiscountID { get; set; }
        public string Date { get; set; }
        public double Total { get; set; }


        public Bill() { }
        public Bill(int billId)
        {
            BillID = billId;
        }
        public Bill(int cusID, int staffID, string date, int discountID)
        {
            CusID = cusID;
            StaffID = staffID;
            Date = date;
            DiscountID = discountID;
        }
        public Bill(int cusID, string date, int discountID)
        {
            CusID = cusID;
            Date = date;
            DiscountID = discountID;
        }
        public Bill(int billId, int cusID, int staffID, string date, int discountID, double total)
        {
            BillID = billId;
            CusID = cusID;
            StaffID = staffID;
            Date = date;
            DiscountID = discountID;
            Total = total;
        }


        //Ham xu ly 1 Bill
        public void PrintThisBill()
        {
            Console.WriteLine("Danh sach bill : ");
            var list = CacheData.bills.Where(o => o.BillID == this.BillID);
            TableDraw.Table(list);
        }

        public double CalculateTotalThisBill()
        {
            double sum = 0;
            List<Discount> discounts = Database<Discount>.readFile(Database<Discount>.DiscountFilePath);
            List<Order> orders = Database<Order>.readFile(Database<Order>.OrderFilePath);
            List<Product> products = Database<Product>.readFile(Database<Product>.ProductFilePath);

            //Xu ly tinh tong gia
            var list = from o in orders
                       from p in products
                       where o.BillID == this.BillID && o.ProductID == p.ProductID
                       select new
                       {
                           price = p.Price,
                           quantity = o.ProductQuantity
                       };
            foreach (var o in list)
            {
                sum += o.price * o.quantity;
            }

            //Xu ly giam gia
            var discountvalue = discounts.Where(d => d.DiscountID == this.DiscountID);
            double percentdiscount = discountvalue.FirstOrDefault() == null ? 0 : discountvalue.FirstOrDefault().PercentDiscount;
            double temp = percentdiscount * sum;
            sum = sum - temp;

            return sum;
        }

        public void ConfirmThisBill()
        {
            Console.WriteLine("\n1.Bam bat ki de xuat Hoa Don ");
            Console.WriteLine("2.Bam 2 de huy Hoa Don ");
            string check = Console.ReadLine();
            if (check == "2")
            {
                Menu.statusMenu = false;
                return;
            }
            else
            {
                this.AddThisBillToDataBase();
                Console.Clear();
                PrintThisBill();
                Console.WriteLine("Cam on quy khach !");
                Console.ReadLine();
            }
        }

        public void AddThisBillToCache()
        {
            CacheData.bills = Database<Bill>.readFile(Database<Bill>.BillFilePath);

            //Add TotalBill
            this.BillID = CacheData.bills.Any() ? CacheData.bills.Max(x => x.BillID) + 1 : 1; // tang id cua Bill len 1
            this.Total = this.CalculateTotalThisBill();

            //Add this bill to CacheData.bills
            CacheData.bills.Add(this);
            Console.WriteLine("Them Oder vao Bill thanh cong ! ");

            //Print this Bill
            PrintThisBill();
            Console.ReadLine();

            //Confirm this Bill
            ConfirmThisBill();

        }

        public bool AddThisBillToDataBase()
        {
            try
            {
                Database<Bill>.writeFile(CacheData.bills, Database<Bill>.BillFilePath); //add to database
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }


        //Ham xu ly CRUD Manager lien quan den Bill


        public bool DeleteManageBill()
        {
            CacheData.bills = Database<Bill>.readFile(Database<Bill>.BillFilePath);

            //Check id product
            int id;
            while (true)
            {
                Console.Write("Nhap id san pham muon xoa :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in CacheData.bills
                           where o.BillID == id
                           select o;
                if (list.Any(oder => oder.BillID == id))
                    break;
                Console.WriteLine("Ma san pham khong hop le vui long nhap lai ! ");
            };

            //Delete product
            CacheData.bills.RemoveAll(x => x.BillID == id);
            Console.WriteLine("Xoa thanh cong !");

            //Print Table
            TableDraw.Table(CacheData.bills);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
            return true;
        }

        public bool AddManageBilltoDataBase()
        {
            try
            {
                var enumerable = from o in CacheData.bills
                                 orderby o.BillID ascending
                                 select o;
                List<Bill> oderByList = enumerable.ToList();
                Database<Bill>.writeFile(oderByList, Database<Bill>.BillFilePath); //add to database
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }


    }
}
