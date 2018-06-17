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

function initCollapse() {
    $('#delivered-panel').collapse();
    $('#notfix-panel').collapse();
    $('#waiting-panel').collapse();
}
// reload partial after change status from not fix panel
function reloadPartial(targetId, transactionId, status) {
    var target = $(targetId); // get the table
    var url = $("#not-fix-table").data('reload-url');
    var counter = $(targetId).closest(".panel").find('.badge');
    var total = $(counter).text();
    console.log("TOTAL: " + total);

    $.ajax({
        method: 'GET',
        cached: false,
        url: url,
        data: {
            transactionId: transactionId,
            status: status
        }
    }).done(function (partial) {
        console.log(partial);
        $(target).append(partial);
        $(counter).text(++total);
    });
}

function reIndexPriceDetail(currentWrapper) {
    $(currentWrapper).children(".form-group").toArray().forEach(function (group, index) {
        $($(group).children(".form-control")[0]).attr("name", "PriceList[" + index + "].Description");
        $($(group).children(".form-control")[1]).attr("name", "PriceList[" + index + "].Price")
    });
}

function addListnerPopoverForCannotFix(caller) {
    var transactionId = $(caller).data("transaction-id");
    var currentTransaction = $(caller).closest("tr");

    $(`#popover-cannotfix-${transactionId}`).on('submit', function (e) {
        e.preventDefault();
        var form = this;
        var url = $(form).attr("action");
        var method = $(form).attr("method");
        var data = $(form).serialize();

        $.ajax({
            url: url,
            method: method,
            cached: false,
            data: data
        }).done(function (data) {
            if (data.Status === 'Success') {
                toastr.success(data.Message);
                $(currentTransaction).remove();
                $(caller).popover("hide");
                reloadPartial("#cannot-fix-table", transactionId, "CannotFix");
            }
            else {
                toastr.error(data.Message);
            }
        });
    });
}

function removePriceDetail(btn) {

    var parentForm = $(btn).parents("form");
    var priceWrapper = $(parentForm).find('.expandable');
    var currentDiv = $(btn).parents(".form-group");
    var count = $(priceWrapper).data('count');

    if (count === 0) return;

    $(currentDiv).remove();
    $(priceWrapper).data('count', count - 1);
    reIndexPriceDetail(priceWrapper);
}

function initFirstPriceDetail(caller) {
    var transactionId = $(caller).data('transaction-id');
    var removeBtn = $(`#remove-btn-0-${transactionId}`);
    $(removeBtn).on('click', function (e) {
        e.preventDefault();
        removePriceDetail(this);
    });
}
function newPriceDetials(index) {
    var formGroup = $("<div class='form-group'></div>");
    var description = $(`<input class="form-control" name="PriceList[${index}].Description" type="text" placeholder="Enter Description" required/>`);
    var price = $(`<input class="form-control" name="PriceList[${index}].Price" type="Number" placeholder="price" required/>`);
    var deleteBtn = $(`<button class='btn btn-sm' style='float: right'><i class='fa fa-remove'></i></button>`);
    $(deleteBtn).on('click', function (e) {
        e.preventDefault();
        removePriceDetail(this);
    });
    formGroup.append(description).append(price).append(deleteBtn);
    return formGroup;
}


function addListenerPopoverForFix(caller) {
    var transactionId = $(caller).data("transaction-id");
    var currentTransaction = $(caller).closest("tr");

    $(`#add-more-detail-${transactionId}`).on('click', function (e) {
        var btn = $(this);
        var parentDiv = $(btn).parents('form').find('.expandable');
        var count = $(parentDiv).data('count');
        var priceDetail = newPriceDetials(count + 1);
        parentDiv.append(priceDetail);
        $(parentDiv).data('count', count + 1);
    });

    $(`#popover-fix-${transactionId}`).on('submit', function (e) {
        e.preventDefault();
        var form = this;
        var url = $(form).attr("action");
        var method = $(form).attr("method");
        var data = $(form).serialize();

        $.ajax({
            url: url,
            method: method,
            cached: false,
            data: data
        }).done(function (data) {
            if (data.Status === 'Success') {
                toastr.success(data.Message);
                $(currentTransaction).remove();
                $(caller).popover("hide");
                reloadPartial("#fixed-table", transactionId, "Fixed");
            }
            else {
                toastr.error(data.Message);
            }
        });
    });
}

function initPopover() {

    $('[data-toggle="popover"]').each(function (e) {
        var caller = $(this);
        var action = $(caller).data('transaction-action');
        var title = "Add Price Details"
        if (action === 'cannot-fix') title = "Add Reason";


        caller.popover({
            content: function () {
                var urlAction = $(this).data("url");
                var transactionId = $(this).data('transaction-id');
                var content = '';
                $.ajax({
                    url: urlAction,
                    method: 'GET',
                    cache: false,
                    async: false,
                    data: { 'transactionId': transactionId }
                }).done(function (result) {
                    content = result;
                });
                return content;
            },
            html: true,
            trigger: 'click',
            title: title
        }).on('shown.bs.popover', function (e) {
            caller.data('bs.popover').tip().find('[data-dismiss="popover"]').on('click', function (e) {
                caller.popover('hide');
                caller.data("bs.popover").inState.click = false;
            });
            if (action === 'fix') {
                addListenerPopoverForFix(caller);
                initFirstPriceDetail(caller);
            }
            else {
                // cannot fix 
                addListnerPopoverForCannotFix(caller);
            }
        });
    });
}


$(function (e) {
    initPopover();
    initCollapse();
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