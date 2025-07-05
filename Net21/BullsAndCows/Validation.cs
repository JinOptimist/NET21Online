namespace BullsAndCows
{
    public static class Validation
    {
        public static bool IsValid(string input)
        {
            if (input.Length != 4)
            {
                return false;
            }

            return input.All(char.IsDigit) && input.Distinct().Count() == 4;
        }
    }
}
