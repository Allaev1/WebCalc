using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Application.Contracts.Services.Settings
{
    public interface ISettings
    {
        public int MaxDisplayCharsCount { get; set; }
    }
}
