using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab_1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            do
            {
                string path = @"C:\InformationSecurity-master\InformationSecurity-master\Lab_1\Lab_1\Txts\Text.txt";
                //for (int i = 1040; i < 1072; i++)
                //АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ
                //for (int i = 1072; i < 1104; i++)
                //абвгдежзийклмнопрстуфхцчшщъыьэюя

                uint k = 0;
                //Строка, к которой применяется шифрованияе/дешифрование
                string s = "";
                string s1 = "";
                //Строка - результат шифрования/дешифрования
                string result = "";
                //Величина сдвига при шифровании/дешифровании
                uint shift;

                Console.WriteLine("Введите 1 для шифрованияб, 2 для дешифрования, 3 для дешифрования с помощью частотного анализа, 4 для дешифрования с помощью частотного анализа (биграммы)");
                //Считывание переменной выбора, пока она не станет равной 1 или 2
                while ((k != 1) && (k != 2) && (k != 3) && (k != 4))
                {
                    //Считывание переменной k, если введенные данные имеют тип uint
                    uint.TryParse(Console.ReadLine(), out k);
                    //Вывод сообщения об ошибке, если k != 1 или k != 2
                    if ((k != 1) && (k != 2) && (k != 3) && (k != 4))
                        Console.WriteLine("Ошибка ввода, повторите попытку");
                }

                if (k == 3)
                    goto Step3;

                if (k == 4)
                    goto Step4;

                //Вывод сообщения на экран
                Console.WriteLine("Введите величину сдвига");
                //Считывние величины сдвига
                while (!uint.TryParse(Console.ReadLine(), out shift))
                {
                    //Если введена неверная величина сдвига (отрицательное число, или не число)
                    Console.WriteLine("Ошибка ввода, повторите попытку");
                }

                //Если величина сдвига больше длины алфавита кирилицы
                if (shift > 32)
                    shift = shift % 32;
                //Если выбрано шифрование



                if (k == 1)
                {
                    //Вывод сообщения на экран
                    Console.WriteLine("Строка считывается из файла!");
                    //Считывание строки
                    s = File.ReadAllText(path, Encoding.Default);
                    //Выполение шифрования
                    //Цикл по каждому символу строки
                    for (int i = 0; i < s.Length; i++)
                    {
                        //Console.WriteLine(s1[i] + " : " + Convert.ToInt16(s[i]));
                        //Если не кириллица
                        if (((int)(s[i]) < 1040) || ((int)(s[i]) > 1103))
                            result += s[i];
                        //Если буква является строчной
                        if ((Convert.ToInt16(s[i]) >= 1072) && (Convert.ToInt16(s[i]) <= 1103))
                        {
                            //Если буква, после сдвига выходит за пределы алфавита
                            if (Convert.ToInt16(s[i]) + shift > 1103)
                                //Добавление в строку результатов символ
                                result += Convert.ToChar(Convert.ToInt16(s[i]) + shift - 32);
                            //Если буква может быть сдвинута в пределах алфавита
                            else
                                //Добавление в строку результатов символ
                                result += Convert.ToChar(Convert.ToInt16(s[i]) + shift);
                        }
                        //Если буква является прописной
                        if ((Convert.ToInt16(s[i]) >= 1040) && (Convert.ToInt16(s[i]) <= 1071))
                        {
                            //Если буква, после сдвига выходит за пределы алфавита
                            if (Convert.ToInt16(s[i]) + shift > 1071)
                                //Добавление в строку результатов символ
                                result += Convert.ToChar(Convert.ToInt16(s[i]) + shift - 32);
                            //Если буква может быть сдвинута в пределах алфавита
                            else
                                //Добавление в строку результатов символ
                                result += Convert.ToChar(Convert.ToInt16(s[i]) + shift);
                        }
                    }
                    //Вывод на экран зашифрованной строки
                    Console.WriteLine("Строка успешно зашифрована!");
                    StreamWriter sr = new StreamWriter(@"C:\InformationSecurity-master\InformationSecurity-master\Lab_1\Lab_1\Txts\Result.txt", false);
                    sr.Write(result);
                    sr.Close();
                    Console.WriteLine(result);
                    s = "";
                    result = "";
                }

                //Если было выбрано дешифрование
                if (k == 2)
                {
                    //Вывод сообщения на экран
                    Console.WriteLine("Строка считывается из файла!");
                    //Считывание строки
                    //s = File.ReadAllText(path, Encoding.Default);- не читает если есть Encoding.Default
                    StreamReader sr = new StreamReader(@"C:\InformationSecurity-master\InformationSecurity-master\Lab_1\Lab_1\Txts\Result.txt");
                    s = sr.ReadToEnd();
                    sr.Close();
                    //Time.Start();- вообще не нужно!
                    //Выполение дешифрования
                    //Цикл по каждому символу строки
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (((int)(s[i]) < 1040) || ((int)(s[i]) > 1103))
                            result += s[i];
                        //if (Convert.ToInt16(s[i]) == 32)
                        //  result += s[i];
                        //Если буква является строчной
                        if ((Convert.ToInt16(s[i]) >= 1072) && (Convert.ToInt16(s[i]) <= 1103))
                        {
                            //Если буква, после сдвига выходит за пределы алфавита
                            if (Convert.ToInt16(s[i]) - shift < 1072)
                                //Добавление в строку результатов символ
                                result += Convert.ToChar(Convert.ToInt16(s[i]) - shift + 32);
                            //Если буква может быть сдвинута в пределах алфавита
                            else
                                //Добавление в строку результатов символ
                                result += Convert.ToChar(Convert.ToInt16(s[i]) - shift);
                        }
                        //Если буква является прописной
                        if ((Convert.ToInt16(s[i]) >= 1040) && (Convert.ToInt16(s[i]) <= 1071))
                        {
                            //Если буква, после сдвига выходит за пределы алфавита
                            if (Convert.ToInt16(s[i]) - shift < 1040)
                                //Добавление в строку результатов символ
                                result += Convert.ToChar(Convert.ToInt16(s[i]) - shift + 32);
                            //Если буква может быть сдвинута в пределах алфавита
                            else
                                //Добавление в строку результатов символ
                                result += Convert.ToChar(Convert.ToInt16(s[i]) - shift);
                        }
                    }
                    //Вывод на экран дешифрованной строки
                    Console.WriteLine("Строка успешно дешифрована!");
                    StreamWriter sr1 = new StreamWriter(@"C:\InformationSecurity-master\InformationSecurity-master\Lab_1\Lab_1\Txts\Result.txt", false);
                    sr1.Write(result);
                    sr1.Close();
                    Console.WriteLine(result);
                    s = "";
                    result = "";
                }

            Step3:

                if (k == 3)
                {
                    //Вывод сообщения на экран
                    Console.WriteLine("Весь текст считывается из файла!");
                    //Подсчет повторений 
                    StreamReader sr = new StreamReader(@"C:\InformationSecurity-master\InformationSecurity-master\Lab_1\Lab_1\Txts\FullText.txt");
                    s = sr.ReadToEnd();
                    sr.Close();
                    StreamReader sr1 = new StreamReader(@"C:\InformationSecurity-master\InformationSecurity-master\Lab_1\Lab_1\Txts\Result.txt");
                    s1 = sr1.ReadToEnd();
                    sr1.Close();
                    //Console.WriteLine(s1);
                    int[] fullTextCount = new int[32];
                    int[] resultCount = new int[32];
                    Console.WriteLine("Загрузка...");
                    for (int i = 0; i < s.Length; i++)
                    {
                        if ((Convert.ToInt32(s[i]) >= 1040) && (Convert.ToInt32(s[i]) <= 1103))
                        {
                            if (Convert.ToInt16(s[i]) < 1072)
                                fullTextCount[Convert.ToInt16(s[i]) - 1040]++;
                            else
                                fullTextCount[Convert.ToInt16(s[i]) - 1072]++;
                        }
                      
                    }
                    for (int i = 0; i < s1.Length; i++)
                    {
                        if ((Convert.ToInt32(s1[i]) >= 1040) && (Convert.ToInt32(s1[i]) <= 1103))
                        {
                            if (Convert.ToInt16(s1[i]) < 1072)
                                resultCount[Convert.ToInt16(s1[i]) - 1040]++;
                            else
                                resultCount[Convert.ToInt16(s1[i]) - 1072]++;
                        }

                    }
                    Console.WriteLine("Готово");
                    Console.WriteLine("Количество повторений в целой книге");
                    for (int i = 0; i < 32; i++)
                        Console.WriteLine(Convert.ToChar(i + 1040) + ": " + fullTextCount[i]);
                    Console.WriteLine("Количество повторений в зашифрованной главе");
                    for (int i = 0; i < 32; i++)
                        Console.WriteLine(Convert.ToChar(i + 1040) + ": " + resultCount[i]);

                    //сопоставление максимальных значений 

                    Console.WriteLine("Сопостовление максимумов");

                    char[,] dictionary = new char[32, 2];

                    for (int i = 0; i < 32; i++)
                    {
                        int maxValue = (int)fullTextCount.Max();
                        int maxIndex = fullTextCount.ToList().IndexOf(maxValue);
                        Console.WriteLine(maxValue + " : " + maxIndex);
                        int maxValue1 = (int)resultCount.Max();
                        int maxIndex1 = resultCount.ToList().IndexOf(maxValue1);
                        Console.WriteLine(maxValue1 + " : " + maxIndex1);

                        dictionary[i, 0] = Convert.ToChar(maxIndex + 1040);
                        dictionary[i, 1] = Convert.ToChar(maxIndex1 + 1040);
                        fullTextCount[maxIndex] = 0;
                        resultCount[maxIndex1] = 0;
                        Console.WriteLine(dictionary[i, 0] + " : " + dictionary[i, 1]);
                    }
                    Console.WriteLine("Замена букв в зашифрованном тексте");
                    for (int i = 0; i < s1.Length; i++)
                    {
                        if ((Convert.ToInt16(s1[i]) >= 1040) && (Convert.ToInt16(s1[i]) <= 1103))
                        {
                            for (int j = 0; j < 32; j++)
                            {
                                if (dictionary[j, 1] == s1[i])
                                    result += dictionary[j, 0];
                                else if (Convert.ToChar(Convert.ToInt16(dictionary[j, 1]) + 32) == s1[i])
                                    result += Convert.ToChar(Convert.ToInt16(dictionary[j, 0]) + 32);
                            }
                        }
                        else
                            result += s1[i];
                    }
6
                    Console.WriteLine("Строка успешно дешифрована!");
                    StreamWriter sr2 = new StreamWriter(@"C:\InformationSecurity-master\InformationSecurity-master\Lab_1\Lab_1\Txts\Result.txt", false);
                    sr2.Write(result);
                    sr2.Close();
                    Console.WriteLine(result);
                    s = "";
                    s1 = "";
                    result = "";

                }

            Step4:

                //дешифрования с помощью частотного анализа (биграммы)

                if (k == 4)
                {
                    //Вывод сообщения на экран
                    Console.WriteLine("Весь текст считывается из файла!");
                    //Подсчет повторений 
                    StreamReader sr = new StreamReader(@"C:\InformationSecurity-master\InformationSecurity-master\Lab_1\Lab_1\Txts\FullText.txt");
                    s = sr.ReadToEnd();
                    sr.Close();
                    StreamReader sr1 = new StreamReader(@"C:\InformationSecurity-master\InformationSecurity-master\Lab_1\Lab_1\Txts\Result.txt");
                    s1 = sr1.ReadToEnd();
                    sr1.Close();
                    //Console.WriteLine(s1);
                    int[] fullTextCount = new int[1024];
                    int[] resultCount = new int[1024];
                    Console.WriteLine("Загрузка...");

                    for (int i = 0; i < s.Length - 1; i++)
                    {
                        if ((Convert.ToInt32(s[i]) >= 1040) && (Convert.ToInt32(s[i]) <= 1103) &&
                            (Convert.ToInt32(s[i + 1]) >= 1040) && (Convert.ToInt32(s[i + 1]) <= 1103))
                        {
                            if ((Convert.ToInt16(s[i]) < 1072) && (Convert.ToInt16(s[i + 1]) < 1072))
                                fullTextCount[(Convert.ToInt16(s[i]) - 1040) * 32 + (Convert.ToInt16(s[i + 1]) - 1040)]++;
                            else if ((Convert.ToInt16(s[i]) >= 1072) && (Convert.ToInt16(s[i + 1]) < 1072))
                                fullTextCount[(Convert.ToInt16(s[i]) - 1070) * 32 + (Convert.ToInt16(s[i + 1]) - 1040)]++;
                            else if ((Convert.ToInt16(s[i]) < 1072) && (Convert.ToInt16(s[i + 1]) >= 1072))
                                fullTextCount[(Convert.ToInt16(s[i]) - 1040) * 32 + (Convert.ToInt16(s[i + 1]) - 1070)]++;
                            else if ((Convert.ToInt16(s[i]) >= 1072) && (Convert.ToInt16(s[i + 1]) >= 1072))
                                fullTextCount[(Convert.ToInt16(s[i]) - 1072) * 32 + (Convert.ToInt16(s[i + 1]) - 1072)]++;
                        }
                    }

                    for (int i = 0; i < s1.Length - 1; i++)
                    {
                        if ((Convert.ToInt32(s1[i]) >= 1040) && (Convert.ToInt32(s1[i]) <= 1103) &&
                            (Convert.ToInt32(s1[i + 1]) >= 1040) && (Convert.ToInt32(s1[i + 1]) <= 1103))
                        {
                            if ((Convert.ToInt16(s1[i]) < 1072) && (Convert.ToInt16(s1[i + 1]) < 1072))
                                resultCount[(Convert.ToInt16(s1[i]) - 1040) * 32 + (Convert.ToInt16(s1[i + 1]) - 1040)]++;
                            else if ((Convert.ToInt16(s1[i]) >= 1072) && (Convert.ToInt16(s1[i + 1]) < 1072))
                                resultCount[(Convert.ToInt16(s1[i]) - 1070) * 32 + (Convert.ToInt16(s1[i + 1]) - 1040)]++;
                            else if ((Convert.ToInt16(s1[i]) < 1072) && (Convert.ToInt16(s1[i + 1]) >= 1072))
                                resultCount[(Convert.ToInt16(s1[i]) - 1040) * 32 + (Convert.ToInt16(s1[i + 1]) - 1070)]++;
                            else if ((Convert.ToInt16(s1[i]) >= 1072) && (Convert.ToInt16(s1[i + 1]) >= 1072))
                                resultCount[(Convert.ToInt16(s1[i]) - 1072) * 32 + (Convert.ToInt16(s1[i + 1]) - 1072)]++;
                        }
                    }

                    Console.WriteLine("Количество повторений в целой книге");
                    for (int i = 0; i < 1024; i++)
                        Console.WriteLine(fullTextCount[i]);
                    Console.WriteLine("Количество повторений в зашифрованной главе");
                    for (int i = 0; i < 1024; i++)
                        Console.WriteLine(resultCount[i]);

                    Console.WriteLine("Сопостовление максимумов");

                    string[,] dictionary = new string[1024, 2];

                    for (int i = 0; i < 1024; i++)
                    {
                        int maxValue = (int)fullTextCount.Max();
                        int maxIndex = fullTextCount.ToList().IndexOf(maxValue);
                        Console.WriteLine(maxValue + " : " + maxIndex);
                        int maxValue1 = (int)resultCount.Max();
                        int maxIndex1 = resultCount.ToList().IndexOf(maxValue1);
                        Console.WriteLine(maxValue1 + " : " + maxIndex1);

                        dictionary[i, 0] = Convert.ToChar(maxIndex / 32 + 1040).ToString() + Convert.ToChar((maxIndex - (32 * (maxIndex / 32))) + 1040).ToString();
                        dictionary[i, 1] = Convert.ToChar(maxIndex1 / 32 + 1040).ToString() + Convert.ToChar((maxIndex1 - (32 * (maxIndex1 / 32))) + 1040).ToString();
                        fullTextCount[maxIndex] = -i;
                        resultCount[maxIndex1] = -i;
                        Console.WriteLine(dictionary[i, 0] + " : " + dictionary[i, 1]);
                    }
                    Console.WriteLine(s1);
                    Console.WriteLine("Замена букв в зашифрованном тексте");
                    for (int i = 0; i < s1.Length - 1; i = i + 2)
                    {
                        if ((Convert.ToInt32(s1[i]) >= 1040) && (Convert.ToInt32(s1[i]) <= 1103) &&
                            (Convert.ToInt32(s1[i + 1]) >= 1040) && (Convert.ToInt32(s1[i + 1]) <= 1103))
                        {
                            //Console.Write(s1[i]);
                            //Console.WriteLine(s1[i + 1]);
                            for (int j = 0; j < 1024; j++)
                            {

                                /*
                                Console.WriteLine(dictionary[j, 1][0]);
                                Console.WriteLine(dictionary[j, 1][1]);
                                Console.WriteLine(s1[i]);
                                Console.WriteLine(Convert.ToInt16(s1[i]));
                                Console.WriteLine(Convert.ToChar(Convert.ToInt16(s1[i]) - 32));
                                Console.WriteLine(s1[i + 1]);
                                Console.WriteLine(Convert.ToInt16(s1[i + 1]) - 32);
                                Console.WriteLine(Convert.ToChar(Convert.ToInt16(s1[i + 1]) - 32));
                                */

                                if (dictionary[j, 1][0] == s1[i] &&                                                 //A == A
                                    dictionary[j, 1][1] == s1[i + 1])                                               //A == A
                                {
                                    result += dictionary[j, 0];
                                    break;
                                }
                                else if (dictionary[j, 1][0] == Convert.ToChar(Convert.ToInt16(s1[i]) - 32) &&      //A == а=>A
                                         dictionary[j, 1][1] == s1[i + 1])                                          //A == A
                                {
                                    result += Convert.ToChar(Convert.ToInt16(dictionary[j, 0][0]) + 32);
                                    result += dictionary[j, 0][1];
                                    break;
                                }
                                else if (dictionary[j, 1][0] == s1[i] &&                                            //A == A
                                         dictionary[j, 1][1] == Convert.ToChar(Convert.ToInt16(s1[i + 1]) - 32))    //A == а=>A
                                {
                                    result += dictionary[j, 0][0]; 
                                    result += Convert.ToChar(Convert.ToInt16(dictionary[j, 0][1]) + 32);
                                    break;
                                }
                                else if (dictionary[j, 1][0] == Convert.ToChar(Convert.ToInt16(s1[i] - 32)) &&      //A == а=>A
                                         dictionary[j, 1][1] == Convert.ToChar(Convert.ToInt16(s1[i + 1]) - 32))    //A == а=>A
                                {
                                    result += Convert.ToChar(Convert.ToInt16(dictionary[j, 0][0]) + 32); 
                                    result += Convert.ToChar(Convert.ToInt16(dictionary[j, 0][1]) + 32);
                                    break;
                                }

                            }
                            //Console.WriteLine(result[i]);
                            //Console.Write(result[i + 1]);
                        }
                        else
                        {
                            result += s1[i];
                            result += s1[i + 1];
                            //Console.WriteLine(result[i]);
                            //Console.Write(result[i + 1]);
                        }

                            
                    }

                    Console.WriteLine("Строка успешно дешифрована!");
                    StreamWriter sr2 = new StreamWriter(@"C:\InformationSecurity-master\InformationSecurity-master\Lab_1\Lab_1\Txts\Result.txt", false);
                    sr2.Write(result);
                    sr2.Close();
                    Console.WriteLine(result);

                    s = "";
                    s1 = "";
                    result = "";
                }
                Console.WriteLine("Для выхода из программы нажмите Escape");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
