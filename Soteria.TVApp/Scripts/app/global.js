var Global = {
    RenderTimeAgo: function RenderTimeAgo(ctrl) {
        timeago().render($(ctrl));
    },
    InitiateAjaxRequest: function InitiateAjaxRequest(URL, Type, Params, ContentType) {
		console.log(URL);
        var headerToken = $('.hdnHeaderToken').val();
        var ajaxResponse = {
            IsSuccess: false,
            response: null
        }
        var ajaxRequest = $.ajax({
            url: URL,
            type: Type,
			beforeSend: function(){
				$('.loading_img').show();
			},
			complete: function(){
				$('.loading_img').hide();
			},
            data: Params,
            async: false,
            contentType: (ContentType != null || ContentType != "" || ContentType != undefined) ? ContentType : "application/x-www-form-urlencoded; charset=UTF-8",
            headers: {
                "Authorization": 'Bearer ' + headerToken
            }
        });
        ajaxRequest.done(function (response) {
			$('.loading_img').hide();
            ajaxResponse.IsSuccess = true;
            ajaxResponse.response = response;
        });
        ajaxRequest.fail(function (xhr, textStatus) {
			$('.loading_img').hide();
            console.log(textStatus);
        });
        ajaxRequest.always(function (response) {$('.loading_img').hide();
        });
        return ajaxResponse;
    },
    FileUploadAjaxRequest: function FileUploadAjaxRequest(URL, Type, UploadData, token) {
        var headerToken = $('.hdnHeaderToken').val();
        var ajaxResponse = {
            IsSuccess: false,
            response: null
        }
        var ajaxRequest = $.ajax({
            url: URL,
            data: UploadData,
            type: Type,
            contentType: false,
            processData: false,
            async: false,
            headers: {
                "Authorization": 'Bearer ' + headerToken
            },
        });
        ajaxRequest.done(function (response) {
            ajaxResponse.IsSuccess = true;
            ajaxResponse.response = response;
        });
        ajaxRequest.fail(function (xhr, textStatus) {
            console.log(textStatus);
        });
        return ajaxResponse;
    },
    ToasterSuccess: function ToasterSuccess(msg, title) {
        toastr.success(msg, title, {
            "closeButton": true
        });
    },
    ToasterInfo: function ToasterInfo(msg, title) {
        toastr.info(msg, title, {
            "closeButton": true
        });
    },
    ToasterWarning: function ToasterWarning(msg, title) {
        toastr.warning(msg, title, {
            "closeButton": true
        });
    },
    ToasterError: function ToasterError(msg, title) {
        toastr.error(msg, title, {
            "closeButton": true
        });
    }
}
String.prototype.format = function () {
    a = this;
    for (k in arguments) {
        a = a.replace("{" + k + "}", arguments[k])
    }
    return a
}
