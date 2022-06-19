using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calories.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Proszę wpisać imię")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Proszę wpisać login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Proszę wpisać hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Proszę wpisać email")]
        [EmailAddress]
        public string Email { get; set; }

        public bool checkPassword(string pwd, string pwdRep)
        {
            if (pwd == pwdRep)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        public bool verifyPassword(string pwd)
        {
            bool verified = BCrypt.Net.BCrypt.Verify(pwd, Password);
            if (verified)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void changePassword(string oldPwd, string newPwd, string repNewPwd)
        {
            // ...
        }

        public bool checkUserInDatabsa(UserModel newUser)
        {
            using (var db = new MyDbContext())
            {
                var usr = db.Users.Any(u => u.Login == newUser.Login || u.Email == newUser.Email);

                if (usr)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
