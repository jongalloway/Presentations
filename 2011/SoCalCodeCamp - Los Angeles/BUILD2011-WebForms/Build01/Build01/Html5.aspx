<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Html5.aspx.cs" MasterPageFile="~/Site.Master"
    Inherits="Build01.Html5" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Content/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <ol>
            <li>
                <asp:Label runat="server" Text="Color:" />
                <asp:TextBox runat="server" ID="color" TextMode="Color" />
            </li>
            <li>
                <asp:Label ID="Label1" runat="server" Text="Date:" />
                <asp:TextBox runat="server" ID="TextBox1" TextMode="Date" />
            </li>
            <li>
                <asp:Label ID="Label2" runat="server" Text="Email:" />
                <asp:TextBox runat="server" ID="TextBox2" TextMode="Email" />
            </li>u
            <li>
                <asp:Label ID="Label3" runat="server" Text="Url:" />
                <asp:TextBox runat="server" ID="TextBox3" TextMode="Url" />
            </li>
        </ol>
        <asp:Button Text="Submit" runat="server" />
    </div>
</asp:Content>
