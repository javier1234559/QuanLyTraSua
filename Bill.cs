namespace MilkTeaStore
{
    class Bill {
        public int BillID { get; set; }
        public int CusID { get; set; }
        public int StaffID { get; set; }
        public string Date;
        public string DiscountID { get; set; }
        public long Total { get; set; }
        public List<Bill> bills { get; set; }

        public Bill()
        {

        }
        public Bill(int billId )
        {
            BillID = billId;
        }
        public Bill(int billId, int cusID, int staffID, string date, string discountID, long total)
        {
            BillID = billId;
            CusID = cusID;
            StaffID = staffID;
            Date = date;
            DiscountID = discountID;
            Total = total;
        }
        public void printBill()
        {
            Console.WriteLine("Danh sach bill : ");
            this.bills = Database<Bill>.readFile(Database<Bill>.BillFilePath);
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
        public long TotalBill()
        {
            long sum = 0;
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
            return sum;
        }
        // void xulyngaydathang()
    }
}
