using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Application.Contracts.Constants.DTO
{
    public class CreateConstantDto
    {
        public string Name { get; set; } = null!;

        public float Value { get; set; }

        public string? Description { get; set; }
    }
}
