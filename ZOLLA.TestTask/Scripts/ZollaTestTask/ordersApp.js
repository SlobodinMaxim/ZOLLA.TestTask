; (function () {
    const ordersApp = angular.module(`ordersApp`, []);

    ordersApp.filter(`range`, () => {
        return function (input, total) {
            total = parseInt(total);

            for (let index = 0; index < total; index++) {
                input.push(index);
            }

            return input;
        };
    });

    ordersApp.service(`orderService`, function ($http) {
        this.getOrders = function (clientId, page) {
            return $http({
                method: `get`,
                params: {
                    clientId,
                    page
                },
                url: `/Order/Get`,
            });
        };
    });

    ordersApp.controller(`orderController`, function ($scope, $document, orderService) {
        const initialize = function (page) {
            const clientId = jDocument.find(`#client`).data(`clientId`);
            orderService.getOrders(clientId, page).then(responseData => {
                $scope.orders = responseData.data.orders;
                $scope.pageInfo = responseData.data.pageInfo;
            });
        }

        const jDocument = $($document[0]);
        jDocument.on(`shown.bs.modal`, () => initialize(1));

        $scope.setPage = function (page) {
            initialize(page);
        };
    });
})();