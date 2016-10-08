using System;
using System.Collections.Generic;
using System.Linq;

namespace classStudents.Models
{
    public class InMemorySchoolClassRepository:ISchoolClassRepository
    {
        private List<schoolClass> _db ;

        public InMemorySchoolClassRepository(List<schoolClass> db)
        {
            if (db == null)
            {
                this._db = new List<schoolClass>();
            }
            else
            {
                this._db = db;
            }
        }
        public Dictionary<string, object> createNewSchoolClass(schoolClass classToCreate)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                this._db.Add(classToCreate);
                result.Add("success", true);
                result.Add("data", classToCreate);
                return result;
            }
            catch(Exception e)
            {
                result.Add("success", false);
                result.Add("data", e.Message);
                return result;
            }
        }
        public Dictionary<string, object> updateSchoolClass(schoolClass classToUpdate)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                schoolClass orignalSchoolClass = getSchoolClassById(classToUpdate.Id);
                int index = this._db.IndexOf(orignalSchoolClass);
                if (index != -1)
                {
                    this._db[index] = classToUpdate;
                }
                result.Add("success", true);
                result.Add("data", classToUpdate);
                return result;
            }
            catch(Exception e)
            {
                result.Add("success", false);
                result.Add("error", e.Message);
                return result;
            }
        }
        public Dictionary<string, object> deleteSchoolClass(int Id)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                schoolClass schoolClassToDelete = getSchoolClassById(Id);
                if (schoolClassToDelete != null)
                {
                    this._db.Remove(schoolClassToDelete);
                    result.Add("success", true);
                    result.Add("data", schoolClassToDelete);
                    return result;
                }
                else
                {
                    result.Add("success", false);
                    result.Add("error", "School Class was not found!");
                    return result;
                }
            }
            catch(Exception e)
            {
                result.Add("success", false);
                result.Add("error", e.Message);
                return result;
            }
        }
        public schoolClass getSchoolClassById(int Id)
        {
            schoolClass schoolClassToFind = null; 
            schoolClassToFind = this._db.FirstOrDefault(sc=>sc.Id == Id);
            return schoolClassToFind;
        }
        public string getSchoolClassName(int Id)
        {
            string schoolClassName = string.Empty;
            schoolClassName = this._db.FirstOrDefault(sc => sc.Id == Id).className;
            return schoolClassName;
        }
        public List<schoolClass> getAllSchoolClasses()
        {
            return _db;
        }
        public bool saveChanges()
        {
            return true;
        }

    }
}