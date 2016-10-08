using classStudents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace classStudents.Controllers
{
    public class StudentController : ApiController
    {
        IStudentRepository _repository;
        public StudentController(IStudentRepository repository)
        {
            _repository = repository;
        }
        public StudentController()
        {
            _repository = new StudentRepository();
        }
        public List<Student> getSchoolClassStudents(int schoolClassId)
        {
            return _repository.getSchoolClassStudents(schoolClassId);
        }

        public Student getStudentById(int Id)
        {
            return _repository.getStudentById(Id);
        }
        [HttpPost]
        public Dictionary<string, object> createNewStudent(Student StudentToCreate)
        {
            return _repository.createNewStudent(StudentToCreate);
        }
        [HttpPost]
        public Dictionary<string, object> updateStudent(Student StudentToUpdate)
        {
            return _repository.updateStudent(StudentToUpdate);
        }
        [HttpPost]
        public bool deleteStudent(int Id)
        {
            return _repository.deleteStudent(Id);
        }
    }
}
