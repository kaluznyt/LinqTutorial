// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Student.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Student type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LinqTutorial
{
    using System.Collections.Generic;

    public class Student
    {
        public int StudentID { get; set; }
        public int StandardID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }

        internal class StudentComparer : IEqualityComparer<Student>
        {
            public bool Equals(Student x, Student y)
            {
                if (x.StudentID == y.StudentID
                    && x.StudentName.ToLower() == y.StudentName.ToLower())
                    return true;

                return false;
            }

            public int GetHashCode(Student obj)
            {
                return obj.StudentID.GetHashCode();
            }
        }
    }
}


