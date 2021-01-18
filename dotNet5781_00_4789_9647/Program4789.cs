using System;

namespace dotNet5781_00_4789_9647
{
    partial class Program
    {


        static void Main(string[] args)
        {
            Welcome4789();
            Welcome9647();
            Console.ReadKey();


        }
        private static void Welcome4789()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("{0}, welcome to my first console application", name);
        }
        static partial void Welcome9647();

    }

}
