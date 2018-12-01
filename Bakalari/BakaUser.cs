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
            Token = TokenGenerator.CreateToken(domain, user, password).Result;
            Domain = domain;
            User = user;
        }

        public string Token { get; private set; }
        public string Domain { get; private set; }
        public string User { get; private set; }

        public async Task<Event[]> GetEvents()
        {
            string xmlstring = await Helper.GetDataAsync($"https://{Domain}/login.aspx?hx={Token}&pm=akce");
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

        public async Task<Homework[]> GetHomework()
        {
            string xmlstring = await Helper.GetDataAsync($"https://{Domain}/login.aspx?hx={Token}&pm=ukoly");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlstring);

            Homework[] homework = new Homework[xml.ChildNodes[1].ChildNodes[0].ChildNodes.Count];

            for (int i = 0; i < homework.Length; i++)
            {
                XmlNode node = xml.ChildNodes[1].ChildNodes[0].ChildNodes[i];

                homework[i] = new Homework
                (
                    node["predmet"].InnerText,
                    node["zkratka"].InnerText,
                    DateTime.ParseExact(node["zadano"].InnerText.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture),
                    DateTime.ParseExact(node["nakdy"].InnerText.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture),
                    node["popis"].InnerText,
                    Homework.ParseHomeworkStatus(node["status"].InnerText),
                    Homework.ParseHomeworkType(node["typ"].InnerText),
                    node["id"].InnerText
                );
            }

            return homework;
        }

        public async Task<UserInfo> GetUserInfo()
        {
            string xmlstring = await Helper.GetDataAsync($"https://{Domain}/login.aspx?hx={Token}&pm=login");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlstring);

            XmlNode node = xml.ChildNodes[1];

            return new UserInfo
            (
                node["verze"].InnerText,
                node["jmeno"].InnerText,
                UserInfo.ParseUserType(node["typ"].InnerText),
                node["skola"].InnerText,
                node["typskoly"].InnerText,
                node["trida"].InnerText,
                node["rocnik"].InnerText,
                node["moduly"].InnerText.Split("*".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            );
        }
    }
}