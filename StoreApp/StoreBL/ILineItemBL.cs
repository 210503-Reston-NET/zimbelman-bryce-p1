using System.Collections.Generic;
using StoreModels;
namespace StoreBL
{
    public interface ILineItemBL
    {
        /// <summary>
        /// Business Logic to add a line item
        /// </summary>
        /// <param name="lineItem"></param>
        /// <param name="product"></param>
        /// <returns></returns>
         LineItem AddLineItem(LineItem lineItem, Product product);
         /// <summary>
         /// Business Logic to retrieve all line items
         /// </summary>
         /// <returns></returns>
         List<LineItem> GetAllLineItems();
         /// <summary>
         /// Business Logic to retrieve specific line item
         /// </summary>
         /// <param name="orderID"></param>
         /// <returns></returns>
         List<LineItem> GetLineItems(int orderID);

        /// <summary>
        /// Business Logic to delete a specific line item
        /// </summary>
        /// <param name="lineItem"></param>
        /// <returns></returns>
         LineItem DeleteLineItem(LineItem lineItem);
    }
}