using System.Text.RegularExpressions;
using WebApi.ViewModels;

namespace WebApi.Services
{
    public static class UserValidation
    {
        private static bool UserUserNameCheck(string userName)
        {
            if (userName.Length < 5 || userName.Length > 50)
            {
                //The Username length must be in range 5-50
                return false;
            }

            string pattern = @"^[A-Za-z0-9]*$";
            if (!Regex.IsMatch(userName, pattern))
            {
                //you can only use numbers and letters for user name
                return false;
            }

            return true;
        }
        private static bool UserPasswordCheck(string password)
        {
            if (password.Length < 8 || password.Length > 30)
            {
                //The Password length must be in range 8-30"
                return false;
            }

            string pattern = @"^[A-Za-z0-9@#$%^&*+=_!]*$";
            if (!Regex.IsMatch(password, pattern))
            {
                //you can only use numbers and letters and this symboles {@ # $ % ^ & * + = _ !} for password
                return false;
            }

            string pattern_az = @"[a-z]+";
            if (!Regex.IsMatch(password, pattern_az))
            {
                //The pass word must contain small letter
                return false;
            }

            string pattern_AZ = @"[A-Z]+";
            if (!Regex.IsMatch(password, pattern_AZ))
            {
                //The pass word must contain capital letter 
                return false;
            }

            string pattern_09 = @"[0-9]+";
            if (!Regex.IsMatch(password, pattern_09))
            {
                //The pass word must contain at least one number
                return false;
            }

            return true;
        }
        private static bool UserEmailCheck(string email)
        {
            if (email == null)
            {
                //please enter your email
                return false;
            }
            string pattern = @"^[a-zA-Z0-9]+(?:[a-zA-Z0-9._-]*[a-zA-Z0-9])?@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, pattern))
            {
                //please enter a correct email address.
                return false;
            }
            return true;
        }
        private static bool UserNameCheck(string name)
        {
            if (name.Length == 0)
            {
                //please enter your name
                return false;
            }
            if (name.Length > 30)
            {
                //maximum length of name is 30 characters
                return false;
            }
            return true;
        }

        public static bool IsUserValid(this UserViewModel user)
        {
            if (user.Image.Length > 1048576)
            { return false; }

            if (!UserUserNameCheck(user.UserName))
            { return false; }

            if (!UserPasswordCheck(user.Password))
            { return false; }

            if (!UserEmailCheck(user.Email))
            { return false; }

            if (!UserNameCheck(user.FirstName))
            { return false; }

            if (!UserNameCheck(user.LastName))
            { return false; }

            return true;
        }
    }
}
