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
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(menu.Prompt);
            Console.ResetColor();
            for (int i = 0; i < menu.Options.Length; i++)
            {
                Console.Write("\t\t\t\t\t\t");
                if (i == menu.SelectedIndex)
                { 
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<< {menu.Options[i]} >>");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"<< {menu.Options[i]} >>");
                }
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
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(prompt);
            Console.ResetColor();
            Console.SetCursorPosition(24, 10);
            string str = "Eat as much food as you can to gain weight, strength and try to crush your opponent.\n\t\t\tBut be careful, the more you move, the more weight you will lose.\n\t\t\tCollect food items of different weights to help you defeat your opponent.";
            for (int ctr = 0; ctr <= str.Length - 1; ctr++)
            {
                Console.Write("{0}", str[ctr]);
                Thread.Sleep(30);

            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(53,20);
            Console.WriteLine("Authors:");
            Console.ResetColor();
            Console.SetCursorPosition(38, 22);
            Console.Write("Dorota Maliszewska and Justyna Sadowska");
            Console.WriteLine("\n\n\n\n\n\nPress any key to return to the menu.");
            Console.ReadKey(true);

        }

        public void Exit()
        {
            Console.Clear();
            string[] options = { "yes", "no" };
            int selectedIndex = 0;
            ConsoleKey keyPressed;
            do
            {
                Console.Clear(); //czyscimy konsole
                Console.SetCursorPosition(35, 5);
                Console.WriteLine("Are you sure you want to exit the game?");
                for (int i = 0; i < options.Length; i++)
                {
                    Console.Write("\t\t\t\t\t\t");
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($" << {options[i]} >>");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($" << {options[i]} >>");
                    }

                }
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); //wczytujemy informacje o wcisnietym przycisku
                keyPressed = keyInfo.Key; //przypisujemy wartosc wcisnietego przycisku

                if (keyPressed == ConsoleKey.DownArrow) //jesli wcisnieta jest strzalka w dol
                {
                    selectedIndex++;
                    if (selectedIndex == options.Length)
                    {
                        selectedIndex = 0;
                    }
                }
                else if (keyPressed == ConsoleKey.UpArrow) //jesli wcisnieta jest strzalka w gore
                {
                    selectedIndex--;
                    if (selectedIndex == -1)
                    {
                        selectedIndex = options.Length - 1;
                    }

                }


            } while (keyPressed != ConsoleKey.Enter); //petla sie bedzie wykonywac dopoki nie wcisniemy enter
            if(selectedIndex == 0) 
            {
                Console.Clear();
                Console.WriteLine("Exiting the application...");
                System.Threading.Thread.Sleep(1000);
                Environment.Exit(0);

            }
            else
            {
                return;
            }

        }
        public void LoadGameResults(string resultFilePath, int x, int y)
        {
            List<GameResult> gameResults = new List<GameResult>();
            if (File.Exists(resultFilePath))
            {
                // Odczytujemy wszystkie linie z pliku rankingowego
                string[] lines = File.ReadAllLines(resultFilePath);
                
                foreach (string line in lines)
                {
                    // Dzielimy linię na nazwę gracza i wynik
                    string[] parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        string playerName = parts[0];
                        int score = int.Parse(parts[1]);
                        string timeStr = parts[2];
                        TimeSpan gameTime = TimeSpan.Parse(timeStr);

                        Player player = new Player(playerName, 0);
                        player.Weight=score;

                        gameResults.Add(new GameResult (player, gameTime ));
                    }
                }
            }
            if (gameResults.Count > 0)
            {
                // Sortujemy wyniki malejąco według punktów
                gameResults = gameResults.OrderBy(result => result.Time).ThenByDescending(result => result.Score).ToList();

                int position = 1;
                foreach (var result in gameResults)
                {
                    Console.SetCursorPosition(x, y+position);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{ position}.");
                    Console.ResetColor();
                    Console.SetCursorPosition(x+3, y + position);
                    Console.WriteLine(result.PlayerName);
                    Console.SetCursorPosition(x + 15, y + position);
                    Console.WriteLine($"{result.Score} points");
                    Console.SetCursorPosition(x + 30, y + position);
                    Console.WriteLine($"{result.Time.TotalSeconds} sec");
                    position++;
                }
            }
            else
            {
                Console.WriteLine("No results in the ranking yet.");
            }

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
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(prompt);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 9);
            Console.WriteLine("STANDARD BOARD");
            Console.ResetColor();
            string resultFilePath = "ranking.txt";
            LoadGameResults(resultFilePath, 0, 9);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(40, 9);
            Console.WriteLine("BOARD WITH STATIC OBSTACLES");
            Console.ResetColor();
            resultFilePath = "rankingStatic.txt";
            LoadGameResults(resultFilePath, 40, 9);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(80, 9);
            Console.WriteLine("BOARD WITH RANDOM OBSTACLES");
            Console.ResetColor();
            resultFilePath = "rankingRandom.txt";
            LoadGameResults(resultFilePath, 80, 9);
            Console.SetCursorPosition(0, 28);
            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey(true);
        }
    }
    


}
