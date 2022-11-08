using MilkTeaStore;

namespace MilkTeaStore
{
    class Oder
    {
        public double discountCode;
        public List<Product> ListDrink;
        public Oder(){
            ListDrink = new List<Product> ();
        }
        public Oder(List<Product> listDrink)
        {
            ListDrink = listDrink;
        }
        /*public void addOder()
        {
            Console.Write("Hay nhap so luong : ");
            int sl = Int32.Parse(Console.ReadLine());
            for(int i = 0; i < sl; i++)
            {
                Console.WriteLine("Hay nhap ten sp ");
                String name = Console.ReadLine();
                Console.WriteLine("Hay nhap kich thuoc [S,M,L]  ");
                string tem = Console.ReadLine().ToUpper(); 
                SIZE size = Enum.Parse<SIZE>(tem);
                Console.WriteLine("Hay nhap so luong : ");
                int productSl = Int32.Parse(Console.ReadLine());

                Menu menu = new Menu();
                int index = menu.checkNameMenu(name);
                int price = menu.getPricebyIndex(index);
                int originalPrice = menu.getOriginalPrice(index);

                ListDrink.Add(new ThucUong(name, price, size, originalPrice,productSl));
                Console.WriteLine();
            }

        }*/
        /*public void printOderList()
        {
            Console.WriteLine("{0,-10} {1,-10} {2,5}", "Ten sp", "So luong", "Gia");
            foreach (Product thucUong in ListDrink)
            {
                int tonggia = thucUong.Price;
                if(thucUong.slOder >= 2)
                {
                    tonggia *= thucUong.slOder;
                }
                Console.WriteLine("{0,-10} {1,-10} {2,5}", thucUong.Name, thucUong.slOder,tonggia);
            }
        }*/
        public void editOder()
        {
            Console.WriteLine("Hay nhap vi tri chinh sua ");
            int index = Int32.Parse(Console.ReadLine());

        }
        //public void DeleteOder()

        
    }



}
