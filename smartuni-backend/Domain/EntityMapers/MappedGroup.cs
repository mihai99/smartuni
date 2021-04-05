using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.EntityMapers
{
    public class MappedGroup
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public List<MappedStudent> Students { get; set; }
    }

    public static class GroupMapper
    {
        public static MappedGroup Map(this Group group)
        {
            var mappedGroup = new MappedGroup()
            {
                Id = group.Id,
                Year = group.Year,
                Name = group.Name,
                Students = new List<MappedStudent>(),
            };
            if(group.Students != null && group.Students.Count > 0)
            {
                mappedGroup.Students = group.Students.Select(x => x.Map()).ToList();
            }
            return mappedGroup;
        }
    }
}
