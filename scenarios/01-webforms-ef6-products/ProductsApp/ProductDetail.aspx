<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetail.aspx.cs" Inherits="ProductsApp.ProductDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Product Detail</h2>

    <asp:Panel ID="DetailPanel" runat="server" Visible="false">
        <div class="form-row"><label>Name:</label> <asp:Literal ID="NameLiteral" runat="server" /></div>
        <div class="form-row"><label>Category:</label> <asp:Literal ID="CategoryLiteral" runat="server" /></div>
        <div class="form-row"><label>Price:</label> <asp:Literal ID="PriceLiteral" runat="server" /></div>
        <div class="form-row"><label>In Stock:</label> <asp:Literal ID="InStockLiteral" runat="server" /></div>
        <div class="form-row"><label>Created:</label> <asp:Literal ID="CreatedLiteral" runat="server" /></div>
    </asp:Panel>

    <asp:Label ID="NotFoundLabel" runat="server" CssClass="validation-error" Visible="false"
        Text="Product not found." />

    <p><a href="~/Default.aspx" runat="server">&laquo; Back to list</a></p>
</asp:Content>
