using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using cropto.Shared;

namespace cropto.Backend
{
    public class DataFetch
    {

        private static Dictionary<double, double> bitcoinvaluesS = new Dictionary<double, double>();

        private static Dictionary<double, double> marketValue = new Dictionary<double, double>();

        public static Dictionary<double, double> GetBitcoinMarketValue {
            get {return marketValue;}
        }
    
        public static Dictionary<double, double> GetBitcoinvaluesSearch
        {
            get { return bitcoinvaluesS; }
        }


        /// <summary>
        /// Starts a timer that repeatedly calls an asynchronous method to retrieve Bitcoin values in Euros from the specified API URL.
        /// </summary>
        /// <param name="_apiUrl">The URL of the API to retrieve Bitcoin values from.</param>
        public void ApiSearchDataTask(string _apiUrl)
        {
            Timer timer = new Timer(async state => bitcoinvaluesS = await GetBitcoinValueInEurosInRange(_apiUrl), null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
        }


        private async Task<Dictionary<double, double>> GetBitcoinValueInEurosInRange(string _apiUrl)
        {
            Dictionary<double, double> bitcoinvalues = new Dictionary<double, double>();

            try
            {
                using HttpClient _httpClient = new();

                using HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl);

                HttpResponseMessage succes = response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                JsonResultParser user = JsonConvert.DeserializeObject<JsonResultParser>(jsonResponse);

                //0 = bitcoin value in euros
                //1 = timestamp
                
                user.Prices.ForEach(values => bitcoinvalues.Add(values[0], values[1]));
                //marketValue.Add(value[0], value[1])
                user.Total_volumes.ForEach(value => marketValue.Add(value[0], value[1]));

                _httpClient.Dispose();

            }

            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                //NOTIFY USER
            }
            return bitcoinvalues;
        }

    }

}
