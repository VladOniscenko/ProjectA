namespace KeyboardMenu;

public class Game
    {
        public void Start()
        {
            // WriteLine("Game started!");

            // WriteLine("Press any key to exit...");

            // ReadKey(true);

            string prompt = "Please select an option:";
            string[] options = { "Play", "About", "Quit" };
            Menu mainMenu = new Menu(prompt, options);
            int SelectedIndex = mainMenu.Run();

            switch (SelectedIndex)
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
            Introduction();
            
            Console.Clear();
            Console.WriteLine("Game started!");
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

        public void Introduction()
        {
            Console.Clear();
            
            int sleepTime = 1000;
            Console.WriteLine("The winds whisper of a hero destined to rise.");
            Thread.Sleep(sleepTime);

            string playerName = "";
            bool validName = false;

            while (!validName)
            {
                Console.WriteLine("Tell me, child of Aincrad—what is your name?");
                playerName = Console.ReadLine();

                // Check if the name only contains letters
                if (string.IsNullOrWhiteSpace(playerName) || !playerName.All(char.IsLetter))
                {
                    Thread.Sleep(sleepTime);
                    Console.WriteLine("That name does not seem quite right. Try again, child.");
                }

                if (playerName.Length < 3 || playerName.Length > 16)
                {
                    Thread.Sleep(sleepTime);
                    Console.WriteLine("That name does not seem quite right. Try again, child.");
                }
                else
                {
                    validName = true; // Break the loop when the name is valid
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

            Console.WriteLine("\nPress enter to continue:");
            Console.ReadLine();
        }
    }