'use strict';

votterApp.controller('LoginController',
    function LoginController($scope, $resource, votterData) {
        $scope.signIn = function() {
            alert($scope.email);
            alert($scope.password);
        }
    }
);
