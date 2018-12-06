using System;

namespace Bakalari
{
    public class Homework
    {
        public string Subject { get; internal set; }
        public string SubjectShortcut { get; internal set; }
        public DateTime Assigned { get; internal set; }
        public DateTime ToWhen { get; internal set; }
        public string Description { get; internal set; }
        public HomeworkStatus Status { get; internal set; }
        public HomeworkType Type { get; internal set; }
        public string Id { get; internal set; }

        public override string ToString() => Description;

        internal static HomeworkStatus ParseHomeworkStatus(string s)
        {
            if (s == "aktivni")
                return HomeworkStatus.Active;
            else if (s == "pozde")
                return HomeworkStatus.Late;
            else
                throw new NotImplementedException("This HomeworkStatus is not yet implemented");
        }

        internal static HomeworkType ParseHomeworkType(string s)
        {
            if (s == "20")
                return HomeworkType.Twenty;
            else
                throw new NotImplementedException("This Homeworktype is not yet implemented");
        }
    }

    public enum HomeworkStatus
    {
        Active,
        Late
    }



    public enum HomeworkType
    {
        Twenty
    }
}