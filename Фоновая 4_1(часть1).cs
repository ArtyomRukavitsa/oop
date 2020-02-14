using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Вариант8
{
    class Parallelepiped
    {
        int h;
        int sh;
        int g;
        int x;
        int y;

        // Конструктор по умолчанию
        public Parallelepiped()
        {
            h = 5; sh = 6; g = 7;
        }

        // Конструктор по параметрам
        public Parallelepiped(int h, int sh, int g, int x, int y)
        {
            this.h = h;
            this.sh = sh;
            this.g = g;
            this.x = x;
            this.y = y;
        }

        // Информация о параллелепипеде
        public void Info(string number)
         {
             Console.WriteLine("\nИнформация о {0} параллелепипеде:\n" +
                 "высота = {1}\nширина = {2}\nглубина = {3}\nкоордината вставки: {4};{5}\n", number, h, sh, g, x, y);
         }

        // Объем
        public int Size()
        {
            return sh * g * h;
        }


        // Площадь боковых сторон
        public int SideS()
        {
            return 2 * (sh * h + g * h);
        }

        public int GetHeight() { return h; }

        public int GetWidth() { return sh; }

        public int GetDepth() { return g; }

        // Сдвигаю параллелепипед на координату (x;y)
        public void Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Ставлю новые глубину и ширину
        public void Change(int g, int sh)
        {
            this.g = g;
            this.sh = sh;
        }

        // Пересечение по оси Х
        private int IntersectionX(int x1, int x2, int x3, int x4)
        {
            int delta = 0;
            if (x1 <= x3 && x1 <= x2)
            {
                if (x4 <= x2) delta = x4 - x3;
                else delta = x2 - x3;
            }
            else if (x3 <= x1 && x1 <= x4)
            {
                if (x1 <= x3) delta = x3 - x4;
                else delta = x4 - x1;
            }
            return delta;
        }

        // Пересечение по оси У
        private int IntersectionY(int y1, int y2, int y3, int y4)
        {
            int delta = 0;
            if (y1 >= y3 && y3 >= y2)
            {
                if (y4 >= y2) delta = y3 - y4;
                else delta = y3 - y2;
            }
            else if (y3 >= y1 && y1 >= y4)
            {
                if (y1 >= y3) delta = y3 - y4;
                else delta = y1 - y4;
            }
            return delta;
        }

        // Метод, вызывающий два предыдущих для поиска пересечения
        public void FindRectangle(Parallelepiped obj)
        {
            int new_g = IntersectionX(x, x + g, obj.x, obj.x + obj.g);
            int new_sh = IntersectionY(y, y - sh, obj.y, obj.y - obj.sh);
            Console.Write("Пересечение двух прямоугольников: ");
            if (new_g == 0 || new_sh == 0) { new_g = 0; new_sh = 0; Console.WriteLine("нет"); }
            else Console.WriteLine("g: {0} sh: {1}", new_g, new_sh);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Parallelepiped first;
            Console.Write("Если хотите использовать конструктор по умолчанию, введите 1, иначе любую цифру: ");
            int command = int.Parse(Console.ReadLine());
            if (command == 1) { first = new Parallelepiped(); Console.WriteLine("Сработал конструктор по умолчанию!"); }
            else
            {
                Console.Write("Введите высоту параллелепипеда: ");
                int height = int.Parse(Console.ReadLine());
                Console.Write("Введите ширину параллелепипеда: ");
                int width = int.Parse(Console.ReadLine());
                Console.Write("Введите глубину параллелепипеда: ");
                int depth = int.Parse(Console.ReadLine());
                Console.Write("Введите координату x вставки: ");
                int x = int.Parse(Console.ReadLine());
                Console.Write("Введите координату y вставки: ");
                int y = int.Parse(Console.ReadLine());
                first = new Parallelepiped(height, width, depth, x, y);
                Console.WriteLine("Сработал конструктор с параметрами!");
            }
            //first = new Parallelepiped(0, 6, 7, 0, 0);

            first.Info("первом");
            Console.WriteLine("Объем параллелепипеда: {0} см3", first.Size());
            Console.WriteLine("Площадь боковой поверхности параллелепипеда: {0} см2", first.SideS());
           
            /*Console.WriteLine("Проверяю тесты!");
            Console.WriteLine("\nТест 1");
            Parallelepiped second = new Parallelepiped(0, 2, 2, 1, -1);
            second.Info("втором");
            first.FindRectangle(second);

            Console.WriteLine("\nТест 2");
            second.Move(5, 1);
            second.Info("втором");
            first.FindRectangle(second);

            Console.WriteLine("\nТест 3");
            second.Move(6, 3);
            second.Info("втором");
            first.FindRectangle(second);

            Console.WriteLine("\nТест 4");
            second.Move(5, -5);
            second.Info("втором");
            first.FindRectangle(second);

            Console.WriteLine("\nТест 5");
            second.Move(-1, 1);
            second.Info("втором");
            first.FindRectangle(second);

            Console.WriteLine("\nТест 6");
            second.Move(0, -5);
            second.Info("втором");
            first.FindRectangle(second);

            Console.WriteLine("\nТест 7");
            second.Move(-4, 2);
            second.Info("втором");
            first.FindRectangle(second);

            Console.WriteLine("\nТест 8");
            second.Move(6, -2);
            second.Info("втором");
            first.FindRectangle(second);

            Console.WriteLine("\nТест 9");
            second.Move(3, -6);
            second.Info("втором");
            first.FindRectangle(second);

            Console.WriteLine("\nТест 10");
            second.Move(4, -3);
            second.Info("втором");
            first.FindRectangle(second);*/

        }
    }
}
