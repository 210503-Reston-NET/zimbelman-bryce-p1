using System.Collections.Generic;
using StoreModels;
namespace StoreBL
{
    public interface IInventoryBL
    {
        /// <summary>
        /// Business Logic to replenish store inventory
        /// </summary>
        /// <param name="nameOfStore"></param>
        /// <param name="numOfProducts"></param>
        /// <param name="productQuantity"></param>
        /// <returns></returns>
         List<int> ReplenishInventory(string nameOfStore, List<int> productQuantity);
        /// <summary>
        /// Business Logic to subtract store inventory 
        /// </summary>
        /// <param name="nameOfStore"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
         List<int> SubtractInventory(string nameOfStore, List<int> quantity);
         /// <summary>
         /// Business Logic to retrieve specific store inventory
         /// </summary>
         /// <param name="locationId"></param>
         /// <returns></returns>
         Inventory GetStoreInventoryByLocation(int locationId);
        /// <summary>
        /// Business Logic to retrieve specific inventory
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
         Inventory GetStoreInventory(int inventoryId);
        /// <summary>
        /// Business Logic to retrieve a list of all inventories
        /// </summary>
        /// <returns></returns>
         List<Inventory> GetAllInventories();
        /// <summary>
        /// Business Logic to add a specific store inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        Inventory AddInventory(Inventory inventory);
        /// <summary>
        /// Business Logic to edit a specific store inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        Inventory EditInventory(Inventory inventory);
    }
}