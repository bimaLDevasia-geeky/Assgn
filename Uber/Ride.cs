using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uber
{
    public class Ride
    {
        //static Variables
        static int counter = 100;
        public static int totalRides { set; get; }
        public static double totalEarnings { set; get; }
        public static int baseFare { set; get; }
        public static double surgeMultiplier { set; get; }

        //instance members
        public string RideId { set; get; }
        public string DriverName { set; get; }
        public string PassengerName { set; get; }
        public double DistanceKm { set; get; }
        public double Fare { set; get; }



        //static Constructor 
        static Ride()
        {
            Console.WriteLine("Uber System Initialized. Ready to book rides...\n");
            totalRides = 0;
            totalEarnings = 0;
            surgeMultiplier = 1.0;
            counter = 1000;
            baseFare = 50;

        }
        //for calculating Fare
        static double FareCalculation(double distance)
        {
            return baseFare + ((double)distance * 15 * (double)surgeMultiplier);
        }

        //constructor to intialise
        public Ride(string DriverName, string PassengerName, double DistanceKm)
        {
            this.RideId = "Ride_" + counter;
            this.DriverName = DriverName;
            this.PassengerName = PassengerName;
            this.DistanceKm = DistanceKm;
            this.Fare = FareCalculation(this.DistanceKm);
            counter++;
            totalEarnings += this.Fare;
            totalRides++;
        }

        //function to set the surgeMultiplier
        public static void SetSurgeMultiplier(double multiplier)
        {
            surgeMultiplier = multiplier;
        }

        //function to show static details
        public static void ShowRideSummary()
        {
            Console.WriteLine($"\nThe Details are \n The total Ride : {totalRides} \n The total earnings :Rs {totalEarnings}\n");

        }
        //individual details
        public void ShowRideDetails()
        {
            Console.WriteLine($"The Ride Id : {this.RideId} \n The Driver Name : {this.DriverName}\n" +
                $"The Passenger Name : {this.PassengerName}\n Distance travelled : {this.DistanceKm} km\n " +
                $"The fare : Rs {this.Fare} \n" );


        }
    }
}
