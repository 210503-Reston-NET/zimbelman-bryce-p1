using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using StoreModels;

namespace StoreWebUI.Models
{

    public class LocationVM
    {

        public LocationVM(Location location)
        {
            LocationID = location.LocationID;
            StoreName = location.StoreName;
            City = location.City;
            State = location.State;
            Address = location.Address; 
        }

        public LocationVM()
        {
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
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// This represents the city of a store location
        /// </summary>
        /// <value></value>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// This represents the state of a store location
        /// </summary>
        /// <value></value>
        [Required]
        [DisplayName("State or Province")]
        public string State { get; set; }

        /// <summary>
        /// This represents the name of a store location
        /// </summary>
        /// <value></value>
        [Required]
        [DisplayName("Store Name")]
        public string StoreName { get; set; }
    }
}
