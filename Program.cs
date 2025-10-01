using App;

SaveUserSystem save_user_system = new SaveUserSystem();

List<IUser> users = save_user_system.LoadUser();

IUser active_user = null;
bool running = true;

InputHelper helper = new InputHelper();

while (running)
{
      if (active_user == null)
      {
            Console.Clear();
            Console.WriteLine("[1] login\n[2] register\n[3] quit\nChoose a index to move around [X]");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                  Console.Clear();
                  switch ((MainMenu)choice)
                  {
                        case MainMenu.Login:
                              Console.WriteLine("===== Login =====");
                              string login_username = helper.ReadRequired("Username: ");
                              string login_password = helper.ReadRequired("Username: ");
                              foreach (Account acc in users)
                              {
                                    if (acc.Username == login_username && acc._password == login_password)
                                    {
                                          active_user = acc;
                                          break;
                                    }
                                    else
                                    {
                                          Console.WriteLine("User not found...");
                                    }
                              }
                              break;
                        case MainMenu.Register:
                              Console.WriteLine("===== Register =====");
                              string name = helper.ReadRequired("Name: ");
                              string username = helper.ReadRequired("Username: ");
                              string password = helper.ReadRequired("Password: ");
                              bool exists = false;
                              foreach (Account acc in users)
                              {
                                    if (acc.Username == username)
                                    {
                                          exists = true;
                                          break;
                                    }
                              }
                              if (exists)
                              {
                                    Console.WriteLine("Username already exists");
                              }
                              else
                              {
                                    users.Add(new Account(name, username, password));
                                    save_user_system.SaveUser(users);
                                    Console.WriteLine("Account successfully created and saved!");
                              }
                              break;
                        case MainMenu.Quit:
                              Console.WriteLine("===== Quiting =====");
                              running = false;
                              break;
                        default:
                              Console.WriteLine("Unvalid choice...");
                              break;
                  }
                  Console.ReadLine();
            }
      }
      else
      {
            Console.WriteLine("Logged in");
            Console.ReadLine();
      }
}

// A user needs to be able to register an account DONE
// A user needs to be able to log out.
// A user needs to be able to log in.
// A user needs to be able to upload information about the item they wish to trade.
// A user needs to be able to browse a list of other users items.
// A user needs to be able to request a trade for other users items.
// A user needs to be able to browse trade requests.
// A user needs to be able to accept a trade request.
// A user needs to be able to deny a trade request.
// A user needs to be able to browse completed requests.
// Additional Mandatory Features
// In addition to the original features, we now need an automatic save and load system described by the following features:

// The program needs to save relevant data to the computers file system whenever a state change is made.
// The program needs to be able to start and then automatically load all relevant data so it can function as if it was never closed.


