using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sqlite.Dal.Models
{
    public class Teacher : Person
    {
        public decimal Salary { get; set; }
    }
}