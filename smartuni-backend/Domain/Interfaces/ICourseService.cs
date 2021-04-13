using System;
using System.Collections.Generic;
using Domain.EntityMapers;
namespace Domain.Interfaces
{
    public interface ICourseService
    {
        public Course CreateCourse(string name, string description, int credits, int year, int semester);

        public List<MappedCourse> GetAll();

        public bool DeleteCourse(string id);
    }
}
