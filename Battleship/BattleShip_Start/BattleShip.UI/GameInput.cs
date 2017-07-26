using BattleShip.BLL.Requests;
using BattleShip.BLL.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    public class GameInput
    {
        public string promptForPlayerNames(string playerNameInput)
        {
            string _playerName;

            do
            {
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("Player please enter your name or enter \"Q\" to quit: ");
                _playerName = Console.ReadLine();

                if(_playerName.ToUpper().Equals("Q"))
                {
                    Console.Write("Press any key to surrender and quit game.");
                    Console.Read();
                    Environment.Exit(0);
                }

                if (String.IsNullOrEmpty(_playerName))
                {
                    Console.WriteLine("Name can not be left empty!  Press enter key to enter and then enter a name!");
                    Console.ReadLine();
                    continue;

                }
                Console.WriteLine("Thank You {0}! Press enter to continue.", _playerName);
                Console.ReadLine();
                Console.ResetColor();
                Console.Clear();

            } while (String.IsNullOrEmpty(_playerName));

            return _playerName;
        }

        public string promptForShipPlacementCoordinate(ShipType shipType, string player)
        {
            string placementCoord = "";
           
            Console.ForegroundColor = ConsoleColor.White;

            do
            {
                Console.Write("{0}, Please enter a coordinate starting with a letter (A-J) and then a number (1-10) example \"B10\" to place your {1}: ", player, shipType);
                placementCoord = Console.ReadLine().ToUpper();


                if (String.IsNullOrEmpty(placementCoord))
                {
                    Console.WriteLine("Please enter a coordinate to continue! Press any key to continue");
                    Console.ReadKey();
                    continue;
                }

                string intString = placementCoord.Substring(1);
                try
                {
                    if (placementCoord.Length >= 2 && placementCoord[0] >= 'A' && placementCoord[0] <= 'J'
                             && int.Parse(intString) >= 1 && int.Parse(intString) <= 10)
                    {
                        Console.WriteLine("You have entered {0}. Press any key to continue and enter direction of your ship.", placementCoord);
                        Console.ReadKey();
                        break;
                    }

                    else
                    {
                        Console.WriteLine("Coordinate is invalid. Please try again.");
                        continue;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Coordinate is invalid. Please try again.");
                    continue;
                }

            } while (true);

            Console.ResetColor();

            return placementCoord;

        }

        public ShipDirection promptForShipPlacementDirection()
        {
            string placementDir;
            ShipDirection shipDir = 0;

            Console.ForegroundColor = ConsoleColor.White;

            do
            {
                Console.Write("Enter the direction you would like your ship to face. Enter N for North, S for South, W for West, and E for East: ");
                placementDir = Console.ReadLine().ToUpper();
                if ((placementDir.Equals("N")) || (placementDir.Equals("S")) || (placementDir.Equals("W")) || (placementDir.Equals("E")))
                    {
                    Console.WriteLine("You have chosen {0} for your ship's direction. Enter to continue", placementDir);
                    Console.ReadLine();
                    
                    switch (placementDir)
                    {
                        case "N":
                            shipDir = ShipDirection.Up;
                            break;
                        case "S":
                            shipDir = ShipDirection.Down;
                            break;
                        case "E":
                            shipDir = ShipDirection.Right;
                            break;
                        case "W":
                            shipDir = ShipDirection.Left;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("You must enter a direction for your ship to face (N, S, E, W)! Press enter to continue");
                    Console.ReadLine();
                    continue;
                }
                Console.ResetColor();
                return shipDir;

            } while (true);
        }

        public bool seeWhoGoesFirst()
        {
            bool _playerGoesFirst = false;
            Random _r = new Random();
            int _headsOrTails = _r.Next(0, 3);
            int hOrT = 0;
            string _coinSideCalled;

            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Player One Enter \"H\" for heads or \"T\" for tails to see who goes first. Enter \"Q\" to exit game: ");
                _coinSideCalled = Console.ReadLine().ToUpper();

                if (_coinSideCalled.Equals("H"))
                {
                    hOrT = 1;
                    break;
                }
                else if (_coinSideCalled.Equals("T")) 
                {
                    hOrT = 2;
                    break;
                }
                else if (_coinSideCalled.Equals("Q"))
                {
                    Console.WriteLine("Press any key to surrender and quit game.");
                    Console.Read();
                    Environment.Exit(0);
                }
                else
                {
                    Console.Write("Please call heads or tails to see which player goes first or enter \"Q\" to quit: ");
                }
            } while (true);
                
            if((hOrT == _headsOrTails))
            {
                _playerGoesFirst = true;
                Console.WriteLine("You have won the coin toss and get to take the first shot! Press any key to continue!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("You have lost the coin toss and Player 2 will take the first shot.  Press any key to continue!");
                _playerGoesFirst = false;
                Console.ReadKey();
            }
            Console.ResetColor();
            return _playerGoesFirst;
        }

        public string promptForPlayerShot(int playerNumber, string playerName)     {
            string shotCoordinate;

            Console.ForegroundColor = ConsoleColor.White;
            do
            {
                Console.Write("Player {0}, {1}, Please enter your shot coordinate in the form of (A-J) for rows and (1-10) for column. Example ('B10'): ", playerNumber + 1 , playerName);
                shotCoordinate = Console.ReadLine().ToUpper();
                try
                {
                    if (shotCoordinate.Length >= 2 && shotCoordinate[0] >= 'A' && shotCoordinate[0] <= 'J'
                        && int.Parse(shotCoordinate.Remove(0, 1)) >= 1 && int.Parse(shotCoordinate.Remove(0, 1)) <= 10)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                catch(FormatException e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Invalid coordinate format! You must enter coordinates in the form of the first coordinate being A-J and the second coordinate as (1-10)!");
                    Console.WriteLine("Press Enter to try again!");
                    Console.ReadLine();
                    Console.Clear();

                    continue; 
                }
            } while (true);
            Console.ResetColor();
            return shotCoordinate;
            
            
        }
    }
}
