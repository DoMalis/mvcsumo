using SumoMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Views
{
    public interface IGameView
    {
        Player CreatePlayer(int id);
        void EndGame(GameResult gameResult, int mode);
        int ChooseGameMode();
        void DisplayPlayersInformation(Player player, Player player2);
        void DisplayObstacle(int obstacleX, int obstacleY);
        void DisplayObstacleVertical(int obstacleX, int obstacleY);
        void DisplayFood(Food food);
        void DisplayGameTime(Stopwatch gameTimer);
        void setPlayerInNewPositionInField(Player player);
        void deletePlayerFromOldPositionInField(Player player, int sideLength, int y);
        void DisplayBattleFieldBorders(int x0, int y0, int length);
        void DisplayStartGame();
        void DisplayEndGame();

        



    }
}
