$(function () {
    $(".category").on('click', function () {
        var categoryId = $(this).attr('categoryId')
        $.ajax({
            url: "/shop/FilterProduct",
            method: "GET",
            data: {
                categoryId: categoryId
            },
            contentType: "application/json",
            success: function (response) {
                    $('#product').html(response)
            }
        })
    })
})