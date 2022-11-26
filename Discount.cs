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


    }
}
