using System;

namespace KeyboardMenu
{
    class Program
    {
        static void Main()
        {
            Game game = new Game();
            game.Start();
        }
    }

    class Game
    {
        public void Start()
        {
            // WriteLine("Game started!");

            // WriteLine("Press any key to exit...");

            // ReadKey(true);

            string prompt = "Please select an option:";
            string[] options = {"Play", "About", "Quit"};
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
    }

    
}
