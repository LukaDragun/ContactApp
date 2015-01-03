var ContactList = function ($scope, $http) {

    $scope.options = [
        "Pretraga po IMENU",
        "Pretraga po PREZIMENU",
        "Pretraga po TAGU"];
    
    $scope.readfromserver = function () {
        $scope.contacts = [];
        $http({
            url: "/Home/Read",
            method: "POST",
        }).success(function (data, status, headers, config) {
            data.forEach(function (entry) {
                var j = JSON.parse(JSON.stringify(entry));
                $scope.contacts.push(j);
            })
        }).error(function (data, status, headers, config) {
            alert("failz");
        });
    }

    $scope.search = function () {
        $scope.contacts = [];
        if ($scope.text == null || $scope.text == "")
            $scope.readfromserver();
        else
        {

        $http({
            url: "/Home/Search",
            data: { "action": $scope.select , "searchtext": $scope.text },
            method: "POST",
        }).success(function (data, status, headers, config) {
            data.forEach(function (entry) {
                var j = JSON.parse(JSON.stringify(entry));
                $scope.contacts.push(j);
            })
        }).error(function (data, status, headers, config) {
            alert("failz");
        });

        }
    }

    $scope.delete = function (index) {
        $http({
            url: "/Home/Delete/" + index,
            method: "POST",
        }).success(function (data, status, headers, config) {
            alert("Contact deleted successfully");
            $scope.contacts = [];
            $scope.readfromserver();
        }).error(function (data, status, headers, config) {
            alert(status);
        });
    }

    $scope.readfromserver();
 
}

ContactList.$inject = ['$scope', '$http'];