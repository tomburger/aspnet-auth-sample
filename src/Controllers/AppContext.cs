using System;
using System.Web;
using System.Web.Mvc;
using AuthSample.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace AuthSample.Controllers
{
	public class ApplicationContext : IDisposable
	{
		public ApplicationContext(Controller controller)
		{

            Users = new UserManager<LoginUser>(new LoginStore());
            Users.UserValidator = new UserValidator<LoginUser>(this.Users)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            //var provider = new AesDataProtectionProvider(Glaass.Domain.Constants.UserManagerDataProtectionAppName);
            //var protector = provider.Create(Glaass.Domain.Constants.UserManagerDataProtectionPurposeFirstPassword, Glaass.Domain.Constants.UserManagerDataProtectionPurposeForgottenPassword);
            //Users.UserTokenProvider = new DataProtectorTokenProvider<LoginUser>(protector)
            //{
            //    TokenLifespan = Glaass.Domain.Constants.UserManagerTokenLifespan
            //};

			UserId = controller.Request.IsAuthenticated ? controller.User.Identity.GetUserId() : string.Empty; ;
            AuthManager = controller.HttpContext.GetOwinContext().Authentication;
		}

		public UserManager<LoginUser> Users { get; private set; }

		public string UserId { get; private set; }
		public IAuthenticationManager AuthManager { get; private set; }
		public bool IsAuthenticated => !string.IsNullOrEmpty(UserId); 

		public void Dispose()
		{
			Users.Dispose();
		}
	}
}