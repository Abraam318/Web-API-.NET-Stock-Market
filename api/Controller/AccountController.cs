using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Controller
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {
            try{
                if(!ModelState.IsValid){
                    return BadRequest();
                }
                var appUser = new AppUser{
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var CreatedUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if(CreatedUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser,"User");
                    if(roleResult.Succeeded)
                    {
                        return Ok("User Created");
                    }
                    else{
                        return StatusCode(500,roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500,CreatedUser.Errors);

                }

            }
            catch (Exception e)
            {
                return StatusCode(500,e);

            }
        }
    }
}