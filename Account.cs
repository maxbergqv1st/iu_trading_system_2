namespace App;

class Account : IUser
{
      public string Name;
      public string Username;

      public string _password;

      public Account(string name, string username, string password)
      {
            Name = name;
            Username = username;
            _password = password;
      }

      public bool TryLogin(string username, string password)
      {
            return username == Username && password == _password;
      }
}