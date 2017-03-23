function getRootUrl() {
    return '';
}

var systemSearchApp = angular.module("systemSearchApp", ['ui.bootstrap']);
systemSearchApp.filter('PageStart', function () {
    return function (input, start) {
        if (input) {
            start = +start;
            return input.slice(start);
        }
        return [];
    };
});

systemSearchApp.filter('filterHtmlDecode', [
    '$sce', function ($sce) {
        var div = document.createElement('div');
        return function (text) {
            div.innerHTML = text;
            return $sce.trustAsHtml(div.textContent);
        };
    }
]);
systemSearchApp.controller("userController", function ($scope, $http, $timeout) {
    getUserSearch();

    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this user?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Admin/ChangeStatusUser",
                params: {
                    id: JSON.stringify(item.UserId)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getUserSearch();
                }
            });
        } else {
            return false;
        }
    };
    $scope.resetPassword = function (item) {
        var r = confirm("Do you want to reset password for this user?");
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Admin/ResetPassword",
                params: {
                    id: JSON.stringify(item.UserId)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getUserSearch();
                }
            });
        } else {
            return false;
        }
    };
    $scope.assignRole = function (item) {
        var roleIds = getRoleList(item.UserId);
        var response = $http({
            method: "post",
            url: getRootUrl() + "/Admin/AssignRole",
            params: {
                id: JSON.stringify(item.UserId),
                roleIds: JSON.stringify(roleIds)
            }
        });
        response.then(function (msg) {
            if (redirectToLogin(msg)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                alert(msg.data.toString());
                var popupid = "#assign_role_popup_" +item.UserId;
                $(popupid).dialog("close");
                getUserSearch();
                return;
            }
        });
    };

    $scope.isChecked = function (item, roleId) {
        if (item.RoleList.indexOf(roleId) > -1) {
            return true;
        }
        return false;
    };

    $scope.userSearch = function () {
        getUserSearch();
    };

    function getUserSearch() {
        var model = {
            UserName: $("#id-user-name").val(),
            Status: $("#status").val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Admin/GetListUser",
            params: {
                un: JSON.stringify(model.UserName),
                status: JSON.stringify(model.Status),
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };

    }

    function getRoleList(userId) {
        var roleList = "";
        var idCheckBox = "#checkAllBoxesRole_" + userId + " input:checked";
        $(idCheckBox).each(function () {
            roleList = roleList + $(this).val() + ",";
        });
        return roleList;

    }
});
systemSearchApp.controller("roleController", function ($scope, $http, $timeout) {
    getRoleList();
    $scope.roleSearch = function () {
        getRoleList();
    };

    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this role?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Role/ChangeStatus",
                params: {
                    id: JSON.stringify(item.RoleId)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getRoleList();
                }
            });
        } else {
            return false;
        }
    };

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "id-role-name" || e.target.id === "status-code")) {
            getRoleList();
        }
    });
    function getRoleList() {
        var model = {
            RoleName: $('#id-role-name').val(),
            RoleStatus: $('#status-code').val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Role/GetListRole",
            params: {
                roleName: JSON.stringify(model.RoleName),
                roleStatus: JSON.stringify(model.RoleStatus),
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});
systemSearchApp.controller("newController", function ($scope, $http, $timeout) {
    getListNew();

    $scope.newSearch = function () {
        getListNew();
    };
    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this News?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/New/ChangeStatus",
                params: {
                    id: JSON.stringify(item.ID)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getListNew();
                }
            });
        } else {
            return false;
        }
    };
    $scope.deleteNew = function (item) {
        var confirmMesg = "Do you want to Delete this New?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/New/Delete",
                params: {
                    id: JSON.stringify(item.ID)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getListNew();
                }
            });
        } else {
            return false;
        }
    };

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "status")) {
            getListNew();
        }
    });

    function getListNew() {
        var model = {
            Status: $('#status').val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/New/GetListNew",
            params: {
                status: JSON.stringify(model.Status),
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10;
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});
systemSearchApp.controller("supController", function ($scope, $http, $timeout) {
    getListSup();
    $scope.supportSearch = function () {
        getListSup();
    };
    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this Support?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Support/ChangeStatus",
                params: {
                    id: JSON.stringify(item.ID)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getListSup();
                }
            });
        } else {
            return false;
        }
    };
    $scope.deleteSupport = function (item) {
        var confirmMesg = "Do you want to Delete this Support?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Support/Delete",
                params: {
                    id: JSON.stringify(item.ID)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getListSup();
                }
            });
        } else {
            return false;
        }
    };
    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "status")) {
            getListSup();
        }
    });

    function getListSup() {
        var model = {
            Status: $('#status').val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Support/GetList",
            params: {
                status: JSON.stringify(model.Status),
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10;
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});
systemSearchApp.controller("menuController", function ($scope, $http, $timeout) {
    getListMenu();

    $scope.configSearch = function () {
        getListMenu();
    };
    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this Menu?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Menu/ChangeStatus",
                params: {
                    id: JSON.stringify(item.Id)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getListMenu();
                }
            });
        } else {
            return false;
        }
    };



    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "status")) {
            getListMenu();
        }
    });

    function getListMenu() {
        var model = {
            Status: $('#status').val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Menu/GetListMenu",
            params: {
                status: JSON.stringify(model.Status),
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10;
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});
systemSearchApp.controller("answerController", function ($scope, $http, $timeout) {
    getAnswerList();

    $scope.answerSearch = function () {
        getAnswerList();
    };

    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this Answer?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Answer/ChangeStatus",
                params: {
                    id: JSON.stringify(item.AS_ID)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getAnswerList();
                }
            });
        } else {
            return false;
        }
    };
    $scope.deleteAnswer = function (item) {
        var confirmMesg = "Do you want to Delete this Answer?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Answer/Delete",
                params: {
                    id: JSON.stringify(item.AS_ID)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getAnswerList();
                }
            });
        } else {
            return false;
        }
    };
    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "status")) {
            getAnswerList();
        }
    });

    function getAnswerList() {
        debugger
        var model = {
            Status: $('#status').val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Answer/GetListAnswer",
            params: {
                status: JSON.stringify(model.Status),
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});
systemSearchApp.controller("currencyController", function ($scope, $http, $timeout) {
    getCurrencyList();

    $scope.currencySearch = function () {
        getCurrencyList();
    };

    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this currency?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Currency/ChangeStatusCurrency",
                params: {
                    currencyCode: JSON.stringify(item.CurrencyCode)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getCurrencyList();
                }
            });
        } else {
            return false;
        }
    };

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "currency-code" || e.target.id === "currency-name" || e.target.id === "status")) {
            getCurrencyList();
        }
    });

    function getCurrencyList() {
        var model = {
            CurrencyCode: $('#currency-code').val(),
            CurrencyName: $('#currency-name').val(),
            Status: $('#status').val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Currency/GetListCurrency",
            params: {
                currencyCode: JSON.stringify(model.CurrencyCode),
                currencyName: JSON.stringify(model.CurrencyName),
                status: JSON.stringify(model.Status),
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});
systemSearchApp.controller("configController", function ($scope, $http, $timeout) {
    getListConfig();

    $scope.configSearch = function () {
        getListConfig();
    };
    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this Config?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Config/ChangeStatus",
                params: {
                    code: JSON.stringify(item.Code)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getListConfig();
                }
            });
        } else {
            return false;
        }
    };



    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "code" || e.target.id === "status")) {
            getListConfig();
        }
    });

    function getListConfig() {
        var model = {
            Code: $('#code').val(),
            Status: $('#status').val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Config/GetList",
            params: {
                code: JSON.stringify(model.Code),
                status: JSON.stringify(model.Status),
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10;
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});
systemSearchApp.controller("blockController", function ($scope, $http, $timeout) {
    getListBlock();

    $scope.blockSearch = function () {
        getListBlock();
    };
    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this Block?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Block/ChangeStatus",
                params: {
                    id: JSON.stringify(item.ID)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getListBlock();
                }
            });
        } else {
            return false;
        }
    };

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "tab" || e.target.id === "status")) {
            getListBlock();
        }
    });

    function getListBlock() {
        var model = {
            Tab: $('#tab').val(),
            Status: $('#status').val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Block/GetList",
            params: {
                tab: JSON.stringify(model.Tab),
                status: JSON.stringify(model.Status),
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10;
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});
systemSearchApp.controller("imageController", function ($scope, $http, $timeout) {
    getListImage();

    $scope.imageSearch = function () {
        getListImage();
    };
    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this Image?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Image/ChangeStatus",
                params: {
                    id: JSON.stringify(item.ID)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getListImage();
                }
            });
        } else {
            return false;
        }
    };


    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "type" || e.target.id === "status")) {
            getListImage();
        }
    });

    function getListImage() {
        var model = {
            Type: $('#type').val(),
            Status: $('#status').val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Image/GetList",
            params: {
                type: JSON.stringify(model.Type),
                status: JSON.stringify(model.Status),
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10;
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});
systemSearchApp.controller("cutomertestController", function ($scope, $http, $timeout) {
    getListCustomer();

    $scope.configSearch = function () {
        getListCustomer();
    };
    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this Customer?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Customer/ChangeStatus",
                params: {
                    id: JSON.stringify(item.ID)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getListCustomer();
                }
            });
        } else {
            return false;
        }
    };

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "status")) {
            getListCustomer();
        }
    });

    function getListCustomer() {
        var model = {
            Name: $('#name').val(),// cái này là tên từ model(Name) với từ id bên view(name) Ok
            Status: $('#status').val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Customer/GetListCustomer", // cái này gọi về action ở controller
            params: {
                name: JSON.stringify(model.Name),
                status: JSON.stringify(model.Status),
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10;
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});
systemSearchApp.controller("categoryController", function ($scope, $http, $timeout) {
    getListCategory();

    $scope.configSearch = function () {
        getListCategory();
    };
    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this Category?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Category/ChangeStatus",
                params: {
                    id: JSON.stringify(item.ID)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getListCategory();
                }
            });
        } else {
            return false;
        }
    };


    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "status")) {
            getListCategory();
        }
    });

    function getListCategory() {
        var model = {

            Status: $('#status').val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Category/GetListCategory",
            params: {
                status: JSON.stringify(model.Status),
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10;
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});
systemSearchApp.controller("functionController", function ($scope, $http, $timeout) {
    getListFunction();

    $scope.functionSearch = function () {
        getListFunction();
    };
    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this Category?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Category/ChangeStatus",
                params: {
                    id: JSON.stringify(item.ID)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getListFunction();
                }
            });
        } else {
            return false;
        }
    };
    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "code")) {
            getListFunction();
        }
    });

    function getListFunction() {
        var model = {
            Code: $('#code').val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Function/GetList",
            params: {
                code: JSON.stringify(model.Code),
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10;
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});
// cái này xử lý kiểu khác tạo mới luôn đi

systemSearchApp.controller('customerController', function ($scope, $http) {
    var idCustomer = 0;
    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "account" || e.target.id === "cif" || e.target.id === "id-passport")) {
            searchCustomer();
        }
    });
    $scope.searchCustomer = function () {
        searchCustomer();
    };
    $scope.createCustomer = function () {
        if ($('#notify-result').hasClass('red')) {
            $('#notify-result').removeClass('red');
        }
        if ($('#cus-staff-code').val() === "" || $('#cus-email').val() === "") {
            alert("Staff code và Email là các trường bắt buộc.");
            return false;
        }
        var model = {
            CifNo: document.getElementById('cus-cif').innerHTML,
            IdOrPassport: document.getElementById('cus-idpassport').innerHTML,
            CustomerName: document.getElementById('cus-name').innerHTML,
            CifBranch: $('#branch-cif').val(),
            CustomerType: $('#cus-type').val(),
            BirthDay: $('#cus-birth').val(),
            Resident: $('#cus-resident').val(),
            Email: $('#cus-email').val(),
            Address: $('#cus-address').val(),
            Telephone: $('#tel-number').val(),
            AccountNo: $('#account-no').val(),
            TelephoneOTP: $('#tel-number-otp').val(),
            Sex: $('#cus-sex').val(),
            StaffCode: $('#cus-staff-code').val(),
            SegmentationCustomer: $('#cus-segment').val(),
            CreatedUser: $('#user-created').val(),
            BranchCodeCreatedUser: $('#branch-code').val(),
            PosCodeCreatedUser: $('#pos-code').val(),
            PackageCode: $('#pkg-code').val()
        };
        $http.post('/Customer/CreateCustomer', model)
        .then(function (data) {
            if (redirecToLoginByTypeOf(data.data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {

                //alert(data.data.data);
                if (data.data.id !== 0) {
                    document.getElementById('notify-result').innerHTML = data.data.data;
                    idCustomer = data.data.id;
                    if (!$('#save-customer').hasClass('hidden')) {
                        $('#save-customer').addClass('hidden');
                        $('#approval-customer').removeClass('hidden');
                    }
                } else {
                    $('#notify-result').addClass('red');
                    document.getElementById('notify-result').innerHTML = data.data.data;
                }
            }
        });
        return false;
    };
    $scope.sendApprovalCustomer = function () {
        $http({
            method: "post",
            url: getRootUrl() + "/Customer/SendApprovalCustomer",
            params: {
                id: idCustomer
            }
        }).then(function (data) {
            if (redirecToLoginByTypeOf(data.data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                if (data.data.data == "true") {
                    alert("Gửi phê duyệt thành công");
                    window.location.href = getRootUrl() + "/Customer/Index?f=4&c=100";
                } else {
                    $('#div-mesg').addClass('red');
                    document.getElementById('notify-result').innerHTML = data.data.data;
                }
            }
        });
    };
    $scope.cancelCustomer = function () {
        window.location.href = getRootUrl() + "/Customer/EditCustomer/" + idCustomer + "?f=4&c=178";
    };
    function searchCustomer() {
        document.getElementById('notify-customer').innerHTML = "";
        //hide info
        if ($('#info').hasClass('show')) {
            $('#info').removeClass('show').addClass('hidden');
        }
        $('#progess').modal("show");
        $http({
            method: "post",
            url: getRootUrl() + "/Customer/SearchCustomer",
            params: {
                cif: JSON.stringify($('#cif').val()),
                id: JSON.stringify($('#id-passport').val()),
                account: JSON.stringify($('#account').val())
            }
        }).then(function (msg) {
            $('#progess').modal("hide");
            if (msg.data === "0") {
                document.getElementById('notify-customer').innerHTML = "Số chứng minh thư này đã được đăng ký cho cif khác. Vui lòng tìm kiếm theo số cif.";
                return false;
            }
            if (msg.data === "1") {
                document.getElementById('notify-customer').innerHTML = "Khách hàng đã đăng ký dịch vụ.";
                return false;
            }
            if (msg.data === "2") {
                document.getElementById('notify-customer').innerHTML = "Số tài khoản trên là tài khoản đồng chủ sở hữu. Đề nghị GDV tìm kiếm theo CIF để đảm bảo thông tin khách hàng.";
                return false;
            }
            if (msg.data === "3") {
                document.getElementById('notify-customer').innerHTML = "Khách hàng sở hữu số tài khoản trên đã đăng ký dịch vụ theo số tài khoản khác. Đề nghị GDV tìm kiếm theo CIF để đảm bảo thông tin khách hàng.";
                return false;
            }
            else if (msg.data === "") {
                document.getElementById('notify-customer').innerHTML = "Không tìm thấy thông tin khách hàng. Khách hàng chưa đăng ký tài khoản tại BIDV.";
                return false;
            }
            else if (redirecToLoginByTypeOf(msg.data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            }
            else {
                var model = msg.data;
                if (model.Count > 1) {
                    document.getElementById('notify-customer').innerHTML = "Có tổng số " + model.Count + " kết quả trả về. Giao dịch viên chú ý kiểm tra thông tin và tìm kiếm theo số cif từ SIBS để đảm bảo chính xác thông tin khách hàng đăng ký.";
                }
                document.getElementById('cus-cif').innerHTML = model.CifNo;
                document.getElementById('cus-idpassport').innerHTML = model.IdOrPassport;
                document.getElementById('cus-name').innerHTML = model.CustomerName;
                if (model.Sex !== "" && model.Sex !== null) {
                    $('#cus-sex').val(model.Sex);
                }
                if (model.CifBranch !== "" && model.CifBranch !== null) {
                    $('#branch-cif').val(model.CifBranch);
                }
                if (model.Resident !== "" && model.Resident !== null) {
                    $('#cus-resident').val(model.Resident);

                }
                $('#pos-code').val(model.PosCodeCreatedUser);
                $('#user-created').val(model.CreatedUser);
                $('#cus-birth').val(model.BirthDay);
                $('#cus-email').val(model.Email);
                $('#cus-address').val(model.Address);
                $('#branch-code').val(model.BranchCodeCreatedUser);
                $('#cus-type').val(model.CustomerType);
                $('#cus-staff-code').val(model.StaffCode);
                setOption(model.AccountNoList, "account-no");
                setOption(model.Mobiles, "tel-number");
                setOption(model.Mobiles, "tel-number-otp");
                if ($('#info').hasClass('hidden')) {
                    $('#info').removeClass('hidden').addClass('show');
                }
                if ($('#save-customer').hasClass('hidden')) {
                    $('#save-customer').removeClass('hidden');
                }
                if (!$('#approval-customer').hasClass('hidden')) {
                    $('#approval-customer').addClass('hidden');
                }
            }
        });
    }
});

systemSearchApp.controller('confirmCusController', function ($scope, $http, $timeout) {
    getCustomerConfirmListSearch();
    $scope.customerSearch = function () {
        getCustomerConfirmListSearch();
    };
    $scope.customerApprove = function () {
        var models = [];
        angular.forEach($scope.filtered, function (item) {
            if (item.Selected == true) {
                models.push(item);
            }
        });
        if (models.length <= 0) {
            alert("Bạn phải lựa chọn ít nhất 1 bản ghi để duyệt.");
            return false;
        }
        $http.post('/Customer/ApprovalCustomer', models)
            .then(function (data) {
                if (redirectToLogin(data)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    getCustomerConfirmListSearch();
                    $scope.selectedAll = false;
                }
            });


    };
    $scope.customerReject = function () {
        var models = [];
        angular.forEach($scope.filtered, function (item) {
            if (item.Selected == true) {
                models.push(item);
            }
        });
        if (models.length <= 0) {
            alert("Bạn phải lựa chọn ít nhất 1 bản ghi để từ chối duyệt.");
            return false;
        }
        $http.post('/Customer/RejectCustomer', models)
            .then(function (data) {
                if (redirectToLogin(data)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    getCustomerConfirmListSearch();
                    $scope.selectedAll = false;
                }
            });

    };
    $scope.checkAll = function () {
        if ($scope.selectedAll) {
            $scope.selectedAll = true;
        } else {
            $scope.selectedAll = false;
        }
        angular.forEach($scope.filtered, function (item) {
            item.Selected = $scope.selectedAll;
        });
    };


    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "user-name" || e.target.id === "status" || e.target.id === "id-passport" ||
        e.target.id === "cif" || e.target.id === "cus-name" || e.target.id === "fromDate" || e.target.id === "toDate")) {
            getCustomerConfirmListSearch();
        }
    });

    function getCustomerConfirmListSearch() {
        var model = {
            UserName: $("#user-name").val(),
            Status: $("#status").val(),
            IdPassport: $("#id-passport").val(),
            Cif: $("#cif").val(),
            CusName: $('#cus-name').val(),
            FromDate: $("#fromDate").val(),
            ToDate: $("#toDate").val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Customer/GetListCutomerConfirm",
            params: {
                cusName: JSON.stringify(model.CusName),
                mobile: JSON.stringify(model.UserName),
                listStatus: JSON.stringify(model.Status),
                idPassport: JSON.stringify(model.IdPassport),
                cif: JSON.stringify(model.Cif),
                fd: JSON.stringify(model.FromDate),
                td: JSON.stringify(model.ToDate)
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});

systemSearchApp.controller('updateCusController', function ($scope, $http) {
    $scope.currentPage = 1;
    $scope.PerPageItems = 10;
    getLisCusUpdate();
    $scope.cusUpdateSearch = function () {
        getLisCusUpdate();
    };
    $scope.pageChanged = function () {
        getLisCusUpdate();
    };
    $scope.changeStatus = function (item) {
        var confirmMesg = "Bạn muốn ngưng kích hoạt dịch vụ?";
        if (item.StatusId == 'A') {
            confirmMesg = "Bạn muốn hủy dịch vụ?";
        }
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Customer/LockOrUnlock",
                params: {
                    id: JSON.stringify(item.Id),
                    status: JSON.stringify("A")
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getLisCusUpdate();
                }
            });
        } else {
            return false;
        }
    }
    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "account-no" || e.target.id === "id-passport" ||
        e.target.id === "cif" || e.target.id === "user-name")) {
            getLisCusUpdate();
        }
    });

    function getLisCusUpdate() {
        var model = {
            Cif: $('#cif').val(),
            AccountNo: $('#account-no').val(),
            IdPassport: $('#id-passport').val(),
            UserName: $('#user-name').val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Customer/GetListUpdateCustomers",
            params: {
                cif: JSON.stringify(model.Cif),
                accountNo: JSON.stringify(model.AccountNo),
                idPassport: JSON.stringify(model.IdPassport),
                userName: JSON.stringify(model.UserName),
                pageIndex: $scope.currentPage,
                pageSize: $scope.PerPageItems
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data.records;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = data.total; //tổng số bản ghi trong danh sách.
                $scope.numberPages = Math.ceil($scope.totalItems / $scope.PerPageItems);
            }
        });
    }
});

