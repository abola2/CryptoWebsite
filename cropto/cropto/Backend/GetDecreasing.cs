namespace cropto.Backend
{
    
    public class GetDecreasing
    {

        /// <summary>
        /// Returns the length of the maximum decreasing sequence of Bitcoin values within the specified range of timestamps.
        /// </summary>
        /// <param name="start">The starting timestamp of the range.</param>
        /// <param name="end">The ending timestamp of the range.</param>
        /// <returns>The length of the maximum decreasing sequence of Bitcoin values within the specified range of timestamps.</returns>
        public double BitcoinvaluesSearch(double start, double end)
        {
             return GetMaxDecreasingSequence(DataFetch.GetBitcoinvaluesSearch, start, end); 
        }

        /// <summary>
        /// Returns the length of the maximum decreasing sequence of values in the specified range of prices.
        /// </summary>
        /// <param name="prices">A dictionary of prices, where the keys represent timestamps and the values represent prices.</param>
        /// <param name="start">The starting timestamp of the range.</param>
        /// <param name="end">The ending timestamp of the range.</param>
        /// <returns>The length of the maximum decreasing sequence of values in the specified range of prices.</returns>

        private double GetMaxDecreasingSequence(Dictionary<double, double> prices, double start, double end)
        {
            IEnumerable<double> stringsInRange = prices.Where(pair => pair.Key >= start && pair.Key <= end).Select(pair => pair.Value);

            int currentSequence = 0;
            int maxSequence = 0;
            double lastValue = 0;

            foreach (double value in stringsInRange)
            {
                if (value < lastValue)
                {
                    currentSequence++;
                }
                else
                {
                    maxSequence = Math.Max(maxSequence, currentSequence);
                    currentSequence = 0;
                }
                lastValue = value;
            }

            // Check if the current sequence is the new maximum
            maxSequence = Math.Max(maxSequence, currentSequence);

            // Return the maximum sequence
            return maxSequence;
        }
    }
}