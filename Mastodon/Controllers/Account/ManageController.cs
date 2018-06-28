using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OsOEasy.Data;
using OsOEasy.Data.Models;
using OsOEasy.Models.ManageViewModels;
using OsOEasy.Services;
using Stripe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OsOEasy.Controllers.Account
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMailGunEmailSender _emailSender;
        private readonly IStripeService _stripeService;
        private readonly ILogger _logger;

        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          ApplicationDbContext dbContext,
          IMailGunEmailSender emailSender,
          IStripeService stripeService,
          ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _emailSender = emailSender;
            _stripeService = stripeService;
            _logger = loggerFactory.CreateLogger<ManageController>();
        }

        //
        // GET: /Manage/Index
        [HttpGet]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.AccountUpdateSuccess ? "Account updated!"
                : message == ManageMessageId.RequiredFieldMissing ? "Verify all fields are filled in to update values."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }

            var model = new IndexViewModel
            {
                HasPassword = await _userManager.HasPasswordAsync(user),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Website = user.Website,
                AccountCreationDate = user.AccountCreationDate,
                PhoneNumber = user.PhoneNumber,
                AccountMessage = GetAccountMessage(user),
                PaymentViewModel = new PaymentViewModel { SubscriptionPlan = user.SubscriptionPlan }
            };
            return View(model);
        }

        private string GetAccountMessage(ApplicationUser user)
        {
            String message = string.Empty;

            if (user.AccountSuspended)
            {
                return "Account suspended, please make a payment to activate your account!";
            }
            else if (user.MonthlyPromotionLimitReached)
            {
                return "Monthly promotion limit reached, you currently have " + user.PromoClaimsForCurrentMonth +
                    " promotion claims this month.  Please upgrade your account to continue to send promotions out!";
            }

            return message;
        }

        //
        // GET: /Manage/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeAccountPlan(IndexViewModel model, IFormCollection collection)
        {
            string newSubscriptonSelection = model.PaymentViewModel.SubscriptionPlan;

            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                using (_dbContext)
                {
                    var dbUser = _dbContext.Users.Find(user.Id);

                    //Process subscription payment
                    string stripeToken = collection["stripeToken"];
                    StripeSubscription stripeSubscription = _stripeService.SubscribeToPlan(dbUser, stripeToken, newSubscriptonSelection);

                    HandleChangeOfPlan(user);

                    dbUser.SubscriptionPlan = newSubscriptonSelection;
                    dbUser.StripeCustomerId = stripeSubscription.CustomerId;

                    _dbContext.SaveChanges();
                }

                return RedirectToAction(nameof(Index), new { Message = ManageMessageId.AccountUpdateSuccess });
            }

            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        private void HandleChangeOfPlan(ApplicationUser user)
        {
            //StripeService stripe = new StripeService();
            //string results = stripe.SendTestCharge();



        }

        //
        // GET: /Manage/SetPassword
        [HttpGet]
        public IActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserAccount(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index), new { Message = ManageMessageId.RequiredFieldMissing });
            }

            var user = await GetCurrentUserAsync();
            if (user != null)
            {

                using (_dbContext)
                {
                    var dbUser = _dbContext.Users.Find(user.Id);
                    dbUser.FirstName = model.FirstName;
                    dbUser.LastName = model.LastName;
                    dbUser.Website = model.Website;
                    dbUser.PhoneNumber = model.PhoneNumber;

                    _dbContext.SaveChanges();
                }

                return RedirectToAction(nameof(Index), new { Message = ManageMessageId.AccountUpdateSuccess });
            }

            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public enum ManageMessageId
        {
            AccountUpdateSuccess,
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RequiredFieldMissing,
            Error
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        #endregion
    }
}
