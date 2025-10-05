namespace App;

public class Item //Skapar en public class.
{
      public string OwnerUsername { get; set; }
      //Kan båda läsas och ändras utifrån med get set.
      public string Name { get; set; }
      public string Description { get; set; }
      

      public Item(string ownerUsername, string name, string description) //Konstruktor för klassen Item. Tilldelar värden till varibaler.
      {
            OwnerUsername = ownerUsername; 
            Name = name;
            Description = description;
      }
}