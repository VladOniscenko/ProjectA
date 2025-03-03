public class Quest
{
    public string QuestName;
    public string QuestDescription;
    public int AmountOfMonstersToKill;
    public Weapon? Reward;
    public bool IsCompleted;
    public int ID;

    public Quest(int id, string name, string description, int amount, Weapon? reward = null)
    {
        ID = id;
        QuestName = name;
        QuestDescription = description;
        AmountOfMonstersToKill = amount;
        Reward = reward;
        IsCompleted = false; // Default op false
    }
}


