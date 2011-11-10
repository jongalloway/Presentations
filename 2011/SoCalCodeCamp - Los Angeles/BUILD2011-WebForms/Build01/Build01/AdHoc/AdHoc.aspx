<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdHoc.aspx.cs" Inherits="Build01.AdHoc.AdHoc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input name="Foo" value="Testing" />
    <input name="Bar" value="Hello" />
    <input name="Count" value="3" />
    <input type="submit" value="Go" />
</asp:Content>