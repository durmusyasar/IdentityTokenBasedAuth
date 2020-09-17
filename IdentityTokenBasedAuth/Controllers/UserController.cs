using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IdentityTokenBasedAuth.Domain.Services;
using IdentityTokenBasedAuth.Models;
using IdentityTokenBasedAuth.ResourceViewModel;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityTokenBasedAuth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase, IActionFilter
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> getUser()
        {
            AppUser user = await userService.GetUserByUserName(User.Identity.Name);

            return Ok(user.Adapt<UserViewModelResource>());
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.ModelState.Remove("Password");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserViewModelResource userViewModel)
        {
            var response = await userService.UpdateUser(userViewModel, User.Identity.Name);

            if (response.Success)
            {
                return Ok(response.Extra);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserPicture(IFormFile picture)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory() + "wwwroot/UserPictures", fileName);

            using (var steam = new FileStream(path, FileMode.Create))
            {
                await picture.CopyToAsync(steam);
            }
            var result = new
            {
                path = "https://" + Request.Host + "/UserPictures" + fileName
            };

            var response = await userService.UploadUserPicture(result.path, User.Identity.Name);

            if (response.Success)
            {
                return Ok(path);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

    }
}
