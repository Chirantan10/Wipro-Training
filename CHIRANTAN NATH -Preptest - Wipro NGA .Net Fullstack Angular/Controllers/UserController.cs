
namespace SecureLoginApp.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        public IActionResult UserProfile()
        {
            ViewBag.Message = "Welcome, User! Here is your profile information.";
            return View();
        }
    }
}
