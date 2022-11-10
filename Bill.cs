namespace MilkTeaStore
{
    class Bill {
        public string BillID { get; set; }
        public string CusID { get; set; }
        public string StaffID { get; set; }
        public string Date;
        public string DiscountID { get; set; }
        public long Total { get; set; }

        public Bill()
        {

        }

        public Bill(string billId, string cusID, string staffID, string date, string discountID, long total)
        {
            BillID = billId;
            CusID = cusID;
            StaffID = staffID;
            Date = date;
            DiscountID = discountID;
            Total = total;
        }
        
    }
}
