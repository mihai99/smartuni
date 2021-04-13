using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain;

namespace Services.IOC
{
	public static class ServicesIOC
	{
		public static void AddServices(this IServiceCollection services)
		{
			services.AddScoped<IGroupService, GroupService>();
			services.AddScoped<IStudentService, StudentService>();
			services.AddScoped<IFirebaseService, FirebaseService>();
			services.AddScoped<ICourseService, CourseService>();
			services.AddScoped<ITeacherService, TeacherService>();
		}
	}
}
