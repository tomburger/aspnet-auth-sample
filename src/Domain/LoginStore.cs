using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AuthSample.Domain
{
    public class LoginStore :
                            IUserPasswordStore<LoginUser>,
                            IUserSecurityStampStore<LoginUser>,
                            IUserStore<LoginUser>,
                            IUserEmailStore<LoginUser>,
                            IDisposable
    {
        private ICollection<LoginUser> users = new List<LoginUser>();

        public LoginStore() 
        {
            var hasher = new PasswordHasher();
            var passwordHash = hasher.HashPassword("LetItBe");
            foreach (var user in new[] { "ringo", "george", "paul", "john" })
            {
                users.Add(new LoginUser 
                { 
                    Id = user,
                    UserName = user + "@beat.les",
                    Email = user + "@beat.les",
                    Confirmed = true,
                    PasswordHash = passwordHash,
                    SecurityStamp = "",
                });
            }
        }

        public Task<LoginUser> FindByIdAsync(string userId)
        {
            return Task.FromResult(this.users.FirstOrDefault(u => u.Id == userId));
        }

        public Task<LoginUser> FindByNameAsync(string userName)
        {
            return Task.FromResult(this.users.FirstOrDefault(u => u.UserName == userName));
        }

        public Task<LoginUser> FindByEmailAsync(string email)
        {
            return Task.FromResult(this.users.FirstOrDefault(u => u.Email == email));
        }

        public Task CreateAsync(LoginUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(LoginUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(LoginUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(LoginUser user, string passwordHash)
        {
            return Task.Run(() => user.PasswordHash = passwordHash);
        }

        public Task<string> GetPasswordHashAsync(LoginUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(LoginUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetSecurityStampAsync(LoginUser user, string stamp)
        {
            return Task.Run(() => user.SecurityStamp = stamp);
        }

        public Task<string> GetSecurityStampAsync(LoginUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetEmailAsync(LoginUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(LoginUser user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(LoginUser user)
        {
            return Task.FromResult(user.Confirmed);
        }

        public Task SetEmailConfirmedAsync(LoginUser user, bool confirmed)
        {
            user.Confirmed = confirmed;
            return UpdateAsync(user);
        }

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
    }
}