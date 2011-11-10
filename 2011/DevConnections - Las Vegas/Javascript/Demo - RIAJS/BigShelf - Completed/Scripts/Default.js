$(document).ready(function () {

    var serviceUrl = "BigShelf-BigShelfService.svc";

    var remoteBooks = [];
    $([remoteBooks]).dataSource({
        serviceUrl: serviceUrl,
        queryName: "GetBooksForSearch",
        bufferChanges: false
    });
    var books = [];
    $([books]).dataSource({
        inputData: remoteBooks
    }); 

    var flaggedBooks;
    $.dataSource({
        serviceUrl: serviceUrl,
        queryName: "GetProfileForSearch",
        refresh: function (profiles) {
            flaggedBooks = profiles[0].get_FlaggedBooks();
            render();
        }
    }).refresh();

    function render() {

        // A list UI control over our "books" array.
        var booksList = $("#books").list({
            data: books,
            template: $("#bookTemplate"),
            itemAdded: function (book, elements) {
                // Bind edit controls on each book element to a FlaggedBook entity for "profile".
                enableFlaggingForBook(book, elements[0]);
            }
        }).data("list");
        booksList._view.render();

        // Our "Sort By:" sort control.
        $(".sortButton").click(function () {
            var newSort = $(this).data("book-sort"),
                $currentSortElement = $(".sortButton.selected"),
                currentSort = $currentSortElement.data("book-sort");
            if (newSort !== currentSort) {
                // Only clicked sort button gets the "selected" class.
                $currentSortElement.removeClass("selected");
                $(this).addClass("selected");

                // Refresh those books displayed, based on the new sort.
                refreshBooksList();
            }
        })
        .eq(0).addClass("selected");

        // A "search" text box to do substring searches on book title.
        $("#searchBox").autocomplete({
            source: function () {
                // A pause in typing in our search control should refresh the search results.
                refreshBooksList();
            },
            minLength: 0
        }).watermark();

        $("#pager").pager({
            dataSource: $([books]).dataSource(),
            pageSize: 6
        });

        refreshBooksList(true);

        //
        // Helper functions
        //

        function refreshBooksList(all) {
            var dataSource = $([books]).dataSource();

            var filter = {
                property: "Title",
                operator: "Contains",
                value: $("#searchBox").val() || ""
            };
            dataSource.option("filter", filter);

            dataSource.option("sort", { property: $(".sortButton.selected").text() });

            dataSource.refresh({ all: all });
        };

        function enableFlaggingForBook(book, bookElement) {
            var flaggedBook = getFlaggedBook(book),  // Will be null if current profile hasn't yet saved/rated this book.
                $button = $("input:button[name='status']", bookElement),
                ratingChanged;

            if (flaggedBook) {
                // Style the Save button based on initial flaggedBook.Rating value.
                styleSaveBookButton();

                // Clicks on the star rating control are translated onto "flaggedBook.Rating".
                ratingChanged = function (event, value) {
                    $.observable(flaggedBook).setProperty("Rating", value.rating);
                    styleSaveBookButton();
                };
            } else {
                // If this book has not yet been flagged by the user create a new flagged book 
                flaggedBook = { BookId: book.Id, Rating: 0 };

                // Clicking on the Save button will add the new flagged book entity to "flaggedBooks".
                $button.click(function () {
                    $.observable(flaggedBooks).insert(0, flaggedBook);
                    styleSaveBookButton();
                });

                // Clicks on the star rating control are translated onto "flaggedBook.Rating". Also, since the book
                // was not previously flagged, this will also add a new flagged book entity to "flaggedBooks".
                ratingChanged = function (event, value) {
                    $.observable(flaggedBook).setProperty("Rating", value.rating);
                    $.observable(flaggedBooks).insert(0, flaggedBook);
                    styleSaveBookButton();
                };
            }

            // Bind our ratingChanged method to the appropriate event from the starRating control
            $(".star-rating", bookElement)
                .starRating(flaggedBook.Rating)
                .bind("ratingChanged", ratingChanged);

            function styleSaveBookButton() {
                $button
                    .val(flaggedBook.Rating > 0 ? "Done reading" : "Might read it")
                    .removeClass("book-notadded book-saved book-read")
                    .addClass(flaggedBook.Rating > 0 ? "book-read" : "book-saved")
                    .disable();
            };
        };

        function getFlaggedBook(book) {
            return $.grep(flaggedBooks, function (myFlaggedBook) {
                return myFlaggedBook.BookId === book.Id;
            })[0];
        };
    };
});
