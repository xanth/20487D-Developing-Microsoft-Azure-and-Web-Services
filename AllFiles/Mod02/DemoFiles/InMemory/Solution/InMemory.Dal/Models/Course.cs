using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InMemory.Dal.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int CourseId { get; set; }
        public virtual string Name { get; set; }
        public virtual Teacher CourseTeacher { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}