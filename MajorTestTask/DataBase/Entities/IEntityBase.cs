using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorTestTask.DataBase.Entities
{
    public interface IEntityBase
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
    public abstract class EntityBase : IEntityBase
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

    }
}
