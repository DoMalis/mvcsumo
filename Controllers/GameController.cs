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


        public void createPlayers()
        {
            gameModel.Player1=gameView.createPlayer(1);
            gameModel.Player1=gameView.createPlayer(2);

        }

        public void StartGame()
        {
            createPlayers();

        }



    }
}
