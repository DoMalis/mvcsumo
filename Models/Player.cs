using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Models
{
    public class Player
    {
        public int Id;
        public string Nick;
        public int Weight;
        public int Points;
        public int x;
        public int y;
        public char shape;


        public Player(String nick, int id)
        {
            this.Nick=nick;
            this.Id=id;
            this.Points=0;
            this.Weight=100;

        }

    }
}
