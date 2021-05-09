$(function () {
    $('#txtimgurl').change(function () {
        $('#uimgurl').hide();
        $('#uimgurl').attr('src', $('#txtimgurl').val())
    })
})