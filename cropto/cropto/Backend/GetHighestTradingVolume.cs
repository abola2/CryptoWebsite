
namespace cropto.Backend
{
    
    public class GetHighestTradingVolume 
    {

        /// <summary>
        /// Get highest trading volume
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public double GetHighestTradingVolumesInRange(double start, double end)
        {
             return HighestTradingVolume(DataFetch.GetBitcoinMarketValue, start, end); 
        }


        private double HighestTradingVolume(Dictionary<double, double> prices, double start, double end)
        {
            IEnumerable<double> stringsInRange = prices.Where(pair => pair.Key >= start && pair.Key <= end).Select(pair => pair.Value);
            double time = 0;
            try {
                double max = stringsInRange.Max();
                time = prices.FirstOrDefault(x => x.Value == max).Key;
            } catch(System.InvalidOperationException ex){
                Console.WriteLine("Invalid dates: "+ex);
            }
            return time;
        }
    }
}