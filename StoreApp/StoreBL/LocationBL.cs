using System.Linq;
using System.Collections.Generic;
using StoreModels;
using StoreDL;
using System;
using Serilog;

namespace StoreBL
{

    /// <summary>
    /// Business logic class for location model
    /// </summary>
    public class LocationBL : ILocationBL
    {

        private readonly IRepository _repo;
        public LocationBL(IRepository repo) {
            _repo = repo;
        }

        public Location AddLocation(Location location)
        {
            if (_repo.GetLocation(location) != null) {
                Log.Information("Location already exists");
                throw new Exception ("Location already exists");
            }
            Log.Information("BL sent location to DL");
            return _repo.AddLocation(location);
        }

        public List<Location> GetAllLocations()
        {
            Log.Information("BL attempt to retrieve list of all locations from DL");
            return _repo.GetAllLocations();
        }

        public Location GetLocationById(int locationId)
        {
            List<Location> locations = GetAllLocations();
            if (locations.Count == 0) {
                Log.Information("No Locations Found");
                throw new Exception ("No Locations Found");
            } else {
                foreach (Location location in locations) {
                    if (locationId.Equals(location.LocationID)) {
                        Log.Information("BL sent location to UI");
                        return location;
                    }
                }
                Log.Information("No matching locations found");
                throw new Exception ("No matching locations found"); 
            }
        }
        public Location GetLocation(string locationName)
        {
            List<Location> locations = GetAllLocations();
            if (locations.Count == 0) {
                Log.Information("No locations found");
                throw new Exception ("No Locations Found");
            } else {
                foreach (Location location in locations) {
                    if (locationName.Equals(location.StoreName)) {
                        Log.Information("BL sent location to UI");
                        return location;
                    }
                }
                Log.Information("No matching locations found");
                throw new Exception ("No matching locations found"); 
            }
        }

        public Location DeleteLocation(Location location)
        {
            Location toBeDeleted = _repo.GetLocation(location);
            if (toBeDeleted != null)
            {
                Log.Information("BL sent location to DL for deletion");
                return _repo.DeleteLocation(toBeDeleted);
            } else
            {
                throw new Exception("Location Does Not Exist");
            }
        }

        public Location EditLocation(Location location)
        {
            Log.Information("BL sent updated location to DL");
            return _repo.EditLocation(location);
        }
    }
}