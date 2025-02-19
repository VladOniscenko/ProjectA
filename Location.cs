public class Location
{
        public int ID;
        public string Name;
        public string Description;
        public Quest QuestAvailableHere;
        public Monster MonsterLivingHere;
        public Location? LocationToNorth; 
        public Location? LocationToEast;
        public Location? LocationToSouth; 
        public Location? LocationToWest; 


        public Location(int id, string name, string description, Quest questAvailableHere, Monster monsterLivingHere)
        {
            ID = id;
            Name = name;
            Description = description;
            QuestAvailableHere =  questAvailableHere;
            MonsterLivingHere = monsterLivingHere;
        }
        
        
        public void DisplayMap(Location location)
        {
        
            bool north = LocationToNorth is not null;
            bool east = LocationToEast is not null;
            bool south = LocationToSouth is not null;
            bool west = LocationToWest is not null;
            string locationName = Name;
        
            Console.WriteLine("Where would you like to go?");
            Console.WriteLine($"You are at: {locationName} From here you can go:");
        
            if (north)
            {
                Console.WriteLine("    N");
                Console.WriteLine("    |");
            }

            if (west)
            {
                Console.Write("W---");
                Console.Write("|");
            }
            else
            {
                Console.Write("    |");
            }

            if (east)
            {
                Console.WriteLine("---E");
            }

            if (south)
            {
                if (!east)
                {
                    Console.Write("\n");
                }
                Console.WriteLine("    |");
                Console.WriteLine("    S");
            }
        }
}
