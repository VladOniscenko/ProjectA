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
    
    public Dictionary<char, string> BattleOptionList = new()
    {
        {'A', $"Attack"},
        {'H', "Heal yourself"},
        {'T', "Try to talk it out ¯\\_(ツ)_/¯ "},
        {'F', "Flee (This will cancel the quest)"}
    };
    
    public Game()
    {
        string prompt = @" _____                          ___      _                 _                  
/  ___|                        / _ \    | |               | |                 
\ `--. _   _ _ __   ___ _ __  / /_\ \ __| |_   _____ _ __ | |_ _   _ _ __ ___ 
 `--. \ | | | '_ \ / _ \ '__| |  _  |/ _` \ \ / / _ \ '_ \| __| | | | '__/ _ \
/\__/ / |_| | |_) |  __/ |    | | | | (_| |\ V /  __/ | | | |_| |_| | | |  __/
\____/ \__,_| .__/ \___|_|    \_| |_/\__,_| \_/ \___|_| |_|\__|\__,_|_|  \___|
            | |                                                               
            |_|                                                              ";
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
                StartGame();
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
        CurrentPlayer = new("Player", World.WeaponByID(1));

        // set player current location
        CurrentPlayer.MoveToLocation(World.LocationByID(World.LOCATION_ID_HOME));

        // play the introduction of the game and ask for the name of the player
        if (IntroductionPlayed == false)
        {
            Introduction();
            IntroductionPlayed = true;
        }

        while (Playing)
        {
            /*
                Write the current possible options below (fight, quest, etc)
            */

            // check if location has quest available to accept or go in a fight if quest was accepted
            CheckForActions();


            /*
                Write the current possible options above (fight, quest, etc)
            */

            // display health
            CurrentPlayer.DisplayHealth();

            // display the map of current location
            CurrentPlayer.DisplayMap();


            // ask the player where they want to go
            string? whereToGo = Console.ReadLine();
            if (whereToGo == "exit")
            {
                Playing = false;
                continue;
            }

            if (whereToGo == "items")
            {
                CheckInventory();
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
        Console.WriteLine("Welcome to Our Console Game!");
        Console.WriteLine("Developed by CookieBytes for Sharkshark\n");

        List<string> collaborators = new List<string>
        {
            "Vladislav Oniscenko",
            "Younis Mehdaoui",
            "Dimitri Korenhof",
            "Brooklyn Robert",
            "Angel Nokhai"
        };

        Random rand = new Random();
        for (int i = collaborators.Count - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            string temp = collaborators[i];
            collaborators[i] = collaborators[j];
            collaborators[j] = temp;
        }

        Console.WriteLine("Collaborators:");
        foreach (var collaborator in collaborators)
        {
            Console.WriteLine($"- {collaborator}");
        }

        Console.WriteLine();

        Console.WriteLine("About CookieBytes:");
        Console.WriteLine("At CookieBytes, we specialize in crafting innovative and engaging gaming experiences.");
        Console.WriteLine(
            "Whether it's creating original concepts or bringing your favorite ideas to life, our goal is to push the boundaries of game development.");
        Console.WriteLine(
            "We are a passionate team committed to providing high-quality entertainment that resonates with gamers of all kinds.\n");

        Console.WriteLine("Thank you for playing our game!");
        Console.WriteLine(
            "We hope you enjoy the adventure and look forward to bringing more exciting projects in the future.");

        Console.WriteLine();
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
            Console.WriteLine($"Tell me, child of {World.WILLAGE_NAME}, what is your name?");
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
            $"Remember your name, {playerName} for the path ahead is long and perilous.",
            $"The town of {World.WILLAGE_NAME} lives in fear.",
            "Giant spiders lurk within the village gates.",
            "At night, they creep in, taking livestock—and worse.",
            "The townsfolk whisper of heroes, but none remain.",
            $"I can feel your resolve, {playerName}. You cannot stand by and do nothing.",
            "With a rusty sword in hand, you shall prepare to fight.",
            "But will steel alone be enough to end this terror?",
            "Legends speak of a sacred blade, lost to time.",
            "Caliburn—the sword of light, waiting to find purpose.",
            "Some say only in the darkest battles does fate reveal its chosen blade.",
            $"Step forward, take up your weapon, and become the hero {World.WILLAGE_NAME} needs, {playerName}."
        };

        PrintWithPause(storyLines);
    }

    public void PlayerWon()
    {
        int sleepTime = 1000;
        string[] winMessages = new string[]
        {
            "The battle has been fought. The sword of light, Caliburn, has cleaved through darkness.",
            $"The giant spiders that once terrorized {World.WILLAGE_NAME} now lie defeated.",
            "The townsfolk, who once whispered of heroes, now sing songs of your bravery.",
            $"You stand at the gates of {World.WILLAGE_NAME}, your heart filled with pride.",
            $"The village is safe once again, thanks to you, {CurrentPlayer.Name}.",
            "You have proven yourself worthy of the legends told in the shadows.",
            "As the sun rises over the horizon, a new chapter begins.",
            $"You, {CurrentPlayer.Name}, are no longer a mere adventurer...",
            $"You are the hero of {World.WILLAGE_NAME}.",
            $"\nCongratulations, {CurrentPlayer.Name}! You have won the game!"
        };

        PrintWithPause(winMessages);
        ExitGame();
    }

    public void CheckIfPlayerWonTheGame()
    {
        if (CurrentPlayer.CurrentLocation.ID != World.LOCATION_ID_SPIDER_FIELD)
        {
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

    public void RewardPlayer()
    {
        Quest quest = CurrentPlayer.CurrentLocation.FightAvailableHere;
        Monster monster = CurrentPlayer.CurrentLocation.MonsterLivingHere;

        if (quest.IsCompleted)
        {
            Console.WriteLine($"You defeated the {monster.Name}s!");

            if (quest.Reward != null)
            {
                Console.WriteLine($"The {monster.Name} left behind a {quest.Reward.Name}...");
                Console.WriteLine($"Will you equip the {quest.Reward.Name}? y/n");
                string? accepted = Console.ReadLine()?.Trim().ToUpper();
                while (accepted != "Y" && accepted != "N") ;
                if (accepted == "Y")
                {
                    CurrentPlayer.CurrentWeapon = quest.Reward;
                }
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
        }
    }

    public void StartFight()
    {
        Monster monster = CurrentPlayer.CurrentLocation.MonsterLivingHere;

        Console.Clear();
        Dictionary<char, string> encounterOptionsList = new()
        {
            { 'E', $"Engage battle with the {monster.Name}." },
            { 'F', $"Flee from the {monster.Name}." }
        };
        
        Menu encounterMenu = new($"You encounterd a {monster.Name}!", encounterOptionsList );
        switch (encounterMenu.Run())
        {
            case 'E':
                Console.WriteLine("Start the battle");
                Fight();
                break;
            case 'F':
                Console.WriteLine("You succesfully ran away");
                Thread.Sleep(1000);
                break;
        }
    }

    public void Fight()
    {
        Quest quest = CurrentPlayer.CurrentLocation.FightAvailableHere;
        Monster currentMonster = CurrentPlayer.CurrentLocation.MonsterLivingHere;
        Menu battleMenu = new("What will be your next move?", BattleOptionList);
        
        Console.Clear();
        Console.WriteLine($"U need to kill {quest.AmountOfMonstersToKill} {currentMonster.Name}'s");
        Console.WriteLine();

        int round = 1;
        while (round <= quest.AmountOfMonstersToKill)
        {
            Console.WriteLine();
            
            currentMonster.CurrentHitPoints = currentMonster.MaximumHitPoints;
            switch(battleMenu.Run(false))
            {
                case 'A':
                    currentMonster.damageMonster(CurrentPlayer.CurrentWeapon.Damage);
                    Console.WriteLine($"Monster {round}: You attack the monster his current health is:{currentMonster.CurrentHitPoints}/{currentMonster.MaximumHitPoints}");
                    break;
                case 'H':
                    CurrentPlayer.HealPlayer(3);
                    Console.WriteLine($"You healed yourself, your current health is:{CurrentPlayer.CurrentHitPoints}/{CurrentPlayer.MaximumHitPoints}");
                    break;
                case 'T':
                    Console.WriteLine($"{currentMonster.Name}s cant talk...");
                    break;
                case 'F':
                    return;
            }
            
            Thread.Sleep(1000);
            if (!currentMonster.IsAlive())
            {
                round++;
                Console.WriteLine($"U killed {currentMonster.Name}");
                continue;
            }

            MonsterTurn();
            if (!CurrentPlayer.IsAlive())
            {
                Console.WriteLine("You died! Press any key to return to main menu.");
                Console.ReadKey();
                Start();
            }
            
            Console.WriteLine($"Your current quest progression: {round}/{quest.AmountOfMonstersToKill}");
            Thread.Sleep(1000);
            
            Console.WriteLine();
        }

        quest.IsCompleted = true;
        RewardPlayer();
        return;
    }
    public void MonsterTurn()
    {
        Console.WriteLine($"Now its the {CurrentPlayer.CurrentLocation.MonsterLivingHere.Name} its turn");
        CurrentPlayer.DamagePlayer(CurrentPlayer.CurrentLocation.MonsterLivingHere.MaximumDamage);
        Thread.Sleep(1000);

        Console.WriteLine($"The {CurrentPlayer.CurrentLocation.MonsterLivingHere.Name} attack you, your current health is:{CurrentPlayer.CurrentHitPoints}/{CurrentPlayer.MaximumHitPoints}");
        Thread.Sleep(1000);
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

    public void CheckInventory()
    {
        Console.Clear();
        Console.WriteLine("You have the following items in your inventory:");
        for (int i = 1; i < CurrentPlayer.items.Count + 1; i++)
        {
            Console.WriteLine($"{i}. {CurrentPlayer.items[i - 1].Name}");
        }

        int input = -1;
        while (true)
        {
            Console.WriteLine("Select an item");
            string? userInput = Console.ReadLine()?.Trim();

            if (!int.TryParse(userInput, out input) || input > CurrentPlayer.items.Count)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and " + CurrentPlayer.items.Count +
                                  ".");
                continue;
            }

            input--;
            break;
        }

        Weapon chosenWeapon = CurrentPlayer.items[input];
        String inventoryPrompt = "Wat would you like to do?";
        Dictionary<char, string> inventoryOptions = new()
        {
            { 'S', "See item stats" },
            { 'E', "Equip item" },
            { 'Q', "Exit inventory" }
        };

        Menu inventoryMenu = new Menu(inventoryPrompt, inventoryOptions);
        switch (inventoryMenu.Run())
        {
            case 'S':
                chosenWeapon.ShowStats();
                CheckInventory();
                break;
            case 'E':
                CurrentPlayer.CurrentWeapon = chosenWeapon;
                Console.WriteLine($"You have equipped the {chosenWeapon.Name}");
                Thread.Sleep(2000);
                break;
            case 'Q':
                break;
        }
    }


    public void CheckForActions()
    {
        CheckIfPlayerWonTheGame();

        Dictionary<char, string> actionOptionsList = new();

        if (CurrentPlayer.CurrentLocation.QuestAvailableHere != null &&
            !CurrentPlayer.CurrentLocation.QuestAvailableHere.AcceptedByPlayer)
        {
            actionOptionsList.Add('T', $"Talk to {CurrentPlayer.CurrentLocation.QuestGiver}");
        }

        if (CurrentPlayer.CurrentLocation.MonsterLivingHere != null &&
            CurrentPlayer.CurrentLocation.FightAvailableHere != null &&
            CurrentPlayer.CurrentLocation.FightAvailableHere.AcceptedByPlayer && !CurrentPlayer.CurrentLocation.FightAvailableHere.IsCompleted)
            
        {
            actionOptionsList.Add('F', $"Fight the {CurrentPlayer.CurrentLocation.MonsterLivingHere.Name}");
        }

        if (actionOptionsList.Count > 0)
        {
            actionOptionsList.Add('W', "Walk by");

            string prompt =
                $"{CurrentPlayer.CurrentLocation.Name} \n{CurrentPlayer.CurrentLocation.Description} \n\nPlease select an option:";
            Menu actionMenu = new Menu(prompt, actionOptionsList);
            switch (actionMenu.Run())
            {
                case 'W':
                    break;
                case 'T':
                    TalkToNpc();
                    break;
                case 'F':
                    StartFight();
                    break;
            }
        }
        
        if (CurrentPlayer.CurrentLocation.ID == World.LOCATION_ID_GUARD_POST)
        {
            Guard();
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
        Console.WriteLine($"You are at {CurrentPlayer.CurrentLocation.Name}");

        
        // Set variables
        bool guardFinished = false;
        int sleepTime = 1000;

        Thread.Sleep(sleepTime);
        Console.WriteLine("You wish to pass the post? You must have proof of your grith.");
        Console.WriteLine("Only then, shall I grant you permission to continue forward.");

        string yesOrNo;
        while (true)
        {
            Console.WriteLine("Have you completed both quests from the farmer's field and the alchemist's garden (Y/n)?");
            yesOrNo = Console.ReadLine().ToUpper();

            if (yesOrNo != "Y" && yesOrNo != "N")
            {
                Console.WriteLine("What was that?");
                continue; // Continues the loop
            }
             
            if (yesOrNo == "N")
            {
                Console.WriteLine("Then turn back at once! You have no proof of your grit");
                CurrentPlayer.CurrentLocation = World.LocationByID(World.LOCATION_ID_TOWN_SQUARE);
                return; // Exits method
            }

            break; // Exits loop
        }

        Quest quest1 = World.QuestByID(World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN);
        Quest quest2 = World.QuestByID(World.QUEST_ID_CLEAR_FARMERS_FIELD);

        if (!quest1.IsCompleted || !quest2.IsCompleted)
        {
            CurrentPlayer.CurrentLocation = World.LocationByID(World.LOCATION_ID_TOWN_SQUARE);
            Console.WriteLine("I can see beneath your lies. Turn back at once!");
            Console.WriteLine("\nPress any key to return");
            Console.ReadKey(true);
            return;
        }

        string[] guardLines = new string[]
        {
            "I see the truth in your eyes.",
            "We shall play a number game. I’ll think of a number between 1 and 10.",
            "You have 3 tries to guess it.",
            "If you fail, you may try again. If you succeed, you shall pass!",
        };

        PrintWithPause(guardLines);

        Console.Clear(); // Clears the console screen. Useful!

        while (!PassedGuard)
        {
            // Generate random number between 0 and 11
            Random randInt = new Random();
            int num = randInt.Next(1, 11);
            int currAttempts = 0;
            int maxAttempts = 3;

            while (maxAttempts > currAttempts)
            {
                Console.WriteLine("Guess the number between 1 and 10.");
                if (!int.TryParse(Console.ReadLine(), out int guessedNum))
                {
                    Console.WriteLine("Invalid guess! Try again.");
                    continue;
                }

                if (guessedNum == num)
                {
                    Console.WriteLine("Correct! You may now pass the gate");
                    PassedGuard = true;
                    break;
                }

                currAttempts++;
                Console.WriteLine($"Incorrect. You have {3 - currAttempts} attempts left!");
            }

            if (!PassedGuard)
            {
                Console.Clear();
                Console.WriteLine("You failed to guess the number in 3 tries. You lack conviction!");
                Thread.Sleep(sleepTime);
                while (true)
                {
                    Console.WriteLine("Do you wish to try again?");
                    Thread.Sleep(sleepTime);
                    Console.WriteLine("Y/N");
                    string noOrYes = Console.ReadLine().ToUpper();
                    if (noOrYes != "Y" && noOrYes != "N")
                    {
                        Console.WriteLine("What was that?");
                        continue;
                    }

                    if (noOrYes == "N")
                    {
                        return;
                    }

                    break;
                }
            }
        }
    }
}