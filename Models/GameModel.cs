
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
        public Board Board { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public int Mode { get ; set; } // 
        public Stopwatch gameTimer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
