namespace TeaStorel
{
    class OperateCost
    {
        public int IdOperateCost { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }


        public OperateCost() { }

        public OperateCost(int idOperateCost, string name, double cost)
        {
            IdOperateCost = idOperateCost;
            this.Name = name;
            this.Cost = cost;
        }


        //Ham xu ly CRUD manager lien quan den Operate Cost
        public void AddManageOperateCost()
        {
            CacheData.operateCosts = Database<OperateCost>.readFile(Database<OperateCost>.OperateCostFilePath);

            //Add operateCost
            Console.WriteLine("Nhap ten chi phi de them :");
            this.Name = Console.ReadLine();
            Console.WriteLine("Nhap muc gia chi phi de them :");
            this.Cost = Double.Parse(Console.ReadLine());
            this.IdOperateCost = CacheData.operateCosts.Any() ? CacheData.operateCosts.Max(x => x.IdOperateCost) + 1 : 1; // tang id cua OperateCost len 1

            //Add product to CacheData.operateCosts
            CacheData.operateCosts.Add(this);
            Console.WriteLine("Them thanh cong !");

            //Print Table
            TableDraw.Table(CacheData.operateCosts);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
        }

        public bool DeleteManageOperateCost()
        {
            CacheData.operateCosts = Database<OperateCost>.readFile(Database<OperateCost>.OperateCostFilePath);

            //Check id operateCost
            int id;
            while (true)
            {
                Console.Write("Nhap id chi phi muon xoa :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in CacheData.operateCosts
                           where o.IdOperateCost == id
                           select o;
                if (list.Any(oder => oder.IdOperateCost == id))
                    break;
                Console.WriteLine("Ma chi phi khong hop le vui long nhap lai ! ");
            };

            //Delete operateCost
            CacheData.operateCosts.RemoveAll(x => x.IdOperateCost == id);
            Console.WriteLine("Xoa thanh cong !");

            //Print Table
            TableDraw.Table(CacheData.operateCosts);
            Console.WriteLine("<---- Back");
            Console.ReadLine();
            return true;
        }

        public bool EditManageOperateCost()
        {
            CacheData.operateCosts = Database<OperateCost>.readFile(Database<OperateCost>.OperateCostFilePath);

            //check id operateCost
            int id;
            while (true)
            {
                Console.Write("Nhap id chi phi muon sua :");
                id = Int32.Parse(Console.ReadLine());
                var list = from o in CacheData.operateCosts
                           where o.IdOperateCost == id
                           select o;
                if (list.Any(oder => oder.IdOperateCost == id))
                    break;
                Console.WriteLine("Ma nhan vien khong hop le vui long nhap lai ! ");
            };
            CacheData.operateCosts.RemoveAll(x => x.IdOperateCost == id);
            this.IdOperateCost = id;

            //Edit operateCost
            Console.WriteLine("Nhap ten chi phi de them :");
            this.Name = Console.ReadLine();
            Console.WriteLine("Nhap muc gia chi phi de them :");
            this.Cost = Double.Parse(Console.ReadLine());

            //Add product to CacheData.operateCost
            CacheData.operateCosts.Add(this);
            Console.WriteLine("Sua thanh cong !");

            //Print Table
            TableDraw.Table(CacheData.operateCosts);
            Console.WriteLine("<---- Back");
            Console.ReadLine();

            return true;
        }

        public bool AddManageOperateCosttoDataBase()
        {
            try
            {
                var enumerable = from o in CacheData.operateCosts
                                 orderby o.IdOperateCost ascending
                                 select o;
                List<OperateCost> oderByList = enumerable.ToList();

                Database<OperateCost>.writeFile(oderByList, Database<OperateCost>.OperateCostFilePath); //add to database
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }


    }
}
