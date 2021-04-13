using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class Student
	{
		public string Id { get; set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string Email { get; private set; }
		public string PhoneNumber { get; private set; }
		public string NumericCode { get; private set; }
		public bool HasChangedPassword { get; private set; }
		public Group? Group { get; set; }
		private Student() {}

		public static Student Create(string Id, string FirstName, string LastName, string Email, string PhoneNumber, string NumericCode)
		{
			return new Student()
			{
				Id = Id + (new Random().Next(0, 1000).ToString()),
				FirstName = FirstName,
				LastName = LastName,
				Email = Email,
				PhoneNumber = PhoneNumber,
				NumericCode = NumericCode,
				HasChangedPassword = false,
			};
		}

	}
}
