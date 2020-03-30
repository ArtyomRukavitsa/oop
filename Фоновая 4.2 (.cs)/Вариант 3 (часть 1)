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
        static Random rnd = new Random();

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
            temperature = FillArray(count_of_strings, day, month);
        }
        static int[,] FillArray(int count_of_strings, int day, int month) // заполнение массива через рандом
        {
            int count = 1;
            int[,] temperature = new int[count_of_strings, 7];
            for (int i = 0; i < count_of_strings; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if ((i == 0 && j >= day - 1) || (i >= 1 && i < count_of_strings - 1) || (i == count_of_strings - 1 && count <= days[month - 1]))
                    {
                        count++;
                        if (month == 12 || month <= 2) temperature[i, j] = rnd.Next(-20, -5);
                        else if (month >= 3 && month <= 5) temperature[i, j] = rnd.Next(-5, 10);
                        else if (month >= 6 && month <= 8) temperature[i, j] = rnd.Next(10, 30);
                        else temperature[i, j] = rnd.Next(5, 15);
                    }
                    else temperature[i, j] = -100;
                }
            }
            return temperature;
        }
        public void PrintArray() // Pretty print 
        {
            int count = 1;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine((Months)(month));
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Пн\tВт\tСр\tЧт\tПт\tСб\tВс");
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < temperature.GetLength(0); i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (temperature[i, j] == -100) Console.Write("\t");
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
        }

        public int MaxDifference() // Поиск max дельты в температуре за день
        {
            int delta = -1;
            for (int i = 0; i < temperature.GetLength(0); i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (j == 6)
                    {
                        if (i != temperature.GetLength(0) - 1 && Math.Abs(temperature[i, j] - temperature[i + 1, 0]) > delta && temperature[i, j] != -100 & temperature[i + 1, 0] != -100)
                        {
                            delta = Math.Abs(temperature[i, j] - temperature[i + 1, 0]);
                        }
                    }
                    else
                    {
                        if (Math.Abs(temperature[i, j] - temperature[i, j + 1]) > delta && temperature[i, j] != -100 & temperature[i, j + 1] != -100)
                        {
                            delta = Math.Abs(temperature[i, j] - temperature[i, j + 1]);
                        }
                    }
                }
            }
            return delta;

            
        }
        public int MaxDifference(out int delta, out int day) // перегрузка метода MaxDifference
        {
            delta = -1;
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
                        }
                        if (i != temperature.GetLength(0) - 1 && temperature[i, j] != -100 & temperature[i + 1, 0] != -100) count++;
                    }
                    else
                    {
                        if (Math.Abs(temperature[i, j] - temperature[i, j + 1]) > delta && temperature[i, j] != -100 & temperature[i, j + 1] != -100)
                        {
                            delta = Math.Abs(temperature[i, j] - temperature[i, j + 1]);
                            day = count;
                            temp = temperature[i, j];
                        }
                        if (temperature[i, j] != -100 & temperature[i, j+1] != -100) count++;
                    }
                }
            }
            return temp;
        }

        public int CountWithZeros // Cвойство для чтения количества дней с нулевой температурой
        {
            get
            {
                int count = 0;
                foreach (int x in temperature) if (x == 0) count++;
                return count;
            }
        }

        public int Month // Свойство для чтения и записи месяца
        {
            get { return month;  }
            set
            {
                try
                {
                    if (value <= 0 || value >= 13) throw new Exception("Значение месяца может быть от 1 до 12! Оставляю прежнее значение!");
                    else
                    {
                        month = value;
                        int count_of_strings = (day + days[this.month - 1]) / 7;
                        if ((day + days[this.month - 1]) % 7 != 0) count_of_strings += 1;
                        temperature = FillArray(count_of_strings, day, month);
                    }
                }
                catch (Exception err) { Console.WriteLine("Ошибка: " + err.Message); }
            }
        }

        public int[,] Temperature // Свойство для чтения температуры
        {
            get { return temperature; }
        }

        public int Day // Свойство для чтения и записи дня
        {
            get { return day; }
            set
            {
                try
                {
                    if (value <= 0 || value >= 8) throw new Exception("Значение дня только от 1 до 7! Оставляю прежнее значение!");
                    else
                    {
                        int delta_days = value - day;

                        if (delta_days > 0) // сдвиг вправо 
                        {
                            int count_of_strings = (day + days[this.month - 1] + value) / 7;
                            if ((day + days[this.month - 1] + value) % 7 != 0) count_of_strings += 1;
                            int[,] copy = new int[count_of_strings, 7];
                            for (int i = 0; i < count_of_strings; i++) { for (int j = 0; j < 7; j++) copy[i, j] = -100; }
                            for (int i = 0; i < temperature.GetLength(0); i++) { for (int j = 0; j < temperature.GetLength(1); j++) copy[i, j] = temperature[i, j]; }
                            int temp = -1000, temp2 = -1000;
                            
                            for (int z = 0; z < delta_days; z++)
                            {
                                for (int i = 0; i < copy.GetLength(0); i++)
                                {
                                    if (i == 0) temp = copy[i, 6];
                                    else temp2 = copy[i, 6];
                                    for (int j = copy.GetLength(1) - 1; j > 0; j--)
                                    {
                                        copy[i, j] = copy[i, j - 1];
                                    }
                                    if (i != 0) { copy[i, 0] = temp; temp = temp2; }
                                }
                            }
                            temperature = new int[count_of_strings, 7];
                            temperature = copy;
                        }
                        else // сдвиг влево
                        {
                            int temp = -1000;
                            delta_days = -delta_days;
                            for (int z = 0; z < delta_days; z++)
                            {
                                for (int i = 0; i < temperature.GetLength(0); i++)
                                {
                                    if (i != 0) temp = temperature[i, 0];
                                    for (int j = 1; j < temperature.GetLength(1); j++)
                                    {
                                        temperature[i, j - 1] = temperature[i, j];
                                    }
                                    if (i != 0) temperature[i - 1, 6] = temp;
                                }
                            }
                        }
                    }
                }
                catch (Exception err) { Console.WriteLine("Ошибка: " + err.Message); }

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
            MatrixWeather weather;
            Console.Write("Ходите создать календарь по умолчанию? ");
            string answer = Console.ReadLine();
            if (answer.ToLower() == "да" || answer.ToLower() == "lf") weather = new MatrixWeather();
            else
            {
                Console.Write("Введи месяц: ");
                int month = int.Parse(Console.ReadLine());
                Console.Write("Введи день: ");
                int day = int.Parse(Console.ReadLine());
                weather = new MatrixWeather(day, month);
            }
            weather.PrintArray();
            Console.WriteLine("Максимальный скачок (в градусах): {0}", weather.MaxDifference());
            int number_of_the_day, delta_degrees;
            int degree = weather.MaxDifference(out delta_degrees, out number_of_the_day);
            Console.WriteLine("Максимальный скачок (в градусах) составил {0}, это случилось в день {1}, температура которого составляла {2} градусов.", delta_degrees, number_of_the_day, degree);
            Console.WriteLine("Количество дней с температурой, равной нулю: {0}", weather.CountWithZeros);
            Console.WriteLine("Количество дней в месяце: {0}", weather.DaysInDiary);
            Console.Write("Хотите поменять значение месяца? ");
            answer = Console.ReadLine();
            if (answer.ToLower() == "да" || answer.ToLower() == "lf") { Console.Write("Введи месяц: "); weather.Month = int.Parse(Console.ReadLine()); }
            Console.Write("Хотите поменять значение дня? ");
            answer = Console.ReadLine();
            if (answer.ToLower() == "да" || answer.ToLower() == "lf") { Console.Write("Введи день: "); weather.Day = int.Parse(Console.ReadLine()); }
            weather.PrintArray();
        }
    }
}
