using System;
using System.Collections.Generic;

namespace FlightFinder.Model
{
    public class Flight
    {
        public int ID { get; set; }
        public IList<Segment> Segments { get; set; }
    }

}
