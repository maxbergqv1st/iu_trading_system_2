namespace App;

public class SaveTradeSystem
{
      public void SaveTrades(List<Trade> trades)
      {
            List<string> lines = new List<string>();
            foreach (Trade t in trades)
            {
                  string senderItems = string.Join("|", t.SenderItems);
                  string reciverItems = string.Join("|", t.ReceiverItems);
                  lines.Add($"{t.Sender},{t.Receiver},{senderItems},{reciverItems} ,{t.Status}");
            }
            File.WriteAllLines("trades.txt", lines);
      }
      public List<Trade> LoadTrades()
      {
            List<Trade> trades = new List<Trade>();
            if (!File.Exists("trades.txt"))
            {
                  return trades;
            }
            string[] lines = File.ReadAllLines("trades.txt");
            foreach (string line in lines)
            {
                  string[] split = line.Split(",");
                  if (split.Length == 4)
                  {
                        string sender = split[0];
                        string receiver = split[1];
                        List<string> senderItems = new List<string>(split[2].Split("|"));
                        List<string> reciverItems = new List<string>(split[3].Split("|"));
                        TradeStatus status = (TradeStatus)Enum.Parse(typeof(TradeStatus), split[3]);

                        trades.Add(new Trade(sender, receiver, senderItems, reciverItems) { Status = status });
                  }
            }
            return trades;
      }
}