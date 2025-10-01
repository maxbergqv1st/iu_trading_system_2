namespace App;

public class InputHelper
{
      public string ReadRequired(string prompt)
      {
            string input = "";
            while (string.IsNullOrWhiteSpace(input))
            {
                  Console.Write(prompt);
                  input = Console.ReadLine();
                  if (string.IsNullOrWhiteSpace(input))
                  {
                        Console.Clear();
                        Console.WriteLine("Unvalid input...");
                  }
            }
            return input;
      }
}

