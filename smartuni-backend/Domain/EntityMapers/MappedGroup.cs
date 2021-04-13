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
        public int StundentNumber { get; set; }
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
                StundentNumber = group.Students.Count,
            };
            return mappedGroup;
        }
    }
}
