namespace MilkTeaStore
{
    class Staff : Person
    {
        public int StaffId { get; set; }
        public string WorkSchedule { get; set; }
        public string Position { get; set; }
        public long Salary { get; set; }
        public int AbsentDay { get; set; } = 0;

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
            List<Staff> staffs = Database<Staff>.readFile(Database<Staff>.StaffFilePath);
            var list = staffs.Where(o => o.StaffId == this.StaffId);
            Database<Staff>.Table(staffs);
        }
        public bool deletethisStaff()
        {
            List<Staff> staffs = Database<Staff>.readFile(Database<Staff>.StaffFilePath);
            List<Bill> bills = Database<Bill>.readFile(Database<Bill>.BillFilePath);
            //--Check id bill
            int id = this.StaffId;
            while (true)
            {
                var list = from o in staffs
                           where o.StaffId == id
                           select o;
                if (list.Any(oder => oder.StaffId == id))
                {
                    staffs.RemoveAll(x => x.StaffId == id);
                    break;
                }
                Console.WriteLine("Xoa khong thanh cong - co the do khong ton tai trong bang !");
            };
            Database<Staff>.writeFile(staffs, Database<Staff>.StaffFilePath); //add to database

            return true;
        }
    }
}