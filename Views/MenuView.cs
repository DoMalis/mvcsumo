using SumoMVC.Controllers;
using SumoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Views
{
    public class MenuView : IMenuView
    {
        public void ShowMenu(IMenuModel menu)
        {
            Console.WriteLine(menu.Prompt);
            for (int i = 0; i < menu.Options.Length; i++)
            {
                string prefix;
                if (i == menu.SelectedIndex)
                {
                    prefix = "*";
                }
                else
                {
                    prefix = " ";
                }
                Console.WriteLine($"{prefix} << {menu.Options[i]} >>");
            }
        }

        public void About()
        {

            string prompt = @" \ \        / / | |                          | |         / ____|                       |  _ \      | | | | | |    | |
  \ \  /\  / /__| | ___ ___  _ __ ___   ___  | |_ ___   | (___  _   _ _ __ ___   ___   | |_) | __ _| |_| |_| | ___| |
   \ \/  \/ / _ \ |/ __/ _ \| '_ ` _ \ / _ \ | __/ _ \   \___ \| | | | '_ ` _ \ / _ \  |  _ < / _` | __| __| |/ _ \ |
    \  /\  /  __/ | (_| (_) | | | | | |  __/ | || (_) |  ____) | |_| | | | | | | (_) | | |_) | (_| | |_| |_| |  __/_|
     \/  \/ \___|_|\___\___/|_| |_| |_|\___|  \__\___/  |_____/ \__,_|_| |_| |_|\___/  |____/ \__,_|\__|\__|_|\___(_)
                                                                                                                     
                                                                                                                     ";
            Console.Clear();
            Console.WriteLine(prompt);
            Console.WriteLine("Eat as much food as you can to gain weight and strength and try to crush your opponent.");
            Console.WriteLine("But be careful, the more you move, the more weight you will lose.");
            Console.WriteLine("Collect food items of different weights to help you defeat your opponent.");
            Console.WriteLine("Authors: Dorota Maliszewska and Justyna Sadowska");
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey(true);

        }

        public void Exit()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to exit the game? (Y/N)");

            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Y)//jesli tak
            {
                Console.WriteLine("Exiting the application...");
                System.Threading.Thread.Sleep(1000);
                Environment.Exit(0);
            }
            else//jesli nie
            {
                return;
            }

        }

        public void PlayOption(GameController gameController)
        {
        }

        public void Ranking()
        {
            string prompt = @" _____             _    _             
 |  __ \           | |  (_)            
 | |__) |__ _ _ __ | | ___ _ __   __ _ 
 |  _  // _` | '_ \| |/ / | '_ \ / _` |
 | | \ \ (_| | | | |   <| | | | | (_| |
 |_|  \_\__,_|_| |_|_|\_\_|_| |_|\__, |
                                  __/ |
                                 |___/ ";
            Console.Clear();
            Console.WriteLine(prompt);
            Console.ReadKey(true);
        }
    }








}
