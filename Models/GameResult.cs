using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Models
{
    public class GameResult
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public TimeSpan Time { get; set; }
    }

}
