using System;


namespace formele_methoden
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            if (!Directory.Exists("Graphs/"))
            {
                Directory.CreateDirectory("Graphs/");
            }

            Lesson1 lesson1 = new Lesson1();
            lesson1.ndfaToDfa();
        }
    }

}

