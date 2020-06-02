using System;
using System.Collections;

namespace practical_task5
{
    public class Program
    {
        // Вывод меню
        static void PrintMenu(string[] menuItems, int choice, string info)
        {
            Console.Clear();
            Console.WriteLine(info);
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == choice) Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{i + 1}. {menuItems[i]}");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        // Выбор пункта из меню
        static int MenuChoice(string[] menuItems, string info = "")
        {
            Console.CursorVisible = false;
            int choice = 0;
            while (true)
            {
                PrintMenu(menuItems, choice, info);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (choice == 0) choice = menuItems.Length;
                        choice--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (choice == menuItems.Length - 1) choice = -1;
                        choice++;
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        return choice;
                }
            }
        }

        // Ввод целого числа
        public static int IntInput(int lBound = int.MinValue, int uBound = int.MaxValue, string info = "")
        {
            bool exit;
            int result;
            Console.Write(info);
            do
            {
                exit = int.TryParse(Console.ReadLine(), out result);
                if (!exit) Console.Write("Введено нецелое число! Повторите ввод: ");
                else if (result <= lBound || result >= uBound)
                {
                    exit = false;
                    Console.Write("Введено недопустимое значение! Повторите ввод: ");
                }
            } while (!exit);
            return result;
        }

        // Проверка ввода матрицы
        public static bool CheckInput(string[] lines, out int[,] matrix)
        {
            int n = lines.Length;
            matrix = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                string[] coll = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (coll.Length != n) return false;
                for (int j = 0; j < n; j++) if (!int.TryParse(coll[j], out matrix[i, j])) return false;
            }

            return true;
        }

        // Поиск убывающих строк
        public static ArrayList FindDecreacaningLines(int[,] matrix)
        {
            ArrayList result = new ArrayList();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                bool ok = true;
                for (int j = 1; j < matrix.GetLength(1); j++) if (matrix[i,j] >= matrix[i, j -1])
                    {
                        ok = false;
                        break;
                    }
                if (ok) result.Add(i + 1);
            }
            return result;
        }

        // Поиск возрастающих строк
        public static ArrayList FindIncreacaningLines(int[,] matrix)
        {
            ArrayList result = new ArrayList();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                bool ok = true;
                for (int j = 1; j < matrix.GetLength(1); j++) if (matrix[i, j] <= matrix[i, j - 1])
                    {
                        ok = false;
                        break;
                    }
                if (ok) result.Add(i + 1);
            }
            return result;
        }

        static void Main(string[] args)
        {
            // Пункты меню
            string[] MENU_ITEMS = { "Задать матрицу", "Выйти из программы" };

            // Индекс пункта - выход из программы
            const int EXIT_CHOICE = 1;

            // Индекс пункта меню, который выбрал пользователь
            int userChoice;

            while (true)
            {
                // Пользователь выбирает действие (выйти или задать матрицу)
                userChoice = MenuChoice(MENU_ITEMS, "Программа для нахождения строк квадратной матрицы, элементы которых образуют монотонные последовательности\nВыберите действие:");
                if (userChoice == EXIT_CHOICE) break;
                Console.Clear();

                // Ввод размерности
                int n = IntInput(lBound: 0, info: "Введите порядок квадратной матрицы (целое число больше 0): ");

                // Ввод матрицы
                int[,] matrix;
                string[] inputLines = new string[n];
                Console.WriteLine($"Введите квадратную матрицу порядка {n}");
                Console.WriteLine("Ввод каждой строки матрицы начинайте с новой строчки, разделяя элементы по столбцам пробелами");
                while (true)
                {
                    for (int i = 0; i < n; i++) inputLines[i] = Console.ReadLine();
                    if (CheckInput(inputLines, out matrix)) break;
                    else Console.WriteLine("Неверный формат ввода матрицы! Повторите ввод:");
                }
                Console.WriteLine();

                // Поиск убывающих и возрастающих строк
                ArrayList decreasingLines = FindDecreacaningLines(matrix);
                ArrayList increasingLines = FindIncreacaningLines(matrix);

                // Вывод результата
                if (decreasingLines.Count == 0 && increasingLines.Count == 0) Console.WriteLine("В матрице нет строк, элементы которых образуют монотонные последовательности.");
                else
                {
                    Console.WriteLine("Номера строки, элементы которых образуют монотонные последовательности:");
                    if (decreasingLines.Count == 0) Console.WriteLine("В матрице нет строк, элементы которых образуют убывающие последовательности.");
                    else
                    {
                        Console.Write("Убывающие:");
                        foreach (var i in decreasingLines) Console.Write(" " + i);
                        Console.WriteLine();
                    }
                    if (increasingLines.Count == 0) Console.WriteLine("В матрице нет строк, элементы которых образуют возрастающие последовательности.");
                    else
                    {
                        Console.Write("Возрастающие:");
                        foreach (var i in increasingLines) Console.Write(" " + i);
                        Console.WriteLine();
                    }
                }
                Console.WriteLine("Нажмите Enter, чтобы вернуться в меню...");
                Console.ReadLine();
            }
        }
    }
}
