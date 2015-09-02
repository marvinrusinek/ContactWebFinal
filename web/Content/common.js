$(function () {
    var postForm = function () {
        if ($(this).attr("disabled"))
            return false;
        var form = $(this).closest("form");
        var container = $(this).closest("#formContainer");
        $.post(
                $(this).attr("href"),
                form.serialize(),
                function (html) {
                    container.replaceWith($(html));
                }
            );
        return false;
    }

    $("Form[name=EntryForm] a.postLink").live("click", function () {
        return postForm.apply(this);
    });

    var selects = $("Form[name=EntryForm] select");
    selects.live("change", function () {
        return postForm.apply(this);
    });
});