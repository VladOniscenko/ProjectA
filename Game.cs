using System.Security.Cryptography;

namespace ProjectA;

public class Game
{
    protected Player CurrentPlayer;
    protected World CurrentWorld;
    protected Menu MainMenu;
    public bool IntroductionPlayed = false;
    public bool Playing;
    public bool PassedGuard;
    
    public Game()
    {
        string prompt = "Please select an option:";
        Dictionary<char, string> options = new ()
        {
            {'P', "Play"},
            {'A', "About"},
            {'Q', "Quit"}
        };
        MainMenu = new Menu(prompt, options);
    }

    public void Start()
    {
        switch (MainMenu.Run())
        {
            case 'P':
                Guard();
                // StartGame();
                break;
            case 'A':
                ShowAbout();
                break;
            case 'Q':
                ExitGame();
                break;
        }
    }

    private void StartGame()
    {
        Playing = true;
        PassedGuard = false;
        CurrentWorld = new();
        CurrentPlayer = new("Player");

        // set player current location
        CurrentPlayer.MoveToLocation(World.LocationByID(World.LOCATION_ID_HOME));
        
        // play the introduction of the game and ask for the name of the player
        if(IntroductionPlayed == false)
        {
            // Introduction();
            IntroductionPlayed = true;
        }

        while(Playing)
        {
            /*
                Write the current possible options below (fight, quest, etc)
            */
            
            // check if location has quest available to accept or go in a fight if quest was accepted
            CheckForActions();
            

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
                CurrentPlayer.Name = playerName;
                break; // Break the loop when the name is valid
            }
        }

        string[] storyLines = new string[]
        {
            $"Remember your name, {playerName}—for the path ahead is long and perilous.",
            "The town of Aincrad lives in fear.",
            "Giant spiders lurk within the village gates.",
            "At night, they creep in, taking livestock—and worse.",
            "The townsfolk whisper of heroes, but none remain.",
            $"I can feel your resolve, {playerName}. You cannot stand by and do nothing.",
            "With a rusty sword in hand, you shall prepare to fight.",
            "But will steel alone be enough to end this terror?",
            "Legends speak of a sacred blade, lost to time.",
            "Caliburn—the sword of light, waiting to find purpose.",
            "Some say only in the darkest battles does fate reveal its chosen blade.",
            $"Step forward, take up your weapon, and become the hero Aincrad needs, {playerName}."
        };
        
