$(document).ready(function (e) {
    $('[data-toggle="tooltip"]').tooltip();   
    $('#add-customer-btn').click(function (e) {
        e.preventDefault();
        $("#add-device-modal").modal("hide");
        $("#add-customer-modal").modal("show");    
    });
});

$(function () {
    var customers = [
        {
            customerId: "cust01",
            label: "Tien Dat",
            description: "Dai Truyen Hinh",
            phone: "0123456789",
        },
        {
            customerId: "cust02",
            label: "Huy Thong",
            description: "Dai Truyen Hinh",
            phone: "01218351464",
        },
        {
            customerId: "cust03",
            label: "Thuy Ngoc ",
            description: "Dai Truyen Hinh",
            phone: "0123456789",
        },
        {
            customerId: "cust04",
            label: "Bach Tuyet",
            description: "Dai Truyen Hinh",
            phone: "0995539831",
        },
        {
            customerId: "cust05",
            label: "Huy Vu",
            description: "Dai Truyen Hinh",
            phone: "0123345566",
        }
    ]
    
    $("#customer").autocomplete({
        minLength: 3,
        source: customers,
        focus: function (event, ui) {
            var customerInfo = ui.item.value + " - " + ui.item.description;
            $("#customerId").val(ui.item.customerId);
            $("#customer").val(customerInfo);
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
                $("#not-found-search").text("No results found ! Please add new customer...");
                $("#add-customer-btn").css("display", "block");
            }
            else {
                $("#not-found-search").text("");
                $("#add-customer-btn").css("display", "none");
            }
        }
    })
        //.autocomplete("option", "appendTo", ".inform")
        //.autocomplete("instance")._renderItem = function (ul, item) {
        //    var row = $("<div>").addClass("row");
        //    $("<div>").addClass("col-md-6").text(item.label).appendTo(row);
        //    $("<div>").addClass("col-md-6").text(item.description).appendTo(row);

        //    return $("<li>")
        //        .append(row)
        //        .appendTo(ul);
        //};
});

function addDevice(customerId) {
    alert("Add Device: " + customerId);
}

function seeDetails(customerId) {
    alert("See Details: " + customerId);
}