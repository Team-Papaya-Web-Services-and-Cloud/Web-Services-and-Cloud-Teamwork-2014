'use strict';

votterApp.controller('RegistrationController',
    function RegistrationController($scope, $resource, votterData) {
        $scope.signUp = function() {
            alert($scope.email);
            alert($scope.password);
            alert($scope.confirmPassword);
        }
    }
);
