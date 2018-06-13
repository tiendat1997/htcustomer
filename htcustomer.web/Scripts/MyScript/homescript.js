function loadCategoryList() {
    var select = $('#device-select');
    var url = $(select).data('url');
    var categoryId = $('#reload-category-id').val();

    $.ajax({
        method: 'GET',
        url: url,
        cached: false
    }).done(function (data) {
        if (data.length > 0) {
            data.forEach(function (category) {
                $(select).append(`<option value=${category.CategoryID}>${category.Name}</option>`);
            });
            // selected : 
            if (categoryId !== null || categoryId.length > 0) {
                $('#device-select').val(categoryId).attr('selected', 'selected');
            }
            else {
                $('#device-select').children[0].attr('selected', 'selected');
            }
        }
    });            
}
function resetNewTransactionForm() {
    var form = $('#transaction-info-form');    
    $(form).find('#Error').val(null);
    $(form).find('#Description').val(null);
}

function closeModal(form) {
    var parentModal = $(form).closest('.modal');
    $(parentModal).modal('hide');
    initForm();    
}

function initForm() {
    $('#customer-info label i').css('display', 'none');
    $('#not-found-search').removeClass('alert alert-warning').text('');
    $('#transaction-info-form').trigger('reset');
    $('#transaction-info-form').find('input').val(null);
    resetNewTransactionForm();
}

function addEventListenerForNewTransaction() {
    $('#add-transaction-btn').on('click', function () {        
        initForm();
    });

    $('#add-customer-btn').click(function (e) {
        e.preventDefault();
        var btn = this;
        var url = $(btn).data('url');
        var data = $('#transaction-info-form').serialize();
        var modalBody = $('#add-transaction-modal .modal-body .inner');

        $.ajax({
            url: url,
            method: 'POST',
            cached: false,
            data: data,
        }).done(function (data) {
            modalBody.replaceWith(data);
        });
    });

    $("#customer-chooser").autocomplete({
        minLength: 1,
        source: function (request, response) {
            // CALLING AJAX
            var cusChooser = $('#customer-chooser');
            var searchVal = request.term; 
            var url = $(cusChooser).data('url');

            $.ajax({
                url: url,
                method: 'GET',
                cached: false,
                data: {
                    'searchCustomer': searchVal,
                    'callFromHome': true
                },
            }).done(function (data) {
                var customData = data.map(function (customer) {
                    return {
                        customerId: customer.CustomerID,
                        label: `${customer.Name} (${customer.Description})`,
                        phone: customer.Phone,
                        description: customer.Description
                    };
                });
                response(customData);
            });

        },
        focus: function (event, ui) {
            $("#Customer_CustomerID").val(ui.item.customerId);
            $("#customer-chooser").val(ui.item.value);
            return false;
        },
        select: function (event, ui) {
            //$("#customer").val(ui.item.value);
            //$("#customer-id").val(ui.item.value);
            //$("#customer-phone").html(ui.item.desc);                  
            return false;
        },
        response: function (event, ui) {
            if (ui.content.length === 0) {
                $("#not-found-search").attr('class', 'alert alert-warning').text("No results found ! Please add new customer...");
                $("#add-customer-btn").css("display", "inline-block");
                $("#Customer_CustomerID").val(null);                
            }
            else {
                $("#not-found-search").removeClass('alert alert-warning').text("");
                $("#add-customer-btn").css("display", "none");
            }
        }
    });

    $('#transaction-info-form').on('submit', function (e) {
        e.preventDefault();
        var form = this;
        var url = $(form).attr('action');
        var data = $(form).serialize();
        var method = $(form).attr('method');
        var modalBody = $('#add-transaction-modal .modal-body .inner');
        var foundCustomer = $('#Customer_CustomerID');

        if ($(foundCustomer).val() === "" || $(foundCustomer).val() === null) {
            toastr.warning("Please choose customer");
            return;
        }
        
        $.ajax({
            url: url,
            method: method,
            cached: false,
            data: data
        }).done(function (message) {
            if (message.Status === 'Success') {
                closeModal(form);
                toastr.success(message.Message);
            }
            else {
                toastr.warning(message.Message);
            }
        });
        
    });
}

function addListenerForAddForm() {

    $('#add-customer-form input[type=submit]').on('click', function (e) {
        var submitBtn = $(this);
        var form = $('#add-customer-form');
        var isCanceled = $(submitBtn).val() === 'Cancel';
        if (isCanceled) {
            $(form).find('#is-cancel').val(true);
            // remove required field 
            $(form).find('input').attr('required', null).attr('pattern', null);
        }                
    });

    $('#add-customer-form').on('submit', function (e) {
        e.preventDefault();
        var form = this;
        var url = $(form).attr('action');
        var data = $(form).serialize();               
        var method = $(form).attr('method');
        var modalBody = $('#add-transaction-modal .modal-body .inner');
        var isCancel = $('#is-cancel').val();

        $.ajax({
            url: url,
            method: method,
            cached: false,
            data: data   
        }).done(function (data) {
            if (data.Status === undefined || data.Status === null) {
                $("#reload-category-id").val($('#Category_CategoryID').val());                
                // success: return partial view 
                modalBody.replaceWith(data);
                if (isCancel === true)
                    $('#customer-chooser').val(null);

                addEventListenerForNewTransaction();
                loadCategoryList();
            } else {
                toastr.warning(data.Message);
            }
        });
    })
}

$(function () {
    $('[data-toggle="tooltip"]').tooltip();   
    addEventListenerForNewTransaction();
    loadCategoryList();
        
});

function addDevice(customerId) {
    alert("Add Device: " + customerId);
}

function seeDetails(customerId) {
    alert("See Details: " + customerId);
}