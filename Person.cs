using System.Diagnostics;

namespace MilkTeaStore
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Numberphone { get; set; }
        public string Address { get; set; }

        public Person()
        {
        }

        public Person(string id, string name, string numberphone, string address)
        {
            Id = id;
            Name = name;
            Numberphone = numberphone;
            Address = address;
        }

       
        public virtual void Output()
        {
            Console.WriteLine("Id : " + Id);
            Console.WriteLine("Ten : " + Name);
            Console.WriteLine("So dien thoai : " + Numberphone);
            Console.WriteLine("Dia chi : " + Address);
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}