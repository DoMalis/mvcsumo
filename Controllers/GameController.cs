using SumoMVC.Models;
using SumoMVC.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SumoMVC.Controllers
{
    public class GameController
    {
        public IGameView gameView;
        public IGameModel gameModel;

        public GameController(IGameView gameView, IGameModel gameModel)
        {
            this.gameView = gameView;
            this.gameModel = gameModel;
        }
        
        public void CreatePlayers() //to oodzielny widok
        {
            gameModel.Player1=gameView.CreatePlayer(1);
            gameModel.Player2=gameView.CreatePlayer(2);

        }
        
        public void ChooseGameMode() //to oddzileny widok
        {
            gameModel.Mode = gameView.ChooseGameMode();
        }

        public void StartGame() 
        {
            CreatePlayers();
            ChooseGameMode();
            gameView.DisplayStartGame();
            CreateGameView();
            GameLogic();
            gameView.DisplayEndGame();
            End();
        }


       public void CreateGameView()
       {
            gameView.DisplayPlayersInformation(gameModel.Player1, gameModel.Player2);
            gameView.DisplayBattleFieldBorders(gameModel.X0,gameModel.Y0,gameModel.SideLength);
            gameModel.ObstacleGrid=GenerateObstacles(gameModel.SideLength);
       }


        public void End()
        {
            gameView.EndGame(gameModel.GameResult,gameModel.Mode);
        }


        public void GameLogic()
        {
            Stopwatch gameTimer = new Stopwatch();
            System.Timers.Timer timer;

            /* int sideLength = 10;
            gameModel.Player1.x = 11;
            gameModel.Player1.y = 9;
            gameModel.Player2.x = 10 + 2 * sideLength - 1;
            gameModel.Player2.y = 8 + sideLength;*/
            int sideLength = gameModel.SideLength;
            gameModel.Player1.x = gameModel.X0+1;
            gameModel.Player1.y = gameModel.Y0+1;
            gameModel.Player2.x = gameModel.X0 + 2 * sideLength - 1;
            gameModel.Player2.y = gameModel.Y0 + sideLength;
            Random random = new Random();
            int randNumber;
            Food food = new Food();
            ConsoleKey keyPressed;
            gameTimer.Restart();
            timer = new System.Timers.Timer(1000);// Timer będzie wyzwalać zdarzenie co 1 sekundę
            timer.Elapsed += (sender, e) => gameView.DisplayGameTime(gameTimer);
            timer.Start();
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
                    MovingWithObstacles(gameModel.Player1, gameModel.Player2, keyPressed, sideLength, gameModel.ObstacleGrid);
                }


                //ustawiamy graczy w ich nowych pozycjach 
                gameView.setPlayerInNewPositionInField(gameModel.Player2);
                gameView.setPlayerInNewPositionInField(gameModel.Player1);




                //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
                //GENEROWANIE JEDZONKA ABY NAKARMIĆ NASZE SUMO GLODOMORKI

                randNumber = random.Next(1, 100);
                if (randNumber % 5 == 0 && food.Eaten == true)  //jeśli jedzonko zostało już zjedzone, to moz ppowstać nowe
                {
                    food=FoodGenerator(gameModel.SideLength);

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

                gameView.DisplayPlayersInformation(gameModel.Player1, gameModel.Player2);

                //kończenie gry:
                if (gameModel.Player1.x==gameModel.Player2.x && gameModel.Player1.y==gameModel.Player2.y && gameModel.Player1.Weight!=gameModel.Player2.Weight)
                    break;
                if (gameModel.Player1.Weight<=0 || gameModel.Player2.Weight<=0)
                    break;


            } while (true);

            timer.Stop();
            gameTimer.Stop();

            TimeSpan time = gameTimer.Elapsed;
            gameModel.GameResult= new GameResult((gameModel.Player1.Weight>gameModel.Player2.Weight) ? gameModel.Player1 : gameModel.Player2, time);
        }
      
        public bool CheckCollision(int x, int y, bool[,] obstacleGrid)
        {
            // sprawdzanie czy nowa pozycja koliduje z przeszkodą
            return obstacleGrid[x - (gameModel.X0+1), y - (gameModel.Y0+1)];
        }



        //OBSŁUGA PORUSZANIA SIĘ
        public void MovingStandard(Player player1, Player player2, ConsoleKey keyPressed, int sideLength)
        {
            //obsluga klawiszy 1 zawodnika, po każdym wcisnieciu strzałki kasujemy pozycję gracza na planszy i stawiamy w nowym miejscu
            if (keyPressed == ConsoleKey.A && player1.x > gameModel.X0 + 1)
            {
                    gameView.deletePlayerFromOldPositionInField(player1, sideLength, gameModel.Y0);
                player1.Weight -= 1;
                player1.x--;
            }

            if (keyPressed == ConsoleKey.W && player1.y > gameModel.Y0 + 1)
            {
                    gameView.deletePlayerFromOldPositionInField(player1, sideLength, gameModel.Y0);
                player1.Weight -= 1;
                player1.y--;
            }
            if (keyPressed == ConsoleKey.D && player1.x < gameModel.X0 + 2 * sideLength - 1)
            {
                    gameView.deletePlayerFromOldPositionInField(player1, sideLength, gameModel.Y0);
                player1.Weight -= 1;
                player1.x++;
            }
            if (keyPressed == ConsoleKey.S && player1.y < gameModel.Y0 + sideLength)
            {
                    gameView.deletePlayerFromOldPositionInField(player1, sideLength, gameModel.Y0);
                player1.Weight -= 1;
                player1.y++;

            }

            //obsluga klawiszy 2 zawodnika
            if (keyPressed == ConsoleKey.LeftArrow && player2.x > gameModel.X0 + 1)
            {
                    gameView.deletePlayerFromOldPositionInField(player2, sideLength, gameModel.Y0);
                player2.Weight -= 1;
                player2.x--;
            }
            if (keyPressed == ConsoleKey.UpArrow && player2.y > gameModel.Y0 + 1)
            {

                    gameView.deletePlayerFromOldPositionInField(player2, sideLength, gameModel.Y0);
                player2.Weight -= 1;
                player2.y--;
            }
            if (keyPressed == ConsoleKey.RightArrow && player2.x < gameModel.X0 + 2 * sideLength - 1)
            {
                    gameView.deletePlayerFromOldPositionInField(player2, sideLength, gameModel.Y0);
                player2.Weight -= 1;
                player2.x++;
            }
            if (keyPressed == ConsoleKey.DownArrow && player2.y < gameModel.Y0 + sideLength)
            {
                    gameView.deletePlayerFromOldPositionInField(player2, sideLength, gameModel.Y0);
                player2.Weight -= 1;
                player2.y++;
            }

        }
        public void MovingWithObstacles(Player player1, Player player2, ConsoleKey keyPressed, int sideLength, bool[,] obstacleGrid)
        {
            //obsluga klawiszy 1 zawodnika, po każdym wcisnieciu strzałki kasujemy pozycję gracza na planszy i stawiamy w nowym miejscu
            if (keyPressed == ConsoleKey.A && player1.x > gameModel.X0 + 1)
            {
                if (!CheckCollision(player1.x - 1, player1.y, obstacleGrid))
                {
                    gameView.deletePlayerFromOldPositionInField(player1, sideLength, gameModel.Y0);
                    player1.Weight -= 1;
                    player1.x--;
                }
            }

            if (keyPressed == ConsoleKey.W && player1.y > gameModel.Y0 + 1)
            {
                if (!CheckCollision(player1.x, player1.y - 1, obstacleGrid))
                {
                    gameView.deletePlayerFromOldPositionInField(player1, sideLength, gameModel.Y0);
                    player1.Weight -= 1;
                    player1.y--;
                }
            }
            if (keyPressed == ConsoleKey.D && player1.x < gameModel.X0 + 2 * sideLength - 1)
            {
                if (!CheckCollision(player1.x + 1, player1.y, obstacleGrid))
                {
                    gameView.deletePlayerFromOldPositionInField(player1, sideLength, gameModel.Y0);
                    player1.Weight -= 1;
                    player1.x++;
                }
            }
            if (keyPressed == ConsoleKey.S && player1.y < gameModel.Y0 + sideLength)
            {
                if (!CheckCollision(player1.x, player1.y + 1, obstacleGrid))
                {
                    gameView.deletePlayerFromOldPositionInField(player1, sideLength, gameModel.Y0);
                    player1.Weight -= 1;
                    player1.y++;
                }

            }

            //obsluga klawiszy 2 zawodnika
            if (keyPressed == ConsoleKey.LeftArrow && player2.x > gameModel.X0 + 1)
            {
                if (!CheckCollision(player2.x - 1, player2.y, obstacleGrid))
                {
                        gameView.deletePlayerFromOldPositionInField(player2, sideLength, gameModel.Y0);
                    player2.Weight -= 1;
                    player2.x--;
                }
            }
            if (keyPressed == ConsoleKey.UpArrow && player2.y > gameModel.Y0 + 1)
            {
                if (!CheckCollision(player2.x, player2.y - 1, obstacleGrid))
                {
                        gameView.deletePlayerFromOldPositionInField(player2, sideLength, gameModel.Y0);
                    player2.Weight -= 1;
                    player2.y--;
                }
            }
            if (keyPressed == ConsoleKey.RightArrow && player2.x < gameModel.X0 + 2 * sideLength - 1)
            {
                if (!CheckCollision(player2.x + 1, player2.y, obstacleGrid))
                {
                        gameView.deletePlayerFromOldPositionInField(player2, sideLength, gameModel.Y0);
                    player2.Weight -= 1;
                    player2.x++;
                }
            }
            if (keyPressed == ConsoleKey.DownArrow && player2.y < gameModel.Y0 + sideLength)
            {
                if (!CheckCollision(player2.x, player2.y + 1, obstacleGrid))
                {
                        gameView.deletePlayerFromOldPositionInField(player2, sideLength, gameModel.Y0);
                    player2.Weight -= 1;
                    player2.y++;
                }
            }

        }


        //JEDZONKO
        public Food FoodGenerator(int sideLength)
        {
            Food food = new Food();
            bool foodGenerated = false;
            Random random = new Random();
            int randNumber;

            while (!foodGenerated)
            {
                food.kg = random.Next(1, 40);
                food.x = random.Next((gameModel.X0+1), gameModel.X0 + 2 * sideLength - 1);
                food.y = random.Next((gameModel.Y0+1), gameModel.Y0 + sideLength);
                if (gameModel.Mode == 0)
                {
                    food.Eaten = false;
                    foodGenerated = true;
                    gameView.DisplayFood(food);
                }
                else//warunek ze jedzenie nie bedzie sie genrowac na przeszkodach
                {
                    if ((!CheckCollision(food.x,food.y,gameModel.ObstacleGrid)) &&
                    (food.x != gameModel.Player1.x || food.y != gameModel.Player1.y) &&
                    (food.x != gameModel.Player2.x || food.y != gameModel.Player2.y)
                    )
                    {
                        food.Eaten = false;
                        foodGenerated = true;
                        gameView.DisplayFood(food);
                    }
                }
            }

            return food;
        }

        //PRZESZKODY
        //wywoływanie odpowiedniej metody tworzenia przeszkód
        public bool[,] GenerateObstacles(int sideLength)
        {
            if (gameModel.Mode == 0)
            {
                return null;

            }
            else if (gameModel.Mode == 1)
            {
                return CreateObstacles(gameModel.SideLength);

            }
            else if (gameModel.Mode == 2)
            {
                return CreateRandomObstacles(gameModel.Player1, gameModel.Player2, gameModel.SideLength);
            }
            return null;

        }

        //tworzenie przeszkód
        public bool[,] CreateRandomObstacles(Player player1, Player player2, int sideLength)
        {
            bool[,] obstacleGrid = new bool[2 * sideLength, sideLength]; // inicjalizacja tablicy przeszkód

            Random random = new Random();

            for (int i = 0; i < 8; i++)
            {
                int obstacleX = random.Next(gameModel.X0+1, gameModel.X0 + 2 * sideLength - 1);
                int obstacleY = random.Next(gameModel.Y0+1, gameModel.Y0 + sideLength);

                // sprawdzanie czy nowa pozycja przeszkody koliduje z innymi przeszkodami lub graczami
                if (!CheckCollision(obstacleX, obstacleY, obstacleGrid) &&
                    (obstacleX != player1.x || obstacleY != player1.y) &&
                    (obstacleX != player2.x || obstacleY != player2.y))
                {
                    obstacleGrid[obstacleX - (gameModel.X0+1), obstacleY - (gameModel.Y0+1)] = true; // ustawienie komórki jako zajętą
                    gameView.DisplayObstacle(obstacleX, obstacleY);

                }
                else
                {
                    i--; // jeśli pozycja koliduje powtarzamy iteracje
                }
            }
            return obstacleGrid;
        }
        public bool[,] CreateObstacles(int sideLength)
        {
            bool[,] obstacleGrid = new bool[2 * sideLength, sideLength]; // Inicjalizacja tablicy przeszkód

            // Stałe współrzędne przeszkód
            int obstacleStartX = 43;
            int obstacleStartY = 13;

            for (int i = 0; i < 5; i++)
            {
                int obstacleX = obstacleStartX + i;
                int obstacleY = obstacleStartY;

                obstacleGrid[obstacleX - (gameModel.X0+1), obstacleY - (gameModel.Y0+1)] = true; // Ustawienie komórki jako zajętą

                gameView.DisplayObstacle(obstacleX, obstacleY);
            }

            obstacleStartX = 49;
            obstacleStartY = 19;
            for (int i = 0; i < 4; i++)
            {
                int obstacleX = obstacleStartX + i;
                int obstacleY = obstacleStartY;

                obstacleGrid[obstacleX - (gameModel.X0+1), obstacleY - (gameModel.Y0+1)] = true; // Ustawienie komórki jako zajętą

                gameView.DisplayObstacle(obstacleX, obstacleY);

            }

            obstacleStartX = 53;
            obstacleStartY = 15;
            for (int i = 0; i < 5; i++)
            {
                int obstacleX = obstacleStartX + i;
                int obstacleY = obstacleStartY;

                obstacleGrid[obstacleX - (gameModel.X0+1), obstacleY - (gameModel.Y0+1)] = true; // Ustawienie komórki jako zajętą

                gameView.DisplayObstacle(obstacleX, obstacleY);

            }

            obstacleStartX = 44;
            obstacleStartY = 16;
            for (int i = 0; i < 3; i++)
            {
                int obstacleY = obstacleStartY + i;
                int obstacleX = obstacleStartX;

                obstacleGrid[obstacleX - (gameModel.X0+1), obstacleY - (gameModel.Y0+1)] = true; // ustawienie komórki jako zajętą

                gameView.DisplayObstacleVertical(obstacleX, obstacleY);//zmienic klocek

            }
            return obstacleGrid;
        }
    }
}
