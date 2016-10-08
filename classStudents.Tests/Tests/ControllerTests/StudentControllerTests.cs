using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using classStudents.Models;
using System.Collections.Generic;
using classStudents.Controllers;
using System.Configuration;
using System.IO;

namespace classStudents.Tests.Controllers
{
    [TestClass]
    public class StudentControllerTests
    {
        StudentController studentController;
        public StudentControllerTests() { }
        private Student getNewStudent(int id, string firstname, string surename,int age, decimal gpa, int schoolClassId)
        {
            return new Student
            {
                Id = id,
                firstname = firstname,
                surname = surename,
                 Age = age,
                 GPA = gpa,
                 schoolClassId = schoolClassId
            };
        }
        [TestInitialize]
        public void TestInitialize()
        {
            /* Set up path to db on the main project app_Data */
            var dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
            var absoluteDataDirectory = Path.GetFullPath(dataDirectory);
            AppDomain.CurrentDomain.SetData("DataDirectory", absoluteDataDirectory);
        }
        [TestMethod]
        public void getSchoolClassStudentsTest()
        {
            //arrange
            Student student1 = getNewStudent(1, "Sam", "Kim", 20, 3.5m,0);
            Student student2 = getNewStudent(2, "Mo", "Jafat", 30, 3.8m, 1);
            List<Student> students = new List<Student>();
            students.Add(student1);
            students.Add(student2);
            studentController = new StudentController(new InMemoryStudentRepository(students));
            //act
            var result = studentController.getSchoolClassStudents(1);
            //assert
            var StudentsList = (List<Student>)result;
            Assert.AreEqual(1,StudentsList.Count);
            CollectionAssert.Contains(StudentsList, student2);
        }
        [TestMethod]
        public void getStudentByIdTest()
        {
            //arrange
            Student student1 = getNewStudent(1, "Sam", "Kim", 20, 3.5m, 0);
            List<Student> students = new List<Student>();
            students.Add(student1);
            studentController = new StudentController(new InMemoryStudentRepository(students));
            //act
            var result = studentController.getStudentById(student1.Id);
            //Assert
            Student returnedSchoolClass = (Student)result;
            Assert.AreEqual(student1, returnedSchoolClass);
        }
        [TestMethod]
        public void createNewStudentTest()
        {
            //arrange
            List<Student> students = new List<Student>();
            studentController = new StudentController(new InMemoryStudentRepository(students));
            //act
            Student student1 = getNewStudent(1, "Sam", "Kim", 20, 3.5m, 0);
            Dictionary<string, object> result = studentController.createNewStudent(student1);
            //Assert
            Assert.IsTrue((bool)result["success"]);
        }
        // actual Db test
        [TestMethod]
        public void createNewStudentWithUniqueSurnameTest()
        {
            //arrange
            studentController = new StudentController(new StudentRepository());
            SchoolClassController schoolClassController = new SchoolClassController(new SchoolClassRepository());
            //create Class
            Dictionary<string, object> classCreated = schoolClassController.createNewSchoolClass(
                new schoolClass {
                    className = "Biology",
                    Location = "Melbourne",
                    teacherName = "Blah Blah"
                });
            // get class Id
            int schoolClassId = ((schoolClass)classCreated["data"]).Id;
            //act
            Student student1 = getNewStudent(0, "Sam", "Kim", 20, 3.5m, schoolClassId);
            Dictionary<string, object> result1 = studentController.createNewStudent(student1);
            Student student2 = getNewStudent(0, "Tim", "Kim", 18, 3.0m, schoolClassId);
            Dictionary<string, object> result2 = studentController.createNewStudent(student2);
            //Assert
            Assert.IsTrue((bool)result1["success"]);
            Assert.IsFalse((bool)result2["success"]);
            Assert.AreEqual("Surename should be unique.", (string)result2["error"]);
            //clean up
            studentController.deleteStudent(student1.Id);
            schoolClassController.deleteSchoolClass(schoolClassId);

        }
        [TestMethod]
        public void updateStudentTest()
        {
            //arrange
            Student student1 = getNewStudent(1, "Sam", "Kim", 20, 3.5m, 0);
            List<Student> students = new List<Student>();
            students.Add(student1);
            studentController = new StudentController(new InMemoryStudentRepository(students));
            //act
            Student studentToUpdate = getNewStudent(1, "Sam", "Kim", 20, 3.0m, 0);
            Dictionary<string, object> result = studentController.updateStudent(studentToUpdate);
            //Assert
            Assert.IsTrue((bool)result["success"]);
            decimal newGpa = students.Find(s => s.Id == student1.Id).GPA;
            Assert.AreEqual(3.0m, newGpa);
        }
        [TestMethod]
        public void deleteStudentTest()
        {
            //arrange
            Student student1 = getNewStudent(1, "Sam", "Kim", 20, 3.5m, 0);
            Student student2 = getNewStudent(2, "Mo", "Jafat", 30, 3.8m, 1);
            List<Student> students = new List<Student>();
            students.Add(student1);
            students.Add(student2);
            studentController = new StudentController(new InMemoryStudentRepository(students));
            //act
            bool result = studentController.deleteStudent(1);
            //assert
            Assert.AreEqual(1, students.Count);
            CollectionAssert.DoesNotContain(students, student1);
            CollectionAssert.Contains(students, student2);
        }

    }
}
