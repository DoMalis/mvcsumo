using SumoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Views
{
    public class GameView : IGameView
    {


        public Player createPlayer(int playerId)
        {
            Console.Clear();
            Console.WriteLine("Wprowadź nazwę " + playerId + " gracza: ");
            string nazwa = Console.ReadLine();
            Player player = new Player(nazwa,playerId);
            if (playerId == 1) player.shape = '+';
            else player.shape = 'o';
            return player;
        }



    }
}
