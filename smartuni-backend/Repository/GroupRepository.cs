using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public class GroupRepository : IRepository<Group>
	{
		private readonly DbContext context;

		public GroupRepository(DbContext context)
		{
			this.context = context;
		}

		public Group Create(Group instance)
		{
			var createdGroup = context.Add(instance);
			return createdGroup.Entity;
		}

		public void Delete(Group instance)
        {
			context.Remove(instance);
		}

        public List<Group> GetAll()
		{
			return context.Set<Group>().Include(x => x.Students).ToList();
		}

		public Group GetById(string id)
		{
			return context.Set<Group>().Include(x => x.Students).SingleOrDefault(x => x.Id == System.Guid.Parse(id));
		}

        public void Save()
        {
			context.SaveChanges();
        }

        public Group Update(Group instance)
		{
			var updatedGroup = context.Update(instance);
			return updatedGroup.Entity;
		}
	}
}
