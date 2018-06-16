$(function () {
    $(document).on('click', ".btn-deliver-transaction", function () {
        var url = $(this).data("url");
        var id = $(this).data("id");
        $.ajax({
            type: "GET",
            url: url,
            cache: false
        }).done(function (data) {
            if (data.Status === "Failure") {
                toastr.error(data.Message);
            } else {
                var $transaction = $("#transaction-detail-" + id);
                var $count = $transaction.closest(".panel").find(".badge");
                var currentCount = parseInt($count.html());
                $count.html(currentCount - 1);
                $transaction.remove();
                if (data.Status === "Success") {
                    toastr.success(data.Message);
                }
                if (data.Status == null || data.Status == undefined) {
                    console.log(data);
                    $("#delivered-panel").find("tbody").append(data);
                    var $count = $("#delivered-panel").closest(".panel").find(".badge");
                    var currentCount = parseInt($count.html());
                    $count.html(currentCount + 1);
                    toastr.success("Success");
                }
                
            }
        });
    });
})