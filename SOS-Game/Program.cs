using System;
using System.Data.Common;


class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the TIC TAC TOE GAME!");

        while (true)
        {

            // Get player names
            Console.Write("Enter the name of Player 1: ");
            string playerOne = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Enter the name of Player 2: ");
            string playerTwo = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("First player is starting...");
            Console.WriteLine();
            string currentPlayer = playerOne;

            // Initialize scores
            int playerOneScore = 0;
            int playerTwoScore = 0;

            // Get column and row size
            Console.Write($"{currentPlayer},Write the board Column:");
            int sizeX = int.Parse(Console.ReadLine());
            Console.Write($"{currentPlayer},Write the board Rows:");
            int sizeY = int.Parse(Console.ReadLine());


            char[,] board = new char[sizeY, sizeX]; // Initialize the char array board.

            PrintBoard(board);

            bool gameWon = false;
            bool boardFull = false;


            while (!gameWon && !boardFull)
            {
                Console.Write($"{currentPlayer}, Select Column:");
                int column = int.Parse(Console.ReadLine()) - 1;
                Console.WriteLine();

                if (0 <= column && column < sizeX)
                {
                    Console.Write($"{currentPlayer},Select Row:");
                    int row = int.Parse(Console.ReadLine()) - 1;
                    Console.WriteLine();

                    if ((0 <= column && column < sizeX) && (0 <= row && row < sizeY))
                    {
                        Console.Write($"{currentPlayer},Write Letter: (X/O): ");
                        char letter = char.ToUpper(Console.ReadKey().KeyChar);
                        Console.WriteLine();

                        if (letter == 'X' || letter == 'O')
                        {
                            if (board[row, column] == '\0') // Ensure cell is empty
                            {
                                board[row, column] = letter;
                                PrintBoard(board);

                                int xoxCount = (CheckForSos(board, row, column));

                                if (xoxCount > 0)
                                {
                                    if (currentPlayer == playerOne)
                                    {
                                        playerOneScore += xoxCount;
                                        Console.WriteLine($"{playerOne} forms {xoxCount} XOX! Current score: {playerOneScore}");
                                    }
                                    else
                                    {
                                        playerTwoScore += xoxCount;
                                        Console.WriteLine($"{playerTwoScore} forms {xoxCount} XOX! Current score: {playerTwoScore}");
                                    }
                                }

                                // Check if board is full
                                boardFull = CheckBoardFull(board);

                                // Switch player if game is not won
                                if (!boardFull)
                                {
                                    currentPlayer = currentPlayer == playerOne ? playerTwo : playerOne;
                                }

                            }
                            else { Console.WriteLine("Cell is already taken. Choose a different cell."); }
                        }
                        else { Console.WriteLine("Wrong letter please write 'X' or 'O'"); }
                    }
                    else { Console.WriteLine("Invalid row size. Please try again."); }
                }
                else { Console.WriteLine("Invalid column size. Please try again."); }
            }

            if (boardFull)
            {
                Console.WriteLine("The game is a draw. No empty cells left.");
                Console.WriteLine($"Final Scores: {playerOne}: {playerOneScore}, {playerTwo}: {playerTwoScore}");
            }

            Console.Write("Do you want to play again? (y/n): ");
            {
                if (Console.ReadLine()?.ToLower() != "y")
                {
                    break;
                }
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

            int CheckForSos(char[,] board, int row, int column)
            {
                int xoxCount = 0;
                // Check horizontal, vertical and diagonal for SOS
                if (CheckDiagonal(board, row, column)) xoxCount++;
                if (CheckVertical(board, row, column)) xoxCount++;
                if (CheckHorizontal(board, row, column)) xoxCount++;

                return xoxCount;
            }
        }


        static bool CheckDiagonal(char[,] board, int row, int column)
        {
            int sizeY = board.GetLength(0);
            int sizeX = board.GetLength(1);

            //Diagonal Bottom-Right Control
            for (int i = -2; i <= 0; i++)
            {

                if (row + i >= 0 && row + i + 2 < sizeY &&
                    column + i >= 0 && column + i + 2 < sizeX)
                {
                    if (board[row + i, column + i] == 'X' &&
                        board[row + i + 1, column + i + 1] == 'O' &&
                        board[row + i + 2, column + i + 2] == 'X')
                    {
                        return true;
                    }
                }
            }

            //Diagonal Bottom-Left Control

            for (int i = -2; i <= 0; i++)
            {
                if (row + i >= 0 && row + i + 2 < sizeY &&
                    column - i - 2 >= 0 && column - i < sizeX)
                {
                    if (board[row + i, column - i] == 'X' &&
                        board[row + i + 1, column - i - 1] == 'O' &&
                        board[row + i + 2, column - i - 2] == 'X')
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static bool CheckVertical(char[,] board, int row, int column)
        {
            int sizeY = board.GetLength(0);

            //Vertical Control

            if (row >= 2 && board[row - 2, column] == 'X' &&
                board[row - 1, column] == 'O' &&
                board[row, column] == 'X')
            {
                return true;
            }

            if (row >= 1 && row < sizeY - 1 &&
                board[row - 1, column] == 'X' &&
                board[row, column] == 'O' &&
                board[row + 1, column] == 'X')
            {
                return true;
            }

            if (row < sizeY - 2 &&
                board[row, column] == 'X' &&
                board[row + 1, column] == 'O' &&
                board[row + 2, column] == 'X')
            {
                return true;
            }


            return false;
        }

        static bool CheckHorizontal(char[,] board, int row, int column)
        {
            int sizeX = board.GetLength(1);

            // Horizontal Control

            if (column >= 2 && board[row, column - 2] == 'X' &&
               board[row, column - 1] == 'O' &&
               board[row, column] == 'X')
            {
                return true;
            }

            if (column >= 1 && column < sizeX - 1 &&
                board[row, column - 1] == 'X' &&
                board[row, column] == 'O' &&
                board[row, column + 1] == 'X')
            {
                return true;
            }

            if (column < sizeX - 2 &&
                board[row, column] == 'X' &&
                board[row, column + 1] == 'O' &&
                board[row, column + 2] == 'X')
            {
                return true;
            }


            return false;
        }

        static bool CheckBoardFull(char[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == '\0') // Empty cell found
                    {
                        return false;
                    }
                }
            }
            return true; // No empty cells found.
        }
    }
}