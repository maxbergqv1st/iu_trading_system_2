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
      public List<IUser> LoadUser() // void doesnt return anny value
      {
            List<IUser> users = new List<IUser>();
            string[] lines = File.ReadAllLines("users.txt");
            foreach (string line in lines)
            {
                  string[] split = line.Split(",");
                  if (split.Length == 3)
                  {
                        string name = split[0];
                        string username = split[1];
                        string password = split[2];
                        users.Add(new Account(name, username, password));
                  }
            }
            return users;
      }
}