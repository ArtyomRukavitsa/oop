using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Практика11

{
    class Program
    {
        enum Months { январь = 1, февраль, март, апрель, май, июнь, июль, август, сентябрь, октябрь, ноябрь, декабрь };

        /*enum Seasons
        {
            январь = 'З', февраль = 'З',
            март = 'В', апрель = 'В', май = 'В',
            июнь = 'Л', июль = 'Л', август = 'Л',
            сентябрь = 'О', октябрь = 'О', ноябрь = 'О', декабрь = 'З'
        }*/

        enum nMonths
        {
            январь = 31, февраль = 28, март = 31,
            апрель = 30, май = 31, июнь = 30, июль = 31, август = 31,
            сентябрь = 30, октябрь = 31, ноябрь = 30, декабрь = 31
        };
        
        static int GetMonth(int month, int number, int days)
        {
            int c = 1;
            bool flag = true;
            while (days > 365) days -= 365;
            foreach (int el in Enum.GetValues(typeof(nMonths)))
            {
                if (month == c) days -= (el - number);
                else if (month < c) days -= el;
                if (days < 0) { flag = false;  break; }
                c++;
            }
            // Для случаев, если месяц не найден. Это может быть, если количество дней, 
            // прошедших в году, меньше, чем мы задали
            if (flag)
            {
                c = 1;
                foreach (int el in Enum.GetValues(typeof(nMonths)))
                {
                    days -= el;
                    if (days < 0) break;
                    c++;
                }
            }
            return c;
        }

        static void Main(string[] args)
        {
            Months name;
            for (name = Months.январь; name <= Months.декабрь; name++) Console.WriteLine("Месяц: \"{0}\", соответствует числу {1}", name, (int)name);

            Console.Write("Введите номер месяца: ");
            int n = int.Parse(Console.ReadLine());
            Console.Write("Введите номер дня: ");
            int number = int.Parse(Console.ReadLine());
            Console.Write("Какое количество дней нужно отсчитать? ");
            int days = int.Parse(Console.ReadLine());

            int month = GetMonth(n, number, days);

            if (month == 12 || month <= 2) Console.WriteLine("Зима!");
            else if (month >= 3 && month <= 5) Console.WriteLine("Весна!");
            else if (month >= 6 && month <= 8) Console.WriteLine("Лето!");
            else Console.WriteLine("Осень!");
        }
    }
}
