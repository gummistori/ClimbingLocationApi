using System;
using System.Collections.Generic;

namespace ClimbingLocationApi.Models
{
    public class ResponseModel
    {
        public DateTime NewTime { get; set; }
        public List<Mob> Vehicles { get; set; }

        public ResponseModel()
        {
            this.Vehicles = new List<Mob>();
        }
    }
}
