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
            Console.SetCursorPosition(40, 7);
            Console.WriteLine("Enter a name for player number " + playerId + ": ");
            string nazwa;
            do
            {
                Console.SetCursorPosition(52, 8);
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
            string[] options = { "standard", "static", "random" };
            string prompt = "\n\n\n\n\n\n\n\t\t\t\t\t\tChoose game mode ";
            Console.WriteLine(prompt);
            int selectedIndex = 0;
            ConsoleKey keyPressed;
            do
            {
                Console.Clear(); //czyscimy konsole
                Console.WriteLine(prompt);
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
                        selectedIndex = options.Length-1;
                    }

                }


            } while (keyPressed != ConsoleKey.Enter); //petla sie bedzie wykonywac dopoki nie wcisniemy enter
            Console.Clear();

            return selectedIndex;
        }



        //WIDOK GRY
        //GENEROWANIE PLANSZY:
        public void DisplayBattleFieldBorders()
        {

            int sideLength = 10;
            int X0 = 10;
            int Y0 = 8;
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

        


//FOOD GENERATOR
        
        //DISPLAY OBSTALCE

       
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
                Console.SetCursorPosition(43, 7);
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
                Console.WriteLine("Your score is saved");
                
                Console.WriteLine("\nPress any key to return to the menu.");

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
            Console.SetCursorPosition(0, 2);
            Console.Write(new string(' ', Console.WindowWidth));
            //wyswieltmay dane pierwszego gracza, ustawiamy miejsce od ktorego zaczynamy wypisywac dane

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Player 1");

            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Nick: " + player1.Nick);

            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Weight: " + player1.Weight);

            Console.SetCursorPosition(0, 3);
            Console.WriteLine("Shape: " + player1.shape);

            //wyswieltmay dane drugiego gracza, ustawiamy miejsce od ktorego zaczynamy wypisywac dane
            Console.SetCursorPosition(60, 0);
            Console.WriteLine("Player 2");

            Console.SetCursorPosition(60, 1);
            Console.WriteLine("Nick: " + player2.Nick);

            Console.SetCursorPosition(60, 2);
            Console.WriteLine("Weight: " + player2.Weight);

            Console.SetCursorPosition(60, 3);
            Console.WriteLine("Shape: " + player2.shape);

            Console.SetCursorPosition(30, 0);
            if (player1.Weight > player2.Weight)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Currently winning:");
                Console.SetCursorPosition(30, 1);
                Console.WriteLine(player1.Nick);
                Console.ResetColor();
            }
            else if (player2.Weight > player1.Weight)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Currently winning:");
                Console.SetCursorPosition(30, 1);
                Console.WriteLine(player2.Nick);
                Console.ResetColor();
            }


        }

        //PORUSZANIE SIE GRACZY:
        public void deletePlayerFromOldPositionInField(Player player, int sideLength)
        {
            Console.SetCursorPosition(player.x, player.y);
            if (player.y == 8 + sideLength) Console.Write("_");
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

        //TIMER
        public void DisplayGameTime(Stopwatch gameTimer)
        {
            TimeSpan gameDuration = gameTimer.Elapsed;

            // Clear the timer area at the bottom of the screen
            Console.SetCursorPosition(0, Console.WindowHeight - 5);
            Console.Write(new string(' ', Console.WindowWidth));

            // Display the game time
            Console.SetCursorPosition(30, Console.WindowHeight - 5);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Game Time: " + gameDuration.TotalSeconds.ToString("F0") + " sec");
            Console.ResetColor();
        }

        //DISPLAY FOOD
        public void DisplayFood(Food food)
        {
            Console.SetCursorPosition(food.x, food.y);
            Console.Write('F');
        }



    }
}

