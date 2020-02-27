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

        // Конструктор с параметрами
        public Parallelepiped(int h, int sh, int g, int x, int y)
        {
            this.h = h;
            this.sh = sh;
            this.g = g;
            this.x = x;
            this.y = y;
        }

        // Свойство высоты + исключение
        public int H
        {
            get { return h; }
            set
            {
                try
                {
                    if (value <= 0) throw new Exception("Высота не может быть отрицательной! Ставлю значение по умолчанию, равное 5");
                    else h = value;
                }
                catch (Exception err)
                {
                    Console.WriteLine("Ошибка: " + err.Message);
                    h = 5;
                }
            }
        }

        // Свойство ширины + исключение
        public int SH
        {
            get { return sh; }
            set
            {
                try
                {
                    if (value <= 0) throw new Exception("Ширина не может быть отрицательной! Ставлю значение по умолчанию, равное 6");
                    else sh = value;
                }
                catch (Exception err)
                {
                    Console.WriteLine("Ошибка: " + err.Message);
                    sh = 6;
                }
            }
        }

        // Свойство глубины + исключение
        public int G
        {
            get { return g; }
            set
            {
                try
                {
                    if (value <= 0) throw new Exception("Ширина не может быть отрицательной! Ставлю значение по умолчанию, равное 7");
                    else g = value;
                }
                catch (Exception err)
                {
                    Console.WriteLine("Ошибка: " + err.Message);
                    g = 7;
                }
            }
        }

        // Свойство: является ли объект кубом?
        public bool isCube
        {
            get
            {
                if (h == sh && sh == g) return true;
                else return false;
            }
        }

        // Информация о параллелепипеде
        public void Info(string number)
        {
            Console.WriteLine("\nИнформация о {0} параллелепипеде:\n" +
                "высота = {1}\nширина = {2}\nглубина = {3}\nкоордината вставки: {4};{5}\n", number, h, sh, g, x, y);
        }

        // Свойство для чтения объема параллелепипеда
        public int Volume
        {
            get
            {
                return sh * g * h;
            }
        }

        // Свойство для чтения площади боковых сторон
        public int SideS
        {
            get
            {
                return 2 * (sh * h + g * h);
            }
        }

        // Свойство для чтения диагонали параллелепипеда
        public double Diagonal
        {
            get
            {
                return Math.Sqrt(sh * sh + h * h + g * g);
            }
        }

        // Свойство для чтения площади основания
        public int BasisS
        {
            get
            {
                return sh * g;
            }
        }

        // Свойство для чтения площади поверхности параллелепипеда
        public int SurfaceS
        {
            get
            {
                return SideS + 2 * BasisS;
            }
        }

        // Свойство для чтения длин ребер параллелепипеда
        public int LengthOfEdges
        {
            get
            {
                return 4 * (sh + g + h);
            }
        }

        // Свойство для чтения радиуса шара, описанного около параллелепипеда (куба)
        public double SphereRadius
        {
            get
            {
                if (isCube) return Diagonal / 2;
                else return 0;
            }
        }

        /* Сдвигаю параллелепипед на координату (x;y)
        public void Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }*/

        /* Ставлю новые глубину и ширину
        public void Change(int g, int sh)
        {
            this.g = g;
            this.sh = sh;
        }*/

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
                int height, width, depth;
                try
                {
                    Console.Write("Введите высоту параллелепипеда: ");
                    height = int.Parse(Console.ReadLine());
                    if (height <= 0) throw new Exception();
                }
                catch
                {
                    Console.WriteLine("Введена неверная высота, ставлю значение, равное 5");
                    height = 5;
                }
                try
                {
                    Console.Write("Введите ширину параллелепипеда: ");
                    width = int.Parse(Console.ReadLine());
                    if (width <= 0) throw new Exception();
                }
                catch
                {
                    Console.WriteLine("Введена неверная ширина, ставлю значение, равное 6");
                    width = 6;
                }
                try
                {
                    Console.Write("Введите глубину параллелепипеда: ");
                    depth = int.Parse(Console.ReadLine());
                    if (depth <= 0) throw new Exception();
                }
                catch
                {
                    Console.WriteLine("Введена неверная высота, ставлю значение, равное 7");
                    depth = 7;
                }
                Console.Write("Введите координату x вставки: ");
                int x = int.Parse(Console.ReadLine());
                Console.Write("Введите координату y вставки: ");
                int y = int.Parse(Console.ReadLine());
                first = new Parallelepiped(height, width, depth, x, y);
                Console.WriteLine("Сработал конструктор с параметрами!");
            }
            // Для теста вставок из части 1: first = new Parallelepiped(0, 6, 7, 0, 0);

            first.Info("первом");
            Console.WriteLine("Объем параллелепипеда: {0} см3", first.Volume);
            Console.WriteLine("Площадь боковой поверхности параллелепипеда: {0} см2", first.SideS);
            Console.WriteLine("Площадь основания параллелепипеда: {0} см2", first.BasisS);
            Console.WriteLine("Площадь полной поверхности параллелепипеда: {0} см2", first.SurfaceS);
            Console.WriteLine("Диагональ параллелепипеда: {0:f2} см", first.Diagonal);
            Console.WriteLine("Периметр всех ребр параллелепипеда: {0} cм", first.LengthOfEdges);
            Console.WriteLine("Является ли  параллелепипед кубом: {0}",  first.isCube ? "да" : "нет");
            if (first.SphereRadius != 0) Console.WriteLine("Радиус шара, описанного около параллелепипеда: {0:f2} см", first.SphereRadius);
            else Console.WriteLine("Шар невозможно описать");


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
