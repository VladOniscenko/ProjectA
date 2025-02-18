using System.Reflection.Metadata.Ecma335;

public class Player
{
    public string Name;
    public int CurrentHitPoints;
    public int MaximumHitPoints;
    // public Weapon CurrentWeapon;
    // public Location CurrentLocation;
    public Player(string Name){
        this.Name = Name;
        this.CurrentHitPoints = 30;
        this.MaximumHitPoints = 30;
        // this.CurrentWeapon = CurrentWeapon; //starts with rusty sword
        // this.CurrentLocation = CurrentLocation; //starts at your house
    }

    public void DamagePlayer(int DamageAmount){
        CurrentHitPoints -= DamageAmount;
        if (CurrentHitPoints < 0){
            //game over functionality
        }
    }

    public void HealPlayer(int HealAmount){
        CurrentHitPoints += HealAmount;
        if (CurrentHitPoints > MaximumHitPoints){
            CurrentHitPoints = MaximumHitPoints;
        }
    }



}