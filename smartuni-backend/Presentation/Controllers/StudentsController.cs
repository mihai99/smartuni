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
	public class StudentsController : ControllerBase
	{
		private readonly IStudentService studentService;

		public StudentsController(IStudentService studentService)
		{
			this.studentService = studentService;
		}

		[HttpGet]
		public IEnumerable<MappedStudent> Get()
		{
			return studentService.GetAll();
		}

		[HttpGet]
		[Route("in-group/{groupId}")]
		public ActionResult<List<MappedStudent>> GetStudentsFromGroup([FromRoute] string groupId)
        {
			return Ok(studentService.GetByGroupId(groupId));
        }

		[HttpPost]
		public async Task<ActionResult<MappedStudent>> CreateStudentAccount([FromBody] StudentDTO student)
		{
			MappedStudent createdStudent = await studentService.Create(student.FirstName, student.LastName, student.Email, student.PhoneNumber, student.NumericCode);
			if (createdStudent != null)
			{
				return CreatedAtAction("CreateStudentAccount", createdStudent);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpDelete("{studentId}")]
		public ActionResult DeleteStudents([FromRoute] string studentId)
        {
			studentService.DeleteStudents(new string[] { studentId });
			return NoContent();
        }
	}
}
