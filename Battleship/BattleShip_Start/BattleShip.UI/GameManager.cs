using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using System;


namespace BattleShip.UI
{
    public class GameManager
    {
        public bool _gameWon = false;
        public Board[] _board = new Board[2];
        public GameInput _gameInput = new GameInput();
        public GameView _gameView = new GameView();
        public string[] _players = new string[2];
        public int _playerTurn = 0;
        public Coordinate _playerShotCoordinate;
        public FireShotResponse _shotResponse;
        public Coordinate _playerShipPlacement;
        public ShipDirection _playerShipDir;
        public ShipPlacement _boardPlacementRequest;
        public string[,] _boardStateOne = new string[11, 11];
        public string[,] _boardStateTwo = new string[11, 11];
        public string[,] _currentBoardState = new string[11, 11];

        public GameManager(GameView _gameView, GameInput _gameInput)
        {
            this._gameView = new GameView();
            this._gameInput = new GameInput();
            _board[0] = new Board();
            _board[1] = new Board();
        }

        public void startGame()
        {
            displayWelcome();
            promptForPlayerName();
            promptForShipPlacement();
            coinToss();

            do
            {
                showGameState(_playerTurn);
                promptPlayerForCoordinate();
                attemptShot();
                handleAttemptedShot(_shotResponse);
                Console.ReadKey();

            } while (!_gameWon);

            if (_gameWon)
            {
                string quitGame = "";
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Would you like to play again?  Y or N: ");
                quitGame = Console.ReadLine().ToUpper();

                switch (quitGame)
                {
                    case "Y":
                        Console.WriteLine("Press Enter to start another game.");
                        Console.ReadLine();
                        _gameWon = false;
                        Console.Clear();
                        _board[0] = new Board();
                        _board[1] = new Board();

                        for (int r = 0; r < 11; r++)
                        {
                            for (int c = 0; c < 11; c++)
                            {
                                _boardStateOne[c, r] = " ";
                                _boardStateTwo[c, r] = " ";
                                _currentBoardState[c, r] = " ";
                            }
                        }

                        startGame();
                        break;
                    case "N":
                        Console.WriteLine("Press Enter to exit game.");
                        Console.ReadLine();
                        Environment.Exit(0);
                        break;
                }
            }
        }


        public void displayWelcome()
        {
            _gameView.displayWelcome();
        }

        public void promptForPlayerName()
        {
            _players[0] = _gameInput.promptForPlayerNames("Player 1");
            _players[1] = _gameInput.promptForPlayerNames("Player 2");
        }

        public void promptForShipPlacement()
        {
            for (int i = 0; i < _board.Length; i++)
            {

                Console.ForegroundColor = ConsoleColor.White;



                ShipType shipType = ShipType.Destroyer;
                int turn = _playerTurn + 1;
                int shipcount = 0;
                string printShipType = "";
                int numberOfSlots = 0;

                Console.WriteLine("Player {0}, {1} it is time to place your ships on the board!", turn, _players[i]);

                do
                {
                    Console.Clear();
                    showGameState(_playerTurn);


                    string shipPosition = _gameInput.promptForShipPlacementCoordinate(shipType, _players[_playerTurn]);
                    CoordConverter xCoord = new CoordConverter(shipPosition);

                    _playerShipPlacement = new Coordinate(xCoord.coord, int.Parse(shipPosition.Substring(1)));
                    ShipDirection shipDir = _gameInput.promptForShipPlacementDirection();
                    _playerShipDir = shipDir;

                    PlaceShipRequest request = new PlaceShipRequest()
                    {
                        ShipType = shipType,
                        Coordinate = _playerShipPlacement,
                        Direction = _playerShipDir,
                    };

                    _boardPlacementRequest = _board[i].PlaceShip(request);

                    if (!handleBoardPlacementRequest(_boardPlacementRequest))
                        continue;
                    switch (shipType)
                    {
                        case ShipType.Destroyer:
                            printShipType = "D";
                            numberOfSlots = 2;
                            break;
                        case ShipType.Cruiser:
                            printShipType = "c";
                            numberOfSlots = 3;
                            break;
                        case ShipType.Carrier:
                            printShipType = "C";
                            numberOfSlots = 5;
                            break;
                        case ShipType.Submarine:
                            printShipType = "S";
                            numberOfSlots = 3;
                            break;
                        case ShipType.Battleship:
                            printShipType = "B";
                            numberOfSlots = 4;
                            break;

                    }

                    for (int r = 0; r < numberOfSlots; r++)
                    {
                        if (shipDir == ShipDirection.Down)
                        {
                            _currentBoardState[(_playerShipPlacement.XCoordinate), (_playerShipPlacement.YCoordinate) + r] = printShipType;
                        }
                        else if (shipDir == ShipDirection.Up)
                        {
                            _currentBoardState[(_playerShipPlacement.XCoordinate), (_playerShipPlacement.YCoordinate) - r] = printShipType;
                        }
                        else if (shipDir == ShipDirection.Right)
                        {
                            _currentBoardState[(_playerShipPlacement.XCoordinate) + r, (_playerShipPlacement.YCoordinate)] = printShipType;
                        }
                        else if (shipDir == ShipDirection.Left)
                        {
                            _currentBoardState[(_playerShipPlacement.XCoordinate) - r, (_playerShipPlacement.YCoordinate)] = printShipType;
                        }

                    }

                    if (shipcount == 4)
                    {
                        if (_playerTurn == 0)
                        {
                            showGameState(_playerTurn);
                            Console.WriteLine("You have completed setting up your board. Press Enter to clear board and allow Player 2 to set up their board.");
                        }
                        else
                        {
                            showGameState(_playerTurn);
                            Console.WriteLine("You have completed the set up of your board. Press Enter to start to coin toss.");
                        }

                        Console.ReadLine();
                        for (int r = 0; r < 11; r++)
                        {
                            for (int c = 0; c < 11; c++)
                            {
                                _currentBoardState[c, r] = "";
                            }

                        }
                        changeTurnToNextPlayer();
                    }

                    shipcount++;
                    shipType++;
                    Console.Clear();

                } while (shipcount < 5);

            }
        }


