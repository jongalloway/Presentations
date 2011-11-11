(function ($) {
    $.fn.extend({
        starRating: function (settings) {
            if (typeof settings === "number") {
                settings = { initialRating: settings };
            }

            var defaults = {
                initialRating: 0,
                maxRating: 5
            };
            var options = $.extend({}, defaults, settings);
            var $this = this;

            return $this.each(function () {
                for (var i = 1; i <= options.maxRating; i++) {
                    $this.append(i <= options.initialRating ? "<td class='star-rating-element star-rating-element-selected'/>" : "<td class='star-rating-element'/>");
                }
                var $starsCtrl = $(".star-rating-element", $this).click(click).mouseenter(mouseenter).mouseleave(mouseleave);

                function click() {
                    $(this).nextAll().removeClass('star-rating-element-selected').end().prevAll().andSelf().addClass('star-rating-element-selected');
                    $this.triggerHandler("ratingChanged", { rating: $starsCtrl.index(this) + 1 });
                }
                function mouseenter() { $(this).prevAll().andSelf().addClass('star-rating-element-hover'); }
                function mouseleave() { $(this).prevAll().andSelf().removeClass('star-rating-element-hover'); }
            });
        }
    });
})(jQuery);
