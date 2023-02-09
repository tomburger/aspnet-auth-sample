using System.Web.Mvc;
using System.Web.Routing;

namespace AuthSample.Controllers
{
	public class BaseController : Controller
	{
		public ApplicationContext App { get; private set; }

		protected override void Dispose(bool disposing)
		{
			if (disposing && App != null)
				App.Dispose();
			base.Dispose(disposing);
		}

		protected override void Initialize(RequestContext requestContext)
		{
			base.Initialize(requestContext);
			App = new ApplicationContext(this);
		}
	}
}