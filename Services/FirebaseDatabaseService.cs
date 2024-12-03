using CloudSparkMAUI.Models;
using Firebase.Database;
using Firebase.Database.Query;
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
        private readonly FirebaseClient _firebaseClient;

        public FirebaseDatabaseService()
        {
            _firebaseClient = new FirebaseClient(FirebaseUrl);
        }

        public async Task<List<Models.Image>> GetItemsAsync()
        {
            var data = await _firebaseClient
                .Child("items")
                .OnceAsync<Models.Image>();

            return data.Select(entry => entry.Object).ToList();
        }

        public async Task AddItemAsync(string itemName)
        {
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
