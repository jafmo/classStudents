using System;
using System.Collections.Generic;
using System.Linq;

namespace classStudents.Models
{
    public class SchoolClassRepository:ISchoolClassRepository
    {
        private classStudentsEntities _db;
        public SchoolClassRepository()
        {
        }
        public Dictionary<string, object> createNewSchoolClass(schoolClass classToCreate)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                using (_db = new classStudentsEntities())
                {
                    _db.schoolClasses.Add(classToCreate);
                    _db.SaveChanges();
                    result.Add("success", true);
                    result.Add("data", classToCreate);
                    return result;
                }
            }
            catch (Exception e)
            {
                result.Add("success", false);
                result.Add("data", classToCreate);
                result.Add("error", "Error Add School Class.\r\n" + e.InnerException.InnerException.Message);
                return result;
            }
        }
        public Dictionary<string, object> updateSchoolClass(schoolClass classToUpdate)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                using (_db = new classStudentsEntities())
                {
                    schoolClass originallClass = _db.schoolClasses.FirstOrDefault(sc => sc.Id == classToUpdate.Id);
                    if (originallClass != null)
                    {
                        originallClass.className = classToUpdate.className;
                        originallClass.Location = classToUpdate.Location;
                        originallClass.teacherName = classToUpdate.teacherName;
                        _db.SaveChanges();
                        result.Add("success", true);
                        result.Add("data", classToUpdate);
                        return result;
                    }
                    else
                    {
                        result.Add("success", false);
                        result.Add("data", classToUpdate);
                        result.Add("error", "Class was not found.");
                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                result.Add("success", false);
                result.Add("data", classToUpdate);
                result.Add("error", "Error Update School Class.\r\n" + e.InnerException.InnerException.Message);
                return result;
            }
        }
        public Dictionary<string, object> deleteSchoolClass(int Id)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            schoolClass schoolClassToDelete = getSchoolClassById(Id);
            try
            {
                if (schoolClassToDelete != null)
                {
                    using (_db = new classStudentsEntities())
                    {
                        _db.schoolClasses.Attach(schoolClassToDelete);
                        _db.schoolClasses.Remove(schoolClassToDelete);
                        _db.SaveChanges();
                        result.Add("success", true);
                        result.Add("data", schoolClassToDelete);
                        return result;
                    }
                }
                else
                {
                    result.Add("success", false);
                    result.Add("data", schoolClassToDelete);
                    result.Add("error", "School Class was not found!");
                    return result;
                }
            }
            catch (Exception e)
            {
                result.Add("success", false);
                result.Add("data", schoolClassToDelete);
                result.Add("error", "Error Delete School Class.\r\n" + e.InnerException.InnerException.Message);
                return result;
            }
        }
        public schoolClass getSchoolClassById(int Id)
        {
            schoolClass schoolClassToFind = null;
            using (_db = new classStudentsEntities())
            {
                schoolClassToFind = _db.schoolClasses.FirstOrDefault(sc => sc.Id == Id);
            }
            return schoolClassToFind;
        }
        public string getSchoolClassName(int Id)
        {
            string schoolClassName = string.Empty;
            using (_db = new classStudentsEntities())
            {
                schoolClassName = _db.schoolClasses.FirstOrDefault(sc => sc.Id == Id).className;
            }
            return schoolClassName;
        }
        public List<schoolClass> getAllSchoolClasses()
        {
            List<schoolClass> result = new List<schoolClass>();
            using (_db = new classStudentsEntities())
            {
                result = _db.schoolClasses.ToList();
            }
            return result;
        }
    }
}