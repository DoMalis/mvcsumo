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

        public Player createPlayer(int playerId)
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
            Player player = new Player(nazwa,playerId);
            if (playerId == 1) player.shape = '+';
            else player.shape = 'o';
            return player;
        }
        /*public int ChooseGameMode(IMenuModel menu)
        {
            Console.Clear();
            string[] options = { "standard", "static", "random" };
            string prompt = "Choose game mode ";
            Console.WriteLine(menu.Prompt);

            //int selectedindex = sizeMenu.Run();

            //return selectedindex;
            
        }*/
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
