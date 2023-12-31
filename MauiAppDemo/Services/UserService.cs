using MauiAppDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiAppDemo.Services
{
    public static class UserService
    {
        public static List<User> GetAll()
        {
            string appUsersFilePath = Utils.GetUserDirectory();
            if (!File.Exists(appUsersFilePath))
                return new List<User>();

            var json = File.ReadAllText(appUsersFilePath);
            if (json.Trim().Length > 0)
            {
                var deserializedData = JsonSerializer.Deserialize<List<User>>(json);
                if (deserializedData != null)
                    return deserializedData;
            }

            return new List<User>();
        }

        private static void SaveAll(List<User> users)
        {
            string appUsersFilePath = Utils.GetUserDirectory();
            var json = JsonSerializer.Serialize(users);
            File.WriteAllText(appUsersFilePath, json);
        }

        public static List<User> Create(Guid userId, string username, string password, Role role)
        {
            List<User> users = GetAll();
            bool usernameExists = users.Any(x => x.Username == username);

            if (usernameExists)
            {
                throw new Exception("Username already exists.");
            }

            users.Add(
                new User
                {
                    Username = username,
                    PasswordHash = Utils.HashSecret(password),
                    Role = role,
                    CreatedBy = userId
                }
            );
            SaveAll(users);
            return users;
        }

        public static void SeedUsers()
        {
            var users = GetAll().FirstOrDefault(x => x.Role == Role.Admin);

            if (users == null)
            {
                Create(Guid.Empty, "admin", "password", Role.Admin);
            }
        }
    }
}