systemSearchApp.controller('managerCusController', function ($scope, $http) {
    $scope.currentPage = 1;
    $scope.PerPageItems = 10;
    getCusManagerListSearch();
    $scope.cusManagerSearch = function () {
        getCusManagerListSearch();
    };
    $scope.pageChanged = function () {
        getCusManagerListSearch();
    };
    $scope.changeStatus = function (item) {
        var confirmMesg;
        if (item.StatusId == 3) confirmMesg = "Bạn muốn hủy kích hoạt dịch vụ?";
        else {
            confirmMesg = "Bạn muốn kích hoạt dịch vụ?";
        }
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Customer/LockOrUnlock",
                params: {
                    id: JSON.stringify(item.Id),
                    status: JSON.stringify(item.StatusId),
                    cif: JSON.stringify(item.CifNo),
                    mobile: JSON.stringify(item.MobileOTP)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getCusManagerListSearch();
                }
            });
        } else {
            return false;
        }
    };
    $scope.resetPassword = function (item) {
        var r = confirm("Bạn muốn đặt lại mật khẩu kích hoạt?");
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Customer/ResetPassword",
                params: {
                    id: JSON.stringify(item.Id)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getCusManagerListSearch();
                }
            });
        } else {
            return false;
        }
    };

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "user-name" || e.target.id === "status" || e.target.id === "id-passport" ||
        e.target.id === "cif" || e.target.id === "cus-name" || e.target.id === "fromDate" || e.target.id === "toDate"
        || e.target.id === "branch-code" || e.target.id === "account-no")) {
            getCusManagerListSearch();
        }
    });

    function getCusManagerListSearch() {
        var model = {
            BranchCode: $('#branch-code').val(),
            AccountNo: $('#account-no').val(),
            UserName: $("#user-name").val(),
            Status: $("#status").val(),
            IdPassport: $("#id-passport").val(),
            Cif: $("#cif").val(),
            CusName: $('#cus-name').val(),
            FromDate: $("#fromDate").val(),
            ToDate: $("#toDate").val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Customer/GetListCutomerManager",
            params: {
                accountNo: JSON.stringify(model.AccountNo),
                cusName: JSON.stringify(model.CusName),
                userName: JSON.stringify(model.UserName),
                status: JSON.stringify(model.Status),
                idPassport: JSON.stringify(model.IdPassport),
                cif: JSON.stringify(model.Cif),
                fd: JSON.stringify(model.FromDate),
                td: JSON.stringify(model.ToDate),
                branchCode: JSON.stringify(model.BranchCode),
                pageIndex: $scope.currentPage,
                pageSize: $scope.PerPageItems
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data.records;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = data.total; //tổng số bản ghi trong danh sách.
                $scope.numberPages = Math.ceil($scope.totalItems / $scope.PerPageItems);
            }
        });
    }
});

