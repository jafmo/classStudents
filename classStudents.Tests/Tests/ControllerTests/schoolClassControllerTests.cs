using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using classStudents.Controllers;
using classStudents.Models;
using System.Collections.Generic;

namespace classStudents.Tests.Controllers
{
    [TestClass]
    public class schoolClassControllerTests
    {
        private SchoolClassController schoolClassController;
        private schoolClass getNewShcoolClass(int Id, string className, string Location, string teacherName )
        {
            return new schoolClass
            {
                Id = Id,
                className = className,
                Location = Location,
                teacherName = teacherName
            };
        }
        public schoolClassControllerTests() {}

        [TestMethod]
        public void getAllSchoolClassesTest()
        {
            //arrange
            schoolClass class1 = getNewShcoolClass(1,"Biology","Melbourne uni","John Doe");
            schoolClass class2 = getNewShcoolClass(1, "Physics", "Melbourne uni", "Jane Doe");
            List<schoolClass> schoolClasses = new List<schoolClass>();
            schoolClasses.Add(class1);
            schoolClasses.Add(class2);
            schoolClassController = new SchoolClassController(new InMemorySchoolClassRepository(schoolClasses));
            //act
            var result = schoolClassController.getAllSchoolClasses();
            //assert
            var schoolClassesList = (List<schoolClass>)result;
            Assert.AreEqual(2,schoolClassesList.Count);
            CollectionAssert.Contains(schoolClassesList, class1);
            CollectionAssert.Contains(schoolClassesList, class2);
        }
        [TestMethod]
        public void getSchoolClassByIdTest()
        {
            //arrange
            schoolClass class1 = getNewShcoolClass(1, "Biology", "Melbourne uni", "John Doe");
            List<schoolClass> schoolClasses = new List<schoolClass>();
            schoolClasses.Add(class1);
            schoolClassController = new SchoolClassController(new InMemorySchoolClassRepository(schoolClasses));
            //act
            var result = schoolClassController.getSchoolClassById(class1.Id);
            //Assert
            schoolClass returnedSchoolClass = (schoolClass)result;
            Assert.AreEqual(class1, returnedSchoolClass);
        }
        [TestMethod]
        public void getSchoolClassNameTest()
        {
            //arrange
            schoolClass class1 = getNewShcoolClass(1, "Biology", "Melbourne uni", "John Doe");
            List<schoolClass> schoolClasses = new List<schoolClass>();
            schoolClasses.Add(class1);
            schoolClassController = new SchoolClassController(new InMemorySchoolClassRepository(schoolClasses));
            //act
            var result = schoolClassController.getSchoolClassName(class1.Id);
            //Assert
            string returnedSchoolClassName = (string)result;
            Assert.AreEqual(class1.className, returnedSchoolClassName);
        }
        [TestMethod]
        public void createNewSchoolClassTest()
        {
            //Arrange
            List<schoolClass> schoolClasses = new List<schoolClass>();
            schoolClassController = new SchoolClassController(new InMemorySchoolClassRepository(schoolClasses));
            //act
            schoolClass newSchoolClass = getNewShcoolClass(0,"Biology","Sydney","Blah Blah");
            Dictionary<string, object> result = schoolClassController.createNewSchoolClass(newSchoolClass);
            //Assert
            Assert.IsTrue((bool)result["success"]);
        }
        [TestMethod]
        public void updateSchoolClassTest()
        {
            //Arrange
            schoolClass class1 = getNewShcoolClass(1, "Biology", "Melbourne uni", "John Doe");
            List<schoolClass> schoolClasses = new List<schoolClass>();
            schoolClasses.Add(class1);
            schoolClassController = new SchoolClassController(new InMemorySchoolClassRepository(schoolClasses));
            //act
            schoolClass schoolClassToUpdate = getNewShcoolClass(1, "Biology", "Sydney", "John Doe");
            Dictionary<string, object> result = schoolClassController.updateSchoolClass(schoolClassToUpdate);
            //Assert
            Assert.IsTrue((bool)result["success"]);
            string newlocation = schoolClasses.Find(s => s.Id == class1.Id).Location;
            Assert.AreEqual("Sydney", newlocation);
        }
        [TestMethod]
        public void deleteSchoolClassTest()
        {
            //arrange
            schoolClass class1 = getNewShcoolClass(1, "Biology", "Melbourne uni", "John Doe");
            List<schoolClass> schoolClasses = new List<schoolClass>();
            schoolClasses.Add(class1);
            schoolClassController = new SchoolClassController(new InMemorySchoolClassRepository(schoolClasses));
            //act
            Dictionary<string, object> result = schoolClassController.deleteSchoolClass(class1.Id);
            //assert
            Assert.IsTrue((bool)result["success"]);
        }
 
    }
}
