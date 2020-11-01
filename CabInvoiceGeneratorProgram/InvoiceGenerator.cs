using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGeneratorProgram
{
    public class InvoiceGenerator
    {
        readonly int COST_PER_KM;
        readonly int COST_PER_MIN;
        readonly int MIN_FARE;
        double totalFare;
        RideType rideType;

        InvoiceSummary invoiceSummary = new InvoiceSummary();
        RideRepository rideRepository = new RideRepository();
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public InvoiceGenerator()
        {
        }

        /// <summary>
        /// Parameterised constructor
        /// </summary>
        /// <param name="rideList"></param>
        /// <param name="rideType"></param>
        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;

            if (rideType.Equals(RideType.NORMAL))
            {
                this.COST_PER_KM = 10;
                this.COST_PER_MIN = 1;
                this.MIN_FARE = 5;
            }
            else if (rideType.Equals(RideType.PREMIUM))
            {
                this.COST_PER_KM = 15;
                this.COST_PER_MIN = 2;
                this.MIN_FARE = 20;
            }
            else
            {
                throw new CabInvoiceException(CabInvoiceException.Type.INVALID_RIDE_TYPE, "Ride type is Invalid");
            }
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

        /// <summary>
        /// Add rides to dictionary according to user id
        /// </summary>
        /// <param name="userId"></param>
        public void AddRides(int userId, List<Ride> rideList)
        {
            rideRepository.Add(userId, rideList);
        }

        /// <summary>
        /// Given user id get invoice
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public InvoiceData GetUserInvoice(int userId)
        {
            List<Ride> rideList = rideRepository.GetRides(userId);
            InvoiceData data = GetInvoiceSummary(rideList);
            return data;
        }
    }
}