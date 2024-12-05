using CloudSparkMAUI.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using Microsoft.Maui.ApplicationModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSparkMAUI.Services
{
    public class FirebaseDatabaseService
    {
        private const string FirebaseUrl = "https://cloudspark-42ed1-default-rtdb.europe-west1.firebasedatabase.app/";
        private FirebaseClient? _firebaseClient = null;

        private readonly FirebaseAuthService _firebaseAuthService;
        private string _cachedToken = string.Empty;

        public FirebaseDatabaseService()
        {
            _firebaseAuthService = new FirebaseAuthService();
        }
        private async Task EnsureFirebaseStorageInitializedOrTokenExpiredAsync()
        {
            if (_firebaseClient == null)
            {
                _cachedToken = await GetFirebaseTokenAsync();

                _firebaseClient = new FirebaseClient(FirebaseUrl, new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(_cachedToken)
                });
            }
            else
            {
                string token = await GetFirebaseTokenAsync();

                if (!token.Equals(_cachedToken))
                {
                    _firebaseClient = new FirebaseClient(FirebaseUrl, new FirebaseOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(token)
                    });
                }
            }
        }

        private async Task<string> GetFirebaseTokenAsync()
        {
            return await _firebaseAuthService.GetTokenAsync();
        }

        public async Task<List<Models.Image>> GetItemsAsync()
        {
            await EnsureFirebaseStorageInitializedOrTokenExpiredAsync();

            var data = await _firebaseClient
                .Child("items")
                .OnceAsync<Models.Image>();

            return data.Select(entry => entry.Object).ToList();
        }

        public async Task AddItemAsync(string itemName)
        {
            await EnsureFirebaseStorageInitializedOrTokenExpiredAsync();

            var newItem = new Models.Image
            {
                Name = itemName
            };

            await _firebaseClient
                .Child("items")
                .PostAsync(newItem);
        }
    }
}
