using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace AuthSample
{
	public class Application : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
		}

		protected void Application_End()
		{
		}
	}
}
