'use strict';

votterApp.controller('RegistrationController',
    function RegistrationController($scope, $resource, votterData) {
        $scope.signUp = function () {
            votterData.register($scope.username, $scope.password)
            alert($scope.email);
            alert($scope.password);
            alert($scope.confirmPassword);
        }
    }
);
