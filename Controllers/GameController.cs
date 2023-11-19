using SumoMVC.Models;
using SumoMVC.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Controllers
{
    public class GameController
    {
        private IGameView gameView;
        private IGameModel gameModel;

        public GameController(IGameView gameView, IGameModel gameModel)
        {
            this.gameView = gameView;
            this.gameModel = gameModel;
        }
        public void CreatePlayers() //to oodzielny widok
        {
            gameModel.Player1=gameView.CreatePlayer(1);
            gameModel.Player2=gameView.CreatePlayer(2);

        }
        public void ChooseGameMode() //to oddzileny widok
        {
            gameModel.Mode = gameView.ChooseGameMode();
        }

        public void StartGame() //to chyba nie xd
        {
            CreatePlayers();
            ChooseGameMode();
            Game();
        }

        public void Game() //to oddzielny widok
        {
            DisplayPlayerInformation();
            CreateBattleField();
            gameModel.GameResult=gameView.GameLogic(gameModel);
            End();

        }

        public void End()//to oddzielny widok
        {
            gameView.EndGame(gameModel.GameResult,gameModel.Mode);
        }


        public void CreateBattleField()//to tlyko czesc widoku chyba
        {
            gameModel.obstacleGrid=gameView.CreateBattleField(gameModel);
        }
        public void DisplayPlayerInformation() //to tak jak wyzej
        {
            gameView.DisplayPlayersInformation(gameModel.Player1, gameModel.Player2);
        }










    }
}
