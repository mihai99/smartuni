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
	public class GroupController : ControllerBase
	{
		private readonly IGroupService groupService;


		public GroupController(IGroupService groupService)
		{
			this.groupService = groupService;
		}

		[HttpGet]
		public IEnumerable<Group> Get()
		{
			return groupService.GetAll();
		}

		[HttpPost]
		public ActionResult<Group> CreateStudentAccount([FromBody] GroupDTO group)
		{
			Group createdGroup = groupService.CreateGroup(group.Name, group.Year);

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
		public ActionResult<Group> UpdateGroupStudents([FromBody] GroupStudentsDTO groupStudents)
		{
			Group updatedGroup = groupService.UpdateGroupStudents(groupStudents.groupId, groupStudents.studentIds);

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
