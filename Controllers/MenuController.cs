using SumoMVC.Models;
using SumoMVC.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Controllers
{
    public class MenuController
    {
        private IMenuModel menuModel;
        private IMenuView menuView;

        public MenuController(IMenuView _menuView, IMenuModel _menuModel)
        {
            this.menuView = _menuView;
            this.menuModel = _menuModel;
        }
        public void ShowMenu()
        {
            menuView.ShowMenu(menuModel);
        }

        public void DisplayAboutInformation()
        {
            menuView.About();
        }

        public void Exit()
        {
            menuView.Exit();
        }

        public void PlayOption(GameController gameController)
        {
            gameController.StartGame();
        }

        public void Ranking()
        {
            menuView.Ranking();
        }



    }
}
