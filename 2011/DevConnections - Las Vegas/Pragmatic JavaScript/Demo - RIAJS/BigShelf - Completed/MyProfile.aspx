<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MyProfile.aspx.cs" Inherits="BigShelf.MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Big Shelf - Update your Profile</title>

    <!-- Frameworks -->
    <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.6.3.js" type="text/javascript"></script>
    <script src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/base/jquery-ui.css" />
    <!-- jquery plugins -->
    <script src="Scripts/jquery.render.js" type="text/javascript"></script>
    <script src="Scripts/jquery.views.js" type="text/javascript"></script>
    <script src="Scripts/jquery.bb.watermark.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.disable.js" type="text/javascript"></script>
    <script src="http://ajax.microsoft.com/ajax/jquery.validate/1.6/jquery.validate.js" type="text/javascript"></script>
    <!-- Client library code -->
    <script src="Scripts/ria.js" type="text/javascript"></script>
    <!-- App scripts -->
    <script src="Scripts/jquery.list.js" type="text/javascript"></script>
    <script src="Scripts/MyProfile.js" type="text/javascript"></script>

    <script id="profile-friend-template" type="text/x-jquery-tmpl">
        <li class="friend-item"><span>${$ctx.getFriendName($data)}</span><span class="revertDelete">&nbsp;</span></li>
    </script>

        <script type="text/javascript">
            // Needed by declarative binding of profileName below.
            function firstName(name) {
                return name.split(" ")[0];
            };
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="profileTitle">
        <span>Hey <span id="profileName" data-getFrom="firstName([Name])">friend</span>, update your profile below</span>
    </div>

    <div id="profileContainer">
        <form id="profileForm">
            <table border="0">
                <tr class="profile-property">
                    <td align="right" class="profile-property-label">Name</td>
                    <td width="7px"></td>
                    <td>
                        <div>
                        <input type="text" name="Name" class="profile-property-input" data-to="[Name]" data-getFrom="[Name]"/>
                        <span class="revertDelete moveIntoInputBox">&nbsp;</span>
                        </div>
                    </td>
                </tr>
                <tr><td height="10"></td></tr>    
                <tr class="profile-property">
                    <td align="right" class="profile-property-label">Email Address</td>
                    <td width="7px"></td>
                    <td>
                        <div>
                            <input type="text" name="EmailAddress" class="profile-property-input" data-to="[EmailAddress]" data-getFrom="[EmailAddress]"/>
                            <span class="revertDelete moveIntoInputBox">&nbsp;</span>
                        </div>
                    </td>
                </tr>
            </table>
        </form>
        <table border="0">
            <tr><td colspan="3" height="10"><img src="Styles/Seperator.png" /></td></tr>     
            <tr>
                <td align="right" class="profile-property-label">Friends</td>
                <td width="7px"></td>
                <td>
                    <div id="profile-friends-wrapper">
                        <ul id="friend-list"></ul>
                    </div>
                </td>
            </tr>  
            <tr>
                <td height="10"/>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td id="add-friend">
                    <input id="add-friend-text" type="text" class="profile-property-input" title="add friend..." />
                    <button type="button" class="profile-button" id="add-friend-button" disabled="disabled">Add Friend</button>
                </td>
            </tr>
            <tr><td height="20"></td></tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <table border="0">
                        <tr>
                            <td><button disabled="disabled" type="button" id="submit" class="profile-button">Update Profile</button></td> 
                            <td width="14px"></td> 
                            <td><button disabled="disabled" type="button" id="cancel" class="profile-button">Cancel</button></td> 
                        </tr>
                    </table>                
                </td>
            </tr>
        </table>  
    </div>
</asp:Content>
