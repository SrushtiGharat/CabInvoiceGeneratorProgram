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
        double totalFare;

        InvoiceSummary invoiceSummary = new InvoiceSummary();

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
        /// <summary>
        /// Calculate Fare For Multiple Rides
        /// </summary>
        /// <param name="rideList"></param>
        /// <returns></returns>
        public double CalculateFareForMultipleRides(List<Ride> rideList)
        {
            this.totalFare = 0;
            foreach (var ride in rideList)
            {
                this.totalFare = totalFare + CalculateFare(ride);
            }
            return this.totalFare;
        }
        /// <summary>
        /// Get Enhanced Invoice
        /// </summary>
        /// <param name="rideList"></param>
        /// <returns></returns>
        public InvoiceData GetInvoiceSummary(List<Ride> rideList)
        {
            double fare = CalculateFareForMultipleRides(rideList);
            InvoiceData data = invoiceSummary.GetInvoice(rideList.Count, totalFare);
            return data;
        }
    }
}