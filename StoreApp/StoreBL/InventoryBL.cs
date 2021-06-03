using System.Collections.Generic;
using StoreModels;
using StoreDL;
using System;
using System.Linq;
using Serilog;

namespace StoreBL
{
    /// <summary>
    /// Business Logic class for inventory model
    /// </summary>
    public class InventoryBL : IInventoryBL
    {
        private readonly IRepository _repo;
        private readonly ILocationBL _locationBL;
        private readonly IProductBL _productBL;
        public InventoryBL(IRepository repo, ILocationBL locationBL, IProductBL productBL) {
            _repo = repo;
            _locationBL = locationBL;
            _productBL = productBL;
        }

        public Inventory AddInventory(Inventory inventory)
        {
            if (_repo.GetStoreInventory(inventory) != null)
            {
                Log.Information("Store inventory already exists");
                throw new Exception("Store inventory already exists");
            }
            Log.Information("BL sent new store inventory to DL");
            Location location = _locationBL.GetLocationById(inventory.LocationID);
            Product product = _productBL.GetProductById(inventory.ProductID);
            return _repo.AddInventory(inventory, location, product);
        }

        public Inventory EditInventory(Inventory inventory)
        {
            _repo.UpdateInventory(inventory);
            Log.Information("Updated inventory sent to DL");
            return inventory;
        }

        public List<Inventory> GetAllInventories()
        {
            Log.Information("BL attempt to retreive all inventory from DL");
            return _repo.GetAllInventories();
        }

        public Inventory GetStoreInventory(int inventoryId)
        {
            Log.Information("BL retrieve store inventory from DL");
            return _repo.GetStoreInventory(inventoryId);
        }

        public List<Inventory> GetStoreInventoryByLocation(int locationId)
        {
            List<Inventory> inventories = _repo.GetAllInventories();
            List<Inventory> storeInventory = new List<Inventory>();
                foreach (Inventory inventory in inventories) {
                    if (locationId.Equals(inventory.LocationID)) {
                    storeInventory.Add(inventory);
                    }
                }
            if (storeInventory.Any())
            {
                Log.Information("BL sent inventory to UI");
                return storeInventory;
            }
            if (!inventories.Any()) {
                Log.Information("No inventories found");
                throw new Exception("No inventories found");
            } else {
                Log.Information("No matching locations found");
                throw new Exception ("No matching locations found");
            }
    }

        public List<int> ReplenishInventory(string nameOfStore, List<int> productQuantity)
        {
            int i = 0;
            List<Inventory> inventories = _repo.GetAllInventories();
            List<Product> products = _repo.GetAllProducts();
            List<int> updatedInventory = new List<int>();
            Log.Information("BL attempt to retrive specific location from DL");
            Location location = _locationBL.GetLocation(nameOfStore);
            bool inventoryUpdated = false;
            foreach (Product item in products)
            {
                inventoryUpdated = false;
                // If no inventory entries exist
                if (!inventories.Any()) {
                    Inventory newInventory = new Inventory(location.LocationID, item.ProductID, productQuantity[i]);
                    updatedInventory.Add(productQuantity[i]);
                    i++;
                    Log.Information("BL sent new inventory to DL");
                    _repo.AddInventory(newInventory, location, item);
                    inventoryUpdated = true;
                }
                foreach (Inventory inventory in inventories) {
                    // If match is found update inventory
                    if (inventory.LocationID.Equals(location.LocationID) && inventory.ProductID.Equals(item.ProductID) && !inventoryUpdated) {
                        inventory.Quantity += productQuantity[i];
                        updatedInventory.Add(inventory.Quantity);
                        i++;
                        Log.Information("BL sent updated inventory to DL");
                        _repo.UpdateInventory(inventory);
                        inventoryUpdated = true;
                        }
                    }
                    // If inventory exists but specific item does not
                    if (!inventoryUpdated) {
                        Inventory newInventory = new Inventory(location.LocationID, item.ProductID, productQuantity[i]);
                        updatedInventory.Add(productQuantity[i]);
                        i++;
                        Log.Information("BL sent new inventory to DL");
                        _repo.AddInventory(newInventory, location, item);
                    }
                }
                Log.Information("BL sent updated inventory to UI");
                return updatedInventory;
            }

        public List<int> SubtractInventory(string nameOfStore, List<int> quantity)
        {
            int i = 0;
            List<Inventory> inventories = _repo.GetAllInventories();
            List<Product> products = _repo.GetAllProducts();
            List<int> updatedInventory = new List<int>();
            Log.Information("BL attempt to retrieve specific location from DL");
            Location location = _locationBL.GetLocation(nameOfStore);
            foreach (Product item in products)
            {
                // If match found update inventory
                foreach (Inventory inventory in inventories) {
                    if (inventory.LocationID.Equals(location.LocationID) && inventory.ProductID.Equals(item.ProductID)) {
                        // If not enough inventory exists for purchase, throw exception
                        if (inventory.Quantity-quantity[i] < 0) {
                            throw new Inventory.NotEnoughInventoryException("Not enough item in inventory");
                        }
                        inventory.Quantity -= quantity[i];
                        updatedInventory.Add(inventory.Quantity);
                        i++;
                        Log.Information("BL sent updated inventory to DL");
                        _repo.UpdateInventory(inventory);
                        }
                    }
                }
                Log.Information("BL sent updated inventory to UI");
                return updatedInventory;
            }
        }
    }
