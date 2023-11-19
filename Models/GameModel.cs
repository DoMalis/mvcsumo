
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
        public bool[,]? obstacleGrid {  get; set; }
            public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public int Mode { get ; set; } // 
        public GameResult GameResult { get; set; }  
    }
}
