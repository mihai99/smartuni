using System;
using System.Collections.Generic;
using System.Linq;
using Domain.EntityMapers;
using Domain;
using Domain.Interfaces;

namespace Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> courseRepository;
        private readonly IRepository<Teacher> teacherRepository;

        public CourseService(IRepository<Course> courseRepository, IRepository<Teacher> teacherRepository)
        {
            this.courseRepository = courseRepository;
            this.teacherRepository = teacherRepository;
        }

        public Course CreateCourse(string name, string description, int credits, int year, int semester)
        {
            Course course = Course.Create(name, description, credits, year, semester);
            courseRepository.Create(course);
            courseRepository.Save();
            return course;
        }

        public bool DeleteCourse(string id)
        {
            var course = this.courseRepository.GetById(id);
            if(course != null)
            {
            course.Teachers.ForEach(x => {
                x.Courses.Remove(course);
                teacherRepository.Update(x);
            });
            courseRepository.Delete(course);
            courseRepository.Save();
            }
            return true;
        }

        public List<MappedCourse> GetAll()
        {
            return courseRepository.GetAll().Select(x => x.MapWithTeachers()).ToList();
        }
    }
}
