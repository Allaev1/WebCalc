using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.Shared
{
    public class EntityBase
    {
        private EntityBase() { }

        public EntityBase(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
