namespace MilkTeaStore
{
    class Supply
    {
        public int ProductID { get; set; }
        public string IngreID { get; set; }
        public string DateSupply { get; set; }
        public int Quantity { get; set; }
        public Supply()
        {

        }
        public Supply(int productID, string ingreID, string dateSupply, int quantity)
        {
            ProductID = productID;
            IngreID = ingreID;
            DateSupply = dateSupply;
            Quantity = quantity;
        }

    }
}
