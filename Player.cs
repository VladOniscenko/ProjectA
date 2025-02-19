public class Player
{
    public string Name;
    public int CurrentHitPoints;
    public int MaximumHitPoints;
    // public Weapon CurrentWeapon;
    // public Location CurrentLocation;
    public Player(string name){
        Name = name;
        CurrentHitPoints = 30;
        MaximumHitPoints = 30;
        // this.CurrentWeapon = CurrentWeapon; //starts with rusty sword
        // this.CurrentLocation = CurrentLocation; //starts at your house
    }

    public void DamagePlayer(int damageAmount){
        CurrentHitPoints = Math.Max(0, CurrentHitPoints - damageAmount);
    }

    public void HealPlayer(int healAmount){
        CurrentHitPoints = Math.Min(MaximumHitPoints, CurrentHitPoints + healAmount)
    }
        // Console.WriteLine("Please enter your name");
        // string name = Console.ReadLine();
        // Player player = new Player(name);
}