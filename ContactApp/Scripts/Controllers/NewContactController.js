var NewContact = function ($scope, $http, $routeParams,$location) {
    $scope.change = 0;
    $scope.user = $routeParams.id;

    $scope.resetfields = function () {
        $scope.new = {
            id: '0',
            name: '',
            surname: '',
            organization: '',
            adress: '',
            emails: [],
            phones: [],
            tags: []
        }
    }



    $scope.addmail = function () {
        $scope.new.emails.push({ val: '' })
    }

    $scope.removemail = function (index) {
        $scope.new.emails.splice(index, 1);
    }

    $scope.addphone = function () {
        $scope.new.phones.push({ val: '' })
    }

    $scope.removephone = function (index) {
        $scope.new.phones.splice(index, 1);
    }

    $scope.addtag = function () {
        $scope.new.tags.push({ val : '' })
    }

    $scope.removetag = function (index) {
        $scope.new.tags.splice(index, 1);
    }

    $scope.delete = function () {
        $http({
            url: "/Home/Delete/" + $routeParams.id,
            method: "POST",
        }).success(function (data, status, headers, config) {
            alert("Contact deleted successfully");
            $location.path('');
        }).error(function (data, status, headers, config) {
            alert(status);
        });
    }

    $scope.sendtoserver = function () {
        $scope.change--;
        $http({
            url: "/Home/Insert",
            method: "POST",
            data: $scope.new
        }).success(function (data, status, headers, config) {
            alert("Contact inserted successfully");
            $location.path('');
        }).error(function (data, status, headers, config) {
            alert(status);
        });
    }

    if ($routeParams.id > 0) {
        $http({
            url: "/Home/Read/" + $routeParams.id,
            method: "POST",
        }).success(function (data, status, headers, config) {
            var i=0;    
            data.forEach(function (entry) {
                var j = JSON.parse(JSON.stringify(entry));
                $scope.new = entry;
                i++;
            });
            if(i==0)
                $location.path('/Kontakt/0');
        }).error(function (data, status, headers, config) {
            alert(status);
        });
    }
    else
        $scope.resetfields();




}

NewContact.$inject = ['$scope', '$http', '$routeParams','$location'];