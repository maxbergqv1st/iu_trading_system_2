using App;

SaveUserSystem save_user_system = new SaveUserSystem();
List<IUser> users = save_user_system.LoadUser();

SaveItemSystem save_item_system = new SaveItemSystem();
List<Item> items = save_item_system.LoadItems();

SaveTradeSystem save_trade_system = new SaveTradeSystem();
List<Trade> trades = save_trade_system.LoadTrades();

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
            Console.WriteLine("[1] Upload\n[2] Browse Trades\n[3] Trade Request\n[4] Trade Pending\n[5] Trade History\n[6] Sign Out");
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
                              Console.WriteLine("===== Create Trade Request =====");
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
                              //ovanf;r 'r tradebrowse igen
                              string receiver = helper.ReadRequired("Enter the username of the user you wanna trade with: ");

                              string name_of_item = helper.ReadRequired("Enter the name of the item you want to trade: ");
                              List<string> offered_item = new List<string>();
                              offered_item.Add(name_of_item);

                              string wanted_item = helper.ReadRequired("Enter the name of the item you want from the other user: ");
                              List<string> offer_wanted_items = new List<string>();
                              offer_wanted_items.Add(wanted_item);

                              
                              Trade new_trade = new Trade(((Account)active_user).Username, receiver, offered_item, offer_wanted_items);
                              trades.Add(new_trade);
                              save_trade_system.SaveTrades(trades);
                              Console.WriteLine("Trade request created and saved!");
                              break;
                        case LoggedInMenu.TradePending:
                              Console.WriteLine("===== Pending Trade Requests =====");
                              
                              List<Trade> pending_trades = new List<Trade>();
                              int index_of_item = 1;
                              foreach (Trade t in trades)
                              {
                                    if (t.Receiver == ((Account)active_user).Username && t.Status == TradeStatus.Pending)
                                    {
                                          Console.WriteLine($"[{index_of_item}] From: {t.Sender}, Offers: {string.Join(", ", t.SenderItems)} | Wants: {string.Join(", ", t.ReceiverItems)}");
                                          pending_trades.Add(t);
                                          index_of_item++;
                                    }
                              }
                              Console.Write("Choose a trade number (or 0 to cancel): ");
                              string input_trade = Console.ReadLine();
                              if (int.TryParse(input_trade, out int trade_choice) && trade_choice > 0 && trade_choice <= pending_trades.Count)
                              {
                                    Trade chosen_trade = pending_trades[trade_choice - 1];
                                    Console.WriteLine($"You selected trade from {chosen_trade.Sender}, Items: {string.Join(", ", chosen_trade.Sender)}");
                                    //adda senders offers och vad de vill ha av dig
                                    Console.WriteLine("[1] Accept  [2] Deny"); // kolla om helper.ReadReq funkar sen
                                    string input_decision = Console.ReadLine();
                                    if (input_decision == "1")
                                    {
                                          chosen_trade.Status = TradeStatus.Accepted;
                                          Console.WriteLine("Trade accepted!");
                                          foreach (string itemName in chosen_trade.SenderItems)
                                          {
                                                foreach (Item i in items)
                                                {
                                                      if (i.Name == itemName && i.OwnerUsername == chosen_trade.Sender)
                                                      {
                                                            i.OwnerUsername = chosen_trade.Receiver;
                                                      }
                                                }
                                          }
                                          foreach (string itemName in chosen_trade.ReceiverItems)
                                          {
                                                foreach (Item i in items)
                                                {
                                                      if (i.Name == itemName && i.OwnerUsername == chosen_trade.Receiver)
                                                      {
                                                            i.OwnerUsername = chosen_trade.Sender;
                                                      }
                                                }
                                          }
                                          save_item_system.SaveItem(items);
                                    }
                                    else if (input_decision == "2")
                                    {
                                          chosen_trade.Status = TradeStatus.Denied;
                                          Console.WriteLine("Trade denied.");
                                    }
                                    save_trade_system.SaveTrades(trades);
                              }
                              
                              break;
                        case LoggedInMenu.TradeHistory:
                              Console.WriteLine("===== trading history =====");
                              foreach (Trade t in trades)
                              {
                                    if (t.Sender == ((Account)active_user).Username || t.Receiver == ((Account)active_user).Username)
                                    {
                                    Console.WriteLine($"Sender: {t.Sender} | Offered: [{string.Join(", ", t.SenderItems)}]  <---> Receiver: {t.Receiver} | Offered: {string.Join(", ", t.ReceiverItems)}, Status: {t.Status}");
                                    }
                              }
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


