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
	public class CourseRepository : IRepository<Course>
	{
		private readonly DbContext context;

		public CourseRepository(DbContext context)
		{
			this.context = context;
		}

		public Course Create(Course instance)
		{
			var createdCourse = context.Add(instance);
			return createdCourse.Entity;
		}

		public void Delete(Course instance)
		{
			context.Remove(instance);
		}

		public List<Course> GetAll()
		{
			return context.Set<Course>().Include(x => x.Teachers).ToList();
		}

		public Course GetById(string id)
		{
			try
            {
				return context.Set<Course>().Include(x => x.Teachers).SingleOrDefault(x => x.Id == System.Guid.Parse(id));
            }
			catch
            {
				return null;
            }
		}

		public void Save()
        {
			context.SaveChanges();
        }

        public Course Update(Course instance)
		{
			var updatedCourse = context.Update(instance);
			return updatedCourse.Entity;
		}
	}
}
