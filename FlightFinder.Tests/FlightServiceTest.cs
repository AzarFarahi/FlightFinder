using FlightFinder.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Collections.Generic;
using FlightFinder.Model;

namespace FlightFinder.Tests
{
    //The test class for FlightService 
    [TestClass]
    public class FlightServiceTest
    {
        [TestMethod]
        public void GetFlights_InThePast_ShouldNotReturn()
        {
            var dateTimeService = new Mock<IDateTimeService>();
            dateTimeService.Setup(m => m.Now()).Returns(new DateTime(2017, 2, 17));
            var flightBuilder = new Mock<IFlightBuilder>();
            flightBuilder.Setup(f => f.GetFlights()).Returns(new List<Flight>
            {
                new Flight
                {
               Segments = new List<Segment>
                    {
                   new Segment
                     {
                       ArrivalDate = new DateTime(2017, 2, 10),
                       DepartureDate = new DateTime(2017,2,10)
                     }
                    }
                 }
             });

            var flightService = new FlightService(flightBuilder.Object, dateTimeService.Object);
            var validFlights = flightService.GetValidFlights();

            Assert.AreEqual(0, validFlights.Count);

        }

        [TestMethod]
        public void GetFlights_SegmentArrivesBeforeDeparture_ShouldNotReturn()
        {
            var dateTimeService = new Mock<IDateTimeService>();
            dateTimeService.Setup(m => m.Now()).Returns(new DateTime(2017, 2, 17, 15, 10, 10));
            var flightBuilder = new Mock<IFlightBuilder>();
            flightBuilder.Setup(f => f.GetFlights()).Returns(new List<Flight>
            {
                new Flight
                {
               Segments = new List<Segment>
                    {
                   new Segment
                     {
                       ArrivalDate = new DateTime(2017,2,18,12,10,00),
                       DepartureDate = new DateTime(2017,2,18,13,10,10)
                     },

                   new Segment
                     {
                       ArrivalDate = new DateTime(2017,2,18,14,10,00),
                       DepartureDate = new DateTime(2017,2,18,12,20,00)
                     }

                    }
                 }
             });

            var flightService = new FlightService(flightBuilder.Object, dateTimeService.Object);
            var validFlights = flightService.GetValidFlights();

            Assert.AreEqual(0, validFlights.Count);

        }

        [TestMethod]
        public void GetFlighst_GapMoreThanTwoHours_ShouldNotReturn()
        {
            var dateTimeService = new Mock<IDateTimeService>();
            dateTimeService.Setup(m => m.Now()).Returns(new DateTime(2017, 2, 17, 15, 10, 10));
            var flightBuilder = new Mock<IFlightBuilder>();
            flightBuilder.Setup(f => f.GetFlights()).Returns(new List<Flight>
            {
                new Flight
                {
               Segments = new List<Segment>
                    {
                   new Segment
                     {
                       ArrivalDate = new DateTime(2017,2,18,13,10,00),
                       DepartureDate = new DateTime(2017,2,18,12,10,10)
                     },

                   new Segment
                     {
                       ArrivalDate = new DateTime(2017,2,18,14,10,00),
                       DepartureDate = new DateTime(2017,2,18,13,20,00)
                     },

                    new Segment
                     {
                       ArrivalDate = new DateTime(2017,2,18,16,20,00),
                       DepartureDate = new DateTime(2017,2,18,14,20,00)
                     }
                    }
                 }
             });

            var flightService = new FlightService(flightBuilder.Object, dateTimeService.Object);
            var validFlights = flightService.GetValidFlights();

            Assert.AreEqual(0, validFlights.Count);

        }

        [TestMethod]
        public void GetFlights_ValidFlight_ShouldReturn()
        {
            var dateTimeService = new Mock<IDateTimeService>();
            dateTimeService.Setup(m => m.Now()).Returns(new DateTime(2017, 2, 17, 15, 10, 10));
            var flightBuilder = new Mock<IFlightBuilder>();
            flightBuilder.Setup(f => f.GetFlights()).Returns(new List<Flight>
            {
                new Flight
                {
               Segments = new List<Segment>
                    {
                   new Segment
                     {
                       ArrivalDate = new DateTime(2017,2,18,13,10,00),
                       DepartureDate = new DateTime(2017,2,18,12,10,10)
                     },

                   new Segment
                     {
                       ArrivalDate = new DateTime(2017,2,18,16,10,00),
                       DepartureDate = new DateTime(2017,2,18,15,11,00)
                     },

                    new Segment
                     {
                       ArrivalDate = new DateTime(2017,2,18,19,20,00),
                       DepartureDate = new DateTime(2017,2,18,18,20,00)
                     }
                    }
                 }
             });

            var flightService = new FlightService(flightBuilder.Object, dateTimeService.Object);
            var validFlights = flightService.GetValidFlights();

            Assert.AreEqual(1, validFlights.Count);
        }
        }
    }
