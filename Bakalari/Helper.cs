using System.Net;
using System.IO;
using System.Threading.Tasks;
using System;

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

        public static string ToLongType(this UserType type)
        {
            if (type == UserType.Student)
                return "žák";
            else if (type == UserType.Parent)
                return "rodič";
            else
                throw new ArgumentNullException("type", "The argument cannot be null");
        }
    }
}