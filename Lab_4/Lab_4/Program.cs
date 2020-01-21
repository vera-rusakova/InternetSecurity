using System;
using System.Linq;
using System.Numerics;

namespace Lab_4
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int p = 7;  //SimpleNumber(RandomNumber(1, 10)); обязательно простое число - требование протокола
            int q = 5; //SimpleNumber(RandomNumber(1, 10)); обязательно простое число - требование протокола
            int n = p * q;  //35 - произведение p*q
            int s = (p - 1) * (q - 1);  //24 - значение функции Эйлера
            int d = 29; // - секретная экспонента, обратное к числу e по модулю значения ф-ии Эйлера

            //if (s % d == 0)
            //{
               //d++;
            //}

            int e = 1; //- открытая экспонента

            while ((e * d) % s != 1)
            {
                e++;
                Console.WriteLine(e);
            }
            
            Console.WriteLine("P = " + p);
            Console.WriteLine("Q = " + q);
            Console.WriteLine("N = " + n);
            Console.WriteLine("S = " + s);
            Console.WriteLine("Public Key");
            Console.WriteLine("D = " + d);
            Console.WriteLine("Private Key");
            Console.WriteLine("E = " + e);
 

            string message = "";

            // Для шифрования берем открытый ключ, создаем сеансовый ключ (текст сообщения)
            // Для шифрования посимвольно производим операцию c = m^e mod n (m - текст) 
            Console.WriteLine("Enter message to encrypt");
            message = Console.ReadLine();

            BigInteger[] array = new BigInteger[message.Length];
            BigInteger[] arrayf = new BigInteger[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                Console.WriteLine(message[i] + " = " + (Convert.ToInt32(message[i] - 96)));
                Console.WriteLine(Math.Pow(Convert.ToInt32(message[i]) - 96, e));
                array[i] = BigInteger.Pow(Convert.ToInt32(message[i]) - 96, e) % n;
                Console.WriteLine(array[i]);
            }

            string encryptedMessage = "";
            // Для дешифрования берем зашифрованный сеансовый ключ, закрытый ключ
            // Для того, чтобы расшифровать, c^d mod n

            for (int i = 0; i < message.Length; i++)
            {
                encryptedMessage += Convert.ToChar((int)array[i] + 96);
            }

            Console.WriteLine(encryptedMessage);

            Console.WriteLine("Decrypt?");
            Console.ReadKey();

            for (int i = 0; i < message.Length; i++)
            {
                arrayf[i] = (BigInteger.Pow(array[i], d)) % n;
                Console.WriteLine((BigInteger.Pow(array[i], d)));
                Console.WriteLine((BigInteger.Pow(array[i], d)) % n);
            }

            string decryptedMessage = "";

            for (int i = 0; i < message.Length; i++)
            {
                Console.WriteLine(arrayf[i]);
                decryptedMessage += Convert.ToChar((int)arrayf[i] + 96);
            }

            Console.WriteLine(decryptedMessage);



            Console.ReadKey();

        }

        static int RandomNumber(int a, int b)
        {
            Random rand = new Random();
            return rand.Next(a, b);
        }

  //      static int SimpleNumber(int randnum)
  //      {
  //          return (randnum * 2 + 1) * 2 + 1;
  //      }

//       int mod(string num, int a) 
//{ 
    // Initialize result 
//    int res = 0; 
  
    // Обрабатываются все символы num- 
//    for (int i = 0; i < num.Length; i++) 
//         res = (res*10 + (int)num[i] - '0') %a; 
  
//    return res; 
//} 
    }
}