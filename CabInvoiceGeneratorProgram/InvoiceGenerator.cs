using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGeneratorProgram
{
    public class InvoiceGenerator
    {
        readonly int COST_PER_KM = 10;
        readonly int COST_PER_MIN = 1;
        readonly int MIN_FARE = 5;

        /// <summary>
        /// Default constructor
        /// </summary>
        public InvoiceGenerator()
        {
        }

        /// <summary>
        /// Calculate Fare for a single ride
        /// </summary>
        /// <param name="ride"></param>
        /// <returns></returns>
        public double CalculateFare(Ride ride)
        {
            if (ride == null)
            {
                throw new CabInvoiceException(CabInvoiceException.Type.NULL_RIDES, "Ride is Invalid");
            }
            if (ride.distance <= 0)
            {
                throw new CabInvoiceException(CabInvoiceException.Type.INVALID_DISTANCE, "Distance is Invalid");
            }
            if (ride.time <= 0)
            {
                throw new CabInvoiceException(CabInvoiceException.Type.INVALID_TIME, "Time is Invalid");
            }

            double fare = (ride.distance * COST_PER_KM) + (ride.time * COST_PER_MIN);
            return Math.Max(fare, MIN_FARE);
        }
    }
}