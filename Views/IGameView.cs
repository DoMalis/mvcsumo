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
        //void DisplayPlayersInformation(Player player1, Player player2);
        //void DisplayGameTime(TimeSpan gameDuration);


        Player CreatePlayer(int id);//idk czy to tu powinno byc
        int ChooseGameMode();
        void CreateBattleField(IGameModel gameModel);
    }
}
