namespace MilkTeaStore
{
    enum SIZE
    {
        S,
        M,
        L
    }
    
    class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public SIZE Size { get; set; }
        public int OriginalPrice { get; set; }
        public int bestSeller { get; set; }
        public int rate { get; set; }
        public int slStocking { get; set; }
        public Product()
        {

        }
        public Product(string name, int price, int originalPrice, int sl)
        {
            Name = name;
            Price = price;
            OriginalPrice = originalPrice;
            slStocking = sl;
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