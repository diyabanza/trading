using App;

List<User> users = new List<User>();
users.Add(new User("kevindiyab", "abc123"));

User? active_user = null;

bool running = true;
while (running)
{
    if (active_user == null)
    {
        Console.Clear();
        System.Console.WriteLine("Log in (type '1')");
        System.Console.WriteLine("Register (type '2')");
        switch (Console.ReadLine())
        {
            case "1": // LOGGA IN
                Console.Clear();
                System.Console.Write("Username: ");
                string username = Console.ReadLine();
                System.Console.Write("Password: ");
                string password = Console.ReadLine();
                foreach (User user in users)
                {
                    if (username == user.Username && password == user.Password)
                    {
                        Console.Clear();
                        System.Console.WriteLine($"Welcome {username}!");
                        System.Console.WriteLine("Press ENTER to continue");
                        Console.ReadLine();
                        active_user = user;
                        break;
                    }
                }
                if (active_user == null) // OM ANVÄNDAREN INTE FINNS, BÖRJA OM
                {
                    System.Console.WriteLine("Incorrect username or password.");
                    Console.ReadLine();
                    break;
                }
                break;

            case "2": // REGISTRERA
                Console.Clear();
                System.Console.Write("Username: ");
                string newUsername = Console.ReadLine();
                System.Console.Write("Password: ");
                string newPassword = Console.ReadLine();
                users.Add(new User(newUsername, newPassword));
                Console.Clear();
                System.Console.WriteLine($"{newUsername} created.");
                Console.ReadLine();
                break;
        }
    }
    else
    {
        // LOGGA UT
        System.Console.WriteLine("Press ENTER to continue");
        System.Console.WriteLine("Type 'logout' to log out");
        string logout = Console.ReadLine();
        if (logout.ToLower() == "logout")
        {
            active_user = null;
            break;
        }
        break;
    }
}
