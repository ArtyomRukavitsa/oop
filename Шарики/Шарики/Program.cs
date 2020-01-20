using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Шарики
{
    class Program
    {
        // Генерация икса для шарика
        static int randomX()
        {
            var rand = new Random();
            return rand.Next(9);
        }

        // Генерация игрека для шарика
        static int randomY()
        {
            var rand = new Random();
            return rand.Next(9);
        }

        // Отвечает за генерацию значения шарика в массиве + генерация цвета
        static int randomColor()
        {
            var rand = new Random();
            int random_numb = rand.Next(6);
            int index = random_numb + 1;
            return index;
        }

        // Ввод количества шариков на поле (с ограничением в 81 шарик)
        static int enterCountOfBalls()
        {
            int n;
            do
            {
                Console.Write("Введите количество шариков на поле: ");
                n = int.Parse(Console.ReadLine());
            }
            while (n > 81);
            return n;
        }

        // Pretty print :)
        static void MyPrint(int[,] array)
        {
            int elem;
            ConsoleColor[] colors = new ConsoleColor[]
            {ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green,
                ConsoleColor.Yellow, ConsoleColor.DarkMagenta, ConsoleColor.DarkBlue,
                ConsoleColor.Magenta};
            bool black = true;
            string c = char.ConvertFromUtf32(9679);
            Console.Write("  ");
            for (int i = 0; i < array.GetLength(0); i++)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("{0} ", i);
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (black)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        black = false;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        black = true;
                    }
                    elem = array[i, j];
                    if (elem != 0)
                    {
                        Console.ForegroundColor = colors[elem - 1];
                        Console.Write("{0} ", c);
                    }
                    else Console.Write("  ");
                }
                Console.WriteLine();
            }
        }

        // Можно ли сходить в клетку? (без проверки маршрута)
        static bool Move(int x1, int y1, int x2, int y2, int[,] array)
        {
            if (x1 >= 9 || x2 >= 9 || y1 >= 9 || y2 >= 9) return false;
            if (array[y1, x1] != 0 && array[y2, x2] == 0)
            {
                array[y2, x2] = array[y1, x1];
                array[y1, x1] = 0;
                return true;
            }

            return false;
        }

        // Генерирование рандомных значений в массив (с проверкой повтора)
        static void randomBalls(int n, int[, ] array)
        {
            int x, y;
            for (int i = 0; i < n; i++)
            {
                do
                {
                    x = randomX();
                    y = randomY();
                }
                while (array[x, y] != 0);
                array[x, y] = randomColor();
            }
        }

        // Игровой процесс
        static void Game(int[,] array)
        {
            string command;
            string[] move_command;
            int x1, y1, x2, y2;
            int total = 0;
            bool flag;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                command = Console.ReadLine();
                if (command == "exit") break;
                move_command = command.Split(' ');
                x1 = int.Parse(move_command[1]);
                y1 = int.Parse(move_command[2]);
                x2 = int.Parse(move_command[3]);
                y2 = int.Parse(move_command[4]);
                Console.Clear();
                if (Move(x1, y1, x2, y2, array))
                {
                    Console.WriteLine("Успешно!");
                    flag = true;
                }
                else {
                    Console.WriteLine("Ход невозможен!");
                    flag = false;
                }
                
                MyPrint(array);
                Console.ForegroundColor = ConsoleColor.Gray;
                if (flag)
                {
                    total += horizontalCheck(array);
                    total += verticalCheck(array);
                    total += diagonalCheck(array);
                    Console.WriteLine();
                    Console.WriteLine("Поле после проверки:");
                    randomBalls(3, array);
                    MyPrint(array);
                }
                //if (flag) randomBalls(3, array);
                //MyPrint(array);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Счет: {0}", total);
               
            }
        }

        // Проверки!
        static int horizontalCheck(int[,] array)
        {
            int count;
            int total_count = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                count = 1;
                for (int j = 0; j < array.GetLength(1) - 1; j++)
                {
                    if ((array[i, j] == array[i, j + 1]) && array[i, j] != 0)
                    {
                        count++;
                        if (j == 7 && count >= 3)
                        {
                            array[i, 8] = 0;
                            for (int z = j; z > j - count + 1; z--)
                            {
                                array[i, z] = 0;
                            }
                            count = 1;
                        }
                    }
                    else if (count >= 3)
                    {
                        total_count += count;
                        for (int z = j; z > j - count; z--)
                        {
                            array[i, z] = 0;
                        }
                        count = 1;
                    }
                    else count = 1; 
                }
            }
            return total_count;
        }

        static int verticalCheck(int[,] array)
        {
            int count;
            int total_count = 0; 
            for (int j = 0; j < array.GetLength(1); j++)
            {
                count = 1;
                for (int i = 0; i < array.GetLength(0) - 1; i++)
                {
                    if ((array[i, j] == array[i + 1, j]) && array[i, j] != 0)
                    {
                        count++;
                        if (i == 7 && count >= 3)
                        {
                            total_count += count;
                            array[8, j] = 0;
                            for (int z = i; z > i - count + 1; z--)
                            {
                                array[z, j] = 0;
                            }
                            count = 1;
                        }
                    }
                    else if (count >= 3)
                    {
                        total_count += count;
                        for (int z = i; z > i - count; z--)
                        {
                            array[z, j] = 0;
                        }
                        count = 1;
                    }
                    else count = 1;
                }
            }
            return total_count;
        }

        static int diagonalCheck(int[,] array)
        {
            int total_count = 0;

            //     *
            //   *
            // * <- идем из этого шарика
            for (int i = 2; i < 9; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (array[i, j] == array[i - 1, j + 1] && array[i, j] == array[i - 2, j + 2] && array[i, j] != 0)
                    {
                        array[i, j] = 0;
                        array[i - 1, j + 1] = 0;
                        array[i - 2, j + 2] = 0;
                        total_count += 3;
                    }
                }
            }

            // * <- идем из этого шарика
            //   *
            //     * 
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (array[i, j] == array[i + 1, j + 1] && array[i, j] == array[i + 2, j + 2] && array[i, j] != 0)
                    {
                        array[i, j] = 0;
                        array[i + 1, j + 1] = 0;
                        array[i + 2, j + 2] = 0;
                        total_count += 3;
                    }
                }
            }

            // *
            //   *
            //     * <- идем из этого шарика
            for (int i = 2; i < 9; i++)
            {
                for (int j = 2; j < 9; j++)
                {
                    if (array[i, j] == array[i - 1, j - 1] && array[i, j] == array[i - 2, j - 2] && array[i, j] != 0)
                    {
                        array[i, j] = 0;
                        array[i - 1, j - 1] = 0;
                        array[i - 2, j - 2] = 0;
                        total_count += 3;
                    }
                }
            }

            //     * <- идем из этого шарика
            //   *
            // * 
            for (int i = 0; i < 7; i++)
            {
                for (int j = 2; j < 9; j++)
                {
                    if (array[i, j] == array[i + 1, j - 1] && array[i, j] == array[i + 2, j - 2] && array[i, j] != 0)
                    {
                        array[i, j] = 0;
                        array[i + 1, j - 1] = 0;
                        array[i + 2, j - 2] = 0;
                        total_count += 3;
                    }
                }
            }
            return total_count;
        }

        static void Main(string[] args)
        { 
            int n;
            int[,] field = new int[9, 9];
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("Привет! Команды:\nexit - покинуть игру\n" +
                "move x1 y1 x2 y2 - переместить шарик с координаты (x1;y1) на (x2;y2)\n" +
                "Примечание: иксы - столбцы, игреки - строчки");
            n = enterCountOfBalls();
            randomBalls(n, field);
            MyPrint(field);
            Game(field);
        }
    }
}
