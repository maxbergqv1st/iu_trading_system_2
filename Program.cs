using App;
using Microsoft.VisualBasic;

SaveUserSystem save_user_system = new SaveUserSystem(); // Skapar ett nytt objekt av SaveUserSystem.
List<IUser> users = save_user_system.LoadUser(); // Laddar users till en lista från users.txt. 

SaveItemSystem save_item_system = new SaveItemSystem();
List<Item> items = save_item_system.LoadItems(); 

SaveTradeSystem save_trade_system = new SaveTradeSystem(); 
List<Trade> trades = save_trade_system.LoadTrades(); 

IUser active_user = null; // När active_user är null
bool running = true; // När running är true kör while loopen. 

InputHelper helper = new InputHelper(); // Kollar input och sätter regel för att strängen inte får vara tom. 

while (running) // = true.
{
      if (active_user == null) // active_user är null från början. därför körs detta scoopet först.
      {
            Console.Clear();
            Console.WriteLine("[1] login\n[2] register\n[3] quit\nWrite a index to move around [X]");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int choice)) // Kollar om input går att konvertera till en int. 
            {
                  Console.Clear();
                  switch ((MainMenu)choice) // Switchen använder min enum MainMenu.choice om den inte hittar caset körs default.
                  {
                        case MainMenu.Login:
                              Console.WriteLine("===== Login =====");
                              string login_username = helper.ReadRequired("Username: "); // helper.ReadRequired tillåter inte en tom sträng.
                              string login_password = helper.ReadRequired("Password: ");
                              bool found = false; // found false innan vi kollar om username finns. 
                              foreach (Account acc in users) // foreach loopar genom alla Account in users.
                              {
                                    if (acc.Username == login_username && acc._password == login_password) // Kollar om Username är likamed login_username input, samma med password.
                                    {
                                          active_user = acc; // Scoopet har hittat en users som matchar input. sätter active_user till funnen user.
                                          Console.WriteLine("Signing in...");
                                          found = true; // Boolen found sätts till true. 
                                          break;
                                    }
                              }
                              if (!found) // Om inte found kör detta.
                              {
                                    Console.WriteLine("User not found...");
                              }
                              break;
                        case MainMenu.Register:
                              Console.WriteLine("===== Register =====");
                              string name = helper.ReadRequired("Name: "); //kör min helper.ReadReqquired där jag inte vill ha några tomma strängar som alternativ. 
                              string username = helper.ReadRequired("Username: ");
                              string password = helper.ReadRequired("Password: ");
                              bool exists = false; // Sätter exists till false. 
                              foreach (Account acc in users) // Kollar genom alla users. 
                              {
                                    if (acc.Username == username) // Om input username hittas i users, sätter exists till true och breakar. 
                                    {
                                          exists = true; 
                                          break;
                                    }
                              }
                              if (exists) // Kollar sen om exists är true. 
                              {
                                    Console.WriteLine("Username already exists"); 
                              }
                              else // Om !exists (INTE) Skapa users. 
                              {
                                    users.Add(new Account(name, username, password));
                                    save_user_system.SaveUser(users); //Sparar users. SaveUsers till users.txt. 
                                    Console.WriteLine("Account successfully created and saved!");
                              }
                              break;
                        case MainMenu.Quit:
                              Console.WriteLine("===== Quiting =====");
                              running = false; // stänger av while loopen till programet. 
                              break;
                        default:
                              Console.WriteLine("Unvalid choice..."); // defaultar alla icke inputs som inte leder nånstans.
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
                              string item_name = helper.ReadRequired("Item name: "); // Tillåter inte en tom sträng.
                              string item_description = helper.ReadRequired("Item description: ");
                              items.Add(new Item(((Account)active_user).Username, item_name, item_description)); //Lägger till ett item. Där accountet som är inloggat(active_user) Username.
                              save_item_system.SaveItem(items); // Sparar items till items.txt.
                              Console.WriteLine("Item uploaded and saved!");
                              break;
                        case LoggedInMenu.Browse:
                              Console.WriteLine("===== Browse Other Users Items =====");
                              if (items.Count == 0) // Om inga items finns tillgängliga. 
                              {
                                    Console.WriteLine("no items listed"); 
                              }
                              else
                              {
                                    int index = 1; // Sätter en index för att displaya varje item.
                                    foreach (Item i in items)
                                    {
                                          if (i.OwnerUsername != ((Account)active_user).Username)
                                          {
                                                Console.WriteLine($"[{index}] Owner: {i.OwnerUsername}  | Item: {i.Name} | Description {i.Description}");
                                                index++; // Plussar index för varje item.
                                          }
                                    }
                              }
                              break;
                        case LoggedInMenu.TradeRequest:
                              Console.WriteLine("===== Create Trade Request =====");
                              if (items.Count == 0) // Om det inte finns några item listade.
                              {
                                    Console.WriteLine("no items listed"); 
                              }
                              else // Om items finns. 
                              {
                                    int index = 1; // En index för alla items till foreach loopen.
                                    foreach (Item i in items) // 
                                    {
                                          if (i.OwnerUsername != ((Account)active_user).Username) // Kollar om Item Owner inte är samma med active_user. 
                                          {                                                       // för att jag vill inte kunna tradea requesta mig själv.
                                                Console.WriteLine($"[{index}] Owner: {i.OwnerUsername}  | Item: {i.Name} | Description {i.Description}");
                                                index++; //  ++index för varje item i items. 
                                          }
                                    }
                              }
                              string receiver = helper.ReadRequired("Enter the username of the user you wanna trade with: "); // Skriv Usernamnet på usern du vill tradea. 
                              bool ReceiverExists = false; // Satt till vi inte hittat en Username som vill tradea med.
                              foreach (Account acc in users) // Loopar genom alla users. 
                              {
                                    if (acc.Username == receiver) // Username är samma som input receiver.
                                    {
                                          ReceiverExists = true; // Sätter den till true, för vi hittat en user.. 
                                          break; //Avslutar scoopet.
                                    }
                              }
                              if (!ReceiverExists) // Om inte ReceiverExists.
                              {
                                    // ReceiverExists = false; // behåller den false. // Hade denna innan men kommentera ut den nu för tror inte den behövs. Eftersom jag inte gör någon ändring på värdet blir den onödig.
                                    Console.WriteLine($"User {receiver} does not exist. Trade request cancelled");
                                    break; // Breakar.
                              }
                              Console.WriteLine("===== Create Trade Request =====");
                              Console.WriteLine("Your items: ");
                              foreach (Item i in items) // Loopar genom mina items. 
                              {
                                    if (i.OwnerUsername == ((Account)active_user).Username) // Kollar så active_user är owner.
                                    {
                                          Console.WriteLine($" - {i.Name}"); 
                                    }
                              }
                              Console.WriteLine("Enter the name of the item [YOU] wanna offer: ");
                              string offered_item_input = Console.ReadLine() ?? ""; // Tillåter tom sträng som offered_items.
                              List<string> offered_items = new List<string>(); // Skapar en lista av offered items. 
                              bool invalidTrade = false; // skapar en bool för invalid Trade. 
                              if (!string.IsNullOrWhiteSpace(offered_item_input)) // Kollar om användaren INTE skrev en tom sträng, null eller space.
                              {
                                    string[] splits = offered_item_input.Split(","); // Delar upp vid , om det är flera items.
                                    foreach (string split in splits) // Loopar alla items usern skrev. 
                                    {
                                          string trimmed = split.Trim(); // Tar bort eventuella mellanslag för varje item om det behövs.
                                          bool found = false; // item inte hittat än.
                                          foreach (Item i in items) //Loopar alla item items.
                                          {
                                                if (i.OwnerUsername == ((Account)active_user).Username && i.Name == trimmed) // Kollar om item tillhör användaren och om namnet matchar input.
                                                {

                                                      if (!string.IsNullOrEmpty(trimmed)) // Säkerställer att namnet inte är tomt. 
                                                      {
                                                            found = true; // Sätter item som hittat.
                                                            offered_items.Add(trimmed); // Lägger till item/items till offered_items.
                                                            break; //Avbryter loopen då item är hittat.
                                                      }
                                                }
                                          }
                                          if (!found) // Om inte item är hittat kör detta.
                                          {
                                                Console.WriteLine($"Item {trimmed} wasnt found, please check spelling or if item exists");
                                                invalidTrade = true; //invalidTrade blir true. 
                                          }
                                    }
                                    if (invalidTrade)//Eftersom invalid trade blir true i !found.
                                    {
                                          Console.WriteLine("Trade request cancelled...");
                                          break; // Avbryt loopen utan att skapa en trade/ offer. 
                                    }
                              }
                              Console.WriteLine($"Items owned by {receiver}:");
                              foreach (Item i in items) // Loopar alla items.
                              {
                                    if (i.OwnerUsername == receiver) // Kollar om itemets owner stämmer överense med input namnet för receiver
                                    {
                                          Console.WriteLine($" - {i.Name}"); // Loggar alla items som matchar receiver.
                                    }
                              }
                              Console.WriteLine("Enter the item name(s) you want from [USER], Separate with comma (,) or leave empty if none: ");
                              string wanted_item_input = Console.ReadLine() ?? "";
                              List<string> wanted_items = new List<string>(); // Skapar en lista av wanted_items.
                              if (!string.IsNullOrWhiteSpace(wanted_item_input)) // Kollar om användaren INTE skrev ett tom sträng, null eller space.
                              {
                                    string[] splits = wanted_item_input.Split(","); // Delar upp vid , om det är flera items.
                                    foreach (string split in splits) // Loopar alla items usern skrev.
                                    {
                                          string trimmed = split.Trim(); // Tar bort eventuella mellanslag för varje item om det behövs.
                                          bool found = false; // Found är false.
                                          if (!string.IsNullOrEmpty(trimmed)) // Om itemet inte är tomt, fortsätt.
                                          {
                                                foreach (Item i in items) // Loopar alla items.
                                                {
                                                      if (i.OwnerUsername == receiver && i.Name == trimmed)  // Kollar om itemet ägs av mottagaren och är rätt namn.
                                                      {
                                                            found = true; // Sätter found till true då item existerar.
                                                            wanted_items.Add(trimmed); // Lägger till itemet i listan wanted items.
                                                            break; // Avslutar loopen då item är hittat och sparat.
                                                      }
                                                }
                                                if (!found) // Om inte hittat.
                                                {
                                                      Console.WriteLine($"Item {trimmed} wasnt found, please check spelling or if item exists");
                                                      invalidTrade = true; // Sätter invalidTrade till true.
                                                }

                                          }
                                    }
                                    if (invalidTrade) //invalidTrade är true
                                    {
                                          Console.WriteLine("Trade request cancelled..."); 
                                          break; // Avbryt loopen och trade/offer är avbrutet.
                                    }
                              }
                              
                              Trade new_trade = new Trade(((Account)active_user).Username, receiver, offered_items, wanted_items);
                              //Skapar en trade om alla kraven lyckats och !found blir true och invalidTrade hålls false under hela loopen.
                              trades.Add(new_trade); // Lägger till traden i listan.
                              save_trade_system.SaveTrades(trades); // Sparar traden till trades.txt. (Lokala databasen)
                              Console.WriteLine("Trade request created and saved!");
                              break; // Avslutar casen.
                        case LoggedInMenu.TradePending:
                              Console.WriteLine("===== Pending Trade Requests =====");

                              List<Trade> pending_trades = new List<Trade>(); // Skapar en lista av pending trades.
                              int index_of_item = 1; // Lägger en index för loopa alla objekt i en lista.
                              foreach (Trade t in trades) // Loopar alla trades.
                              {
                                    if (t.Receiver == ((Account)active_user).Username && t.Status == TradeStatus.Pending) // Om receiver är samma som active_user och status är pending.
                                    { // Loopa allt som matchar.
                                          Console.WriteLine($"[{index_of_item}] From: {t.Sender}, Offers: {string.Join(", ", t.SenderItems)} | Wants: {string.Join(", ", t.ReceiverItems)}");
                                          pending_trades.Add(t); //Lägger till trade i listan pending_trades.
                                          index_of_item++; // Plussar index för varje varv.
                                    }
                              }
                              Console.Write("Choose a trade number (or 0 to cancel): ");
                              string input_trade = Console.ReadLine();
                              if (int.TryParse(input_trade, out int trade_choice) && trade_choice > 0 && trade_choice <= pending_trades.Count) // Kollar om input är ett giltligt nummer av valmöjligheterna.
                              {
                                    Trade chosen_trade = pending_trades[trade_choice - 1]; //Hämtar den valda traden. -1 för indexen som visas är plussade med 1. för att inte visa första itemet som 0.
                                    Console.WriteLine($"You selected trade from {chosen_trade.Sender}");
                                    Console.WriteLine($" Sender offers: {string.Join(", ", chosen_trade.SenderItems)}"); //Joinar om de är flera items med ett , och space mellan varje item.
                                    Console.WriteLine($" They offers: {string.Join(", ", chosen_trade.ReceiverItems)}");
                                    //Visar detaljer om den valda traden.
                                    Console.WriteLine("[1] Accept  [2] Deny");
                                    string input_decision = Console.ReadLine();
                                    if (input_decision == "1") // Om man väljer 1 (Accept) kör detta scoopet
                                    {
                                          chosen_trade.Status = TradeStatus.Accepted; //Statusen på traden sätts från pending till accepted.
                                          Console.WriteLine("Trade accepted!");
                                          foreach (string itemName in chosen_trade.SenderItems)// Loopar igenm alla items som sändaren erbjuder.
                                          {
                                                foreach (Item i in items) // Loopar alla items.
                                                {
                                                      if (i.Name == itemName && i.OwnerUsername == chosen_trade.Sender) //Kollar så att item matchar och ägs av sändaren.
                                                      {
                                                            i.OwnerUsername = chosen_trade.Receiver; //Byter ägaren till mottagaren.
                                                      }
                                                }
                                          }

                                          foreach (string itemName in chosen_trade.ReceiverItems) //Loopar alla items som mottagaren erbjuder.
                                          {
                                                foreach (Item i in items) // Loopar alla items.
                                                {
                                                      if (i.Name == itemName && i.OwnerUsername == chosen_trade.Receiver) //Kollar så att item matchar och ägs av mottagaren.
                                                      {
                                                            i.OwnerUsername = chosen_trade.Sender; // Byter ögaren till sändaren.
                                                      }
                                                }
                                          }
                                          
                                          save_item_system.SaveItem(items); //Sparar alla items efter man bytt ägare.
                                    }
                                    else if (input_decision == "2") // Om man väljer 2 (Denied) kör detta scoopet
                                    {
                                          chosen_trade.Status = TradeStatus.Denied; // Sätter trade till denied.
                                          Console.WriteLine("Trade denied.");
                                    }
                                    save_trade_system.SaveTrades(trades); //Sparar trade statusen till denied.
                              }
                              break; // Avslutar caset.
                        case LoggedInMenu.TradeHistory:
                              Console.WriteLine("===== Trading History =====");
                              foreach (Trade t in trades) //Loopar alla trades.
                              {
                                    if ((t.Sender == ((Account)active_user).Username || t.Receiver == ((Account)active_user).Username) && t.Status != TradeStatus.Pending) //lägger prio ett i en (då de får högre prio) annars går && har högre prio ||. 
                                    { // Kollar så att active_user är med i en trade som sändare eller mottagare. Och om traden inte är pending.
                                    Console.WriteLine($"Sender: {t.Sender} | Offered: [{string.Join(", ", t.SenderItems)}]  <---> Receiver: {t.Receiver} | Offered: {string.Join(", ", t.ReceiverItems)}, Status: {t.Status}");
                                    } //Loopar allt som har de kraven.
                              }
                              Console.WriteLine("===== Pending Trades =====");
                              foreach (Trade t in trades) // Gör samma fast kollar om trade statusen är pending.
                                    {
                                          if ((t.Sender == ((Account)active_user).Username || t.Receiver == ((Account)active_user).Username) && t.Status == TradeStatus.Pending) //lägger prio ett i en (då de får högre prio) annars går && har högre prio ||. 
                                          {
                                                Console.WriteLine($"Sender: {t.Sender} | Offered: [{string.Join(", ", t.SenderItems)}]  <---> Receiver: {t.Receiver} | Offered: {string.Join(", ", t.ReceiverItems)}, Status: {t.Status}");
                                          } // Loopar alla trades som frf är pending och man är mottagare eller sändare.
                                    }
                              break; // Avslutar casen.
                        case LoggedInMenu.Logout:
                              active_user = null; //Sätter active_user till null för att jag vill inte stänga ner programmet. Alltså är running frf true. 
                              Console.WriteLine("logging out...");
                              break; //Avslutar casen. Hoppar tillbaka till MainMenu......
                  }
                  Console.ReadLine();
            }
            
      }
}

