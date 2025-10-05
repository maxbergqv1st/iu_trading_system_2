namespace App;

class Account : IUser //Skapar en klass Account som implementerar interfacet IUser
{
      public string Name;
      public string Username;

      public string _password;

      public Account(string name, string username, string password) //Konstruktor för klassen Account. Tilldelar värden till varibaler.
      {
            Name = name;
            Username = username;
            _password = password;
      }

      public bool TryLogin(string username, string password) //Metod för att använda Account till IUsern loggin.
      {
            return username == Username && password == _password; //Skickar username och password till metoden. Returnar true eller false.
      }
}