using App;

List<User> users = new List<User>();
users.Add(new User("kevindiyab", "abc123"));

User? active_user = null;

string input = "";

bool running = true;
while (running)
{
    if (active_user == null)
    {
        Console.Clear();
        System.Console.WriteLine("Log in (type '1')");
        System.Console.WriteLine("Register (type '2')");
        input = Console.ReadLine();

        switch (input)
        {
            case "1": // kolla om användaren finns i listan
                Console.Clear();
                System.Console.Write("Username: ");
                string username = Console.ReadLine();
                System.Console.Write("Password: ");
                string password = Console.ReadLine();
                foreach (User user in users)
                {
                    if (username == user.Username && password == user.Password)
                    {
                        active_user = user;
                        System.Console.WriteLine($"Welcome {username}!");
                        Console.ReadLine();
                        break;
                    }
                }
                System.Console.WriteLine("Incorrect username or password.");
                Console.ReadLine();
                break;

            case "2": // lägg till användaren i listam
                Console.Clear();
                System.Console.Write("Username: ");
                username = Console.ReadLine();
                System.Console.Write("Password: ");
                password = Console.ReadLine();
                users.Add(new User("kevindiyab", "abc123"));
                break;
        }

    }

}