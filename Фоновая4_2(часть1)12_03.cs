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

        public MatrixWeather()
        {
            day = month = 1;
            int count_of_strings = (day + days[this.month - 1]) / 7;
            if ((day + days[this.month - 1]) % 7 != 0) count_of_strings += 1;
            temperature = FillArray(count_of_strings, day, month);
        }
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
                    if (temperature[i, j] == -100)  Console.Write("\t");
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("{0} ", count);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("{0}\t", temperature[i, j]);
                        count++;
                    }
                    /*if (i == 0)
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
                    }*/

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

        public int Month
        {
            get { return month;  }
            set
            {
                try
                {
                    if (value <= 0) throw new Exception("Значение месяца не может быть отрицательным! Оставляю прежнее значение!");
                }
                catch (Exception err) { Console.WriteLine("Ошибка: " + err.Message); }
            }
        }

        public int[,] Temperature
        {
            get { return temperature; }
        }
        public int Day
        {
            get { return day; }
            set
            {
                int delta_days = value - day;
                if (delta_days > 0) // сдвиг вправо 
                {
                    int count_of_strings = (day + days[this.month - 1] + delta_days) / 7;
                    if ((day + days[this.month - 1] + delta_days) % 7 != 0) count_of_strings += 1;
                    int[] copy_mas = new int[count_of_strings * 7];
                    
                    //Console.WriteLine(copy_mas.Length);
                    int i = 0;
                    foreach (int x in temperature)
                    {
                        copy_mas[i] = x;
                        i++;
                    }
                    int temp;
                    for (int j = 0; j < delta_days; j++)
                    {
                        temp = copy_mas[copy_mas.Length - 1];
                        for (i = copy_mas.Length - 1; i > 0;  i--)
                        {
                            copy_mas[i] = copy_mas[i - 1];
                        }
                        copy_mas[0] = temp;
                    }
                    temperature = new int[count_of_strings, 7];
                    for (int x = 0; x < count_of_strings; x++) { for (int y = 0; y < 7; y++) temperature[x, y] = -100; }
                    //foreach (int x in copy_mas) Console.Write($"{x} ");
                    int index = 0;
                    for (i = 0; i < count_of_strings; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            //Console.WriteLine(index);
                            temperature[i, j] = copy_mas[index];
                            index++;
                        }
                    }
                    
                }
                else
                {
                    int count_of_strings = (day + days[this.month - 1]) / 7;
                    if ((day + days[this.month - 1]) % 7 != 0) count_of_strings += 1;
                    int[] copy_mas = new int[count_of_strings * 7];
                    //Console.WriteLine(copy_mas.Length);
                    int i = 0;
                    foreach (int x in temperature)
                    {
                        copy_mas[i] = x;
                        i++;
                    }
                    int temp;
                    for (int j = 0; j < Math.Abs(delta_days); j++)
                    {
                        temp = copy_mas[0];
                        for (i = 1; i < copy_mas.Length; i++)
                        {
                            copy_mas[i - 1] = copy_mas[i];
                        }
                        copy_mas[copy_mas.Length - 1] = temp;
                    }
                    temperature = new int[count_of_strings, 7];
                    //foreach (int x in copy_mas) Console.Write($"{x} ");
                    int index = 0;
                    for (i = 0; i < count_of_strings; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            temperature[i, j] = copy_mas[index];
                            index++;
                        }
                    }
                }
                    
                
            }
        }

        public int DaysInDiary // Свойство для чтения количества дней в дневнике
        {
            get { return days[month - 1];  }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введи месяц: ");
            int month = int.Parse(Console.ReadLine());
            Console.Write("Введи день: ");
            int day = int.Parse(Console.ReadLine());

            MatrixWeather weather = new MatrixWeather(day, month);
            weather.PrintArray();
            Console.WriteLine("Максимальный скачок (в градусах): {0}", weather.MaxDifference());
            int number_of_the_day, delta_degrees;
            int degree = weather.MaxDifference(out delta_degrees, out number_of_the_day);
            Console.WriteLine("Максимальный скачок (в градусах) составил {0}, это случилось в день {1}, температура которого составляла {2} градусов.", delta_degrees, number_of_the_day, degree);
            Console.WriteLine("Количество дней с температурой, равной нулю: {0}", weather.CountWithZeros);
            weather.Day = 1;
            weather.PrintArray();
            //int count_of_strings = (day + MatrixWeather.days[month - 1]) / 7;
            //MatrixWeather.FillArray(count_of_strings);
        }
    }
}
