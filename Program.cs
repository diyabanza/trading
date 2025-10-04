using App;

List<User> users = new List<User>();
users.Add(new User("kd", "asd"));
users.Add(new User("jonas", "asd"));
users.Add(new User("fisk", "asd"));
users.Add(new User("bo", "asd"));

User? active_user = null;
string active_username = "";

List<Item> items = new List<Item>();
items.Add(new Item("7up Zero", "A warm 7up that expired around three years ago.", users[0].Username));
items.Add(new Item("Pack of cards", "Contains 43 cards and the package is broken. Would like to trade it for a new deck if possible.", users[0].Username));
items.Add(new Item("Red T-Shirt", "Medium Size", users[1].Username));

List<Trade> trades = new List<Trade>();
trades.Add(new Trade(items[2].Name, items[0].Name, users[1].Username, users[0].Username, Tradestatus.Pending));

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
        System.Console.WriteLine("Browse trade requests (type '3')");
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
                while (true)
                {
                    Console.Clear();
                    System.Console.WriteLine($"Upload {name}? ('yes'/'no')\n ");
                    System.Console.Write("--> ");
                    string upload = Console.ReadLine();
                    if (upload.ToLower() == "yes")
                    {
                        items.Add(new Item(name, description, active_username));
                        System.Console.WriteLine($"Your item ({name}) was uploaded!");
                        Console.ReadLine();
                        break;
                    }
                    if (upload.ToLower() == "no")
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                break;

            case "2": // VISA ANDRAS ITEMS
                while (true)
                {
                    try
                    {
                        List<Item> othersItems = new List<Item>();
                        Console.Clear();
                        int i = 1;
                        foreach (Item item in items) // skriver ut alla items som inte är ens egna & lägger till dem i en separat lista
                        {
                            if (item.Owner != active_username)
                            {
                                System.Console.WriteLine($"Item: {item.Name}\nOwner: {item.Owner}\n(type '{i}')\n");
                                othersItems.Add(new Item(item.Name, item.Description, item.Owner));
                                ++i;
                            }
                        }
                        System.Console.WriteLine("Type 'goback' to go back\n");
                        System.Console.Write("--> ");
                        string backinput = Console.ReadLine();
                        if (backinput == "goback")
                        {
                            Console.Clear();
                            break;
                        }
                        int input;
                        int input2;
                        int.TryParse(backinput, out input);
                        input = input - 1;
                        if (input > othersItems.Count())
                        {
                            continue;
                        }
                        Console.Clear(); // visar mer info om det item man vill se (nedan)
                        System.Console.WriteLine($"Item: {othersItems[input].Name}\n\nDescription: {othersItems[input].Description}\n\nOwner: {othersItems[input].Owner}");
                        System.Console.WriteLine("\n -- -- -- -- -- -- \n\nRequest trade? ('yes'/'no')");
                        System.Console.Write("--> ");
                        string requestTrade = Console.ReadLine();
                        switch (requestTrade.ToLower())
                        {
                            case "yes":
                                List<Item> yourItems = new List<Item>();
                                foreach (Item item in items) // lägger till ens egna items i en separat lista
                                {
                                    if (active_username == item.Owner)
                                    {
                                        yourItems.Add(new Item(item.Name, item.Description, item.Owner));
                                    }
                                }
                                if (yourItems.Count() <= 0)
                                {
                                    System.Console.WriteLine("\nYou don't have any items to trade with...");
                                    Console.ReadLine();
                                    break;
                                }
                                Console.Clear();
                                System.Console.WriteLine("Which item of yours would you like to trade?");
                                i = 1;
                                foreach (Item item in yourItems) // visar alla ens egna items
                                {
                                    System.Console.WriteLine($"{item.Name}\n(type '{i}')\n");
                                    ++i;
                                }
                                System.Console.WriteLine("Type 'goback' to go back\n");
                                System.Console.Write("--> ");
                                input2 = Convert.ToInt32(Console.ReadLine()) - 1;
                                if (Convert.ToString(input2) == "goback")
                                {
                                    Console.Clear();
                                    break;
                                }
                                if (input2 > yourItems.Count())
                                {
                                    continue;
                                }
                                Console.Clear();
                                System.Console.WriteLine($"You give: {yourItems[input2].Name}");
                                System.Console.WriteLine($"{othersItems[input].Owner} gives: {othersItems[input].Name}");
                                System.Console.WriteLine("\nConfirm trade request? ('yes'/'no')");
                                switch (Console.ReadLine())
                                {
                                    case "yes":
                                        Console.Clear();
                                        trades.Add(new Trade(yourItems[input2].Name, othersItems[input].Name, active_username, othersItems[input].Owner, Tradestatus.Pending));
                                        System.Console.WriteLine($"Trade request was sent to {othersItems[input].Owner}");
                                        Console.ReadLine();
                                        break;

                                    case "no":
                                        Console.Clear();
                                        break;

                                    default:
                                        continue;
                                }
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
                break;

            case "3": // VISA TRADE REQUESTS
                Console.Clear();
                System.Console.WriteLine("Received trade requests [Pending] (type '1')");
                System.Console.WriteLine("Sent trade requests [Pending] (type '2')");
                System.Console.WriteLine("Approved trade requests (type '3')");
                System.Console.WriteLine("Denied trade requests (type '4')\n");
                System.Console.Write("--> ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        List<Trade> receivedTrades = new List<Trade>();
                        int i = 1;
                        foreach (Trade trade in trades)
                        {
                            if (trade.Receiver == active_username && trade.Status == Tradestatus.Pending)
                            {
                                System.Console.WriteLine($"{trade.Sender} wants to trade your item ({trade.ReceiverItems}) for theirs ({trade.SenderItems}) (type '{i}')\n");
                                receivedTrades.Add(new Trade(trade.SenderItems, trade.ReceiverItems, trade.Sender, trade.Receiver, Tradestatus.Pending));
                                ++i;
                            }
                        }
                        if (receivedTrades.Count() <= 0)
                        {
                            System.Console.WriteLine("You haven't received any trade requests");
                            Console.ReadLine();
                            break;
                        }
                        i = 0;
                        System.Console.Write("--> ");
                        int input = Convert.ToInt32(Console.ReadLine()) - 1; // null bug
                        foreach (Trade trade in receivedTrades)
                        {
                            if (input == i)
                            {
                                Console.Clear();
                                System.Console.WriteLine($"Trade your item ({trade.ReceiverItems}) for {trade.Sender}'s item ({trade.SenderItems})?\n");
                                System.Console.WriteLine("Accept (type '1')");
                                System.Console.WriteLine("Deny (type '2')");
                                System.Console.WriteLine("Go back (type 'goback')");
                                System.Console.Write("\n--> ");
                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        foreach (Item item in items)
                                        {
                                            if (item.Name == receivedTrades[i].ReceiverItems && item.Owner == active_username)
                                            {
                                                item.Owner = receivedTrades[i].Sender;
                                            }
                                            if (item.Name == receivedTrades[i].SenderItems && item.Owner == receivedTrades[i].Sender)
                                            {
                                                item.Owner = active_username;
                                            }
                                            // gör traden till approved
                                        }
                                        foreach (Trade t in trades)
                                        {
                                            if (t.Receiver == active_username && t.Sender == receivedTrades[i].Sender && t.ReceiverItems == receivedTrades[i].ReceiverItems && t.SenderItems == receivedTrades[i].SenderItems)
                                            {
                                                t.Status = Tradestatus.Approved;
                                            }
                                        }
                                        Console.Clear();
                                        System.Console.WriteLine("Trade completed!");
                                        Console.ReadLine();
                                        break;

                                    case "2":
                                        Console.Clear();
                                        System.Console.WriteLine("Trade denied!");
                                        foreach (Trade t in trades)
                                        {
                                            if (t.Receiver == active_username && t.Sender == receivedTrades[i].Sender && t.ReceiverItems == receivedTrades[i].ReceiverItems && t.SenderItems == receivedTrades[i].SenderItems)
                                            {
                                                t.Status = Tradestatus.Denied;
                                            }
                                        }
                                        Console.ReadLine();
                                        break;
                                }
                            }
                            ++i;
                        }
                        break;

                    case "2":
                        Console.Clear();
                        foreach (Trade trade in trades)
                        {
                            if (trade.Sender == active_username && trade.Status == Tradestatus.Pending)
                            {
                                System.Console.WriteLine($"You want to trade your item ({trade.SenderItems}) for {trade.Receiver}'s item ({trade.ReceiverItems}).\n");
                            }
                        }
                        Console.ReadLine();
                        break;

                    case "3":
                        Console.Clear();
                        foreach (Trade trade in trades)
                        {
                            if (trade.Receiver == active_username && trade.Status == Tradestatus.Approved)
                            {
                                System.Console.WriteLine($"You traded your item ({trade.ReceiverItems}) for {trade.Sender}'s item ({trade.SenderItems})");
                            }
                            if (trade.Sender == active_username && trade.Status == Tradestatus.Approved)
                            {
                                System.Console.WriteLine($"You traded your item ({trade.SenderItems}) for {trade.Receiver}'s item ({trade.ReceiverItems})");
                            }
                        }
                        Console.ReadLine();
                        break;

                    case "4":
                        Console.Clear();
                        foreach (Trade trade in trades)
                        {
                            if (trade.Receiver == active_username && trade.Status == Tradestatus.Denied)
                            {
                                System.Console.WriteLine($"You didn't trade your item ({trade.ReceiverItems}) for {trade.Sender}'s item ({trade.SenderItems})");
                            }
                            if (trade.Sender == active_username && trade.Status == Tradestatus.Denied)
                            {
                                System.Console.WriteLine($"You didn't trade your item ({trade.SenderItems}) for {trade.Receiver}'s item ({trade.ReceiverItems})");
                            }
                        }
                        Console.ReadLine();
                        break;
                }
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
