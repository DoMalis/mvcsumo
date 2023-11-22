using SumoMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SumoMVC.Views
{
    public class GameView : IGameView
    {

        //WIDOK TWORZENIE GRACZA 
        public Player CreatePlayer(int playerId)
        {
            Console.Clear();
            Console.SetCursorPosition(40, 5);
            Console.WriteLine("Enter a name for player number " + playerId + ": ");
            string nazwa;
            do
            {
                Console.SetCursorPosition(52, 7);
                Console.ForegroundColor = ConsoleColor.Red;
                nazwa = Console.ReadLine();
                Console.ResetColor();
                
            } while (string.IsNullOrWhiteSpace(nazwa));
            Player player = new Player(nazwa, playerId);
            if (playerId == 1) player.shape = '☻';
            else player.shape = '☺';
            return player;
        }


        //WIDOK WYBIERANIE TRYBU
        public int ChooseGameMode()
        {
            Console.Clear();
            string[] options = { "standard board", "board with static obstacles", "board with random obstacles" };
            string prompt = "\n\n\n\n\n\t\t\t\t\t\tCHOOSE YOUR GAME MODE\n";
            Console.WriteLine(prompt);
            int selectedIndex = 0;
            ConsoleKey keyPressed;
            do
            {
                Console.Clear(); //czyscimy konsole
                Console.WriteLine(prompt);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.Write("\t\t\t\t\t");
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
                        selectedIndex = options.Length-1;
                    }

                }


            } while (keyPressed != ConsoleKey.Enter); //petla sie bedzie wykonywac dopoki nie wcisniemy enter
            Console.Clear();

            return selectedIndex;
        }



        //WIDOK GRY
        //GENEROWANIE PLANSZY:
        public void DisplayBattleFieldBorders(int x0, int y0, int length)
        {

            int sideLength = length;
            int X0 = x0;
            int Y0 = y0;
            for (int i = 1; i <= 2 * sideLength; i++)
            {
                Console.SetCursorPosition(X0 + i, Y0);
                Console.Write("_");
            }
            for (int i = 1; i <= sideLength; i++)
            {
                Console.SetCursorPosition(X0, Y0 + i);
                Console.Write("|");
            }
            for (int i = 1; i <= 2 * sideLength; i++)
            {
                Console.SetCursorPosition(X0 + i, Y0 + sideLength);
                Console.Write("_");
            }
            for (int i = 1; i <= sideLength; i++)
            {
                Console.SetCursorPosition(X0 + 2 * sideLength, Y0 + i);
                Console.Write("|");
            }

        }

               
        //WIDOK KOŃCA GRY
        public void EndGame(GameResult gameResult, int mode)
        {
            Console.Clear();
            string[] options = { "yes", "no" };
            int selectedIndex = 0;
            ConsoleKey keyPressed;
            do
            {
                Console.Clear(); //czyscimy konsole
                Console.SetCursorPosition(45, 7);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The winner is " + gameResult.PlayerName + "!");
                Console.ResetColor();
                Console.SetCursorPosition(37, 9);
                Console.WriteLine("Game duration: " + gameResult.Time.TotalSeconds.ToString("F0") + " seconds");
                Console.SetCursorPosition(35, 9);
                Console.WriteLine(gameResult.PlayerName + ", do you want to save your score?");
                
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
            if (selectedIndex == 0) 
            {
                //zapisanie do rankingu
                string resultFilePath;
                // SaveGameResult(gameMode, player1.Nick, player1.Weight, gameTimer.Elapsed.TotalSeconds.ToString("F0"));
                // Ranking(gameMode);

                if (mode == 0)
                {
                    resultFilePath = "ranking.txt";

                }
                else if (mode == 1)
                {
                    resultFilePath = "rankingStatic.txt";

                }
                else
                {
                    resultFilePath = "rankingRandom.txt";

                }
                using (StreamWriter writer = new StreamWriter(resultFilePath, true))
                {

                    Console.WriteLine(gameResult.Time.TotalSeconds.ToString("F0"));
                    writer.WriteLine(gameResult.PlayerName + "," + gameResult.Score + "," + gameResult.Time.ToString(@"hh\:mm\:ss"));
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(50,9);
                Console.WriteLine("Your score is saved!");
                Console.ResetColor();
                Console.SetCursorPosition(0, 28);
                Console.WriteLine("Press any key to return to the menu.");

                Console.ReadKey(true);
                return;
            }
            else
            {
                return;
            }
            
        }




        //TO ZOSTAJE :
        //INFORMACJE O GRACZACH
        public void DisplayPlayersInformation(Player player1, Player player2)
        {
            Console.SetCursorPosition(0, 9);
            Console.Write(new string(' ', Console.WindowWidth));
            //wyswieltmay dane pierwszego gracza, ustawiamy miejsce od ktorego zaczynamy wypisywac dane
            if(player1.Weight > player2.Weight)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.SetCursorPosition(10,7);
            Console.WriteLine("PLAYER 1");

            Console.SetCursorPosition(10, 8);
            Console.WriteLine("NICK: " + player1.Nick);

            Console.SetCursorPosition(10, 9);
            Console.WriteLine("WEIGHT: " + player1.Weight);

            Console.SetCursorPosition(10, 10);
            Console.WriteLine("SHAPE: " + player1.shape);
            Console.ResetColor();
            if (player2.Weight > player1.Weight)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }           
            //wyswieltmay dane drugiego gracza, ustawiamy miejsce od ktorego zaczynamy wypisywac dane
            Console.SetCursorPosition(85, 7);
            Console.WriteLine("PLAYER 2");

            Console.SetCursorPosition(85, 8);
            Console.WriteLine("NICK: " + player2.Nick);

            Console.SetCursorPosition(85, 9);
            Console.WriteLine("WEIGHT: " + player2.Weight);

            Console.SetCursorPosition(85, 10);
            Console.WriteLine("SHAPE: " + player2.shape);
            Console.ResetColor();

        }

        //PORUSZANIE SIE GRACZY:
        public void deletePlayerFromOldPositionInField(Player player, int sideLength, int y0)
        {
            Console.SetCursorPosition(player.x, player.y);
            if (player.y == y0 + sideLength) Console.Write("_");
            else Console.Write(" ");
        }
        public void setPlayerInNewPositionInField(Player player)
        {
            Console.SetCursorPosition(player.x, player.y);
            Console.Write(player.shape);

        }

        public void DisplayObstacle(int obstacleX, int obstacleY)
        {
            Console.SetCursorPosition(obstacleX, obstacleY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('■');
            Console.ResetColor();
        }
        public void DisplayObstacleVertical(int obstacleX, int obstacleY)
        {
            Console.SetCursorPosition(obstacleX, obstacleY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('█');
            Console.ResetColor();
        }

        //TIMER
        public void DisplayGameTime(Stopwatch gameTimer)
        {
            TimeSpan gameDuration = gameTimer.Elapsed;

            // Clear the timer area at the bottom of the screen
            Console.SetCursorPosition(0, 25);
            Console.Write(new string(' ', Console.WindowWidth));

            // Display the game time
            Console.SetCursorPosition(42, 25);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("GAME TIME: " + gameDuration.TotalSeconds.ToString("F0") + " sec");
            Console.ResetColor();
        }

        //DISPLAY FOOD
        public void DisplayFood(Food food)
        {
            Console.SetCursorPosition(food.x, food.y);
            Console.Write('F');
        }

        public void DisplayStartGame()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string prompt = @"
                                                    ____  
                                                   |___ \ 
                                                     __) |
                                                    |__ < 
                                                    ___) |
                                                   |____/ ";
            Console.Clear();
            Console.SetCursorPosition(57,7);
            Console.WriteLine(prompt);
            Thread.Sleep(1000);
            Console.Clear();
            prompt = @"                       
                                                     ___  
                                                    |__ \ 
                                                       ) |
                                                      / / 
                                                     / /_ 
                                                    |____|
