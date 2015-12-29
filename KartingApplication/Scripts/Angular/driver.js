
// <reference path="../angular.js" />

//var app = angular.module('kartingApp', ['ngResource']);

var app = angular.module('kartingApp', ['ui.grid', 'ui.grid.selection'])

    app.controller('DriverController', ['$scope', 'driverService', function ($scope, driverService) {

        $scope.driverGrid = {
            enableSorting: true,
            enableFiltering: true,
            columnDefs: [
              { field: 'FirstName' },
              { field: 'LastName' },
              { field: 'BirthDate' }
            ],
            enableRowSelection: true,
            enableRowHeaderSelection: false,
            multiSelect: false
        };

        $scope.editMode = false;

        $scope.driverGrid.onRegisterApi = function (gridApi) {
            gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                $scope.selectedDriver = gridApi.grid.selection.lastSelectedRow.entity;
            });
        };

        $scope.driverGrid.data = {
            FirstName: "",
            LastName: "",
            ID: "",
            BirthDate: "",
            Picture: ""
        };

        $scope.selectedDriver = $scope.driverGrid.data;
 
        driverService.getDrivers($scope);

    }]);

    app.controller('EditDriverController', ['$scope', 'DriverController', function ($scope, DriverController) {
    //app.controller('EditDriverController', ['$scope', function ($scope) {
        $scope.driver = DriverController.selectedDriver;

        //$scope.driver = { FirstName: "Jefke", LastName: "Pieters" };

      
    }]);

    app.service('driverService', ['$http', function ($http) {
    this.getDrivers = function ($scope) {
        return $http({
            method: "GET",
            url: "../api/driver",
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.driverGrid.data = data;
            console.log(data);
        }).error(function (data) {
            console.log(data);
        });;
    }}]);
