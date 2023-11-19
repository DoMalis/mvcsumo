using SumoMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SumoMVC.Views
{
    public class GameView : IGameView
    {
        // private Stopwatch gameTimer = new Stopwatch();


        //WIDOK TWORZENIE GRACZA 
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


        //WIDOK WYBIERANIE TRYBU
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
            Console.Clear();

            return selectedIndex;
        }



        //WIDOK GRY
        //GENEROWANIE PLANSZY:
        public bool[,] CreateBattleField(IGameModel gameModel)
        {
            //DisplayPlayersInformation(gameModel.Player1, gameModel.Player2); //wyswietlamy informacje o zawodnikach

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
                return null;

            }
            else if (gameModel.Mode == 1)
            {
                return CreateObstacles(sideLength);

            }
            else if (gameModel.Mode == 2)
            {
                return CreateRandomObstacles(gameModel.Player1, gameModel.Player2, sideLength);


            }

            //gameTimer.Restart();
            //GameLogic(gameModel.Player1, gameModel.Player2, battlefieldsize, gameMode); //rozpoczyna się gra
            //Console.Clear();
            //Console.ReadKey(true);
            //RunMainMenu();

            Console.ReadKey(true);
            return null;

        }

        public bool[,] CreateObstacles(int sideLength)
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
            return obstacleGrid;
        }
        private bool[,] CreateRandomObstacles(Player player1, Player player2, int sideLength)
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
            return obstacleGrid;
        }
        private bool CheckCollision(int x, int y, bool[,] obstacleGrid)
        {
            // sprawdzanie czy nowa pozycja koliduje z przeszkodą
            return obstacleGrid[x - 11, y - 9];
        }

        //DANE GRACZY NA GÓRZE EKRANU:
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
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("Player 2");

            Console.SetCursorPosition(50, 1);
            Console.WriteLine("Nick: " + player2.Nick);

            Console.SetCursorPosition(50, 2);
            Console.WriteLine("Weight: " + player2.Weight);

            Console.SetCursorPosition(50, 3);
            Console.WriteLine("Shape: " + player2.shape);


        }

        //PORUSZANIE SIE GRACZY:
        private void deletePlayerFromOldPositionInField(Player player, int sideLength)
        {
            Console.SetCursorPosition(player.x, player.y);
            if (player.y == 8 + sideLength) Console.Write("_");
            else Console.Write(" ");
        }
        private void setPlayerInNewPositionInField(Player player)
        {
            Console.SetCursorPosition(player.x, player.y);
            Console.Write(player.shape);

        }
        private void MovingStandard(Player player1, Player player2, ConsoleKey keyPressed, int sideLength)
        {
            //obsluga klawiszy 1 zawodnika, po każdym wcisnieciu strzałki kasujemy pozycję gracza na planszy i stawiamy w nowym miejscu
            if (keyPressed == ConsoleKey.A && player1.x > 10 + 1)
            {
                deletePlayerFromOldPositionInField(player1, sideLength);
                player1.Weight -= 1;
                player1.x--;
            }

            if (keyPressed == ConsoleKey.W && player1.y > 8 + 1)
            {
                deletePlayerFromOldPositionInField(player1, sideLength);
                player1.Weight -= 1;
                player1.y--;
            }
            if (keyPressed == ConsoleKey.D && player1.x < 10 + 2 * sideLength - 1)
            {
                deletePlayerFromOldPositionInField(player1, sideLength);
                player1.Weight -= 1;
                player1.x++;
            }
            if (keyPressed == ConsoleKey.S && player1.y < 8 + sideLength)
            {
                deletePlayerFromOldPositionInField(player1, sideLength);
                player1.Weight -= 1;
                player1.y++;

            }

            //obsluga klawiszy 2 zawodnika
            if (keyPressed == ConsoleKey.LeftArrow && player2.x > 10 + 1)
            {
                deletePlayerFromOldPositionInField(player2, sideLength);
                //player2.Weight -= 1;
                player2.x--;
            }
            if (keyPressed == ConsoleKey.UpArrow && player2.y > 8 + 1)
            {

                deletePlayerFromOldPositionInField(player2, sideLength);
                //player2.Weight -= 1;
                player2.y--;
            }
            if (keyPressed == ConsoleKey.RightArrow && player2.x < 10 + 2 * sideLength - 1)
            {
                deletePlayerFromOldPositionInField(player2, sideLength);
                //player2.Weight -= 1;
                player2.x++;
            }
            if (keyPressed == ConsoleKey.DownArrow && player2.y < 8 + sideLength)
            {
                deletePlayerFromOldPositionInField(player2, sideLength);
                //player2.Weight -= 1;
                player2.y++;
            }

        }
        private void MovingWithObstacles(Player player1, Player player2, ConsoleKey keyPressed, int sideLength, bool[,] obstacleGrid)
        {
            //obsluga klawiszy 1 zawodnika, po każdym wcisnieciu strzałki kasujemy pozycję gracza na planszy i stawiamy w nowym miejscu
            if (keyPressed == ConsoleKey.A && player1.x > 10 + 1)
            {
                if (!CheckCollision(player1.x - 1, player1.y, obstacleGrid))
                {
                    deletePlayerFromOldPositionInField(player1, sideLength);
                    player1.Weight -= 1;
                    player1.x--;
                }
            }

            if (keyPressed == ConsoleKey.W && player1.y > 8 + 1)
            {
                if (!CheckCollision(player1.x, player1.y - 1, obstacleGrid))
                {
                    deletePlayerFromOldPositionInField(player1, sideLength);
                    player1.Weight -= 1;
                    player1.y--;
                }
            }
            if (keyPressed == ConsoleKey.D && player1.x < 10 + 2 * sideLength - 1)
            {
                if (!CheckCollision(player1.x + 1, player1.y, obstacleGrid))
                {
                    deletePlayerFromOldPositionInField(player1, sideLength);
                    player1.Weight -= 1;
                    player1.x++;
                }
            }
            if (keyPressed == ConsoleKey.S && player1.y < 8 + sideLength)
            {
                if (!CheckCollision(player1.x, player1.y + 1, obstacleGrid))
                {
                    deletePlayerFromOldPositionInField(player1, sideLength);
                    player1.Weight -= 1;
                    player1.y++;
                }

            }

            //obsluga klawiszy 2 zawodnika
            if (keyPressed == ConsoleKey.LeftArrow && player2.x > 10 + 1)
            {
                if (!CheckCollision(player2.x - 1, player2.y, obstacleGrid))
                {
                    deletePlayerFromOldPositionInField(player2, sideLength);
                    player2.Weight -= 1;
                    player2.x--;
                }
            }
            if (keyPressed == ConsoleKey.UpArrow && player2.y > 8 + 1)
            {
                if (!CheckCollision(player2.x, player2.y - 1, obstacleGrid))
                {
                    deletePlayerFromOldPositionInField(player2, sideLength);
                    player2.Weight -= 1;
                    player2.y--;
                }
            }
            if (keyPressed == ConsoleKey.RightArrow && player2.x < 10 + 2 * sideLength - 1)
            {
                if (!CheckCollision(player2.x + 1, player2.y, obstacleGrid))
                {
                    deletePlayerFromOldPositionInField(player2, sideLength);
                    player2.Weight -= 1;
                    player2.x++;
                }
            }
            if (keyPressed == ConsoleKey.DownArrow && player2.y < 8 + sideLength)
            {
                if (!CheckCollision(player2.x, player2.y + 1, obstacleGrid))
                {
                    deletePlayerFromOldPositionInField(player2, sideLength);
                    player2.Weight -= 1;
                    player2.y++;
                }
            }

        }

        //LOGIKA DZIAŁANIA GRY
        public GameResult GameLogic(IGameModel gameModel)
        {
            //Stopwatch gameTimer = new Stopwatch();
            //System.Timers.Timer timer;
            int sideLength = 10;
            gameModel.Player1.x = 11;
            gameModel.Player1.y = 9;
            gameModel.Player2.x = 10 + 2 * sideLength - 1;
            gameModel.Player2.y = 8 + sideLength;

            Random random = new Random();
            int randNumber;
            Food food = new Food();
            ConsoleKey keyPressed;
            //gameModel.gameTimer.Start();
            //timer = new System.Timers.Timer(1000);// Timer będzie wyzwalać zdarzenie co 1 sekundę
            //timer.Elapsed += (sender, e) => DisplayGameTime();
            //timer.Start();
            do
            {
                //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
                //STEROWANIE ZAWODNIKAMI
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); //wczytujemy informacje o wcisnietym przycisku
                keyPressed = keyInfo.Key; //przypisujemy wartosc wcisnietego przycisku
                if (gameModel.Mode == 0)
                {
                    MovingStandard(gameModel.Player1, gameModel.Player2, keyPressed, sideLength);
                }
                else
                {
                    MovingWithObstacles(gameModel.Player1, gameModel.Player2, keyPressed, sideLength, gameModel.obstacleGrid);
                }


                //ustawiamy graczy w ich nowych pozycjach 
                setPlayerInNewPositionInField(gameModel.Player2);
                setPlayerInNewPositionInField(gameModel.Player1);




                //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
                //GENEROWANIE JEDZONKA ABY NAKARMIĆ NASZE SUMO GLODOMORKI

                randNumber = random.Next(1, 100);
                if (randNumber % 5 == 0 && food.Eaten == true)  //jeśli jedzonko zostało już zjedzone, to moz ppowstać nowe
                {
                    bool foodGenerated = false;

                    while (!foodGenerated)
                    {
                        food.kg = random.Next(1, 40);
                        food.x = random.Next(11, 10 + 2 * sideLength - 1);
                        food.y = random.Next(9, 8 + sideLength);
                        if (gameModel.Mode == 0)
                        {
                            Console.SetCursorPosition(food.x, food.y);
                            Console.Write('F');
                            food.Eaten = false;
                            foodGenerated = true;
                        }
                        else//warunek ze jedzenie nie bedzie sie genrowac na przeszkodach
                        {
                            if ((!gameModel.obstacleGrid[food.x - 11, food.y - 9]) &&
                            (food.x != gameModel.Player1.x || food.y != gameModel.Player1.y) &&
                            (food.x != gameModel.Player2.x || food.y != gameModel.Player2.y)
                            )
                            {
                                Console.SetCursorPosition(food.x, food.y);
                                Console.Write('F');
                                food.Eaten = false;
                                foodGenerated = true;
                            }
                        }

                    }

                }



                //-=-=-=-=--=-=-=-=-=-=-=-=-=-=-=--==-=-=-
                //Co sie stanie jak glodomorki zjedzą ??

                if ((gameModel.Player1.x == food.x && gameModel.Player1.y == food.y))
                {
                    food.Eaten = true;
                    gameModel.Player1.Weight += food.kg;

                }

                if (gameModel.Player2.x == food.x && gameModel.Player2.y == food.y)
                {
                    food.Eaten = true;
                    gameModel.Player2.Weight += food.kg;
                }

                DisplayPlayersInformation(gameModel.Player1, gameModel.Player2);


                if (gameModel.Player1.x==gameModel.Player2.x && gameModel.Player1.y==gameModel.Player2.y && gameModel.Player1.Weight!=gameModel.Player2.Weight)
                    break;
                if (gameModel.Player1.Weight<=0 || gameModel.Player2.Weight<=0)
                    break;


            } while (true);

            TimeSpan time = TimeSpan.Parse("00:00:10");
            return new GameResult((gameModel.Player1.Weight>gameModel.Player2.Weight) ? gameModel.Player1 : gameModel.Player2, time);


        }


        //WIDOK KOŃCA GRY
        public void EndGame(GameResult gameResult, int mode)
        {
            Console.Clear();

            Console.WriteLine("The winner is " + gameResult.PlayerName + "!");
            //Console.WriteLine("Game duration: " + gameTimer.Elapsed.TotalSeconds.ToString("F0") + " seconds");
            Console.WriteLine(gameResult.PlayerName + ", do you want to save your score?(Y/N)");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)//jesli tak
                {
                    //zapisanie do rankingu
                    string resultFilePath;
                    // SaveGameResult(gameMode, player1.Nick, player1.Weight, gameTimer.Elapsed.TotalSeconds.ToString("F0"));
                    // Ranking(gameMode);
                    if (mode==0)
                    {
                        resultFilePath = "ranking.txt";

                    }
                    else if (mode ==1)
                    {
                        resultFilePath = "rankingStatic.txt";

                    }
                    else
                    {
                        resultFilePath = "rankingRandom.txt";

                    }
                    using (StreamWriter writer = new StreamWriter(resultFilePath, true))
                    {
                        writer.WriteLine(gameResult.PlayerName + "," + gameResult.Score + "," + gameResult.Time);
                    }
                    Console.WriteLine("Your score is saved");
                    Console.WriteLine("\nPress any key to return to the menu.");

                    Console.ReadKey(true);
                    return;
                }
                else if (key.Key == ConsoleKey.N)//jesli nie
                {
                    return;
                }
            } while (key.Key != ConsoleKey.Y || key.Key != ConsoleKey.N);

        }










        /*private void DisplayGameTime()
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

