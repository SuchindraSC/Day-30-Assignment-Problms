using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    public class invoiceGenerator
    {
        RideType rideType;
        private RideRepository rideRepository;

        private readonly double MINIMUM_COST_PER_KM;
        private readonly int COST_PER_TIME;
        private readonly double MINIMUM_FARE;


        public invoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
            this.rideRepository = new RideRepository();
            try
            {
                if(rideType.Equals(RideType.PREMIUM))
                {
                    this.MINIMUM_COST_PER_KM = 15;
                    this.COST_PER_TIME = 2;
                    this.MINIMUM_FARE = 20;
                }
                else if(rideType.Equals(RideType.NORMAL))
                {
                    this.MINIMUM_COST_PER_KM = 10;
                    this.COST_PER_TIME = 1;
                    this.MINIMUM_FARE = 5;
                }
            }
            catch(cabInvoiceException)
            {
                throw new cabInvoiceException(cabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
            }
        }
        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0;
            try
            {
                totalFare = (distance * MINIMUM_COST_PER_KM) + (time * COST_PER_TIME);
            }
            catch(cabInvoiceException)
            {
                if(rideType.Equals(null))
                {
                    throw new cabInvoiceException(cabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
                }
                if(distance <= 0)
                {
                    throw new cabInvoiceException(cabInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
                }
                if(time < 0)
                {
                    throw new cabInvoiceException(cabInvoiceException.ExceptionType.INVALID_TIME, "Invalid Time");
                }
            }
            return Math.Max(totalFare, MINIMUM_FARE);
        }

        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            try
            {
                foreach(Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
            }
            catch(cabInvoiceException)
            {
                if(rides == null)
                {
                    throw new cabInvoiceException(cabInvoiceException.ExceptionType.NULL_RIDES, "Rides Are Null");
                }
            }
            return new InvoiceSummary(rides.Length, totalFare);
        }

        public void AddRides(string userId, Ride[] rides)
        {
            try
            {
                rideRepository.AddRide(userId, rides);
            }
            catch(cabInvoiceException)
            {
                if(rides == null)
                {
                    throw new cabInvoiceException(cabInvoiceException.ExceptionType.NULL_RIDES, "Rides Are Null");
                }
            }
        }
        public InvoiceSummary CalculateFare(Ride[] rides, int numOfRides, double averageFarePerRide)
        {
            double totalFare = 0;
            numOfRides = 0;
            averageFarePerRide = 0;
            try
            {
                foreach (Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
                averageFarePerRide = totalFare / numOfRides;
            }
            catch (cabInvoiceException)
            {
                if (rides == null)
                {
                    throw new cabInvoiceException(cabInvoiceException.ExceptionType.NULL_RIDES, "Rides Are Null");
                }
            }
            return new InvoiceSummary(rides.Length, totalFare, averageFarePerRide);
        }

        public InvoiceSummary GetInvoiceSummary(string userId)
        {
            try
            {
                return this.CalculateFare(rideRepository.GetRide(userId));
            }
            catch(cabInvoiceException)
            {
                throw new cabInvoiceException(cabInvoiceException.ExceptionType.INVALID_USERID, "Invalid UserId");
            }
        }
    }
}
