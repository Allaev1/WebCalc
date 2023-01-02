using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Application.Contracts.Services.Formater
{
    public interface IFormater
    {
        public Task<string> GetFormatedStringFromAsync(double value);
    }
}
