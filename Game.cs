using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

// used this video to help me get started
// https://www.youtube.com/watch?v=qAWhGEPMlS8

namespace KeyboardMenu
{
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
        }
    }
}