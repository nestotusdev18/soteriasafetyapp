var Dashboard = {
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
	LoadBathroomVisit: function LoadBathroomVisit() {
        var ajaxResponse;
		var UserId = parseInt($('#hdnUserId').val());
        var params = {
            UserId: UserId,
        }
        ajaxResponse = Global.InitiateAjaxRequest("/view/dashboard", "Post", params);
        $('.mainView__content').html('');
        $('.mainView__content').html(ajaxResponse.response);
        $('#hdnUserId').val(UserId);
    },
	
	 LoadTVApp: function LoadTVApp() {
		/*
        var ajaxResponse;
		var UserId = parseInt($('#hdnUserId').val());
		var SchoolId = parseInt($('#hdnSchoolId').val());
		var SchoolCode = parseInt($('#hdnSchoolCode').val());
        var params = {
            UserId: UserId,
			SchoolId: SchoolId,
			SchoolCode: SchoolCode,
        }
        ajaxResponse = Global.InitiateAjaxRequest("/view/TVAppDashboard", "Post", params);
        $('.mainView__content').html('');
        $('.mainView__content').html(ajaxResponse.response);
        $('#hdnUserId').val(UserId);*/
    },
	
}







