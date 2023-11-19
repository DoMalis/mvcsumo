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
        public void CreatePlayers()
        {
            gameModel.Player1=gameView.CreatePlayer(1);
            gameModel.Player2=gameView.CreatePlayer(2);

        }
        public void ChooseGameMode()
        {
            gameModel.Mode = gameView.ChooseGameMode();
        }
        public void CreateBattleField()
        {
            gameView.CreateBattleField(gameModel);
        }
        public void DisplayPlayerInformation()
        {
            gameView.DisplayPlayersInformation(gameModel.Player1,gameModel.Player2);
        }

        public void StartGame()
        {
            CreatePlayers();
            ChooseGameMode();
            DisplayPlayerInformation();
            CreateBattleField();




        }





    }
}
