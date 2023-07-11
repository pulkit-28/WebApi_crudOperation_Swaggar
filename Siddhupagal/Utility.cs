namespace Siddhupagal
{
    public class Utility
    {
        public static bool hasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%^&*/()=?><@{}.-=+_',;:";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;

            }
            return false;
        }
        public static bool hasNumeric(string input)
        {
            string numeric = @"0123456789";
            foreach (var item1 in numeric)
            {
                if (input.Contains(item1)) return true;
            }
            return false;
        }
    }
}
