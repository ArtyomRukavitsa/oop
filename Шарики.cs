using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Фоновая_33
{
    class Program
    {
        static int[][] InputArray(int n)
        {
            int[][] massiv;
            massiv = new int[n][];
            string[] data;
            for (int i = 0; i < massiv.Length; i++)
            {
                Console.Write("Введите элементы {0} строки: ", i);
                data = Console.ReadLine().Split(' ');
                massiv[i] = new int[data.Length];
                for (int j = 0; j < massiv[i].Length; j++)
                {
                    massiv[i][j] = int.Parse(data[j]);
                }
            }
            return massiv;
        }

        static void PrintArray(int[][] massiv)
        {
            foreach (int[] x in massiv)
            {
                foreach (int elem in x) Console.Write("{0} ", elem);
                Console.WriteLine();
            }
        }

        static int MaxIndexOfLength(int[][] massiv)
        {
            int max = 0;
            foreach (int[] array in massiv)
            {
                if (array.Length > max) max = array.Length;
            }
            return max;
        }

        static int[] Statistics(int[][] massiv)
        {
            int[] mas = new int[MaxIndexOfLength(massiv)];
            int el, ind = 0;
            for (int j = 0; j < MaxIndexOfLength(massiv); j++)
            {
                el = 0;
                for (int i = 0; i < massiv.Length; i++)
                {
                    if (massiv[i].Length >= j + 1)
                    {
                        if (massiv[i][j] % 2 == 0 && el == 0) el = massiv[i][j];
                    }
                }
                mas[ind] = el;
                ind++;
            }
            return mas;
        }

        static void MyShift(int[][] massiv, int k)
        {
            int temp = massiv[k][massiv[k].Length - 1];
            for (int i = massiv[k].Length - 1; i > 0; i--)
            {
                massiv[k][i] = massiv[k][i - 1];
            }
            massiv[k][0] = temp;
        }

        static void MyDelete(ref int[][] massiv)
        {
            int count, count_of_zeros = 0;
            for (int i = 0; i < massiv.Length; i++)
            {
                count = 0;
                foreach (int x in massiv[i]) if (x == 0) count++;
                if (count > massiv[i].Length / 2) count_of_zeros++;
            }
            int[][] arr = new int[massiv.Length - count_of_zeros][];
            int b = 0;
            for (int i = 0; i < massiv.Length; i++)
            {
                count = 0;
                foreach (int x in massiv[i]) if (x == 0) count++;
                if (count <= massiv[i].Length / 2)
                {
                    arr[b] = new int[massiv[i].Length];
                    arr[b] = massiv[i];
                    b++;
                }
            }
            massiv = arr;
        }
        static void Main(string[] args)
        {
            int[][] array;
            Console.Write("Введите количество строк: ");
            int n = int.Parse(Console.ReadLine());
            array = new int[n][];
            array = InputArray(n);
            Console.WriteLine("Вывод массива: ");
            PrintArray(array);
            int[] mas = Statistics(array);
            Console.Write("Четные элементы столбцов: ");
            foreach (int x in mas) Console.Write("{0} ", x);
            MyDelete(ref array);
            Console.WriteLine("\nУплотнили массив: ");
            PrintArray(array);
            Console.Write("Какую строку сдвигать? (нумерация с нуля) ");
            int k = int.Parse(Console.ReadLine());
            Console.Write("\nСколько раз сдвинуть строку? ");
            int count = int.Parse(Console.ReadLine());
            for (int i = 0; i < count % array[k].Length; i++) MyShift(array, k);
            PrintArray(array);
        }
    }
}
