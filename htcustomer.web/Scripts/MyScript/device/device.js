$(function () {
    $('.date-picker').datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        dateFormat: 'MM yy',
        onClose: function (dateText, inst) {
            $(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, 1));
        }
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




