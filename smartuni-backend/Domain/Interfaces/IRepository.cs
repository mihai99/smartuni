using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IRepository<T>
	{
		public T Create(T instance);
		public T GetById(string id);
		public List<T> GetAll();
		public T Update(T instance);
		public void Delete(T instance);
		public void Save();
	}
}
