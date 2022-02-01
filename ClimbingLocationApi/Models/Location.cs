using System;

namespace ClimbingLocationApi.Models
{
    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime LastUpdate { get; set; }

        public Location()
        {
            this.X = 10;
            this.Y = 0;
            this.LastUpdate = DateTime.MinValue;
        }
    }
}