systemSearchApp.controller("templateController", function ($scope, $http, $timeout) {
    getList();

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "temp-code" || e.target.id === "temp-vi" || e.target.id === "temp-en")) {
            getList();
        }
    });
    $scope.templateSearch = function () {
        getList();
    };

    function getList() {
        var model = {
            Code: $('#temp-code').val(),
            TempVi: $('#temp-vi').val(),
            TempEn: $('#temp-en').val()
        };
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Template/GetList",
            params: {
                code: JSON.stringify(model.Code),
                temVi: JSON.stringify(model.TempVi),
                temEn: JSON.stringify(model.TempEn)
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});

systemSearchApp.controller("ruleController", function ($scope, $http, $timeout) {
    getList();

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "service-code" || e.target.id === "account-type")) {
            getList();
        }
    });
    $scope.ruleSearch = function () {
        getList();
    };
    $scope.delete = function (item) {
        var confirmMesg = "Do you want delete this rule.";
        var r = confirm(confirmMesg);
        if (r === true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Rule/Delete",
                params: {
                    id: JSON.stringify(item.Id),
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getList();
                }
            });
        } else {
            return false;
        }
    };
    function getList() {
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Rule/GetList",
            params: {
                serviceCode: JSON.stringify($('#service-code').val()),
                accountType: JSON.stringify($('#account-type').val()),
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});

