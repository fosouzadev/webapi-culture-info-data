using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FoSouzaDev.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CultureInfoDataController : ControllerBase
    {
        private static double GetRandomValue() => new Random().NextDouble() * 1_000_000;

        [HttpGet]
        public IResult Get([FromQuery] string cultureInfoName = "pt-BR")
        {
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(cultureInfoName);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            return Results.Ok(new
            {
                cultureInfo.Name,
                cultureInfo.DisplayName,
                cultureInfo.NativeName,
                cultureInfo.EnglishName,
                cultureInfo.ThreeLetterISOLanguageName,
                cultureInfo.ThreeLetterWindowsLanguageName,
                NumberFormat = new
                {
                    cultureInfo.NumberFormat.NumberGroupSeparator,
                    cultureInfo.NumberFormat.NumberDecimalSeparator,
                    RandomMoneratyValues = new string[]
                    {
                        $"{GetRandomValue():C2}",
                        $"{GetRandomValue():C2}",
                        $"{GetRandomValue():C2}",
                        $"{GetRandomValue():C2}"
                    },
                    RandomValues = new string[]
                    {
                        $"{GetRandomValue():N2}",
                        $"{GetRandomValue():N2}",
                        $"{GetRandomValue():N2}",
                        $"{GetRandomValue():N2}"
                    }
                },
                DatetimeFormat = new
                {
                    DaysOfWeek = cultureInfo.DateTimeFormat.DayNames,
                    DayOfWeekNow = cultureInfo.DateTimeFormat.GetDayName(DateTimeOffset.Now.DayOfWeek),
                    Months = cultureInfo.DateTimeFormat.MonthNames,
                    MonthNow = cultureInfo.DateTimeFormat.GetMonthName(DateTimeOffset.Now.Month),
                    Now = $"{DateTimeOffset.Now:dd/MM/yyyy HH:mm:ss:ffff:zz gg MMMM ddd dddd}"
                }
            });
        }
    }
}