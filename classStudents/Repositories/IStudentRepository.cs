using System;
using System.Collections.Generic;

namespace classStudents.Models
{
    public interface IStudentRepository
    {
        Dictionary<string, object> createNewStudent(Student studentToCreate);
        Dictionary<string, object> updateStudent(Student studentToUpdate);
        bool deleteStudent(int Id);
        Student getStudentById(int Id);
        List<Student> getSchoolClassStudents(int schoolClassId);
    }
}