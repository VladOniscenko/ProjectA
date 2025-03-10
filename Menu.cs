using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

// used this video to help me get started
// https://www.youtube.com/watch?v=qAWhGEPMlS8

namespace ProjectA
{
    public class Menu
    {
        private int SelectedIndex;
        private Dictionary<char, string> Options;
        private string Prompt;

        public Menu(string prompt, Dictionary<char, string> options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
        }

        //  create options in menu
        private void DisplayOptions()
        {
            WriteLine(Prompt);

            //  Loop over options
            int i = 0;
            foreach (var option in Options)
            {
                string currentOption = option.Value; // Get the option value (string)
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
                i++; // Increment index to track selected option
            }

            ResetColor();
        }

        public char Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();

                DisplayOptions();
                ConsoleKeyInfo keyInfo = ReadKey(true);

                keyPressed = keyInfo.Key;
                //     update selectedindex bades on arrowkeys
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Count - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Count)
                    {
                        SelectedIndex = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);

            List<char> optionKeys = Options.Keys.ToList();
            return optionKeys[SelectedIndex];
        }
    }
}