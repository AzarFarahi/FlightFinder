using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFinder.Model
{
    public class Segment
    {
        public int ID { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public virtual Flight Flight { get; set; }
    }

}
