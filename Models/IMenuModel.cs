using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Models
{
    public interface IMenuModel
    {
        public string[] Options { get; set; }
        public int SelectedIndex { get; set; }
        public string Prompt { get; set; }

    }
}
