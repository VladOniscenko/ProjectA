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

    public string QuestName;
    public string QuestDescription;
    public string QuestGiver;
    public int AmountOfMonstersToKill;
    public string Reward;
    public bool IsCompleted;

    // Volledige constructor
    public Quest(string name, string description, string giver, int amount, string reward)
    {
        QuestName = name;
        QuestDescription = description;
        QuestGiver = giver;
        AmountOfMonstersToKill = amount;
        Reward = reward;
        IsCompleted = false; // Default op false
    }
}


