<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="jQueryWebForms.WebForm1" %>
<%@ Register Src="~/WebUserControl1.ascx" TagPrefix="uc" TagName="WebUserControl1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager runat="server" EnableCdn="true" AjaxFrameworkMode="Disabled"
        LoadScriptsBeforeUI="false">
        <%--<Scripts>
            <asp:ScriptReference Name="jquery" />
        </Scripts>--%>
    </asp:ScriptManager>

    <div data-foo="bar"></div>

    <uc:WebUserControl1 runat="server" />
    <uc:WebUserControl1 runat="server" />


</asp:Content>