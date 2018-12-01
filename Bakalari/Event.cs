using System;

namespace Bakalari
{
    public class Event
    {
        public Event(string name, DateTime date, string time, string description, bool visible, string[] teachers, string[] classes, string[] rooms)
        {
            Name = name;
            Date = date;
            Time = time;
            Description = description;
            Visible = visible;
            Teachers = teachers;
            Classes = classes;
            Rooms = rooms;
        }

        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public string Time { get; private set; }
        public string Description { get; private set; }
        public bool Visible { get; private set; }
        public string[] Teachers { get; private set; }
        public string[] Classes { get; private set; }
        public string[] Rooms { get; private set; }

        public override string ToString() => Name;
    }
}