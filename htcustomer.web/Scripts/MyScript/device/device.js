function filterTransaction() {
    var datePicker = $('#dateFilter');
    var datetime = new Date($(datePicker).val());
    var month = datetime.getMonth() + 1;
    var year = datetime.getFullYear();
    var categoryId = $('#selectDevice option:selected').attr('id');
    var url = $(datePicker).data('url');
    var statusId = $(datePicker).data('status-id');
    var replacedContent = $('#transaction-wrapper .panel');

    $.ajax({
        url: url,
        method: 'get',
        cache: false,
        data: {
            statusId: statusId,
            month: month,
            year: year,
            categoryId: categoryId
        },
    }).done(function (result) {
        $(replacedContent).replaceWith(result);
        initPopover();
    });
}

function loadCategoryList() {
    var select = $('#selectDevice');
    var url = $(select).data('url');

    $.ajax({
        url: url,
        cache: false,
        method: 'GET'
    }).done(function (data) {
        if (data !== null) {
            $(select).append($(`<option>All</option>`));
            data.forEach(function (category) {
                $(select).append($(`<option id=${category.CategoryID}>${category.Name}</option>`));
            });
        }
        else {
            toastr.warning("Something went wrong with load category list");
        }
    }).fail(function (message) {
        toastr.error("Error during loading cateogy list");
    });
}

function DeliverTransaction() {

}
function reIndexPriceDetail(currentWrapper) {
    $(currentWrapper).children(".form-group").toArray().forEach(function (group, index) {
        $($(group).children(".form-control")[0]).attr("name", "PriceList[" + index + "].Description");
        $($(group).children(".form-control")[1]).attr("name", "PriceList[" + index + "].Price")
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



$(function () {
    initPopover();
    // load all category 
    loadCategoryList();

    $('.date-picker').datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        dateFormat: 'MM yy',
        onClose: function (dateText, instant) {
            $(this).datepicker('setDate', new Date(instant.selectedYear, instant.selectedMonth, 1));
            // Calling ajax function 
            filterTransaction();
        },
    });
    $('.date-picker').datepicker('setDate', new Date());

    $('.filter-tab').on('click', function () {
        var statusId = $(this).data('status-id');
        $('#dateFilter').data('status-id', statusId);

        filterTransaction();
    });

    $('#selectDevice').on('change', function () {
        filterTransaction();
    });
});


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
            }
            else {
                toastr.error(data.Message);
            }
        });
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
            }
            else {
                toastr.error(data.Message);
            }
        });
    });
}

function initFirstPriceDetail(caller) {
    var transactionId = $(caller).data('transaction-id');
    var removeBtn = $(`#remove-btn-0-${transactionId}`);
    $(removeBtn).on('click', function (e) {
        e.preventDefault();
        removePriceDetail(this);
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






