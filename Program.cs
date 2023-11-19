using SumoMVC.Controllers;
using SumoMVC.Models;
using SumoMVC.Views;

namespace SumoMVC
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] options = { "Play", "About", "Ranking", "Exit" };
            string prompt = @" _____ _   ____  ________     _____      _              _____                _     
/  ___| | | |  \/  |  _  |_  |  ___|    | |     ___    /  __ \              | |    
\ `--.| | | | .  . | | | (_) | |__  __ _| |_   ( _ )   | /  \/_ __ _   _ ___| |__  
 `--. \ | | | |\/| | | | |   |  __|/ _` | __|  / _ \/\ | |   | '__| | | / __| '_ \ 
/\__/ / |_| | |  | \ \_/ /_  | |__| (_| | |_  | (_>  < | \__/\ |  | |_| \__ \ | | |
\____/ \___/\_|  |_/\___/(_) \____/\__,_|\__|  \___/\/  \____/_|   \__,_|___/_| |_|
                                                                                   
               ";

            IMenuModel menuModel = new MenuModel(prompt, options);
            IMenuView menuView = new MenuView();

            MenuController menuController = new MenuController(menuView, menuModel);

            while (true)
            {
                Console.Clear();
                menuController.ShowMenu();

                
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        menuModel.SelectedIndex--;
                        if (menuModel.SelectedIndex == -1)
                        {
                            menuModel.SelectedIndex = menuModel.Options.Length-1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        menuModel.SelectedIndex++;
                        if (menuModel.SelectedIndex == menuModel.Options.Length)
                        {
                            menuModel.SelectedIndex = 0;
                        }
                        break;
                    case ConsoleKey.Enter:
                        switch (menuModel.SelectedIndex)
                        {
                            case 0:
                                IGameView gameView = new GameView();
                                IGameModel gameModel = new GameModel();
                                GameController gameController=new GameController(gameView, gameModel);
                                menuController.PlayOption(gameController);
                                break;
                            case 1:
                                menuController.DisplayAboutInformation();
                                break;
                            case 2:
                                menuController.Ranking();
                                break;
                            case 3:
                                menuController.Exit();
                                break;
                            default:
                                break;
                        }
                        break;
                }

            }

        }
    }
}
