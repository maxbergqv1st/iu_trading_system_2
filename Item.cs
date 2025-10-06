namespace App;

public class Item //Skapar en public class.
{
      public string OwnerUsername { get; set; }
      //Kan båda läsas och ändras utifrån med get set.
      public string Name { get; set; }
      public string Description { get; set; }
      
      public bool Tradeable { get; set; }


      public Item(string ownerUsername, string name, string description, bool tradeable) //Konstruktor för klassen Item. Tilldelar värden till varibaler.
      {
            OwnerUsername = ownerUsername;
            Name = name;
            Description = description;
            Tradeable = tradeable; //Sätter tradeable item till true så fort man skapar ett item.      
      }
}