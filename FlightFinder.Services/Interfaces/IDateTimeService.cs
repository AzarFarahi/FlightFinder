using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFinder.Services
{
    //The interface is used as a way to make the date time configurable when testing
   public interface IDateTimeService
    {
        DateTime Now();
    }
}
