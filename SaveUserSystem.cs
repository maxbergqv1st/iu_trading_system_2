namespace App;

public class SaveUserSystem // piblic class som sparar och läser in IUser Account från filen users.txt
{
      public void SaveUser(List<IUser> users) // Sparar en lista av IUser objektet Acount till filen users.txt.
      {
            List<string> lines = new List<string>(); // Skapar en tom lista av strängar lines.
            foreach (Account acc in users) // Loopar alla users.
            {
                  lines.Add($"{acc.Name},{acc.Username},{acc._password}"); // Lägger till en user med rätt format som en lines och separerar med , komma.
            }
            File.WriteAllLines("users.txt", lines); // Skriver till filen users.txt. med strängar av users.
      }
      public List<IUser> LoadUser() // Läser in IUser objektet Account från filen users.txt. och retunrerar en lista av users.
      {
            List<IUser> users = new List<IUser>();
            if (!File.Exists("users.txt")) // i load, finns det ingen users.txt
            {
                  return users; // Returna en tom lista av users
            }
            string[] lines = File.ReadAllLines("users.txt"); // Läser alla rader från filen som strängar
            foreach (string line in lines) // Loopar varje rad i users.txt
            {
                  string[] split = line.Split(","); //Delar upp raden vid ,. 
                  if (split.Length == 3) // Kontrollerar att raden har exakt tre delar.
                  {
                        string name = split[0];
                        string username = split[1];
                        string password = split[2];
                        users.Add(new Account(name, username, password)); // Skapar ett nytt Account objekt med dessa värden och lägger till i listan.
                  }
            }
            return users; // Returnar listan med alla inlästa IUser-objekt (Account).
      }
}