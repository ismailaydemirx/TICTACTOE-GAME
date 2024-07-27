using System;
using System.Data.Common;


class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the TIC TAC TOE GAME!");

        while (true)
        {

            Console.Write("Write the board Column:");
            int sizeX = int.Parse(Console.ReadLine());
            Console.Write("Write the board Rows:");
            int sizeY = int.Parse(Console.ReadLine());


            char[,] board = new char[sizeY, sizeX];

            PrintBoard(board);


            while (true)
            {
                Console.Write("Select Column:");
                int column = int.Parse(Console.ReadLine()) - 1;
                Console.WriteLine();

                if (0 <= column && column < sizeX)
                {
                    Console.Write("Select Row:");
                    int row = int.Parse(Console.ReadLine()) - 1;
                    Console.WriteLine();

                    if ((0 <= column && column < sizeX) && (0 <= row && row < sizeY))
                    {
                        Console.Write("Write Letter:");
                        char letter = Console.ReadKey().KeyChar;
                        Console.WriteLine();

                        if (letter == 'X' || letter == 'O')
                        {
                            board[row, column] = letter;

                            PrintBoard(board);
                        }
                        else
                        {
                            Console.WriteLine("Wrong letter please write 'X' or 'O'");
                        }
                    }
                    else { Console.WriteLine("Invalid row size. Please try again."); }
                }
                else { Console.WriteLine("Invalid column size. Please try again."); }
            }

            static void PrintBoard(char[,] board)
            {
                Console.Clear();
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        Console.Write(board[i, j] == '\0' ? '_' : board[i, j]);
                    }
                    Console.WriteLine();
                }
            }

            bool CheckForSos(char[,] board, int row, int column)
            {
                // Check horizontal, vertical and diagonal for SOS
                return CheckHorizontal(board, row, column) ||
                       CheckVertical(board, row, column) ||
                       CheckDiagonal(board, row, column);
            }
        }


        bool CheckDiagonal(char[,] board, int row, int column)
        {
            int sizeY = board.GetLength(0);
            int sizeX = board.GetLength(1);

            //Diagonal Bottom-Right Control
            for (int i = -2; i <= 0; i++)
            {

                if (row + i >= 0 && row + 2 < sizeY &&
                    column + i >= 0 && column + 2 < sizeX)
                {
                    if (board[row + i, column + i] == 'X' &&
                        board[row + i + 1, column + i + 1] == 'O' &&
                        board[row + i + 2, column + i + 2] == 'X')
                    {
                        return true;
                    }
                }
            }

            for (int i = 0; i <= 2; i++)
            {
                if (row + i > 0 && row + i + 2 < sizeY &&
                    column - i >= 0 && column - i - 2 < sizeX)
                {

                    //Diagonal Bottom-Left Control
                    if (board[row + i, column - 1] == 'X' &&
                        board[row + i + 1, column - i - 1] == 'O' &&
                        board[row + i + 2, column - i - 2] == 'X')
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        bool CheckVertical(char[,] board, int row, int column)
        {
            int startRow = Math.Max(0, row - 2);
            int endRow = Math.Max(0, row + 2);

            //Vertical Control
            for (int i = startRow; i < endRow; i++)
            {
                if (board[row, i] == 'X' && board[row, i++] == 'O' && board[row, i + 2] == 'X')
                {
                    return true;
                }
            }

            return false;
        }

        bool CheckHorizontal(char[,] board, int row, int column)
        {
            int startColumn = Math.Max(0, column - 2);
            int endColumnt = Math.Max(0, column + 2);

            // Horizontal Control
            for (int i = startColumn; i < endColumnt; i++)
            {
                if (board[row, i] == 'X' && board[row, i + 1] == 'O' && board[row, i + 2] == 'X')
                {
                    return true;
                }
            }
            return false;
        }
    }
}