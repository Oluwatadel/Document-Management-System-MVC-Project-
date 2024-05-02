using DMSMVC.Service.Interface;
using System.Text.RegularExpressions;

namespace DMSMVC.Service.Implementation
{
    public class RegexCheck : IRegexCheck
    {
        public bool IsValidPin(string input)
        {
            string phnRegex = @"([0-9]{6,8}$)";
            Regex re = new Regex(phnRegex);
            if (re.IsMatch(input))
            {
                return true;
            }
            return false;

        }
        
        public bool IsValidPhoneNumber(string input)
        {
            string phnRegex = @"([0][7-9][0-1][0-9]{8}$)";
            Regex re = new Regex(phnRegex);
            if (re.IsMatch(input))
            {
                return true;
            }
            return false;

        }

        public bool IsValidEmail(string input)
        {
            string phnRegex = @"\b[a-z0-9]+@[a-z0-9]+\.[a-z]{2,}\b";
            Regex re = new Regex(phnRegex);
            if (re.IsMatch(input))
            {
                return true;
            }
            return false;

        }
    }
}