systemSearchApp.controller("productController", function ($scope, $http, $timeout) {
    getList();

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "name-vi" || e.target.id === "name-en" || e.target.id === "status")) {
            getList();
        }
    });
    $scope.productSearch = function () {
        getList();
    };
    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this product?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/Product/ChangeStatus",
                params: {
                    id: JSON.stringify(item.Id),
                    curStatus: JSON.stringify(item.Status)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getList();
                }
            });
        } else {
            return false;
        }
    };
    function getList() {
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/Product/GetList",
            params: {
                status: JSON.stringify($('#status').val()),
                nameVi: JSON.stringify($('#name-vi').val()),
                nameEn: JSON.stringify($('#name-en').val())
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});

systemSearchApp.controller("productDetailController", function ($scope, $http, $timeout) {
    getList();

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "name-vi" || e.target.id === "cat-id" || e.target.id === "name-en" || e.target.id === "status")) {
            getList();
        }
    });
    $scope.productDetailSearch = function () {
        getList();
    };
    $scope.changeStatus = function (item) {
        var confirmMesg = "Do you want to change status this product Detail?";
        var r = confirm(confirmMesg);
        if (r == true) {
            var response = $http({
                method: "post",
                url: getRootUrl() + "/ProductDetail/ChangeStatus",
                params: {
                    id: JSON.stringify(item.Id),
                    curStatus: JSON.stringify(item.Status)
                }
            });
            response.then(function (msg) {
                if (redirectToLogin(msg)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    alert(msg.data.toString());
                    getList();
                }
            });
        } else {
            return false;
        }
    };
    function getList() {
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/ProductDetail/GetList",
            params: {
                catId: JSON.stringify($('#cat-id').val()),
                status: JSON.stringify($('#status').val()),
                nameVi: JSON.stringify($('#name-vi').val()),
                nameEn: JSON.stringify($('#name-en').val())
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = $scope.datas.length; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                $scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.datas.length / $scope.PerPageItems);
            }
        });
        $scope.filter = function () {
            $timeout(function () {
                $scope.filteredItems = $scope.filtered.length;
                $scope.numberPages = Math.ceil($scope.filtered.length / $scope.PerPageItems);
            }, 10);
        };
    }
});

