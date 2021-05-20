using System.Collections.Generic;
namespace StoreModels
{
    /// <summary>
    /// Location Model
    /// </summary>
    public class Location
    {
        public Location(string storeName, string address, string city, string state) {
            this.StoreName = storeName;
            this.Address = address;
            this.City = city;
            this.State = state;
        }

        public Location() {
            
        }

        public Location(int locationId, string storeName, string address, string city, string state) : this(storeName, address, city, state) {
            this.LocationID = locationId;
        }

        /// <summary>
        /// This represents a unique value for every location
        /// </summary>
        /// <value></value>
        public int LocationID { get; set; }
        /// <summary>
        /// This represents the street address of a store location
        /// </summary>
        /// <value></value>
        public string Address { get; set; }

        /// <summary>
        /// This represents the city of a store location
        /// </summary>
        /// <value></value>
        public string City { get; set; }

        /// <summary>
        /// This represents the state of a store location
        /// </summary>
        /// <value></value>
        public string State { get; set; }

        /// <summary>
        /// This represents the name of a store location
        /// </summary>
        /// <value></value>
        public string StoreName { get; set; }

        public List<Order> Orders { get; set; }

        public List<Inventory> Inventories { get; set; }


        public override string ToString()
        {
            return $"Name: {StoreName} \nAdress: {Address} \nCity: {City} \nState: {State}";
        }

        public bool Equals(Location location) {
            return this.StoreName.Equals(location.StoreName);
        }
    }
}