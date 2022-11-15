namespace MilkTeaStore
{
    class Ingredient
    {
        public int IngreID { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public string Supplier { get; set; }

        public Ingredient()
        {

        }
        public Ingredient(int ingreID, string name, long price, string supplier)
        {
            IngreID = ingreID;
            Name = name;
            Price = price;
            Supplier = supplier;
        }
    }
}
