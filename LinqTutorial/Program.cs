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
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

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

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        private static void Print(string currentMethod)
        {
            var line = "######################################################";
            Console.WriteLine();
            Console.WriteLine(line);
            Console.WriteLine("{0," + (line.Length / 2 + currentMethod.Length / 2) + "}", currentMethod);
            Console.WriteLine(line);
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
            this.EmptyRangeRepeat();
            this.Distinct();
            this.Except();
            this.Intersect();
            this.Union();
            this.SkipSkipWhile();
            this.TakeTakeWhile();
            this.AsEnumerableCastToListToDictionaryToArray();
            this.LambdaExpression();
            this.ExpressionTree();
            this.let();
            this.into();
        }

        private void into()
        {
            Print(this.GetCurrentMethod());

            var teenAgerStudents = from s in this.StudentList
                                   where s.Age > 12 && s.Age < 60
                                   select s
                                       into teenStudents
                                   where teenStudents.StudentName.StartsWith("B")
                                   select teenStudents.StudentName;

            foreach (var teenAgerStudent in teenAgerStudents)
            {
                Console.WriteLine(teenAgerStudent);
            }
        }

        private void let()
        {
            Print(this.GetCurrentMethod());

            var lowercaseStudentNames = from s in this.StudentList
                                        where s.StudentName.ToLower().StartsWith("r")
                                        select s.StudentName.ToLower();

            var lowercaseStudentNamesWithLet = from s in this.StudentList
                                               let lowercaseStudentName = s.StudentName.ToLower()
                                               where lowercaseStudentName.StartsWith("r")
                                               select lowercaseStudentName;

            foreach (var name in lowercaseStudentNamesWithLet)
            {
                Console.WriteLine(name);
            }
        }

        private void ExpressionTree()
        {
            Print(this.GetCurrentMethod());

            // To create the expression tree, first of all, create a parameter expression where Student is the type of the parameter and 's' is the name of the parameter as below:
            ParameterExpression pe = Expression.Parameter(typeof(Student), "student");

            // Now, use Expression.Property() to create s.Age expression where s is the parameter and Age is the property name of Student. (Expression is an abstract class that contains static helper methods to create the Expression tree manually.)
            MemberExpression me = Expression.Property(pe, "Age");

            // Now, create a constant expression for 18:
            ConstantExpression constant = Expression.Constant(18, typeof(int));

            // Till now, we have built expression trees for s.Age(member expression) and 18(constant expression).We now need to check whether a member expression is greater than a constant expression or not.For that, use the Expression.GreaterThanOrEqual() method and pass the member expression and constant expression as parameters:
            BinaryExpression body = Expression.LessThanOrEqual(me, constant);

            // Thus, we have built an expression tree for a lambda expression body s.Age >= 18.We now need to join the parameter and body expressions.Use Expression.Lambda(body, parameters array) to join the body and parameter part of the lambda expression s => s.age >= 18:
            var isAdultExprTree = Expression.Lambda<Func<Student, bool>>(body, new[] { pe });

            Console.WriteLine("Expression Tree: {0}", isAdultExprTree);

            Console.WriteLine("Expression Tree Body: {0}", isAdultExprTree.Body);

            Console.WriteLine("Number of Parameters in Expression Tree: {0}", isAdultExprTree.Parameters.Count);

            Console.WriteLine("Parameters in Expression Tree: {0}", isAdultExprTree.Parameters[0]);

            Expression<Func<Student, bool>> isTeenAgerExpr = s => s.Age > 12 && s.Age < 20;

            Console.WriteLine("Expression: {0}", isTeenAgerExpr);

            Console.WriteLine("Expression Type: {0}", isTeenAgerExpr.NodeType);

            var parameters = isTeenAgerExpr.Parameters;

            foreach (var param in parameters)
            {
                Console.WriteLine("Parameter Name: {0}", param.Name);
                Console.WriteLine("Parameter Type: {0}", param.Type.Name);
            }
            var bodyExpr = isTeenAgerExpr.Body as BinaryExpression;

            Console.WriteLine("Left side of body expression: {0}", bodyExpr.Left);
            Console.WriteLine("Binary Expression Type: {0}", bodyExpr.NodeType);
            Console.WriteLine("Right side of body expression: {0}", bodyExpr.Right);
            Console.WriteLine("Return Type: {0}", isTeenAgerExpr.ReturnType);
        }

        private void LambdaExpression()
        {
            Print(this.GetCurrentMethod());

            Expression<Func<Student, bool>> isTeenAgerExpr = s => s.Age > 12 && s.Age < 20;

            Console.WriteLine(isTeenAgerExpr);

            //compile Expression using Compile method to invoke it as Delegate
            Func<Student, bool> isTeenAger = isTeenAgerExpr.Compile();
            Console.WriteLine(isTeenAger);

            //Invoke
            bool result = isTeenAger(new Student() { StudentID = 1, StudentName = "Steve", Age = 20 });
            Console.WriteLine(result);
        }

        // The Conversion operators in LINQ are useful in converting the type of the elements in a sequence(collection).
        // There are three types of conversion operators: As operators(AsEnumerable and AsQueryable), 
        // To operators(ToArray, ToDictionary, ToList and ToLookup), and Casting operators(Cast and OfType).

        // The AsEnumerable and AsQueryable methods cast or convert a source object to IEnumerable<T> or IQueryable<T> respectively.
        private void AsEnumerableCastToListToDictionaryToArray()
        {
            Print(this.GetCurrentMethod());

            Student[] studentArray =
                {
                    new Student() { StudentID = 1, StudentName = "John", Age = 18 },
                    new Student() { StudentID = 2, StudentName = "Steve", Age = 21 },
                    new Student() { StudentID = 3, StudentName = "Bill", Age = 25 },
                    new Student() { StudentID = 4, StudentName = "Ram", Age = 20 },
                    new Student() { StudentID = 5, StudentName = "Ron", Age = 31 },
                };

            ReportTypeProperties(studentArray);
            ReportTypeProperties(studentArray.AsEnumerable());
            ReportTypeProperties(studentArray.AsQueryable());

            // studentArray.Cast<Student>() is the same as (IEnumerable<Student>)studentArray but Cast<Student>() is more readable.
            ReportTypeProperties(studentArray);
            ReportTypeProperties((IEnumerable<Student>)studentArray);
            ReportTypeProperties(studentArray.Cast<Student>());

            // To operators force the execution of the query. It forces the remote query provider to execute 
            // a query and get the result from the underlying data source e.g.SQL Server database.
            var strList = new List<string>() { "One", "Two", "Three", "Four", "Three" };

            var strArray = strList.ToArray<string>(); // converts List to Array

            var list = strArray.ToList<string>(); // converts array into list

            // following converts list into dictionary where StudentId is a key
            var studentDict = this.StudentList.ToDictionary<Student, int>(s => s.StudentID);

            foreach (var key in studentDict.Keys)
            {
                Console.WriteLine("Key: {0}, Value: {1}", key, (studentDict[key] as Student).StudentName);
            }
        }

        static void ReportTypeProperties<T>(T obj)
        {
            Console.WriteLine("Compile-time type: {0}", typeof(T).Name);
            Console.WriteLine("Actual type: {0}", obj.GetType().Name);
        }

        // Take Takes elements up to a specified position starting from the first element in a sequence.
        // TakeWhile Returns elements from the first element until an element does not satisfy the condition.If the first element itself doesn't satisfy the condition then returns an empty collection.
        private void TakeTakeWhile()
        {
            Print(this.GetCurrentMethod());

            var strList = new List<string>() { "One", "Two", "Three", "Four", "Five" };

            var newList = strList.Take(2);

            foreach (var str in newList)
                Console.WriteLine(str);

            var result = strList.TakeWhile(s => s.Length < 4);

            foreach (string str in result)
                Console.WriteLine(str);

            var resultList = strList.TakeWhile((s, i) => s.Length > i);

            foreach (string str in resultList)
                Console.WriteLine(str);
        }

        // Partitioning operators split the sequence(collection) into two parts and return one of the parts.
        // Skip Skips elements up to a specified position starting from the first element in a sequence.
        // SkipWhile   Skips elements based on a condition until an element does not satisfy the condition. If the first element itself doesn't satisfy the condition, it then skips 0 elements and returns all the elements in the sequence.
        private void SkipSkipWhile()
        {
            Print(this.GetCurrentMethod());

            var strList = new List<string>() { "One", "Two", "Three", "Four", "Five", "Six" };

            var newList = strList.Skip(2);

            foreach (var str in newList) Console.WriteLine(str);

            var resultList = strList.SkipWhile(s => s.Length < 4);

            foreach (string str in resultList) Console.WriteLine(str);

            var result = strList.SkipWhile((s, i) => s.Length > i);

            foreach (string str in result)
                Console.WriteLine(str);

        }

        // The Union extension method requires two collections and returns a new collection that 
        // includes distinct elements from both the collections.Consider the following example.
        private void Union()
        {
            Print(this.GetCurrentMethod());

            IList<string> strList1 = new List<string>() { "One", "Two", "three", "Four" };
            IList<string> strList2 = new List<string>() { "Two", "THREE", "Four", "Five" };

            var result = strList1.Union(strList2);

            foreach (string str in result)
                Console.WriteLine(str);

        }

        // The Intersect extension method requires two collections.
        // It returns a new collection that includes common elements that exists in both the collection.Consider the following example.
        private void Intersect()
        {
            Print(this.GetCurrentMethod());

            var strList1 = new List<string>() { "One", "Two", "Three", "Four", "Five" };
            var strList2 = new List<string>() { "Four", "Five", "Six", "Seven", "Eight" };

            var result = strList1.Intersect(strList2);

            foreach (string str in result)
                Console.WriteLine(str);
        }

        // The Except() method requires two collections.It returns a new collection with elements from the 
        // first collection which do not exist in the second collection(parameter collection).
        private void Except()
        {
            Print(this.GetCurrentMethod());

            var strList1 = new List<string>() { "One", "Two", "Three", "Four", "Five" };
            var strList2 = new List<string>() { "Four", "Five", "Six", "Seven", "Eight" };

            var result = strList1.Except(strList2);

            foreach (string str in result)
                Console.WriteLine(str);

        }

        // The Distinct extension method returns a new collection of unique elements from the given collection.
        // The Distinct extension method doesn't compare values of complex type objects. 
        // You need to implement IEqualityComparer<T> interface in order to compare the values of complex types. 
        // In the following example, StudentComparer class implements IEqualityComparer<Student> to compare Student objects.
        private void Distinct()
        {
            Print(this.GetCurrentMethod());

            var strList = new List<string>() { "One", "Two", "Three", "Two", "Three" };

            var intList = new List<int>() { 1, 2, 3, 2, 4, 4, 3, 5 };

            var distinctList1 = strList.Distinct();

            foreach (var str in distinctList1)
                Console.WriteLine(str);

            var distinctList2 = intList.Distinct();

            foreach (var i in distinctList2)
                Console.WriteLine(i);

            var studentList = new List<Student>() {
                        new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                        new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
                        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                        new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                        new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
                    };


            var distinctStudents = studentList.Distinct(new Student.StudentComparer());

            foreach (Student std in distinctStudents)
                Console.WriteLine(std.StudentName);
        }

        // LINQ includes generation operators DefaultIfEmpty, Empty, Range & Repeat.The Empty, 
        // Range & Repeat methods are not extension methods for IEnumerable or IQueryable but they are simply static methods defined in a static class Enumerable.
        // Empty Returns an empty collection
        // Range   Generates collection of IEnumerable<T> type with specified number of elements with sequential values, starting from first element.
        // Repeat Generates a collection of IEnumerable<T> type with specified number of elements and each element contains same specified value.
        private void EmptyRangeRepeat()
        {
            Print(this.GetCurrentMethod());

            var emptyCollection1 = Enumerable.Empty<string>();
            var emptyCollection2 = Enumerable.Empty<Student>();

            Console.WriteLine("Count: {0} ", emptyCollection1.Count());
            Console.WriteLine("Type: {0} ", emptyCollection1.GetType().Name);

            Console.WriteLine("Count: {0} ", emptyCollection2.Count());
            Console.WriteLine("Type: {0} ", emptyCollection2.GetType().Name);

            var intCollection = Enumerable.Range(10, 10);
            Console.WriteLine("Total Count: {0} ", intCollection.Count());

            for (int i = 0; i < intCollection.Count(); i++)
                Console.WriteLine("Value at index {0} : {1}", i, intCollection.ElementAt(i));

            var intCollection2 = Enumerable.Repeat<int>(10, 10);
            Console.WriteLine("Total Count: {0} ", intCollection2.Count());

            for (int i = 0; i < intCollection2.Count(); i++)
                Console.WriteLine("Value at index {0} : {1}", i, intCollection2.ElementAt(i));


        }

        // The DefaultIfEmpty() method returns a new collection with the default value if the given 
        // collection on which DefaultIfEmpty() is invoked is empty.
        // Another overload method of DefaultIfEmpty() takes a value parameter that should be replaced with default value.
        private void DefaultIfEmpty()
        {
            Print(this.GetCurrentMethod());

            var emptyList = new List<string>();

            var newList1 = emptyList.DefaultIfEmpty();
            var newList2 = emptyList.DefaultIfEmpty("None");

            Console.WriteLine("Count: {0}", newList1.Count());
            Console.WriteLine("Value: {0}", newList1.ElementAt(0));

            Console.WriteLine("Count: {0}", newList2.Count());
            Console.WriteLine("Value: {0}", newList2.ElementAt(0));

            var emptyList2 = new List<int>();

            var newList3 = emptyList2.DefaultIfEmpty();
            var newList4 = emptyList2.DefaultIfEmpty(100);

            Console.WriteLine("Count: {0}", newList3.Count());
            Console.WriteLine("Value: {0}", newList3.ElementAt(0));

            Console.WriteLine("Count: {0}", newList4.Count());
            Console.WriteLine("Value: {0}", newList4.ElementAt(0));

            var emptyStudentList = new List<Student>();

            var newStudentList1 = emptyStudentList.DefaultIfEmpty(new Student());

            var newStudentList2 = emptyStudentList.DefaultIfEmpty(new Student()
            {
                StudentID = 0,
                StudentName = ""
            });

            Console.WriteLine("Count: {0} ", newStudentList1.Count());
            Console.WriteLine("Student ID: {0} ", newStudentList1.ElementAt(0));

            Console.WriteLine("Count: {0} ", newStudentList2.Count());
            Console.WriteLine("Student ID: {0} ", newStudentList2.ElementAt(0).StudentID);

        }

        // The Concat() method appends two sequences of the same type and returns a new sequence(collection).
        private void Concat()
        {
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

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
            Print(this.GetCurrentMethod());

            var mixedList = new ArrayList();
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
            Print(this.GetCurrentMethod());

            var stringList = new List<string>
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


