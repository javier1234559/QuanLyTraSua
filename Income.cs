namespace TeaStorel
{

    class Income
    {
        public int IdIncome { get; set; }
        public string Month { get; set; }
        public double ValueIncomeByMonth { get; set; }


        public Income() { }

        public Income(int idIncome, string month, double valueIncomeByMonth)
        {
            IdIncome = idIncome;
            Month = month;
            ValueIncomeByMonth = valueIncomeByMonth;
        }

        public Income(string month)
        {
            Month = month;
        }


        //Ham tinh toan doanh thu 1 thang
        public double TotalBillByMonth()
        {
            List<Bill> bills = Database<Bill>.readFile(Database<Bill>.BillFilePath);

            //Tim danh sach bill theo thang
            var query = bills.Where(b => b.Date.Split("/").Last() == this.Month);

            double sum = 0;

            foreach (var i in query)
            {
                sum += i.Total;
            }

            return sum;
        }

        public double TotalSalary()
        {
            var staffs = Database<Staff>.readFile(Database<Staff>.StaffFilePath);

            double sum = 0;

            foreach (var i in staffs)
            {
                sum += i.Salary;
            }

            return sum;
        }

        public double TotalProductHadSell()
        {
            List<Order> oders = Database<Order>.readFile(Database<Order>.OrderFilePath);
            List<Product> products = Database<Product>.readFile(Database<Product>.ProductFilePath);
            List<Bill> bills = Database<Bill>.readFile(Database<Bill>.BillFilePath);

            //Tim danh sach bill theo thang
            var billsByMonth = bills.Where(b => b.Date.Split("/").Last() == this.Month);

            var query = from o in oders
                        from p in products
                        from b in billsByMonth
                        where o.ProductID == p.ProductID && o.BillID == b.BillID
                        select new
                        {
                            id = b.BillID,
                            quantity = o.ProductQuantity,
                            price = p.OriginalPrice
                        };

            double sum = 0;
            foreach (var i in query)
            {
                sum += i.quantity * i.price;
            }
            return sum;
        }

        public double TotalOperateCost()
        {

            List<OperateCost> operateCosts = Database<OperateCost>.readFile(Database<OperateCost>.OperateCostFilePath);

            double sum = 0;

            foreach (var i in operateCosts)
            {
                sum += i.Cost;
            }

            return sum;
        }

        public double MainCalCulateByMonth()
        {

            double IncomeFromBill = TotalBillByMonth();

            double CostMustBePaid = TotalSalary() + TotalProductHadSell() + TotalOperateCost();

            this.ValueIncomeByMonth = IncomeFromBill - CostMustBePaid;

            return this.ValueIncomeByMonth;
        }

        public void PrintThisIncome()
        {
            CacheData.income = Database<Income>.readFile(Database<Income>.IncomeFilePath);

            this.MainCalCulateByMonth(); //tinh toan

            Console.WriteLine("\nTong chi phi thu nhap thang '" + this.Month + "' tu Bill : " + TotalBillByMonth());
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Tong chi phi van hanh phai tra thang '" + this.Month + "' la : " + TotalOperateCost());
            Console.WriteLine("Tong chi phi von nguyen lieu ban ra thang '" + this.Month + "' la : " + TotalProductHadSell());
            Console.WriteLine("Tong chi phi luong cho nhan vien thang '" + this.Month + "' la : " + TotalSalary());
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Tong thu nhap con lai la : " + this.ValueIncomeByMonth);


            Console.Write("Luu vao tinh toan vao DataBase (y/n) ?");
            string c = Console.ReadLine();
            if (c == "y")
            {
                this.IdIncome = CacheData.income.Any() ? CacheData.income.Max(x => x.IdIncome) + 1 : 1; // tang id cua Income len 1
                CacheData.income.Add(this);
                SaveThisIncome();
                Console.WriteLine("Luu ket qua vao DataBase thanh cong !");
                Console.ReadLine();
            }

        }

        public bool SaveThisIncome()
        {
            try
            {
                Database<Income>.writeFile(CacheData.income, Database<Income>.IncomeFilePath); //add to database
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }


        //Ham xuat tat ca doanh thu trong DataBase
        public void PrintInComeEveryMonth()
        {
            List<Income> incomes = Database<Income>.readFile(Database<Income>.IncomeFilePath);

            Console.Clear();
            Console.WriteLine("\nDoanh thu hang thang la :");
            TableDraw.Table(incomes);
            Console.WriteLine("<------- Back ");
            Console.ReadLine();
        }


    }
}
