using System.Security.Cryptography;

namespace KeyboardMenu;

public class Game
    {
        protected Player CurrentPlayer;
        protected World CurrentWorld;
        protected Menu MainMenu;
        public bool IntroductionPlayed = false;
        public bool Playing = false;
        public Game()
        {
            CurrentWorld = new();
            CurrentPlayer = new Player("Player", this);

            // set player current location
            CurrentPlayer.MoveToLocation(World.LocationByID(World.LOCATION_ID_HOME));

            string prompt = "Please select an option:";
            string[] options = ["Play", "About", "Quit"];
            MainMenu = new Menu(prompt, options);
        }

        public void Start()
        {
            Console.Clear();
            switch (MainMenu.Run())
            {
                case 0:
                    StartGame();
                    break;
                case 1:
                    ShowAbout();
                    break;
                case 2:
                    ExitGame();
                    break;
            }
        }

        private void StartGame()
        {
            Playing = true;

            // play the introduction of the game and ask for the name of the player
            if(IntroductionPlayed == false)
            {
                Introduction();
                IntroductionPlayed = true;
            }

            while(Playing)
            {
                Console.Clear();
                
                /*
                    Write the current possible options below (fight, quest, etc)
                */

                

                /*
                    Write the current possible options above (fight, quest, etc)
                */


                // display the map of current location
                CurrentPlayer.DisplayMap();

                // ask the player where they want to go
                string? whereToGo = Console.ReadLine();
                if(whereToGo == "exit")
                {
                    Playing = false;
                    continue;
                }

                // move the player to the location by direction
                CurrentPlayer.MoveTo(whereToGo);
            }

            Start(); // return to menu
        }

        public void OnPlayerDeath()
        {
            Console.Clear();
            Console.WriteLine("You have fallen in battle...");
            Thread.Sleep(2000);
            Console.WriteLine("The town of Aincrad remains in peril.");
            Thread.Sleep(2000);
            Console.WriteLine("Perhaps another hero will rise...");
            Thread.Sleep(2000);

            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey(true);
            Start(); // return to menu
        }

        private void ShowAbout()
        {
            Console.Clear();
            Console.WriteLine("About this game...");
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey(true);
            Start(); // return to menu
        }

        private void ExitGame()
        {
            Console.Clear();
            Console.WriteLine("Exiting game...");
            Environment.Exit(0);
        }

        public static void Introduction()
        {
            Console.Clear();
            
            int sleepTime = 1000;
            Console.WriteLine("The winds whisper of a hero destined to rise.");
            Thread.Sleep(sleepTime);

            string? playerName;
            while (true)
            {
                Console.WriteLine("Tell me, child of Aincrad—what is your name?");
                playerName = Console.ReadLine();

                // Check if the name only contains letters
                if (string.IsNullOrWhiteSpace(playerName) || !playerName.All(char.IsLetter) || playerName.Length < 3)
                {
                    Thread.Sleep(sleepTime);
                    Console.WriteLine("That name does not seem quite right. Try again, child.");
                }
                else
                {
                    break; // Break the loop when the name is valid
                }
            }

            Thread.Sleep(sleepTime);
            Console.WriteLine($"Remember your name, {playerName}—for the path ahead is long and perilous.");
            Thread.Sleep(sleepTime);
            Console.WriteLine($"The town of Aincrad lives in fear.");
            Thread.Sleep(sleepTime);
            Console.WriteLine($"Giant spiders lurk within the village gates.");
            Thread.Sleep(sleepTime);
            Console.WriteLine($"At night, they creep in, taking livestock—and worse.");
            Thread.Sleep(sleepTime);
            Console.WriteLine($"The townsfolk whisper of heroes, but none remain.");
            Thread.Sleep(sleepTime);
            Console.WriteLine($"I can feel your resolve, {playerName}. You can not stand by and do nothing.");
            Thread.Sleep(sleepTime);
            Console.WriteLine($"With a rusty sword in hand, you shall prepare to fight.");
            Thread.Sleep(sleepTime);
            Console.WriteLine($"But will steel alone be enough to end this terror?");
            Thread.Sleep(sleepTime);
            Console.WriteLine($"Legends speak of a sacred blade, lost to time.");
            Thread.Sleep(sleepTime);
            Console.WriteLine($"Caliburn—the sword of light, waiting to find purpose.");
            Thread.Sleep(sleepTime);
            Console.WriteLine($"Some say only in the darkest battles does fate reveal its chosen blade.");
            Thread.Sleep(sleepTime);
            Console.WriteLine($"Step forward, take up your weapon, and become the hero Aincrad needs, {playerName}.");

            Console.WriteLine("\nPress any key to continue:");
            Console.ReadLine();
        }
    }