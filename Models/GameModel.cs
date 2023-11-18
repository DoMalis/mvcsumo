using SumoMVC.ENum;
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
        public Player Player1 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Player Player2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Mode Mode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Stopwatch gameTimer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
