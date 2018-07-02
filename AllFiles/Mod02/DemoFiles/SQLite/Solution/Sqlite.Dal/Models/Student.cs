using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sqlite.Dal.Models
{
    public class Student : Person
    {
        public int Grade { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}