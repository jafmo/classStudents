using System;
using System.Collections.Generic;
using System.Linq;

namespace classStudents.Models
{
    public class StudentRepository:IStudentRepository
    {
        private classStudentsEntities _db;

        public StudentRepository()
        {
        }
        public void setUniqueSurname()
        {
            using (_db = new classStudentsEntities())
            {
                _db.Database.ExecuteSqlCommand("ALTER TABLE [dbo].[Student] ADD CONSTRAINT UQ_surname UNIQUE (surname, schoolClassId)");
            }
        }
        public Dictionary<string, object> createNewStudent(Student studentToCreate)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                using (_db = new classStudentsEntities())
                {
                    _db.Students.Add(studentToCreate);
                    _db.SaveChanges();
                    result.Add("success", true);
                    result.Add("data", studentToCreate);
                    return result;
                }
            }
            catch(Exception e)
            {
                result.Add("success", false);
                result.Add("data", studentToCreate);
                string error = e.InnerException.InnerException.Message;
                if(error.ToLower().IndexOf("uq_surname") > -1)
                {
                    result.Add("error", "Surename should be unique.");
                }
                else
                {
                    result.Add("error", "Failed to add student.\r\n" + e.Message);
                }
                return result;
            }
        }
        public Dictionary<string, object> updateStudent(Student studentToUpdate)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                using (_db = new classStudentsEntities())
                {
                    Student originalStudent = _db.Students.FirstOrDefault(s => s.Id == studentToUpdate.Id);
                    originalStudent.firstname = studentToUpdate.firstname;
                    originalStudent.surname = studentToUpdate.surname;
                    originalStudent.Age = studentToUpdate.Age;
                    originalStudent.GPA = studentToUpdate.GPA;
                    _db.SaveChanges();
                    result.Add("success", true);
                    result.Add("data", studentToUpdate);
                    return result;
                }
            }
            catch (Exception e)
            {
                result.Add("success", false);
                result.Add("data", studentToUpdate);
                string error = e.InnerException.InnerException.Message;
                if (error.ToLower().IndexOf("uq_surname") > -1)
                {
                    result.Add("error", "Surename should be unique.");
                }
                else
                {
                    result.Add("error", "Failed to update student.\r\n" + e.Message);
                }
                return result;
            }
        }
        public bool deleteStudent(int Id)
        {
            try
            {
                using (_db = new classStudentsEntities())
                {
                    _db.Students.Remove(_db.Students.ToList().FirstOrDefault(s => s.Id == Id));
                    _db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public Student getStudentById(int id)
        {
            Student studentToFind = null;
            using (_db = new classStudentsEntities())
            {
                foreach (var student in _db.Students)
                {
                    if (student.Id == id)
                    {
                        studentToFind = student;
                        break;
                    }
                }
            }
            return studentToFind;
        }
        public List<Student> getSchoolClassStudents(int schoolClassId)
        {
            List<Student> restult = new List<Student>();
            using (_db = new classStudentsEntities())
            {
                foreach (Student student in _db.Students)
                {
                    if (student.schoolClassId == schoolClassId)
                    {
                        restult.Add(student);
                    }
                }
            }
            return restult;
        }
    }
}