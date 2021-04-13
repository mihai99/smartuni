using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	class StudentsRepository : IRepository<Student>
	{
		private readonly DbContext context;

		public StudentsRepository(DbContext context)
		{
			this.context = context;
		}

		public Student Create(Student instance)
		{
			var createdStudent = context.Add(instance);
			context.SaveChanges();
			return createdStudent.Entity;
		}

        public void Delete(Student instance)
		{
			context.Remove(instance);
		}

        public List<Student> GetAll()
		{
			return context.Set<Student>().Include(x => x.Group).ToList();
		}

		public Student GetById(string id)
		{
			return context.Set<Student>().SingleOrDefault(x => x.Id == id);
		}

        public void Save()
        {
			context.SaveChanges();
        }

        public Student Update(Student instance)
		{
			var updatedStudent = context.Update(instance);
			return updatedStudent.Entity;
		}
	}
}
