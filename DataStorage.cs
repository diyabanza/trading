using System;
using System.IO;
using System.Collections.Generic;

namespace App
{
    class DataStorage
    {
        public static void SaveUsers(List<User> users)
        {
            List<string> lines = new List<string>();
            for (int i = 0; i < users.Count; i++)
            {
                lines.Add(users[i].Username + "|" + users[i].Password);
            }
            File.WriteAllLines("users.txt", lines);
        }

        public static List<User> LoadUsers()
        {
            List<User> users = new List<User>();
            if (!File.Exists("users.txt")) return users;
            string[] lines = File.ReadAllLines("users.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');
                users.Add(new User(parts[0], parts[1]));
            }
            return users;
        }
        public static void SaveItems(List<Item> items)
        {
            List<string> lines = new List<string>();
            for (int i = 0; i < items.Count; i++)
            {
                lines.Add(items[i].Name + "|" + items[i].Description + "|" + items[i].Owner);
            }
            File.WriteAllLines("items.txt", lines);
        }

        public static List<Item> LoadItems()
        {
            List<Item> items = new List<Item>();
            if (!File.Exists("items.txt")) return items;
            string[] lines = File.ReadAllLines("items.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');
                items.Add(new Item(parts[0], parts[1], parts[2]));
            }
            return items;
        }

        public static void SaveTrades(List<Trade> trades)
        {
            List<string> lines = new List<string>();
            for (int i = 0; i < trades.Count; i++)
            {
                lines.Add(trades[i].SenderItems + "|" + trades[i].ReceiverItems + "|" + trades[i].Sender + "|" + trades[i].Receiver + "|" + trades[i].Status.ToString());
            }
            File.WriteAllLines("trades.txt", lines);
        }

        public static List<Trade> LoadTrades()
        {
            List<Trade> trades = new List<Trade>();
            if (!File.Exists("trades.txt")) return trades;
            string[] lines = File.ReadAllLines("trades.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');
                Tradestatus status = (Tradestatus)Enum.Parse(typeof(Tradestatus), parts[4]);
                trades.Add(new Trade(parts[0], parts[1], parts[2], parts[3], status));
            }
            return trades;
        }
    }
}