using System.Collections.Generic;

namespace TeaStorel
{
    class Order
    {
        public int BillID { get; set; }
        public int ProductID { get; set; }
        public int ProductQuantity { get; set; }


        public Order() { }

        public Order(int billID, int productID, int productQuantity)
        {
            BillID = billID;
            ProductID = productID;
            ProductQuantity = productQuantity;
        }


        //ham xu ly 1 Order
        public void AddThisOrder()
        {
            if (CacheData.orders == null)
            {
                CacheData.orders = Database<Order>.readFile(Database<Order>.OrderFilePath);
            }
            List<Product> products = Database<Product>.readFile(Database<Product>.ProductFilePath);
            List<Bill> bills = Database<Bill>.readFile(Database<Bill>.BillFilePath);

            //Add this Order
            while (true)
            {

                //Check id Order
                while (true)
                {
                    Console.Write("Nhap id de order :");
                    this.ProductID = Int32.Parse(Console.ReadLine());
                    var list = from p in products
                               where p.ProductID == this.ProductID
                               select p;
                    if (list.Any(cus => cus.ProductID == this.ProductID))
                        break;
                    Console.WriteLine("Ma san pham khong hop le vui long nhap lai ! ");
                };

                //Add Order to CacheData.oders
                this.BillID = bills.Any() ? bills.Max(x => x.BillID) + 1 : 1; // tang id cua Bill len 1
                Console.Write("Nhap so luong : ");
                this.ProductQuantity = Int32.Parse(Console.ReadLine());
                CacheData.orders.Add(this);
                Console.Write("Them 1 order nua ? (y/n)");
                string c = Console.ReadLine();
                if (c == "n") break;
            }

            //Print this Oder List
            PrintThisOrderList();
            Console.WriteLine("<---- Back");
            Console.ReadLine();
        }

        public bool DeleteProductInOrder()
        {
            List<Product> products = Database<Product>.readFile(Database<Product>.ProductFilePath);

            //Check id product
            while (true)
            {
                Console.Write("Nhap id san pham muon xoa :");
                this.ProductID = Int32.Parse(Console.ReadLine());
                if (CacheData.orders == null) return false;
                try
                {
                    var list = from o in CacheData.orders
                               where o.ProductID == this.ProductID
                               select o;
                    if (list.Any(order => order.ProductID == this.ProductID))
                        break;
                }
                catch
                {
                    Console.WriteLine("Ma san pham khong hop le vui long nhap lai ! ");

                }
            };

            //Delete product in CacheData.oders
            CacheData.orders.RemoveAll(x => x.ProductID == this.ProductID);
            Console.WriteLine("Xoa san pham co id = " + this.ProductID + " khoi danh sach oder thanh cong !");

            //Print this OrderList
            PrintThisOrderList();
            Console.WriteLine("<---- Back");
            Console.ReadLine();

            return true;
        }

        public void PrintThisOrderList()
        {
            Console.WriteLine("Danh sach order : ");
            if (CacheData.orders != null)
            {
                var listorder = CacheData.orders.Where(o => o.BillID == this.BillID);
                TableDraw.Table(listorder);
            }
        }

        public bool AddThisOrderToDatabase()
        {
            try
            {
                Database<Order>.writeFile(CacheData.orders, Database<Order>.OrderFilePath); //add to database
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }


        //Ham xu ly CRUD manager lien quan den Order


    }
}
