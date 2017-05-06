// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Tk">
//   Copyright
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LinqTutorial
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public IList<Student> StudentList { get; } =
            new List<Student>()
                {
                    new Student() { StudentID = 1, StudentName = "John", StandardID = 1, Age = 19 },
                    new Student() { StudentID = 2, StudentName = "Moin", StandardID = 1, Age = 21 },
                    new Student() { StudentID = 3, StudentName = "Bill", StandardID = 2, Age = 32 },
                    new Student() { StudentID = 4, StudentName = "Ram", StandardID = 2, Age = 14 },
                    new Student() { StudentID = 5, StudentName = "Ron" }
                };

        public IList<Standard> StandardList { get; } =
            new List<Standard>()
                {
                    new Standard() { StandardID = 1, StandardName = "Standard 1" },
                    new Standard() { StandardID = 2, StandardName = "Standard 2" },
                    new Standard() { StandardID = 3, StandardName = "Standard 3" }
                };

        private static void Main()
        {
            new Program().Run();
        }

        private void Run()
        {
            this.Where();
            this.OfType();
            this.OrderByThenBy();
            this.GroupByToLookup();
            this.Join();
            this.GroupJoin();
            this.Select();
            this.AllAnyContain();
            this.Aggregate();
            this.Average();
            this.Count();
            this.Max();
            this.Sum();
            this.ElementAt();
            this.FirstOrDefault();
            this.LastOrDefault();
            this.SingleOrDefault();
            this.SequenceEqual();
            this.Concat();
            this.DefaultIfEmpty();
        }

        // The DefaultIfEmpty() method returns a new collection with the default value if the given 
        // collection on which DefaultIfEmpty() is invoked is empty.
        private void DefaultIfEmpty()
        {

        }

        // The Concat() method appends two sequences of the same type and returns a new sequence(collection).
        private void Concat()
        {
            var collection1 = new List<string>() { "One", "Two", "Three" };
            var collection2 = new List<string>() { "Five", "Six" };

            var collection3 = collection1.Concat(collection2);

            foreach (string str in collection3) Console.WriteLine(str);
        }

        // There is only one equality operator: SequenceEqual.The SequenceEqual method checks whether the number of 
        // elements, value of each element and order of elements in two collections are equal or not.
        // If the collection contains elements of primitive data types then it compares the values and number of 
        // elements, whereas collection with complex type elements, checks the references of the objects.So, 
        // if the objects have the same reference then they considered as equal otherwise they are considered not equal.
        private void SequenceEqual()
        {
            var strList1 = new List<string>() { "One", "Two", "Three", "Four", "Three" };
            var strList2 = new List<string>() { "One", "Two", "Three", "Four", "Three" };
            var isEqual = strList1.SequenceEqual(strList2); // returns true

            Console.WriteLine(isEqual);

            var strList3 = new List<string>() { "Two", "Three", "Four", "Three" };


            isEqual = strList1.SequenceEqual(strList3); // returns false
            Console.WriteLine(isEqual);

            var std = new Student() { StudentID = 1, StudentName = "Bill" };

            var studentList1 = new List<Student>() { std };

            var studentList2 = new List<Student>() { std };

            isEqual = studentList1.SequenceEqual(studentList2); // returns true

            var std1 = new Student() { StudentID = 1, StudentName = "Bill" };
            var std2 = new Student() { StudentID = 1, StudentName = "Bill" };

            var studentList3 = new List<Student>() { std1 };

            var studentList4 = new List<Student>() { std2 };

            isEqual = studentList3.SequenceEqual(studentList4);// returns false
        }


        // Single Returns the only element from a collection, or the only element that satisfies a condition.
        // If Single() found no elements or more than one elements in the collection then throws InvalidOperationException.
        // SingleOrDefault The same as Single, except that it returns a default value of a specified generic type, 
        // instead of throwing an exception if no element found for the specified condition.However, it will thrown
        // InvalidOperationException if it found more than one element for the specified condition in the collection.
        private void SingleOrDefault()
        {
            var oneElementList = new List<int>() { 7 };
            var intList = new List<int>() { 7, 10, 21, 30, 45, 50, 87 };
            IList<string> strList = new List<string>() { null, "Two", "Three", "Four", "Five" };
            IList<string> emptyList = new List<string>();

            Console.WriteLine("The only element in oneElementList: {0}", oneElementList.Single());
            Console.WriteLine("The only element in oneElementList: {0}", oneElementList.SingleOrDefault());

            Console.WriteLine("Element in emptyList: {0}", emptyList.SingleOrDefault());

            Console.WriteLine("The only element which is less than 10 in intList: {0}", intList.Single(i => i < 10));

            // Followings throw an exception
            try
            {
                Console.WriteLine("The only Element in intList: {0}", intList.Single());
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

            try
            {
                Console.WriteLine("The only Element in intList: {0}", intList.SingleOrDefault());
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

            try
            {
                Console.WriteLine("The only Element in emptyList: {0}", emptyList.Single());
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

            // following throws error because list contains more than one element which is less than 100
            try
            {
                Console.WriteLine("Element less than 100 in intList: {0}", intList.Single(i => i < 100));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

            // following throws error because list contains more than one element which is less than 100
            try
            {
                Console.WriteLine("Element less than 100 in intList: {0}", intList.SingleOrDefault(i => i < 100));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            // following throws error because list contains more than one elements
            try
            {
                Console.WriteLine("The only Element in intList: {0}", intList.Single());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            // following throws error because list contains more than one elements
            try
            {
                Console.WriteLine("The only Element in intList: {0}", intList.SingleOrDefault());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            // following throws error because list does not contains any element
            try
            {
                Console.WriteLine("The only Element in emptyList: {0}", emptyList.Single());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // Last and LastOrDefault has two overload methods. One overload method doesn't take any input parameter and
        // returns last element from the collection. Second overload method takes a lambda expression to specify a
        // condition and returns last element that satisfies the specified condition.
        // Last Returns the last element from a collection, or the last element that satisfies a condition
        // LastOrDefault   Returns the last element from a collection, or the last element that satisfies a condition.
        // Returns a default value if no such element exists.
        private void LastOrDefault()
        {
            var intList = new List<int>() { 7, 10, 21, 30, 45, 50, 87 };
            var strList = new List<string>() { null, "Two", "Three", "Four", "Five" };
            var emptyList = new List<string>();

            Console.WriteLine("Last Element in intList: {0}", intList.Last());

            Console.WriteLine("Last Even Element in intList: {0}", intList.Last(i => i % 2 == 0));

            Console.WriteLine("Last Element in strList: {0}", strList.Last());

            Console.WriteLine("emptyList.Last() throws an InvalidOperationException");
            Console.WriteLine("-------------------------------------------------------------");
            try
            {
                Console.WriteLine(emptyList.Last());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Last Element in intList: {0}", intList.LastOrDefault());

            Console.WriteLine("Last Even Element in intList: {0}",
                intList.LastOrDefault(i => i % 2 == 0));

            Console.WriteLine("Last Element in strList: {0}", strList.LastOrDefault());

            Console.WriteLine("Last Element in emptyList: {0}", emptyList.LastOrDefault());

            // Be careful while specifying condition in Last() or LastOrDefault(). Last() will throw an exception if a collection does not include any element that satisfies the specified condition or includes null element.
            // If a collection includes null element then LastOrDefault() throws an exception while evaluting the specified condition.The following example demonstrates this.
            try
            {
                Console.WriteLine("Last Element which is greater than 250 in intList: {0}", intList.Last(i => i > 250));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            try
            {
                Console.WriteLine("Last Even Element in intList: {0}", strList.LastOrDefault(s => s.Contains("T")));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // The First and FirstOrDefault method returns an element from the zeroth index in the collection
        // i.e.the first element.Also, it returns an element that satisfies the specified condition.
        // First Returns the first element of a collection, or the first element that satisfies a condition.
        // FirstOrDefault Returns the first element of a collection, or the first element that satisfies a condition.Returns a default value if index is out of range.
        private void FirstOrDefault()
        {
            var intList = new List<int>() { 7, 10, 21, 30, 45, 50, 87 };
            var strList = new List<string>() { null, "Two", "Three", "Four", "Five" };
            var emptyList = new List<string>();

            Console.WriteLine("1st Element in intList: {0}", intList.First());
            Console.WriteLine("1st Even Element in intList: {0}", intList.First(i => i % 2 == 0));

            Console.WriteLine("1st Element in strList: {0}", strList.First());

            Console.WriteLine("emptyList.First() throws an InvalidOperationException");
            Console.WriteLine("-------------------------------------------------------------");

            try
            {
                Console.WriteLine(emptyList.First());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("1st Element in intList: {0}", intList.FirstOrDefault());
            Console.WriteLine("1st Even Element in intList: {0}", intList.FirstOrDefault(i => i % 2 == 0));

            Console.WriteLine("1st Element in strList: {0}", strList.FirstOrDefault());

            Console.WriteLine("1st Element in emptyList: {0}", emptyList.FirstOrDefault());

            // Be careful while specifying condition in First() or FirstOrDefault(). 
            // First() will throw an exception if a collection does not include any element that satisfies the specified condition or includes null element.
            try
            {
                Console.WriteLine("1st Element which is greater than 250 in intList: {0}", intList.First(i => i > 250));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            try
            {
                Console.WriteLine("1st Even Element in intList: {0}", strList.FirstOrDefault(s => s.Contains("T")));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // ElementAt Returns the element at a specified index in a collection
        // ElementAtOrDefault Returns the element at a specified index in a collection or a default value if the index is out of range.
        private void ElementAt()
        {
            var intList = new List<int>() { 10, 21, 30, 45, 50, 87 };
            var strList = new List<string>() { "One", "Two", null, "Four", "Five" };

            Console.WriteLine("1st Element in intList: {0}", intList.ElementAt(0));
            Console.WriteLine("1st Element in strList: {0}", strList.ElementAt(0));
            Console.WriteLine("2nd Element in intList: {0}", intList.ElementAt(1));
            Console.WriteLine("2nd Element in strList: {0}", strList.ElementAt(1));
            Console.WriteLine("3rd Element in intList: {0}", intList.ElementAtOrDefault(2));
            Console.WriteLine("3rd Element in strList: {0}", strList.ElementAtOrDefault(2));
            Console.WriteLine("10th Element in intList: {0} - default int value", intList.ElementAtOrDefault(9));
            Console.WriteLine(
                "10th Element in strList: {0} - default string value (null)",
                strList.ElementAtOrDefault(9));
            Console.WriteLine("intList.ElementAt(9) throws an exception: Index out of range");
            Console.WriteLine("-------------------------------------------------------------");

            try
            {
                Console.WriteLine(intList.ElementAt(9));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        // The Sum() method calculates the sum of numeric items in the collection.
        private void Sum()
        {
            var intList = new List<int>() { 10, 21, 30, 45, 50, 87 };

            var total = intList.Sum();

            Console.WriteLine("Sum: {0}", total);

            int sumOfEvenElements = intList.Sum(i => i % 2 == 0 ? i : 0);

            Console.WriteLine("Sum of Even Elements: {0}", sumOfEvenElements);

            var sumOfAge = this.StudentList.Sum(s => s.Age);

            Console.WriteLine("Sum of all student's age: {0}", sumOfAge);

            var numOfAdults = this.StudentList.Sum(s => s.Age >= 18 ? 1 : 0);

            Console.WriteLine("Total Adult Students: {0}", numOfAdults);
        }

        // The Max operator returns the largest numeric element from a collection.
        private void Max()
        {
            var intList = new List<int>() { 10, 21, 30, 45, 50, 87 };

            var largest = intList.Max();

            Console.WriteLine("Largest Element: {0}", largest);

            var largestEvenElements = intList.Max(i => i % 2 == 0 ? i : 0);

            Console.WriteLine("Largest Even Element: {0}", largestEvenElements);

            var oldest = this.StudentList.Max(s => s.StudentName.Length);

            Console.WriteLine($"Oldest student is: {oldest}");
        }

        // The Count operator returns the number of elements in the collection 
        // or number of elements that have satisfied the given condition.
        private void Count()
        {
            var numOfStudents = this.StudentList.Count();

            Console.WriteLine("Number of Students: {0}", numOfStudents);

            var numOfMatureStudents = this.StudentList.Count(s => s.Age >= 18);

            Console.WriteLine("Number of mature Students: {0}", numOfMatureStudents);

            var totalAge = (from s in this.StudentList
                            where s.Age >= 18
                            select s.Age).Count();

            Console.WriteLine("Number of mature Students: {0}", totalAge);
        }



        // Average extension method calculates the average of the numeric items in the collection.
        // Average method returns nullable or non-nullable decimal, double or float value.
        private void Average()
        {
            var intList = new List<int>() { 10, 20, 30 };

            var avg = intList.Average();

            Console.WriteLine("Average: {0}", avg);

            var avgAge = this.StudentList.Average(s => s.Age);

            Console.WriteLine("Average Age of Student: {0}", avgAge);
        }

        // The aggregation operators perform mathematical operations like Average, Aggregate, 
        // Count, Max, Min and Sum, on the numeric property of the elements in the collection.
        private void Aggregate()
        {
            var strList = new List<string>() { "One", "Two", "Three", "Four", "Five" };

            var commaSeperatedString = strList.Aggregate((accumulator, currentItem) =>
                {
                    Console.WriteLine($"{nameof(accumulator)}:{accumulator}");
                    Console.WriteLine($"{nameof(currentItem)}:{currentItem}");
                    return accumulator + ", " + currentItem;
                });

            Console.WriteLine(commaSeperatedString);

            var commaSeperatedStringWithSeed = strList.Aggregate(
                "Seed: ",
                (accumulator, currentItem) =>
                    {
                        Console.WriteLine($"{nameof(accumulator)}:{accumulator}");
                        Console.WriteLine($"{nameof(currentItem)}:{currentItem}");
                        return accumulator + currentItem + ", ";
                    });

            Console.WriteLine(commaSeperatedStringWithSeed);

            var commaSeperatedStringWithSeedAndResultSelector = strList.Aggregate(
                string.Empty,
                (accumulator, currentItem) =>
                    {
                        Console.WriteLine($"{nameof(accumulator)}:{accumulator}");
                        Console.WriteLine($"{nameof(currentItem)}:{currentItem}");
                        return accumulator + currentItem + ", ";
                    },
                str => str.Substring(0, str.Length - 2));

            Console.WriteLine(commaSeperatedStringWithSeedAndResultSelector);

        }

        // All Checks if all the elements in a sequence satisfies the specified condition
        // Any Checks if any of the elements in a sequence satisfies the specified condition
        // Contain Checks if the sequence contains a specific element
        // Quantifier operators are Not Supported with C# query syntax.
        private void AllAnyContain()
        {
            // checks whether all the students are teenagers    
            var areAllStudentsTeenAger = this.StudentList.All(s => s.Age > 12 && s.Age < 20);

            Console.WriteLine(areAllStudentsTeenAger);

            var isAnyStudentTeenAger = this.StudentList.Any(s => s.Age > 12 && s.Age < 20);

            Console.WriteLine(isAnyStudentTeenAger);

            var intList = new List<int>() { 1, 2, 3, 4, 5 };
            var result = intList.Contains(10);  // returns false
            Console.WriteLine(result);

            var student = new Student { StudentID = 3, StudentName = "Bill" };
            Console.WriteLine(this.StudentList.Contains(student));
            Console.WriteLine(this.StudentList.Contains(student, new StudentComparer()));
        }

        internal class StudentComparer : IEqualityComparer<Student>
        {
            public bool Equals(Student x, Student y)
            {
                return x.StudentID == y.StudentID && string.Equals(x.StudentName, y.StudentName, StringComparison.CurrentCultureIgnoreCase);
            }

            public int GetHashCode(Student obj)
            {
                return obj.GetHashCode();
            }
        }


        // The Select operator always returns an IEnumerable collection which contains elements based on a 
        // transformation function.It is similar to the Select clause of SQL that produces a flat result set.
        // The SelectMany operator projects sequences of values that are based on a transform function and then flattens them into one sequence.
        private void Select()
        {
            var selectResultString = from s in this.StudentList
                                     select s.StudentName;

            var selectResultQuery = from s in this.StudentList
                                    select new { Name = "Mr. " + s.StudentName, Age = s.Age };

            // iterate selectResult
            foreach (var item in selectResultQuery)
            {
                Console.WriteLine("Student Name: {0}, Age: {1}", item.Name, item.Age);
            }

            var selectResultMethod = this.StudentList.Select(s => new { Name = "Mr. " + s.StudentName, Age = s.Age });

            foreach (var item in selectResultMethod)
            {
                Console.WriteLine("Student Name: {0}, Age: {1}", item.Name, item.Age);
            }

        }

        // The GroupJoin operator performs the same task as Join operator except that GroupJoin returns a 
        // result in group based on specified group key. The GroupJoin operator joins two sequences based on 
        // key and groups the result by matching key and then returns the collection of grouped result and key.
        private void GroupJoin()
        {

            var groupJoinMethod = this.StandardList.GroupJoin(
                this.StudentList,
                std => std.StandardID,
                s => s.StandardID,
                (std, studentsGroup) => new { Students = studentsGroup, StandardFullName = std.StandardName });

            var groupJoinQuery = from std in this.StandardList
                                 join stu in this.StudentList on std.StandardID equals stu.StandardID into
                                 studentsPerStandardCollection
                                 select new
                                 {
                                     StandardFullName = std.StandardName,
                                     Students = studentsPerStandardCollection
                                 };


            foreach (var item in groupJoinQuery)
            {
                Console.WriteLine(item.StandardFullName);

                foreach (var stud in item.Students)
                {
                    Console.WriteLine(stud.StudentName);
                }
            }
        }

        // The Join operator operates on two collections, inner collection & outer collection.
        // It returns a new collection that contains elements from both the collections which satisfies 
        // specified expression.It is the same as inner join of SQL.
        private void Join()
        {
            var innerJoinMethod = this.StudentList.Join( // outer sequence
                this.StandardList,
                student => student.StandardID,
                standard => standard.StandardID,
                (student, standard) => new
                {
                    StudentId = student.StudentID,
                    Student = student,
                    Standard = standard
                });

            var innerJoinQuery = from student in this.StudentList
                                 join standard in this.StandardList on student.StandardID equals standard.StandardID
                                 select new { StudentId = student.StudentID, Student = student, Standard = standard };

        }

        // The GroupBy operator returns a group of elements from the given collection based on some key value.
        // Each group is represented by IGrouping<TKey, TElement> object. Also, the GroupBy method has eight 
        // overload methods, so you can use appropriate extension method based on your requirement in method syntax.
        private void GroupByToLookup()
        {
            var groupedResultQuery = from s in this.StudentList
                                     group s by s.Age;

            var groupedResultMethod = this.StudentList.GroupBy(s => s.Age);

            foreach (var ageGroup in groupedResultMethod)
            {
                Console.WriteLine("Age Group: {0}", ageGroup.Key);  // Each group has a key 

                // Each group has a inner collection  
                foreach (var s in ageGroup)
                {
                    Console.WriteLine("Student Name: {0}", s.StudentName);
                }
            }

            var lookupResult = this.StudentList.ToLookup(s => s.Age);

            foreach (var group in lookupResult)
            {
                Console.WriteLine("Age Group: {0}", group.Key); // Each group has a key 

                // Each group has a inner collection  
                foreach (var s in group)
                {
                    Console.WriteLine("Student Name: {0}", s.StudentName);
                }
            }
        }

        // OrderBy sorts the values of a collection in ascending or descending order.
        // It sorts the collection in ascending order by default because ascending keyword is optional here.
        // Use descending keyword to sort collection in descending order.

        // The OrderBy() method sorts the collection in ascending order based on specified field.
        // Use ThenBy() method after OrderBy to sort the collection on another field in ascending order. 
        // Linq will first sort the collection based on primary field which is specified by OrderBy method and then sort the resulted collection in ascending order again based on secondary field specified by ThenBy method.
        private void OrderByThenBy()
        {
            var orderByResult = from s in this.StudentList
                                orderby s.StudentName
                                select s;

            var orderByDescendingResult = from s in this.StudentList
                                          orderby s.StudentName descending
                                          select s;

            var studentsInAscOrder = this.StudentList.OrderBy(s => s.StudentName);

            var studentsInDescOrder = this.StudentList.OrderByDescending(s => s.StudentName);

            var orderByResultMultipleSorting = from s in this.StudentList
                                               orderby s.StudentName, s.Age
                                               select new { s.StudentName, s.Age };

            var thenByResult = this.StudentList.OrderBy(s => s.StudentName).ThenBy(s => s.Age);

            var thenByDescResult = this.StudentList.OrderBy(s => s.StudentName).ThenByDescending(s => s.Age);


        }

        // The OfType operator filters the collection based on the ability to cast an element in 
        // a collection to a specified type.
        private void OfType()
        {
            IList mixedList = new ArrayList();
            mixedList.Add(0);
            mixedList.Add("One");
            mixedList.Add("Two");
            mixedList.Add(3);
            mixedList.Add(new Student() { StudentID = 1, StudentName = "Bill" });

            var stringResult = from s in mixedList.OfType<string>()
                               select s;

            var intResult = from s in mixedList.OfType<int>()
                            select s;
        }


        // The Where operator (Linq extension method) filters the collection based on a given criteria 
        // expression and returns a new collection.The criteria can be specified as lambda expression or Func delegate type.
        private void Where()
        {
            IList<string> stringList = new List<string>
                                           {
                                               "C# Tutorials",
                                               "VB.NET Tutorials",
                                               "Learn C++",
                                               "MVC Tutorials" ,
                                               "Java"
                                           };

            var resultStringList = from s in stringList where s.Contains("Tutorials") select s;

            var resultQuery = from s in this.StudentList
                              orderby s.StudentName descending
                              where s.Age > 12 && s.Age < 18
                              select s;

            var resultGroup = from s in this.StudentList group s by s.Age;

            foreach (var ageGroup in resultGroup)
            {
                Console.WriteLine("Age Group: {0}", ageGroup.Key); // Each group has a key 

                // Each group has inner collection
                foreach (var s in ageGroup)
                {
                    Console.WriteLine("Student Name: {0}", s.StudentName);
                }
            }

            var byIndexSearch = this.StudentList.Where((student, i) => i == 2);
        }
    }
}


