using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Interfaces;

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
		public Group CreateGroup(string name, int year)
		{
			var createdGroup = groupRepository.Create(Group.Create(name, year));
			return createdGroup;
		}

		public List<Group> GetAll()
		{
			return groupRepository.GetAll();
		}

		public Group GetById(string id)
		{
			return groupRepository.GetById(id);
		}

		public Group UpdateGroupStudents(string groupId, List<string> studentIds)
		{
			var groupToUpdate = groupRepository.GetById(groupId);
			var studentList = studentRepository.GetAll().Where(x => studentIds.Contains(x.Id)).ToList();
			studentList.ForEach(x => {
				x.Group = groupToUpdate;
				studentRepository.Update(x);
			});
			groupToUpdate.UpdateStudentList(studentList);
			groupRepository.Update(groupToUpdate);
			return groupToUpdate;
		}
	}
}
