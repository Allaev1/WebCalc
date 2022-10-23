using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Domain.Constant.Exceptions;

namespace WebCalc.Domain.Constant
{
    public class Constant
    {
        //For serialization
        private Constant() { }

        internal Constant(
            string name,
            float value,
            string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EmptyConstantNameException(nameof(Name));

            Id = Guid.NewGuid();
            Name = name;
            Value = value;
        }

        public Guid Id { get; }

        public string Name { get; private set; }

        public float Value { get; set; }

        public string? Description { get; set; }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EmptyConstantNameException(nameof(Name));

            Name = name;
        }
    }
}