        public bool handleBoardPlacementRequest(ShipPlacement shipPlacement)
        {
            switch (shipPlacement)
            {
                case ShipPlacement.NotEnoughSpace:
                    Console.Write("There is not enough space for this ship. Press any key to try another coordinate!");
                    Console.ReadKey();
                    Console.Clear();
                    return false;
                case ShipPlacement.Overlap:
                    Console.Write("This ship overlaps another ship! Press any key to try another coordinate!");
                    Console.ReadKey();
                    Console.Clear();
                    return false;
                case ShipPlacement.Ok:
                    Console.Write("Placement of ship is a success! Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                    return true;
                default:
                    return false;

            }
        }

        public void coinToss()
        {
            _playerTurn = 0;
            if (!(_gameInput.seeWhoGoesFirst()))
            {
                changeTurnToNextPlayer();
            }

        }

        public void showGameState(int _playerTurn)
        {
            
            _currentBoardState = _boardStateOne;

            if (_playerTurn == 1)
            {
                _currentBoardState = _boardStateTwo;
            }

            for (int r = 0; r < 11; r++)
            {
                for (int c = 0; c < 11; c++)
                {
                    Coordinate coordinate = new Coordinate(c, r);

                    ShotHistory value;
                    if (_board[opponentsBoard(_playerTurn)].ShotHistory.TryGetValue(coordinate, out value))
                    {
                        switch (value)
                        {

                            case ShotHistory.Hit:
                                _currentBoardState[c, r] = "H";
                                break;

                            case ShotHistory.Miss:
                                _currentBoardState[c, r] = "M";
                                break;

                            case ShotHistory.Unknown:
                                _currentBoardState[c, r] = " ";
                                Console.ResetColor();
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("");
                    }
                }
            }
            _gameView.showBoardState(_currentBoardState);
        }

        public void promptPlayerForCoordinate()
        {
            string alphaNumericPosition = _gameInput.promptForPlayerShot(_playerTurn, _players[_playerTurn]);
            CoordConverter xCoord = new CoordConverter(alphaNumericPosition);
            _playerShotCoordinate = new Coordinate(xCoord.coord, int.Parse(alphaNumericPosition.Substring(1)));
        }

        public void attemptShot()
        {
            _shotResponse = _board[opponentsBoard(_playerTurn)].FireShot(_playerShotCoordinate);
        }

        public void handleAttemptedShot(FireShotResponse _shotResponse)
        {

            switch (_shotResponse.ShotStatus)
            {
                case ShotStatus.Duplicate:
                    Console.Write("Duplicate Shot! Please try again!");
                    break;
                case ShotStatus.Hit:
                    Console.Write("You have hit your opponent's {0}!", _shotResponse.ShipImpacted);
                    changeTurnToNextPlayer();
                    break;
                case ShotStatus.HitAndSunk:
                    Console.Write("You have sunk your opponent's {0}!", _shotResponse.ShipImpacted);
                    changeTurnToNextPlayer();
                    break;
                case ShotStatus.Invalid:
                    Console.Write("Invalid coordinate!");
                    break;
                case ShotStatus.Miss:
                    Console.Write("Your shot was a miss!");
                    changeTurnToNextPlayer();
                    break;
                case ShotStatus.Victory:
                    Console.Write("You have sunk your opponent's {0} and are victorious!!", _shotResponse.ShipImpacted);
                    _gameWon = true;
                    break;

            }

        }

        public void changeTurnToNextPlayer()
        {
            if (_playerTurn == 0)
            {
                _playerTurn = 1;
            }
            else
            {
                _playerTurn = 0;
            }

        }

        public int opponentsBoard(int playerTurn)
        {
            int board = 0;
            if (playerTurn == 0)
                return board = 1;

            return board;
        }

    }
}




