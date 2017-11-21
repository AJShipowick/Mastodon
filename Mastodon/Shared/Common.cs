using OsOEasy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OsOEasy.Shared
{

    public interface ICommon
    {
        Task<ApplicationUser> GetCurrentUserAsync(HttpContext context);
    }

    public class Common : ICommon
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public Common(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<ApplicationUser> GetCurrentUserAsync(HttpContext context)
        {
            return _userManager.GetUserAsync(context.User);
        }

    }
}
