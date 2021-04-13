using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class Group
	{
		public Guid Id { get; private set; }
		public int Year { get; private set; }
		public string Name { get; private set; }
		public List<Student> Students { get; private set; }
		public string GroupLeaderId { get; private set; }

        private Group() { }

		public static Group Create(string name, int year)
		{
			return new Group()
			{
				Id = System.Guid.NewGuid(),
				Year = year,
				Name = name,
				Students = new List<Student>(),
			};
		}

		public List<Student> UpdateStudentList(List<Student> students)
		{
			students.ForEach(student => student.Group = this);
			Students.AddRange(students.Where(x => !Students.Contains(x)));
			return Students;
		}
	}
}
