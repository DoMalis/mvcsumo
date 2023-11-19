using SumoMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Views
{
    public class GameView : IGameView
    {
        // private Stopwatch gameTimer = new Stopwatch();

        public Player CreatePlayer(int playerId)
        {
            Console.Clear();
            Console.WriteLine("Wprowadź nazwę " + playerId + " gracza: ");
            string nazwa;
            do
            {
                nazwa = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nazwa))
                {
                    Console.WriteLine("Nazwa użytkownika nie może być pusta. Wprowadź ponownie: ");
                }

            } while (string.IsNullOrWhiteSpace(nazwa));
            Player player = new Player(nazwa, playerId);
            if (playerId == 1) player.shape = '+';
            else player.shape = 'o';
            return player;
        }

        public int ChooseGameMode()
        {
            Console.Clear();
            string[] options = { "standard", "static", "random" };
            string prompt = "Choose game mode ";
            Console.WriteLine(prompt);
            int selectedIndex = 0;
            ConsoleKey keyPressed;
            do
            {
                Console.Clear(); //czyscimy konsole
                Console.WriteLine(prompt);
                for (int i = 0; i < options.Length; i++)
                {
                    string prefix;
                    if (i == selectedIndex)
                    {
                        prefix = "*";
                    }
                    else
                    {
                        prefix = " ";
                    }
                    Console.WriteLine($"{prefix} << {options[i]} >>");
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

            return selectedIndex;
        }

        public void CreateBattleField(IGameModel gameModel)
        {
            //DisplayPlayersInformation(player1, player2); //wyswietlamy informacje o zawodnikach

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

            //obstacles
            if (gameModel.Mode == 0)
            {
                //nie ma przeszkod

            }
            else if (gameModel.Mode == 1)
            {
                CreateObstacles(gameModel.Player1, gameModel.Player2, sideLength);

            }
            else if (gameModel.Mode == 2)
            {
                CreateRandomObstacles(gameModel.Player1, gameModel.Player2, sideLength);


            }
            else
            {
                return;
            }
            //gameTimer.Restart();
            //GameLogic(player1, player2, battlefieldsize, gameMode); //rozpoczyna się gra
            //Console.Clear();
            //Console.ReadKey(true);
            //RunMainMenu();

            Console.ReadKey(true);
        }




        public void CreateObstacles(Player player1, Player player2, int sideLength)
        {
            bool[,] obstacleGrid = new bool[2 * sideLength, sideLength]; // Inicjalizacja tablicy przeszkód

            // Stałe współrzędne przeszkód
            int obstacleStartX = 15;
            int obstacleStartY = 10;

            Console.SetCursorPosition(0, 0); // Kursor na początek planszy

            for (int i = 0; i < 5; i++)
            {
                int obstacleX = obstacleStartX + i;
                int obstacleY = obstacleStartY;

                obstacleGrid[obstacleX - 11, obstacleY - 9] = true; // Ustawienie komórki jako zajętą

                Console.SetCursorPosition(obstacleX, obstacleY);
                Console.Write('X');
            }

            obstacleStartX = 17;
            obstacleStartY = 17;
            for (int i = 0; i < 5; i++)
            {
                int obstacleX = obstacleStartX + i;
                int obstacleY = obstacleStartY;

                obstacleGrid[obstacleX - 11, obstacleY - 9] = true; // Ustawienie komórki jako zajętą

                Console.SetCursorPosition(obstacleX, obstacleY);
                Console.Write('X');
            }

            obstacleStartX = 20;
            obstacleStartY = 13;
            for (int i = 0; i < 5; i++)
            {
                int obstacleX = obstacleStartX + i;
                int obstacleY = obstacleStartY;

                obstacleGrid[obstacleX - 11, obstacleY - 9] = true; // Ustawienie komórki jako zajętą

                Console.SetCursorPosition(obstacleX, obstacleY);
                Console.Write('X');
            }

            obstacleStartX = 13;
            obstacleStartY = 14;
            for (int i = 0; i < 3; i++)
            {
                int obstacleY = obstacleStartY + i;
                int obstacleX = obstacleStartX;

                obstacleGrid[obstacleX - 11, obstacleY - 9] = true; // ustawienie komórki jako zajętą

                Console.SetCursorPosition(obstacleX, obstacleY);
                Console.Write('X');
            }
        }
        private void CreateRandomObstacles(Player player1, Player player2, int sideLength)
        {
            bool[,] obstacleGrid = new bool[2 * sideLength, sideLength]; // inicjalizacja tablicy przeszkód

            Console.SetCursorPosition(0, 0); // kursor na początek planszy

            Random random = new Random();

            for (int i = 0; i < 8; i++)
            {
                int obstacleX = random.Next(11, 10 + 2 * sideLength - 1);
                int obstacleY = random.Next(9, 8 + sideLength);

                // sprawdzanie czy nowa pozycja przeszkody koliduje z innymi przeszkodami lub graczami
                if ((!obstacleGrid[obstacleX - 11, obstacleY - 9]) &&
                    (obstacleX != player1.x || obstacleY != player1.y) &&
                    (obstacleX != player2.x || obstacleY != player2.y))
                {
                    obstacleGrid[obstacleX - 11, obstacleY - 9] = true; // ustawienie komórki jako zajętą

                    Console.SetCursorPosition(obstacleX, obstacleY);
                    Console.Write('X');
                }
                else
                {
                    i--; // jeśli pozycja koliduje powtarzamy iteracje
                }
            }

        }

        //jezu to chyba nie bedzie tutaj ja pierdole
        /*private void DisplayPlayersInformation(Player player1, Player player2)
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
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("Player 2");

            Console.SetCursorPosition(50, 1);
            Console.WriteLine("Nick: " + player2.Nick);

            Console.SetCursorPosition(50, 2);
            Console.WriteLine("Weight: " + player2.Weight);

            Console.SetCursorPosition(50, 3);
            Console.WriteLine("Shape: " + player2.shape);


        }
        private void DisplayGameTime()
        {
            TimeSpan gameDuration = gameTimer.Elapsed;

            // Clear the timer area at the bottom of the screen
            Console.SetCursorPosition(0, Console.WindowHeight - 5);
            Console.Write(new string(' ', Console.WindowWidth));

            // Display the game time
            Console.SetCursorPosition(0, Console.WindowHeight - 5);
            Console.Write("Game Time: " + gameDuration.TotalSeconds.ToString("F0") + " sec");
        }*/

    }
}

