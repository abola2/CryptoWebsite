
namespace cropto.Backend
{
    
    public class GetLowestTradingVolume
    {

        public (double, double, double, double) GetBestDaySellAndBuy(double startDate, double endDate)
        {
            return GetBestDaySellAndBuyInTimestampRange(DataFetch.GetBitcoinvaluesSearch, startDate, endDate); 
        }

        /// <summary>
        /// Finds the best day buy and sell trading volume within a specific time range in a dictionary of prices.
        /// </summary>
        /// <param name="prices">Dictionary containing price data (key: price, value: volume).</param>
        /// <param name="start">Starting price of the range.</param>
        /// <param name="end">Ending price of the range.</param>
        /// <returns>A tuple containing the lowest and highest trading volumes within the price range.</returns>
        private (double, double, double, double) GetBestDaySellAndBuyInTimestampRange(Dictionary<double, double> prices, double start, double end)
            {
                IEnumerable<double> stringsInRange = prices.Where(pair => pair.Key >= start && pair.Key <= end).Select(pair => pair.Value);
                double maxPrice = stringsInRange.DefaultIfEmpty(0.0).Max();
                double searchMin = stringsInRange.TakeWhile(num => num != maxPrice).Concat(new[] { maxPrice }).Min();
                double minPrice = stringsInRange.Where(num => num >= searchMin && num <= maxPrice).DefaultIfEmpty(0.0).Min();
                double minDate = DataFetch.GetBitcoinvaluesSearch.FirstOrDefault(x => x.Value == minPrice).Key;
                double maxDate = DataFetch.GetBitcoinvaluesSearch.FirstOrDefault(x => x.Value == maxPrice).Key;
                return (minPrice, minDate, maxPrice, maxDate);
            }
    }
}