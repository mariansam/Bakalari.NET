using System;

namespace Bakalari
{
    public class Event
    {
        public string Name { get; internal set; }
        public DateTime Date { get; internal set; }
        public string Time { get; internal set; }
        public string Description { get; internal set; }
        public bool Visible { get; internal set; }
        public string[] Teachers { get; internal set; }
        public string[] Classes { get; internal set; }
        public string[] Rooms { get; internal set; }

        public override string ToString() => Name;
    }
}