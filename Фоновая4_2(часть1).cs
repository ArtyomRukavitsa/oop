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
        public static int[] days = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        public int[,] temperature;

        public MatrixWeather(int day, int month)
        {
            this.day = day;  
            this.month = month;
            int count_of_strings = (day + days[this.month - 1]) / 7;
            if ((day + days[this.month - 1]) % 7 != 0) count_of_strings += 1;
            Console.WriteLine(day + days[this.month - 1]);
            //temperature = new int[count_of_strings, 7];
            temperature = FillArray(count_of_strings);
            //PrintArray(temperature, day, days[this.month - 1]);
            //Console.WriteLine(count_of_strings);
            //temperature = new int[(Months)this.month, 7];
        }
        static int[,] FillArray(int count_of_strings)
        {
            int[,] temperature = new int[count_of_strings, 7];
            Random rnd = new Random();
            for (int i = 0; i < count_of_strings; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //if (i == 0 &&)
                    temperature[i, j] = rnd.Next(-10, 10);
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
                        if ((i+1)  * 7 + day > count && count < days[this.month - 1])
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


    }
    class Program
    {
        static void Main(string[] args)
        {
            int month = int.Parse(Console.ReadLine());
            int day = int.Parse(Console.ReadLine());
            
            MatrixWeather q = new MatrixWeather(day, month);
            q.PrintArray();
            
            //int count_of_strings = (day + MatrixWeather.days[month - 1]) / 7;
            //MatrixWeather.FillArray(count_of_strings);
        }
    }
}
