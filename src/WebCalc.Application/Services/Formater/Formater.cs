using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Application.Contracts.Services.Formater;
using WebCalc.Application.Contracts.Services.Settings;

namespace WebCalc.Application.Services.Formater
{
    public class Formater : IFormater
    {
        private readonly ISettings settings;
        private const double DEFAULT_ACCURACY = 4;

        public Formater(ISettings settings)
        {
            this.settings = settings;
        }

        public async Task<string> GetFormatedStringFromAsync(double value)
        {
            var delimiterOn = await settings.GetAsync<bool>(SettingsNames.DelimiterOn);
            var roundUpOn = await settings.GetAsync<bool>(SettingsNames.RoundUpOn);
            var accuracy = await settings.GetAsync<int>(SettingsNames.Accuracy);

            if (delimiterOn & roundUpOn)
            {
                return value.ToString($"N{accuracy}", CultureInfo.CurrentCulture);
            }
            else if (delimiterOn & !roundUpOn)
            {
                return value.ToString($"N{DEFAULT_ACCURACY}", CultureInfo.CurrentCulture);
            }
            else if (!delimiterOn & roundUpOn)
            {
                return string.Format($"{{0:F{accuracy}}}", value);
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
