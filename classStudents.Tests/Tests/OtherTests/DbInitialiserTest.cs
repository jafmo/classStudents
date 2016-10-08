using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using classStudents.Repositories;
using classStudents.Models;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace classStudents.Tests.Tests.OtherTests
{
    [TestClass]
    public class DbInitialiserTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            /* Set up path to db on the main project app_Data */
            var dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
            var absoluteDataDirectory = Path.GetFullPath(dataDirectory);
            AppDomain.CurrentDomain.SetData("DataDirectory", absoluteDataDirectory);
        }
        [TestMethod]
        public void DbInitialiserRunTest()
        {
            //arrange
            DbInitialiser initialiser = new DbInitialiser();
            //act
            initialiser.run();
            // assert
            SchoolClassRepository schoolClassRepo = new SchoolClassRepository();
            StudentRepository studentRepo = new StudentRepository();
            List<schoolClass> schoolClassList = schoolClassRepo.getAllSchoolClasses();
            Assert.AreEqual(2, schoolClassList.Count);
            List<Student>  studentList1 = studentRepo.getSchoolClassStudents(schoolClassList[0].Id);
            Assert.AreEqual(2, studentList1.Count);
            List<Student> studentList2 = studentRepo.getSchoolClassStudents(schoolClassList[1].Id);
            Assert.AreEqual(2, studentList2.Count);
        }
    }
}
