using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Course
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Credits { get; private set; }
        public string Description { get; private set; }
        public int Year { get; set; }

        public int Semester { get; private set; }
        public List<Teacher> Teachers { get; private set; }

        private Course() {}

        public static Course Create(string name, string description, int credits, int year, int semester)
        {
            return new Course()
            {
                Name = name,
                Description = description,
                Credits = credits,
                Year = year,
                Semester = semester,
                Teachers = new List<Teacher>(),
            };
        }
        public List<Teacher> AddTeacher(Teacher t)
        {
            this.Teachers.Add(t);
            return this.Teachers;
        }
        public List<Teacher> RemoveTeacher(Teacher t)
        {
            this.Teachers.Remove(t);
            return this.Teachers;
        }
    }
}
