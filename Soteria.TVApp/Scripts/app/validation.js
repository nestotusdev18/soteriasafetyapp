var Validation = {
	ValidatePart: function ValidatePart() {
		$("#part_form").validate({
			rules: {
				PartNumber: {
					required: !0,
					remote: {
					url: "/api/parts/partNumberAvailability",
					type: "post",
					data: {
					    VendorID: $('#hdnVendorId').val(),
						VendorBrandID :  $('#hdnVendorBrandId').val(),
					    PartNumber: function() {
						return $( "#PartNumber" ).val();
					  }
					},
					dataFilter: function(response) {
						response = JSON.parse(response);

						if($('#VendorPartID').val() >0 ){
							if($( "#PartNumberHidden" ).val().toLowerCase() == $( "#PartNumber" ).val().toLowerCase()){
								return true;
							}
						}
						
						if(response.PartNumberAvailability.IsPartNumberAvailable){
							return true;
						}
						else{
							return false;
						}
					},
				  },
				},
				PartDesc: {
					required: !0
				},
				Msrp: {
					required: !0,
					number: true,
					min:0
				},
				Map: {
					required: !0,
					number: true,
					min:0
				},
			},
			messages: {
				PartNumber: {
					required: 'Part Number is Required',
					remote: 'Part Number already exists!',
					
				},
				PartDesc: {
					required: 'Part Description is Required',
				},
				Msrp: {
					required: 'Msrp is Required',
					number : 'Please enter a valid msrp price',
					min : 'Please enter a valid msrp price',
				},
				Map: {
					required: 'MAP is Required',
					number : 'Please enter a valid map price',
					min : 'Please enter a valid map price',
				}
			},
			invalidHandler: function(e, r) {
			},
			submitHandler: function(e) {
				
				var param = {
					VendorPartID: $('#VendorPartID').val(),
					PartNumber: $('#PartNumber').val(),
					PartName: $('#PartDesc').val(),
					Msrp: $('#Msrp').val(),
					Map: $('#Map').val(),
					VendorID: $('#hdnVendorId').val(),
					VendorBrandID :  $('#hdnVendorBrandId').val(),
					CreatedBy :  $('#hdnUserId').val(),
				};
				   
				if($('#VendorPartID').val() >0 ){
					responseMsg = "Part Updated Successfully!";
				}
				else{
					responseMsg = "Part Created Successfully!";
				}
				
				ajaxResponse = Global.InitiateAjaxRequest("/api/parts/addUpdateVendorPart", "Post", param);
			   
				if (ajaxResponse.IsSuccess) {
					var hdnBrandName = $('#hdnVendorBrandName').val();
					Dashboard.LoadParts($('#hdnVendorId').val(),$('#hdnVendorBrandId').val(),String(hdnBrandName));
					return swal({
					title: "",
					icon: "success",
					text: responseMsg,
					confirmButtonClass: "btn btn-success ripple"
					}), !1
				}
			}
		})
	},
	
	
	ValidateLogin: function ValidateLogin() {
		$("#login_form").validate({
			errorElement: 'span',
			rules: {
				Password: {
					required: !0,
					remote: {
					url: "/api/user/validateCurrentPassword",
					type: "post",
					data: {
					    UserId: $('#hdnUserId').val(),
					    Password: function() {
							return $( "#Password" ).val();
						}
					},
					dataFilter: function(response) {
						response = JSON.parse(response);
						
						if(response.CurrentPasswordAvailability.IsPasswordAvailable){
							return true;
						}
						else{
							return false;
						}
					},
				  },
				},
				NewPassword: {
					required: !0
				},
			},
			messages: {
				Password: {
					required: 'Current Password is Required',
					remote: 'Current Password is Invalid',
					
				},
				NewPassword: {
					required: 'New Password is Required',
				},
				
			},
			errorPlacement: function(error, element) {
				//console.log(error);
				//console.log($(element).parent().next());
				$(element).parent().next('span').html(error);
				//$('.customError').html('');
				//$('.customError').html(error);
			},
			invalidHandler: function(e, r) {
			},
			submitHandler: function(e) {
				
				var param = {
					Password: $('#NewPassword').val(),
					UserId: $('#hdnUserId').val(),
				};
				   
				responseMsg = "Password Updated Successfully!";
				
				ajaxResponse = Global.InitiateAjaxRequest("/api/user/changePassword", "Post", param);
			   
				if (ajaxResponse.IsSuccess) {
					return swal({
					title: "",
					icon: "success",
					text: responseMsg,
					confirmButtonClass: "btn btn-success ripple"
					}), !1
				}
			}
		})
	},
	
	ValidateSubscription: function ValidateSubscription(){
      $("#subscription_form").validate({
         errorElement: 'span',
			rules: {
				SubStartDate: {
					required: !0,
				},
            SubEndDate: {
					required: !0,
				},
            SubRate: {
					required: !0,
				},
            SubAmount: {
					required: !0,
				},
			},
         messages: {
				SubStartDate: {
					required: 'Start Date is Required',
				},
				SubEndDate: {
					required: 'End Date is Required',
				},
            SubRate: {
					required: 'Rate is Required',
				},
				SubAmount: {
					required: 'Amount is Required',
				},
			},
         invalidHandler: function(e, r) {
			},
         submitHandler: function(e) {
            
            var param = {
               SubscriptionID: $('#hdnSubscriptionId').val(),
               VendorID: $('#hdnVendorId').val(),
					VendorBrandID : $('#hdnVendorBrandId').val(),
					StartDate: $('#SubStartDate').val(),
					EndDate: $('#SubEndDate').val(),
               AmountBilled: $('#SubRate').val(),
               AmountPaid: $('#SubAmount').val(),
               Notes: $('#SubNotes').val(),
               CreatedBy: $('#hdnUserId').val()
				};
				if($('#hdnSubscriptionId').val() >0 ){
					responseMsg = $('#hdnVendorBrandName').val()+" - Subscription Updated Successfully!";
				}
				else{
					responseMsg = $('#hdnVendorBrandName').val()+" - Subscription Created Successfully!";
				}   
				
				ajaxResponse = Global.InitiateAjaxRequest("/api/brand/addUpdateSubscription", "Post", param);
			   
				if (ajaxResponse.IsSuccess) {
               Dashboard.LoadManageSubscriptions();
					return swal({
					title: "",
					icon: "success",
					text: responseMsg,
					confirmButtonClass: "btn btn-success ripple"
					}), !1
				}
			}
      })
   },
   
	ValidateIdentifier: function ValidateIdentifier(){
      
      var websiteId = $('#hdnWebSiteId').val();
      
      $("#identifier_form").validate({
         errorElement: 'span',
			rules: {
				BrandIdentifier: {
					required: !0,
				},
         },
         messages: {
				SubStartDate: {
					required: 'Start Date is Required',
				},
         },
         invalidHandler: function(e, r) {
			},
         submitHandler: function(e) {
            if(websiteId > 0){
               var param = {
                  BrandIdentifierMappingID: $('#hdnBrandIdentifierMappingId').val(),
                  VendorBrandID : $('#hdnVendorBrandId').val(),
                  WebSiteID: $('#hdnWebSiteId').val(),
                  BrandIdentifier: $('#BrandIdentifier').val()
               };
               responseMsg = $('#hdnVendorBrandName').val()+" - "+$('#hdnWebSiteCode').val()+" Identifier Updated Successfully!";               
               ajaxResponse = Global.InitiateAjaxRequest("/api/brand/addupdatebrandidentifier", "Post", param);
            }else{
               var ajaxResponse1;
               responseMsg = $('#hdnVendorBrandName').val()+" Identifier Created Successfully!";
               ajaxResponse1 = Global.InitiateAjaxRequest("/api/brand/websiteslist", "Post", param); 
               var websiteIds = ajaxResponse1.response.WebSiteIds;
               $.each(websiteIds, function( key, value ) {
                  var param = {
                     BrandIdentifierMappingID: $('#hdnBrandIdentifierMappingId').val(),
                     VendorBrandID : $('#hdnVendorBrandId').val(),
                     WebSiteID: value.WebSiteID,
                     BrandIdentifier: $('#BrandIdentifier').val()
                  };
                  ajaxResponse = Global.InitiateAjaxRequest("/api/brand/addupdatebrandidentifier", "Post", param);
               });
            }
				if (ajaxResponse.IsSuccess) {
               Dashboard.LoadBrandIdentifiers($('#hdnVendorBrandId').val(), $('#hdnVendorBrandName').val());
					return swal({
                  title: "",
                  icon: "success",
                  text: responseMsg,
                  confirmButtonClass: "btn btn-success ripple"
					}), !1
				}
         }
      })
   },
}