namespace MilkTeaStore
{
    class Discount
    {
        public int DiscountID { get; set; }
        public string DiscountVal { get; set; }
        public double percentDiscount { get; set; }
        public string DiscountName { get; set; }
        public List<Discount> DiscountList { get; set; }
        public Discount()
        {
        }
        public Discount(int discountID, string discountVal,double percentDiscount, string discountName)
        {
            this.percentDiscount=percentDiscount;
            DiscountID = discountID;
            DiscountVal = discountVal;
            DiscountName = discountName;
        }
        public int CheckDiscount(string day)
        {
            DiscountList = Database<Discount>.readFile(Database<Discount>.DiscountFilePath);

            var query = from d in DiscountList
                        where d.DiscountVal == day
                        select new { id = d.DiscountID };
            int id = 0;
            foreach(var item in query)
            {
                id = item.id;
            }
            return id;
        }
    }
}
