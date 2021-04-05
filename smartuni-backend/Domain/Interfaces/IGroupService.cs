using System.Collections.Generic;
using Domain.EntityMapers;

namespace Domain.Interfaces
{
	public interface IGroupService
	{
		public MappedGroup CreateGroup(string name, int year);
		public MappedGroup UpdateGroupStudents(string groupId, List<string> studentIds);
		public List<MappedGroup> GetAll();
		public MappedGroup GetById(string id);
	}
}
