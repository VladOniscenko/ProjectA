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
            int currentX = 2;
            int currentY = 3;
            switch(ID)
            {
                case 1:
                currentY = 3;
                break;

                case 2:
                currentY = 2;
                break;

                case 3:
                currentX = 3;
                currentY = 2;
                break;

                case 4:
                currentY = 1;
                break;

                case 5:
                currentY = 0;
                break;

                case 6:
                currentX = 1;
                currentY = 2;
                break;

                case 7:
                currentX = 0;
                currentY = 2;
                break;

                case 8:
                currentX = 4;
                currentY = 2;
                break;

                case 9:
                currentX = 5;
                currentY = 2;
                break;

            }
        
            // List<string> minimap = new List<string>(locations);

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
