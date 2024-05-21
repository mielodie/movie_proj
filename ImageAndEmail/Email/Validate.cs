using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace movie_project.ImageAndEmail.Email
{
    public class Validate
    {
        public static bool IsValidEmail(string email)
        {
            var emailAddress = new EmailAddressAttribute();
            return emailAddress.IsValid(email);
        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^(84|0[35789])[0-9]{8}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
