using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DatabaseApp1.DbApp.Models
{
    [Table("students")]
    public class Student
    {
        [Key, Column("id")]
        public int Id { get; set; }
        
        [Column("first_name")]
        public string FirstName { get; set; }
        
        [Column("last_name")]
        public string LastName { get; set; }
        
        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; }
        
        public virtual ICollection<StudentCourse> Courses { get; set; }

        public override string ToString()
        {
            var courses = Courses != null
                ? string.Join(", ", Courses.Select(x => x.CourseEntity.Name).ToList())
                : string.Empty;
            
            return $"{FirstName} {LastName} registered at {RegistrationDate:d} and attends to course(s) {courses}";
        }
    }
}