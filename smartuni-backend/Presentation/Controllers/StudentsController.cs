using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Interfaces;
using Domain;
using Presentation.DTOs;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class StudentsController : ControllerBase
	{
		private readonly IRepository<Student> repository;


		public StudentsController(IRepository<Student> repository)
		{
			this.repository = repository;
		}

		[HttpGet]
		public IEnumerable<Student> Get()
		{
			return repository.GetAll();
		}

		[HttpPost]
		public async Task<ActionResult<Student>> CreateStudentAccount([FromBody] StudentDTO student)
		{
			Student createdStudent = repository.Create(Student.Create(student.Id, student.FirstName, student.LastName, student.Email, student.PhoneNumber, student.NumericCode));
			if (createdStudent != null)
			{
				return CreatedAtAction("CreateStudentAccount", createdStudent);
			}
			else
			{
				return BadRequest();
			}

		}
	}
}
