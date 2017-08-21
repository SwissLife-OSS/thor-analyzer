namespace ChilliCream.Logging.Analyzer
{
    internal static class ObjectExtensions
    {
        public static bool IsDefault(this object value)
        {
            if (value == null)
            {
                return true;
            }

            return value.Equals(value.GetType().Default());
        }
    }
}