using App;

string active_username = ""; // variabel för namnet på användaren som är inloggad

List<User> users = DataStorage.LoadUsers(); // lista med alla användare
if (users.Count == 0)
{
    users.Add(new User("kd", "asd"));
    users.Add(new User("jonas", "asd"));
    users.Add(new User("fisk", "asd"));
    users.Add(new User("bo", "asd"));
    DataStorage.SaveUsers(users);
}

List<Item> items = DataStorage.LoadItems(); // lista med alla items
if (items.Count == 0)
{
    items.Add(new Item("7up Zero", "A warm 7up that expired around three years ago.", users[0].Username));
    items.Add(new Item("Pack of cards", "Contains 43 cards and the package is broken. Would like to trade it for a new deck if possible.", users[0].Username));
    items.Add(new Item("Red T-Shirt", "Medium Size", users[1].Username));
    DataStorage.SaveItems(items);
}

List<Trade> trades = DataStorage.LoadTrades(); // lista med alla trades oavsett status
if (trades.Count == 0)
{
    trades.Add(new Trade(items[2].Name, items[0].Name, users[1].Username, users[0].Username, Tradestatus.Pending));
    DataStorage.SaveTrades(trades);
}

bool running = true;
while (running)
{
    if (active_username == "") // utloggad pga att användarens username är tomt
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
            case "1": // om input är == 1, påbörjas inloggnings-caset
                Console.Clear();
                System.Console.Write("Username: ");
                string username = Console.ReadLine(); // sparar det username som användaren skriver in
                System.Console.Write("Password: ");
                string password = Console.ReadLine(); // samma fast med password
                foreach (User user in users) // går igenom varje user i users-listan
                {
                    if (username == user.Username && password == user.Password) // om det username OCH password som användaren skrivit in stämmer med något i user-listan...
                    {
                        Console.Clear();
                        active_username = username; // ...inloggad, eftersom active_username inte är tom längre...
                        break; // ...hoppar därför till else satsen efter break
                    }
                }
                if (active_username == "") // om det username som användaren skrev in inte finns i user-listan...
                {
                    System.Console.WriteLine("\nIncorrect username or password.");
                    Console.ReadLine();
                    break; // ...börja om med inloggningen
                }
                break;

            case "2": // om input är == 2, påbörjas registrerings-caset
                Console.Clear();
                System.Console.Write("Username: ");
                username = Console.ReadLine(); // samma som i case 1
                System.Console.Write("Password: ");
                password = Console.ReadLine(); // -||-
                users.Add(new User(username, password)); // lägger till dom sparade stringsen i user-listan så man senare kan logga in genom det kontot
                DataStorage.SaveUsers(users);
                Console.Clear();
                System.Console.WriteLine($"{username} created.");
                Console.ReadLine();
                break;

            case "quit": // om input är == quit...
                Console.Clear();
                running = false; // ...bryts while-loopen
                break; 
        }
    }
    else // inloggad eftersom användarens username inte är tomt längre
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
            case "1": // påbörjar uppladdningen av ett item
                Console.Clear();
                System.Console.Write("Name of item: ");
                string name = Console.ReadLine(); // sparar namnet av itemet
                System.Console.Write("Write a description about the item: ");
                string description = Console.ReadLine(); // sparar beskrivningen av itemet
                while (true) // ifall användaren senare skriver något som inte är yes eller no så börjar den om
                {
                    Console.Clear();
                    System.Console.WriteLine($"Upload {name}? ('yes'/'no')\n ");
                    System.Console.Write("--> ");
                    string upload = Console.ReadLine();
                    if (upload.ToLower() == "yes") // om användaren skriver "yes" (oavsett små eller stora bokstäver)...
                    {
                        items.Add(new Item(name, description, active_username)); // ...läggs itemet till i item-listan
                        DataStorage.SaveItems(items);
                        System.Console.WriteLine($"Your item ({name}) was uploaded!");
                        Console.ReadLine();
                        break; // går tillbaks till menyn
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

            case "2": // påbörjar att skriva ut alla andra användares items
                while (true) 
                {
                    try // undviker errors
                    {
                        List<Item> othersItems = new List<Item>(); // tom lista som ska innehålla andra användares items
                        Console.Clear();
                        int i = 1; // skapar en variable som senare ska öka med +1 varje gång "foreach" loopar, så varje item blir numrerat
                        foreach (Item item in items) // går igenom varje item i item-listan
                        {
                            if (item.Owner != active_username) // om namnet på den som äger itemet är samma som den som är inloggad...
                            {
                                System.Console.WriteLine($"Item: {item.Name}\nOwner: {item.Owner}\n(type '{i}')\n"); // ...skriver ut namnet på itemet och ägarens namn
                                othersItems.Add(new Item(item.Name, item.Description, item.Owner)); // ...lägger till itemet i listan som skapades innan
                                ++i; // ...ökar i med +1
                            }
                        }
                        System.Console.WriteLine("Type 'goback' to go back\n");
                        System.Console.Write("--> ");
                        string backinput = Console.ReadLine();
                        if (backinput == "goback") // om användaren skriver "goback"...
                        {
                            Console.Clear();
                            break; // ...gå tillbaks till menyn
                        }
                        int input;
                        int input2;
                        int.TryParse(backinput, out input); // inputen måste vara int här (tror jag) eftersom vi subtraherar den med 1 nedan
                        input = input - 1; // -1 eftersom listor börjar på 0, så det som användaren skriver matchar med listan
                        if (input > othersItems.Count()) // om input är större än så många items som finns i den nya listan...
                        {
                            continue; // ...börja om while loopen
                        }
                        Console.Clear(); // visar mer info om det item man vill se (nedan)
                        System.Console.WriteLine($"Item: {othersItems[input].Name}\n\nDescription: {othersItems[input].Description}\n\nOwner: {othersItems[input].Owner}");
                        System.Console.WriteLine("\n -- -- -- -- -- -- \n\nRequest trade? ('yes'/'no')");
                        System.Console.Write("--> ");
                        string requestTrade = Console.ReadLine();
                        switch (requestTrade.ToLower()) // frågar om man vill gå vidare i traden
                        {
                            case "yes":
                                List<Item> yourItems = new List<Item>(); // tom lista med ens egna items
                                foreach (Item item in items) // lägger till ens egna items i den separata listan om användarens username stämmer överrens med itemets owner
                                {
                                    if (active_username == item.Owner)
                                    {
                                        yourItems.Add(new Item(item.Name, item.Description, item.Owner));
                                    }
                                }
                                if (yourItems.Count() <= 0) // om man inte har några items...
                                {
                                    System.Console.WriteLine("\nYou don't have any items to trade with...");
                                    Console.ReadLine();
                                    break; // ...kan inte fortsätta traden och bryts ut
                                }
                                Console.Clear();
                                System.Console.WriteLine("Which item of yours would you like to trade?");
                                i = 1; // återställer i till 1 så den nya "listan" av items kan bli numrerad
                                foreach (Item item in yourItems) // visar alla ens egna items
                                {
                                    System.Console.WriteLine($"{item.Name}\n(type '{i}')\n");
                                    ++i; // ökar i med +1
                                }
                                System.Console.WriteLine("Type 'goback' to go back\n");
                                System.Console.Write("--> ");
                                input2 = Convert.ToInt32(Console.ReadLine()) - 1; // -1 igen så att input2 matchar med listan
                                if (Convert.ToString(input2) == "goback") // om använder skriver goback så bryts loopen
                                {
                                    Console.Clear();
                                    break;
                                }
                                if (input2 > yourItems.Count()) // om användaren skriver ett högre nummer än antalet egna items som finns...
                                {
                                    continue; // ...börja om
                                }
                                Console.Clear();
                                System.Console.WriteLine($"You give: {yourItems[input2].Name}");
                                System.Console.WriteLine($"{othersItems[input].Owner} gives: {othersItems[input].Name}");
                                System.Console.WriteLine("\nConfirm trade request? ('yes'/'no')");
                                switch (Console.ReadLine()) // confirmar så användaren faktiskt vill utföra traden
                                {
                                    case "yes":
                                        Console.Clear(); // lägger till traden i trades (nedan)
                                        trades.Add(new Trade(yourItems[input2].Name, othersItems[input].Name, active_username, othersItems[input].Owner, Tradestatus.Pending));
                                        DataStorage.SaveTrades(trades);
                                        System.Console.WriteLine($"Trade request was sent to {othersItems[input].Owner}");
                                        Console.ReadLine();
                                        break; // bryter loopen eftersom man är klar

                                    case "no":
                                        Console.Clear();
                                        break; // bryter loopen utan att utföra traden

                                    default: // om något annat än yes eller no skrivs...
                                        continue; // ...börja om
                                }
                                break;

                            case "no": // går tillbaks eftersom man inte vill skicka en trade request
                                Console.Clear();
                                break;

                            default: // börja om ifall något annat skrivs
                                continue;
                        }

                    }
                    catch // börja om ifall något error sker
                    {
                        continue;
                    }
                }
                break;

            case "3": // påbörjar att visa trade requests
                Console.Clear();
                System.Console.WriteLine("Received trade requests [Pending] (type '1')");
                System.Console.WriteLine("Sent trade requests [Pending] (type '2')");
                System.Console.WriteLine("Approved trade requests (type '3')");
                System.Console.WriteLine("Denied trade requests (type '4')\n");
                System.Console.Write("--> ");
                switch (Console.ReadLine()) // användaren får välja vilka typ av trades som ska visas
                {
                    case "1":
                        while (true)
                        {
                            try
                            {
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
                                int input = Convert.ToInt32(Console.ReadLine()) - 1;
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
                                                DataStorage.SaveItems(items);
                                                DataStorage.SaveTrades(trades);
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
                                                DataStorage.SaveTrades(trades);
                                                Console.ReadLine();
                                                break;
                                        }
                                    }
                                    ++i;
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                        break;

                    case "2": // visar trades man har skickat till andra som är pending
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

                    case "3": // visar approved trades
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

                    case "4": // visar denied trades
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
                active_username = "";
                break;

            default:
                continue;
        }
    }
}