systemSearchApp.controller("hisSmsController", function ($scope, $http) {
    $scope.currentPage = 1;
    $scope.PerPageItems = 10;
    getList();
    $scope.hisSmsSearch = function () {
        getList();
    };
    $scope.pageChanged = function () {
        getList();
    };
    $scope.viewDetail = function (item) {
        $http({
            method: "post",
            url: getRootUrl() + "/HistorySMS/ViewDetail",
            params: {

                id: JSON.stringify(item.Id),
            }
        }).then(function (msg) {

            if (redirectToLogin(msg)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                var model = msg.data;
                document.getElementById('info-mobile').innerHTML = model.MobileNo;
                document.getElementById('info-branch').innerHTML = model.BranchName;
                document.getElementById('info-keyword').innerHTML = model.Keyword;
                document.getElementById('info-kind').innerHTML = model.SmsKind;
                document.getElementById('info-sendfr').innerHTML = model.SendFrom;
                document.getElementById('info-result').innerHTML = model.Result;
                document.getElementById('info-sentdate').innerHTML = model.SentDate;
                document.getElementById('info-recdate').innerHTML = model.ReceivedDate;
                document.getElementById('info-content').innerHTML = model.Content;
                //document.getElementById('info-mobile').innerHTML = model.MobileNo;
                $('#viewDetail').modal("show");
            }
        });
    };

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "branch" || e.target.id === "mobile-no") || e.target.id === "sendfr"
            || e.target.id === "kind" || e.target.id === "keyword" || e.target.id === "result" || e.target.id === "fromDate" || e.target.id === "toDate") {
            getList();
        }
    });
    function getList() {
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/HistorySMS/GetList",
            params: {
                branchCode: JSON.stringify($('#branch').val()),
                mobile: JSON.stringify($('#mobile-no').val()),
                keyword: JSON.stringify($('#keyword').val()),
                kind: JSON.stringify($('#kind').val()),
                sendfr: JSON.stringify($('#sendfr').val()),
                result: JSON.stringify($('#result').val()),
                frd: JSON.stringify($('#fromDate').val()),
                td: JSON.stringify($('#toDate').val()),
                pageIndex: $scope.currentPage,
                pageSize: $scope.PerPageItems
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data.records;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = data.total; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                //$scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.totalItems / $scope.PerPageItems);
            }
        });
    }
});

