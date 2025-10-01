namespace App;

public class Item
{
      public string Name { get; set; }
      public string Description { get; set; }
      public string OwnerUsername { get; set; }

      public Item(string name, string description, string ownerUsername)
      {
            Name = name;
            Description = description;
            OwnerUsername = ownerUsername;
      }
}