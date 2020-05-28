using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Вариант_4
{
    public class WallException : Exception
    {
        public WallException(string message) : base(message) { }
    }
    public class EnemyException : Exception
    {
        public EnemyException(string message) : base(message) { }
    }
    public class NotEmptyException : Exception
    {
        public NotEmptyException(string message) : base(message) { }
    }
    class Field
    {
        SuperClass[,] matrix;
        static Random rnd = new Random();

        public Field()
        {
            matrix = new SuperClass[15, 15];
            GenerateField();
        }

        public Field(int m)
        {
            matrix = new SuperClass[m, m];
            GenerateField();
        }

        public void GenerateField()
        {
            int x, y;
            // Заполнение пустыми объектами
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = new Empty(i, j);
                }
            }
            // Cтавлю стены
            for (int i = 0; i < 15; i++)
            {
                do
                {
                    x = rnd.Next(0, matrix.GetLength(0));
                    y = rnd.Next(0, matrix.GetLength(0));
                }
                while (matrix[x, y].ClassName != "Empty");
                matrix[x, y] = new Wall(x, y);
            }

            // Ставлю еду
            for (int i = 0; i < 10; i++)
            {
                do
                {
                    x = rnd.Next(0, matrix.GetLength(0));
                    y = rnd.Next(0, matrix.GetLength(0));
                }
                while (matrix[x, y].ClassName != "Empty");
                matrix[x, y] = new Food(x, y);
            }
            // Ставлю одну вишенку
            do
            {
                x = rnd.Next(0, matrix.GetLength(0));
                y = rnd.Next(0, matrix.GetLength(0));
            }
            while (matrix[x, y].ClassName != "Empty");
            matrix[x, y] = new Cherry(x, y);

            // Место для Pacman
            do
            {
                x = rnd.Next(0, matrix.GetLength(0));
                y = rnd.Next(0, matrix.GetLength(0));
            }
            while (matrix[x, y].ClassName == "Empty");
            matrix[x, y] = new Pacman(x, y, 1);

            // Место для Ghost
            do
            {
                x = rnd.Next(0, matrix.GetLength(0));
                y = rnd.Next(0, matrix.GetLength(0));
            }
            while (matrix[x, y].ClassName == "Empty");
            matrix[x, y] = new Ghost(x, y, 1);

            // Место для SmartGhost
            do
            {
                x = rnd.Next(0, matrix.GetLength(0));
                y = rnd.Next(0, matrix.GetLength(0));
            }
            while (matrix[x, y].ClassName == "Empty");
            matrix[x, y] = new SmartGhost(x, y, 1);
        }
        public void MyPrint()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (matrix[i, j].Draw() == "P") Console.ForegroundColor = ConsoleColor.DarkGreen;
                    else if (matrix[i, j].Draw() == "G") Console.ForegroundColor = ConsoleColor.DarkRed;
                    else if (matrix[i, j].Draw() == "S") Console.ForegroundColor = ConsoleColor.DarkYellow;
                    else if (matrix[i, j].Draw() == "1") Console.ForegroundColor = ConsoleColor.DarkCyan;
                    else if (matrix[i, j].Draw() == "2") Console.ForegroundColor = ConsoleColor.Magenta;
                    else if (matrix[i, j].Draw() == "3") Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("{0} ", matrix[i, j].Draw());
                }
                Console.WriteLine();
            }
        }
        public SuperClass[,] Matrix
        {
            get { return matrix; }
        }

        public Pacman FindPacman
        {
            get
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j].ClassName == "Pacman")
                        {
                            return new Pacman(i, j, 1);
                        }
                    }
                }
                return new Pacman();
            }
        }

        public Ghost FindGhost
        {
            get
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j].ClassName == "Ghost")
                        {
                            return new Ghost(i, j, 1);
                        }
                    }
                }
                return new Ghost();
            }
        }

        public SmartGhost FindSmartGhost
        {
            get
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j].ClassName == "SmartGhost")
                        {
                            return new SmartGhost(i, j, 1);
                        }
                    }
                }
                return new SmartGhost();
            }
        }
    }
    abstract class SuperClass
    {
        protected int x;
        protected int y;
        protected static Random rnd = new Random();
        public SuperClass() { }
        public SuperClass(int x, int y)
        {
            this.x = x; this.y = y;
        }
        public abstract string Draw();
        public abstract string ClassName { get; }
    }
    abstract class Creature : SuperClass
    {
        protected int v;
        public Creature() : base() { }
        public Creature(int x, int y, int v) : base(x, y)
        {
            this.v = v;
        }
        public abstract void Move(string direction, Field field);

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }
    class Pacman : Creature
    {
        int score;
        public Pacman() : base() { score = 0; }
        public Pacman(int x, int y, int v) : base(x, y, v) { score = 0; }

        public override string Draw()
        {
            return "P";
        }

        public override void Move(string direction, Field field)
        {
            int x = -1, y = -1;
            // Поиск Pacman на карте
            for (int i = 0; i < field.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < field.Matrix.GetLength(1); j++)
                {
                    if (field.Matrix[i, j].ClassName == "Pacman")
                    {
                        x = i; y = j;
                        break;
                    }
                }
            }
            int new_x = x, new_y = y; // новые координаты для пакмана
            if (direction == "w") new_x -= v;
            else if (direction == "a") new_y -= v;
            else if (direction == "s") new_x += v;
            else if (direction == "d") new_y += v;
            try
            {
                if (new_x - x > 1)
                {
                    if (!(field.Matrix[new_x - 1, new_y].ClassName == "Food" ||
                        field.Matrix[new_x - 1, new_y].ClassName == "Cherry" ||
                        field.Matrix[new_x - 1, new_y].ClassName == "Empty"))
                        throw new NotEmptyException("Вы пытаетесь перейти через занятую клетку!");
                }
                else if (x - new_x > 1)
                {
                    if (!(field.Matrix[new_x + 1, new_y].ClassName == "Food" ||
                        field.Matrix[new_x + 1, new_y].ClassName == "Cherry" ||
                        field.Matrix[new_x + 1, new_y].ClassName == "Empty"))
                        throw new NotEmptyException("Вы пытаетесь перейти через занятую клетку!");
                }
                else if (new_y - y > 1)
                {
                    if (!(field.Matrix[new_x, new_y - 1].ClassName == "Food" ||
                        field.Matrix[new_x, new_y - 1].ClassName == "Cherry" ||
                        field.Matrix[new_x, new_y - 1].ClassName == "Empty"))
                        throw new NotEmptyException("Вы пытаетесь перейти через занятую клетку!");
                }
                else if (y - new_y > 1)
                {
                    if (!(field.Matrix[new_x, new_y + 1].ClassName == "Food" ||
                         field.Matrix[new_x, new_y + 1].ClassName == "Cherry" ||
                         field.Matrix[new_x, new_y + 1].ClassName == "Empty"))
                        throw new NotEmptyException("Вы пытаетесь перейти через занятую клетку!");
                }
                if (field.Matrix[new_x, new_y].ClassName == "Ghost" ||
                    field.Matrix[new_x, new_y].ClassName == "SmartGhost") throw new EnemyException("Вы врезались во врага!");
                else if (field.Matrix[new_x, new_y].ClassName == "Wall") throw new WallException("Вы врезались в стену!");
                else if (field.Matrix[new_x, new_y].ClassName == "Food") score += 100;
                else if (field.Matrix[new_x, new_y].ClassName == "Cherry")
                {
                    Console.WriteLine("Вы выиграли!");
                    Environment.Exit(0);
                }
                field.Matrix[new_x, new_y] = new Pacman(new_x, new_y, 1);
                field.Matrix[x, y] = new Empty(x, y);
            }
            catch (IndexOutOfRangeException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка! Так невозможно сходить!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (EnemyException Error)
            {
                field.MyPrint();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Вы проиграли! " + Error.Message);
                Environment.Exit(0);
            }
            catch (WallException Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка! " + Error.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (NotEmptyException Error)
            {
                Console.WriteLine("Ошибка: " + Error.Message);
            }
        }

        public override string ClassName { get { return "Pacman"; } }

        public int Score { get { return score; } }

        public int V
        {
            get { return v; }
            set
            {
                try
                {
                    if (value > 2)
                    {
                        v = 1;
                        throw new Exception("WOW! Слишком большая скорость! Ставлю 1");
                    }
                    else if (value <= 0)
                    {
                        v = 1;
                        throw new Exception("Неее. Скорость может быть равна только 1 или 2. Ставлю 1");
                    }
                    v = value;
                }
                catch (Exception Error)
                {
                    Console.WriteLine("\nОшибка: " + Error.Message + "\n");
                }
            }
        }

    }
    class Ghost : Creature
    {
        public Ghost() : base() { }
        public Ghost(int x, int y, int v) : base(x, y, v) { }

        public override string Draw()
        {
            return "G";
        }

        public override void Move(string direction, Field field)
        {
            int new_x = x, new_y = y; // новые координаты для привидения
            string[] mas = new string[] { "w", "a", "s", "d" };
            while (true)
            {
                if (direction == "w") new_x -= v;
                else if (direction == "a") new_y -= v;
                else if (direction == "s") new_x += v;
                else if (direction == "d") new_y += v;
                try
                {
                    if (field.Matrix[new_x, new_y].ClassName == "Empty" || field.Matrix[new_x, new_y].ClassName == "Food" || field.Matrix[new_x, new_y].ClassName == "Cherry")
                    {
                        field.Matrix[x, y] = new Empty(x, y);
                        this.X = new_x;
                        this.Y = new_y;
                        field.Matrix[new_x, new_y] = this;
                        break;
                    }
                    else
                    {
                        direction = mas[rnd.Next(0, 4)];
                        new_x = x; new_y = y;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    direction = mas[rnd.Next(0, 4)];
                }
            }
        }
        public override string ClassName { get { return "Ghost"; } }

    }
    class SmartGhost : Creature
    {
        public SmartGhost() : base() { }
        public SmartGhost(int x, int y, int v) : base(x, y, v) { }

        public override string Draw()
        {
            return "S";
        }

        public override void Move(string direction, Field field)
        {
            int new_x = x, new_y = y; // новые координаты для привидения
            string[] mas = new string[] { "w", "a", "s", "d" };
            Pacman pacman = field.FindPacman;
            if (pacman.X > x)
            {
                new_x++;
                field.Matrix[x, y] = new Empty(x, y);
                this.X = new_x;
                this.Y = new_y;
                field.Matrix[new_x, new_y] = this;
            }
            else if (pacman.X == x)
            {
                if (pacman.Y > y) new_y++;
                else new_y--;
                field.Matrix[x, y] = new Empty(x, y);
                this.X = new_x;
                this.Y = new_y;
                field.Matrix[new_x, new_y] = this;
            }
            else
            {
                new_x--;
                field.Matrix[x, y] = new Empty(x, y);
                this.X = new_x;
                this.Y = new_y;
                field.Matrix[new_x, new_y] = this;
            }
        }
        public override string ClassName { get { return "SmartGhost"; } }

    }
    class Empty : SuperClass
    {
        public Empty() : base() { }
        public Empty(int x, int y) : base(x, y) { }
        public override string Draw()
        {
            return "0";
        }
        public override string ClassName { get { return "Empty"; } }

    }
    class Wall : SuperClass
    {
        public Wall() : base() { }
        public Wall(int x, int y) : base(x, y) { }
        public override string Draw()
        {
            return "1";
        }
        public override string ClassName { get { return "Wall"; } }

    }
    class Food : SuperClass
    {
        public Food() : base() { }
        public Food(int x, int y) : base(x, y) { }
        public override string Draw()
        {
            return "2";
        }
        public override string ClassName { get { return "Food"; } }

    }
    class Cherry : SuperClass
    {
        public Cherry() : base() { }
        public Cherry(int x, int y) : base(x, y) { }
        public override string Draw()
        {
            return "3";
        }
        public override string ClassName { get { return "Cherry"; } }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            string[] directions = new string[] { "w", "a", "s", "d" };
            string direction_for_ghost, direction_for_smartghost;
            Console.Write("Введите размер поля М х М: ");
            int m = int.Parse(Console.ReadLine());
            Field field = new Field(m);


            Pacman pacman = field.FindPacman;
            Ghost ghost = field.FindGhost;
            SmartGhost smartghost = field.FindSmartGhost;
            string command = "ok";
            while (true)
            {
                field.MyPrint();
                while (command != "w" && command != "a" && command != "s" && command != "d"
                    && command != "check" && command != "clear" && command != "exit" && !command.Contains("set"))
                {
                    Console.Write(
                        "Команды для Пакмана: W — вверх, A — вправо, S — вниз, D — влево" +
                        "\nЧтобы окончить игру, введи exit" +
                        "\nЧтобы узнать счет, введи check" +
                        "\nЧтобы очистить консоль, введи clear" +
                        "\nЧтобы изменить скорость пакмана, введи set <число> (доступно только 1 или 2): "
                        );
                    command = Console.ReadLine().ToLower();
                }
                if (command == "exit") break;
                else if (command == "check")
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ваш счет: {0}", pacman.Score);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (command == "clear") Console.Clear();
                else if (command.Contains("set"))
                {
                    int get_speed = int.Parse(command.Split(' ')[1]);
                    pacman.V = get_speed;
                }
                else if (command == "w" || command != "a" || command != "s" || command != "d")
                {
                    direction_for_ghost = directions[rnd.Next(0, 4)];
                    ghost.Move(direction_for_ghost, field);
                    direction_for_smartghost = directions[rnd.Next(0, 4)];
                    smartghost.Move(direction_for_ghost, field);
                    pacman.Move(command, field);
                    if (pacman.Score == 500)
                    {
                        Console.WriteLine("Вы выиграли! ");
                        break;
                    }
                }
                else continue;
                command = "";
            }

        }
    }
}