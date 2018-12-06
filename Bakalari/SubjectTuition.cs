using System;

namespace Bakalari
{
    public class SubjectTuition
    {
        public int Index { get; internal set; }
        public DateTime Date { get; internal set; }
        public int Lesson { get; internal set; }
        public int LessonNumber { get; internal set; }
        public string Topic { get; internal set; }
        public string Note { get; internal set; }
    }
}