/// <reference path="jquery-1.6.4.js" />
(function ($) {
    /// <param name="$" type="jQuery" />

    $.fn.demo = function (settings) {
        var config = {
            color: "red"
        };

        $.extend(config, settings);

        this.click(function () {
            $(this).css({ background: config.color });
        });

        

    };

})(jQuery);