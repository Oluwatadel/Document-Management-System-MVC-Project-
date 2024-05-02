using System.Text.RegularExpressions;

namespace DMSMVC.Service.Interface
{
    public interface IRegexCheck
    {
        bool IsValidPin(string input);
        bool IsValidEmail(string input);
        bool IsValidPhoneNumber(string input);
    }
}
