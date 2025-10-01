namespace App;

public class SaveItemSystem
{
      public void SaveItem(List<Item> items)
      {
            List<string> lines = new List<string>();
            foreach (Item item in items)
            {
                  lines.Add($"{item.Name},{item.Description},{item.OwnerUsername}");
            }
            File.WriteAllLines("items.txt", lines);
      }
}