using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseApp1.DbApp.Models
{
    [Table("lectures")]
    public class Lecture
    {
        [Key, Column("id")]
        public int Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("course_id")]
        public int CourseId { get; set; }
        
        public Course CourseEntity { get; set; }
        
        [Column("description")]
        public string Description { get; set; }
        
        [Column("date")]
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return
                $"Lecture {Name} belongs to course {CourseEntity?.Name ?? CourseId.ToString()} will be at {Date:d} and will tell you about {Description}";
        }
    }
}