using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

// used this video to help me get started
// https://www.youtube.com/watch?v=qAWhGEPMlS8

namespace KeyboardMenu
{
    class Menu
    {
        private int SelectedIndex;
        private string[] Options;
        private string Prompt;
            
        public Menu(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
        }

        //  create options in menu
        private void DisplayOptions()
        {
            WriteLine(Prompt);
            //  loop over options
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefix;
                if (i == SelectedIndex)
                {
                    prefix = "*";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine($"{prefix} << {currentOption} >>");
            }
            ResetColor();
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
               Clear();
               DisplayOptions();
               ConsoleKeyInfo keyInfo = ReadKey(true);

               keyPressed = keyInfo.Key;
                 //     update selectedindex bades on arrowkeys
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);
            
            return SelectedIndex;
        }

    }
}