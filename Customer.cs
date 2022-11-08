using System.Runtime.CompilerServices;

namespace MilkTeaStore
{
    class Customer:Person
    {
       
        public Customer()
        {
           
        }
        
        public Customer(string id, string name, string numberphone, string address) : base( id ,name, numberphone, address)
        {

            
        }

        public void oderThucUong(string code)
        {
            Console.WriteLine("Hay nhap danh sach");
            //dsOder = new Oder();
            //dsOder.addOder();

            /*if(this == IStrongBox s)
            od = new Order(ds thuc uong);
            
            od.capnhatMoidat();*/
            //od.luu(); //list 

            
        }
    }
}