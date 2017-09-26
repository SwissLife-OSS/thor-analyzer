using System.IO;
using System.Threading.Tasks;

namespace ChilliCream.Tracing.Analyzer.Tests
{
    public static class FileHelper
    {
        public static async Task<string> ReadAllTextAsync(string path)
        {
            using (StreamReader reader = File.OpenText(path))
            {
                return await reader.ReadToEndAsync().ConfigureAwait(false);
            }
        }
    }
}