using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityMapers
{
    public class MappedCourse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }
        public List<MappedTeacher> Teachers { get; set; }
    }
    public static class CourseMapper
    {
        public static MappedCourse MapWithTeachers(this Course course)
        {
            return new MappedCourse()
            {
                Id = course.Id,
                Name = course.Name,
                Credits = course.Credits,
                Description = course.Description,
                Year = course.Year,
                Semester = course.Semester,
                Teachers = course.Teachers != null ? course.Teachers.Select(x => x.MapWithoutCourses()).ToList() : new List<MappedTeacher>(),
            };
        }

        public static MappedCourse MapWithoutTeachers(this Course course)
        {
            return new MappedCourse()
            {
                Id = course.Id,
                Name = course.Name,
                Credits = course.Credits,
                Description = course.Description,
                Year = course.Year,
                Semester = course.Semester,
                Teachers = new List<MappedTeacher>(),
            };
        }
    }
}
