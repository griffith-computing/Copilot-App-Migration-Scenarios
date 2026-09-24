<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductCreate.aspx.cs" Inherits="ProductsApp.ProductCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Add Product</h2>

    <div class="form-row">
        <label for="<%= NameTextBox.ClientID %>">Name:</label>
        <asp:TextBox ID="NameTextBox" runat="server" MaxLength="100" />
        <asp:RequiredFieldValidator ID="NameRequiredValidator" runat="server"
            ControlToValidate="NameTextBox" ErrorMessage="Name is required."
            CssClass="validation-error" Display="Dynamic" />
    </div>

    <div class="form-row">
        <label for="<%= CategoryTextBox.ClientID %>">Category:</label>
        <asp:TextBox ID="CategoryTextBox" runat="server" MaxLength="50" />
        <asp:RequiredFieldValidator ID="CategoryRequiredValidator" runat="server"
            ControlToValidate="CategoryTextBox" ErrorMessage="Category is required."
            CssClass="validation-error" Display="Dynamic" />
    </div>

    <div class="form-row">
        <label for="<%= PriceTextBox.ClientID %>">Price:</label>
        <asp:TextBox ID="PriceTextBox" runat="server" />
        <asp:RequiredFieldValidator ID="PriceRequiredValidator" runat="server"
            ControlToValidate="PriceTextBox" ErrorMessage="Price is required."
            CssClass="validation-error" Display="Dynamic" />
        <asp:RangeValidator ID="PriceRangeValidator" runat="server"
            ControlToValidate="PriceTextBox" Type="Currency" MinimumValue="0" MaximumValue="100000"
            ErrorMessage="Price must be between 0 and 100,000." CssClass="validation-error" Display="Dynamic" />
    </div>

    <div class="form-row">
        <label for="<%= InStockCheckBox.ClientID %>">In Stock:</label>
        <asp:CheckBox ID="InStockCheckBox" runat="server" Checked="true" />
    </div>

    <div class="form-row">
        <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="SaveButton_Click" />
    </div>

    <asp:Label ID="StatusLabel" runat="server" />
</asp:Content>
