// Showing Loading Icon when searching customer : 
// isShowed = true -> show else hide
function toggleLoadingCustomerIcon(isShowed) {
    if (isShowed) {
        $("#loading-customer").css("display", "block");
        $(".address-book").css("opacity", "0.3");
    }
    else {
        $("#loading-customer").css("display", "none");
        $(".address-book").css("opactiy", "1");
    }
}

function toggleModal(modal, isShowed) {
    if (isShowed) {
        // Show
    }
    else {
        // Hide
        $(modal).modal("hide");
    }
}

function showNotification(result) {
    if (result.Status === 'Success') {
        toastr.success(result.Message);
    }
    else if (result.Status === 'Unvalidated') {
        var ul = $("<ul></ul>");
        result.Errors.forEach(function (error) {
            var li = $(`<li>${error}</li>`);
            ul.append(li);
        });
        toastr.warning(ul);
    }
    else if (result.Status === 'Failure') {
        toastr.error(result.Message);
    }
}

$(function (e) {
    // custom validation message for form input
    //$("form input").on("invalid", function (e) {
    //    if ($(this).attr("type") === 'phone') {
    //        this.setCustomValidity("Phone is not a valid format !");
    //    }
    //    else if ($(this).attr("type") == 'email') {
    //        this.setCustomValidity("Email is not a valid format !");
    //    }
    //});   

    // RESET FORM AFTER ADDING 
    $("#new-customer-modal").on("hidden.bs.modal", function (e) {
        $($(this).find("form")).trigger("reset");
    });

    //ADD NEW CUSTOMER FUNCTION
    $("#new-customer-form").on("submit", function (e) {
        e.preventDefault();
        var currentModal = $(this).parents(".modal");
        var url = $(this).attr("action");
        var data = $(this).serialize();
        var method = $(this).attr("method");

        $.ajax({
            url: url,
            method: method,
            data: data,
            cached: false,
        }).done(function (result) {
            showNotification(result);
            if (result.Status === 'Success') {
                toggleModal(currentModal, false);
            }
        });
    });

    // SEARCH CUSTOMER FUNCTION
    $("#customer-search-form").on("submit", function (e) {
        e.preventDefault();
        toggleLoadingCustomerIcon(true); // Show Loading Gif 
        var submitBtn = $(this).find("button[type=submit]");
        var url = $(submitBtn).data("url");
        var data = $(this).serialize();

        $.ajax({
            url: url,
            data: data,
            method: 'GET',
            cached: false
        }).done(function (result) {
            if (result !== null) {
                $("#customer-search-result .row").replaceWith(result);
            }
            else {
                alert("Error Occurs");
            }
            toggleLoadingCustomerIcon(false);
        }).fail(function (e) {
            alert("Error occurs");
        });
    });

    // TRIGGER FOR EDIT CUSTOMER
    $("#customer-submit-edit").on('click', function (e) {
        var formId = $(this).data('target');
        var btnSubmit = this;
        var url = $(this).data('url');
        var isOpen = $(this).data('open-form');
        var data;

        if (isOpen) {
            // UPDATE CUSTOMER
            data = $(formId).serialize();            
            // CALLING AJAX
            $.ajax({
                url: url,
                method: 'post',
                cached: false,
                data: data
            }).done(function (result) {
                showNotification(result);
                if (result.Status === 'Success') {
                    // DISABLE ALL INPUT 
                    $(formId).find('input').toArray().forEach(function (ele) {
                        $(ele).attr('disabled', 'disabled');
                    });
                    // CHANGE BUTTON TO EDIT
                    $(btnSubmit).removeClass('btn-success').addClass('btn-primary');
                    $(btnSubmit).html("<i class='fa fa-edit'></i> &nbsp; Edit");
                    // CHANGE FLAG 'open-form' TO FALSE
                    $(btnSubmit).data('open-form', false);
                }
            });
        }
        else {
            // ENABLE ALL INPUT
            $(formId).find('input').toArray().forEach(function (ele) {
                $(ele).removeAttr('disabled');
            });
            // CHANGE BUTTON TO SAVE 
            $(btnSubmit).removeClass('btn-primary').addClass('btn-success');
            $(btnSubmit).html("<i class='fa fa-save'></i> &nbsp; Save");
            // CHANGE FLAG 'open-form' TO TRUE
            $(btnSubmit).data('open-form', true);
        }
    });

    // TRIGGER FOR REMOVE CUSTOMER
    $("#customer-submit-remove").on('click', function (e) {
        var formId = $(this).data("target");
        var editButton = $("#customer-submit-edit");
        var url = $(this).data('url');
        var isOpenForEdit = $(editButton).data('open-form');
        var data;

        if (isOpenForEdit) {
            toastr.warning("Please finish editing before remove");
            return;
        }
        else {
            $("#edit-customer-form #CustomerID").removeAttr('disabled');
            data = $(formId).serialize();
            $.ajax({
                url: url,
                method: 'post',                
                cached: false,
                data: data
            }).done(function (message) {
                showNotification(message);
                if (message.Status === 'Success') {
                    // REMOVE CURRENT FORMAT 
                    var afterRemove = $("<ul>");
                    afterRemove.append("<li><h2>Customer has been removed!</h2></li>");
                    afterRemove.append("<li><h2>Choose another customer to continue!</h2></li>");
                    $("#customer-wrapper").replaceWith(afterRemove);
                }
            });
        }
       
    });

});