";
            Console.SetCursorPosition(57, 7);
            Console.WriteLine(prompt);
            Thread.Sleep(1000);
            Console.Clear();
            prompt = @"                       
                                                      __ 
                                                     /_ |
                                                      | |
                                                      | |
                                                      | |
                                                      |_|
";
            Console.SetCursorPosition(57, 7);
            Console.WriteLine(prompt);
            Thread.Sleep(1000);
            Console.Clear();
            Console.SetCursorPosition(57, 7);
            prompt = @"
                                     _____ _______       _____ _______ 
                                    / ____|__   __|/\   |  __ \__   __|
                                   | (___    | |  /  \  | |__) | | |   
                                    \___ \   | | / /\ \ |  _  /  | |   
                                    ____) |  | |/ ____ \| | \ \  | |   
                                   |_____/   |_/_/    \_\_|  \_\ |_|   
                                     
                                     
";
            Console.WriteLine(prompt);
            Thread.Sleep(1000);
            Console.ResetColor();
            Console.Clear();
        }
        public void DisplayEndGame()
        {
            string prompt = @"
                                                  _  __       ____      
                                                 | |/ /      / __ \     
                                                 | ' /      | |  | |    
                                                 |  <       | |  | |    
                                                 | . \   _  | |__| |  _ 
                                                 |_|\_\ (_)  \____/  (_)
                                            
";
            Console.Clear();
            Console.ForegroundColor= ConsoleColor.DarkRed;
            Console.SetCursorPosition(50, 7);
            Console.WriteLine(prompt);
            Thread.Sleep(2500);
            Console.ResetColor();
        }
    }
}

