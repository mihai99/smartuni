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

        public List<MappedStudent> AddStudentsToGroup(string groupId, List<string> studentIds)
        {
			var groupToUpdate = groupRepository.GetById(groupId);
			if (groupToUpdate == null)
			{
				return null;
			}
			var studentList = studentRepository.GetAll().Where(x => studentIds.Contains(x.Id)).ToList();
			studentList.ForEach(student =>
			{
				if (student.Group != null)
				{
					student.Group.Students.Remove(student);
					groupRepository.Update(student.Group);
				}
			});
			groupToUpdate.UpdateStudentList(studentList);
			groupRepository.Update(groupToUpdate);
			groupRepository.Save();
			return groupToUpdate.Students.Select(x => x.Map()).ToList();
		}

        public MappedGroup CreateGroup(string name, int year)
		{
			var createdGroup = groupRepository.Create(Group.Create(name, year));
			groupRepository.Save();
			return createdGroup.Map();
		}

        public bool DeleteGroup(string id)
        {
			var group = this.groupRepository.GetById(id);
			group.Students.ForEach(x => {
				x.Group = null;
				studentRepository.Update(x);
			});
			groupRepository.Delete(group);
			groupRepository.Save();
			return true;
        }

        public List<MappedGroup> GetAll()
		{
			return groupRepository.GetAll().Select(x => x.Map()).ToList();
		}

        public List<MappedGroupWithStudents> GetAllWithStudents()
        {
			return groupRepository.GetAll().Select(x => x.MapWithStudents()).ToList();
		}

        public MappedGroup GetById(string id)
		{
			return groupRepository.GetById(id).Map();
		}

        public List<MappedStudent> RemoveStudentsToGroup(string groupId, List<string> studentIds)
        {
			var groupToUpdate = groupRepository.GetById(groupId);
			studentIds.ForEach(id =>
			{
				var student = studentRepository.GetById(id);
				if (student != null && groupToUpdate.Students.Contains(student))
				{
					student.Group = null;
					studentRepository.Update(student);
					groupToUpdate.Students.Remove(student);
				}
			});
			groupRepository.Update(groupToUpdate);
			groupRepository.Save();
			return groupToUpdate.Students.Select(x => x.Map()).ToList();
		}
	}
}
