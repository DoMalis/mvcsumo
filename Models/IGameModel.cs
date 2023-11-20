
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Models
{
    public interface IGameModel
    {
        bool[,]? ObstacleGrid { get; set; }
        Player Player1 { get; set; }
        Player Player2 { get; set; }
        int Mode {  get; set; }
        public GameResult GameResult { get; set; }
        int SideLength { get;set; }
        int X0 { get; set; }
        int Y0 { get; set; }

    }
}