systemSearchApp.controller("ussdController", function ($scope, $http) {

    $scope.currentPage = 1;
    $scope.PerPageItems = 10;
    getList();

    $scope.UssdSearch = function () {
        getList();
    };
    $scope.pageChanged = function () {
        getList();
    };

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "mobile-no") || e.target.id === "requesttype"
            || e.target.id === "status" || e.target.id === "fromDate" || e.target.id === "toDate") {
            getList();
        }
    });

    function getList() {
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/UssdLog/GetListUssdLog",
            params: {
                mobileNo: JSON.stringify($('#mobile-no').val()),
                requestType: JSON.stringify($('#requesttype').val()),
                status: JSON.stringify($('#status').val()),
                frd: JSON.stringify($('#fromDate').val()),
                td: JSON.stringify($('#toDate').val()),
                pageIndex: $scope.currentPage,
                pageSize: $scope.PerPageItems
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data.records;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = data.total; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                //$scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.totalItems / $scope.PerPageItems);
            }
        });
    }
});

systemSearchApp.controller("productRegistController", function ($scope, $http) {
    $scope.currentPage = 1;
    $scope.PerPageItems = 10;
    getList();
    $scope.productRegistSearch = function () {
        getList();
    };
    $scope.pageChanged = function () {
        getList();
    };
    $scope.productRegistApprove = function () {
        var models = [];
        angular.forEach($scope.datas, function (item) {
            if (item.Selected == true) {
                models.push(item);
            }
        });
        if (models.length <= 0) {
            alert("Bạn phải lựa chọn ít nhất 1 bản ghi để duyệt, và chỉ có thể thực hiện duyệt đối với các bản ghi có trạng thái Inital.");
            return false;
        }
        $http.post('/ProductRegistration/ApprovalProductRegist', models)
            .then(function (data) {
                if (redirectToLogin(data)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    getList();
                    $scope.selectedAll = false;
                }
            });


    };
    $scope.productRegistReject = function () {
        var models = [];
        angular.forEach($scope.datas, function (item) {
            if (item.Selected == true) {
                models.push(item);
            }
        });
        if (models.length <= 0) {
            alert("Bạn phải lựa chọn ít nhất 1 bản ghi để từ chối duyệt, và chỉ có thể thực hiện duyệt đối với các bản ghi có trạng thái Inital.");
            return false;
        }
        $http.post('/ProductRegistration/RejectProductRegist', models)
            .then(function (data) {
                if (redirectToLogin(data)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    getList();
                    $scope.selectedAll = false;
                }
            });

    };
    $scope.checkAll = function () {
        if ($scope.selectedAll) {
            $scope.selectedAll = true;
        } else {
            $scope.selectedAll = false;
        }
        angular.forEach($scope.datas, function (item) {
            if (item.StatusId == '0') {
                item.Selected = $scope.selectedAll;
            }
        });
    };
    $scope.sendApproval = function (item) {
        $http({
            method: "post",
            url: getRootUrl() + "/ProductRegistration/SendApproval",
            params: {
                id: JSON.stringify(item.Id)
            }
        }).then(function (msg) {

            if (redirectToLogin(msg)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                getList();
            }
        });
    };
    $scope.viewDetail = function (item) {
        var models = [];
        if (!$('#confirm').hasClass('hidden')) {
            $('#confirm').addClass('hidden');
        }
        if (item.StatusId == "0") {
            $('#confirm').removeClass('hidden');
        }
        $http({
            method: "post",
            url: getRootUrl() + "/ProductRegistration/ViewDetail",
            params: {

                id: JSON.stringify(item.Id),
            }
        }).then(function (msg) {

            if (redirectToLogin(msg)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                var model = msg.data;
                document.getElementById('info-mobile').innerHTML = model.Mobile;
                document.getElementById('info-branch').innerHTML = model.BranchName;
                document.getElementById('info-idnumber').innerHTML = model.IdNumber;
                document.getElementById('info-cusname').innerHTML = model.CusName;
                document.getElementById('info-status').innerHTML = model.StatusName;
                document.getElementById('info-sendapprovaldate').innerHTML = model.SendApprovalDate;
                document.getElementById('info-product').innerHTML = model.ProductName;
                document.getElementById('info-createddate').innerHTML = model.CreatedDate;
                document.getElementById('info-sendapprovaluser').innerHTML = model.SendApprovalUser;
                document.getElementById('info-confirmdate').innerHTML = model.ConfirmDate;
                document.getElementById('info-confirmuser').innerHTML = model.ConfirmUser;
                $('#viewDetail').modal("show");
            }
        });
        $('#approval').click(function () {
            models.push(item);
            if (models.length <= 0) {
                alert("Bạn phải lựa chọn ít nhất 1 bản ghi để duyệt, và chỉ có thể thực hiện duyệt đối với các bản ghi có trạng thái Inital.");
                return false;
            }
            $http.post('/ProductRegistration/ApprovalProductRegist', models)
           .then(function (data) {
               if (redirectToLogin(data)) {
                   window.location.href = getRootUrl() + "/Account/Login";
               } else {
                   getList();
               }
           });
        });
        $('#reject').click(function () {
            models.push(item);
            if (models.length <= 0) {
                alert("Bạn phải lựa chọn ít nhất 1 bản ghi để duyệt, và chỉ có thể thực hiện duyệt đối với các bản ghi có trạng thái Inital.");
                return false;
            }
            $http.post('/ProductRegistration/RejectProductRegist', models)
           .then(function (data) {
               if (redirectToLogin(data)) {
                   window.location.href = getRootUrl() + "/Account/Login";
               } else {
                   getList();
               }
           });
        });
    };

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "id-number" || e.target.id === "cus-name") || e.target.id === "mobile-number"
            || e.target.id === "branch" || e.target.id === "fromDate" || e.target.id === "toDate" || e.target.id === "status") {
            getList();
        }
    });
    function getList() {
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/ProductRegistration/GetList",
            params: {
                frd: JSON.stringify($('#fromDate').val()),
                td: JSON.stringify($('#toDate').val()),
                mobile: JSON.stringify($('#mobile-number').val()),
                idnumber: JSON.stringify($('#id-number').val()),
                cusname: JSON.stringify($('#cus-name').val()),
                status: JSON.stringify($('#status').val()),
                branch: JSON.stringify($('#branch').val()),
                pageIndex: $scope.currentPage,
                pageSize: $scope.PerPageItems
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data.records;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = data.total; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                //$scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.totalItems / $scope.PerPageItems);
            }
        });
    }
});

