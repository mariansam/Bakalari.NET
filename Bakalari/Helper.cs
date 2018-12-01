using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace Bakalari
{
    static class Helper
    {
        public static async Task<string> GetDataAsync(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}