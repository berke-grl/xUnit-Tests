using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StocksService : IStocksService
    {

        private readonly List<BuyOrder> _buyOrders;
        private readonly List<SellOrder> _sellOrders;

        public StocksService()
        {
            _buyOrders = new List<BuyOrder>();
            _sellOrders = new List<SellOrder>();
        }
        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            //Validation: buyOrderRequest can't be null
            if (buyOrderRequest == null) throw new ArgumentNullException(nameof(buyOrderRequest));

            //Model Validation
            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            buyOrder.BuyOrderID = Guid.NewGuid();

            _buyOrders.Add(buyOrder);

            //convert the BuyOrder object into BuyOrderResponse type
            return buyOrder.ToBuyOrderResponse();
        }

        public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null) throw new ArgumentNullException(nameof(sellOrderRequest));

            //Model Validation
            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderID = Guid.NewGuid();

            _sellOrders.Add(sellOrder);

            return sellOrder.ToSellOrderResponse();
        }

        public List<BuyOrderResponse> GetBuyOrders()
        {
            return
                _buyOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).Select(temp => temp.ToBuyOrderResponse()).ToList();
        }

        public List<SellOrderResponse> GetSellOrders()
        {
            return
                _sellOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).Select(temp => temp.ToSellOrderResponse()).ToList();
        }
    }
}
