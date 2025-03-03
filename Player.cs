using KeyboardMenu;

public class Player
{
    public string Name;
    public int CurrentHitPoints;
    public int MaximumHitPoints;
    // public Weapon CurrentWeapon;
    public Location? CurrentLocation;

    public Player(string name, Game gameInstance){
        Name = name;
        CurrentHitPoints = 30;
        MaximumHitPoints = 30;
        // this.CurrentWeapon = CurrentWeapon; //starts with rusty sword
        // this.CurrentLocation = CurrentLocation; //starts at your house
        game = gameInstance;
    }

    public void DamagePlayer(int damageAmount){
        CurrentHitPoints = Math.Max(0, CurrentHitPoints - damageAmount);
    }

    public void HealPlayer(int healAmount){
        CurrentHitPoints = Math.Min(MaximumHitPoints, CurrentHitPoints + healAmount);
    }
        // Console.WriteLine("Please enter your name");
        // string name = Console.ReadLine();
        // Player player = new Player(name);

    private void CheckPlayerHealth(){
        if(CurrentHitPoints <= 0){
            Console.WriteLine("You have died");
            Game.OnPlayerDeath();
        }
    }
    public void MoveToLocation(Location? location){
        if (location is null){
            Console.WriteLine("Location not found");
            return;
        }

        CurrentLocation = location;
    }

    public void DisplayMap(){
        if(CurrentLocation is not null){
            CurrentLocation.DisplayMap();
            return;
        }

        Console.WriteLine("WHERE TF ARE YOU??");
    }

    public void MoveTo(string? direction){
        /*
            This functions serves to move the player to a new location by direction like "N", "E", "S", "W"
        */
        
        if(CurrentLocation is null){
            Console.WriteLine("You are lost");
            return;
        }
        
        if(direction is null){
            Console.WriteLine("Invalid direction");
            return;
        }
        
        Location? newLocation = direction.ToUpper() switch
        {
            "N" => CurrentLocation.LocationToNorth,
            "E" => CurrentLocation.LocationToEast,
            "S" => CurrentLocation.LocationToSouth,
            "W" => CurrentLocation.LocationToWest,
            _ => null,
        };

        if(newLocation is null){
            Console.WriteLine("You cannot go that way");
            return;
        }

        CurrentLocation = newLocation;
    }
}