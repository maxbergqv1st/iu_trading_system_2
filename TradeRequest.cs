namespace App;

public class TradeRequest
{
      public string Sender { get; set; }
      public string Receiver { get; set; }
      public List<string> Items { get; set; }
      public TradeStatus Status { get; set; }


      public TradeRequest(string sender, string receiver, List<string> items)
      {
            Sender = sender;
            Receiver = receiver;
            Items = items;
            Status = TradeStatus.Pending;
      }
}