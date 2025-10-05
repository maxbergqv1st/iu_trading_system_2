namespace App;

public interface IUser //Skapar interface av IUSER
{
            public bool TryLogin(string username, string password); // Metod av att loggain med usernamer och password.
}