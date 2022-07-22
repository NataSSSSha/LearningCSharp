using System;

namespace TicTacToe
{
    internal class Program
    {
        /// <summary>
        /// counts the number of moves
        /// </summary>
        public int countMove = 1;
        /// <summary>
        /// contains move data
        /// </summary>
        char[] possibleMove = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        /// <summary>
        /// shows the field 
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < possibleMove.Length; i++)
            {
                Console.Write($"| {possibleMove[i]}");
                if ((i + 1) % 3 == 0)
                    Console.WriteLine("|");
            }
        }
        /// <summary>
        /// makes move
        /// </summary>
        public void Move()
        {
            if (countMove % 2 == 0)
                Console.WriteLine("Второй игрок, введите номер:");
            else
                Console.WriteLine("Первый игрок, введите номер:");
            string? input = Console.ReadLine();
            bool result = Int32.TryParse(input, out int number);
            if (result == true)
            {
                if (number > 0 & number < 10)
                {
                    possibleMove[number - 1] = (countMove % 2 == 0) ? 'X' : '0';
                    countMove++;
                }
                else
                    Console.WriteLine("Ошибка. Попробуйте снова");
            }
            else
                Console.WriteLine("Ошибка. Попробуйте снова");
        }
        /// <summary>
        /// checks win combination
        /// </summary>
        /// <returns> true,if the game continues,
        /// false - there is a winner</returns>
        public bool CheckWin()
        {
            bool win = false;
            for (int i = 0; i < 7; i++)
            {
                if (i <= 2)
                    if (possibleMove[i] == possibleMove[i + 3] & possibleMove[i] == possibleMove[i + 6])
                    {
                        i += 7;
                        win = true;
                    }
                if (i == 0 || i % 3 == 0)

                    if (possibleMove[i] == possibleMove[i + 1] & possibleMove[i] == possibleMove[i + 2])
                    {
                        i += 7;
                        win = true;
                    }
                if (i == 0)
                    if (possibleMove[i] == possibleMove[i + 4] & possibleMove[i] == possibleMove[i + 8])
                    {
                        i += 7;
                        win = true;
                    }
                if (i == 2)
                    if (possibleMove[i] == possibleMove[i + 2] & possibleMove[i] == possibleMove[i + 4])
                    {
                        i += 7;
                        win = true;
                    }
            }

            if (win)
            {
                if (countMove % 2 == 0)
                    Console.WriteLine("Первый игрок выйграл");
                else
                    Console.WriteLine("Второй игрок выйграл");
                return false;
            }
            if (countMove > 9)
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