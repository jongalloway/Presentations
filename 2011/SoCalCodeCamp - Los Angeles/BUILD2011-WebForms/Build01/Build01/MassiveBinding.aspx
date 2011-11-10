<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MassiveBinding.aspx.cs" Inherits="Build01.MassiveBinding" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater runat="server" ModelType="dynamic" SelectMethod="GetItems">
        <ItemTemplate>
            <div>ID: <%#: Item.ProductID %>, Name: <%#: Item.ProductName %></div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>