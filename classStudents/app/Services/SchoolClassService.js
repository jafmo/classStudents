schoolClassStudentsApp.service('SchoolClassService', ['$http', '$httpParamSerializerJQLike',
    function ($http, $httpParamSerializerJQLike) {
        //Create new record
        this.post = function (schoolClass) {
            var request = $http({
                method: "POST",
                url: "http://localhost:54140/api/SchoolClass/createNewSchoolClass/",
                data: $httpParamSerializerJQLike(schoolClass),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            });
            return request;
        }
        //Get Single Records
        this.get = function (id) {
            return $http.get("http://localhost:54140/api/SchoolClass/getSchoolClassById/" + id);
        }
        //Get class Name
        this.getSchoolClassName = function (id) {
            return $http.get("http://localhost:54140/api/SchoolClass/getSchoolClassName/" + id);
        }
        //Get All 
        this.getAllSchoolClasses = function () {
            return $http.get("http://localhost:54140/api/SchoolClass/getAllSchoolClasses");
        }
        //Update the Record
        this.put = function (schoolClass) {
            var request = $http({
                method: "POST",
                url: "http://localhost:54140/api/SchoolClass/updateSchoolClass",
                data: $httpParamSerializerJQLike(schoolClass),
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
                url: "http://localhost:54140/api/SchoolClass/deleteSchoolClass/" + id
            });
            return request;
        }
    }]);