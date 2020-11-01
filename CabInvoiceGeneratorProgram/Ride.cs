using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGeneratorProgram
{
    public class Ride
    {
        public double distance;
        public int time;

        public Ride()
        {
            distance = 0;
            time = 0;
        }
        public Ride(double distance, int time)
        {
            this.distance = distance;
            this.time = time;
        }
    }
}