using classStudents.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace classStudents.Repositories
{
    public class DbInitialiser
    {
        public void run()
        {
            //Drop Database and initialise
            Database.SetInitializer(new DropCreateDatabaseAlways<classStudentsEntities>());
            SchoolClassRepository schoolClassRepo = new SchoolClassRepository();
            StudentRepository studentRepo = new StudentRepository();
            studentRepo.setUniqueSurname();

            var class1 = schoolClassRepo.createNewSchoolClass(
                new schoolClass
                {
                    className = "Biology",
                    Location = "B12",
                    teacherName = "Mr Burns"
                });

            var class2 = schoolClassRepo.createNewSchoolClass(
            new schoolClass
            {
                className = "Physics",
                Location = "B10",
                teacherName = "Mr Blah"
            });

            var student1 = studentRepo.createNewStudent(
                    new Student
                    {
                        firstname = "Michael",
                        surname = "Jackson1",
                        Age = 20,
                        GPA = 4.0m,
                        schoolClassId = ((schoolClass)class1["data"]).Id
                    });

            var student2 = studentRepo.createNewStudent(
                new Student
                {
                    firstname = "Janet",
                    surname = "Jackson2",
                    Age = 17,
                    GPA = 3.0m,
                    schoolClassId = ((schoolClass)class1["data"]).Id
                });

            var student3 = studentRepo.createNewStudent(
                new Student
                {
                    firstname = "Joe",
                    surname = "Jackson1",
                    Age = 22,
                    GPA = 2.8m,
                    schoolClassId = ((schoolClass)class2["data"]).Id
                });

            var student4 = studentRepo.createNewStudent(
                new Student
                {
                    firstname = "Tom",
                    surname = "Jackson2",
                    Age = 25,
                    GPA = 3.3m,
                    schoolClassId = ((schoolClass)class2["data"]).Id
                });
        }
    }
}