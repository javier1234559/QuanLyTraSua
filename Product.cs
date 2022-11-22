using System.Collections.Generic;

namespace TeaStorel
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
        public Product(int productID, string name, SIZE size, int price, int originalPrice, int slStocking)
        {
            ProductID = productID;
            Name = name;
            Size = size;
            Price = price;
            OriginalPrice = originalPrice;
            this.quantityInStock = slStocking;
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
        
        //Quan Ly
        public void addManageProduct()
        {
            CacheData.products = Database<Product>.readFile(Database<Product>.ProductFilePath);
            //--Check add product
            Console.WriteLine("Nhap ten san pham de them :");
            this.Name = Console.ReadLine();

            Console.WriteLine("Nhap kich thuoc san pham de them :");// convert to enum
            SIZE kichthuoc;
            string size = Console.ReadLine();
            if (Enum.TryParse<SIZE>(size, out kichthuoc)) ;
            this.Size = kichthuoc;

            Console.WriteLine("Nhap gia von san pham :");
            this.OriginalPrice = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Nhap gia san pham muon ban :");
            this.Price = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Nhap so luong san pham muon ton kho :");
            this.quantityInStock = Int32.Parse(Console.ReadLine());

            this.ProductID = CacheData.products.Any() ? CacheData.products.Max(x => x.ProductID) + 1 : 1; // tang id cua product len 1

            //--Add product to CacheData.products
            CacheData.products.Add(new Product(ProductID,Name,this.Size,this.Price,this.OriginalPrice,this.quantityInStock));
            
            Console.WriteLine("Them thanh cong !");
            Database<Product>.Table(CacheData.products);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
        }
        public bool deleteManageProduct()
        {
            //--Check id sp
            CacheData.products = Database<Product>.readFile(Database<Product>.ProductFilePath);
            int id;
            while (true) { 
                Console.Write("Nhap id san pham muon xoa :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in CacheData.products
                           where o.ProductID == id
                           select o;
                if (list.Any(oder => oder.ProductID == id))
                    break;
                Console.WriteLine("Ma san pham khong hop le vui long nhap lai ! ");
            };

            CacheData.products.RemoveAll(x => x.ProductID == id);

            Console.WriteLine("Xoa thanh cong !");
            Database<Product>.Table(CacheData.products);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
            return true;
        }
        public bool editManageProduct()
        {
            CacheData.products = Database<Product>.readFile(Database<Product>.ProductFilePath);
            //--Check id sp
            int id;
            while (true)
            {
                Console.Write("Nhap id san pham muon sua :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in CacheData.products
                           where o.ProductID == id
                           select o;
                if (list.Any(oder => oder.ProductID == id))
                    break;
                Console.WriteLine("Ma san pham khong hop le vui long nhap lai ! ");
            };
            CacheData.products.RemoveAll(x => x.ProductID == id);
            this.ProductID = id;
            //--Check add product
            Console.WriteLine("Nhap ten san pham de sua :");
            this.Name = Console.ReadLine();
            Console.WriteLine("Nhap kich thuoc san pham de sua :");// convert to enum
            SIZE kichthuoc;
            string size = Console.ReadLine();
            if (Enum.TryParse<SIZE>(size, out kichthuoc)) ;
            this.Size = kichthuoc;

            Console.WriteLine("Nhap gia san pham muon sua :");
            this.Price = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Nhap gia von san pham muon sua:");
            this.OriginalPrice = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Nhap gia san pham muon sua  :");
            this.Price = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Nhap gia san pham muon sua :");
            this.quantityInStock = Int32.Parse(Console.ReadLine());

            
            //--Add product to CacheData.products
            CacheData.products.Add(new Product(this.ProductID, this.Name, kichthuoc, this.Price, this.OriginalPrice, this.quantityInStock));

            Console.WriteLine("Sua thanh cong !");
            Database<Product>.Table(CacheData.products);
            Console.WriteLine("<---- Back");
            Console.ReadLine();

            return true;
        }
        public bool addManageProducttoDataBase()
        {
            try
            {
                var enumerable = from o in CacheData.products
                                 orderby o.ProductID descending
                                 select o;
                List<Product> oderByList = enumerable.ToList();
                Database<Product>.writeFile(oderByList, Database<Product>.ProductFilePath); //add to database
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }
    }
}