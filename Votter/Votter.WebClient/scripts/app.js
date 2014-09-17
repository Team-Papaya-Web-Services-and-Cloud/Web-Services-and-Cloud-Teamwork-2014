'use strict';

var votterApp = angular.module('votterApp', ['ngResource', 'ngRoute'])
    .config(function ($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'templates/home.html'
            })
            .otherwise({redirectTo: '/'});
    })
    .constant('author', 'Papaya Team')
    .constant('copyright', 'Telerik Academy');