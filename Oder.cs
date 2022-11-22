using System.Collections.Generic;

namespace TeaStorel
{
    class Oder
    {
       
        public int BillID { get; set; }
        public  int ProductID { get; set; }
        public int ProductQuantity { get; set; }
        
       
        public Oder(){

        }
        public Oder(int billID, int productID, int productQuantity)
        {
            BillID = billID;
            ProductID = productID;
            ProductQuantity = productQuantity;
        }
        
        //Xua ly oder
        public void addOder()
        {
            CacheData.oders = Database<Oder>.readFile(Database<Oder>.OderFilePath);
            List<Product> products = Database<Product>.readFile(Database<Product>.ProductFilePath);
            List<Bill> bills = Database<Bill>.readFile(Database<Bill>.BillFilePath);
            this.ProductID = 0;

            while (true)
            {
                //--Check add oder
                while (true)
                {
                    Console.Write("Nhap id de oder :");
                    this.ProductID = Int32.Parse(Console.ReadLine());
                    var list = from p in products
                               where p.ProductID == this.ProductID
                               select p;
                    if (list.Any(cus => cus.ProductID == this.ProductID))
                        break;
                    Console.WriteLine("Ma san pham khong hop le vui long nhap lai ! ");
                };
                Console.Write("Nhap so luong : ");
                this.ProductQuantity = Int32.Parse(Console.ReadLine());

                //--Add oder to oders 
                this.BillID = bills.Any() ? bills.Max(x => x.BillID) + 1 : 1; // tang id cua Bill len 1
                CacheData.oders.Add(new Oder(this.BillID, this.ProductID, this.ProductQuantity));
                Console.Write("Them 1 oder nua ? (y/n)");
                string c = Console.ReadLine();
                if (c == "n") break;
            }

            printOderList();
            Console.WriteLine("<---- Back");
            Console.ReadLine();
        }
        public bool addOderToDatabase(){
            try { 
                Database<Oder>.writeFile(CacheData.oders, Database<Oder>.OderFilePath); //add to database
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }
        public bool deleteProductInOder()
        {
            List<Product> products = Database<Product>.readFile(Database<Product>.ProductFilePath);
            //--Check id sp
            while (true)
            {
                Console.Write("Nhap id san pham muon xoa :");
                this.ProductID = Int32.Parse(Console.ReadLine());
                if (CacheData.oders == null) return false;
                var list = from o in CacheData.oders
                           where o.ProductID == this.ProductID
                           select o;
                if (list.Any(oder => oder.ProductID == this.ProductID))
                    break;
                Console.WriteLine("Ma san pham khong hop le vui long nhap lai ! ");
            };

            CacheData.oders.RemoveAll(x => x.ProductID == this.ProductID);

            printOderList();
            Console.WriteLine("<---- Back");
            Console.ReadLine();

            return true;
        }
        public void printOderList()
        {
            Console.WriteLine("Danh sach oder : ");
            if (CacheData.oders != null)
            {
                var listoder = CacheData.oders.Where(o => o.BillID == this.BillID);
                Database<Oder>.Table(listoder);
            }
        }

        //Quan ly Oder  
       /* public void printOder()
        {
            CacheData.oders = Database<Oder>.readFile(Database<Oder>.OderFilePath);
            var list = CacheData.oders.Where(o => o. == this.CusId);
            Database<Oder>.Table(CacheData.oders);
        }
        public void addManageOder()
        {
            this.CacheData.oders = Database<Oder>.readFile(Database<Oder>.OderFilePath);
            //--Check add Oder
            Console.WriteLine("Nhap ten khach hang de them :");
            this.Name = Console.ReadLine();
            Console.WriteLine("Nhap so dien thoai khach hang de them :");
            this.Numberphone = Console.ReadLine();
            Console.WriteLine("Nhap dia chi khach hang de them :");
            this.Address = Console.ReadLine();

            this.CusId = CacheData.oders.Any() ? CacheData.oders.Max(x => x.CusId) + 1 : 1; // tang id cua Oder len 1

            //--Add Oder to Oders
            CacheData.oders.Add(this);

            Console.WriteLine("Them thanh cong !");
            Database<Oder>.Table(CacheData.oders);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
        }
        public bool deleteManageOder()
        {
            //--Check id sp
            this.CacheData.oders = Database<Oder>.readFile(Database<Oder>.OderFilePath);
            int id;
            while (true)
            {
                Console.Write("Nhap id nhan vien muon xoa :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in CacheData.oders
                           where o.CusId == id
                           select o;
                if (list.Any(oder => oder.CusId == id))
                    break;
                Console.WriteLine("Ma nhan vien khong hop le vui long nhap lai ! ");
            };

            CacheData.oders.RemoveAll(x => x.CusId == id);

            Console.WriteLine("Xoa thanh cong !");
            Database<Oder>.Table(CacheData.oders);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
            return true;
        }
        public bool editManageOder()
        {
            this.CacheData.oders = Database<Oder>.readFile(Database<Oder>.OderFilePath);
            //--Check id sp
            int id;
            while (true)
            {
                Console.Write("Nhap id nhan vien muon sua :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in CacheData.oders
                           where o.CusId == id
                           select o;
                if (list.Any(oder => oder.CusId == id))
                    break;
                Console.WriteLine("Ma nhan vien khong hop le vui long nhap lai ! ");
            };
            CacheData.oders.RemoveAll(x => x.CusId == id);

            this.CusId = id;
            //--Check add Oder
            Console.WriteLine("Nhap ten khach hang de sua :");
            this.Name = Console.ReadLine();
            Console.WriteLine("Nhap so dien thoai khach hang de sua :");
            this.Numberphone = Console.ReadLine();
            Console.WriteLine("Nhap dia chi khach hang de sua :");
            this.Address = Console.ReadLine();

            //--Add Oder to Oders
            this.CacheData.oders.Add(this);

            Console.WriteLine("Sua thanh cong !");
            Database<Oder>.Table(this.CacheData.oders);
            Console.WriteLine("<---- Back");
            Console.ReadLine();

            return true;
        }
        public bool addManageOdertoDataBase()
        {
            try
            {
                var enumerable = from o in this.CacheData.oders
                                 orderby o.CusId descending
                                 select o;
                List<Oder> oderByList = enumerable.ToList();

                Database<Oder>.writeFile(oderByList, Database<Oder>.OderFilePath); //add to database
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }*/

    }

}
