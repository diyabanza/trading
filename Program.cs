// A user needs to be able to register an account +
// A user needs to be able to log out +
// A user needs to be able to log in +
// A user needs to be able to upload information about the item they wish to trade +
// A user needs to be able to browse a list of other users items +
// A user needs to be able to request a trade for other users items 
// A user needs to be able to browse trade requests.
// A user needs to be able to accept a trade request.
// A user needs to be able to deny a trade request.
// A user needs to be able to browse completed requests.

using App;

List<User> users = new List<User>();
users.Add(new User("kevindiyab", "abc123"));
users.Add(new User("jonas07", "abc123"));
users.Add(new User("fisk", "3"));

User? active_user = null;
string active_username = "";

List<Item> items = new List<Item>();
items.Add(new Item("7up Zero", "A warm 7up that expired around three years ago.", users[0].Username));
items.Add(new Item("Pack of cards", "Contains 43 cards and the package is broken. Would like to trade it for a new deck if possible.", users[0].Username));
items.Add(new Item("Red T-Shirt", "Medium Size", users[1].Username));

List<Trade> trades = new List<Trade>();
trades.Add(new Trade(items[0].Name, users[1].Username, users[0].Username, "Pending"));

bool running = true;
while (running)
{
    if (active_user == null) // UTLOGGAD
    {
        Console.Clear();
        System.Console.WriteLine("--- Trading System ---\n");
        System.Console.WriteLine("Log in (type '1')");
        System.Console.WriteLine("Register (type '2')");
        System.Console.WriteLine("Quit (type 'quit')");
        System.Console.Write("\n--> ");

        string input = Console.ReadLine();
        switch (input)
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
                        active_user = user;
                        active_username = username;
                        break;
                    }
                }
                if (active_user == null) // OM ANVÄNDAREN INTE FINNS, BÖRJA OM
                {
                    System.Console.WriteLine("\nIncorrect username or password.");
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

            case "quit":
                Console.Clear();
                running = false;
                break;
        }
    }
    else // INLOGGAD
    {
        // MENY
        Console.Clear();
        System.Console.WriteLine($"--- Trading System --- (logged in as {active_username})\n"); 
        System.Console.WriteLine("Upload an item (type '1')");
        System.Console.WriteLine("View list of other users items (type '2')");
        System.Console.WriteLine("Browse trade requests (type '3')"); // pending, denied, approved
        System.Console.WriteLine("Log out (type 'logout')");
        System.Console.Write("\n--> ");
        switch (Console.ReadLine())
        {
            case "1": // LADDA UPP ITEM
                Console.Clear();
                System.Console.Write("Name of item: ");
                string name = Console.ReadLine();
                System.Console.Write("Write a description about the item: ");
                string description = Console.ReadLine();
                Console.Clear();
                items.Add(new Item(name, description, active_username));
                System.Console.WriteLine($"Your item ({name}) was uploaded!");
                Console.ReadLine();
                break;

            case "2": // VISA ANDRAS ITEMS
                while (true)
                {
                    try
                    {
                        List<Item> othersItems = new List<Item>();
                        Console.Clear();
                        int i = 1;
                        foreach (Item item in items) // skriver ut alla items som inte är ens egna
                        {
                            if (item.Owner != active_username)
                            {
                                System.Console.WriteLine($"Item: {item.Name}\nOwner: {item.Owner}\n(type '{i}')\n");
                                othersItems.Add(new Item(item.Name, item.Description, item.Owner));
                                ++i;
                            }
                        }
                        System.Console.Write("--> ");
                        int input;
                        input = Convert.ToInt32(Console.ReadLine()) - 1;
                        if (input > othersItems.Count())
                        {
                            continue;
                        }
                        Console.Clear();
                        System.Console.WriteLine($"Item: {othersItems[input].Name}\n\nDescription: {othersItems[input].Description}\n\nOwner: {othersItems[input].Owner}");
                        System.Console.WriteLine("\n -- -- -- -- -- -- \n\nRequest trade? ('yes'/'no')");
                        System.Console.Write("--> ");
                        string requestTrade = Console.ReadLine();
                        switch (requestTrade.ToLower())
                        {
                            case "yes":
                                Console.Clear();
                                i = 1;
                                foreach (Item item in items) // visar alla ens egna items
                                {
                                    if (item.Owner == active_username)
                                    {
                                        System.Console.WriteLine($"{item.Name}\n(type '{i}')\n");
                                        ++i;
                                    }
                                }
                                System.Console.WriteLine("Type 'goback' to go back\n");
                                System.Console.WriteLine("Which item of yours would you like to trade?");
                                System.Console.Write("--> ");
                                input = Convert.ToInt32(Console.ReadLine());
                                if (Convert.ToString(input) == "goback")
                                    {
                                        Console.Clear();
                                        break;
                                    }
                                i = 1;
                                foreach (Item item in items)
                                {
                                    if (item.Owner == active_username)
                                    {
                                        ++i;
                                        if (input == i)
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (input != i)
                                {
                                    continue;
                                }
                                System.Console.WriteLine($"You want to give: {items[i].Name}");
                                Console.ReadLine();
                                break;

                            case "no":
                                Console.Clear();
                                break;

                            default:
                                continue;
                        }
                        
                    }
                    catch
                    {
                        continue;
                    }
                }

            case "3":
                Console.Clear();
                foreach (Trade trade in trades)
                {
                    if (trade.Receiver == active_username)
                    {
                        System.Console.WriteLine($"Item: {trade.Items}\n, Sender: {trade.Sender}\n");
                    }
                }
                Console.ReadLine();
                break;

            case "logout":
                active_user = null;
                active_username = "";
                break;

            default:
                continue;
        }
    }
}