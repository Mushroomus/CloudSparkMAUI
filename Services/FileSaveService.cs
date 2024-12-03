
using Microsoft.Maui.Storage;

namespace CloudSparkMAUI.Services
{
    public class FileSaveService
    {
        public async Task<string?> SaveFileAsync(string fileName, string fileExtension)
        {
            //// Windows-specific file save logic
            //string? filePath = null;

            //// Create the save file picker logic only for Windows
            //if (OperatingSystem.IsWindows())
            //{
            //    var filePickerResult = await FilePicker.PickAsync(new PickOptions
            //    {
            //        FileTypes = FilePickerFileType.Images
            //    });

            //    if (filePickerResult != null)
            //    {
            //        filePath = filePickerResult.FullPath;
            //    }
            //}

            //return filePath;
            return string.Empty;
        }
    }
}
