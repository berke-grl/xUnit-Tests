using Microsoft.Extensions.Configuration;
using ServiceContracts;
using System.Text.Json;

namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public Dictionary<string, object>? GetCompanyProfile(string stockSymbol)
        {
            //Create Http Client
            HttpClient httpClient = _httpClientFactory.CreateClient();

            //Create Http Request
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["apiKey"]}"),
                Method = HttpMethod.Get
            };

            //Send Http Request
            HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);

            //Read Response
            string response = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();

            //Convert Response to Dictinoary type
            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

            //Error or Invalid data checks
            if (responseDictionary == null) throw new InvalidOperationException("No response from server");
            if (responseDictionary.ContainsKey("error")) throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            return responseDictionary;
        }

        public Dictionary<string, object>? GetStockPriceQuote(string stockSymbol)
        {
            //Create Http Client
            HttpClient httpClient = _httpClientFactory.CreateClient();

            //Create Http Request
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["apiKey"]}"),
                Method = HttpMethod.Get
            };

            //Send Http Request
            HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);

            //Read Http Response
            string response = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();

            //Convert Response to Dictinoary type
            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

            //Error or Invalid data checks
            if (responseDictionary == null) throw new InvalidOperationException("No response from server");
            if (responseDictionary.ContainsKey("error")) throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            return responseDictionary;
        }
    }
}