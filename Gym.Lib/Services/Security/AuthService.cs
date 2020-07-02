using Gym.Lib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Lib.Services.Security
{
    public class AuthService : BaseService, IAuthService
    {
        public AuthService(gymEntities db) : base(db)
        {
        }


        #region Utils
        internal static string CalculateHash(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashedPwd = HashAlgorithm.Create("MD5").ComputeHash(bytes);
            return BitConverter.ToString(hashedPwd).Replace("-", "").ToLower();
        }
        #endregion
        //public bool RegisterUser(string name, string lastname, string email, string password, out int customerId)
        //{
        //    customerId = 0;
        //    if (CheckIfCustomerAlreadyExists(email))
        //    {
        //        throw new CustomerAlreadyExistsException(email);
        //    }
        //    var customer = new Customer
        //    {
        //        Email = email,
        //        Name = name,
        //        Surname = lastname,
        //        PasswordEncrypted = CalculateHash(password)
        //    };
        //    db.Customer.Add(customer);
        //    db.SaveChanges();
        //    customerId = customer.Id;
        //    return true;
        //}

        public string ValidateUser(string email, string password, out int userId)
        {
            userId = 0;
            var user = db.User.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user == null)
                return "No User found with this email";

            var hashedPwd = CalculateHash(password);
            bool isValid = hashedPwd == user.PasswordEncrypted;
            if (!isValid)
                return "Check provided password";
            userId = user.Id;
            return null;
        }

        public User GetUserById(int userId)
        {
            return db.User.FirstOrDefault(c => c.Id == userId);
        }



        public bool CheckIfUserAlreadyExists(string email)
        {
            return db.User.FirstOrDefault(x => x.Email == email) != null;
        }
    }
}
