var schoolClassStudentsApp = angular.module('schoolClassStudentsApp', ['ngResource', 'ngRoute']);
// configure our routes
schoolClassStudentsApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider

        // route for the home page
        .when('/', {
            templateUrl: 'views/SchoolClasses/List.html',
            controller: 'SchoolClassController'
        })

        // route for Edit
        .when('/:id/Edit', {
            templateUrl: 'views/SchoolClasses/Edit.html',
            controller: 'SchoolClassController'
        })

        // route for Delete
        .when('/:id/Delete', {
            templateUrl: 'views/SchoolClasses/Delete.html',
            controller: 'SchoolClassController'
        })

        // route for Add
        .when('/Add', {
            templateUrl: 'views/SchoolClasses/Add.html',
            controller: 'SchoolClassController'
        })

        // route for students 
        .when('/students/:id', {
            templateUrl: 'views/Students/List.html',
            controller: 'StudentController'
        })

        // route for Edit
        .when('/students/:id/Edit', {
            templateUrl: 'views/Students/Edit.html',
            controller: 'StudentController'
        })

        // route for Delete
        .when('/students/:id/Delete', {
            templateUrl: 'views/Students/Delete.html',
            controller: 'StudentController'
        })

        // route for Add
        .when('/students/:id/Add', {
            templateUrl: 'views/Students/Add.html',
            controller: 'StudentController'
        });
}
]);