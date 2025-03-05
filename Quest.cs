public class Quest
{
    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors

    // Brainstorm bitchesssss

    // Elke quest heeft: 
    // - een naam 
    // - een beschrijving
    // - Iemand die de quest aanbiedt. 
    // - een x aantal dingen to kill
    // - een locatie waar de quest plaatsvindt
    // - een beloning? 

    string QuestName;
    string QuestDescription;
    string QuestGiver;
    Monster MonsterToKill;
    int AmountOfMonstersToKill;
    Location LocationOfQuest;
    string Reward;
    public bool IsCompleted { get; private set; }

    // Volledige constructor
    public Quest(string name, string description, string giver, Monster monster, int amount, Location location, string reward)
    {
        this.QuestName = name;
        this.QuestDescription = description;
        this.QuestGiver = giver;
        this.MonsterToKill = monster;
        this.AmountOfMonstersToKill = amount;
        this.LocationOfQuest = location;
        this.Reward = reward;
        this.IsCompleted = false; // Default to false
    }
}


