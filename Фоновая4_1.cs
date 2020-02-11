using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Вариант8
{
    class Parallelepiped
    {
        double length;
        double height;
        double width;

        public Parallelepiped()
        {
            this.length = 5;
            this.height = 6;
            this.width = 7;
        }
        public Parallelepiped(double length, double height, double width)
        {
            this.length = length;
            this.height = height;
            this.width = width;
        }
        public double Size()
        {
            return length * width * height;
        }
        public double SideS()
        {
            return 2 * (length * height + width * height);
        }
        public void Info()
        {
            Console.WriteLine("Информация о Вашем параллелепипеде:\n длина = {0}\n высота = {1}\n ширина = {2}", length, height, width);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Parallelepiped first;
            Console.Write("Если хотите использовать конструктор по умолчанию, введите 1, иначе любую цифру: ");
            int command = int.Parse(Console.ReadLine());
            if (command == 1) first = new Parallelepiped();
            else
            {
                Console.Write("Введите длину параллелепипеда: ");
                double length = double.Parse(Console.ReadLine());
                Console.Write("Введите высоту параллелепипеда: ");
                double height = double.Parse(Console.ReadLine());
                Console.Write("Введите длину параллелепипеда: ");
                double width = double.Parse(Console.ReadLine());
                first = new Parallelepiped(length, width, height);
            }

            first.Info();


        }
    }
}
