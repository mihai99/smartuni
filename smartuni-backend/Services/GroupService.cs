using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Interfaces;
using Domain.EntityMapers;

namespace Services
{
	public class GroupService : IGroupService
	{
		private readonly IRepository<Group> groupRepository;
		private readonly IRepository<Student> studentRepository;

		public GroupService(IRepository<Group> groupRepository, IRepository<Student> studentRepository)
		{
			this.groupRepository = groupRepository;
			this.studentRepository = studentRepository;
		}
		public MappedGroup CreateGroup(string name, int year)
		{
			var createdGroup = groupRepository.Create(Group.Create(name, year));
			return createdGroup.Map();
		}

		public List<MappedGroup> GetAll()
		{
			return groupRepository.GetAll().Select(x => x.Map()).ToList();
		}

		public MappedGroup GetById(string id)
		{
			return groupRepository.GetById(id).Map();
		}

		public MappedGroup UpdateGroupStudents(string groupId, List<string> studentIds)
		{
			var groupToUpdate = groupRepository.GetById(groupId);
			var studentList = studentRepository.GetAll().Where(x => studentIds.Contains(x.Id)).ToList();
			/*studentList.ForEach(x => {
				x.Group = groupToUpdate;
				studentRepository.Update(x);
			});*/
			groupToUpdate.UpdateStudentList(studentList);
			groupRepository.Update(groupToUpdate);
			return groupToUpdate.Map();
		}
	}
}
