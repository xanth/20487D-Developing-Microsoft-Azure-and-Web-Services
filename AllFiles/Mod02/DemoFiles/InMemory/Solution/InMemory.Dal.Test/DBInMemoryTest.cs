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
        private DbContextOptions<SchoolContext> options = new DbContextOptionsBuilder<SchoolContext>()
                                                              .UseInMemoryDatabase(databaseName: "TestDatabase")
                                                              .Options;

        [TestMethod]
        public void AddStudentTest()
        {
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

        [TestMethod]
        public void UpdateTeacherSalaryTest()
        {
            Teacher teacher = new Teacher {Name = "Terry Adams" , Salary = 10000};
            using (var context = new SchoolContext(options))
            {
                DbInitializer.Initialize(context);
                teacher = context.Teachers.Add(teacher).Entity;
                context.SaveChanges();
                teacher.Salary += 10000;
                context.Teachers.Update(teacher);
                context.SaveChanges();
            }

            using(var context = new SchoolContext(options))
            {
                var result = context.Teachers.FirstOrDefault((s)=> s.PersonId == teacher.PersonId);
                
                Assert.AreEqual(result.Salary,20000);
            }
        }

    }
}
