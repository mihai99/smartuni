using System;

namespace Domain.EntityMapers
{
    public class MappedStudent
    {
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string NumericCode { get;  set; }
        public int Year { get; set; }
        public string GroupName { get; set; }
        public Guid GroupId { get; set; }
    }

    public static class StudentMapper
    {
        public static MappedStudent Map(this Student student)
        {
            return new MappedStudent()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                NumericCode = student.NumericCode,
                Year = student.Group != null ? student.Group.Year : 0,
                GroupName = student.Group != null ? student.Group.Name : "",
                GroupId = student.Group != null ? student.Group.Id : Guid.Empty,

            };
        }
    }
}
