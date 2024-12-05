using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;
using Google.Apis.Auth.OAuth2;

namespace CloudSparkMAUI.Services
{
    public class FirebaseStorageService
    {
        private const string FirebaseStorageUrl = "cloudspark-42ed1.firebasestorage.app";
        private FirebaseStorage? _firebaseStorage = null;

        private readonly FirebaseAuthService _firebaseAuthService;
        private string _cachedToken = string.Empty;

        public FirebaseStorageService()
        {
            _firebaseAuthService = new FirebaseAuthService();
        }

        private async Task EnsureFirebaseStorageInitializedOrTokenExpiredAsync()
        {
            if (_firebaseStorage == null)
            {
                _cachedToken = await GetFirebaseTokenAsync();

                _firebaseStorage = new FirebaseStorage(FirebaseStorageUrl, new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(_cachedToken)
                });
            }
            else
            {
                string token = await GetFirebaseTokenAsync();

                if (!token.Equals(_cachedToken))
                {
                    _firebaseStorage = new FirebaseStorage(FirebaseStorageUrl, new FirebaseStorageOptions
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

        public async Task<string> GetImageUrlAsync(string imageName)
        {
            await EnsureFirebaseStorageInitializedOrTokenExpiredAsync();

            try
            {
                var imageUrl = await _firebaseStorage
                    .Child("images")
                    .Child(imageName)
                    .GetDownloadUrlAsync();

                return imageUrl;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public async Task UploadImageAsync(Stream imageStream, string imageName)
        {
            try
            {
                await EnsureFirebaseStorageInitializedOrTokenExpiredAsync();

                var storageReference = _firebaseStorage
                    .Child("images")
                    .Child(imageName);

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                var uploadTask = storageReference.PutAsync(imageStream, cancellationToken, "image/png");

                await uploadTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading image: {ex.Message}");
            }
        }
    }
}
