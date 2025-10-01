using System.Reflection.Metadata;

namespace App;

public class SaveUserSystem
{

      public void SaveUser(List<IUser> users)
      {
            // string[] lines = new string[users.Count];
            List<string> lines = new List<string>();
            foreach (Account acc in users)
            {
                  lines.Add($"{acc.Name},{acc.Username},{acc._password}");
            }
            File.WriteAllLines("users.txt", lines);
      }
}