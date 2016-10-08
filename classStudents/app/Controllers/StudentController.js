schoolClassStudentsApp.controller('StudentController', function ($scope, $location, $routeParams, StudentService, SchoolClassService) {
    $scope.Title = "Students";
    $scope.init = function () {
        $scope.schoolClassId = $routeParams.id;
        getSchoolClassName($scope.schoolClassId);
        getSchoolClassStudents($scope.schoolClassId);
        $scope.selectedStudent = [];
        $scope.errorMessage = "";
    }
    var getSchoolClassName = function (schoolClassId) {
        SchoolClassService.getSchoolClassName(schoolClassId).then(function (schoolClassName) {
            $scope.schoolClassName = schoolClassName.data;
        });
    }
    var getSchoolClassStudents = function (schoolClassId) {
        StudentService.getSchoolClassStudents(schoolClassId).then(function (students) {
            $scope.students = students.data
        });
    }
    $scope.go = function (path) {
        $location.path(path);
    };
    $scope.goBack = function()
    {
        history.back();
    }
    $scope.loadEdit = function () {
        var id = $routeParams.id;
        StudentService.get(id).then(function (student) {
            $scope.selectedStudent = student.data;
        });
    }
    $scope.updateStudent = function (student) {
        StudentService.put(student).then(function (student) {
            $scope.selectedStudent = student.data.data;
            if (student.data.success == true) {
                $scope.goBack();
            }
            else {
                $scope.errorMessage = student.data.error;
            }
        });
    }
    $scope.addStudent = function (student) {
        $scope.schoolClassId = $routeParams.id;
        student.schoolClassId = $scope.schoolClassId;
        StudentService.post(student).then(function (student) {
            $scope.selectedStudent = student.data.data;
            if (student.data.success == true) {
                $scope.goBack();
            }
            else {
                $scope.errorMessage = student.data.error;
            }
        });
    }
    $scope.deleteStudent = function (id) {
        StudentService.delete(id).then(function (response) {
            if (response) {
                $scope.goBack();
            }
        });
    }
    $scope.loadDelete = function () {
        var id = $routeParams.id;
        StudentService.get(id).then(function (student) {
            $scope.selectedStudent = student.data;
        });
    }
    $scope.loadAdd = function () {
        $scope.selectedStudent = getStudent();
    }
    var getStudent = function () {
        var student =
        {
            Id: 0,
            firstname: "",
            surname: "",
            Age: 0,
            GPA: 0,
            schoolClassId:0
        };
        return student;
    }
});