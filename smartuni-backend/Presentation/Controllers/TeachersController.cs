using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Interfaces;
using Domain.EntityMapers;
using Presentation.DTOs;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TeachersController : ControllerBase
	{
		private readonly ITeacherService teacherService;

		public TeachersController(ITeacherService teacherService)
		{
			this.teacherService = teacherService;
		}

		[HttpGet]
		public IEnumerable<MappedTeacher> Get()
		{
			return teacherService.GetAll();
		}

		[HttpPost]
		public async Task<ActionResult<MappedTeacher>> CreateTeacherAccount([FromBody] TeacherDTO teacher)
		{
			MappedTeacher createdTeacher = await teacherService.CreateTeacherAccount(teacher.FirstName, teacher.LastName, teacher.Email, teacher.Age);
			if (createdTeacher != null)
			{
				return CreatedAtAction("CreateTeacherAccount", createdTeacher);
			}
			else
			{
				return BadRequest();
			}
		}

		[Route("{teacherId}/{courseId}/add")]
		[HttpPut]
		public ActionResult<List<MappedCourse>> AddCourseForTeacher([FromRoute] string teacherId, [FromRoute] string courseId)
		{
			var coursesList = teacherService.AsignCourse(teacherId, courseId);
			if (coursesList != null)
			{
				return Ok(coursesList);
			}
			else
			{
				return BadRequest();
			}
		}

		[Route("{teacherId}/{courseId}/remove")]
		[HttpPut]
		public ActionResult<List<MappedCourse>> RemoveCourseForTeacher([FromRoute] string teacherId, [FromRoute] string courseId)
		{
			var coursesList = teacherService.RemoveCourse(teacherId, courseId);
			if (coursesList != null)
			{
				return Ok(coursesList);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpDelete("{teacherId}")]
		public ActionResult DeleteStudents([FromRoute] string teacherId)
        {
			teacherService.DeleteTeacher(teacherId);
			return NoContent();
        }
	}
}
