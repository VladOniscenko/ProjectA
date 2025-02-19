static class Program
{
    static void Main()
    {
       Introduction(); 
    }

    static void Introduction()      // async is a keyword in C# that allows methods to run asynchronously
    {
        Console.WriteLine("The winds whisper of a hero destined to rise.");
        Thread.Sleep(3000);

        string playerName = "";
        bool validName = false;

        while (!validName)
        {
            Console.WriteLine("Tell me, child of Aincrad—what is your name?");
            playerName = Console.ReadLine();

            // Check if the name only contains letters
            if (string.IsNullOrWhiteSpace(playerName) || !playerName.All(char.IsLetter))
            {
                Thread.Sleep(1500);
                Console.WriteLine("That name does not seem quite right. Try again, child.");
            }
            if (playerName.Length < 3 || playerName.Length > 16)
            {
                Thread.Sleep(1500);
                Console.WriteLine("That name does not seem quite right. Try again, child.");
            }
            else
            {
                validName = true;  // Break the loop when the name is valid
            }
        }
        
        Thread.Sleep(1500);
        Console.WriteLine($"Remember your name, {playerName}—for the path ahead is long and perilous.");
        Thread.Sleep(4000);
        Console.WriteLine($"The town of Aincrad lives in fear.");
        Thread.Sleep(4000);
        Console.WriteLine($"Giant spiders lurk within the village gates.");
        Thread.Sleep(4000);
        Console.WriteLine($"At night, they creep in, taking livestock—and worse.");
        Thread.Sleep(4000);
        Console.WriteLine($"The townsfolk whisper of heroes, but none remain.");
        Thread.Sleep(4000);
        Console.WriteLine($"I can feel your resolve, {playerName}. You can not stand by and do nothing.");
        Thread.Sleep(4500);
        Console.WriteLine($"With a rusty sword in hand, you shall prepare to fight.");
        Thread.Sleep(4000);
        Console.WriteLine($"But will steel alone be enough to end this terror?");
        Thread.Sleep(4000);
        Console.WriteLine($"Legends speak of a sacred blade, lost to time.");
        Thread.Sleep(4000);
        Console.WriteLine($"Caliburn—the sword of light, waiting to find purpose.");
        Thread.Sleep(4500);
        Console.WriteLine($"Some say only in the darkest battles does fate reveal its chosen blade.");
        Thread.Sleep(4500);
        Console.WriteLine($"Step forward, take up your weapon, and become the hero Aincrad needs, {playerName}.");
    }
}