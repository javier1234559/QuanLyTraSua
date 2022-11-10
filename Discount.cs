namespace MilkTeaStore
{
    class Discount
    {
        public string DiscountID { get; set; }
        public float DiscountVal { get; set; }
        public string DiscountName { get; set; }

        public Discount()
        {
        }

        public Discount(string discountID, float discountVal, string discountName)
        {
            DiscountID = discountID;
            DiscountVal = discountVal;
            DiscountName = discountName;
        }
    }
}
