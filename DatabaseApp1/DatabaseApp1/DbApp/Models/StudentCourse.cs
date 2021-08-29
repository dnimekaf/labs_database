using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseApp1.DbApp.Models
{
    [Table(("students_courses"))]
    public class StudentCourse
    {
        [Column("student_id")]
        public int StudentId { get; set; }
        
        [Column("course_id")]
        public int CourseId { get; set; }
        
        public Student StudentEntity { get; set; }
        
        public Course CourseEntity { get; set; }
        
    }
}