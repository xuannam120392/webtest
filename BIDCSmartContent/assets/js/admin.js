//$("#<%=btnConfirm.ClientID%>").click(function () {
//    alert("tienvv");
//    if (isUnique("#<%=txtAddUsername.ClientID%>", true, 1) &&
//	        isNotEmpty("#<%=txtAddPassword.ClientID%>", 6) &&
//	        compareInput("#<%=txtAddPassword.ClientID%>", "=", "#<%=txtRePassword.ClientID%>", true) &&
//	        isNotEmpty("#<%=txtFullname.ClientID%>", 3)) {
//        if ($("#<%=ddlBranch.ClientID%> option").size() <= 0) {
//            alert("Please insert Branch before create new user.");
//            return false;
//        } else
//            return true;
//    } else
//        return false;
//});

//function SKRedirect() {
//    if (confirm("Do you want to go to List?"))
//        return true;
//    else
//        return false;
//}


function ConfirmationDistrict() {
    if (confirm("Are you sure you want to add new this district?")) {
        alert($(".txtDistrictCode").val());
        if ($(".txtDistrictCode").val() != "" && $(".txtDistrictName").val() != "")
            return true;
        else
            return false;
    }
    else
        return false;
}


function Confirmation() {
    if (confirm("Are you sure you want to delete this user?"))

        return true;
    else
        return false;
}

function ConfirmRedirec(url) {
    if (confirm("Are you sure you want to go to list?")) {
        alert(url);
        window.location.href(url);
        return true;
    }
    else
        return false;
}

var index = 0;
var iStatus = 1;
//Get Role ID
function GetRoleID(rId, rSt) {
    index = rId;
    iStatus = rSt;
};
$("#btnRoleListOK").on("click", function (e) {
    var idRole = index
    var iSt = iStatus
    $.ajax({
        type: "POST",
        url: "Default.aspx/DeleteRole",
        data: "{pID: '" + idRole + "',pStatus: '" + iSt + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d.split('|')[0] = "0") {
                $('.modal-body').html("Successfull...");
                $('.modal-body').fadeIn(6000);
                $("#phMain").load("Control/Role/List.ascx");
            }
        },
        failure: function (msg) {
            alert(msg);
        },
        error: function (xhr, err) {
            alert(err);
        }
    });
});
//Get usser ID
index = 0;
function GetUserID(id) {
    index = id;
};
$("#btnUserListOK").on("click", function (e) {
    var idRole = index
    $.ajax({
        type: "POST",
        url: "Default.aspx/DeleteUser",
        data: "{pID: '" + idRole + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d = "OK") {
                $('.modal-body').html("Successfull...");
                $('.modal-body').fadeIn(6000);
                $("#phMain").load("Control/User/List.ascx");
            }
        },
        failure: function (msg) {
            alert(msg);
        },
        error: function (xhr, err) {
            alert(err);
        }
    });
});

// Load user
$("#assign_role_popup").on("click", function (e) {
    var idUser = index;
    $.ajax({
        type: "POST",
        url: "Default.aspx/GetUserById",
        data: "{pID: '" + idUser + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('.formAsignRoleHtml').html(msg.d);
            //$('.modal-body').reload();
            $("#assign_role_popup").reload();
        },
        failure: function (msg) {
            alert(msg);
        },
        error: function (xhr, err) {
            alert(err);
        }
    });
});


//Get city ID
var cityStatus = 1;
function GetCityID(e,ciStatus) {
    index = e;
    cityStatus = ciStatus
};
$("#btnCityListOK").on("click", function (e) {
    var citycode = index
    var iSt = cityStatus
    $.ajax({
        type: "POST",
        url: "Default.aspx/ActiveOrBlockCity",
        data: "{pID: '" + citycode + "',pStatus: '" + iSt + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d = "OK") {
                $('.modal-body').html("Successfull...");
                $('.modal-body').fadeIn(6000);
                $("#phMain").load("Control/City/List.ascx");
            }
        },
        failure: function (msg) {
            alert(msg);
        },
        error: function (xhr, err) {
            alert(err);
        }
    });
});


//==================================================Distrist==========================================
var districtStatus = 1;
var disID = "000";
function GetDistrictID(e, disStatus) {
    disID = e;
    districtStatus = disStatus;
};
$("#btnDistrictListOK").on("click", function (e) {
    var districtcode = disID;
    alert(districtcode);
    var iSt = districtStatus;
    $.ajax({
        type: "POST",
        url: "Default.aspx/ActiveOrBlockDistrict",
        data: "{pID: '" + districtcode + "',pStatus: '" + iSt + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d = "OK") {
                $('.modal-body').html("Successfull...");
                $('.modal-body').fadeIn(6000);
                $("#phMain").load("Control/District/List.ascx");
            }
        },
        failure: function (msg) {
            alert(msg);
        },
        error: function (xhr, err) {
            alert(err);
        }
    });
});
//==================================================end Distrist==========================================
//Asign Role
$("#btnAsignRole").on("click", function (e) {
    if (confirm("Are you sure you want to asign this role?")) {
        var userid = index
        alert(userid);
        var allVals = [];
        $("input[type='checkbox']:checked").each(
        function () {
            // Your code goes here...
            allVals.push($(this).val());
        });
        alert(allVals);
        $.ajax({
            type: "POST",
            url: "Default.aspx/AsignRole",
            data: "{pID: '" + userid + "',pRole: '" + allVals + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d = "OK") {
                    $('.modal-body').html("Successfull...");
                    $('.modal-body').fadeIn(6000);
                    $("#phMain").load("Control/User/List.ascx");
                }
            },
            failure: function (msg) {
                alert(msg);
            },
            error: function (xhr, err) {
                alert(err);
            }
        });
    }
    else
        return false;
});


