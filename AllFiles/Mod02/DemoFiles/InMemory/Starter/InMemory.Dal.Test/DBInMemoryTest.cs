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
        public void AddStudentTest()
        {
            Student student = new Student {Name = "Kari Hensien"};
            using (var context = new SchoolContext(_options))
            {
                DbInitializer.Initialize(context);
                student = context.Students.Add(student).Entity;
                context.SaveChanges();
            }

            using(var context = new SchoolContext(_options))
            {
                var result = context.Students.FirstOrDefault((s)=> s.PersonId == student.PersonId);                
                Assert.IsNotNull(result);
            }
        }
   }
}
