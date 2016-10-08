using classStudents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace classStudents.Controllers
{
    public class SchoolClassController : ApiController
    {
        ISchoolClassRepository _repository;
        public SchoolClassController(ISchoolClassRepository repository)
        {
            _repository = repository;
        }
        public SchoolClassController()
        {
            _repository = new SchoolClassRepository();
        }
        public List<schoolClass> getAllSchoolClasses()
        {
            return _repository.getAllSchoolClasses();
        }

        public schoolClass getSchoolClassById(int Id)
        {
            return _repository.getSchoolClassById(Id);
        }
        public string getSchoolClassName(int Id)
        {
            return _repository.getSchoolClassName(Id);
        }
        [HttpPost]
        public Dictionary<string, object> createNewSchoolClass(schoolClass SchoolClassToCreate)
        {
            return _repository.createNewSchoolClass(SchoolClassToCreate);
        }
        [HttpPost]
        public Dictionary<string, object> updateSchoolClass(schoolClass SchoolClassToUpdate)
        {
            return _repository.updateSchoolClass(SchoolClassToUpdate);
        }
        [HttpPost]
        public Dictionary<string, object> deleteSchoolClass(int Id)
        {
            return _repository.deleteSchoolClass(Id);
        }
    }
}
