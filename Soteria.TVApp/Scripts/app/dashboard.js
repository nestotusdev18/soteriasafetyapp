﻿var Dashboard = {
    FeaturePending: function FeaturePending() {
        alert("Feature coming soon...");
    },
    AppLogout: function AppLogout() {
        window.location = "/app/logout";
    },
    LoadDashboard: function LoadDashboard() {
        var ajaxResponse;
		var UserId = parseInt($('#hdnUserId').val());
		var SchoolId = parseInt($('#hdnSchoolId').val());
		var SchoolCode = parseInt($('#hdnSchoolCode').val());
        var params = {
            UserId: UserId,
			SchoolId: SchoolId,
			SchoolCode: SchoolCode,
        }
        ajaxResponse = Global.InitiateAjaxRequest("/view/dashboard", "Post", params);
        $('.mainView__content').html('');
        $('.mainView__content').html(ajaxResponse.response);
        $('#hdnUserId').val(UserId);
    },
	LoadCheckpointLogActivities: function LoadCheckpointLogActivities() {
        var ajaxResponse;
		var UserId = parseInt($('#hdnUserId').val());
		var SchoolId = parseInt($('#hdnSchoolId').val());
		var SchoolCode = parseInt($('#hdnSchoolCode').val());
        var params = {
            UserId: UserId,
			SchoolId: SchoolId,
			SchoolCode: SchoolCode,
        }
        ajaxResponse = Global.InitiateAjaxRequest("/view/checkpointLogActivities", "Post", params);
        $('.mainView__content').html('');
        $('.mainView__content').html(ajaxResponse.response);
        $('#hdnUserId').val(UserId);
    },
	LoadSchoolDashboard: function LoadSchoolDashboard() {
		var SchoolId = parseInt($('#hdnSchoolId').val());
		url = 'AppDashboard?SchoolId='+SchoolId;
		window.open(url, '_blank');
    }
	
	
}







