namespace MilkTeaStore
{
    partial class Program
    {
        class Database
        {
            //string filePath = "DataBase\\Customer.txt";
            public string FilePath { get; set; }
            public Database(string filepath)
            {
                FilePath = filepath;
            }

            //Get customer
            public List<Customer> getCustomerData()
            {
                List<Customer> customerList = new List<Customer>();

                List<string> lines = File.ReadAllLines(FilePath).ToList();

                foreach (string line in lines)
                {
                    string[] entries = line.Split(',');

                    Customer customer = new Customer();
                    customer.Id = entries[0];
                    customer.Name = entries[1];
                    customer.Numberphone = entries[2];
                    customer.Address = entries[3];

                    customerList.Add(customer);
                }

                return customerList;
            }
            public int setCustomerData(List<Customer> customerList)
            {
                List<string> output = new List<string>();
                foreach (Customer customer in customerList)
                {
                    output.Add($"{customer.Id},{customer.Name},{customer.Numberphone},{customer.Address}");
                }
                // writing to file
                File.WriteAllLines(FilePath, output);

                return 1;
            }
            public int addCustomerData(List<Customer> addcustomerList)
            {
                List<string> output = new List<string>();
                foreach (Customer customer in addcustomerList)
                {
                    output.Add($"{customer.Id},{customer.Name},{customer.Numberphone},{customer.Address}");
                }
                // writing to file
                File.AppendAllLines(FilePath, output);

                return 1;
            }
            
            //Get Staff
            public List<Staff> getStaffData()
            {
                List<Staff> staffList = new List<Staff>();

                List<string> lines = File.ReadAllLines(FilePath).ToList();

                foreach (string line in lines)
                {
                    string[] entries = line.Split(',');

                    Staff staff = new Staff();
                    staff.Id = entries[0];
                    staff.Name = entries[1];
                    staff.Numberphone = entries[2];
                    staff.Address = entries[3];
                    staff.WorkSchedule = entries[4];
                    staff.Position = entries[5];
                    staff.Salary = long.Parse(entries[6]);
                    staff.AbsentDay = Int32.Parse(entries[7]);

                    staffList.Add(staff);
                }

                return staffList;
            }
            public int setStaffData(List<Staff> staffList)
            {
                List<string> output = new List<string>();
                foreach (var staff in staffList)
                {
                    output.Add($"{staff.Id},{staff.Name},{staff.Numberphone},{staff.Address},{staff.WorkSchedule},{staff.Position},{staff.Salary},{staff.AbsentDay}");
                }
                // writing to file
                File.WriteAllLines(FilePath, output);

                return 1;
            }
            public int addStaffData(List<Staff> addstaffList)
            {
                List<string> output = new List<string>();
                foreach (var staff in addstaffList)
                {
                    output.Add($"{staff.Id},{staff.Name},{staff.Numberphone},{staff.Address},{staff.WorkSchedule},{staff.Position},{staff.Salary},{staff.AbsentDay}");
                }
                // writing to file
                File.AppendAllLines(FilePath, output);

                return 1;
            }


        }
    }
}
