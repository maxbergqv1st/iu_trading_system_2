namespace App;

public class Trade // Skapar en klass som representerar en trade mellan två users.
{
      public string Sender { get; set; } // Använder get set för att läsa och sätta värden utifrån.
      public string Receiver { get; set; }
      public List<string> SenderItems { get; set; } //Skapar en lista av strängar med items sändaren vill erbjuda.
      public List<string> ReceiverItems { get; set; } //Skapar en lista av strängar med items sändaren vill ha av mottagaren.
      public TradeStatus Status { get; set; } 


      public Trade(string sender, string receiver, List<string> sender_items, List<string> receiver_items) // Konsturkor för trade klassen.
      {
            Sender = sender;
            Receiver = receiver;
            SenderItems = sender_items;
            ReceiverItems = receiver_items;
            Status = TradeStatus.Pending; // Sätter statusen direkt till Pending då vi vet att den ska vara det när man skapar en trade.
      }
}