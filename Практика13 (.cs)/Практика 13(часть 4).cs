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

       /* public void Move(char direction, int value) // сдвиг 
        {
            if (direction == 'x') this.x += value;
            else if (direction == 'y') this.y += value;
            else this.z += value;
        }*/

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
            get { return x; }
            set
            {
                try
                {
                    if (value < 0) throw new Exception("Ваше число отрицательное, оставляю прежним!");
                    else x = value;
                }
                catch (Exception err)
                {
                    Console.WriteLine("Ошибка: " + err.Message);
                }
                
            }
        }

        public int Y
        {
            get { return y; }
            set
            {
                try
                {
                    if (value < 0) {
                        y = 100;
                        throw new Exception("Ваше число меньше нуля, недопустимое значение! Ставлю значение 100");
                    }
                    else if (value > 100) {
                        y = 100;
                        throw new Exception("Ваше число больше ста, недопустимое значение! Ставлю значение 100");
                    }
                    else y = value;
                }
                catch (Exception err)
                {
                    Console.WriteLine("Ошибка: " + err.Message);
                }
            }
        }

        public int Z
        {
            get { return z; }
            set
            {
                try
                {
                    if (value > (x + y)) throw new Exception("Ваше значение z превышает сумму (x + y), оставляю прежним!");
                    else z = value;
                }
                catch (Exception err)
                {
                    Console.WriteLine("Ошибка: " + err.Message);
                }
            }
        }
        public void Sum(Point3D obj) // суммирование через объект
        {
            this.x += obj.x;
            this.y += obj.y;
            this.z += obj.z;
        }

        public void Sum(int x, int y, int z) // суммирование через три параметра
        {
            this.x += x;
            this.y += y;
            this.z += z;
        }

        public void Sum(int number) // суммирование через один параметр
        {
            this.x += number;
            this.y += number;
            this.z += number;
        }

        public int Multiplication // умножение на коэффициент
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
                try
                {
                    Console.Write("Введите координату x: ");
                    int x = int.Parse(Console.ReadLine());
                    Console.Write("Введите координату y: ");
                    int y = int.Parse(Console.ReadLine());
                    Console.Write("Введите координату z: ");
                    int z = int.Parse(Console.ReadLine());
                    if (x % 5 == 0 || y % 5 == 0 || z % 5 == 0) point = new Point3D(x, y, z);
                    else throw new Exception("Ни одно из значений не кратно пяти! Создаю объект с координатами (5;5;5)");
                }
                catch (Exception err)
                {
                    Console.WriteLine("Ошибка: " + err.Message);
                    point = new Point3D(5, 5, 5);
                }
                
            }
            return point;
        }
        static void Main(string[] args)
        {
            Point3D point1 = NewPoint();
            Point3D point2 = new Point3D(5, 5, 5);

            int command; //Число команды
            // char direction;  Направление для сдвига точки
            // int value;  На сколько сдвигать точку
            string str;
            while (true)
            {
                Console.Write(
                    "Введите:" +
                    "\n1 - сменить координаты x, y, z " +
                    "\n2 - для вывода координат точки" +
                    "\n3 - для выхода" +
                    "\n4 - для получения радиус-вектора" +
                    "\n5 - для суммирования полей 1ой точки через второй объект" +
                    "\n6 - для суммирования полей 1ой точки через числа" +
                    "\n7 - для увеличения полей 1ой точки на коэффициент" +
                    "\n8 - для увеличения полей на определенное число:" +
                    "\n9 - для очистки консоли: "  
                );
                str = Console.ReadLine();
                while (!Int32.TryParse(str, out command))
                {
                    Console.Write("Вы ввели не число, пробуйте еще раз: ");
                    str = Console.ReadLine();
                }
                //command = int.Parse(Console.ReadLine());
                if (command == 1)
                {
                    Console.Write("Новая координата по x: ");
                    point1.X = int.Parse(Console.ReadLine());
                    Console.Write("Новая координата по y: ");
                    point1.Y = int.Parse(Console.ReadLine());
                    Console.Write("Новая координата по z: ");
                    point1.Z = int.Parse(Console.ReadLine());
                }
                else if (command == 2) point1.MyPrint();
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
                    Console.Write("Введите число: ");
                    point1.Sum(int.Parse(Console.ReadLine()));
                    point1.MyPrint();
                }
                else if (command == 9) Console.Clear();
                
            }
        }
    }
}
