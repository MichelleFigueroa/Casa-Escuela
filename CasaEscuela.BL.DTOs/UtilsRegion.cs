using System.Globalization;
namespace CasaEscuela.BL.DTOs
{
    public static class UtilsRegion
    {
        /// <summary>
        /// Convierte la fecha hora del el salvador
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static DateTime ConvertSV(this DateTime fecha)
        {
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            if (zonaHoraria.SupportsDaylightSavingTime)
            {
                // Si la zona horaria admite el horario de verano, obtén la regla de horario de verano y establece la fecha de inicio y fin en el mismo valor para desactivar el horario de verano
                TimeZoneInfo.AdjustmentRule[] reglasHorarioVerano = zonaHoraria.GetAdjustmentRules();

                foreach (TimeZoneInfo.AdjustmentRule regla in reglasHorarioVerano)
                {
                    TimeZoneInfo.AdjustmentRule nuevaRegla = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(regla.DateStart, regla.DateEnd, TimeSpan.Zero, regla.DaylightTransitionStart, regla.DaylightTransitionEnd);
                    zonaHoraria = TimeZoneInfo.CreateCustomTimeZone(zonaHoraria.Id, zonaHoraria.BaseUtcOffset, zonaHoraria.DisplayName, zonaHoraria.StandardName, zonaHoraria.DaylightName, new TimeZoneInfo.AdjustmentRule[] { nuevaRegla });
                }
            }
            DateTime fechaConvertida = TimeZoneInfo.ConvertTime(fecha, TimeZoneInfo.Local, zonaHoraria);

            return fechaConvertida;
        }
        public static DateTime GetFechaZonaHoraria(this DateTime fecha)
        {
            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            if (zonaHoraria.SupportsDaylightSavingTime)
            {
                // Si la zona horaria admite el horario de verano, obtén la regla de horario de verano y establece la fecha de inicio y fin en el mismo valor para desactivar el horario de verano
                TimeZoneInfo.AdjustmentRule[] reglasHorarioVerano = zonaHoraria.GetAdjustmentRules();

                foreach (TimeZoneInfo.AdjustmentRule regla in reglasHorarioVerano)
                {
                    TimeZoneInfo.AdjustmentRule nuevaRegla = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(regla.DateStart, regla.DateEnd, TimeSpan.Zero, regla.DaylightTransitionStart, regla.DaylightTransitionEnd);
                    zonaHoraria = TimeZoneInfo.CreateCustomTimeZone(zonaHoraria.Id, zonaHoraria.BaseUtcOffset, zonaHoraria.DisplayName, zonaHoraria.StandardName, zonaHoraria.DaylightName, new TimeZoneInfo.AdjustmentRule[] { nuevaRegla });
                }
            }
            DateTime fechaConvertida = TimeZoneInfo.ConvertTime(fecha, TimeZoneInfo.Local, zonaHoraria);

            return fechaConvertida;
        }
        public static String GetFechaEsp(this DateTime fecha, string formato)
        {
            CultureInfo culture = new CultureInfo("es-ES");
            return fecha.ToString(formato, culture);
        }
    }
}
