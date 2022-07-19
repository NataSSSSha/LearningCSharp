
using System;

namespace XO
{
        internal class Program
        {
            public int count = 1;
            char[] array = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            public int checkChange;
        public void Print()
            {
                for (int i = 0; i < array.Length; i++)
                {
                    Console.Write($"| {array[i]}");
                    if ((i + 1) % 3 == 0)
                        Console.WriteLine("|");
                }
            }
            public void Move()
            {
            checkChange = 0;
            if (count%2 == 0)
                Console.WriteLine("Второй игрок, введите номер:");
            else
                Console.WriteLine("Первый игрок, введите номер:");
            string? input = Console.ReadLine();
            
                bool result = char.TryParse(input, out var number);
            if (result == true)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (number == array[i] & number != 'X' & number != '0')
                    {
                        array[i] = (count % 2 == 0) ? 'X' : '0';
                        count++;
                        checkChange++;
                    }
                }
                if (checkChange==0)
                    Console.WriteLine("Ошибка. Попробуйте снова");
            }
            else
                Console.WriteLine("Ошибка. Попробуйте снова");
            }
        public bool CheckWin()
        {
            if (array[0] == array[1] & array[0] == array[2] ||
                array[3] == array[4] & array[3] == array[5] ||
                array[6] == array[7] & array[6] == array[8] ||
                array[0] == array[3] & array[0] == array[6] ||
                array[1] == array[4] & array[1] == array[7] ||
                array[2] == array[5] & array[2] == array[8] ||
                array[0] == array[4] & array[0] == array[8] ||
                array[2] == array[4] & array[2] == array[6])
            {
                if (count % 2 == 0)
                    Console.WriteLine("Первый игрок выйграл");
                else
                    Console.WriteLine("Второй игрок выйграл");
                return false;
            }

            else if (count > 9)
            {
                Console.WriteLine("Ничья");
                return false;
            }
            return true;
        }
            static void Main()
            {
            Program game = new Program();
            game.Print();
            do
                {
                game.Move();
                game.Print();
                }
                while (game.CheckWin());
            }
        }
    }


