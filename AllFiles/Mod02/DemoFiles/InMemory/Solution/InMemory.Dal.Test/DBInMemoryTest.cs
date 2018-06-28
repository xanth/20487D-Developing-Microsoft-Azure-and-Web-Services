using Microsoft.VisualStudio.TestTools.UnitTesting;
using InMemory.Dal.Database;
using Microsoft.EntityFrameworkCore;
using InMemory.Dal.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace InMemory.Dal.Test
{
    [TestClass]
    public class DBInMemoryTest
    {
        [TestMethod]
        public void GetDataFromDB()
        {
            var options = new DbContextOptionsBuilder<SchoolContext>()
                                .UseInMemoryDatabase(databaseName: "TestDatabase")
                                .Options;

            using(var context = new SchoolContext(options))
            {
                DbInitializer.Initialize(context);
                List<Student> studentsList = context.Students.ToList();
                
                Assert.AreEqual(10,studentsList.Count);
            }
        }
    }
}
