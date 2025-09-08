namespace SecureLoginApp.Controllers
{
    public class AccountController(UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager) : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "User does not exist.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                    return RedirectToAction("Dashboard", "Admin");

                return RedirectToAction("UserProfile", "User");
            }

            if (result.IsLockedOut)
                ModelState.AddModelError("", "This account is locked out.");
            else if (result.IsNotAllowed)
                ModelState.AddModelError("", "This account is not allowed to sign in.");
            else
                ModelState.AddModelError("", "Invalid username or password.");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
