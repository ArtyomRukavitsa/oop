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

        public Point3D()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public Point3D(int x_coord, int y_coord, int z_coord)
        {
            this.x = x_coord;
            this.y = y_coord;
            this.z = z_coord;
        }

        public void Move(char direction, int value)
        {
            if (direction == 'x') this.x += value;
            else if (direction == 'y') this.y += value;
            else this.z += value;
        }

        public void MyPrint()
        {
            Console.WriteLine("Координаты точки: ({0}, {1}, {2})", this.x, this.y, this.z);
        }

    }
    class Program
    {
        static void Main(string[] args)
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

            char direction; // Направление для сдвига точки
            int value; // На сколько сдвигать точку
            while (true)
            {
                Console.Write("Введите 1 для сдвига точки, введите 2 для вывода координат точки, иначе введите любую цифру для выхода: ");
                command = int.Parse(Console.ReadLine());
                if (command == 1)
                {
                    Console.Write("По какой оси сдвигать точку? ");
                    direction = char.Parse(Console.ReadLine());
                    Console.Write("На сколько сдвигать точку? ");
                    value = int.Parse(Console.ReadLine());
                    point.Move(direction, value);
                }
                else if (command == 2)
                {
                    point.MyPrint();
                }
                else break;
            }
        }
    }
}