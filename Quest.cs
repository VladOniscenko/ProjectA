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

    string questName;
    string questDescription;
    string questGiver;
    Monster monsterToKill;
    int amountOfMonstersToKill;
    Location locationOfQuest;
    string reward;
    bool isCompleted;

    // Volledige constructor
    public Quest(string name, string description, string giver, Monster monster, int amount, Location location, string reward)
    {
        this.questName = name;
        this.questDescription = description;
        this.questGiver = giver;
        this.monsterToKill = monster;
        this.amountOfMonstersToKill = amount;
        this.locationOfQuest = location;
        this.reward = reward;
        this.isCompleted = false; // Default op false
    }
}


