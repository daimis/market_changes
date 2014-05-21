$(function () {
    $("ul.main1 li").click(function (event) {
        event.preventDefault();
        $(this).find("li").toggle();
        $(this).find("li.m3").hide();
    });
});