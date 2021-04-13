using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain;

namespace Repository.IOC
{
	public static class RepositoryIOC
	{
		public static void AddDataAccess(this IServiceCollection services)
		{
			services.AddSingleton<DbContext, EntityFrameworkContext>();
			services.AddScoped<IRepository<Student>, StudentsRepository>();
			services.AddScoped<IRepository<Group>, GroupRepository>();
			services.AddScoped<IRepository<Course>, CourseRepository>();
			services.AddScoped<IRepository<Teacher>, TeacherRepository>();
		}
	}
}
