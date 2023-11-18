using SumoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Views
{
    public class PlayerView: IPlayerVIew
    {
        public Player createPlayer(int id)
        {
            Console.Clear();
            Console.WriteLine("Wprowadź nazwę " + id + " gracza: ");
            string nazwa = Console.ReadLine();
            Player player = new Player(nazwa,id);
            if (id == 1) player.shape = '+';
            else player.shape = 'o';
            return player;
        }
    }
}
