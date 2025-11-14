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
        public async Task<SaleOrderResponse> getSaleOrderById(int saleOrderId)
        {
            var getSaleOrderResponse = method.GetCallApi(saleURL + "SaleOrder/GetById?saleOrderId=" + saleOrderId);
            if (string.IsNullOrWhiteSpace(getSaleOrderResponse))
                return new SaleOrderResponse();
            var getSaleOrder = JsonConvert.DeserializeObject<SaleOrderResponse>(getSaleOrderResponse)
                ?? new SaleOrderResponse();
            return getSaleOrder;
        }

        public async Task<SaleOrderItemsResponse> getSaleOrderItemByItemIdAndShadeIdAndSaleOrderItemId(int itemId, int shadeId, int saleOrderItemId)
        {
            var getSaleOrderItemResponse = method.GetCallApi(saleURL + "SaleOrderItems/GetByItemIdAndShadeIdAndSaleOrderItemId?itemId=" + itemId + "&shadeId=" + shadeId + "&saleOrderItemId=" + saleOrderItemId);
            if (string.IsNullOrWhiteSpace(getSaleOrderItemResponse))
                return new SaleOrderItemsResponse();
            var getSaleOrderItem = JsonConvert.DeserializeObject<SaleOrderItemsResponse>(getSaleOrderItemResponse)
                ?? new SaleOrderItemsResponse();
            return getSaleOrderItem;
        }

        public async Task<SaleOrderItemsResponse> getSaleOrderItemById(int saleOrderItemsId)
        {
            var getSaleOrderResponse = method.GetCallApi(saleURL + "SaleOrderItems/GetById?saleOrderItemsId=" + saleOrderItemsId);
            if (string.IsNullOrWhiteSpace(getSaleOrderResponse))
                return new SaleOrderItemsResponse();
            var getSaleOrder = JsonConvert.DeserializeObject<SaleOrderItemsResponse>(getSaleOrderResponse)
                ?? new SaleOrderItemsResponse();
            return getSaleOrder;
        }
    }
}