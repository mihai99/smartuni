using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IGroupService
	{
		public Group CreateGroup(string name, int year);
		public Group UpdateGroupStudents(string groupId, List<string> studentIds);
		public List<Group> GetAll();
		public Group GetById(string id);
	}
}
