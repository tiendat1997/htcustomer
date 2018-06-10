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
            categoryId : categoryId
        },
    }).done(function (result) {
        $(replacedContent).replaceWith(result);
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
        if (data != null) {
            $(select).append($(`<option>All</option>`));
            data.forEach(function (category) {
                $(select).append($(`<option id=${category.CategoryID}>${category.Name}</option>`));
            });
        }
        else {
          
        }
    }).fail(function (message) {
        toastr.error("Error during loading cateogy list");
    });
}

function DeliverTransaction() {

}

$(function () {
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

    $(document).on('click', ".btn-deliver-transaction",function () {
        var url = $(this).data("url");
        var id = $(this).data("id");
        $.ajax({
            type: "GET",
            url: url,
            cache: false
        }).done(function (data) {
            if (data.Status === "Success") {
                $("#transaction-detail-" + id).remove();
                toastr.success(data.Message);
            } else {
                toastr.error(data.Message);
            }
            });
    });
});

$(function () {
    $('[data-toggle="popover"]').each(function (e) {      
        var button = $(this);       

        button.popover({
            content: function () {
                var urlAction = $(this).data("url");
                var content = '';
                $.ajax({
                    url: urlAction,
                    method: 'GET',
                    cache: false,
                    async: false,
                    data: {}
                }).done(function (result) {
                    content = result;
                });
                return content;
            },
            html: true,
            trigger: 'click',
            title: 'Price Details: Device_Name'
        }).on('shown.bs.popover', function (e) {          
            button.data('bs.popover').tip().find('[data-dismiss="popover"]').on('click', function (e) {
                button.popover('hide');     
                button.data("bs.popover").inState.click = false;
            });
        });
    }); 
});




