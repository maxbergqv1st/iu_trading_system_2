namespace App;

public class Item
{
      public string OwnerUsername { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      

      public Item(string ownerUsername, string name, string description)
      {
            OwnerUsername = ownerUsername;
            Name = name;
            Description = description;
      }
}