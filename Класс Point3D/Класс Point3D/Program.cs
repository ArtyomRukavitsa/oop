using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Класс_Point3D
{
    class Point3D
    {
        public int x;
        public int y;
        public int z;

        public Point3D()
        {
            x = 0;
            y = 0;
            z = 0;
        }
        public Point3D(int x_coord, int y_coord, int z_coord)
        {
            x = x_coord;
            y = y_coord;
            z = z_coord;
        }
        public void Move(char direction, int value)
        {
            if (direction == 'x') x += value;
            else if (direction == 'y') y += value;
            else z += value;
        }
        public void MyPrint()
        {
            Console.WriteLine("Координаты точки: ({0}, {1}, {2})", x, y, z);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Point3D point = new Point3D();
            Console.Write("Если хотите использовать конструктор по умолчанию, введите 1, иначе 2: ");
            int command = int.Parse(Console.ReadLine());
            if (command == 1) point = new Point3D();
            else if (command == 2)
            {
                Console.Write("Введите координату x: ");
                int x = int.Parse(Console.ReadLine());
                Console.Write("Введите координату y: ");
                int y = int.Parse(Console.ReadLine());
                Console.Write("Введите координату z: ");
                int z = int.Parse(Console.ReadLine());
                point = new Point3D(x, y, z);
            }
            else { Console.WriteLine("Я Вас не понимаю... Завершаю работу"); Environment.Exit(0);}

            
            char direction; // Направление для сдвига точки
            int value; // На сколько сдвигать точку
            while (true)
            {
                Console.Write("Введите 1 для сдвига точки, введите 2 для вывода координат точки, введите 3 для выхода: ");
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
