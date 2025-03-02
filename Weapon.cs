public class Weapon
{   
    // Attributes of the "Blueprint"
    public string Name { get; set; }
    public int Damage { get; set; }
    public double Weight { get; set; }

    // Constructor -> Create an object of the "blueprint"
    public Weapon(string name, int damage, double weight)
    {
        Name = name;
        Damage = damage;
        Weight = weight;
    }

    // Method of the "blueprint" -> Actions of the weapon
    public void Attack()
    {
        Console.WriteLine($"{Name} swings, it deals {Damage} damage!");
    }

    public void ShowStats()
    {
        Console.WriteLine($"Weapon: {Name}\n Damage: {Damage}\n Weight: {Weight}");
    }

    public void Upgrade(int extraDamage)
    {
        Damage += extraDamage;
        Console.WriteLine($"{Name} has been upgraded! New damage: {Damage}");
    }
}

// Test (Doesn't work?)
class Program
{
    static void Main()
    {
        Weapon sword = new Weapon("Blades of Chaos", 50, 3.5);
        sword.ShowStats();
        sword.Attack();
    }
}