//Add Role
$("#btnConfirmRole").on("click", function (e) {
    var roleDes = $("#txtRoleDes").val();
    var rolename = $("#txtRoleName").val();
    if (rolename != null) {
        $(".lblErrorMsg").text('Role name not value null');
        return false;
    }
    if (confirm("Are you sure you want to add this role?")) {
        $(".lblErrorMsg").text('');
        var allVals = [];
        $("input[type='checkbox']:checked").each(
                function () {
                    // Your code goes here...
                    allVals.push($(this).val());
                });
        alert(allVals);
        $.ajax({
            type: "POST",
            url: "Default.aspx/AddRole",
            data: "{pRoleName: '" + rolename + "',pRoleDes: '" + roleDes + "',pRole: '" + allVals + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d.split('|')[0] = "0") {
                    alert(msg.d.split('|')[1]);
                    $('.modal-body').html("Successfull...");
                    $('.modal-body').fadeIn(6000);
                    $("#phMain").load("Control/Role/List.ascx");
                }
            },
            failure: function (msg) {
                alert(msg);
            },
            error: function (xhr, err) {
                alert(err);
            }
        });
    }
    else
        return false;
});

//Edit Role
$("#btnUpdateRole").on("click", function (e) {
    var roleDes = $(".txtDescription").val();
    var rolename = $(".txtRoleName").val();
    var roleCode = $(".lblRoleCode").text();
    if (confirm("Are you sure you want to edit this role?")) {
        $(".lblErrorMsg").text('');
        var allVals = [];
        $("input[type='checkbox']:checked").each(
                function () {
                    // Your code goes here...
                    allVals.push($(this).val());
                });
        alert(allVals);
        $.ajax({
            type: "POST",
            url: "Default.aspx/EditRole",
            data: "{roleID: '" + roleCode + "',pRoleName: '" + rolename + "',pRoleDes: '" + roleDes + "',pRole: '" + allVals + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d.split('|')[0] = "0") {
                    alert(msg.d.split('|')[1]);
                    $('.modal-body').html("Successfull...");
                    $('.modal-body').fadeIn(6000);
                    $("#phMain").load("Control/Role/List.ascx");
                }
            },
            failure: function (msg) {
                alert(msg);
                $("#phMain").load("Control/Role/List.ascx");
            },
            error: function (xhr, err) {
                alert(err);
            }
        });
    }
    else
        return false;
});


//Edit City
$("#btnUpdateCity").on("click", function (e) {
    var cityName = $(".txtCityName").val();
    var cityCode = $(".txtCityCode").val();
    if (confirm("Are you sure you want to edit this city?")) {
        $.ajax({
            type: "POST",
            url: "Default.aspx/EditCity",
            data: "{cityID: '" + cityCode + "',pCityName: '" + cityName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d.split('|')[0] = "0") {
                    $('.lblmsg').fadeIn(60000);
                    $(".lblmsg").text(msg.d.split('|')[1]);
                }
            },
            failure: function (msg) {
                $('.lblmsg').fadeIn(60000);
                $(".lblmsg").text(msg.d);
            },
            error: function (xhr, err) {
                $('.lblmsg').fadeIn(60000);
                $(".lblmsg").text(err.d);
            }
        });
    }
    else
        return false;
});

//if ($("#isAgeSelected").is(':checked'))
//    $("#txtAge").show();  // checked
//else
//    $("#txtAge").hide();  // unchecked

//active menu
$('ul.nav-top li').click(function () {
    var intdex = $(this).index() + 1;
    //    alert($('ul.nav-list li#idleft' + intdex + '').attr('class'));
    //    var valueli = $('ul.nav-list li#idleft' + intdex + '').attr('class');
    //    if (valueli = "active") {
    //        $('ul.nav-list li#idleft' + intdex + '').removeClass('active');
    //        $('ul.nav-list li#idleft' + intdex + '').addClass('active hsub open');
    //    }
    //    if (valueli = "hsub") {
    //        $('ul.nav-list li#idleft' + intdex + '').removeClass('hsub');
    //        $('ul.nav-list li#idleft' + intdex + '').addClass('active hsub open');
    //    }
    //    if (valueli = "hsub open") {
    //        $('ul.nav-list li#idleft' + intdex + '').removeClass('hsub open');
    //        $('ul.nav-list li#idleft' + intdex + '').addClass('active hsub open');
    //    }
    //    if (valueli = "active hsub open") {
    //        $('ul.nav-list li#idleft' + intdex + '').removeClass('active hsub open');
    //        $('ul.nav-list li#idleft' + intdex + '').addClass('active hsub open');
    //    }
    //    else {
    //        $('ul.nav-list li#idleft' + intdex + '').addClass('open');
    //    }
    $('ul.nav-list li').removeClass('active');
    $('ul.nav-list li').removeClass('hsub open');
    $('ul.nav-list li#idleft' + intdex + '').addClass('hsub open');
    $('ul.nav-list li#idleft' + intdex + '').addClass('active hsub open');
});

//Change pass

