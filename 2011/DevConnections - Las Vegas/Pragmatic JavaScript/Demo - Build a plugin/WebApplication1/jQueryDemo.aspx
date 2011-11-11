<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="jQueryDemo.aspx.cs" Inherits="WebApplication1.jQueryDemo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <script src="Scripts/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="Scripts/jquery.Demo.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {

            $("a").demo({color:"blue"});

        });
    
    </script>

</asp:Content>