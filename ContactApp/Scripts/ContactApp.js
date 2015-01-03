var ContactApp = angular.module('ContactApp', ['ngRoute']);

//ContactApp.controller('IndexController', IndexController);
ContactApp.controller('ContactList', ContactList);
ContactApp.controller('NewContact', NewContact);

var configFunction = function ($routeProvider, $locationProvider) {

    $locationProvider.html5Mode(true);

    $routeProvider.
        when('/Kontakti', {
            templateUrl: 'Routes/ContactList'
        })
        .when('/Kontakt/:id', {
            templateUrl: '/Routes/NewContact'
        });

    
}
configFunction.$inject = ['$routeProvider', '$locationProvider'];

ContactApp.config(configFunction);