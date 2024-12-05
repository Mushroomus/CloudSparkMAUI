using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSparkMAUI.Services
{
    public class FirebaseAuthService
    {
        private readonly FirebaseAuthProvider _authProvider;
        private string _apiKey = "AIzaSyAOizr3xm2uV4XOgxjE9i8ljUjOfpfPdG8";

        private FirebaseAuthLink _auth = null;

        public FirebaseAuthService()
        {
            _authProvider = new FirebaseAuthProvider(new FirebaseConfig(_apiKey));
        }

        private async Task<string> LogInAnonymouslyAsync()
        {
            try
            {
                _auth = await _authProvider.SignInAnonymouslyAsync();
                return _auth.FirebaseToken;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> GetTokenAsync()
        {
            if (_auth == null || _auth.IsExpired())
                _auth = await _authProvider.SignInAnonymouslyAsync();

            return _auth.FirebaseToken;
        }
    }
}
