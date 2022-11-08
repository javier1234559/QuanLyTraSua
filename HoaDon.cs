namespace MilkTeaStore
{
    class HoaDon {
        public string nameKH;
        public Oder dsOder;
        public Uudai uudai;
        //Datetime thoigianmua 

        public HoaDon( Oder dsOder, string nameKH) 
        {
            this.dsOder = dsOder;
            this.nameKH = nameKH;
        }
        public void printHoaDon()
        {
            Console.WriteLine("Hoa don cua khach hanng " + nameKH);
            foreach(Product i in dsOder.ListDrink)
            {
                Console.WriteLine(i.ToString());
            }
            Console.WriteLine(checkuudai());
        }
        public double checkuudai() { return 0; }
        
    }
}