systemSearchApp.controller("confirmProductRegistController", function ($scope, $http) {
    $scope.currentPage = 1;
    $scope.PerPageItems = 10;
    getList();
    $scope.confirmProductRegistSearch = function () {
        getList();
    };
    $scope.pageChanged = function () {
        getList();
    };
    $scope.confirmProductRegistApprove = function () {
        var models = [];
        angular.forEach($scope.datas, function (item) {
            if (item.Selected == true) {
                models.push(item);
            }
        });
        if (models.length <= 0) {
            alert("Bạn phải lựa chọn ít nhất 1 bản ghi để duyệt.");
            return false;
        }
        $http.post('/ConfirmProductRegist/ApprovalProductRegist', models)
            .then(function (data) {
                if (redirectToLogin(data)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    getList();
                    $scope.selectedAll = false;
                }
            });


    };
    $scope.confirmProductRegistReject = function () {
        var models = [];
        angular.forEach($scope.datas, function (item) {
            if (item.Selected == true) {
                models.push(item);
            }
        });
        if (models.length <= 0) {
            alert("Bạn phải lựa chọn ít nhất 1 bản ghi để từ chối duyệt.");
            return false;
        }
        $http.post('/ConfirmProductRegist/RejectProductRegist', models)
            .then(function (data) {
                if (redirectToLogin(data)) {
                    window.location.href = getRootUrl() + "/Account/Login";
                } else {
                    getList();
                    $scope.selectedAll = false;
                }
            });

    };
    $scope.checkAll = function () {
        if ($scope.selectedAll) {
            $scope.selectedAll = true;
        } else {
            $scope.selectedAll = false;
        }
        angular.forEach($scope.datas, function (item) {
            item.Selected = $scope.selectedAll;
        });
    };
    $scope.viewDetail = function (item) {
        var models = [];
        $http({
            method: "post",
            url: getRootUrl() + "/ConfirmProductRegist/ViewDetail",
            params: {

                id: JSON.stringify(item.Id),
            }
        }).then(function (msg) {

            if (redirectToLogin(msg)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                var model = msg.data;
                document.getElementById('info-mobile').innerHTML = model.Mobile;
                document.getElementById('info-branch').innerHTML = model.BranchName;
                document.getElementById('info-idnumber').innerHTML = model.IdNumber;
                document.getElementById('info-cusname').innerHTML = model.CusName;
                document.getElementById('info-status').innerHTML = model.StatusName;
                document.getElementById('info-sendapprovaldate').innerHTML = model.SendApprovalDate;
                document.getElementById('info-product').innerHTML = model.ProductName;
                document.getElementById('info-createddate').innerHTML = model.CreatedDate;
                document.getElementById('info-sendapprovaluser').innerHTML = model.SendApprovalUser;
                document.getElementById('info-confirmdate').innerHTML = model.ConfirmDate;
                document.getElementById('info-confirmuser').innerHTML = model.ConfirmUser;
                $('#viewDetail').modal("show");
            }
        });
        $('#approval').click(function () {
            models.push(item);
            if (models.length <= 0) {
                alert("Bạn phải lựa chọn ít nhất 1 bản ghi để duyệt, và chỉ có thể thực hiện duyệt đối với các bản ghi có trạng thái Inital.");
                return false;
            }
            $http.post('/ConfirmProductRegist/ApprovalProductRegist', models)
           .then(function (data) {
               if (redirectToLogin(data)) {
                   window.location.href = getRootUrl() + "/Account/Login";
               } else {
                   getList();
               }
           });
        });
        $('#reject').click(function () {
            models.push(item);
            if (models.length <= 0) {
                alert("Bạn phải lựa chọn ít nhất 1 bản ghi để duyệt, và chỉ có thể thực hiện duyệt đối với các bản ghi có trạng thái Inital.");
                return false;
            }
            $http.post('/ConfirmProductRegist/RejectProductRegist', models)
           .then(function (data) {
               if (redirectToLogin(data)) {
                   window.location.href = getRootUrl() + "/Account/Login";
               } else {
                   getList();
               }
           });
        });
    };

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "id-number" || e.target.id === "cus-name") || e.target.id === "mobile-number"
            || e.target.id === "branch" || e.target.id === "fromDate" || e.target.id === "toDate" || e.target.id === "status") {
            getList();
        }
    });
    function getList() {
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/ConfirmProductRegist/GetList",
            params: {
                frd: JSON.stringify($('#fromDate').val()),
                td: JSON.stringify($('#toDate').val()),
                mobile: JSON.stringify($('#mobile-number').val()),
                idnumber: JSON.stringify($('#id-number').val()),
                cusname: JSON.stringify($('#cus-name').val()),
                status: JSON.stringify($('#status').val()),
                branch: JSON.stringify($('#branch').val()),
                pageIndex: $scope.currentPage,
                pageSize: $scope.PerPageItems
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data.records;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = data.total; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                //$scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.totalItems / $scope.PerPageItems);
            }
        });
    }
});

