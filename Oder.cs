using MilkTeaStore;
using System.Collections.Generic;

namespace MilkTeaStore
{
    class Oder
    {
        public int BillID { get; set; }
        public  int ProductID { get; set; }
        public int ProductQuantity { get; set; }
        public List<Oder> oders { get; set; }
        public Oder(){

        }
        public Oder(int billID, int productID, int productQuantity)
        {
            BillID = billID;
            ProductID = productID;
            ProductQuantity = productQuantity;
        }
        public void addOder()
        {
            this.oders = Database<Oder>.readFile(Database<Oder>.OderFilePath);
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
                Console.Write("Nhap so luong va bam c sau do de hoan tat: ");
                this.ProductQuantity = Int32.Parse(Console.ReadLine());

                //--Add oder to oders 
                this.BillID = bills.Any() ? bills.Max(x => x.BillID) + 1 : 1; // tang id cua Bill len 1
                oders.Add(new Oder(this.BillID, this.ProductID, this.ProductQuantity));
                Console.Write("----------------");
                string c = Console.ReadLine();
                if (c == "c") break;
            }

            printOderList();
            Console.WriteLine("<---- Back");
            Console.ReadLine();
        }
        public bool addOderToDatabase(){
            try { 
                Database<Oder>.writeFile(this.oders, Database<Oder>.OderFilePath); //add to database
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }
        public bool deleteOder()
        {
            List<Product> products = Database<Product>.readFile(Database<Product>.ProductFilePath);
            //--Check id sp
            int id;
            while (true)
            {
                Console.Write("Nhap id san pham muon xoa :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in oders
                           where o.ProductID == id
                           select o;
                if (list.Any(oder => oder.ProductID == id))
                    break;
                Console.WriteLine("Ma san pham khong hop le vui long nhap lai ! ");
            };

            oders.RemoveAll(x => x.ProductID == id);

            printOderList();
            Console.WriteLine("<---- Back");
            Console.ReadLine();

            return true;
        }
        public void printOderList()
        {
            Console.WriteLine("Danh sach oder : ");
            var listoder = oders.Where(o => o.BillID == this.BillID);
            Database<Oder>.Table(listoder);
        }
    }



}
