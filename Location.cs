public class Location
{
        public int ID;
        public string Name;
        public string Description;
        public Quest? QuestAvailableHere;
        public Monster? MonsterLivingHere;
        public Location? LocationToNorth; 
        public Location? LocationToEast;
        public Location? LocationToSouth; 
        public Location? LocationToWest; 


        public Location(int id, string name, string description, Quest? questAvailableHere = null, Monster? monsterLivingHere = null)
        {
            ID = id;
            Name = name;
            Description = description;
            QuestAvailableHere =  questAvailableHere;
            MonsterLivingHere = monsterLivingHere;
        }
        
        
        public void DisplayMap()
        {
        
            bool north = LocationToNorth is not null;
            bool east = LocationToEast is not null;
            bool south = LocationToSouth is not null;
            bool west = LocationToWest is not null;
            string locationName = Name;
            string locationDescription = Description;


            char[,] miniMap = {
            { ' ', ' ', 'P', ' ', ' ', ' ' },
            { ' ', ' ', 'A', ' ', ' ', ' ' },
            { 'V', 'F', 'T', 'G', 'B', 'S' },
            { ' ', ' ', 'H', ' ', ' ', ' ' }
            };

            var coordinates = new Dictionary<int, int[]>{
                {1, [2, 3]},
                {2, [2, 2]},
                {3, [3, 2]},
                {4, [2, 1]},
                {5, [2, 0]},
                {6, [1, 2]},
                {7, [0, 2]},
                {8, [4, 2]},
                {9, [5, 2]}
            };

            int currentX = coordinates[ID][0];
            int currentY = coordinates[ID][1];

            for (int i = 0; i < miniMap.GetLength(0); i++)
            {
                for (int j = 0;j < miniMap.GetLength(1); j++)
                {
                    if (j == currentX && i == currentY)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(miniMap[i, j]); 
                        Console.ForegroundColor = ConsoleColor.Gray; 
                    }
                    else
                    {
                        Console.Write(miniMap[i, j]);
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine(locationDescription);
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

            Console.Write("\n\nWhere would you like to go? ");
        }
}
