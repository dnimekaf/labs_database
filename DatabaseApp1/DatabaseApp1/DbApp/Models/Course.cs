using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseApp1.DbApp.Models
{
    [Table("courses")]
    public class Course
    {
        [Key, Column("id")]
        public int Id { get; set; }
        
        [Column("course_name")]
        public string Name { get; set; }
        
        [Column("course_start")]
        public DateTime Start { get; set; }
        
        [Column("course_end")]
        public DateTime End { get; set; }
        
        public virtual ICollection<Lecture> Lectures { get; set; }
        
        public virtual ICollection<StudentCourse> Students { get; set; }

        public override string ToString()
        {
            return $"Course {Name} with id {Id} starts on {Start:d}, ends on {End:d}, has {Lectures?.Count ?? 0} lectures and {Students?.Count ?? 0} students";
        }
    }
}