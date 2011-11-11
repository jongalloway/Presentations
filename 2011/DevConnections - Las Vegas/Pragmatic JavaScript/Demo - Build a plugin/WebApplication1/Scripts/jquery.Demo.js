/// <reference path="jquery-1.6.4.js" />
(function ($) {

    

    $.fn.demo = function (options) {
        var settings = {
            color: "red"
        };

        $.extend(settings, options);

        // refer to the collection demo was called on via 'this'
        this.css("background", settings.color);

    };


} (window.jQuery));


