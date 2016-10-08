schoolClassStudentsApp.controller('SchoolClassController', function ($scope, $location, $routeParams, SchoolClassService) {
    $scope.Title = "School Classes";
    $scope.init = function () {
        $scope.selectedRow = null;
        getAllSchoolClasses();
        $scope.selectedSchoolClass = [];
        $scope.errorMessage = "";
    }
    var getAllSchoolClasses = function () {
        SchoolClassService.getAllSchoolClasses().then(function (schoolClasses) {
            $scope.schoolClasses = schoolClasses.data
        });
    }
    $scope.setClickedRow = function (index, schoolClassId) {
        $scope.selectedRow = index;
        $scope.go('/students/' + schoolClassId);
    }
    $scope.go = function (path) {
        $location.path(path);
    };
    $scope.loadEdit = function () {
        var id = $routeParams.id;
        SchoolClassService.get(id).then(function (schoolClass) {
            $scope.selectedSchoolClass = schoolClass.data;
        });
    }
    $scope.updateSchoolClass = function (schoolClass) {
        SchoolClassService.put(schoolClass).then(function (schoolClass) {
            $scope.selectedSchoolClass = schoolClass.data.data;
            if (schoolClass.data.success == true) {
                $scope.go('/');
            }
            else {
                $scope.errorMessage = schoolClass.data.error;
            }
        });
    }
    $scope.addSchoolClass = function (schoolClass) {
        SchoolClassService.post(schoolClass).then(function (schoolClass) {
            $scope.selectedSchoolClass = schoolClass.data.data;
            if (schoolClass.data.success == true) {
                $scope.go('/');
            }
            else
            {
                $scope.errorMessage = schoolClass.data.error;
            }
        });
    }
    $scope.deleteSchoolClass = function (id) {
        SchoolClassService.delete(id).then(function (schoolClass) {
            $scope.selectedSchoolClass = schoolClass.data;
            if (schoolClass.data) {
                $scope.go('/');
            }
        });
    }
    $scope.loadDelete = function () {
        var id = $routeParams.id;
        SchoolClassService.get(id).then(function (schoolClass) {
            $scope.selectedSchoolClass = schoolClass.data;
        });
    }
    $scope.loadAdd = function () {
        $scope.selectedSchoolClass = getSchoolClass();
    }
    var getSchoolClass = function () {
        var schoolClass =
        {
            Id: 0,
            className: "",
            Location: "",
            teacherName: ""
        };
        return schoolClass;
    }
});