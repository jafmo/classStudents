using System;
using System.Collections.Generic;

namespace classStudents.Models
{
    public class InMemoryStudentRepository:IStudentRepository
    {
        private List<Student> _db;

        public InMemoryStudentRepository(List<Student> db)
        {
            if (db == null)
            {
                this._db = new List<Student>();
            }
            else
            {
                this._db = db;
            }
        }
        public Dictionary<string, object> createNewStudent(Student studentToCreate)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                this._db.Add(studentToCreate);
                result.Add("success", true);
                result.Add("data", studentToCreate);
                return result;
            }
            catch(Exception e)
            {
                result.Add("success", false);
                result.Add("data", e.Message);
                return result;
            }
        }
        public Dictionary<string, object> updateStudent(Student studentToUpdate)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                Student originalStudent = this._db.Find(s=>s.Id == studentToUpdate.Id);
                int index = this._db.IndexOf(originalStudent);
                if (index != -1)
                {
                    this._db[index] = studentToUpdate;
                }
                result.Add("success", true);
                result.Add("data", studentToUpdate);
                return result;
            }
            catch (Exception e)
            {
                result.Add("success", false);
                result.Add("data", e.Message);
                return result;
            }
        }
        public bool deleteStudent(int Id)
        {
            try
            {
                Student studentToDelete = getStudentById(Id);
                if (studentToDelete != null)
                {
                    this._db.Remove(studentToDelete);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public Student getStudentById(int id)
        {
            Student studentToFind = null;
            foreach (var student in this._db)
            {
                if (student.Id == id)
                {
                    studentToFind = student;
                    break;
                }
            }
            return studentToFind;
        }
        public List<Student> getSchoolClassStudents(int schoolClassId)
        {
            List<Student> restult = new List<Student>();
            foreach (Student student in this._db)
            {
                if (student.schoolClassId == schoolClassId)
                {
                    restult.Add(student);
                }
            }
            return restult;
        }
        public bool saveChanges()
        {
            return true;
        }
    }
}