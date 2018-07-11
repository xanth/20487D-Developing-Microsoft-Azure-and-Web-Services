using Sqlite.Dal.Models;
using Sqlite.Dal.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Sqlite.Dal.Repository
{
    public class TeacherRepository
    {
        private DbContextOptions<SchoolContext> _options;
        public TeacherRepository(DbContextOptions<SchoolContext> options)
        {
            _options = options;
        }

        public Teacher Add(Teacher teacher)
        {
            try
            {
                using (var context = new SchoolContext(_options))
                {
                    DbInitializer.Initialize(context);
                    var result = context.Teachers.Add(teacher).Entity;
                    context.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Teacher Update(Teacher teacher)
        {
            try
            {
                using (var context = new SchoolContext(_options))
                {
                    DbInitializer.Initialize(context);
                    var result = context.Teachers.Update(teacher).Entity;
                    context.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}