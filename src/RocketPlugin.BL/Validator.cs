namespace RocketPlugin.BL
{
    public static class Validator
    {
        public static bool ValidateValue(double min, double max, double value)
        {
            return value <= max && value >= min;
        }
    }
}
