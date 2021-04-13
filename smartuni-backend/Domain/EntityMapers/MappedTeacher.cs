using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityMapers
{
    public class MappedTeacher
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public List<MappedCourse> Courses { get; set; }
    }
    public static class TeacherMapper
    {
        public static MappedTeacher MapWithCourses(this Teacher teacher)
        {
            return new MappedTeacher()
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Age = teacher.Age,
                Courses = teacher.Courses != null ? teacher.Courses.Select(x => x.MapWithoutTeachers()).ToList() : new List<MappedCourse>(),
            };
        }

        public static MappedTeacher MapWithoutCourses(this Teacher teacher)
        {
            return new MappedTeacher()
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Age = teacher.Age,
                Courses = new List<MappedCourse>(),
            };
        }
    }
}
