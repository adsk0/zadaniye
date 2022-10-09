using System;
using System.Drawing;

namespace safe_meth
{
    class MainClass
    {
        static int size;                                     // глобальная переменная, размер будущей матрицы
        public static int users_input(string vvod)           // считывание ввода от пользователя
        {
            Console.Write("Введите " + vvod + ": ");
            int input;
            while ((!int.TryParse(Console.ReadLine(), out input)) || input > size)
            {
                Console.Write("Ошибка. Введите " + vvod + ": ");
            }
            return input;
        }

        public static int[,] arr_create(int size)            // создание изначальной матрицы (цифры)
        {
            int[,] mass = new int[size, size];
            return mass;
        }

        public static void show_arr(string[,] palki, int size)   // вывод матрицы с разметкой полей на экран
        {
            Console.WriteLine(" ");
            Console.Write("  ");
            for (int x = 1; x < size + 1; x++)
            {
                Console.Write(x + " ");
            }
            Console.WriteLine(" ");
            for (int i = 0; i < size; i++)
            {
                Console.Write((i + 1) + " ");
                for (int j = 0; j < size; j++)
                {
                    Console.Write(palki[i, j] + " ");
                }
                Console.WriteLine(" ");
            }
            Console.WriteLine(" ");
        }

        public static int[,] change_columns(int[,] mass, int size, int row)  // замена значений столбца
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = row - 1; j < row; j++)
                {
                    if (mass[i, j] == 0)
                    {
                        mass[i, j] = 1;
                    }
                    else
                    {
                        mass[i, j] = 0;
                    }
                }
            }
            return mass;
        }

        public static int[,] change_rows(int[,] mass, int size, int col, int row)     // замена значений строки
        {
            for (int i = col - 1; i < col; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (mass[i, j] == 0)
                    {
                        mass[i, j] = 1;
                    }
                    else
                    {
                        mass[i, j] = 0;
                    }
                }
            }
            if (mass[col - 1, row - 1] == 0)
            {
                mass[col - 1, row - 1] = 1;
            }
            else
            {
                mass[col - 1, row - 1] = 0;
            }
            return mass;
        }

        public static string[,] arr_palki(int[,] mass, int size)             // создание графического массива для интерфейса
        {
            string[,] palki = new string[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (mass[i, j] == 1)
                    {
                        palki[i, j] = "|";
                    }
                    else
                    {
                        palki[i, j] = "-";
                    }
                }
            }
            return palki;
        }

        public static void Main()
        {
            Console.Write("Введите размер поля: ");
            while ((!int.TryParse(Console.ReadLine(), out size)) || size < 2 || size > 9)
            {
                Console.Write("Ошибка. Введите значение от 2 до 9: ");
            }
            int[,] mass = arr_create(size);                  // создается двумерный массив (квадратная матрица)
            Random rnd = new Random();                       // функция для рандома

            for (int i = 0; i < size; i++)                   // цикл заполняет весь изначальный массив еденицами, при этом по даигонали - нули
            {
                for (int j = 0; j < size; j++)
                {
                    mass[i, j] = 1;
                    if (i == j)
                    {
                        mass[i, j] = 0;
                    }
                }
            }
            for (int i = 0; i < size; i++)                   // шейкер. запутывает матрицу вначале игры, т.к. при создании матрицы схожи
            {
                int col = rnd.Next(1, size + 1);             // рандом иммитирует ходы пользователя
                int row = rnd.Next(1, size + 1);
                change_columns(mass, size, row);
                change_rows(mass, size, col, row);
            }
            show_arr(arr_palki(mass, size), size);
            int win = 0;                                      // вспомогательная переменная чтобы проверить конец игры                          
            while (win != size * size)                        // начинается цикл while. он идет время время, пока пользователь не победит.                                                        
            {
                win = 0;               
                int col = users_input("строку");
                int row = users_input("столбец");
                change_columns(mass, size, row);
                change_rows(mass, size, col, row);
                show_arr(arr_palki(mass, size), size);

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (mass[i, j] == mass[0, 0])
                        {
                            win++;
                        }
                    }
                }
            }
            Console.WriteLine("Сейф открыт!!!");
        }
    }
}