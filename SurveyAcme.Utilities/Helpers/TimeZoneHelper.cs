namespace SurveyAcme.Utilities.Helpers
{
    public static class TimeZoneHelper
    {
        public static string TimeZoneId;

        public static DateTime Now
        {
            get
            {
                try
                {
                    TimeZoneInfo _appTimeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId ?? "America/El_Salvador");

                    if (_appTimeZone is null)
                        throw new InvalidOperationException("La zona horaria no ha sido configurada.");

                    return TimeZoneInfo.ConvertTime(DateTime.UtcNow, _appTimeZone);
                }
                catch { return DateTime.UtcNow; }
            }
        }
    }
}
