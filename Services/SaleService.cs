using Newtonsoft.Json;
using PackingApplication.Helper;
using PackingApplication.Models.RequestEntities;
using PackingApplication.Models.ResponseEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Services
{
    public class SaleService
    {
        HTTPMethod method = new HTTPMethod();
        string saleURL = ConfigurationManager.AppSettings["saleURL"];
        public SaleOrderResponse getSaleOrderById(int saleOrderId)
        {
            var getSaleOrderResponse = method.GetCallApi(saleURL + "SaleOrder/GetById?saleOrderId=" + saleOrderId);
            var getSaleOrder = JsonConvert.DeserializeObject<SaleOrderResponse>(getSaleOrderResponse);
            return getSaleOrder;
        }

        public SaleOrderItemsResponse getSaleOrderItemByItemIdAndShadeIdAndSaleOrderItemId(int itemId, int shadeId, int saleOrderItemId)
        {
            var getSaleOrderItemResponse = method.GetCallApi(saleURL + "SaleOrderItems/GetByItemIdAndShadeIdAndSaleOrderItemId?itemId=" + itemId + "&shadeId=" + shadeId + "&saleOrderItemId=" + saleOrderItemId);
            var getSaleOrderItem = JsonConvert.DeserializeObject<SaleOrderItemsResponse>(getSaleOrderItemResponse);
            return getSaleOrderItem;
        }

        public SaleOrderItemsResponse getSaleOrderItemById(int saleOrderItemsId)
        {
            var getSaleOrderResponse = method.GetCallApi(saleURL + "SaleOrderItems/GetById?saleOrderItemsId=" + saleOrderItemsId);
            var getSaleOrder = JsonConvert.DeserializeObject<SaleOrderItemsResponse>(getSaleOrderResponse);
            return getSaleOrder;
        }
    }
}
