using System;
using DAL.Database;
using DAL.Models;
using System.Linq;
using System.Collections.Generic;

namespace DatabaseTester
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyDbContext context = new MyDbContext())
            {
                DbInitializer.Initialize(context);

                Hotel hotel = context.Hotels.FirstOrDefault();
                Console.WriteLine($"hotel name: {hotel.Name}");

                System.Console.WriteLine("Rooms:");
                foreach (Room room in context.Rooms.ToList())
                    Console.WriteLine($"room number: {room.Number}, Price: {room.Price}");

                System.Console.WriteLine("Users:");
                foreach (User user in context.Users.ToList())
                    Console.WriteLine($"user name: {user.Name}, email: {user.Email} ");
            }
        }
    }
}
