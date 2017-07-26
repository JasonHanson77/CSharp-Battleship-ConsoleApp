using BattleShip.BLL.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            Board _boardOne = new Board();
            Board _boardTwo = new Board();
            GameView _gameView = new GameView();
            GameInput _gameInput = new GameInput();
            GameManager _gameManager = new GameManager(_gameView, _gameInput);

            _gameManager.startGame();
        }
    }
}
