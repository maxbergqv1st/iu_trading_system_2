namespace App;
//Mina Enums val. 
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
      TradePending = 4,
      TradeHistory = 5,
      Logout = 6
}

public enum TradeStatus
{
      Pending,
      Accepted,
      Denied
}
