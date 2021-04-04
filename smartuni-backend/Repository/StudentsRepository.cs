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

		public List<Student> GetAll()
		{
			return context.Set<Student>().ToList();
		}

		public Student GetById(string id)
		{
			return context.Set<Student>().SingleOrDefault(x => x.Id == id);
		}

		public Student Update(Student instance)
		{
			var updatedStudent = context.Update(instance);
			context.SaveChanges();
			return updatedStudent.Entity;
		}
	}
}
