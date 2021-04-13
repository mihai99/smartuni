using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.EntityMapers;

namespace Domain.Interfaces
{
    public interface IStudentService
    {
        public List<MappedStudent> GetAll();
        public MappedStudent GetById(string id);

        public Task DeleteStudents(string[] studentIds);
        public Task<MappedStudent> Create(string firstName, string lastName, string email, string phoneNumber, string numericCode);
        public List<MappedStudent> GetByGroupId(string groupId);
    }
}
