<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="BigShelf._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Big Shelf - Read books, share books.</title>
    <!-- Frameworks -->
    <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.6.3.js" type="text/javascript"></script>
    <script src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <!-- jquery plugins -->
    <script src="Scripts/jquery.render.js" type="text/javascript"></script>
    <script src="Scripts/jquery.views.js" type="text/javascript"></script>
    <script src="Scripts/jquery.bb.watermark.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.disable.js" type="text/javascript"></script>
    <!-- Client library code -->
    <script src="Scripts/ria.js" type="text/javascript"></script>
    <!-- App scripts -->
    <script src="Scripts/jquery.pager.js" type="text/javascript"></script>
    <script src="Scripts/jquery.starrating.js" type="text/javascript"></script>
    <script src="Scripts/jquery.list.js" type="text/javascript"></script>
    <!-- Templates -->
    <script id="friendsListTemplate" type="text/x-jquery-tmpl">
        <td class="friendButton selected">${get_FriendProfile().Name}</td>
        <td width='10px'></td>
    </script>
    <script id="bookTemplate" type="text/x-jquery-tmpl">
        <tr class='book-item'>
            <td valign="top"><img src="${'http://images.amazon.com/images/P/' + ASIN + '.01._AA100_PU_PU-5_.jpg'}" /></td>
            <td width="4px"></td>
            <td valign="top">
                <table>
                    <tr>
                        <td class="book-title">${Title}</td>
                    </tr>
                    <tr>
                        <td class="book-author">by ${Author}</td>
                    </tr>
                    <tr>
                        <td class="star-rating"></td>
                    </tr>
                    <tr class="save-button">
                        <td><input type="button" name="status" value="Save" class="book-button book-notadded"/></td>
                    </tr>
                </table>
            </td>
        </tr>
    </script>
    <script id="pageNumberTemplate" type="text/x-jquery-tmpl">
        <td class="pagerButton">${pageNumber * pageSize + 1}-${(pageNumber + 1) * pageSize}</td>
        <td width='10px'></td>
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:LoginView ID="LoginView1" runat="server" EnableViewState="false">
        <AnonymousTemplate>
            <div class="anonymousContent">
                Welcome to Big Shelf! Login first to access your personalized shelf.
            </div>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <!-- Default.js makes AJAX requests that require authentication and should only be included on authenticated pages -->
            <script src="Scripts/Default.js" type="text/javascript"></script>
            <div id="content">
                <div class="catalogFilter disabled" id="booksFilter">
                    <table border="0">
                        <tr>
                            <td style="color:#999999; padding: 4px 4px 4px 4px">Show Me</td>
                            <td width="4px"></td>
                            <td class="filterButton" data-book-view="1">All</td>
                            <td width="4px"></td>
                            <td class="filterButton" data-book-view="2">My books</td>
                            <td width="4px"></td>
                            <td class="filterButton" data-book-view="3">Just friends</td>
                        </tr>
                    </table>
                </div>

                <div class="friendsList">
                    <table border="0">
                        <tr id="friendsList" style="display: none">
                            <td style="color:#999999; padding: 4px 4px 4px 4px">Show Friends</td>
                            <td width="4px"></td>
                        </tr>
                    </table>
                </div>

                <div class="search"><input id="searchBox" type="text" size="25" title="search books..." /></div>

                <div class="sortBy" id="sortBy">
                    <table border="0">
                        <tr>
                            <td style="color:#999999; padding: 4px 4px 4px 4px">Sort By</td>
                            <td width="4px"></td>
                            <td class="sortButton" data-book-sort="1">Title</td>
                            <td width="4px"></td>
                            <td class="sortButton" data-book-sort="2">Author</td>
                        </tr>
                    </table>
                </div>

                <div class="catalog">
                    <table border="0">
                        <tbody id="books"></tbody>
                    </table>
                </div>

                <div class="catalogNavigation">
                    <table border="0">
                        <tr id="pager"></tr>
                    </table>
                </div>
            </div>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
