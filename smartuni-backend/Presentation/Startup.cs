using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Repository;
using Microsoft.EntityFrameworkCore;
using Repository.IOC;
using Services.IOC;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Presentation.ApiUtils;

namespace Presentation
{
	public class Startup
	{
		readonly string AllowFrontendRequests = "_allowFrontendRequests";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy(name: AllowFrontendRequests,
								  builder =>
								  {
									  builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
								  });
			});

			services.AddDataAccess();
			services.AddServices();

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Presentation", Version = "v1" });
			});

			services.AddDbContextPool<EntityFrameworkContext>(
				options => options.UseSqlServer(Configuration.GetConnectionString("SmartuniDatabase"), b => b.MigrationsAssembly("Presentation"))
				);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Presentation v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors(AllowFrontendRequests);

			app.UseMiddleware<JwtMiddleware>();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			FirebaseApp.Create(new AppOptions()
			{
				Credential = GoogleCredential.FromFile("./smartuni-927a2-firebase-adminsdk-vlgcq-a9455fb8e8.json")
			}); ;
		}
	}
}
