using System.Security.Cryptography;

namespace ProjectA;

public class Game
{
    protected Player CurrentPlayer;
    protected World CurrentWorld;
    protected Menu MainMenu;
    public bool IntroductionPlayed = false;
    public bool Playing;
    
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
        
        CurrentWorld = new();
        CurrentPlayer = new("Player", World.WeaponByID(1));

        // set player current location
        CurrentPlayer.MoveToLocation(World.LocationByID(World.LOCATION_ID_HOME));
        
        // play the introduction of the game and ask for the name of the player
        if(IntroductionPlayed == false)
        {
            Introduction();
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
            Console.WriteLine($"You defeated the {monster.Name}s!");

            if(quest.Reward != null){
                Console.WriteLine($"The {monster.Name} left behind a {quest.Reward.Name}...");
                Console.WriteLine($"Will you equip the {quest.Reward.Name}? y/n");
                string? accepted = Console.ReadLine()?.Trim().ToUpper();
                while (accepted != "Y" && accepted != "N");
                if (accepted == "Y")
                {
                    CurrentPlayer.CurrentWeapon = quest.Reward;
                }
            }
                
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
}