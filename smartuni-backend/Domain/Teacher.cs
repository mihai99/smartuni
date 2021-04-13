using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Teacher
    {
        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public int Age { get; private set; }
        public List<Course> Courses { get; private set; }

        private Teacher() {}

        public static Teacher CreateTeacher(string id, string firstName, string lastName, string email, int age)
        {
            return new Teacher()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Age = age,
                Courses = new List<Course>(),
            };
        }
        public List<Course> AddCourse(Course c)
        {
            this.Courses.Add(c);
            return this.Courses;
        }
        public List<Course> RemoveCourse(Course c)
        {
            this.Courses.Remove(c);
            return this.Courses;
        }


    }
}
