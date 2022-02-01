using System;

namespace ClimbingLocationApi.Models
{
    public class RequestModel
    {
        public DateTime OldTime { get; set; }
        public string Lang { get; set; }
        public int SpriteLast { get; set; }
        public int RevokeLast { get; set; }

        public RequestModel()
        {
            OldTime = DateTime.Parse("2022-01-01T15:18:43.100Z");
            Lang = "string";
            SpriteLast = 0;
            RevokeLast = 0;
        }
    }
}
