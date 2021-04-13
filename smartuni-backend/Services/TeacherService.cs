using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Interfaces;
using FirebaseAdmin.Auth;
using Domain.EntityMapers;

namespace Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IRepository<Course> courseRepository;
        private readonly IRepository<Teacher> teacherRepository;
        private readonly IFirebaseService firebaseService;

        public TeacherService(IRepository<Course> courseRepository, IRepository<Teacher> teacherRepository, IFirebaseService firebaseService)
        {
            this.courseRepository = courseRepository;
            this.teacherRepository = teacherRepository;
            this.firebaseService = firebaseService;
        }

        public async Task<MappedTeacher> CreateTeacherAccount(string firstName, string lastName, string email, int age)
        {
            var claims = new Dictionary<string, object>()
                {
                    { "teacher", true },
                };
            UserRecord newteacherResult = await firebaseService.CreateFirebaseAccount(email, "teacherPassword123", claims);
            Teacher createdTeacher = teacherRepository.Create(Teacher.CreateTeacher(newteacherResult.Uid, firstName, lastName, email, age));
            teacherRepository.Save();
            if (teacherRepository != null)
            {
                return createdTeacher.MapWithoutCourses();
            }
            return null;
        }

        public async Task<bool> DeleteTeacher(string id)
        {
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(id);
            var teacher = this.teacherRepository.GetById(id);
            if(teacher != null)
            {
                teacher.Courses.ForEach(x => {
                    x.Teachers.Remove(teacher);
                    courseRepository.Update(x);
                });
                teacherRepository.Delete(teacher);
                teacherRepository.Save();
            }
            return true;
        }
        public List<MappedCourse> AsignCourse(string teacherId, string courseId)
        {
            Teacher teacher = teacherRepository.GetById(teacherId);
            Course course = courseRepository.GetById(courseId);
            if (teacher == null || course == null)
            {
                return null;
            }
            if(teacher.Courses != null && !teacher.Courses.Contains(course))
            {
                teacher.Courses.Add(course);
            }
            if (course.Teachers != null && !course.Teachers.Contains(teacher))
            {
                course.Teachers.Add(teacher);
            }
            courseRepository.Update(course);
            teacherRepository.Update(teacher);
            teacherRepository.Save();
            return teacher.Courses.Select(x => x.MapWithoutTeachers()).ToList(); ;


        }
        public List<MappedCourse> RemoveCourse(string teacherId, string courseId)
        {
            Teacher teacher = teacherRepository.GetById(teacherId);
            Course course = courseRepository.GetById(courseId);
            if (teacher == null || course == null)
            {
                return null;
            }
            if (teacher.Courses.Contains(course))
            {
                teacher.Courses.Remove(course);
            }
            if (course.Teachers.Contains(teacher))
            {
                course.Teachers.Remove(teacher);
            }
            courseRepository.Update(course);
            teacherRepository.Update(teacher);
            teacherRepository.Save();
            return teacher.Courses.Select(x => x.MapWithoutTeachers()).ToList();
        }

        public List<MappedTeacher> GetAll()
        {
            return teacherRepository.GetAll().Select(x => x.MapWithCourses()).ToList();
        }
    }
}
