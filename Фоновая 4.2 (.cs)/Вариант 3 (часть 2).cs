using System;

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
        public MatrixWeather(string str)
        {
            this.day = 1;
            this.month = 1;
            int[,] temperature = new int[4, 7];
            this.temperature = new int[,] { { 1, 2, 3, 4, 5, 6, 7 }, { 8, 9, 10, 11, 12, 13, 14}, { 15, 16, 17, 18, 19, 20, 21 }, 
                { 22, 23, 24, 25, 26, 27, 28 } };
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
                        if (month == 12 || month <= 2) temperature[i, j] = rnd.Next(-15, -10);
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
                            int temp = -100, temp2 = -100;

                            for (int z = 0; z < delta_days; z++)
                            {
                                for (int i = 0; i < copy.GetLength(0); i++)
                                {
                                    temp2 = copy[i, 6];
                                    for (int j = copy.GetLength(1) - 1; j > 0; j--)
                                    {
                                        copy[i, j] = copy[i, j - 1];
                                    }
                                    copy[i, 0] = temp;
                                    temp = temp2;
                                }
                            }
                            temperature = new int[count_of_strings, 7];
                            temperature = copy;
                        }
                        else // сдвиг влево
                        {
                            int temp = -1000;
                            for (int z = 0; z < -delta_days; z++)
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
                        day = value;
                    }
                }
                catch (Exception err) { Console.WriteLine("Ошибка: " + err.Message); }

            }
        }

        public int DaysInDiary // Свойство для чтения количества дней в дневнике
        {
            get { return days[month - 1];  }
        }

        public static bool operator >(MatrixWeather obj1, MatrixWeather obj2) // Перегрузка > (сравниваю месяцы дневников)
        {
            if (obj1.month > obj2.month) return true;
            return false;
        }

        public static bool operator <(MatrixWeather obj1, MatrixWeather obj2) // Перегрузка < (сравниваю месяцы дневников)
        {
            if (obj1.month < obj2.month) return true;
            return false;
        }

        public static MatrixWeather operator ++(MatrixWeather obj) // Перегрузка ++ (прибавляю день, если это возможно)
        {
            obj.Day += 1;
            return obj;
        }

        public static MatrixWeather operator --(MatrixWeather obj) // Перегрузка -- (вычитаю день, если это возможно)
        {
            obj.Day -= 1;
            return obj;
        }

        public static bool operator true(MatrixWeather obj) // Перегрузка true
        {
            int count = 0;
            foreach (int x in obj.temperature)
            {
                if (x != -100 && x < 0) count++;
            }
            if (count == 0) return true;
            return false;
        }

        public static bool operator false(MatrixWeather obj) // Перегрузка false
        {
            int count = 0;
            foreach (int x in obj.temperature)
            {
                if (x != -100 && x < 0) count++;
            }
            if (count == 0) return false;
            return true;
        }

        public static bool operator &(MatrixWeather obj1, MatrixWeather obj2) // Перегрузка &
        {
            for (int i = 0; i < obj1.temperature.GetLength(0); i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (obj1.temperature[i, j] != -100 && obj2.temperature[i, j] != -100)
                    {
                        if (obj1[i, j] != obj2[i, j]) return false;
                    }
                }
            }
            return true;
            // if ((obj1[0, 6] == obj2[0, 6]) || (obj1[0, 5] == obj2[0, 5])) return true;
            // return false;
        }

        public int this[int i, int j] // Двумерный индексатор
        {
            get
            {
                try
                {
                    if (temperature[i, j] == -100)
                    {
                        Console.WriteLine("Увы, за этот день нет данных");
                        return -100;
                    }
                    else
                    {
                        return temperature[i, j];
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Вы вышли за пределы массива");
                    return -100;
                }
            }
            set
            {
                try
                {
                    if (temperature[i, j] != -100) {
                 
                        if (month == 12 || month <= 2)
                        {
                            if (value >= -15 && value <= -10) temperature[i, j] = value;
                            else Console.WriteLine("Неверная температура для данного месяца");
                        }
                        else if (month >= 3 && month <= 5)
                        {
                            if (value >= -5 && value <= 10) temperature[i, j] = value;
                            else Console.WriteLine("Неверная температура для данного месяца");
                        }
                        else if (month >= 6 && month <= 8)
                        {
                            if (value >= 10 && value <= 30) temperature[i, j] = value;
                            else Console.WriteLine("Неверная температура для данного месяца");
                        }
                        else 
                        {
                            if (value >= 5 && value <= 15) temperature[i, j] = value;
                            else Console.WriteLine("Неверная температура для данного месяца");
                        }
                    }
                    else Console.WriteLine("Увы, за этот день нет данных");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Вы вышли за пределы массива");
                }
            }
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
            Console.WriteLine("Создаю второй дневник по умолчанию.");
            MatrixWeather weather2 = new MatrixWeather();
            //weather = new MatrixWeather("Первый");
            //MatrixWeather weather2 = new MatrixWeather("Второй");


            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ПЕЧАТАЮ ПЕРВЫЙ ДНЕВНИК!");
            Console.BackgroundColor = ConsoleColor.Black;
            weather.PrintArray();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ПЕЧАТАЮ ВТОРОЙ ДНЕВНИК!");
            Console.BackgroundColor = ConsoleColor.Black;
            weather2.PrintArray();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Максимальный скачок в первом дневнике (в градусах): {0}", weather.MaxDifference());
            int number_of_the_day, delta_degrees;
            int degree = weather.MaxDifference(out delta_degrees, out number_of_the_day);
            Console.WriteLine("Максимальный скачок в первом дневнике (в градусах) составил {0}, это случилось в день {1}, температура которого составляла {2} градусов.", delta_degrees, number_of_the_day, degree);
            Console.WriteLine("Количество дней в первом дневнике с температурой, равной нулю: {0}", weather.CountWithZeros);
            Console.WriteLine("Количество дней в первом дневнике: {0}", weather.DaysInDiary);
            if (weather > weather2) Console.WriteLine("Да, первый дневник хранит информацию о более позднем месяце");
            else Console.WriteLine("Нет, первый дневник хранит информацию о менее позднем месяце");
            if (weather < weather2) Console.WriteLine("Да, второй дневник хранит информацию о более позднем месяце");
            else Console.WriteLine("Нет, второй дневник хранит информацию о менее позднем месяце");
            Console.WriteLine("Операция инкремента над первым дневником!");
            weather++;
            weather.PrintArray();
            Console.WriteLine("Операция декремента над первым дневником!");
            weather--;
            weather.PrintArray();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Введи, пожалуйста, день недели, о котором хочешь узнать информацию: ");
            int j = int.Parse(Console.ReadLine());
            Console.Write("Введи, пожалуйста, номер недели: ");
            int i = int.Parse(Console.ReadLine());
            int info = weather[i - 1, j - 1];
            if (info != -100) Console.WriteLine("Температура в день [{0}, {1}]: {2}", i, j, info);

            Console.Write("Введи, пожалуйста, день недели, который хочешь изменить: ");
            j = int.Parse(Console.ReadLine());
            Console.Write("Введи, пожалуйста, номер недели: ");
            i = int.Parse(Console.ReadLine());
            Console.Write("Введи, пожалуйста, значение температуры своего дня: ");
            int t = int.Parse(Console.ReadLine());
            weather[i - 1, j - 1] = t;
            weather.PrintArray();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            if (weather) Console.WriteLine("За рассмотренный период температура ни разу не опускалась ниже 0");
            else Console.WriteLine("За рассмотренный период температура опускалась ниже 0");
            if (weather & weather2) Console.WriteLine("Дневники совпадают");
            else Console.WriteLine("Дневники не совпадают");
            //Console.Write("Хотите поменять значение месяца? ");
            //answer = Console.ReadLine();
            //if (answer.ToLower() == "да" || answer.ToLower() == "lf") { Console.Write("Введи месяц: "); weather.Month = int.Parse(Console.ReadLine()); }
            //Console.Write("Хотите поменять значение дня? ");
            //answer = Console.ReadLine();
            //if (answer.ToLower() == "да" || answer.ToLower() == "lf") { Console.Write("Введи день: "); weather.Day = int.Parse(Console.ReadLine()); }
            //weather.PrintArray();
        }
    }
}
