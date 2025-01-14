using Firebase.Auth;
using Firebase.Auth.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOdevHazirlikMaui.Models
{
    public class FirebaseServices
    {
        static string projectId = "finalodevi-86bd9";
        static string ApiKey = "AIzaSyDrVm-PzonE7NgqH88nH41zN60gQRtyu5w";
        static string AuthDomain = $"finalodevi-86bd9.firebaseapp.com";

        static FirebaseAuthConfig authConfig = new FirebaseAuthConfig()
        {
            ApiKey = ApiKey,
            AuthDomain = AuthDomain,
            Providers = new FirebaseAuthProvider[] {new EmailProvider()}
        };

        public static async Task<User> Login(string email, string password)
        {
            try
            {

            var client = new FirebaseAuthClient(authConfig);
            var res = await client.SignInWithEmailAndPasswordAsync(email, password);
            return res.User;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<User> Register(string name, string email, string password)
        {
            try
            {

                var client = new FirebaseAuthClient(authConfig);
                var res = await client.CreateUserWithEmailAndPasswordAsync(email, password, name);
                return res.User;

            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}
