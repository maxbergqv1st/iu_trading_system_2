using App;

List<IUser> user = new List<IUser>();

IUser active_user = null;

bool running = true;

InputHelper helper = new InputHelper();

while (running)
{
      if (active_user == null)
      {
            Console.Clear();
            Console.WriteLine("login\nregister\nquit");
      }
      else
      {
            Console.WriteLine("Logged in");
      }
}


