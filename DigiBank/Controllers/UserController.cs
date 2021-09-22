using BusinessLogic.Interfaces;
using DtoMappings.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Security.Claims;

namespace DigiBank.Controllers
{

    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager, IUserLogic userLogic)
        {
            _userLogic = userLogic;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("api/[controller]/Register")]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            try
            {
                return Ok(_userLogic.RegisterUserAsync(registerDTO));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/Login")]
        public IActionResult Login(LoginDTO loginDTO)    
        {
            try
            {
                return Ok(_userLogic.LoginUser(loginDTO));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("api/[controller]/Address")] 
        [Authorize(Role ="Regular")]
        public IActionResult Address(UserAddressDTO userAddressDTO)
        {
            var loggedInUser = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            userAddressDTO.LoggedInUserId = loggedInUser;
            try
            {
                return Created("Address created successfully", _userLogic.UserAddressAsync(userAddressDTO));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            } 
            
        }
    }
}
