using System.Diagnostics;

namespace MilkTeaStore
{
    class Person
    {
        public string Name { get; set; }
        public string Numberphone { get; set; }
        public string Address { get; set; }

        public Person()
        {
        }

        public Person(string name, string numberphone)
        {
            Name = name;
            Numberphone = numberphone;
        }
        public Person(string name, string numberphone, string address)
        {
            Name = name;
            Numberphone = numberphone;
            Address = address;
        }

        public virtual void Output()
        {
            Console.WriteLine("Ten : " + Name);
            Console.WriteLine("So dien thoai : " + Numberphone);
            Console.WriteLine("Dia chi : " + Address);
        }

    }
}