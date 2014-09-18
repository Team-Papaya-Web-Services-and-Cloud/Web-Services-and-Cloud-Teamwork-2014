'use strict';

votterApp.controller('RegistrationController',
    function RegistrationController($scope, $resource, votterData) {
        $scope.signUp = function () {
            votterData.register($scope.email, $scope.password);
        }
    }
);
