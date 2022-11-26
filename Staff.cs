namespace TeaStorel
{
    class Staff : Person
    {
        public int StaffId { get; set; }
        public string WorkSchedule { get; set; }
        public string Position { get; set; }
        public long Salary { get; set; }
        public int AbsentDay { get; set; } = 0;


        public Staff() { }

        public Staff(int id, string name, string numberphone, string address, string WorkSchedule, string Position, long Salary, int absentday) : base(name, numberphone, address)
        {
            this.StaffId = id;
            this.Position = Position;
            this.Salary = Salary;
            this.WorkSchedule = WorkSchedule;
            this.AbsentDay = absentday;
        }


        //Ham xu ly 1 Stafff
        public void LoginMenuThisStaff()
        {
            Console.Clear();
            Console.WriteLine(String.Format("{0}{1,-55}", "", "Nhan thong tin dang nhap"));
            Console.WriteLine(String.Format("{0}{1,-55}", "", "-----------------------"));
            Console.WriteLine(String.Format("{0}{1,-15}", "1.", this.Name == null ? "Nhap ten nhan vien" : String.Format("Ten nhan vien:{0,11}{1,2}", "|", Name)));
            Console.WriteLine(String.Format("{0}{1,-15}", "2.", this.Numberphone == null ? "Nhap so dien thoai" : String.Format("So dien thoai:{0,12}{1,2}", "|", Numberphone)));
            Console.WriteLine("---------------------");
        }

        public bool CheckAndLoginThisStaff()
        {
            List<Staff> staffs = Database<Staff>.readFile(Database<Staff>.StaffFilePath);
            try
            {
                var query = from o in staffs
                            where o.Numberphone == this.Numberphone && o.Name == this.Name
                            select o;

                var e = query.First();
                this.StaffId = e.StaffId;
                this.Address = e.Address;
                this.Position = e.Position;
                this.Salary = e.Salary;
                this.WorkSchedule = e.WorkSchedule;
                this.AbsentDay = e.AbsentDay;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void PrintThisStaffs()
        {
            CacheData.staffs = Database<Staff>.readFile(Database<Staff>.StaffFilePath);
            var list = CacheData.staffs.Where(o => o.StaffId == this.StaffId);
            TableDraw.Table(list);
            Console.ReadLine();
        }

        public void HistoryOderForCustomer()
        {
            List<Bill> billList = Database<Bill>.readFile(Database<Bill>.BillFilePath);

            var query = from b in billList
                        where b.StaffID == this.StaffId
                        select b;

            Console.Clear();
            Console.WriteLine("\nLich su order cho khach cua nhan vien " + this.Name);
            TableDraw.Table(query);
            Console.WriteLine("<------ Back");
            Console.ReadLine();

        }


        //Ham xu ly CRUD manager lien quan den Staff
        public void AddManageStaff()
        {
            CacheData.staffs = Database<Staff>.readFile(Database<Staff>.StaffFilePath);

            //Add staff
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
            this.StaffId = CacheData.staffs.Any() ? CacheData.staffs.Max(x => x.StaffId) + 1 : 1; // tang id cua Staff len 1

            //Add product to CacheData.staffs
            CacheData.staffs.Add(this);
            Console.WriteLine("Them thanh cong !");

            //Print Table
            TableDraw.Table(CacheData.staffs);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
        }

        public bool DeleteManageStaff()
        {
            CacheData.staffs = Database<Staff>.readFile(Database<Staff>.StaffFilePath);

            //Check id Staff
            int id;
            while (true)
            {
                Console.Write("Nhap id nhan vien muon xoa :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in CacheData.staffs
                           where o.StaffId == id
                           select o;
                if (list.Any(oder => oder.StaffId == id))
                    break;
                Console.WriteLine("Ma nhan vien khong hop le vui long nhap lai ! ");
            };

            //Delete staff
            CacheData.staffs.RemoveAll(x => x.StaffId == id);
            Console.WriteLine("Xoa thanh cong !");

            //Print Table
            TableDraw.Table(CacheData.staffs);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
            return true;
        }

        public bool EditManageStaff()
        {
            CacheData.staffs = Database<Staff>.readFile(Database<Staff>.StaffFilePath);

            //Check id Staff
            int id;
            while (true)
            {
                Console.Write("Nhap id nhan vien muon sua :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in CacheData.staffs
                           where o.StaffId == id
                           select o;
                if (list.Any(oder => oder.StaffId == id))
                    break;
                Console.WriteLine("Ma nhan vien khong hop le vui long nhap lai ! ");
            };
            CacheData.staffs.RemoveAll(x => x.StaffId == id);
            this.StaffId = id;

            //Edit Staff
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

            //Add product to CacheData.staffs
            CacheData.staffs.Add(this);
            Console.WriteLine("Sua thanh cong !");

            //Print Table
            TableDraw.Table(CacheData.staffs);
            Console.WriteLine("<---- Back");
            Console.ReadLine();

            return true;
        }

        public bool AddManageStafftoDataBase()
        {
            try
            {
                var enumerable = from o in CacheData.staffs
                                 orderby o.StaffId ascending
                                 select o;
                List<Staff> oderByList = enumerable.ToList();
                Database<Staff>.writeFile(oderByList, Database<Staff>.StaffFilePath); //add to database
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }
    }
}