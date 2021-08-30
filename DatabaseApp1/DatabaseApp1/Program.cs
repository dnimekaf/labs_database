using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using DatabaseApp1.DbApp;
using Microsoft.Extensions.Configuration;

namespace DatabaseApp1
{
    class Program
    {
        private readonly DbGateway _gateway;
        private const string DateFormat = "yyyy-MM-dd";

        public Program(DbGateway gateway)
        {
            _gateway = gateway;
        }
        
        static async Task Main(string[] args)
        {
            var connectionString = GetConnectionString();
            var gateway = new DbGateway(connectionString);
            var program = new Program(gateway);
            
            Console.WriteLine("Select action:\n1. Show data\n2. Add new data");
            var key = Console.ReadKey();
            if (key.KeyChar == '1')
            {
                Console.WriteLine("\n");
                await program.ShowData();
                return;
            }

            if (key.KeyChar == '2')
            {
                Console.WriteLine("\nSelect desirable entity:\n1. Course\n2. Lecture \n3. Student");
                key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '1':
                    {
                        await program.ReadCourse();
                    } break;
                    case '2':
                    {
                        await program.ReadLecture();
                    } break;
                    case '3':
                    {
                        await program.ReadStudent();
                    } break;
                    default:
                    {
                        Console.WriteLine("Wrong key");                        
                    } break;
                }
            }
        }
        
        static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            var config = builder.Build();
            return config.GetConnectionString("Default");
        }

        private async Task ReadCourse()
        {
            Console.WriteLine("\nEnter course name: ");
            var courseName = Console.ReadLine();
            Console.WriteLine($"Enter start date ({DateFormat}):");
            var startDateTime = ReadDateTime();
            Console.WriteLine($"Enter end date ({DateFormat}):");
            var endDateTime = ReadDateTime();

            var result = await _gateway.AddCourse(courseName, startDateTime, endDateTime);
            Console.WriteLine($"Course added: {result}");
        }

        private async Task ReadLecture()
        {
            Console.WriteLine("\nEnter lecture name: ");
            var name = Console.ReadLine();
            Console.WriteLine($"Enter date ({DateFormat}):");
            var dateTime = ReadDateTime();
            Console.WriteLine("Enter description: ");
            var description = Console.ReadLine();
            Console.WriteLine("Enter course id: ");
            var courseIdStr = Console.ReadLine();
            if (int.TryParse(courseIdStr, out var courseId) == false)
            {
                Console.WriteLine("Wrong course Id");
                return;
            }

            if (await _gateway.IsCourseExists(courseId) == false)
            {
                Console.WriteLine($"Course with id {courseId} does not exist");
                return;
            }
            
            var result = await _gateway.AddLecture(name, courseId, dateTime, description);
            Console.WriteLine($"Lecture added: {result}");
        }

        private async Task ReadStudent()
        {
            Console.WriteLine("\nEnter student first name: ");
            var firstName = Console.ReadLine();
            
            Console.WriteLine("\nEnter student last name: ");
            var lastName = Console.ReadLine();

            Console.WriteLine($"Enter registration date ({DateFormat}):");
            var startDateTime = ReadDateTime();

            var result = await _gateway.AddStudent(firstName, lastName, startDateTime);
            Console.WriteLine($"Student added: {result}");
        }

        private DateTime ReadDateTime()
        {
            var startDateStr = Console.ReadLine();
            if (DateTime.TryParseExact(startDateStr, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dateTime) == false)
            {
                Console.WriteLine("Wrong date value");
            }

            return dateTime;
        }

        private async Task ShowData()
        {
            var courses = await _gateway.FetchCourses();
            Console.WriteLine("Courses\n");
            foreach (var course in courses)
            {
                Console.WriteLine(course);
            }
            Console.WriteLine("\n=======================\n");
            Console.WriteLine("Lectures\n");
            var lectures = await _gateway.FetchLectures();
            foreach (var lecture in lectures)
            {
                Console.WriteLine(lecture);
            }
            
            Console.WriteLine("\n=======================\n");
            Console.WriteLine("Students\n");
            var students = await _gateway.FetchStudents();
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }

       
    }
}