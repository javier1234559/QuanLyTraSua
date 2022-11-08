using System.Runtime.ConstrainedExecution;

namespace MilkTeaStore
{
    class Menu
    {
       
        public static List<Product> dsthucuong;

        public Menu()
        {
            int[] dsOriginalPrice = { 50, 100, 150, 200 };
            int[] dsPrice = { 100, 200, 300, 400 };
            string[] dsNname = { "Nuoc Ngot", "Tra Sua", "Nuoc Cam", "Tra Dao" };
            int[] slStocking = { 10, 20, 30, 40 };

            // Dong tren co the thay the bang DOCFILE
            for (int i = 0; i < dsNname.Length; i++)
            {
                dsthucuong.Add( new Product(dsNname[i], dsPrice[i], dsOriginalPrice[i], slStocking[i]));
            }

        }
        public void printMenu() {
            Console.WriteLine("---------------Menu--------------- ");
            Console.WriteLine("{0,-20} {1,5:N1}", "Ten san pham", "Muc gia");
            foreach(var i in dsthucuong.Select((value,index) => (value,index)))
            {
                Console.WriteLine("{0,-20} {1,5}", i.value.Name, i.value.Price);
            }
            Console.WriteLine("---------------------------------- ");
        }

       /* public int checkNameMenu(string name)
        {
            foreach(var i in dsNname.Select((name,index) => (name,index)))
            {
                if (name.Equals(i.name))
                    return i.index;
            }
            return -1;
        }*/
        public int getPricebyIndex(int index)
        {
            int price = 0;
            return price;
        }
      /*  public int getOriginalPrice(int index)
        {
            int originalprice = dsOriginalPrice[index];
            return originalprice;
        }*/
    }
}