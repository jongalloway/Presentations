(function ($) {
    $.fn.extend({
        pager: function (settings) {
            var args = arguments;
            return this.each(function () {
                if (typeof settings === "object") {

                    var defaults = {
                        currentClass: "ui-state-active",
                        notCurrentClass: "ui-state-default",
                        hoverClass: "ui-state-hover",
                        pageSize: 10,
                        template: "<div style='float: left;'><span style='cursor: pointer;'>${pageNumber + 1}</span><span>&nbsp</span></div>"
                    };
                    var options = $.extend({}, defaults, settings);
                    var $this = $(this);

                    function initialize(dataSource) {
                        var refreshHandler = function () {
                            updatePager(dataSource.getTotalCount());
                        };
                        $(dataSource).bind("datasourcerefresh", refreshHandler);

                        var pageSize = options.pageSize;
                        var pageNumber;
                        var pageCount;

                        function setPage(newPageNumber, refresh) {
                            pageNumber = newPageNumber;
                            dataSource.option("paging", { skip: pageNumber * pageSize, take: pageSize, includeTotalCount: true });
                            if (refresh) {
                                dataSource.refresh();
                            }
                            updatePager();
                        };

                        function updatePager(totalItemCount) {
                            if (totalItemCount !== undefined && totalItemCount !== null) {
                                var newPageCount = totalItemCount === 0 ? 1 : Math.ceil(totalItemCount / pageSize);
                                if (newPageCount !== pageCount) {
                                    pageCount = newPageCount;
                                    $this.empty();

                                    for (var i = 0; i < pageCount; i++) {
                                        var addPageButton = function (pageClicked) {
                                            $($(options.template).render({ pageNumber: i, pageSize: pageSize }))
                                                .appendTo($this)
                                                .addClass("pager-page-button").data("pageNumber", pageClicked)
                                                .click(function () {
                                                    if (pageNumber !== pageClicked) {
                                                        setPage(pageClicked, true);
                                                    }
                                                })
                                                .hover(function () { $(this).addClass(options.hoverClass); }, function () { $(this).removeClass(options.hoverClass); });
                                        };
                                        addPageButton(i);
                                    }
                                }
                                if (pageNumber >= pageCount) {
                                    // On last refresh, the item count decreased such that we're positioned past the last page.
                                    // Put us on the _new_ last page.
                                    pageNumber = pageCount - 1;
                                    setPage(pageNumber, true);
                                }
                            }

                            var $pageButtons = $this.children(".pager-page-button");
                            var $currentPageButton = $pageButtons.filter(function () {
                                return $(this).data("pageNumber") === pageNumber;
                            });
                            $pageButtons.not($currentPageButton).removeClass(options.currentClass).addClass(options.notCurrentClass);
                            $currentPageButton.removeClass(options.notCurrentClass).addClass(options.currentClass);
                        };

                        function destroy() {
                            if (refreshHandler) {
                                $(dataSource).unbind("datasourcerefresh", refreshHandler);
                                $this.children(".pager-page-button").remove();
                                refreshHandler = null;
                            }
                        };

                        $.extend($this.data("__pager__"), {
                            setPage: function (newPageNumber, refresh) {
                                setPage(newPageNumber, refresh);
                            },
                            destroy: function () {
                                destroy();
                            }
                        });

                        setPage(0, false);  // Prime the data source with paging options.  Initial enabling/disabling of paging buttons.
                    };

                    $(this).data("__pager__", {
                        initialize: function (dataSource) {
                            initialize(dataSource);
                        }
                    });

                    var dataSource = options.dataSource;
                    if (dataSource) {
                        initialize(dataSource);
                    }

                } else if (typeof settings === "string") {

                    // Setting options...

                    var pager = $(this).data("__pager__");

                    var option = settings;
                    if (option === "setPage") {
                        var optionValue = args[1];

                        var newPageNumber;
                        var refresh;
                        if (typeof optionValue === "object") {
                            newPageNumber = optionValue.pageNumber || 0;
                            refresh = optionValue.refresh === undefined ? false : optionValue.refresh;  // Default is "false";
                        } else {
                            newPageNumber = optionValue || 0;
                        }

                        pager.setPage(newPageNumber, !!refresh);
                    } else if (option === "destroy") {
                        if (pager && pager.destroy) {
                            pager.destroy();
                        }
                    } else if (option === "option") {
                        if (args[1] !== "dataSource") {
                            throw "Unrecognized pager option";
                        } else {
                            if (pager.destroy) {
                                pager.destroy();
                            }
                            pager.initialize(args[2]);
                        }
                    }
                }

                return this;
            });
        }
    });
})(jQuery);
