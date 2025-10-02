namespace App;

public enum MainMenu
{
      Login = 1,
      Register = 2,
      Quit = 3
}

public enum LoggedInMenu
{
      Upload = 1,
      Browse = 2,
      TradeRequest = 3,
      TradeHistory = 4,
      Logout = 5
}

public enum TradeStatus
{
      Pending,
      Accepted,
      Denied
}
