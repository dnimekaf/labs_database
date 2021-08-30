using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseApp1.DbApp.Models;
using DatabaseApp1.DbApp.OtusContext;
using Microsoft.EntityFrameworkCore;

namespace DatabaseApp1.DbApp
{
    public class DbGateway
    {
        private readonly string _connectionString;

        public DbGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Course>> FetchCourses()
        {
            await using var context = new Context(_connectionString);
            return await context.Courses
                .Include(x => x.Students)
                .Include(x => x.Lectures)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lecture>> FetchLectures()
        {
            await using var context = new Context(_connectionString);
            return await context.Lectures
                .Include(x => x.CourseEntity)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Student>> FetchStudents()
        {
            await using var context = new Context(_connectionString);
            return await context.Students.Include(x => x.Courses)
                .ThenInclude(x => x.CourseEntity)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<Course> AddCourse(string name, DateTime startDate, DateTime endDate)
        {
            var course = new Course
            {
                Name = name,
                Start = startDate,
                End = endDate
            };
            await using var context = new Context(_connectionString);
            context.Courses.Add(course);
            await context.SaveChangesAsync();
            return course;
        }

        public async Task<Lecture> AddLecture(string name, int courseId, DateTime date, string description)
        {
            var lecture = new Lecture
            {
                Name = name,
                Description = description,
                Date = date,
                CourseId = courseId
            };
            
            await using var context = new Context(_connectionString);
            context.Lectures.Add(lecture);
            await context.SaveChangesAsync();
            return lecture;
        }

        public async Task<Student> AddStudent(string firstName, string lastName, DateTime registrationDate)
        {
            var student = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                RegistrationDate = registrationDate
            };
            await using var context = new Context(_connectionString);
            context.Students.Add(student);
            await context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> IsCourseExists(int courseId)
        {
            await using var context = new Context(_connectionString);
            return await context.Courses.AnyAsync(x => x.Id == courseId);
        }
    }
}