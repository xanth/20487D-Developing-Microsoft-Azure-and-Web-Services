
using System;
using System.Collections.Generic;
using Sqlite.Dal.Models;
namespace Sqlite.Dal.Database
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            // Code to create initial data
            context.Database.EnsureCreated();
        }
    }
}