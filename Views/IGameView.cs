using SumoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Views
{
    public interface IGameView
    {
        //
        Player CreatePlayer(int id);

        //
        int ChooseGameMode();

        //
        void DisplayPlayersInformation(Player player, Player player2);
        bool[,] CreateBattleField(IGameModel gameModel);
        GameResult GameLogic(IGameModel gameModel);

        //
        void EndGame(GameResult gameResult, int mode);


        //void DisplayGameTime(TimeSpan gameDuration);
    }
}
