using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    public class RideRepository
    {
        Dictionary<string, List<Ride>> userRides = null;
        public RideRepository()
        {
            this.userRides = new Dictionary<string, List<Ride>>();
        }

        public void AddRide(string userId, Ride[] rides)
        {
            bool rideList = this.userRides.ContainsKey(userId);
            try
            {
                if(!rideList)
                {
                    List<Ride> list = new List<Ride>();
                    list.AddRange(rides);
                    this.userRides.Add(userId, list);
                }
            }
            catch(cabInvoiceException)
            {
                throw new cabInvoiceException(cabInvoiceException.ExceptionType.NULL_RIDES, "Rides Are Null");
            }
        }
        public Ride[] GetRide(string userId)
        {
            try
            {
                return this.userRides[userId].ToArray();
            }
            catch(Exception)
            {
                throw new cabInvoiceException(cabInvoiceException.ExceptionType.INVALID_USERID, "Invalid UserId");
            }
        }
    }
}
