using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.EntityMapers;
using Domain;
using FirebaseAdmin.Auth;

namespace Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> studentRepository;
        private readonly IFirebaseService firebaseService;
        public StudentService(IRepository<Student> studentRepository, IFirebaseService firebaseService)
        {
            this.studentRepository = studentRepository;
            this.firebaseService = firebaseService;
        }
        public async Task<MappedStudent> Create(string firstName, string lastName, string email, string phoneNumber, string numericCode)
        {
            var claims = new Dictionary<string, object>()
                {
                    { "student", true },
                };
            UserRecord newStudentResult = await firebaseService.CreateFirebaseAccount(email, numericCode, claims);
            Student createdStudent = studentRepository.Create(Student.Create(newStudentResult.Uid, firstName, lastName, email, phoneNumber, numericCode));
            studentRepository.Save();
            if (createdStudent != null)
            {
                return createdStudent.Map();
            }
            return null;
        }

        public async Task DeleteStudents(string[] studentIds)
        {
           DeleteUsersResult deleteUsersResult = await FirebaseAuth.DefaultInstance.DeleteUsersAsync(studentIds.ToList());
           studentIds.ToList().ForEach(x =>
           {
               var student = studentRepository.GetById(x);
               if(student != null)
               {
                    studentRepository.Delete(student);
               }
           });
            studentRepository.Save();
        }

        public List<MappedStudent> GetAll()
        {
            return studentRepository.GetAll().Select(x => x.Map()).ToList();
        }

        public List<MappedStudent> GetByGroupId(string groupId)
        {
            try
            {
                return studentRepository.GetAll()
                    .Where(x => x.Group != null && x.Group.Id == Guid.Parse(groupId))
                    .Select(x => x.Map())
                    .ToList();
            }
            catch
            {
                return new List<MappedStudent>();
            }
        }

        public MappedStudent GetById(string id)
        {
            return studentRepository.GetById(id).Map();
        }
    }
}
