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

        public SaleOrderItemsResponse getSaleOrderItemByItemIdAndShadeIdAndSaleOrderId(int itemId, int shadeId, int saleOrderId)
        {
            var getSaleOrderItemResponse = method.GetCallApi(saleURL + "SaleOrderItems/GetByItemIdAndShadeIdAndSaleOrderId?itemId=" + itemId + "&shadeId=" + shadeId + "&saleOrderId=" + saleOrderId);
            var getSaleOrderItem = JsonConvert.DeserializeObject<SaleOrderItemsResponse>(getSaleOrderItemResponse);
            return getSaleOrderItem;
        }

    }
}
