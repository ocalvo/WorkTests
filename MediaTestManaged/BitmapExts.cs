using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Tests.Common;
using System.Runtime.InteropServices;

namespace Windows.Graphics.Imaging
{
    public static class ImagingExtensions
    {
        private static string GetTestName(string id)
        {
            string testName = "testName";
            testName = testName.Replace("Windows::UI::Xaml::Tests::", "");
            testName = testName.Replace("Windows.UI.Xaml.Tests.", "");
            testName = testName.Replace("::", "_");
            testName = testName.Replace(".", "_");
            if (!string.IsNullOrEmpty(id))
            {
                testName += "_" + id;
            }
            const int maxLength = 15;
            if (testName.Length > maxLength)
            {
                testName = testName.Substring(testName.Length - maxLength, maxLength);
            }
            return testName;
        }

        public static Task LogAsync(this SoftwareBitmap @this)
        {
            return @this.LogAsync(string.Empty);
        }

        public static async Task LogAsync(this SoftwareBitmap @this, string Id)
        {
            var folder = Windows.Storage.KnownFolders.PicturesLibrary;
            var name = GetTestName(Id) + ".png";
            var fileName = folder.Path + @"\" + name;
            var file = await folder.CreateFileAsync(name, Storage.CreationCollisionOption.ReplaceExisting);
            try
            {
                using (var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
                {
                    var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                    encoder.SetSoftwareBitmap(@this);
                    await encoder.FlushAsync();
                }
            }
            finally
            {
                //BUG: 9298906 Awaiting DeleteAsync causes an InvalidCastException from WindowHelper to IWindowHelper
                //await file.DeleteAsync(Windows.Storage.StorageDeleteOption.PermanentDelete);
            }
        }
    }
}

