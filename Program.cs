using System.Security.Cryptography.X509Certificates;
using App;

List<User> users = new List<User>();
users.Add(new User("kevindiyab", "abc123"));
users.Add(new User("jonas07", "abc123"));
User? active_user = null;
string active_username = "";

List<Item> items = new List<Item>();
items.Add(new Item("7up Zero", "A warm 7up that expired around three years ago.", users[0].Username));
items.Add(new Item("Pack of cards", "Contains only 43 cards and the package is broken. Would like to trade it for a new deck if possible.", users[0].Username));
items.Add(new Item("Red T-Shirt", "Medium Size", users[1].Username));

bool running = true;
while (running)
{
    if (active_user == null) // UTLOGGAD
    {
        Console.Clear();
        System.Console.WriteLine("Log in (1)");
        System.Console.WriteLine("Register (2)");
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
                        active_username = username;
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
                username = Console.ReadLine();
                System.Console.Write("Password: ");
                password = Console.ReadLine();
                users.Add(new User(username, password));
                Console.Clear();
                System.Console.WriteLine($"{username} created.");
                Console.ReadLine();
                break;
        }
    }
    else // INLOGGAD
    {
        // MENY
        Console.Clear();
        System.Console.WriteLine("Upload an item (1)");
        System.Console.WriteLine("Show list of other users items (2)");
        System.Console.WriteLine("Type 'logout' to log out");
        switch (Console.ReadLine())
        {
            case "1":
                break;

            case "2":
                foreach (Item item in items)
                {
                    if (item.Owner != active_username)
                    {
                        System.Console.WriteLine($"Item: {item.Name}\nDescription: {item.Description}\nOwner: {item.Owner}\n");
                    }
                }
                Console.ReadLine();
                break;

            case "logout":
                string logout = Console.ReadLine();
                if (logout.ToLower() == "logout")
                {
                    active_user = null;
                    break;
                }
                break;
        }
    }
}