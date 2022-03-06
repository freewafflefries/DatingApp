using System.Text;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return; //If we already have User records, abort!

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json"); //Otherwise, read our JSON file full of dummy data
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            //Our dummy data JSON file doesn't have the password property included (don't want random generated passwords for dummy data)
            //But, PasswordHash & PasswordSald it is a required property of the AppUser model
            //So we need to iterate over our List of AppUser's, and assign a password hash.
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }

            //Now we save the changes (all our dummy seed data)
            await context.SaveChangesAsync();
        }
    }
}