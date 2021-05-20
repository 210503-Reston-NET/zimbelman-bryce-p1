using System.Collections.Generic;
using StoreModels;

namespace StoreBL
{
    public interface ILocationBL
    {
        /// <summary>
        /// Business Logic to add a location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
         Location AddLocation(Location location);
         /// <summary>
         /// Business Logic to retrive all locations
         /// </summary>
         /// <returns></returns>
         List<Location> GetAllLocations();
         /// <summary>
         /// Business Logic to retrive specific location
         /// </summary>
         /// <param name="locationId"></param>
         /// <returns></returns>
         Location GetLocation(int locationId);
         /// <summary>
         /// Business Logic to retrieve specific location
         /// </summary>
         /// <param name="locationName"></param>
         /// <returns></returns>
         Location GetLocation(string locationName);
    }
}