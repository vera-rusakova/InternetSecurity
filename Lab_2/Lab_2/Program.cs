using System;

namespace Lab_2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            double a;
            double b;
            double g;
            double p;
            double A;
            double B;
            double K1;
            double K2;

            Random rand = new Random();

            Console.WriteLine("Введите число Алисы: ");
            g = Convert.ToInt64(Console.ReadLine());

            Console.WriteLine("Введите число Боба: ");
            p = Convert.ToInt64(Console.ReadLine());

            //Console.WriteLine("Введите секретное число Алисы: ");
            a = rand.Next(10, 1000);

            //Console.WriteLine("Введите секретное число Боба: ");
            b = rand.Next(10, 1000);

            A = Math.Pow(g, a) % p;
            B = Math.Pow(g, b) % p;

            K1 = Math.Pow(B, a) % p;
            K2 = Math.Pow(A, b) % p;

            Console.WriteLine("a: " + a);
            Console.WriteLine("b: " + b);
            Console.WriteLine("g: " + g);
            Console.WriteLine("p: " + p);
            Console.WriteLine("A: " + A);
            Console.WriteLine("B: " + B);
            Console.WriteLine("K1: " + K1);
            Console.WriteLine("K2: " + K2);

            Console.ReadKey();
        }
    }
}
