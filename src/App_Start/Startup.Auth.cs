using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.Threading.Tasks;
using System.Web;

namespace AuthSample
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/login")
            });

            app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ApplicationCookie);
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = OpenIdConnectAuthenticationDefaults.AuthenticationType,
                ClientId = "Value-does-not-matter",
                Authority = "https://login.microsoftonline.com/common/v2.0",
                Scope = OpenIdConnectScope.OpenIdProfile,
                ResponseType = OpenIdConnectResponseType.CodeIdToken,
                RedirectUri = "http://localhost:44301/.auth/login/aad/callback",
                PostLogoutRedirectUri = "https://www.burger.software",
                TokenValidationParameters = new TokenValidationParameters
                {
                    // instead of using the default validation (validating against a single issuer value, as we do in line of business apps), 
                    // we inject our own multitenant validation logic
                    ValidateIssuer = false,
                    // If the app needs access to the entire organization, then add the logic
                    // of validating the Issuer here.
                    // IssuerValidator
                },
                Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    SecurityTokenValidated = (context) =>
                    {
                        // If your authentication logic is based on users then add your logic here
                        return Task.FromResult(0);
                    },
                    RedirectToIdentityProvider = (context) =>
                    {
                        if (
                            context.OwinContext.Request.Path.ToString() == "/Account/SsoLogin"
                            ||
                            context.OwinContext.Request.Path.ToString() == "/Account/SsoLogout"
                        )
                        {
                            // this is a legitimate attempt and we let it proceed as usual
                        }
                        else
                        {
                            // however all other attempts we want to send to application login page
                            context.OwinContext.Response.Redirect("/login?sso-logged-me-off");
                            context.HandleResponse(); // Suppress the exception
                        }
                        return Task.FromResult(0);
                    },
                    AuthenticationFailed = (context) =>
                    {
                        // Pass in the context back to the app
                        var msg = context.Exception?.Message ?? "Unknown error";
                        context.OwinContext.Response.Redirect("/Account/SsoError?msg=" + HttpUtility.UrlEncode(msg));
                        context.HandleResponse(); // Suppress the exception
                        return Task.FromResult(0);
                    }
                }
            });
        }
    }
}