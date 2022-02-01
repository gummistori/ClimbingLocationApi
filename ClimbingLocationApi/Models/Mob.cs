using System;

namespace ClimbingLocationApi.Models
{
    public class Mob
    {
        public int MobId { get; set; }
        public string Identity { get; set; }
        public DateTime LogTime { get; set; }
        public DateTime TimestampReal { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Bearing { get; set; }
        public double Isn93X { get; set; }
        public double Isn93Y { get; set; }
        public string StreetLocation { get; set; }
        public string Region { get; set; }
        public string CallId { get; set; }

        public Mob()
        {
            MobId = 0;
            Identity = string.Empty;
            LogTime = DateTime.MinValue;
            TimestampReal = DateTime.MinValue;
            Lat = 0;
            Lon = 0;
            Bearing = 0;
            Isn93X = 0;
            Isn93Y = 0;
            StreetLocation = string.Empty;
            Region = string.Empty;
            CallId = string.Empty;
        }
    }
}
