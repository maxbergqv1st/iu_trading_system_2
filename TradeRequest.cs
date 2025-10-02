namespace App;

public class Trade
{
      public string Sender { get; set; }
      public string Receiver { get; set; }
      public List<string> SenderItems { get; set; }
      public List<string> ReceiverItems { get; set; }
      public TradeStatus Status { get; set; }


      public Trade(string sender, string receiver, List<string> sender_items, List<string> receiver_items)
      {
            Sender = sender;
            Receiver = receiver;
            SenderItems = sender_items;
            ReceiverItems = receiver_items;
            Status = TradeStatus.Pending;
      }
}