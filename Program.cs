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
                              Console.WriteLine("===== Logged in =====");
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
      }
}


