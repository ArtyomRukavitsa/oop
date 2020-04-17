using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Вариант_2
{
    class Transport
    {
        protected string title; // Название транспорта
        protected int fuel; // Расход топлива (литров на 100 км пути)
        protected int capacity; // Емкость топливного бака (в литрах):
        public Transport()
        {
            title = "Audi";
            fuel = 10;
            capacity = 300;
        }

        public Transport(string title, int fuel, int capacity)
        {
            this.title = title;
            this.fuel = fuel;
            this.capacity = capacity;
        }

        public string Title // Название транспорта
        {
            get { return title; }
        }

        public double CountOfFuel(int length) // Количество топлива, необходимое для преодоления заданного расстояния
        {
            return length * fuel / 100;
        }

        public double MaxLength // Максимальное расстояние, преодолеваемое транспортным средством на одной полной заправке
        {
            get { return capacity / fuel; }
        }
    }

    class Car : Transport
    {
        int passengers;  // Количество пассажиров в машине на данный момент
        static int max_passengers = 4; // Максимальное количество пассажиров в машине
        public enum CarType { Cедан = 0, Купе, Хэчбек, Универсал, Кабриолет } // тип кузова
        CarType type;
        public Car() : base()
        {
            type = (CarType)1;
            passengers = 4;
        }

        public Car(string title, int fuel, int capacity, int type, int passengers) : base(title, fuel, capacity)
        {
            this.type = (CarType)type;
            this.passengers = passengers;
        }

        public int PercentOfPassengers // Процесс загрузки автомобиля
        {
            get { return passengers * 100 / max_passengers ; }
        }

        public CarType Type // Тип кузова
        {
            get { return type; }
        }

        public int Passengers // Свойство для поля Пассажиры
        {
            get { return passengers;  }
            set
            {
                try
                {
                    if (value > 4 || value < 1)
                    {
                        passengers = 4;
                        throw new Exception("Неправильное количество пассажиров. Ставлю значаение, равное 4");
                    }
                    else
                    {
                        passengers = value;
                    }
                }
                catch (Exception err) { Console.WriteLine("Ошибка:" + err.Message); }
            }
        }
        public new double CountOfFuel(int length)
        {
            return (1 + passengers * 0.1) * base.CountOfFuel(length); 
            // каждый пассажир увеличивает расход топлива на 10%
        }
    }

    class Track : Transport
    {
        int carrying; // Грузоподъемность
        double mass; // Масса груза
        public Track() : base()
        {
            carrying = 1000;
            mass = 200;
        }

        public Track(string title, int fuel, int capacity, int carrying, double mass) : base(title, fuel, capacity)
        {
            this.carrying = carrying;
            this.mass = mass;
        }

        public double Mass // Cвойство для поля Масса груза
        {
            get { return mass; }
            set
            {
                try
                {
                    if (value < 0 || value > carrying)
                    {
                        mass = carrying / 2;
                        throw new Exception("Недопустимое значение массы груза. Ставлю значение, равное половине грузоподъемности");
                    }
                    else
                    {
                        mass = value;
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine("Ошибка: " + err.Message);
                }
            }
        }

        public double PercentOfMass // Процент загрузки ТС в зависиомсти от массы груза
        {
            get { return mass * 100 / carrying; }
        }

        public new double CountOfFuel(int length)
        {
            return (1 + mass * 0.01) * base.CountOfFuel(length);
            // каждый килограмм увеличивает расход топлива на 1%
        }
    }

    class Bus : Transport
    {
        int passengers; // Количество перевозимых пассажиров
        int price; // Стоимость проезда за одного пассажира
        static int max_passengers = 120;

        public Bus() : base()
        {
            passengers = 50;
            price = 25;
        }

        public Bus(string title, int fuel, int capacity, int passengers, int price) : base(title, fuel, capacity)
        {
            this.passengers = passengers;
            this.price = price;
        }

        public double Percent // Процент загрузки автобуса
        {
            get { return passengers * 100 / max_passengers; }
        }

        public new double CountOfFuel(int length) // Количество топлива (учитывается час пик)
        {
            if (isTimePick) return 1.5 * base.CountOfFuel(length);
            return 2 * base.CountOfFuel(length);
        }
        public int Money // Выручка
        {
            get { return passengers * price; }
        }
        public bool isTimePick // Определение, час пик или нет
        {
            get
            {
                if ((double)passengers / max_passengers > 0.7) return true;
                return false;
            }
        }
        public int Passengers
        {
            get { return passengers; }
        }
    }
    class Program
    {
        static void AboutCars()
        {
            Car[] cars = new Car[3];
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                cars[i] = new Car($"Audi-{i + 1}", 10 + (5 * i), 100 + (i * 30), rnd.Next(0, 5), 2 + i);
            }
            int passengers, length;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Информация о легковых автомобилях!");
            Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Вывожу информацию о машине №{0}", i + 1);
                Console.WriteLine("Название машины: {0}", cars[i].Title);
                Console.WriteLine("Тип машины: {0}", cars[i].Type);
                Console.Write("Введите количество пассажиров в легковом автомобиле: ");
                passengers = int.Parse(Console.ReadLine());
                cars[i].Passengers = passengers;
                Console.WriteLine("Процент загрузки авто в зависимости от числа пассажиров: {0}%", cars[i].PercentOfPassengers);
                Console.Write("Введите расстояние, для которого хотите узнать количество топливо: ");
                length = int.Parse(Console.ReadLine());
                Console.WriteLine("Количество топлива, которое нужно для преодоления {0} км, составляет {1} литров", length, cars[i].CountOfFuel(length));
                Console.WriteLine("Максимальная длина, которую может преодолеть {0} на полной заправке, составляет {1} км", cars[i].Title, cars[i].MaxLength);
                Console.WriteLine("---------------------------------------\n");
            }
        }

        static void AboutTracks()
        {
            Track[] tracks = new Track[3];
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                tracks[i] = new Track($"КамАЗ-{i + 1}", 10 + 5 * i, 100 + i * 30, 500 + (i * 200), 300 + (i * 100));
            }
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Информация о грузовых автомобилях!");
            Console.BackgroundColor = ConsoleColor.Black;
            int mass, length;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Вывожу информацию о грузовике №{0}", i + 1);
                Console.WriteLine("Название машины: {0}", tracks[i].Title);
                Console.Write("Введите массу груза для грузовика: ");
                mass = int.Parse(Console.ReadLine());
                tracks[i].Mass = mass;
                Console.WriteLine("Процент загрузки авто в зависимости от массы груза: {0:F1}%", tracks[i].PercentOfMass);
                Console.Write("Введите расстояние, для которого хотите узнать количество топливо: ");
                length = int.Parse(Console.ReadLine());
                Console.WriteLine("Количество топлива, которое нужно для преодоления {0} км, составляет {1} литров", length, tracks[i].CountOfFuel(length));
                Console.WriteLine("Максимальная длина, которую может преодолеть {0} на полной заправке, составляет {1} км", tracks[i].Title, tracks[i].MaxLength);
                Console.WriteLine("---------------------------------------\n");
            }
        }

        static void AboutBuses()
        {
            Bus[] buses = new Bus[3];
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                buses[i] = new Bus($"ЛиАЗ-{i + 1}", 10 + 5 * i, 100 + i * 30, 30 * (1 + i) , 20 + (i * 5));
            }
            int mass, length;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Информация об автобусах!");
            Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Вывожу информацию о грузовике №{0}", i + 1);
                Console.WriteLine("Название автобуса: {0}", buses[i].Title);
                Console.WriteLine("Процент загрузки автобуса: {0:F1}%, количество пассажиров: {1}", buses[i].Percent, buses[i].Passengers);
                if (buses[i].isTimePick) Console.WriteLine("Сейчас час пик!");
                else Console.WriteLine("Сейчас обычное время!");
                Console.WriteLine("Выручка составляет {0} рублей", buses[i].Money);
                Console.Write("Введите расстояние, для которого хотите узнать количество топливо: ");
                length = int.Parse(Console.ReadLine());
                Console.WriteLine("Количество топлива, которое нужно для преодоления {0} км, составляет {1} литров", length, buses[i].CountOfFuel(length));
                Console.WriteLine("Максимальная длина, которую может преодолеть {0} на полной заправке, составляет {1} км", buses[i].Title, buses[i].MaxLength);
                Console.WriteLine("---------------------------------------\n");
            }
        }

        static void Main(string[] args)
        {
            AboutCars();
            AboutTracks();
            AboutBuses();
        }
    }
}
