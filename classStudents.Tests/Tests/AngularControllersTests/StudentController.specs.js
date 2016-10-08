var data =
{
    Student:
        {
        success: true,
        data:
            {
                Id: 1, firstname: "Mo", surname: "Jafat", Age: 30, GPA: 3.0, schoolClassId: 1,
                schoolClass: { Id: 1, className: "Biology", Location: "Melbourne", teacherName: "Blah Blah", Students: [] }
            }
        },
    Students:
        {
        success: true,
        data:
                [
                {
                    Id: 1, firstname: "blah1", surname: "blah1", Age: 20, GPA: 3.5, schoolClassId: 1,
                    schoolClass: { Id: 1, className: "Biology", Location: "Melbourne", teacherName: "Blah Blah", Students: [] }
                }, {
                    Id: 2, firstname: "blah2", surname: "blah2", Age: 18, GPA: 3.2, schoolClassId: 1,
                    schoolClass: { Id: 1, className: "Biology", Location: "Melbourne", teacherName: "Blah Blah", Students: [] }
                }
                ]
                    
        }
};
describe('', function () {
    var $httpBackend,
     expectedUrl = 'http://localhost:54140/api/Student/',
     httpController, location;

    beforeEach(function () {
        module('schoolClassStudentsApp');
    });

    describe('StudentController Test', function () {
        beforeEach(inject(function ($rootScope, $controller, _$httpBackend_, $location) {
            $httpBackend = _$httpBackend_;
            scope = $rootScope.$new();
            location = $location;
            httpController = $controller('StudentController', {
                '$scope': scope
            });
        }));

        it('$scope.go should change location to the path parameter', function () {
            spyOn(location, 'path');
            scope.go('/new/path');
            expect(location.path).toHaveBeenCalledWith('/new/path');
        });

        it('Title set to "Students"', function () {
            expect(httpController).toBeDefined();
            expect(scope.Title).toEqual('Students');
        });

        it('$scope.init should load list of students and initialise selectedStudent', function () {
            $httpBackend.expectGET('http://localhost:54140/api/SchoolClass/getSchoolClassName/undefined').respond(200, "Biology");
            $httpBackend.expectGET(expectedUrl + 'getSchoolClassStudents?schoolclassid=undefined').respond(200, data.Students);
            scope.init();
            $httpBackend.flush();
            expect(httpController).toBeDefined();
            expect(scope.students).toBeDefined();
            expect(scope.students.data.length).toEqual(2);
            expect(scope.selectedStudent).toBeDefined();
            expect(scope.schoolClassName).toBeDefined();
            expect(scope.schoolClassName).toEqual("Biology");
            expect(scope.errorMessage).toBeDefined();
        });

        it('$scope.loadEdit should call "getStudentById" and set selectedStudent"', function () {
            $httpBackend.expectGET(expectedUrl + 'getStudentById/undefined').respond(200, data.Student);
            scope.loadEdit();
            $httpBackend.flush();
            expect(httpController).toBeDefined();
            expect(scope.selectedStudent).toBeDefined();
            expect(scope.selectedStudent.data.Id).toEqual(1);
        });

        it('$scope.updateStudent calls "updateStudent" then return student object.', function () {
            $httpBackend.expectPOST(expectedUrl + 'updateStudent').respond(200, data.Student);
            scope.updateStudent();
            $httpBackend.flush();
            expect(httpController).toBeDefined();
            expect(scope.selectedStudent.Id).toEqual(1);
        });

        it('$scope.addStudent calls "createNewStudent" then return student object.', function () {
            $httpBackend.expectPOST(expectedUrl + 'createNewStudent/').respond(200, data.Student);
            scope.schoolClassId = 1;
            scope.addStudent(data.Student);
            $httpBackend.flush();
            expect(httpController).toBeDefined();
            expect(scope.selectedStudent.Id).toEqual(1);
        });

        it('$scope.deleteStudent calls "deleteStudent" then return Student object.', function () {
            $httpBackend.expectPOST(expectedUrl + 'deleteStudent/undefined').respond(200, data.Student);
            scope.deleteStudent();
            $httpBackend.flush();
            expect(httpController).toBeDefined();
        });

        it('$scope.loadDelete calls "getStudentById" then return Student object.', function () {
            $httpBackend.expectGET(expectedUrl + 'getStudentById/undefined').respond(200, data.Student);
            scope.loadDelete();
            $httpBackend.flush();
            expect(httpController).toBeDefined();
            expect(scope.selectedStudent).toBeDefined();
            expect(scope.selectedStudent.data.Id).toEqual(1);
        });

        it('$scope.loadAdd returns an empty Student object and set selectedStudent.', function () {
            scope.loadAdd();
            expect(httpController).toBeDefined();
            expect(scope.selectedStudent).toBeDefined();
            expect(scope.selectedStudent.Id).toEqual(0);
        });
    });
});