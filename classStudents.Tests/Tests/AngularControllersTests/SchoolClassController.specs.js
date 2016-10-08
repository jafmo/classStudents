var data =
{
    schoolClass:
        {
            success:true,
            data:{ Id: 3, className: "Physics", Location: "Melbourne", teacherName: "Blah Blah", Students: [] }
        },
    schoolClassesList:
        {
            success: true,
            data:
            [
            { Id: 1, className: "Biology", Location: "Melbourne", teacherName: "Blah Blah", Students: [] },
            { Id: 2, className: "Science", Location: "Sydney", teacherName: "Blah Blah", Students: [] }
            ]
            }
};
describe('schoolClassStudentsApp', function () {
    var $httpBackend,
     expectedUrl = 'http://localhost:54140/api/SchoolClass/',
     httpController, location;

    beforeEach(function () {
        module('schoolClassStudentsApp');
    });

    describe('SchoolClassController Test', function () {
        beforeEach(inject(function ($rootScope, $controller, _$httpBackend_, $location) {
            $httpBackend = _$httpBackend_;
            scope = $rootScope.$new();
            location = $location;
            httpController = $controller('SchoolClassController', {
                '$scope': scope
            });
        }));

        it('$scope.go should change location to the path parameter', function () {
            spyOn(location, 'path');
            scope.go('/new/path');
            expect(location.path).toHaveBeenCalledWith('/new/path');
        });

        it('Title set to "School Classes"', function () {
            expect(httpController).toBeDefined();
            expect(scope.Title).toEqual('School Classes');
        });

        it('$scope.init should load list of school classes and initialise selectedSchoolClass', function () {
            $httpBackend.expectGET(expectedUrl + 'getAllSchoolClasses').respond(200, data.schoolClassesList);
            scope.init();
            $httpBackend.flush();
            expect(httpController).toBeDefined();
            expect(scope.schoolClasses).toBeDefined();
            expect(scope.schoolClasses.data.length).toEqual(2);
            expect(scope.selectedSchoolClass).toBeDefined();
            expect(scope.selectedRow).toBeDefined();
            expect(scope.errorMessage).toBeDefined();
        });

        it('$scope.loadEdit should call "getSchoolClassById" and return a selected school class"', function () {
            $httpBackend.expectGET(expectedUrl + 'getSchoolClassById/undefined').respond(200, data.schoolClass);
            scope.loadEdit();
            $httpBackend.flush();
            expect(httpController).toBeDefined();
            expect(scope.selectedSchoolClass).toBeDefined();
            expect(scope.selectedSchoolClass.data.Id).toEqual(3);
        });

        it('$scope.updateSchoolClass calls "updateSchoolClass" then return school class object.', function () {
            $httpBackend.expectPOST(expectedUrl + 'updateSchoolClass').respond(200, data.schoolClass);
            scope.updateSchoolClass();
            $httpBackend.flush();
            expect(httpController).toBeDefined();
            expect(scope.selectedSchoolClass.Id).toEqual(3);
        });

        it('$scope.addSchoolClass calls "createNewSchoolClass" then return school class object.', function () {
            $httpBackend.expectPOST(expectedUrl + 'createNewSchoolClass/').respond(200, data.schoolClass);
            scope.addSchoolClass();
            $httpBackend.flush();
            expect(httpController).toBeDefined();
            expect(scope.selectedSchoolClass.Id).toEqual(3);
        });

        it('$scope.deleteSchoolClass calls "deleteSchoolClass" then return school class object.', function () {
            $httpBackend.expectPOST(expectedUrl + 'deleteSchoolClass/undefined').respond(200, data.schoolClass);
            scope.deleteSchoolClass();
            $httpBackend.flush();
            expect(httpController).toBeDefined();
            expect(scope.selectedSchoolClass.data.Id).toEqual(3);
        });

        it('$scope.loadDelete calls "getSchoolClassById" then return school class object.', function () {
            $httpBackend.expectGET(expectedUrl + 'getSchoolClassById/undefined').respond(200, data.schoolClass);
            scope.loadDelete();
            $httpBackend.flush();
            expect(httpController).toBeDefined();
            expect(scope.selectedSchoolClass).toBeDefined();
            expect(scope.selectedSchoolClass.data.Id).toEqual(3);
        });

        it('$scope.loadAdd returns an empty school class object and set selectedSchoolClass.', function () {
            scope.loadAdd();
            expect(httpController).toBeDefined();
            expect(scope.selectedSchoolClass).toBeDefined();
            expect(scope.selectedSchoolClass.Id).toEqual(0);
        });

        it('Row is selected then  $scope.selectedRow is set', function () {
            scope.setClickedRow(1, 2);
            expect(scope.selectedRow).toEqual(1);
        });
    });
});