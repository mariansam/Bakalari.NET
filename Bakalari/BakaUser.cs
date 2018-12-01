using System.Threading.Tasks;
using System.Xml;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace Bakalari
{
    public class BakaUser
    {
        public BakaUser(string domain, string user, string password)
        {
            Hash = TokenGenerator.CreateHash(domain, user, password).Result;
            Domain = domain;
            User = user;
        }

        public string Hash { get; private set; }
        public string Domain { get; private set; }
        public string User { get; private set; }

        public async Task<Event[]> GetEvents()
        {
            string xmlstring = await Helper.GetDataAsync($"https://{Domain}/login.aspx?hx={Hash}&pm=akce");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlstring);

            Event[] events = new Event[xml.ChildNodes[1].ChildNodes[0].ChildNodes.Count];

            for (int i = 0; i < events.Length; i++)
            {
                XmlNode node = xml.ChildNodes[1].ChildNodes[0].ChildNodes[i];
                events[i] = new Event
                (
                    node["nazev"].InnerText,
                    DateTime.ParseExact(node["datum"].InnerText, "yyyyMMdd", CultureInfo.InvariantCulture),
                    node["cas"].InnerText,
                    node["popis"].InnerText,
                    node["zobrazit"].InnerText == "1",
                    node["proucitele"].InnerText.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries),
                    node["protridy"].InnerText.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries),
                    node["promistnosti"].InnerText.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                );
            }

            return events;
        }
    }
}