// Showing Loading Icon when searching customer : 
// isShowed = true -> show else hide
function toggleLoadingCustomerIcon(isShowed) {
    if (isShowed) {
        $("#loading-customer").css("display", "block");
        $(".address-book").css("opacity","0.3");
    }
    else {
        $("#loading-customer").css("display", "none");
        $(".address-book").css("opactiy","1");
    }    
}
$(function (e) {
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
});