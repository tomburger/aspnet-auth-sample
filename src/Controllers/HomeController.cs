using AuthSample.Models;
using System.Web.Mvc;

namespace AuthSample.Controllers
{
    public class HomeController : BaseController
	{
		[Authorize]
		public ActionResult Index()
		{
			if (App.IsAuthenticated)
			{
				ViewBag.CurrentUser = App.UserId;
				return View();
			}
			else
			{
				return RedirectToAction("Login", "Account");
			}
		}

		[Authorize]
		public ActionResult Second()
		{
			if (App.IsAuthenticated)
			{
				ViewBag.CurrentUser = App.UserId;
				return View();
			}
			else
			{
				return RedirectToAction("Login", "Account");
			}
		}
	}
}