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
    }
}