        PrintWithPause(storyLines);
    }
    
    public void PlayerWon()
    {
        int sleepTime = 1000;
        string[] winMessages = new string[]
        {
            "The battle has been fought. The sword of light, Caliburn, has cleaved through darkness.",
            "The giant spiders that once terrorized Aincrad now lie defeated.",
            "The townsfolk, who once whispered of heroes, now sing songs of your bravery.",
            "You stand at the gates of Aincrad, your heart filled with pride.",
            $"The village is safe once again, thanks to you, {CurrentPlayer.Name}.",
            "You have proven yourself worthy of the legends told in the shadows.",
            "As the sun rises over the horizon, a new chapter begins.",
            $"You, {CurrentPlayer.Name}, are no longer a mere adventurer...",
            "You are the hero of Aincrad.",
            $"\nCongratulations, {CurrentPlayer.Name}! You have won the game!"
        };
        
        PrintWithPause(winMessages);
        ExitGame();
    }
    
    public void CheckIfPlayerWonTheGame()
    {
        if (CurrentPlayer.CurrentLocation.ID != World.LOCATION_ID_SPIDER_FIELD){
            return;
        }
        
        bool playerWon = true;
        foreach (Quest quest in World.Quests)
        {
            if (!quest.IsCompleted)
            {
                playerWon = false;
                break;
            }
        }

        if (playerWon)
        {
            PlayerWon();
        }
    }
    
    public void CheckIfFightWon()
    {
        Quest quest = CurrentPlayer.CurrentLocation.FightAvailableHere;
        Monster monster = CurrentPlayer.CurrentLocation.MonsterLivingHere;
        
        if (quest.IsCompleted)
        {
            // add logic of give a reward to player
                
            //
            
            Console.WriteLine("Wow, you did a good job!");
            Console.WriteLine($"You defeated {monster.Name}");
            
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
            
        }
    }
    
    public void Fight()
    {
        Quest quest = CurrentPlayer.CurrentLocation.FightAvailableHere;
        Monster monster = CurrentPlayer.CurrentLocation.MonsterLivingHere;

        // set IsCompleted to true if won
        
        Console.Clear();
        while (true)
        {
            // remove this below and write fight logic
            Console.WriteLine("You are in a fight");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
            break;
        }
    }
    
    public void TalkToNpc()
    {
        if (CurrentPlayer.CurrentLocation.QuestAvailableHere != null &&
            !CurrentPlayer.CurrentLocation.QuestAvailableHere.AcceptedByPlayer)
        {
            Quest quest = CurrentPlayer.CurrentLocation.QuestAvailableHere;

            Console.Clear();
            
            Console.WriteLine(quest.QuestDescription);
            
            Thread.Sleep(2000);
            Console.WriteLine(quest.QuestName);

            string? accepted;

            do
            {
                Console.WriteLine("Will you accept the quest? Y/n");
                accepted = Console.ReadLine()?.Trim().ToUpper();

            } while (accepted != "Y" && accepted != "N");

            if (accepted == "Y")
            {
                quest.AcceptedByPlayer = true;
            }
        }
    }
    

    public void CheckForActions()
    {
        CheckIfPlayerWonTheGame();

        Dictionary<char, string> actionOptionsList = new ();
        
        if (CurrentPlayer.CurrentLocation.QuestAvailableHere != null &&
            !CurrentPlayer.CurrentLocation.QuestAvailableHere.AcceptedByPlayer)
        {
            actionOptionsList.Add('T', $"Talk to {CurrentPlayer.CurrentLocation.QuestGiver}");
        }
        
        if (CurrentPlayer.CurrentLocation.MonsterLivingHere != null && 
            CurrentPlayer.CurrentLocation.FightAvailableHere != null &&
            CurrentPlayer.CurrentLocation.FightAvailableHere.AcceptedByPlayer)
        {
            actionOptionsList.Add('F', $"Fight the {CurrentPlayer.CurrentLocation.MonsterLivingHere.Name}");
        }
        
        if (actionOptionsList.Count > 0)
        {
            actionOptionsList.Add('W', "Walk by");

            string prompt = $"{CurrentPlayer.CurrentLocation.Name} \n{CurrentPlayer.CurrentLocation.Description} \n\nPlease select an option:";
            Menu actionMenu = new Menu(prompt, actionOptionsList);
            switch (actionMenu.Run())
            {
                case 'W':
                    break;
                case 'T':
                    TalkToNpc();
                    break;
                case 'F':
                    Fight();
                    CheckIfFightWon();
                    break;
            }
        }
    }
    
    public static void PrintWithPause(string[] input)
    {
        bool stopPrinting = false;
        int index = 0;
        
        Console.Clear();

        while (index < input.Length && !stopPrinting)
        {
            Console.WriteLine(input[index]);
            index++;

            if (Console.KeyAvailable)
            {
                Console.ReadKey(intercept: true);
                stopPrinting = true;
            }
            else
            {
                Thread.Sleep(1000);
            }
        }

        while (index < input.Length)
        {
            Console.WriteLine(input[index]);
            index++;
        }
            
        Console.WriteLine("\nPress any key to continue:");
        Console.ReadLine();
    }

    public void Guard()
    {
        Console.Clear();
        int sleepTime = 1000;
        
        // Dialogue when player arrives at Guard's Post
        Thread.Sleep(sleepTime);
        Console.WriteLine("You wish to pass the post? You must have proof of your grith.");
        Thread.Sleep(sleepTime);
        Console.WriteLine("Only then, shall I grant you permission to continue forward.");
        Thread.Sleep(sleepTime);

        bool guardFinished = false;

        while (!guardFinished)
        {
            Console.WriteLine("Have you completed both quests from the farmer's field and the alchemist's garden?");
            Thread.Sleep(sleepTime);
            Console.WriteLine("(Y/N)");
            string yesOrNo = Console.ReadLine();

            // Check if input is valid
            if (!(yesOrNo is "Y" or "N"))
            {
                Console.WriteLine("What was that? I couldn't quite understand. Speak up!");
            }
            else if (yesOrNo is "N")
            {
                Console.WriteLine("Then turn back at once! You have no proof of your grit.");
                guardFinished = true; // Break the loop
            }
            // Input is valid -> Check if answered truthfully
            else
            {   
                Quest quest1 = World.QuestByID(World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN);
                Quest quest2 = World.QuestByID(World.QUEST_ID_CLEAR_FARMERS_FIELD);
                
                // Block of code to check if user has really completed prior two quests.
                if (!(quest1.IsCompleted && quest2.IsCompleted))
                {
                    Console.WriteLine("I can see beneath your lies. Turn back at once!");
                    guardFinished = true;
                }
                else
                {   
                    Console.WriteLine("I can see you speak the truth. It is in your eyes.");
                    Thread.Sleep(sleepTime);
                    Console.WriteLine("We shall play a number game. I will think of a number between and including 1 and 10");
                    Thread.Sleep(sleepTime);
                    Console.WriteLine("You must guess the number in my mind. You have 3 tries to get it right!");
                    Thread.Sleep(sleepTime);
                    Console.WriteLine("Should you fail all 3 chances, you will try again.");
                    Thread.Sleep(sleepTime);
                    Console.WriteLine("Should your resolve not waver, you shall pass this test!");
                    Thread.Sleep(sleepTime);

                    bool validGame = false;
                    do
                    {
                        // Generate random number between 0 and 11
                        Random randInt = new Random();
                        int num = randInt.Next(1, 11);

                        int maxAttempts = 3;
                        while (maxAttempts > 0)
                        {
                            // Ask to guess a number between 0 and 10
                            Console.WriteLine("Guess the number between 1 and 10.");
                            int guessedNum = Convert.ToInt32(Console.ReadLine());

                            // Check attempt
                            if (guessedNum == num)
                            {
                                Console.WriteLine("Correct! You may pass the gate.");
                                PassedGuard = true;
                                validGame = true;
                                guardFinished = true;
                                break;
                            }
                            else
                            {
                                maxAttempts--;
                                if (maxAttempts > 0);
                                {
                                    Console.WriteLine($"Wrong. {maxAttempts} attempts left. Try again!");
                                }
                            }
                        }

                        if (!validGame)
                        {
                            Console.WriteLine("You have used up all your tries. You lack conviction. Try again!");
                        }
                    }
                    while (!validGame);
                }
        }}
    }
}