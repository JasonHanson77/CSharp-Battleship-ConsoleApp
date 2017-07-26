using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{

    public class GameView
    {
        public void displayWelcome()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to Battleship! Prepare yourselves!");
            Console.ResetColor();

        }

        public void showBoardState(string[,] boardState)
        {
            char header = 'A';
                   
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("      B  A  T  T  L  E  S  H  I  P  ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("    ");


            for (int c = 0; c < 10; c++)
            {
                Console.Write("  " + header++);
                
            }

            Console.WriteLine("\n___________________________________ ");

            for (int r = 1; r < 11; r++)
            {
                Console.Write($"{r, 2} |");
                
                for (int c = 1; c < 11; c++)
                {
                    if(boardState[c, r] == "M")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{boardState[c, r], 3}");
                    }
                    else if(boardState[c, r] == "H")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{boardState[c, r],3}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"{boardState[c, r],3}");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
        }
    }
    }

