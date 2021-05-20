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
         Inventory GetStoreInventory(int locationId);
         List<Inventory> GetAllInventories();
    }
}