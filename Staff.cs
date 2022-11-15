namespace MilkTeaStore
{
    class Staff : Person
    {
        public int StaffId { get; set; }
        public string WorkSchedule { get; set; }
        public string Position { get; set; }
        public long Salary { get; set; }
        public int AbsentDay { get; set; } = 0;

        public List<Staff> staffs;
        public Staff()
        {
        }

        public Staff(int id, string name, string numberphone, string address, string WorkSchedule, string Position, long Salary,int absentday) : base(name, numberphone, address)
        {
            this.StaffId = id;
            this.Position = Position;
            this.Salary = Salary;
            this.WorkSchedule = WorkSchedule;
            this.AbsentDay = absentday;
        }
        public void printStaff()
        {
            staffs = Database<Staff>.readFile(Database<Staff>.StaffFilePath);
            var list = staffs.Where(o => o.StaffId == this.StaffId);
            Database<Staff>.Table(staffs);
        }
        public void addStaff()
        {
            this.staffs = Database<Staff>.readFile(Database<Staff>.StaffFilePath);
            //--Check add Staff
            Console.WriteLine("Nhap ten nhan vien de them :");
            this.Name = Console.ReadLine();
            Console.WriteLine("Nhap dia chi nhan vien de them :");
            this.Address = Console.ReadLine();
            Console.WriteLine("Nhap so dien thoai nhan vien de them :");
            this.Numberphone = Console.ReadLine();
            Console.WriteLine("Nhap nhap lich lam viec de them :");
            this.WorkSchedule = Console.ReadLine(); 
            Console.WriteLine("Nhap chuc vu de them :");
            this.Position = Console.ReadLine();
            Console.WriteLine("Nhap ngay nghi de them :");
            this.AbsentDay =Int32.Parse(Console.ReadLine());
            Console.WriteLine("Nhap muc luong de them :");
            this.Salary = Int32.Parse(Console.ReadLine());

            this.StaffId = staffs.Any() ? staffs.Max(x => x.StaffId) + 1 : 1; // tang id cua Staff len 1

            //--Add Staff to Staffs
            staffs.Add(this);

            Console.WriteLine("Them thanh cong !");
            Database<Staff>.Table(staffs);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
        }
        public bool deleteStaff()
        {
            //--Check id sp
            this.staffs = Database<Staff>.readFile(Database<Staff>.StaffFilePath);
            int id;
            while (true)
            {
                Console.Write("Nhap id nhan vien muon xoa :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in staffs
                           where o.StaffId == id
                           select o;
                if (list.Any(oder => oder.StaffId == id))
                    break;
                Console.WriteLine("Ma nhan vien khong hop le vui long nhap lai ! ");
            };

            staffs.RemoveAll(x => x.StaffId == id);

            Console.WriteLine("Xoa thanh cong !");
            Database<Staff>.Table(staffs);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
            return true;
        }
        public bool editStaff()
        {
            this.staffs = Database<Staff>.readFile(Database<Staff>.StaffFilePath);
            //--Check id sp
            int id;
            while (true)
            {
                Console.Write("Nhap id san pham muon sua :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in staffs
                           where o.StaffId == id
                           select o;
                if (list.Any(oder => oder.StaffId == id))
                    break;
                Console.WriteLine("Ma san pham khong hop le vui long nhap lai ! ");
            };
            staffs.RemoveAll(x => x.StaffId == id);

            //--Check add Staff
            Console.WriteLine("Nhap ten nhan vien de them :");
            this.Name = Console.ReadLine();
            Console.WriteLine("Nhap dia chi nhan vien de them :");
            this.Address = Console.ReadLine();
            Console.WriteLine("Nhap so dien thoai nhan vien de them :");
            this.Numberphone = Console.ReadLine();
            Console.WriteLine("Nhap nhap lich lam viec de them :");
            this.WorkSchedule = Console.ReadLine();
            Console.WriteLine("Nhap chuc vu de them :");
            this.Position = Console.ReadLine();
            Console.WriteLine("Nhap ngay nghi de them :");
            this.AbsentDay = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Nhap muc luong de them :");
            this.Salary = Int32.Parse(Console.ReadLine());

            this.StaffId = staffs.Any() ? staffs.Max(x => x.StaffId) + 1 : 1; // tang id cua Staff len 1

            //--Add Staff to Staffs
            this.staffs.Add(this);

            Console.WriteLine("Sua thanh cong !");
            Database<Staff>.Table(this.staffs);
            Console.WriteLine("<---- Back");
            Console.ReadLine();

            return true;
        }
        public bool addStafftoDataBase()
        {
            try
            {
                Database<Staff>.writeFile(this.staffs, Database<Staff>.StaffFilePath); //add to database
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }
    }
}