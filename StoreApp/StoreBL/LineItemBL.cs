using System.Collections.Generic;
using StoreModels;
using StoreDL;
using System;
using System.Linq;
using Serilog;

namespace StoreBL
{
    /// <summary>
    /// Business logic class for LineItem model
    /// </summary>
    public class LineItemBL : ILineItemBL
    {

        private IRepository _repo;
        public LineItemBL(IRepository repo) {
            _repo = repo;
        }
        public LineItem AddLineItem(LineItem lineItem, Product product)
        {
            Log.Information("BL sent line item to DL");
            return _repo.AddLineItem(lineItem, product);
        }

        public LineItem DeleteLineItem(LineItem lineItem)
        {
            Log.Information("BL sent line item to DL for deletion");
            return _repo.DeleteLineItem(lineItem);
        }

        public List<LineItem> GetAllLineItems()
        {
            Log.Information("BL attempt to retrieve all line items from DL");
            return _repo.GetAllLineItems();
        }

        public List<LineItem> GetLineItems(int orderID)
        {
            List<LineItem> lineItems = GetAllLineItems();
            List<LineItem> requestedLineItems = new List<LineItem>();
            if (lineItems.Count == 0) {
                Log.Information("No orders placed");
                throw new Exception("No orders placed");
            } else {
                foreach (LineItem lineItem in lineItems)
                {
                    if (orderID.Equals(lineItem.OrderID)) {
                        requestedLineItems.Add(lineItem);
                    }
                }
                if (!requestedLineItems.Any()) {
                    Log.Information("No matching order found");
                    throw new Exception("No matching order found");
                } else {
                    Log.Information("BL sent list of line items to UI");
                    return requestedLineItems;
                }
                
            }
        }
    }
}