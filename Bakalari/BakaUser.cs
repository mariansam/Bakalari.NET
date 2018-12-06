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

        public override string ToString() => User;

        public async Task<Event[]> GetEvents()
        {
            string xmlstring = await Helper.GetDataAsync($"https://{Domain}/login.aspx?hx={Token}&pm=akce");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlstring);

            Event[] events = new Event[xml.ChildNodes[1].ChildNodes[0].ChildNodes.Count];

            for (int i = 0; i < events.Length; i++)
            {
                XmlNode node = xml.ChildNodes[1].ChildNodes[0].ChildNodes[i];
                events[i] = new Event()
                {
                    Name = node["nazev"].InnerText,
                    Date = DateTime.ParseExact(node["datum"].InnerText, "yyyyMMdd", CultureInfo.InvariantCulture),
                    Time = node["cas"].InnerText,
                    Description = node["popis"].InnerText,
                    Visible = node["zobrazit"].InnerText == "1",
                    Teachers = node["proucitele"].InnerText.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries),
                    Classes = node["protridy"].InnerText.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries),
                    Rooms = node["promistnosti"].InnerText.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                };
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

                homework[i] = new Homework()
                {
                    Subject = node["predmet"].InnerText,
                    SubjectShortcut = node["zkratka"].InnerText,
                    Assigned = DateTime.ParseExact(node["zadano"].InnerText.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture),
                    ToWhen = DateTime.ParseExact(node["nakdy"].InnerText.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture),
                    Description = node["popis"].InnerText,
                    Status = Homework.ParseHomeworkStatus(node["status"].InnerText),
                    Type = Homework.ParseHomeworkType(node["typ"].InnerText),
                    Id = node["id"].InnerText
                };
            }

            return homework;
        }

        public async Task<UserInfo> GetUserInfo()
        {
            string xmlstring = await Helper.GetDataAsync($"https://{Domain}/login.aspx?hx={Token}&pm=login");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlstring);

            XmlNode node = xml.ChildNodes[1];

            return new UserInfo()
            {
                Version = node["verze"].InnerText,
                Name = node["jmeno"].InnerText,
                Type = UserInfo.ParseUserType(node["typ"].InnerText),
                School = node["skola"].InnerText,
                SchoolType = node["typskoly"].InnerText,
                Class = node["trida"].InnerText,
                Grade = node["rocnik"].InnerText,
                Modules = node["moduly"].InnerText.Split("*".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            };
        }

        public async Task<Subject[]> GetSubjects()
        {
            string xmlstring = await Helper.GetDataAsync($"https://{Domain}/login.aspx?hx={Token}&pm=predmety");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlstring);

            Subject[] subjects = new Subject[xml.ChildNodes[1].ChildNodes[0].ChildNodes.Count];

            for (int i = 0; i < subjects.Length; i++)
            {
                XmlNode node = xml.ChildNodes[1].ChildNodes[0].ChildNodes[i];

                subjects[i] = new Subject()
                {
                    Name = node["nazev"].InnerText,
                    Shortcut = node["zkratka"].InnerText,
                    Code = node["kod_pred"].InnerText,
                    Teacher = node["ucitel"].InnerText,
                    TeacherShortcut = node["zkratkauc"].InnerText
                };
            }

            return subjects;
        }

        public async Task<SubjectTuition[]> GetTuition(string subjectCode)
        {
            subjectCode = subjectCode.Replace(' ', '+');

            
        }
    }
}