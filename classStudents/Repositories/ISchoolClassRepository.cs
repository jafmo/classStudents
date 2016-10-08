using System;
using System.Collections.Generic;

namespace classStudents.Models
{
    public interface ISchoolClassRepository
    {
        Dictionary<string, object> createNewSchoolClass(schoolClass classToCreate);
        Dictionary<string, object> updateSchoolClass(schoolClass classToUpdate);
        Dictionary<string, object> deleteSchoolClass(int Id);
        schoolClass getSchoolClassById(int Id);
        string getSchoolClassName(int Id);
        List<schoolClass> getAllSchoolClasses();
    }
}