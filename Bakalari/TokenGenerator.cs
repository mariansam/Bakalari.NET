using System;
using System.Security.Cryptography;
using System.Xml;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bakalari
{
    static class TokenGenerator
    {
        public static async Task<string> CreateToken(string domain, string user, string password)
        {
            string res = await Helper.GetDataAsync($"https://{domain}/login.aspx?gethx={user}");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(res);

            var xmlcreds = xml.ChildNodes[1].ChildNodes;

            string type = xmlcreds[1].InnerText;
            string ikod = xmlcreds[2].InnerText;
            string salt = xmlcreds[3].InnerText;

            string hashpass = Base64(Sha512Sum(salt+ikod+type+password));

            string now = DateTime.Now.ToString("yyyyMMdd");

            string rawtoken = $"*login*{user}*pwd*{hashpass}*sgn*ANDR{now}";

            string token = Base64(Sha512Sum(rawtoken));
            token = token.Replace("\\", "_").Replace("/", "_").Replace("+", "-");

            return token;
        }

        static SHA512 sha = new SHA512Managed();

        static byte[] Sha512Sum(string data) => sha.ComputeHash(Encoding.ASCII.GetBytes(data));

        static string Base64(byte[] data) => Convert.ToBase64String(data);
    }
}
