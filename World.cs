public class World
{

    public static readonly List<Weapon> Weapons = new List<Weapon>();
    public static readonly List<Monster> Monsters = new List<Monster>();
    public static readonly List<Quest> Quests = new List<Quest>();
    public static readonly List<Location> Locations = new List<Location>();
    public static readonly Random RandomGenerator = new Random();

    public const int WEAPON_ID_RUSTY_SWORD = 1;
    public const int WEAPON_ID_CLUB = 2;
    public const int WEAPON_ID_GREAT_SWORD = 3;

    public const int MONSTER_ID_RAT = 1;
    public const int MONSTER_ID_SNAKE = 2;
    public const int MONSTER_ID_GIANT_SPIDER = 3;

    public const int QUEST_ID_CLEAR_ALCHEMIST_GARDEN = 1;
    public const int QUEST_ID_CLEAR_FARMERS_FIELD = 2;
    public const int QUEST_ID_COLLECT_SPIDER_SILK = 3;

    public const int LOCATION_ID_HOME = 1;
    public const int LOCATION_ID_TOWN_SQUARE = 2;
    public const int LOCATION_ID_GUARD_POST = 3;
    public const int LOCATION_ID_ALCHEMIST_HUT = 4;
    public const int LOCATION_ID_ALCHEMISTS_GARDEN = 5;
    public const int LOCATION_ID_FARMHOUSE = 6;
    public const int LOCATION_ID_FARM_FIELD = 7;
    public const int LOCATION_ID_BRIDGE = 8;
    public const int LOCATION_ID_SPIDER_FIELD = 9;

    static World()
    {
        PopulateWeapons();
        PopulateMonsters();
        PopulateQuests();
        PopulateLocations();
    }
    
    public static void PopulateWeapons()
    {
        Weapons.Add(new Weapon(WEAPON_ID_RUSTY_SWORD, "Rusty sword", 5));
        Weapons.Add(new Weapon(WEAPON_ID_CLUB, "Club", 10));
        Weapons.Add(new Weapon(WEAPON_ID_GREAT_SWORD, "GREAT SWORD", 15));
    }

    public static void PopulateMonsters()
    {
        Monster rat = new Monster(MONSTER_ID_RAT, "rat", 1, 3, 3);
        Monster snake = new Monster(MONSTER_ID_SNAKE, "snake", 10, 7, 7);
        Monster giantSpider = new Monster(MONSTER_ID_GIANT_SPIDER, "giant spider", 3, 10, 10);
        
        Monsters.Add(rat);
        Monsters.Add(snake);
        Monsters.Add(giantSpider);
    }

    public static void PopulateQuests()
    {
        Quest clearAlchemistGarden = new Quest(QUEST_ID_CLEAR_ALCHEMIST_GARDEN, "Clear the alchemist's garden", "Those rats art nibbling on mine own h'rbs! I couldst very much useth an adventur'r to taketh careth of those folk …", 3, WeaponByID(WEAPON_ID_CLUB));
        Quest clearFarmersField = new Quest(QUEST_ID_CLEAR_FARMERS_FIELD, "Clear the farmer's field", "I can't w'rk mine own landeth with those pesky snakes slith'ring 'round! Shall thee holp me?", 3, WeaponByID(WEAPON_ID_GREAT_SWORD));
        Quest clearSpidersForest = new Quest(QUEST_ID_COLLECT_SPIDER_SILK, "Collect spider silk", "Kill spiders in the spider forest", 4);
        
        Quests.Add(clearAlchemistGarden);
        Quests.Add(clearFarmersField);
        Quests.Add(clearSpidersForest);
    }

    public static void PopulateLocations()
    {
        // Create each location
        Location home = new Location(LOCATION_ID_HOME, "Home", "Your house. You really need to clean up the place.");
        Location townSquare = new Location(LOCATION_ID_TOWN_SQUARE, "Town square", "You see a fountain.");
        Location alchemistHut = new Location(LOCATION_ID_ALCHEMIST_HUT, "Alchemist's hut", "There are many strange plants on the shelves.", questGiver:"Alchemist");
        alchemistHut.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN);

        Location alchemistsGarden = new Location(LOCATION_ID_ALCHEMISTS_GARDEN, "Alchemist's garden", "Many plants are growing here.");
        alchemistsGarden.MonsterLivingHere = MonsterByID(MONSTER_ID_RAT);
        alchemistsGarden.FightAvailableHere = QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN);

        Location farmhouse = new Location(LOCATION_ID_FARMHOUSE, "Farmhouse", "There is a small farmhouse, with a farmer in front.", questGiver:"Farmer");
        farmhouse.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_FARMERS_FIELD);

        Location farmersField = new Location(LOCATION_ID_FARM_FIELD, "Farmer's field", "You see rows of vegetables growing here.");
        farmersField.MonsterLivingHere = MonsterByID(MONSTER_ID_SNAKE);
        farmersField.FightAvailableHere = QuestByID(QUEST_ID_CLEAR_FARMERS_FIELD);

        Location guardPost = new Location(LOCATION_ID_GUARD_POST, "Guard post", "There is a large, tough-looking guard here.");

        Location bridge = new Location(LOCATION_ID_BRIDGE, "Bridge", "A stone bridge crosses a wide river.", questGiver:"Throll");
        bridge.QuestAvailableHere = QuestByID(QUEST_ID_COLLECT_SPIDER_SILK);

        Location spiderField = new Location(LOCATION_ID_SPIDER_FIELD, "Forest", "You see spider webs covering covering the trees in this forest.");
        spiderField.MonsterLivingHere = MonsterByID(MONSTER_ID_GIANT_SPIDER);
        spiderField.FightAvailableHere = QuestByID(QUEST_ID_COLLECT_SPIDER_SILK);

        // Link the locations together
        home.LocationToNorth = townSquare;

        townSquare.LocationToNorth = alchemistHut;
        townSquare.LocationToSouth = home;
        townSquare.LocationToEast = guardPost;
        townSquare.LocationToWest = farmhouse;

        farmhouse.LocationToEast = townSquare;
        farmhouse.LocationToWest = farmersField;

        farmersField.LocationToEast = farmhouse;

        alchemistHut.LocationToSouth = townSquare;
        alchemistHut.LocationToNorth = alchemistsGarden;

        alchemistsGarden.LocationToSouth = alchemistHut;

        guardPost.LocationToEast = bridge;
        guardPost.LocationToWest = townSquare;

        bridge.LocationToWest = guardPost;
        bridge.LocationToEast = spiderField;

        spiderField.LocationToWest = bridge;

        // Add the locations to the static list
        Locations.Add(home);
        Locations.Add(townSquare);
        Locations.Add(guardPost);
        Locations.Add(alchemistHut);
        Locations.Add(alchemistsGarden);
        Locations.Add(farmhouse);
        Locations.Add(farmersField);
        Locations.Add(bridge);
        Locations.Add(spiderField);
    }

    public static Location? LocationByID(int id)
    {
        foreach (Location location in Locations)
        {
            if (location.ID == id)
            {
                return location;
            }
        }

        return null;
    }

    public static Weapon WeaponByID(int id)
    {
        foreach (Weapon item in Weapons)
        {
            if (item.ID == id)
            {
                return item;
            }
        }

        return null;
    }



    public static Monster MonsterByID(int id)
    {
        foreach (Monster monster in Monsters)
        {
            if (monster.ID == id)
            {
                return monster;
            }
        }

        return null;
    }

    public static Quest QuestByID(int id)
    {
        foreach (Quest quest in Quests)
        {
            if (quest.ID == id)
            {
                return quest;
            }
        }

        return null;
    }
}
