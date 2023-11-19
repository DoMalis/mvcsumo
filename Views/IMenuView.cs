using SumoMVC.Controllers;
using SumoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Views
{
    public interface IMenuView
    {
        void ShowMenu(IMenuModel menu); 
        void About();
        void Ranking();
        void Exit();

    }
}
