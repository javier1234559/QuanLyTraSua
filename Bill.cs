namespace MilkTeaStore
{
    class Bill {
        public int BillID { get; set; }
        public int CusID { get; set; }
        public int StaffID { get; set; }
        public string Date;
        public string DiscountID { get; set; }
        public long Total { get; set; }

        public Bill()
        {

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
            List<Bill> bills = Database<Bill>.readFile(Database<Bill>.BillFilePath);
            var list = bills.Where(o => o.BillID == this.BillID);
            Database<Bill>.Table(list);
        }
        public bool deletethisBill()
        {
            List<Oder> oders = Database<Oder>.readFile(Database<Oder>.OderFilePath);
            List<Bill> bills = Database<Bill>.readFile(Database<Bill>.BillFilePath);
            //--Check id bill
            int id = this.BillID;
            while (true)
            {
                var list = from o in oders
                           where o.ProductID == id
                           select o;
                if (list.Any(oder => oder.ProductID == id)) { 
                    oders.RemoveAll(x => x.ProductID == id);
                    break;
                }
                Console.WriteLine("Xoa khong thanh cong - co the do khong ton tai trong bang !");
            };
            Database<Oder>.writeFile(oders, Database<Oder>.OderFilePath); //add to database

            return true;
        }

        // void xulyngaydathang()
        // long TotalBill() == Total attribute
    }
}
