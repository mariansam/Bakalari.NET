using System;

namespace Bakalari
{
    public class UserInfo
    {
        public UserInfo(string version, string name, UserType type, string school, string schoolType, string studentclass, string grade, string[] modules)
        {
            Version = version;
            Name = name;
            Type = type;
            School = school;
            SchoolType = schoolType;
            Class = studentclass;
            Grade = grade;
            Modules = modules;
        }

        public string Version { get; private set; }
        public string Name { get; private set; }
        public UserType Type { get; private set; }
        public string LongType => Type.ToLongType();
        public string School { get; private set; }
        public string SchoolType { get; private set; }
        public string Class { get; private set; }
        public string Grade { get; private set; }
        public string[] Modules { get; private set; }

        public override string ToString() => Name;

        internal static UserType ParseUserType(string s)
        {
            if (s == "Z")
                return UserType.Student;
            else if (s == "R")
                return UserType.Parent;
            else
                throw new NotImplementedException("This Usertype is not implemented yet.");
        }
    }

    public enum UserType
    {
        Student,
        Parent
    }
}