var DateFilters = {
	DateTimePickerSelect: function DateTimePickerSelect(sourceType) {
		$("#m_daterangepicker_4").daterangepicker({
			buttonClasses: "m-btn btn",
			applyClass: "btn-primary",
			cancelClass: "btn-secondary resetDateRange",
			timePicker: !0,
			timePickerIncrement: 15,
			locale: {
				format: "MM/DD/YYYY h:mm A"
			}
		}, function(a, t, n) {
			
			$(".FilterReset").show();
			
			$("#m_daterangepicker_4 .form-control").val(a.format("MM/DD/YYYY h:mm A") + " / " + t.format("MM/DD/YYYY h:mm A"));
			startDateFormat = a.format("YYYY-MM-DD HH:mm:ss");
			endDateFormat = t.format("YYYY-MM-DD HH:mm:ss");
			if(sourceType==1){
				Dashboard.FetchBeaconActivities(startDateFormat,endDateFormat);
			}
			else if(sourceType==2){
				Dashboard.FetchObjectActivities(startDateFormat,endDateFormat);
			}
			else if(sourceType == 3){
				Dashboard.FetchItemActivities(startDateFormat,endDateFormat);
			}
			else if(sourceType == 4){
				Dashboard.FetchHeadcountLogs(startDateFormat,endDateFormat);
			}
			return;
		},
		$('body').on('click', '.resetDateRange', function() {
			$("#m_daterangepicker_4 #dateInputRange").val("");
			return;
		}),
		$('body').on('click', '.FilterReset', function() {
			$(".FilterReset").hide();
			$("#m_daterangepicker_4 #dateInputRange").val("");
			if(sourceType==1){
				Dashboard.FetchBeaconActivities();
			}
			else if(sourceType==2){
				Dashboard.FetchObjectActivities();
			}
			else if(sourceType == 3){
				Dashboard.FetchItemActivities();
			}
			else if(sourceType == 4){
				Dashboard.FetchHeadcountLogs();
			}
			return;
		})
		)
    },
}

