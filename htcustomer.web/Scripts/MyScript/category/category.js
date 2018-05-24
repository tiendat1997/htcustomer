$(function (e) {
    $(".btn-delete-category").on('click', function () {
        var id = $(this).attr('data-id');
        DeleteCategory(id);
    });

    $(".btn-edit-category").on('click', function () {
        var id = $(this).attr('data-id');

        //change td to input
        var tdCategoryName = $(".td-category-name-" + id);
        var categoryName = tdCategoryName.html();
        var inputCategoryName = $('<input type="text"></input>');
        inputCategoryName.addClass("form-control category-"+id+"-name");
        inputCategoryName.prop('required',true);
        inputCategoryName.val(categoryName);
        tdCategoryName.empty();
        tdCategoryName.append(inputCategoryName);

        //replace EDIT button with SAVE button
        var btnSaveCategory = $("<button></button>");
        btnSaveCategory.addClass("btn btn-sm btn-success btn-save-category");
        btnSaveCategory.attr('data-id', id);
        btnSaveCategory.append("<i class='fa fa-save'></i>");
        btnSaveCategory.on('click', function () {
            var id = $(this).attr('data-id');
            var name = $(".category-" + id + "-name").val();
            UpdateCategory(id, name);
        }); 

        $(this).replaceWith(btnSaveCategory);

    })

});

function DeleteCategory(id) {
    var request = $.ajax({
        type: 'POST',
        url: 'Category/Delete',
        data: {
            categoryId: id
        }
    });

    request.done(function (response) {
        $("#" + id).remove();
    });

    request.fail(function (response) {
        console.log(response);
    });
}

function UpdateCategory(id, name) {
    var request = $.ajax({
        type: 'POST',
        url: 'Category/Update',
        data: {
            CategoryID: id,
            Name: name
        }
    });

    request.done(function (response) {
        window.location.reload();
    });

    request.fail(function (response) {
        console.log(response);
    });
}
