﻿(function ($) {

    $.fn.pager = function (options) {

        var opts = $.extend({}, $.fn.pager.defaults, options);

        return this.each(function () {

            // empty out the destination element and then render out the pager with the supplied options
            $(this).empty().append(renderpager(parseInt(options.pagenumber), parseInt(options.pagecount), options.buttonClickCallback));

            // specify correct cursor activity
            $('.pages li').mouseover(function () { document.body.style.cursor = "pointer"; }).mouseout(function () { document.body.style.cursor = "auto"; });
        });
    };

    // render and return the pager with the supplied options
    function renderpager(pagenumber, pagecount, buttonClickCallback) {

        // setup $pager to hold render
        var $pager = $('<ul class="pages"></ul>');

        // add in the previous and next buttons
        $pager.append(renderButton('首页', pagenumber, pagecount, buttonClickCallback)).append(renderButton('上一页', pagenumber, pagecount, buttonClickCallback));

        var startPoint = 1;
        var endPoint = 9;

        if (pagenumber > 4) {
            startPoint = pagenumber - 4;
            endPoint = pagenumber + 4;
        }

        if (endPoint > pagecount) {
            startPoint = pagecount - 8;
            endPoint = pagecount;
        }

        if (startPoint < 1) {
            startPoint = 1;
        }

        for (var page = startPoint; page <= endPoint; page++) {

            var currentButton = $('<li class="page-number">' + (page) + '</li>');

            page == pagenumber ? currentButton.addClass('pgCurrent') : currentButton.click(function () { buttonClickCallback(this.firstChild.data); });
            currentButton.appendTo($pager);
        }
        $pager.append(renderButton('下一页', pagenumber, pagecount, buttonClickCallback)).append(renderButton('末页', pagenumber, pagecount, buttonClickCallback));

        return $pager;
    }

    function renderButton(buttonLabel, pagenumber, pagecount, buttonClickCallback) {
        var $Button = $('<li class="pgNext">' + buttonLabel + '</li>');
        var destPage = 1;
        switch (buttonLabel) {
            case "首页":
                destPage = 1;
                break;
            case "上一页":
                destPage = pagenumber - 1;
                break;
            case "下一页":
                destPage = pagenumber + 1;
                break;
            case "末页":
                destPage = pagecount;
                break;
        }

        if (buttonLabel == "首页" || buttonLabel == "上一页") {
            pagenumber <= 1 ? $Button.addClass('pgEmpty') : $Button.click(function () { buttonClickCallback(destPage); });
        }
        else {
            pagenumber >= pagecount ? $Button.addClass('pgEmpty') : $Button.click(function () { buttonClickCallback(destPage); });
        }

        return $Button;
    }
 
    $.fn.pager.defaults = {
        pagenumber: 1,
        pagecount: 1
    };

})(jQuery);