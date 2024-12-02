using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public abstract class BaseEntity: IBaseEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
    }
}
