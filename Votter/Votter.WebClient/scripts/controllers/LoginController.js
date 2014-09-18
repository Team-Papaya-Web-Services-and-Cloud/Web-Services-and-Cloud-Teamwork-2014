'use strict';

votterApp.controller('LoginController',
    function LoginController($scope, $resource, votterData) {
        $scope.signIn = function() {
            votterData.login($scope.email, $scope.password);
        }
    }
);
