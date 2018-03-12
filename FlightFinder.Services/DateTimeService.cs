using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFinder.Services
{
    //The implmenetation of date time for the use in Services project
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
