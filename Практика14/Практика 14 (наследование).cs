using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Практика14
{
    class DemoPoint
    {
        protected int x1;
        protected int y1;
        public DemoPoint() { }
        public DemoPoint(int x1, int y1)
        {
            this.x1 = x1;
            this.y1 = y1;
        }
        public void Show()
        {
            Console.Write("({0};{1})", x1, y1);
        }
    }

    class DemoLine : DemoPoint
    {
        protected int x2;
        protected int y2;
        public DemoLine() { }
        public DemoLine(int x1, int y1, int x2, int y2) : base(x1, y1)
        {
            this.x2 = x2;
            this.y2 = y2;
        }
        new public void Show()
        {
            base.Show();
            Console.WriteLine("-({0};{1})", x2, y2);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите 1, если хотите создать точку по умолчанию, иначе любое число: ");
            int index = int.Parse(Console.ReadLine());
            int x1, y1, x2, y2;
            DemoPoint point;
            DemoLine line;
            if (index == 1)
            {
                Console.Write("Введите координату x1: ");
                x1 = int.Parse(Console.ReadLine());
                Console.Write("Введите координату y1: ");
                y1 = int.Parse(Console.ReadLine());
                point = new DemoPoint(x1, y1);
            }
            else { point = new DemoPoint(); x1 = 0; y1 = 0; }
            
            Console.WriteLine("МЕТОД БАЗОВОГО КЛАССА:");
            point.Show();
            Console.Write("\nВведите 1, если хотите создать линию c координатами первой точки и 0;0, иначе любое число: ");
            index = int.Parse(Console.ReadLine());
            if (index == 1)
            {
                Console.Write("Введите координату x2: ");
                x2 = int.Parse(Console.ReadLine());
                Console.Write("Введите координату y2: ");
                y2 = int.Parse(Console.ReadLine());
                line = new DemoLine(x1, y1, x2, y2);
            }
            else line = new DemoLine(x1, y1, 0, 0);
            Console.WriteLine("МЕТОД КЛАССА-НАСЛЕДНИКА:");
            line.Show();
        }
    }
}
