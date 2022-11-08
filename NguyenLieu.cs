namespace MilkTeaStore
{
    class NguyenLieu
    {
        public string nhaCungCap { get; set; }
        public int price { get; set; }
        public int sl { get; set; }


        NguyenLieu(string nhaCungCap, int price, int sl)
        {
            this.nhaCungCap = nhaCungCap;
            this.price = price;
            this.sl = sl;
        }

    }
}
