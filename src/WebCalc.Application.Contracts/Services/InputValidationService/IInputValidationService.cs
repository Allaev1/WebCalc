using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Application.Contracts.Services.InputValidationService
{
    public interface IInputValidationService
    {
        bool IsEditionAllowed(
            char input,
            string value);
    }
}
