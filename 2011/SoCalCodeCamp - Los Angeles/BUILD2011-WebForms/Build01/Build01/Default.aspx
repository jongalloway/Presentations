<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Build01._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Content/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager runat="server" AjaxFrameworkMode="Disabled">
        <Scripts>
            <asp:ScriptReference Name="jquery" Assembly="System.Web" />
            <asp:ScriptReference Path="~/Scripts/" />
        </Scripts>
    </asp:ScriptManager>

    <asp:Panel runat="server" GroupingText="Filter">
        <div>
            <asp:Label runat="server" Text="Created since:" AssociatedControlID="createdSince" />
            <asp:TextBox runat="server" ID="createdSince" MaxLength="10" data-ui-fn="datepicker" />
        </div>
        <div>
            <asp:CheckBox runat="server" ID="createdByMe" Text="Created by me" />
        </div>
        <div>
            <asp:Button Text="Filter" runat="server" />
        </div>
    </asp:Panel>

    <asp:GridView runat="server" ID="issuesGrid"
        ModelType="Build01.Model.Issue" DataKeyNames="ID"
        SelectMethod="GetIssues" AllowPaging="true" PageSize="5" AllowSorting="true"
        AutoGenerateColumns="false" AutoGenerateSelectButton="true">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedOn" />
            <asp:BoundField DataField="CreatedOn" HeaderText="Date Created" SortExpression="CreatedOn" />
        </Columns>
    </asp:GridView>

    <asp:FormView runat="server" ID="issueForm"
        ModelType="Build01.Model.Issue" DataKeyNames="ID,Timestamp"
        SelectMethod="GetIssue" DefaultMode="Edit"
        InsertMethod="InsertIssue"
        UpdateMethod="UpdateIssue"
        OnPreRender="issueForm_PreRender">
        <EditItemTemplate>
            <asp:Panel runat="server" GroupingText='<%#: String.Format("Issue:  {0}", Item.Title) %>'>
                <ol>
                    <li>
                        <asp:Label runat="server" Text="ID" AssociatedControlID="id" />
                        <asp:TextBox runat="server" ID="id" ReadOnly="true" Text="<%# Item.ID %>" />
                    </li>
                    <li>
                        <asp:Label runat="server" Text="Title:" AssociatedControlID="title" />
                        <asp:TextBox runat="server" ID="title" Text="<%# BindItem.Title %>" />
                    </li>
                    <li>
                        <asp:Label runat="server" Text="Description:" AssociatedControlID="description" />
                        <asp:TextBox runat="server" ID="description" TextMode="MultiLine"
                            ValidateRequestMode="Disabled" Text="<%# BindItem.Description %>" />
                    </li>
                    <li>
                        <asp:Label runat="server" Text="Created:" AssociatedControlID="createdOn" />
                        <asp:DynamicControl runat="server" ID="createdOn" DataField="CreatedOn" Mode="Edit" />
                    </li>
                    <li>
                        <asp:Label runat="server" Text="Logged By:" AssociatedControlID="createdBy" />
                        <asp:TextBox runat="server" ID="createdBy" ReadOnly="true" Text="<%# Item.CreatedBy %>" />
                    </li>
                </ol>
                <asp:ValidationSummary runat="server" ShowModelStateErrors="true" HeaderText="Errors" />
                <div class="actions">
                    <asp:CheckBox runat="server" ID="forceSave" Text="Force save changes" Visible="false" /><br />
                    <asp:Button Text="Save" runat="server" CommandName="Update" />
                </div>
            </asp:Panel>
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:Panel runat="server" GroupingText="New Issue">
                <ol>
                    <li>
                        <asp:Label runat="server" Text="Title:" AssociatedControlID="title" />
                        <asp:TextBox runat="server" ID="title" Text="<%# BindItem.Title %>" />
                    </li>
                    <li>
                        <asp:Label runat="server" Text="Description:" AssociatedControlID="description" />
                        <asp:TextBox runat="server" ID="description" TextMode="MultiLine"
                            ValidateRequestMode="Disabled" Text="<%# BindItem.Description %>" />
                    </li>
                </ol>
                <asp:ValidationSummary runat="server" ShowModelStateErrors="true" HeaderText="Errors" />
                <div class="actions">
                    <asp:Button Text="Save" runat="server" CommandName="Insert" />
                </div>
            </asp:Panel>
        </InsertItemTemplate>
        <EmptyDataTemplate>
            Select an issue from the grid above to view its details or 
            <asp:LinkButton Text="create a new issue" runat="server" CommandName="New" />
        </EmptyDataTemplate>
    </asp:FormView>
</asp:Content>
