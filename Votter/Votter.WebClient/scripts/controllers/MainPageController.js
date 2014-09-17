'use strict';

votterApp.controller('MainPageController',
    function MainPageController($scope, $rootScope, author, copyright) {
        $scope.author = author;
        $scope.copyright = copyright;
    }
);