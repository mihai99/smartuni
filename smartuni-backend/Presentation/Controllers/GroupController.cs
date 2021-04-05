using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Interfaces;
using Presentation.DTOs;
using Domain.EntityMapers;


namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GroupController : ControllerBase
	{
		private readonly IGroupService groupService;


		public GroupController(IGroupService groupService)
		{
			this.groupService = groupService;
		}

		[HttpGet]
		public IEnumerable<MappedGroup> Get()
		{
			return groupService.GetAll();
		}

		[HttpPost]
		public ActionResult<MappedGroup> CreateStudentAccount([FromBody] GroupDTO group)
		{
			MappedGroup createdGroup = groupService.CreateGroup(group.Name, group.Year);

			if (createdGroup != null)
			{
				return CreatedAtAction("CreateStudentAccount", createdGroup);
			}
			else
			{
				return BadRequest();
			}
		}

		[Route("update-students")]
		[HttpPut]
		public ActionResult<MappedGroup> UpdateGroupStudents([FromBody] GroupStudentsDTO groupStudents)
		{
			MappedGroup updatedGroup = groupService.UpdateGroupStudents(groupStudents.groupId, groupStudents.studentIds);

			if (updatedGroup != null)
			{
				return Ok(updatedGroup);
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
