using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebCalc.Domain.Constant.Exceptions;

namespace WebCalc.Domain.Shared.Constant
{
    public abstract class ConstantBase<TValue>
    {
        protected ConstantBase() { }

        protected internal ConstantBase(string name, TValue value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EmptyConstantNameException(nameof(Name));

            Id = Guid.NewGuid();
            Name = name;
            Value = value;
        }

        public Guid Id { get; }

        public string Name { get; private set; }

        public TValue Value { get; set; }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EmptyConstantNameException(nameof(Name));

            Name = name;
        }
    }
}
