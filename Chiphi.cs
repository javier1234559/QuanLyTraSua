namespace MilkTeaStore
{
    class Chiphi
    {
        public int matbang { get; set; }
        public int chiphiNguyenLieu { get; set; }
        public int luongNhanVien { get; set; }

        public Chiphi()
        {

        }
        public Chiphi(int chiphiNguyenLieu ,int luongNhanVien, int matbang =50000) 
        {

            this.chiphiNguyenLieu = chiphiNguyenLieu;
            this.luongNhanVien = luongNhanVien;
        }


    }
}
