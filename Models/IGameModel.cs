
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
        Board Board { get; set; }
        Player Player1 { get; set; }
        Player Player2 { get; set; }
        int Mode {  get; set; }
        Stopwatch gameTimer { get; set; }


    }
}
