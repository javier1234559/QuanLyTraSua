namespace MilkTeaStore
{
    public enum SIZE
    {
        S,
        M,
        L
    }

    class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public SIZE Size { get; set; }
        public int Price { get; set; }
        public int OriginalPrice { get; set; }
        public int bestSeller { get; set; }
        public float rate { get; set; }
        public int quantityInStock { get; set; }
        public Product()
        {

        }
        public Product(int productID, string name, SIZE size, int price, int originalPrice, int bestSeller, float rate, int slStocking)
        {
            ProductID = productID;
            Name = name;
            Size = size;
            Price = price;
            OriginalPrice = originalPrice;
            this.bestSeller = bestSeller;
            this.rate = rate;
            this.quantityInStock = slStocking;
        }

        public void Rating()
        {
            Console.WriteLine("Vui long nhap danh gia");
            this.rate = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Cam on ban da danh gia !");
        }
        public int checkBestSeller()
        {
            // Kiem tra trong file sl da dat nhieu nhat
            return this.bestSeller;
        }
    }
}