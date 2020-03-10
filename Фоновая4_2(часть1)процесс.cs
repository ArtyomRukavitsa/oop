using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Вариант_3
{
    enum Months { январь = 1, февраль, март, апрель, май, июнь, июль, август, сентябрь, октябрь, ноябрь, декабрь };
    class MatrixWeather
    {
        int month;
        int day;
        public static int[] days = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public int[,] temperature;

        public MatrixWeather(int day, int month)
        {
            this.day = day;
            this.month = month;
            int count_of_strings = (day + days[this.month - 1]) / 7;
            if ((day + days[this.month - 1]) % 7 != 0) count_of_strings += 1;
            //Console.WriteLine(day + days[this.month - 1]);
            //temperature = new int[count_of_strings, 7];
            temperature = FillArray(count_of_strings, day, month);
            //PrintArray(temperature, day, days[this.month - 1]);
            //Console.WriteLine(count_of_strings);
            //temperature = new int[(Months)this.month, 7];
        }
        static int[,] FillArray(int count_of_strings, int day, int month)
        {
            int count = 1;
            int[,] temperature = new int[count_of_strings, 7];
            Random rnd = new Random();
            for (int i = 0; i < count_of_strings; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if ((i == 0 && j >= day - 1) || (i >= 1 && i < count_of_strings - 1) || (i == count_of_strings - 1 && count <= days[month - 1]))
                    { temperature[i, j] = rnd.Next(-10, 10); count++; }
                    else temperature[i, j] = -100;
                }
            }
            return temperature;
        }
        public void PrintArray()
        {
            int count = 1;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Пн\tВт\tСр\tЧт\tПт\tСб\tВс");
            Console.ForegroundColor = ConsoleColor.Gray;
            // Покраску добавить в f?
            //Console.WriteLine(array.GetLength(0));
            for (int i = 0; i < temperature.GetLength(0); i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i == 0)
                    {
                        if (j < day - 1) Console.Write("\t");
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("{0} ", count);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("{0}\t", temperature[i, j]);
                            count++;
                        }
                    }
                    else if (i == temperature.GetLength(0) - 1)
                    {
                        if ((i + 1) * 7 + day > count && count < days[this.month - 1])
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("{0} ", count);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("{0}\t", temperature[i, j]);
                            count++;
                        }
                        else Console.Write("\t");
                        //count++;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("{0} ", count);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("{0}\t", temperature[i, j]);
                        count++;
                    }

                }
                Console.WriteLine();
            }
            //Console.WriteLine(count);
        }

        public int MaxDifference()
        {
            int delta=-1; //now_delta = 0;
            for (int i = 0; i < temperature.GetLength(0); i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (j == 6)
                    {
                        if (i != temperature.GetLength(0) - 1 && Math.Abs(temperature[i, j] - temperature[i + 1, 0]) > delta && temperature[i, j] != -100 & temperature[i + 1, 0] != -100)
                        {
                            delta = Math.Abs(temperature[i, j] - temperature[i + 1, 0]);
                            //Console.WriteLine($"{i} {j} {delta}");
                        }
                    }
                    else
                    {
                        //Console.WriteLine($"{i}, {j}, {temperature.GetValue(4, 1)}");
                        if (Math.Abs(temperature[i, j] - temperature[i, j + 1]) > delta && temperature[i, j] != -100 & temperature[i, j + 1] != -100)
                        {
                            delta = Math.Abs(temperature[i, j] - temperature[i, j+1]);
                           // Console.WriteLine($"{i} {j} {delta}");
                        }
                    }
                }
            }
            return delta;

            
        }
        public int MaxDifference(out int delta, out int day)
        {
            delta = -1; //now_delta = 0;
            int count = 1;
            int temp = 0;
            day = 0;
            for (int i = 0; i < temperature.GetLength(0); i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (j == 6)
                    {
                        if (i != temperature.GetLength(0) - 1 && Math.Abs(temperature[i, j] - temperature[i + 1, 0]) > delta && temperature[i, j] != -100 & temperature[i + 1, 0] != -100)
                        {
                            delta = Math.Abs(temperature[i, j] - temperature[i + 1, 0]);
                            day = count;
                            temp = temperature[i, j];
                            //Console.WriteLine($"{i} {j} {delta}");
                        }
                        if (i != temperature.GetLength(0) - 1 && temperature[i, j] != -100 & temperature[i + 1, 0] != -100) count++;
                    }
                    else
                    {
                        //Console.WriteLine($"{i}, {j}, {temperature.GetValue(4, 1)}");
                        if (Math.Abs(temperature[i, j] - temperature[i, j + 1]) > delta && temperature[i, j] != -100 & temperature[i, j + 1] != -100)
                        {
                            delta = Math.Abs(temperature[i, j] - temperature[i, j + 1]);
                            day = count;
                            temp = temperature[i, j];
                            // Console.WriteLine($"{i} {j} {delta}");
                        }
                        if (temperature[i, j] != -100 & temperature[i, j+1] != -100) count++;
                    }
                }
            }
            return temp;
        }

        public int CountWithZeros
        {
            get
            {
                int count = 0;
                foreach (int x in temperature) if (x == 0) count++;
                return count;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int month = int.Parse(Console.ReadLine());
            int day = int.Parse(Console.ReadLine());

            MatrixWeather q = new MatrixWeather(day, month);
            q.PrintArray();
            Console.WriteLine("Максимальный скачок (в градусах): {0}", q.MaxDifference());
            int number_of_the_day, delta_degrees;
            int degree = q.MaxDifference(out delta_degrees, out number_of_the_day);
            Console.WriteLine("Максимальный скачок (в градусах) составил {0}, это случилось в день {1}, температура которого составляла {2} градусов.", delta_degrees, number_of_the_day, degree);
            Console.WriteLine("Количество дней с температурой, равной нулю: {0}", q.CountWithZeros);
            //int count_of_strings = (day + MatrixWeather.days[month - 1]) / 7;
            //MatrixWeather.FillArray(count_of_strings);
        }
    }
}
