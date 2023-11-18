using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Models
{
    public class MenuModel :IMenuModel
    {
        public string[] Options {  get; set; }
        public int SelectedIndex {  get; set; }
        public string Prompt {  get; set; }


        public MenuModel(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
        }

    }
}
