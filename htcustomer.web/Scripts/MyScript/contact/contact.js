$(function (e) {
    $("#customer-search-form").on("submit", function (e) {
        e.preventDefault();
        var submitBtn = $(this).find("button[type=submit]");
        var url = $(submitBtn).data("url");
        var data = $(this).serialize();

        $.ajax({
            url: url,
            data: data,
            method: 'GET',
            cached: false
        }).done(function (result) {
            $("#customer-search-result .row").replaceWith(result);
        }).fail(function (e) {
            alert("Error occurs");
        });
    });
});