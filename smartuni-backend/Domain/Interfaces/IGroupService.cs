using System.Collections.Generic;
using Domain.EntityMapers;

namespace Domain.Interfaces
{
	public interface IGroupService
	{
		public MappedGroup CreateGroup(string name, int year);
		public List<MappedStudent> AddStudentsToGroup(string groupId, List<string> studentIds);
		public List<MappedStudent> RemoveStudentsToGroup(string groupId, List<string> studentIds);
		public List<MappedGroupWithStudents> GetAllWithStudents();
		public List<MappedGroup> GetAll();
		public MappedGroup GetById(string id);
		public bool DeleteGroup(string id);
	}
}
