'use strict';

votterApp.directive('navigationBar', function() {
    return {
        restrict: 'A',
        templateUrl: 'templates/directives/navigation-bar.html',
        replace: true,
        scope: false
    }
});