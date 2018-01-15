using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.ChessBoard
{
    class ChessMoves
    {
        static int KnightMoves(string[,] board, string[] startingDigits, int rows, int columns)
        {
            int currentRow = -1;
            int currentColumn = -1;



            int columnBoundaryMin = 0;
            int columnBoundaryMax = columns - 1;
            int rowBoundaryMin = 0;
            int rowBoundaryMax = rows - 1;


            //string startFrom = "4";
            int numberCount = 0;
            foreach (string startFrom in startingDigits)
            {

                currentRow = currentColumn = -1;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (board[i, j].Equals(startFrom))
                        {
                            //Console.Write(string.Format("[{0}] ", board[i, j]));
                            currentRow = i;
                            currentColumn = j;
                        }
                        else
                        {
                            //Console.Write(string.Format("{0} ", board[i, j]));
                        }

                    }
                    //Console.WriteLine();
                }

                if (currentRow != -1 && currentColumn != -1)
                {
                    //Console.WriteLine("Starting position found in row {0} column {1}", currentRow, currentColumn);

                    int rowIndexAfterMove = currentRow;
                    int columnIndexAfterMove = currentColumn;

                    for (int i = 1; i <= 8; i++)
                    {
                        rowIndexAfterMove = currentRow;
                        columnIndexAfterMove = currentColumn;
                        while (true)
                        {
                            switch (i)
                            {
                                case 1:
                                    rowIndexAfterMove = rowIndexAfterMove + 2;
                                    columnIndexAfterMove = columnIndexAfterMove + 1;
                                    break;
                                case 2:
                                    rowIndexAfterMove = rowIndexAfterMove + 2;
                                    columnIndexAfterMove = columnIndexAfterMove - 1;
                                    break;
                                case 3:
                                    rowIndexAfterMove = rowIndexAfterMove - 2;
                                    columnIndexAfterMove = columnIndexAfterMove + 1;
                                    break;
                                case 4:
                                    rowIndexAfterMove = rowIndexAfterMove - 2;
                                    columnIndexAfterMove = columnIndexAfterMove - 1;
                                    break;
                                case 5:
                                    columnIndexAfterMove = columnIndexAfterMove - 2;
                                    rowIndexAfterMove = rowIndexAfterMove - 1;
                                    break;
                                case 6:
                                    columnIndexAfterMove = columnIndexAfterMove - 2;
                                    rowIndexAfterMove = rowIndexAfterMove + 1;
                                    break;
                                case 7:
                                    columnIndexAfterMove = columnIndexAfterMove + 2;
                                    rowIndexAfterMove = rowIndexAfterMove + 1;
                                    break;
                                case 8:
                                    columnIndexAfterMove = columnIndexAfterMove + 2;
                                    rowIndexAfterMove = rowIndexAfterMove - 1;
                                    break;
                            }

                            if (columnIndexAfterMove >= columnBoundaryMin &&
                                columnIndexAfterMove <= columnBoundaryMax &&
                                rowIndexAfterMove >= rowBoundaryMin &&
                                rowIndexAfterMove <= rowBoundaryMax
                                )
                            {
                                if (Char.IsDigit(board[rowIndexAfterMove, columnIndexAfterMove].ToCharArray()[0]))
                                {
                                    numberCount++;
                                    //Console.WriteLine("Valid position {0}", board[rowIndexAfterMove, columnIndexAfterMove]);
                                }

                                //Console.WriteLine("Valid position {0}", board[rowIndexAfterMove, columnIndexAfterMove]);
                            }
                            else
                            {
                                break;
                            }

                        }
                    }
                }
            }

            return numberCount;
        }

        static int BishopMoves(string[,] board, string[] startingDigits, int rows, int columns)
        {
            int currentRow = -1;
            int currentColumn = -1;



            int columnBoundaryMin = 0;
            int columnBoundaryMax = columns - 1;
            int rowBoundaryMin = 0;
            int rowBoundaryMax = rows - 1;


            //string startFrom = "4";
            int numberCount = 0;
            foreach (string startFrom in startingDigits)
            {
                currentRow = currentColumn = -1;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (board[i, j].Equals(startFrom))
                        {
                            Console.Write(string.Format("[{0}] ", board[i, j]));
                            currentRow = i;
                            currentColumn = j;
                        }
                        else
                        {
                            Console.Write(string.Format("{0} ", board[i, j]));
                        }

                    }
                    Console.WriteLine();
                }

                if (currentRow != -1 && currentColumn != -1)
                {
                    Console.WriteLine("Starting position found in row {0} column {1}", currentRow, currentColumn);

                    int rowIndexAfterMove = currentRow;
                    int columnIndexAfterMove = currentColumn;


                    // Moving South East
                    while (true)
                    {
                        columnIndexAfterMove++;
                        rowIndexAfterMove++;

                        if (
                            columnIndexAfterMove >= columnBoundaryMin &&
                            columnIndexAfterMove <= columnBoundaryMax &&
                            rowIndexAfterMove >= rowBoundaryMin &&
                            rowIndexAfterMove <= rowBoundaryMax
                        )
                        {
                            if (Char.IsDigit(board[rowIndexAfterMove, columnIndexAfterMove].ToCharArray()[0]))
                            {
                                numberCount++;
                                Console.WriteLine("Valid position {0}", board[rowIndexAfterMove, columnIndexAfterMove]);
                            }

                        }
                        else
                        {
                            break;
                        }
                    }

                    // Moving North East
                    rowIndexAfterMove = currentRow;
                    columnIndexAfterMove = currentColumn;
                    while (true)
                    {
                        columnIndexAfterMove++;
                        rowIndexAfterMove--;

                        if (
                            columnIndexAfterMove >= columnBoundaryMin &&
                            columnIndexAfterMove <= columnBoundaryMax &&
                            rowIndexAfterMove >= rowBoundaryMin &&
                            rowIndexAfterMove <= rowBoundaryMax
                        )
                        {
                            if (Char.IsDigit(board[rowIndexAfterMove, columnIndexAfterMove].ToCharArray()[0]))
                            {
                                numberCount++;
                                Console.WriteLine("Valid position {0}", board[rowIndexAfterMove, columnIndexAfterMove]);
                            }

                        }
                        else
                        {
                            break;
                        }
                    }


                    // Moving South West
                    rowIndexAfterMove = currentRow;
                    columnIndexAfterMove = currentColumn;
                    while (true)
                    {
                        columnIndexAfterMove--;
                        rowIndexAfterMove++;

                        if (
                            columnIndexAfterMove >= columnBoundaryMin &&
                            columnIndexAfterMove <= columnBoundaryMax &&
                            rowIndexAfterMove >= rowBoundaryMin &&
                            rowIndexAfterMove <= rowBoundaryMax
                        )
                        {
                            if (Char.IsDigit(board[rowIndexAfterMove, columnIndexAfterMove].ToCharArray()[0]))
                            {
                                numberCount++;
                                Console.WriteLine("Valid position {0}", board[rowIndexAfterMove, columnIndexAfterMove]);
                            }

                        }
                        else
                        {
                            break;
                        }
                    }

                    // Moving North West
                    rowIndexAfterMove = currentRow;
                    columnIndexAfterMove = currentColumn;
                    while (true)
                    {
                        columnIndexAfterMove--;
                        rowIndexAfterMove--;

                        if (
                            columnIndexAfterMove >= columnBoundaryMin &&
                            columnIndexAfterMove <= columnBoundaryMax &&
                            rowIndexAfterMove >= rowBoundaryMin &&
                            rowIndexAfterMove <= rowBoundaryMax
                        )
                        {
                            if (Char.IsDigit(board[rowIndexAfterMove, columnIndexAfterMove].ToCharArray()[0]))
                            {
                                numberCount++;
                                Console.WriteLine("Valid position {0}", board[rowIndexAfterMove, columnIndexAfterMove]);
                            }

                        }
                        else
                        {
                            break;
                        }
                    }


                }
            }

            return numberCount;
        }


        static void Start()
        {

            string piece = Console.ReadLine();
            //string piece = "knight";

            int numberLength = Convert.ToInt32(Console.ReadLine());
            //int numberLength = 7;


            //string[] startingDigits = new string[] { "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] startingDigits = Console.ReadLine().Split(' ');

            int boardRows = Convert.ToInt32(Console.ReadLine());
            //int boardRows = 4;

            int boardColumns = Convert.ToInt32(Console.ReadLine());
            //int boardColumns = 3;


            string[,] board = new string[boardRows, boardColumns];
            string[] temp = null;

            for (int i = 0; i < boardRows; i++)
            {
                /*
                if (i == 0)
                    temp = new string[] { "1", "2", "3" };
                if (i == 1)
                    temp = new string[] { "4", "5", "6" };
                if (i == 2)
                    temp = new string[] { "7", "8", "9" };
                if (i == 3)
                    temp = new string[] { "*", "0", "#" };
                */
                temp = Console.ReadLine().Split(' ');
                for (int j = 0; j < boardColumns; j++)
                {
                    board[i, j] = temp[j];
                }

            }

            int possibleNumbers = 0;
            if (piece.Equals("knight"))
            {
                possibleNumbers = KnightMoves(board, startingDigits, boardRows, boardColumns);
            }
            if (piece.Equals("bishop"))
            {
                possibleNumbers = BishopMoves(board, startingDigits, boardRows, boardColumns);
            }

            Console.WriteLine(possibleNumbers);
        }
    }

}
