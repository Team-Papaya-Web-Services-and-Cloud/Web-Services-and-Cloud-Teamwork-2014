'use strict';

votterApp.factory('votterData', function ($resource, $q, $http) {
    var url = 'http://ratemygirlfriendservices.apphb.com/';

    return {
        register: function (email, password) {
            var deferred = $q.defer();

            $http({
                method: 'POST',
                url: url + 'api/Account/Register',
                data: {
                    Email: email,
                    Password: password,
                    ConfirmPassword: password
                }
            })
            .success(function (data, status, headers, config) {
                deferred.resolve(data);
            })

            return deferred.promise;
        },
        login: function (email, password) {
            var deferred = $q.defer();

            $http({
                method: 'POST',
                url: url + 'Token',
                data: {
                    "grant_type": "password",
                    Email: email,
                    Username: email,
                    Password: password,
                }
            })
            .success(function (data, status, headers, config) {
                console.log(data)
                deferred.resolve(data);
            })

            return deferred.promise;
        }
    }
});