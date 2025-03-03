public class Weapon
{   
    // Attributes of the "Blueprint"
    public string Name { get; set; }
    public int Damage { get; set; }
    public int ID { get; set; }

    // Constructor -> Create an object of the "blueprint"
    public Weapon(int id, string name, int damage)
    {
        Name = name;
        Damage = damage;
        ID = id;
    }

    // Method of the "blueprint" -> Actions of the weapon
    public void Attack()
    {
        Console.WriteLine($"{Name} swings, it deals {Damage} damage!");
    }

    public void ShowStats()
    {
        Console.WriteLine($"Weapon: {Name}\n Damage: {Damage}\n");
    }

    public void Upgrade(int extraDamage)
    {
        Damage += extraDamage;
        Console.WriteLine($"{Name} has been upgraded! New damage: {Damage}");
    }
}

// Test (Doesn't work?)
// class Program
// {
//     static void Main()
//     {
//         Weapon sword = new Weapon("Blades of Chaos", 50, 3.5);
//         sword.ShowStats();
//         sword.Attack();
//     }
// }