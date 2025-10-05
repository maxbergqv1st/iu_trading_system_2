namespace App;

public class SaveTradeSystem //Skapar en public class SaveTradeSystem. 
{
      public void SaveTrades(List<Trade> trades) //Sparar en lista av Trades till filen trades.txt
      {
            List<string> lines = new List<string>();// Skapar en lista lines.
            foreach (Trade t in trades) // Loopar alla trades.
            {
                  string senderItems = string.Join("|", t.SenderItems); // Konverterar sända trades till en sträng som separerar med | 
                  string reciverItems = string.Join("|", t.ReceiverItems); // Konverterar mottagna trades till en sträng som separerar med | 
                  lines.Add($"{t.Sender},{t.Receiver},{senderItems},{reciverItems},{t.Status}"); //Lägger ihop hela traden till en sträng. 
            }
            File.WriteAllLines("trades.txt", lines); //Skriver allt till filen trades.txt. med lines.Add.
      }
      public List<Trade> LoadTrades() //Sparar en lista av Loadtrades. Läser hela trades.txt. Och returnar den.
      {
            List<Trade> trades = new List<Trade>(); // Skapar en lista trades.
            if (!File.Exists("trades.txt")) // Om inte trades.txt finns
            {
                  return trades; //Returna en tom lista.
            }
            string[] lines = File.ReadAllLines("trades.txt"); // Om listan finns läs trades.txt.
            foreach (string line in lines) //Loopar alla lines i trades.txt.
            {
                  string[] split = line.Split(","); //Skapar en sträng som splittar varje line.
                  if (split.Length == 5) // Om varje line alltså en trade har längd 5.
                  {
                        string sender = split[0]; // Splitta alla med index. börjar från 0
                        string receiver = split[1];
                        List<string> senderItems = new List<string>(split[2].Split("|")); //Dela upp indexen sender items och receiver items med |
                        List<string> reciverItems = new List<string>(split[3].Split("|"));
                        TradeStatus status = (TradeStatus)Enum.Parse(typeof(TradeStatus), split[4]); // Konvertrar status strängen till enum TradeStatus.

                        trades.Add(new Trade(sender, receiver, senderItems, reciverItems) { Status = status }); //Skapar ett nytt trade-objekt med dessa värden och lägger till dem i listan.
                  }
            }
            return trades; // returnar listan med alla inläsa trades.
      }
}