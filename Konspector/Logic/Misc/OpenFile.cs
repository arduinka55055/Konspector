using System.Text;

namespace Konspector.Misc
{
    public class OpenFile
    {
        public static async Task<FileResult?> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName.EndsWith("md", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("html", StringComparison.OrdinalIgnoreCase))
                    {
                        using var stream = await result.OpenReadAsync();
                        var buffer = new byte[stream.Length];
                        await stream.ReadAsync(buffer.AsMemory(0, buffer.Length));
                        var content = Encoding.UTF8.GetString(buffer);
                        Console.WriteLine($"File content: {content}");
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception choosing file: {ex.Message}");
                //stacktrace
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex);

                // The user canceled or something went wrong
            }

            return null;
        }
        public static async Task<FileResult?> PickDefault(){
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // UTType values
                    { DevicePlatform.Android, new[] { "text/html" } }, // MIME type
                    { DevicePlatform.WinUI, new[] { ".html", ".md" } }, // file extension
                    { DevicePlatform.Tizen, new[] { "*/*" } },
                    { DevicePlatform.macOS, new[] { "html", "md" } }, // UTType values
                    { DevicePlatform.MacCatalyst, new[] { "html", "md" } }, // UTType values
                });

                PickOptions options = new()
                {
                    PickerTitle = "Please select a comic file",
                    FileTypes = customFileType,
                };
                return await PickAndShow(options);
        }
    }
}