using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Вариант_6
{
    class Program
    {
        struct TDate
        {
            public int Year;
            public int Month;
            public int Day;
            public int Hour;
            public int Minute;
            public Months MonthName;
        };

        enum Months { январь = 1, февраль, март, апрель, май, июнь, июль, август, сентябрь, октябрь, ноябрь, декабрь };

        enum nMonths
        {
            январь = 31, февраль = 28, март = 31,
            апрель = 30, май = 31, июнь = 30, июль = 31, август = 31,
            сентябрь = 30, октябрь = 31, ноябрь = 30, декабрь = 31
        };

        static TDate InputTDate()
        {
            TDate data;
            Console.Write("Введите год: ");
            data.Year = int.Parse(Console.ReadLine());
            Console.Write("Введите месяц: ");
            data.Month = int.Parse(Console.ReadLine());
            while (data.Month > 12 || data.Month < 1)
            {
                Console.WriteLine("Такая дата невозможна! Введите еще раз: ");
                data.Month = int.Parse(Console.ReadLine());
            }

            Console.Write("Введите день: ");
            data.Day = int.Parse(Console.ReadLine());
            while (data.Day > 31)
            {
                Console.WriteLine("Такая дата невозможна! Введите еще раз: ");
                data.Day = int.Parse(Console.ReadLine());
            }
            Console.Write("Введите час: ");
            data.Hour = int.Parse(Console.ReadLine());
            while (data.Day > 31)
            {
                Console.WriteLine("Время указано неверно! Введите еще раз: ");
                data.Day = int.Parse(Console.ReadLine());
            }
            Console.Write("Введите минуту: ");
            data.Minute = int.Parse(Console.ReadLine());
            while (data.Minute >= 60)
            {
                Console.WriteLine("Время указано неверно! Введите еще раз: ");
                data.Minute = int.Parse(Console.ReadLine());
            }
            data.MonthName = (Months)data.Month;
            return data;
        }

        static void WriteTDate(TDate data)
        {
            string hour, minute;
            if (data.Hour < 10) hour = "0" + Convert.ToString(data.Hour);
            else hour = Convert.ToString(data.Hour);
            if (data.Minute < 10) minute = "0" + Convert.ToString(data.Minute);
            else minute = Convert.ToString(data.Minute);
            Console.WriteLine($"Дата: {data.Day} {data.MonthName} {data.Year}г.\nВремя: {hour}:{minute}");
        }

        static int Check(TDate data, int date, int month, int year)
        {
            int delta = 0;
            int[] nMonths = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int delta_year = data.Year - year;
            int c = 1;
            Console.WriteLine($"delta_year: {delta_year}");
            if (delta_year >= 2)
            {
                if (data.Month >= month)
                {
                    if (data.Day >= date)
                    {
                        delta += (data.Year - year) * 365;
                        delta_year = 0;
                    }
                    else
                    {
                        delta += (data.Year - year - 1) * 365;
                        delta_year = 1;
                    }
                }
                else
                {
                    delta += (data.Year - year - 1) * 365;
                    delta_year = 1;
                }
                
            }
            Console.WriteLine($"delta: {delta}");
            if (delta_year == 1)
            {
                c = 1;
                foreach (int el in nMonths)
                {
                    if (month == c) delta += (el - date);
                    else if (month < c) delta += el;
                    c++;
                }
                c = 1;
                foreach (int el in nMonths)
                {
                    if (c < data.Month) delta += el;
                    else if (c == data.Month) delta += data.Day;
                    c++;
                }

            }
            if (delta_year == 0)
            {
                foreach (int el in nMonths)
                {
                    if (month == c) delta += (el - date);
                    else if (month < c && c < data.Month) delta += el;
                    else if (c == data.Month) delta += data.Day;
                    c++;
                }
            }
            return delta;
        }

        static void Main(string[] args)
        {
            DateTime today = DateTime.Today;
            string[] f = Convert.ToString(today).Split(' ');
            string[] s = f[0].Split('.');
            int date = int.Parse(s[0]);
            int month = int.Parse(s[1]);
            int year = int.Parse(s[2]);

            Console.WriteLine($"Сегодняшняя дата: {date}.{month}.{year}\n");
            TDate myDate = InputTDate();
            DateTime new_day = new DateTime(myDate.Year, myDate.Month, myDate.Day);
            int result = DateTime.Compare(today, new_day);
            Console.Write("\nСравнение показало, что ");
            if (result > 0) Console.WriteLine("вы опоздали");
            else Console.WriteLine("дата наступит через дней: {0}", Check(myDate, date, month, year));
            //Console.WriteLine("\nНапоминалка!");
            WriteTDate(myDate);
        }
    }
}