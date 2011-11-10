(function ($) {

    $.widget("ria.list", {
        options: {
            data: null,
            template: "<div>Supply a template</div>",
            templateOptions: null,
            itemAdded: null
        },
        _create: function () {
            var data = this.options.data,
                template = $.template(this.options.template),
                templateOptions = this.options.templateOptions,
                that = this,
                context = $.extend({}, templateOptions, {
                    afterCreate: function (itemView) {
                        if (that.options.itemAdded) {
                            that.options.itemAdded(itemView.data, itemView.nodes);
                        }
                    }
                });
            this.element.link(data, template, context);
            this._view = this.element.view(true);
        },
        _destroy: function () {
            // TODO: NYI by JsViews
            // this.element.unlink();
        },
        dataForNode: function (node) {
            return $(node).view().data;
        },
        nodeForData: function (data) {
            // Assumes that data is rendered only once in this list.  Assumes that the rendering has a single root node (not a forest).
            return this.nodesForData(data)[0][0];
        },
        nodesForData: function (data) {
            var itemViews = this._view.views,
                nodes = [];
            for (var i = 0; i < this._view.views.length; i++) {
                var itemView = itemViews[i];
                if (itemView.data === data) {
                    nodes.push(itemView.nodes);
                }
            }
            return nodes;
        }
    });

    // jQuery UI's widget factory deeply copies options, causing the list control to data-bind to
    // a copy of our data array.  This is being fixed in jQuery UI.  For now, here's a workaround.    
    var oldCtor = $.ria.list;
    $.ria.list = function (options, element) {
        var oldCreate = this._create,
            data = options.data;
        this._create = function () {
            this.options.data = data;
            return oldCreate.apply(this, arguments);
        };
        return oldCtor.apply(this, arguments);
    };
    $.ria.list.prototype = oldCtor.prototype;
    $.widget.bridge("list", $.ria.list);

})(jQuery);
