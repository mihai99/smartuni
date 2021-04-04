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
			context.SaveChanges();
			return createdGroup.Entity;
		}

		public List<Group> GetAll()
		{
			return context.Set<Group>().ToList();
		}

		public Group GetById(string id)
		{
			return context.Set<Group>().SingleOrDefault(x => x.Id == System.Guid.Parse(id));
		}

		public Group Update(Group instance)
		{
			var updatedGroup = context.Update(instance);
			context.SaveChanges();
			return updatedGroup.Entity;
		}
	}
}
