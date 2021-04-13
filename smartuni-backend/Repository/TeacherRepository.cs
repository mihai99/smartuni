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
	public class TeacherRepository : IRepository<Teacher>
	{
		private readonly DbContext context;

		public TeacherRepository(DbContext context)
		{
			this.context = context;
		}

		public Teacher Create(Teacher instance)
		{
			var createdTeacher = context.Add(instance);
			return createdTeacher.Entity;
		}

		public void Delete(Teacher instance)
		{
			context.Remove(instance);
		}

		public List<Teacher> GetAll()
		{
			return context.Set<Teacher>().Include(x => x.Courses).ToList();
		}

		public Teacher GetById(string id)
		{
			return context.Set<Teacher>().Include(x => x.Courses).SingleOrDefault(x => x.Id == id);
		}

		public void Save()
        {
			context.SaveChanges();
        }

        public Teacher Update(Teacher instance)
		{
			var createdTeacher = context.Update(instance);
			return createdTeacher.Entity;
		}
	}
}
