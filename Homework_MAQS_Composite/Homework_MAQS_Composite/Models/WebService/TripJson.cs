using Newtonsoft.Json;


namespace Models.WebService
{

    public class TripUser
    {
        /// <summary>
        /// Gets or sets the User ID
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the Nuber of Trips
        /// </summary>
        [JsonProperty("numberOfTrips")]
        public int NumberOfTrips { get; set; }

    }

    public class UserJson
    {
        [JsonProperty("value")]
        public TripUser Value { get; set; }

    }

    public class TripJson
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int numberOfStops { get; set; }
        public Stop[] stops { get; set; }
    }

    public class Stop
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
