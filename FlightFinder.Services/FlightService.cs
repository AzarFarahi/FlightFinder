using FlightFinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightFinder.Services
{
    public class FlightService
    {
        private IFlightBuilder _flightBuilder;
        private IDateTimeService _dateTimeService;

        public FlightService(IFlightBuilder flightBuilder, IDateTimeService dateTimeService)
        {
            _flightBuilder = flightBuilder;
            _dateTimeService = dateTimeService;
        }

        //method returning the valid flights after the checks are done.
        public IList<Flight> GetValidFlights()
        {
            var flights = _flightBuilder.GetFlights();
            return flights.Where(f => IsValid(f)).ToList();
       
        }

        //Method checking if all the sub methods pass and the flight is valid
       private bool IsValid(Flight flight)
        {
            return !IsInPast(flight) && AreSegmentsCorrect(flight) && HasEnoughGap(flight);
        }

        //Method checking whether the flight is in the past
        private bool IsInPast(Flight flight)
        {
            return flight.Segments.OrderBy(s => s.DepartureDate).First().DepartureDate < _dateTimeService.Now();
        }

        //Method checking whether any of the Segments Depart after Arrival
        private bool AreSegmentsCorrect(Flight flight)
        {
            return !flight.Segments.Any(s => s.ArrivalDate < s.DepartureDate);
     
        }

        //Method checking if there is enogh gap between consequent segments
        private bool HasEnoughGap(Flight flight)
        {
            var orderedSegments = flight.Segments.OrderBy(s => s.DepartureDate);
            foreach (var item in orderedSegments.Except(new List<Segment> { orderedSegments.Last() }))
            {
                var index = Array.IndexOf(orderedSegments.ToArray(), item);
                var nextItem = orderedSegments.ElementAt(index + 1);
                if (nextItem.DepartureDate > item.ArrivalDate.AddHours(2))
                    return true;
            }
            return false;
        }

    
}
}
