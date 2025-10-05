namespace App;

public class SaveItemSystem //Skapar en klass med SaveItemSystem
{
      public void SaveItem(List<Item> items) //Sparar en lista av item-objekt till filen items.txt
      {
            List<string> lines = new List<string>(); //Skapar en lista lines.
            foreach (Item item in items) //Loopar alla items.
            {
                  lines.Add($"{item.OwnerUsername},{item.Name},{item.Description}");  //Lägger till itemet med formatet linens med , mellan varje värde.
            }
            File.WriteAllLines("items.txt", lines); //Skriver varje line av item till listan items.txt
      }
      public List<Item> LoadItems() //Läser in item-objekt från filen items.txt och returnar ett item.
      {
            List<Item> items = new List<Item>(); //Skapar en lista items.
            if (!File.Exists("items.txt")) //Om inte filen items.txt finns
            {
                  return items; //Returna en tom lista.
            }
            string[] lines = File.ReadAllLines("items.txt"); //Läser alla rader från filen till strängar.
            foreach (string line in lines) //Loopar alla rader i listan.
            {
                  string[] split = line.Split(","); //Delar upp rader vid ,
                  if (split.Length == 3) //Kontrollerar att varje rad är likamed 3.
                  {
                        string owner = split[0];
                        string name = split[1];
                        string description = split[2];
                        items.Add(new Item(owner, name, description)); //Lägger till varje rad som uppfyllt dem 3 till ett item. Dessa fälten kan inte ta emot en tom sträng. Därför finns det inga items som inte uppfyller dessa kraven.
                  }
            }
            return items; //Returnar itemet.
      }
}