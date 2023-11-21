
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Models
{
    public class GameModel : IGameModel
    {
        public bool[,]? ObstacleGrid { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public int Mode { get; set; }
        public GameResult GameResult { get; set; }
        public int SideLength { get; set; }
        public int X0 { get; set; }
        public int Y0 { get; set; }

        public GameModel()
        {
            SideLength = 10;
            X0 = 40;
            Y0 = 10;
        }



    }
}
