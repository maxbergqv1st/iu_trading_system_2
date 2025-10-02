using App;

SaveUserSystem save_user_system = new SaveUserSystem();
List<IUser> users = save_user_system.LoadUser();

SaveItemSystem save_item_system = new SaveItemSystem();
List<Item> items = save_item_system.LoadItems();

IUser? active_user = null;
bool running = true;

InputHelper helper = new InputHelper();

while (running)
{
      if (active_user == null)
      {
            Console.Clear();
            Console.WriteLine("[1] login\n[2] register\n[3] quit\nChoose a index to move around [X]");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                  Console.Clear();
                  switch ((MainMenu)choice)
                  {
                        case MainMenu.Login:
                              Console.WriteLine("===== Login =====");
                              string login_username = helper.ReadRequired("Username: ");
                              string login_password = helper.ReadRequired("Password: ");
                              foreach (Account acc in users)
                              {
                                    if (acc.Username == login_username && acc._password == login_password)
                                    {
                                          active_user = acc;
                                          Console.WriteLine("Signing in...");
                                          break;
                                    }
                                    else
                                    {
                                          Console.WriteLine("User not found..."); //bugg when logging in
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
            Console.Clear();
            Console.WriteLine("===== Logged in =====");
            Console.WriteLine("[1] Upload\n[2] Browse Trades\n[3] Trade Request\n[4] Trade History\n[5] Logout");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                  Console.Clear();
                  switch ((LoggedInMenu)choice)
                  {
                        case LoggedInMenu.Upload:
                              Console.WriteLine("===== Upload =====");
                              string item_name = helper.ReadRequired("Item name: ");
                              string item_description = helper.ReadRequired("Item description: ");
                              items.Add(new Item(((Account)active_user).Username, item_name, item_description));
                              save_item_system.SaveItem(items);
                              Console.WriteLine("Item uploaded and saved!");
                              break;
                        case LoggedInMenu.Browse:
                              Console.WriteLine("===== Browse Other Users Items =====");
                              if (items.Count == 0)
                              {
                                    Console.WriteLine("no items listed");
                              }
                              else
                              {
                                    int index = 1;
                                    foreach (Item i in items)
                                    {
                                          if (i.OwnerUsername != ((Account)active_user).Username)
                                          {
                                                Console.WriteLine($"[{index}] Owner: {i.OwnerUsername}  | Item: {i.Name} | Description {i.Description}");
                                                index++;
                                          }
                                    }
                              }
                              
                              break;
                        case LoggedInMenu.TradeRequest:
                              Console.WriteLine("trading request");
                              break;
                        case LoggedInMenu.TradeHistory:
                              Console.WriteLine("trading history");
                              break;
                        case LoggedInMenu.Logout:
                              active_user = null;
                              Console.WriteLine("logging out...");
                              break;
                  }
                  Console.ReadLine();
            }
            
      }
}

// TODO
// Bool tradeble.. eller annat 
// requst
//
//
//

// A user needs to be able to register an account DONE
// A user needs to be able to log out. DONE
// A user needs to be able to log in. DONE

// A user needs to be able to upload information about the item they wish to trade. DONE
// A user needs to be able to browse a list of other users items. DONE

// A user needs to be able to request a trade for other users items.
// A user needs to be able to browse trade requests.
// A user needs to be able to accept a trade request.
// A user needs to be able to deny a trade request.
// A user needs to be able to browse completed requests.

// Additional Mandatory Features
// In addition to the original features, we now need an automatic save and load system described by the following features:

// The program needs to save relevant data to the computers file system whenever a state change is made. DONE
// The program needs to be able to start and then automatically load all relevant data so it can function as if it was never closed. DONE


