using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebCalc.Domain.Constant.Exceptions;

namespace WebCalc.Domain.Constant
{
    public class Constant 
    {
        private Constant() { }

        internal Constant(string name, float value, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EmptyConstantNameException(nameof(Name));

            Id = Guid.NewGuid();
            Name = name;
            Value = value;
            Description = description;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public float Value { get; set; }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EmptyConstantNameException(nameof(Name));

            Name = name;
        }

        public string? Description { get; set; }
    }
}
