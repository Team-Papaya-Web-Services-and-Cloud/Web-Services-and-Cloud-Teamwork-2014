'use strict';

votterApp.factory('votterData', function ($resource, $q, $http) {
    var url = 'http://ratemygirlfriendservices.apphb.com/';

    return {
        register: function (email, password) {
            var deferred = $q.defer();

            $http({
                method: 'POST',
                url: url + 'Account/Register',
                data: {
                    Email: email,
                    Password: password,
                    ConfirmPassword: password
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