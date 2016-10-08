schoolClassStudentsApp.service('StudentService', ['$http', '$httpParamSerializerJQLike',
    function ($http, $httpParamSerializerJQLike) {
        //Create new record
        this.post = function (student) {
            var request = $http({
                method: "POST",
                url: "http://localhost:54140/api/Student/createNewStudent/",
                data: $httpParamSerializerJQLike(student),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            });
            return request;
        }
        //Get Single Records
        this.get = function (id) {
            return $http.get("http://localhost:54140/api/Student/getStudentById/" + id);
        }

        //Get All 
        this.getSchoolClassStudents = function (schoolClassId) {
            return $http.get("http://localhost:54140/api/Student/getSchoolClassStudents?schoolclassid=" + schoolClassId);
        }
        //Update the Record
        this.put = function (student) {
            var request = $http({
                method: "POST",
                url: "http://localhost:54140/api/Student/updateStudent",
                data: $httpParamSerializerJQLike(student),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            });
            return request;
        }
        //Delete the Record
        this.delete = function (id) {
            var request = $http({
                method: "POST",
                url: "http://localhost:54140/api/Student/deleteStudent/" + id
            });
            return request;
        }
    }]);