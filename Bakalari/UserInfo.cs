using System;

namespace Bakalari
{
    public class UserInfo
    {
        public string Version { get; internal set; }
        public string Name { get; internal set; }
        public UserType Type { get; internal set; }
        public string LongType => Type.ToLongType();
        public string School { get; internal set; }
        public string SchoolType { get; internal set; }
        public string Class { get; internal set; }
        public string Grade { get; internal set; }
        public string[] Modules { get; internal set; }

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