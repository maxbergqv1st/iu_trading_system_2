namespace App;
//Använder då jag på flera olika ställen inte vill ta emot en tom sträng som ett alternativ.
public class InputHelper //Skapar en public klass.
{
      public string ReadRequired(string prompt) //En public string som kan returna ett värde. Tar emot en sträng prompt.
      {
            string input = ""; //Lagrar inputen i en tom sträng.
            while (string.IsNullOrWhiteSpace(input)) // Loopar om input är tom, null eller mellanslag.
            {
                  Console.Write(prompt); // Frågan man vill ställa och få input på.
                  input = Console.ReadLine(); //Sparar inputen på frågan.
                  if (string.IsNullOrWhiteSpace(input)) // om input frf är tom, null eller mellanslag. 
                  {
                        Console.Clear(); //Cleara consolen.
                        Console.WriteLine("Unvalid input..."); //Unvalid input....
                  }
            }
            return input; // När användaren skriver något som inte är tomt. Retuneras inputen.
      }
}

