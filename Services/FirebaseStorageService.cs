using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;

namespace CloudSparkMAUI.Services
{
    public class FirebaseStorageService
    {
        private const string FirebaseStorageUrl = "cloudspark-42ed1.firebasestorage.app";

        private readonly FirebaseStorage _firebaseStorage;

        public FirebaseStorageService()
        {
            _firebaseStorage = new FirebaseStorage(FirebaseStorageUrl);
        }

        public async Task<string> GetImageUrlAsync(string imageName)
        {
            var storage = new FirebaseStorage(FirebaseStorageUrl);

            try
            {
                var imageUrl = await storage
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
                // Create a reference to the "images" folder and the image name
                var storageReference = _firebaseStorage
                    .Child("images")  // Folder in Firebase Storage
                    .Child(imageName); // Image name (e.g., "captured_image.jpg")

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                var uploadTask = storageReference.PutAsync(imageStream, cancellationToken, "image/png");

                await uploadTask;
            }
            catch (Exception ex)
            {
                // Handle any errors during the upload
                Console.WriteLine($"Error uploading image: {ex.Message}");
            }
        }
    }
}
