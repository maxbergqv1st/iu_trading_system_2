namespace App;

public class SaveItemSystem
{
      public void SaveItem(List<Item> items) // retunerar inte ett värde
      {
            List<string> lines = new List<string>();
            foreach (Item item in items)
            {
                  lines.Add($"{item.OwnerUsername},{item.Name},{item.Description}");
            }
            File.WriteAllLines("items.txt", lines);
      }
      public List<Item> LoadItems() // returnerar ett värde.
      {
            List<Item> items = new List<Item>();
            if (!File.Exists("items.txt"))
            {
                  return items;
            }
            string[] lines = File.ReadAllLines("items.txt");
            foreach (string line in lines)
            {
                  string[] split = line.Split(",");
                  if (split.Length == 3)
                  {
                        string owner = split[0];
                        string name = split[1];
                        string description = split[2];
                        items.Add(new Item(owner, name, description));
                  }
            }
            return items;
      }
      
}