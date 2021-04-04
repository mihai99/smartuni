
using System.Collections.Generic;

namespace Presentation.DTOs
{
	public class GroupStudentsDTO
	{
		public string groupId { get; set; }
		public List<string> studentIds { get; set; }
	}
}
