using System;

namespace Web.Services
{
    public class ServerTimeService : ITimeService
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
