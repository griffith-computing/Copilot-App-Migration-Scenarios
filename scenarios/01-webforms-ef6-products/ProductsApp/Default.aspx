<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProductsApp.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Products</h2>

    <asp:GridView ID="ProductsGridView" runat="server" AutoGenerateColumns="False"
        CssClass="product-grid" DataKeyNames="Id" OnRowCommand="ProductsGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
            <asp:BoundField DataField="InStock" HeaderText="In Stock" />
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="ViewLinkButton" runat="server" CommandName="ViewDetail"
                        CommandArgument='<%# Eval("Id") %>' Text="View" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Label ID="StatusLabel" runat="server" CssClass="validation-error" />
</asp:Content>
