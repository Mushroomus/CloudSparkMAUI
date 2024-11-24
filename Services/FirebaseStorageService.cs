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
        private const string FirebaseStorageUrl = "fir-project-51203.firebasestorage.app";

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
    }
}
