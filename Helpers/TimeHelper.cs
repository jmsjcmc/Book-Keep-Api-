namespace Book_Keep.Helpers
{
    public class TimeHelper
    {
        // Method to get current time in Philippine Standard Time 
        public static DateTime GetPhilippineStandardTime()
        {
            var phpTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"); // Get time zone info object for Philippine Standard Time
            DateTime utcNow = DateTime.UtcNow; // Get current UTC time
            DateTime phpTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, phpTimeZone); // Convert UTC time to Philippine Standard Time
            return phpTime; // Return converted time
        }
    }
}
