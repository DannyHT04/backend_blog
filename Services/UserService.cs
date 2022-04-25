using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_blog.Models;
using backend_blog.Models.DTO;
using backend_blog.Services.Context;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend_blog.Services
{
    public class UserService : ControllerBase
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _context.UserInfo;
        }
        public bool DoesUserExist(string? username)
        {
            // check the table to see if the username exist.
            return _context.UserInfo.SingleOrDefault(user => user.Username == username) != null;
        }
        public bool AddUser(CreateAccountDTO UserToADD)
        {

            bool result = false;
            if (!DoesUserExist(UserToADD.Username))
            {
                UserModel newUser = new UserModel();
                var hashedPassword = HashPassword(UserToADD.Password);
                newUser.Id = UserToADD.Id;
                newUser.Username = UserToADD.Username;
                newUser.Salt = hashedPassword.Salt;
                newUser.Hash = hashedPassword.Hash;

                _context.Add(newUser);

                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public UserModel GetUserByUsername(string username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username);
        }

         public UserIdDTO GetUserIdDTOByUsername(string username)
        {
            var UserInfo = new UserIdDTO();
             var foundUser = _context.UserInfo.SingleOrDefault(user => user.Username == username);
             UserInfo.Userid = foundUser.Id;
             UserInfo.PublisherName = foundUser.Username;
             return UserInfo;
        }
        public IActionResult Login(LoginDTO user)
        {
            IActionResult Result = Unauthorized();
            //    check to see if the user exist
            if (DoesUserExist(user.Username))
            {
                var foundUser = GetUserByUsername(user.Username);
                //    check to see if the password is correct

                if (VerifyUserPassword(user.Password, foundUser.Hash, foundUser.Salt))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DayClassSuperDuperSecreteKey@209"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    Result = Ok(new { Token = tokenString });
                }

            }
            return Result;
        }

        public PasswordDTO HashPassword(string? password)
        {
            PasswordDTO newHashedPassword = new PasswordDTO();
            byte[] SaltBytes = new byte[64];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(SaltBytes);
            var Salt = Convert.ToBase64String(SaltBytes);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltBytes, 10000);
            var Hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            newHashedPassword.Salt = Salt;
            newHashedPassword.Hash = Hash;
            return newHashedPassword;
        }


        public bool VerifyUserPassword(string? Password, string? storedHash, string? storedSalt)
        {
            var SaltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Password, SaltBytes, 10000);
            var newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            return newHash == storedHash;
        }

         public bool UpdateUser(UserModel userToUpdate)
        {
            //This one is sednig over the whole object to be updated
            _context.Update<UserModel>(userToUpdate);
            return _context.SaveChanges() !=0; 
        }
        public bool UpdateUsername(string Username)
        {
            //This one is sednig over just the username.
            //Then you have to get the object to then be updated.
            UserModel foundUser = GetUserByUsername(Username);
            bool result = false;
            if(foundUser != null)
            {
                //A user was foundUser
                foundUser.Username = Username;
                _context.Update<UserModel>(foundUser);
               result =  _context.SaveChanges() != 0;
            }
            return result;
        }

           public bool DeleteUser(string Username)
        {
            //This one is sednig over just the username.
            //Then you have to get the object to then be updated.
            UserModel foundUser = GetUserByUsername(Username);
            bool result = false;
            if(foundUser != null)
            {
                //A user was foundUser
                foundUser.Username = Username;
                _context.Remove<UserModel>(foundUser);
               result =  _context.SaveChanges() != 0;
            }
            return result;
        }
    }
}