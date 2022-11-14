namespace MilkTeaStore
{
    class Bill {
        public int BillID { get; set; }
        public int CusID { get; set; }
        public int StaffID { get; set; }
        public string Date;
        public int DiscountID { get; set; }
        public double Total { get; set; }
        public List<Bill> bills { get; set; }

        public Bill()
        {

        }
        public Bill(int billId )
        {
            BillID = billId;
        }
        public Bill(int cusID, int staffID, string date, int DiscountID)
        {
            CusID = cusID;
            StaffID = staffID;
            Date = date;
            this.DiscountID = DiscountID;
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
        public void addBill()
        {
            bills = Database<Bill>.readFile(Database<Bill>.BillFilePath);

            this.BillID = bills.Any() ? bills.Max(x => x.BillID) + 1 : 1; // tang id cua Bill len 1
            this.Total = this.TotalBill();
            bills.Add(new Bill(this.BillID,this.CusID,this.StaffID,this.Date,this.DiscountID,this.Total));

            printBill();
            Console.WriteLine("\n1.Bam bat ki de xuat Hoa Don ");
            Console.WriteLine("2.Bam 2 de huy Hoa Don ");
            string check = Console.ReadLine();
            if(check == "2") {
                Menu.statusMenu = false;
            }
            else
            {
                this.addBillToDataBase();
                Console.Clear();
                printBill();
                Console.WriteLine("Cam on quy khach !");
            }
            
        }
        public bool addBillToDataBase()
        {
            try
            {
                Database<Bill>.writeFile(bills, Database<Bill>.BillFilePath); //add to database
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }
        public void printBill()
        {
            Console.WriteLine("Danh sach bill : ");
            var list = bills.Where(o => o.BillID == this.BillID);
            Database<Bill>.Table(list);
        }
        public bool deletethisBill()
        {
            List<Oder> oders = Database<Oder>.readFile(Database<Oder>.OderFilePath);
            this.bills = Database<Bill>.readFile(Database<Bill>.BillFilePath);
            //--Check id bill
            while (true)
            {
                var list = from o in oders
                           where o.BillID == this.BillID
                           select o;
                if (list.Any(oder => oder.BillID == this.BillID)) { 
                    oders.RemoveAll(x => x.BillID == this.BillID);
                    break;
                }
                Console.WriteLine("Xoa khong thanh cong - co the do khong ton tai trong bang !");
            };
         
            Database<Oder>.writeFile(oders, Database<Oder>.OderFilePath); //add to database
            return true;
        }
        public double TotalBill()
        {
            double sum = 0;
            List<Discount> discounts = Database<Discount>.readFile(Database<Discount>.DiscountFilePath);
            List<Oder> oders = Database<Oder>.readFile(Database<Oder>.OderFilePath);
            List<Product> products = Database<Product>.readFile(Database<Product>.ProductFilePath);
            var list = from o in oders
                       from p in products
                       where o.BillID == this.BillID && o.ProductID == p.ProductID
                       select new { 
                            price = p.Price,
                            quantity = o.ProductQuantity
                        };
            foreach (var o in list) {
                sum += o.price * o.quantity;
            }
            var discountvalue = discounts.Where(d => d.DiscountID == this.DiscountID);
            
            double percentdiscount = discountvalue.FirstOrDefault() == null ? 0 : discountvalue.FirstOrDefault().percentDiscount;

            sum = percentdiscount * sum;
            return sum;
        }
        // void xulyngaydathang()
    }
}
