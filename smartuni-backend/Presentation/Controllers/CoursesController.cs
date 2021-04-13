using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Interfaces;
using Domain;
using Presentation.DTOs;
using Domain.EntityMapers;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CoursesController : ControllerBase
	{
		private readonly ICourseService courseService;

		public CoursesController(ICourseService ICourseService)
		{
			this.courseService = ICourseService;
		}

		[HttpGet]
		public IEnumerable<MappedCourse> Get()
		{
			return courseService.GetAll();
		}

		[HttpPost]
		public ActionResult<Course> CreateCourse([FromBody] CourseDTO course)
		{
			Course createdCourse = courseService.CreateCourse(course.Name, course.Description, course.Credits, course.Year, course.Semester);
			if (createdCourse != null)
			{
				return CreatedAtAction("CreateCourse", createdCourse);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpDelete("{courseId}")]
		public ActionResult DeleteCourse([FromRoute] string courseId)
        {
			courseService.DeleteCourse(courseId);
			return NoContent();
        }
	}
}
