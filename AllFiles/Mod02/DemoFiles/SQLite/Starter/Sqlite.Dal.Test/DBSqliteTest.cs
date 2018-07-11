using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sqlite.Dal.Database;
using Microsoft.EntityFrameworkCore;
using Sqlite.Dal.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using Sqlite.Dal.Repository;

namespace Sqlite.Dal.Test
{
    [TestClass]
    public class DBSqliteTest
    {
        private StudentRepository _studentRepository;
        private TeacherRepository _teacherRepository;

        public DBSqliteTest()
        {
            _studentRepository = new StudentRepository(_options);
            _teacherRepository = new TeacherRepository(_options);
        }

        [TestMethod]
        public void AddStudentTest()
        {
            Student student = new Student { Name = "Kari Hensien" };
            student = _studentRepository.Add(student);

            using (var context = new SchoolContext(_options))
            {
                var result = context.Students.FirstOrDefault((s) => s.PersonId == student.PersonId);
                Assert.IsNotNull(result);
            }
        }
    }
}
