using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Application.Contracts.Services.Settings;

namespace WebCalc.Application.Services.Settings
{
    public class FakeSettings : ISettings
    {
        public int MaxDisplayCharsCount { get; set; } = 15;
    }
}
