using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sqlite.Dal.Database;
using Microsoft.EntityFrameworkCore;
using Sqlite.Dal.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Sqlite.Dal.Test
{
    [TestClass]
    public class DBSqliteTest
    {


        [TestMethod]
        public void AddStudentTest()
        {
             var options = new DbContextOptionsBuilder<SchoolContext>()
                  .UseSqlite(@"Data Source = D:\MicrosoftLearning\20487D-Developing-Microsoft-Azure-and-Web-Services\AllFiles\Mod02\DemoFiles\SQLite\Database\SqliteSchool.db")
                  .Options;

            Student student = new Student {Name = "Kari Hensien"};
            using (var context = new SchoolContext(options))
            {
                DbInitializer.Initialize(context);
                student = context.Students.Add(student).Entity;
                context.SaveChanges();
            }

            using(var context = new SchoolContext(options))
            {
                var result = context.Students.FirstOrDefault((s)=> s.PersonId == student.PersonId);                
                Assert.IsNotNull(result);
            }
        }
    }
}
