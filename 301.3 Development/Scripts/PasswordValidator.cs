using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301._3_Development.Scripts
{
    public class PasswordValidator
    {
        private string error = "";
        public bool Validate(string password, string email = null)
        {
            

            if (string.IsNullOrWhiteSpace(password))
            {
                error = "Password cannot be empty.";
                return false;
            }

            if (password.Length < 12)
            {
                error = "Password must be at least 12 characters long.";
                return false;
            }

            if (!password.Any(char.IsUpper))
            {
                error = "Password must contain at least one uppercase letter.";
                return false;
            }

            if (!password.Any(char.IsLower))
            {
                error = "Password must contain at least one lowercase letter.";
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                error = "Password must contain at least one digit.";
                return false;
            }

            if (!password.Any(ch => "!@#$%^&*()_+-=[]{};:'\",.<>/?\\|`~".Contains(ch)))
            {
                error = "Password must contain at least one symbol.";
                return false;
            }

            if (email != null && password.Contains(email.Split('@')[0], StringComparison.OrdinalIgnoreCase))
            {
                error = "Password cannot contain your email";
                return false;
            }

            return true;
        }

        public string GetError()
        {
            return error;
        }
    }

}
