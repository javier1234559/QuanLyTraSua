namespace MilkTeaStore
{
    class Staff : Person
    {
        public string StaffId { get; set; }
        public string WorkSchedule { get; set; }
        public string Position { get; set; }
        public long Salary { get; set; }
        public int AbsentDay { get; set; } = 0;

        public Staff()
        {
        }

        public Staff(string id, string name, string numberphone, string address, string WorkSchedule, string Position, long Salary,int absentday) : base(name, numberphone, address)
        {
            this.StaffId = id;
            this.Position = Position;
            this.Salary = Salary;
            this.WorkSchedule = WorkSchedule;
            this.AbsentDay = absentday;
        }
        public override void Output()
        {
            Console.WriteLine("Staff Id :" + StaffId);
            base.Output();
            Console.WriteLine("Chuc Vu : " + Position);
            Console.WriteLine("Muc Luong : " + Salary);
            Console.WriteLine("Ngay vang mat : " + AbsentDay);
        }

    }
}