systemSearchApp.controller("hisTransController", function ($scope, $http) {
    $scope.currentPage = 1;
    $scope.PerPageItems = 10;
    getList();
    $scope.hisTransSearch = function () {
        getList();
    };
    $scope.pageChanged = function () {
        getList();
    };

    $scope.viewDetail = function (item) {
        $http({
            method: "post",
            url: getRootUrl() + "/HistoryTransaction/ViewDetail",
            params: {

                id: JSON.stringify(item.Id),
            }
        }).then(function (msg) {

            if (redirectToLogin(msg)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                var model = msg.data;
                document.getElementById('info-id').innerHTML = model.Id;
                document.getElementById('info-mobile').innerHTML = model.Mobile;
                document.getElementById('info-fracc').innerHTML = model.FromAccount;
                document.getElementById('info-toacc').innerHTML = model.ToAccount;
                document.getElementById('info-service').innerHTML = model.Service;
                document.getElementById('info-transtime').innerHTML = model.TransTime;
                document.getElementById('info-transnote').innerHTML = model.TransNote;
                document.getElementById('info-status').innerHTML = model.StatusName;
                document.getElementById('info-amount').innerHTML = model.Amount;
                document.getElementById('info-flatfee').innerHTML = model.FlatFee;
                document.getElementById('info-feeonatm').innerHTML = model.FeeOnAtm;
                document.getElementById('info-realamount').innerHTML = model.RealAmount;
                document.getElementById('info-currency').innerHTML = model.Currency;
                document.getElementById('info-os').innerHTML = model.Os;
                document.getElementById('info-imei').innerHTML = model.Imei;
                document.getElementById('info-rescode').innerHTML = model.ResCode;
                document.getElementById('info-pharse').innerHTML = model.PharseNote;
                $('#viewDetail').modal("show");
            }
        });
    };

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "id-number" || e.target.id === "cus-name") || e.target.id === "mobile-number"
            || e.target.id === "branch" || e.target.id === "fromDate" || e.target.id === "toDate" || e.target.id === "status") {
            getList();
        }
    });
    function getList() {
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/HistoryTransaction/GetList",
            params: {
                frd: JSON.stringify($('#fromDate').val()),
                td: JSON.stringify($('#toDate').val()),
                mobile: JSON.stringify($('#mobile-number').val()),
                toacc: JSON.stringify($('#to-account').val()),
                status: JSON.stringify($('#status').val()),
                service: JSON.stringify($('#service').val()),
                pageIndex: $scope.currentPage,
                pageSize: $scope.PerPageItems
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data.records;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = data.total; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                //$scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.totalItems / $scope.PerPageItems);
            }
        });
    }
});

systemSearchApp.controller("hisAccessAppController", function ($scope, $http) {
    $scope.currentPage = 1;
    $scope.PerPageItems = 10;
    getList();
    $scope.hisAccessAppSearch = function () {
        getList();
    };
    $scope.pageChanged = function () {
        getList();
    };

    $scope.viewDetail = function (item) {
        $http({
            method: "post",
            url: getRootUrl() + "/HistoryAccessApplication/ViewDetail",
            params: {

                id: JSON.stringify(item.Id),
            }
        }).then(function (msg) {

            if (redirectToLogin(msg)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                var model = msg.data;
                document.getElementById('info-mobile').innerHTML = model.MobileNo;
                document.getElementById('info-request').innerHTML = model.Request;
                document.getElementById('info-device').innerHTML = model.Device;
                document.getElementById('info-os').innerHTML = model.OS;
                document.getElementById('info-status').innerHTML = model.Status;
                document.getElementById('info-requestdate').innerHTML = model.RequestDate;
                document.getElementById('info-respondseddate').innerHTML = model.RespondsedDate;
                document.getElementById('info-desc').innerHTML = model.Description;
                document.getElementById('info-rescode').innerHTML = model.ResCode;
                document.getElementById('info-msgcode').innerHTML = model.MsgCode;
                $('#viewDetail').modal("show");
            }
        });
    };

    $(document).keypress(function (e) {
        if (e.which == 13 && (e.target.id === "request" || e.target.id === "mobile-number" ||
           e.target.id === "fromDate" || e.target.id === "toDate")) {
            getList();
        }
    });
    function getList() {
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/HistoryAccessApplication/GetList",
            params: {
                frd: JSON.stringify($('#fromDate').val()),
                td: JSON.stringify($('#toDate').val()),
                mobile: JSON.stringify($('#mobile-number').val()),
                request: JSON.stringify($('#request').val()),
                pageIndex: $scope.currentPage,
                pageSize: $scope.PerPageItems
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data.records;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = data.total; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                //$scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.totalItems / $scope.PerPageItems);
            }
        });
    }
});

systemSearchApp.controller("sessionAccessAppController", function ($scope, $http) {
    $scope.currentPage = 1;
    $scope.PerPageItems = 10;
    getList();
    $scope.sessionAccessAppSearch = function () {
        getList();
    };
    $scope.pageChanged = function () {
        getList();
    };
    $scope.kickOut = function (item) {
        $http({
            method: "post",
            url: getRootUrl() + "/SessionAccessApplication/KickOut",
            params: {
                id: JSON.stringify(item.Id)
            }
        }).then(function (data) {
            if (redirectToLogin(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                getList();
            }
        });;
    }
    $(document).keypress(function (e) {
        if (e.which == 13 && e.target.id === "mobile-number") {
            getList();
        }
    });
    function getList() {
        var datas = $http({
            method: "get",
            url: getRootUrl() + "/SessionAccessApplication/GetList",
            params: {
                mobile: JSON.stringify($('#mobile-number').val()),
                pageIndex: $scope.currentPage,
                pageSize: $scope.PerPageItems
            }
        });
        datas.success(function (data) {
            if (redirecToLoginByTypeOf(data)) {
                window.location.href = getRootUrl() + "/Account/Login";
            } else {
                $scope.datas = data.records;
                $scope.filteredItems = $scope.datas.length;
                $scope.maxSize = 5;
                $scope.totalItems = data.total; //tổng số bản ghi trong danh sách.
                //$scope.PerPageItems = 10; //cấu hình số bản ghi trên 1 trang
                //$scope.currentPage = 1;
                $scope.numberPages = Math.ceil($scope.totalItems / $scope.PerPageItems);
            }
        });
    }
});

function redirectToLogin(data) {
    if (data.data.toString().indexOf("!DOCTYPE") > 0) {
        return true;
    }
    return false;
}
function redirecToLoginByTypeOf(data) {
    if (typeof data == "string") {
        return true;
    } else {
        return false;
    }
}

function setOption(data, id) {
    var select = document.getElementById(id);
    var selectorSelect = "#" + id;
    $(selectorSelect).empty();
    for (var i = 0; i < data.length; i++) {
        var option = document.createElement("option");
        option.setAttribute("value", data[i]);
        var text = document.createTextNode(data[i]);
        option.appendChild(text);
        select.appendChild(option);
    }

}

function displayCurrency(val) {
    if (isNaN(parseFloat(val))) {
        val = "";
    } else {
        val = parseFloat(val.replace(/,/g, ""))
            .toString()
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }
    return val;
}

