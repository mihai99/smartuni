using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Interfaces;
using Presentation.DTOs;
using Domain.EntityMapers;
using Presentation.ApiUtils;

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
		[Route("with-students")]
		public IEnumerable<MappedGroupWithStudents> GetWithStudents()
		{
			return groupService.GetAllWithStudents();
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

		[Route("{groupId}/add-students")]
		[HttpPut]
		public ActionResult<List<MappedStudent>> AddGroupStudents([FromRoute] string groupId, [FromBody] GroupStudentsDTO groupStudents)
		{
			List<MappedStudent> updatedGroupStudents = groupService.AddStudentsToGroup(groupId, groupStudents.studentIds);
			if (updatedGroupStudents != null)
			{
				return Ok(updatedGroupStudents);
			}
			else
			{
				return BadRequest();
			}
		}

		[Route("{groupId}/remove-students")]
		[HttpPut]
		public ActionResult<List<MappedStudent>> RemoveGroupStudents([FromRoute] string groupId, [FromBody] GroupStudentsDTO groupStudents)
		{
			List<MappedStudent> updatedGroupStudents = groupService.RemoveStudentsToGroup(groupId, groupStudents.studentIds);
			if (updatedGroupStudents != null)
			{
				return Ok(updatedGroupStudents);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpDelete("{id}")]
		public ActionResult<bool> DeleteGroup([FromRoute] string id)
        {
			bool deleteResult = groupService.DeleteGroup(id);
			if(deleteResult)
            {
				return NoContent();
            } else
            {
				return BadRequest();
            }
        }
	}
}
