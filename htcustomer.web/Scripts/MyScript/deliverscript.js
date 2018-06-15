$(function () {
    $(document).on('click', ".btn-deliver-transaction", function () {
        var url = $(this).data("url");
        var id = $(this).data("id");
        $.ajax({
            type: "GET",
            url: url,
            cache: false
        }).done(function (data) {
            if (data.Status === "Success") {
                var $transaction = $("#transaction-detail-" + id);
                var $count = $transaction.closest(".panel").find(".badge");
                var currentCount = parseInt($count.html());
                $count.html(currentCount - 1);
                $transaction.remove();
                toastr.success(data.Message);
            } else {
                toastr.error(data.Message);
            }
        });
    });
})