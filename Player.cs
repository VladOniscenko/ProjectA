using ProjectA;

public class Player
{
    public string Name;
    public int CurrentHitPoints;
    public int MaximumHitPoints;
    public Weapon CurrentWeapon;
    public Location CurrentLocation;

    public Player(string name, Weapon currentWeapon){
        Name = name;
        CurrentHitPoints = 30;
        MaximumHitPoints = 30;
        CurrentWeapon = currentWeapon;
        // this.CurrentLocation = CurrentLocation; //starts at your house
    }

    public void DamagePlayer(int damageAmount){
        CurrentHitPoints = Math.Max(0, CurrentHitPoints - damageAmount);
        CheckPlayerHealth();
    }

    public void HealPlayer(int healAmount){
        CurrentHitPoints = Math.Min(MaximumHitPoints, CurrentHitPoints + healAmount);
    }

    private void  CheckPlayerHealth(){
        if(CurrentHitPoints <= 0){
            Console.WriteLine("You have died... Press any key to go back to the main menu");
            Console.ReadKey(true);
            // Start(); // return to menu
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
        Console.Clear();
        
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