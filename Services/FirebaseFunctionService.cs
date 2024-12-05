using Firebase.Auth;
using Microsoft.Maui.ApplicationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudSparkMAUI.Services
{
    public class FirebaseFunctionService
    {
        private string _firebaseFunctionUrl = "https://resize-image-gwurneni4q-uc.a.run.app";
        private readonly HttpClient _httpClient;
        private readonly FirebaseAuthService _authService;

        public FirebaseFunctionService(FirebaseAuthService authService)
        {
            _authService = authService;
            _httpClient = new HttpClient();
        }

        public async Task<byte[]> GetImageFunctionAsync(string image, string height, string width)
        {
            try
            {
                var token = await _authService.GetTokenAsync();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonPayload = JsonSerializer.Serialize(new
                {
                    file_name = image,
                    width,
                    height
                });

                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(_firebaseFunctionUrl, content);

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
