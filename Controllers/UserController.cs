using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_blog.Models;
using backend_blog.Models.DTO;
using backend_blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_blog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _data;
        public UserController(UserService dataFromService)
        {
            _data = dataFromService;
        }


        [HttpGet("userbyusername/{username}")]
        public UserIdDTO GetUserByUsername(string username)
        {
            return _data.GetUserIdDTOByUsername(username);
        }



        // add a user
        [HttpPost("AddUsers")]
            // if the user already exist
            // if they do not exist we can then have the account be created

        public bool AddUser(CreateAccountDTO UserToADD)
        {
            return _data.AddUser(UserToADD);
        }


        //Get Users
        // [HttpGet("GetAllUsers")]
        // public IEnumerable<UserModel> GetAllUsers()
        // {
        //     return _data.GetAllUsers();
        // }
        // login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO User)
        {
            return _data.Login(User);
        }
        // update user account

        [HttpPost("UpdateUser/{username}")]

        public bool UpdateUser(string username)
        {
            return _data.UpdateUsername(username);
        }


        //Delete User Account
        [HttpPost("DeleteUser/{userToDelete}")]

        public bool DeleteUser(string? userToDelete)
        {
            return _data.DeleteUser(userToDelete);
        }
    }
}