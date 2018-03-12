using FlightFinder.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFinder.Services
{
    //The interface is used for Depency injection and to make the FlightBuilder testable
    public interface IFlightBuilder
    {
        IList<Flight> GetFlights();

    }
}
