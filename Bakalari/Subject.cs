namespace Bakalari
{
    public class Subject
    {
        public Subject(string name, string shortcut, string code, string teacher, string teacherShortcut)
        {
            Name = name;
            Shortcut = shortcut;
            Code = code;
            Teacher = teacher;
            TeacherShortcut = teacherShortcut;
        }

        public string Name { get; private set; }
        public string Shortcut { get; private set; }
        public string Code { get; private set; }
        public string Teacher { get; private set; }
        public string TeacherShortcut { get; private set; }
    }
}