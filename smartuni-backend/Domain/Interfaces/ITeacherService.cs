using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.EntityMapers;

namespace Domain.Interfaces
{
    public interface ITeacherService
    {
        public Task<MappedTeacher> CreateTeacherAccount(string firstName, string lastName, string email, int age);

        public List<MappedTeacher> GetAll();

        public Task<bool> DeleteTeacher(string id);

        public List<MappedCourse> AsignCourse(string teacherId, string courseId);
        public List<MappedCourse> RemoveCourse(string teacherId, string courseId);
    }
}
