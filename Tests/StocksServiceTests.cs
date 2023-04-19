using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace StocksServiceTests
{
    public class StocksServiceTests
    {
        private readonly IStocksService _stocksService;

        public StocksServiceTests()
        {
            _stocksService = new StocksService();
        }

        #region CreateBuyOrder
        [Fact]
        public void CreateBuyOrder_NullRequest()
        {
            //Arrange
            BuyOrderRequest? request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _stocksService.CreateBuyOrder(request);
            });
        }

        [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
        [InlineData(0)] //passing parameters to the tet method
        public void CreateBuyOrder_NoQuantity(uint quantity)
        {
            //Arrange
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Price = 1,
                Quantity = quantity
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stocksService.CreateBuyOrder(request);
            });
        }

        [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
        [InlineData(100001)] //passing parameters to the tet method
        public void CreateBuyOrder_GreaterQuantity(uint quantity)
        {
            //Arrange
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Price = 1,
                Quantity = quantity
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stocksService.CreateBuyOrder(request);
            });
        }

        [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
        [InlineData(0)] //passing parameters to the tet method
        public void CreateBuyOrder_ZeroPrice(double price)
        {
            //Arrange
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Price = price,
                Quantity = 1
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stocksService.CreateBuyOrder(request);
            });
        }

        [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
        [InlineData(10001)] //passing parameters to the tet method
        public void CreateBuyOrder_GreaterPrice(double price)
        {
            //Arrange
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Price = price,
                Quantity = 1
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public void CreateBuyOrder_NullSymbol()
        {
            //Arrange
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                StockName = null,
                StockSymbol = "MSFT",
                Price = 1,
                Quantity = 1
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public void CreateBuyOrder_OlderDate()
        {
            //Arrange
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Price = 1,
                Quantity = 1,
                DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31")

            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public void CreateBuyOrder_ValidData()
        {
            //Arrange
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                Price = 1,
                Quantity = 1,
                DateAndTimeOfOrder = Convert.ToDateTime("2000-12-31")
            };

            //Act
            BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(request);

            //Assert
            Assert.NotEqual(Guid.Empty, buyOrderResponse.BuyOrderID);
        }
        #endregion

        #region CreateSellOrder
        [Fact]
        public void CreateSellOrder_NullRequest()
        {
            //Arrange
            SellOrderRequest? request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateSellOrder(request);
            });

        }

        [Theory]
        [InlineData(0)]
        public void CreateSellOrder_ZeroQuantity(uint quantity)
        {
            //Arrange
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = quantity,
                Price = 1,
                StockName = "Microsoft",
                StockSymbol = "MSFT"
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(request);
            });
        }

        [Theory]
        [InlineData(100001)]
        public void CreateSellOrder_GreaterQuantity(uint quantity)
        {
            //Arrange
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = quantity,
                Price = 1,
                StockName = "Microsoft",
                StockSymbol = "MSFT"
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(request);
            });
        }

        [Theory]
        [InlineData(0)]
        public void CreateSellOrder_ZeroPrice(double price)
        {
            //Arrange
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 1,
                Price = price,
                StockName = "Microsoft",
                StockSymbol = "MSFT"
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(request);
            });
        }

        [Theory]
        [InlineData(10001)]
        public void CreateSellOrder_GreaterPrice(double price)
        {
            //Arrange
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 1,
                Price = price,
                StockName = "Microsoft",
                StockSymbol = "MSFT"
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public void CreateSellOrder_NullSymbol()
        {
            //Arrange
            SellOrderRequest? request = new SellOrderRequest()
            {
                StockSymbol = null,
                Quantity = 1,
                Price = 1,
                StockName = "Microsoft"
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public void CreateSellOrder_OlderDate()
        {
            //Arrange
            SellOrderRequest? request = new SellOrderRequest()
            {
                StockSymbol = null,
                Quantity = 1,
                Price = 1,
                StockName = "Microsoft",
                DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31")
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public void CreateSellOrder_ValidData()
        {
            //Arrange
            SellOrderRequest? request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = Convert.ToDateTime("2024-12-31"),
                Price = 1,
                Quantity = 1
            };

            //Act
            SellOrderResponse sellOrderResponse = _stocksService.CreateSellOrder(request);

            Assert.NotEqual(Guid.Empty, sellOrderResponse.SellOrderID);
        }
        #endregion

        #region GetAllBuyOrders
        [Fact]
        public void GetAllBuyOrders_Empty()
        {
            //Arrange
            List<BuyOrderResponse> buyOrders = _stocksService.GetBuyOrders();

            //Assert
            Assert.Empty(buyOrders);

        }

        [Fact]
        public void GetAllBuyOrders_Check()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest1 = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00")
            };
            BuyOrderRequest buyOrderRequest2 = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00")
            };
            List<BuyOrderRequest> buyOrderRequests = new List<BuyOrderRequest>() { buyOrderRequest1, buyOrderRequest2 };

            List<BuyOrderResponse> buyOrderResponses = new List<BuyOrderResponse>();

            foreach (BuyOrderRequest request in buyOrderRequests)
            {
                BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(request);
                buyOrderResponses.Add(buyOrderResponse);
            }

            //Act
            List<BuyOrderResponse> buyOrders_from_get = _stocksService.GetBuyOrders();

            //Assert
            foreach (BuyOrderResponse buyOrderResponse in buyOrderResponses)
            {
                Assert.Contains(buyOrderResponse, buyOrders_from_get);
            }
        }
        #endregion

        #region GetAllSellOrders
        [Fact]
        public void GetAllSellOrders_Empty()
        {
            //Arrange
            List<SellOrderResponse>? sellOrderResponses = _stocksService.GetSellOrders();

            //Assert
            Assert.Empty(sellOrderResponses);
        }
        [Fact]
        public void GetAllSellOrders_Checks()
        {
            //Arrange
            SellOrderRequest sellOrderRequest1 = new SellOrderRequest()
            {
                Quantity = 1,
                Price = 1,
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00")
            };
            SellOrderRequest sellOrderRequest2 = new SellOrderRequest()
            {
                Quantity = 1,
                Price = 1,
                StockName = "Microsoft",
                StockSymbol = "MSFT",
                DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00")
            };
            List<SellOrderRequest> sellOrderRequests = new List<SellOrderRequest>()
            {
                sellOrderRequest1,
                sellOrderRequest2
            };

            List<SellOrderResponse> sellOrderResponses = new List<SellOrderResponse>();

            foreach (SellOrderRequest request in sellOrderRequests)
            {
                SellOrderResponse response = _stocksService.CreateSellOrder(request);
                sellOrderResponses.Add(response);
            }

            //act
            List<SellOrderResponse> sellOrderResponses_from_get = _stocksService.GetSellOrders();

            foreach(SellOrderResponse response in sellOrderResponses)
            {
                //Assert
                Assert.Contains(response, sellOrderResponses_from_get);
            }
        }
        #endregion
    }
}