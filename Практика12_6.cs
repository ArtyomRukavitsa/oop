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
            while (data.Minute > 31)
            {
                Console.WriteLine("Время указано неверно! Введите еще раз: ");
                data.Minute = int.Parse(Console.ReadLine());
            }
            data.MonthName = (Months) data.Month;
            return data;
        }

        static void WriteTDate(TDate data)
        {
            Console.WriteLine($"Дата: {data.Day} {data.MonthName} {data.Year}г.\nВремя: {data.Hour}:{data.Minute}");
        }
        static void Main(string[] args)
        {
            DateTime today = DateTime.Today;
            TDate myDate = InputTDate();
            WriteTDate(myDate);
        }
    }
}
