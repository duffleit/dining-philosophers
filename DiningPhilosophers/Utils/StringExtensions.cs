namespace DiningPhilosophers.Utils
{
    public static class StringExtensions
    {
        public static int ParseAsIntOrSetDefaul(this string parseString, int defaultValue)
        {
            int parsedInt;
            return (int.TryParse(parseString, out parsedInt)) ? parsedInt : defaultValue;
        }
    }
}