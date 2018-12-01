using System;

namespace Bakalari
{
    public class Homework
    {
        public Homework(string subject, string subjectShortcut, DateTime assigned, DateTime toWhen, string description, HomeworkStatus status, HomeworkType type, string id)
        {
            Subject = subject;
            SubjectShortcut = subjectShortcut;
            Assigned = assigned;
            ToWhen = toWhen;
            Description = description;
            Status = status;
            Type = type;
            Id = id;
        }

        public string Subject { get; private set; }
        public string SubjectShortcut { get; private set; }
        public DateTime Assigned { get; private set; }
        public DateTime ToWhen { get; private set; }
        public string Description { get; private set; }
        public HomeworkStatus Status { get; private set; }
        public HomeworkType Type { get; private set; }
        public string Id { get; private set; }

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