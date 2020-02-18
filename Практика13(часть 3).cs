using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Класс_Point3D
{
    class Point3D
    {
        private int x;
        private int y;
        private int z;

        public Point3D() {  } // конструктор по умолчанию

        public Point3D(int x, int y, int z) // конструктор по значениям
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Move(char direction, int value) // сдвиг 
        {
            if (direction == 'x') this.x += value;
            else if (direction == 'y') this.y += value;
            else this.z += value;
        }

        public void MyPrint() // вывод координат
        {
            Console.WriteLine("Координаты точки: ({0}, {1}, {2})", this.x, this.y, this.z);
        }

        public double RadiusVector // радиус-вектор 
        {
            get { return Math.Sqrt(x * x + y * y + z * z); }
        }

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                if (value > 0) x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                if (value > 0 && value <= 100) y = value;
                else y = 100;
            }
        }

        public int Z
        {
            get
            {
                return z;
            }
            set
            {
                if (z < (x + y)) z = value;
                else Console.WriteLine("Ваше значение z превышает допустимое!");
            }
        }
        public void Sum(Point3D obj) // суммирование
        {
            this.x += obj.x;
            this.y += obj.y;
            this.z += obj.z;
        }
        public void Sum(int x, int y, int z)
        {
            this.x += x;
            this.y += y;
            this.z += z;
        }
        public int Multiplication
        {
            set
            {
                x *= value;
                y *= value;
                z *= value;
            }
        }

    }
    class Program
    {
        static Point3D NewPoint()
        {
            Point3D point;
            Console.Write("Если хотите использовать конструктор по умолчанию, введите 1, иначе любую цифру: ");
            int command = int.Parse(Console.ReadLine());
            if (command == 1) point = new Point3D();
            else
            {
                Console.Write("Введите координату x: ");
                int x = int.Parse(Console.ReadLine());
                Console.Write("Введите координату y: ");
                int y = int.Parse(Console.ReadLine());
                Console.Write("Введите координату z: ");
                int z = int.Parse(Console.ReadLine());
                point = new Point3D(x, y, z);
            }
            return point;
        }
        static void Main(string[] args)
        {
            Point3D point1 = NewPoint();
            Point3D point2 = NewPoint();

            int command;
            char direction; // Направление для сдвига точки
            int value; // На сколько сдвигать точку
            while (true)
            {
                Console.Write(
                    "Введите:\n1 - для сдвига точки" +
                    "\n2 - для вывода координат точки" +
                    "\n3 - для выхода" +
                    "\n4 - для получения радиус-вектора" +
                    "\n5 - для суммирования полей 1ой точки через второй объект" +
                    "\n6 - для суммирования полей 1ой точки через числа" +
                    "\n7 - для увеличения полей 1ой точки на коэффициент: "
                );
                command = int.Parse(Console.ReadLine());
                if (command == 1)
                {
                    Console.Write("По какой оси сдвигать точку? ");
                    direction = char.Parse(Console.ReadLine());
                    Console.Write("На сколько сдвигать точку? ");
                    value = int.Parse(Console.ReadLine());
                    point1.Move(direction, value);
                }
                else if (command == 2)
                {
                    point1.MyPrint();
                }
                else if (command == 3) break;
                else if (command == 4) Console.WriteLine("Радиус-вектор 1ой точки равен: {0:f2}", point1.RadiusVector);
                else if (command == 5)
                {
                    point1.Sum(point2);
                    point1.MyPrint();
                }
                else if (command == 6)
                {
                    Console.WriteLine("Введите x, y, z");
                    int x = int.Parse(Console.ReadLine());
                    int y = int.Parse(Console.ReadLine());
                    int z = int.Parse(Console.ReadLine());
                    point1.Sum(x, y, z);
                    point1.MyPrint();
                }
                else if (command == 7)
                {
                    Console.Write("Введите коэффициент: ");
                    point1.Multiplication = int.Parse(Console.ReadLine());
                    point1.MyPrint();
                }
                else if (command == 8)
                {
                    
                }
            }
        }
    }
}
