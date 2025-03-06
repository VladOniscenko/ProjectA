using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace ProjectA;

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
            CurrentPlayer = new("Player");

            // set player current location
            CurrentPlayer.MoveToLocation(World.LocationByID(World.LOCATION_ID_HOME));

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

            Thread.Sleep(sleepTime);
            Console.WriteLine($"Remember your name, {playerName} for the path ahead is long and perilous.");
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

        public void CheckIfPlayerWonTheGame()
        {
            // logic to check if player won the game
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
            int monsterCount = 0;
            Quest quest = CurrentPlayer.CurrentLocation.FightAvailableHere;
            Monster monster = CurrentPlayer.CurrentLocation.MonsterLivingHere;

            // set IsCompleted to true if won
            
            Console.Clear();
            while (true)
            {
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
                        battleTurn(monster, quest, monsterCount);
                        break;
                    case 'F':
                        Console.WriteLine("Flee");
                        break;
                }
                break;
            }
        }

        public void battleTurn(Monster currentMonster, Quest currentQuest, int monsterCount)
        {
            monsterCount++;
            bool whileFighting = true;
            Quest quest = CurrentPlayer.CurrentLocation.FightAvailableHere;

            Dictionary<char, string> battleOptionList = new()
            {
                {'A', $"Attack the {currentMonster.Name}"},
                {'H', "Heal yourself"},
                {'T', "Try to talk it out ¯\\_(ツ)_/¯ "},
                {'F', "Flee (This will cancel the quest)"}
            };

            Dictionary<char, string> continueOptionList = new()
            {
                {'Y', "Yes"},
                {'N', "No"},
            };

            
            Menu battleMenu = new("What will be your next move?", battleOptionList);
            Menu continueMenu = new($"You killed the {currentMonster.Name}, But the next came. \n Will you continue? \n {monsterCount} ", continueOptionList);
                    while(whileFighting == true)
                    {

                        switch(battleMenu.Run())
                        {
                            case 'A':
                            currentMonster.damageMonster(1);// adjust when weapon is made
                            Console.WriteLine($"You attack the monster his current health is:{currentMonster.CurrentHitPoints}/{currentMonster.MaximumHitPoints}");
                            if (currentMonster.CurrentHitPoints > 0)
                            {
                                monsterTurn();
                            }
                            Thread.Sleep(1000);
                            
                            break;
                            case 'H':
                            CurrentPlayer.HealPlayer(3);
                            Console.WriteLine($"You healed yourself your current health is:{CurrentPlayer.CurrentHitPoints}/{CurrentPlayer.MaximumHitPoints}");
                            Thread.Sleep(1000);
                            monsterTurn();
                            break;
                            case 'T':
                            Console.WriteLine($"{currentMonster.Name}s cant talk...");
                            Thread.Sleep(1000);
                            monsterTurn();
                            break;
                            case 'F':
                            // canceling the quest
                            break;
                        }
                    
                        if (currentMonster.CurrentHitPoints == 0 )
                        {
                           
                            if (monsterCount == 3 && CurrentPlayer.CurrentHitPoints > 0)
                            {   
                                Console.WriteLine("You completed the quest");
                                whileFighting = false;
                                quest.IsCompleted = true;
                            }
                            else
                            {
                                switch(continueMenu.Run())
                                {
                                    case 'Y':
                                    currentMonster.CurrentHitPoints = currentMonster.MaximumHitPoints;
                                    
                                    Console.WriteLine($"{monsterCount}");
                                    Thread.Sleep(1000);
                                    battleTurn(currentMonster, currentQuest, monsterCount);
                                    break;
                                    case 'N':
                                    currentMonster.CurrentHitPoints = currentMonster.MaximumHitPoints;
                                    
                                    // go out the fight
                                    break;
                                }

                            }

                            
                        }

                        if (CurrentPlayer.CurrentHitPoints == 0 )
                            { 
                                Console.WriteLine("you died restart the game");
                                // restart the whole game
                                break;
                            }
                        
                    }
            }
        public void monsterTurn()
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
        

        public void CheckForActions()
        {
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
                actionOptionsList.Add('F', $"Explore {CurrentPlayer.CurrentLocation.Name}");
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
                        CheckIfPlayerWonTheGame();
                        break;
                }
            }
        }
    }