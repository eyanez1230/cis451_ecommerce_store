<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ViewCart.aspx.vb" Inherits="ViewCart" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row-fluid">
            <div class="col-md-6">
                <h4>Cart Items</h4>
            </div>
            <div class="col-md-6">
                <div class="pull-right" style="margin-top: -37px; margin-bottom: 20px;">
                    <asp:Button ID="EmptyCart" runat="server" Text="Empty Cart" CssClass="btn btn-danger" />
                    <a href="Checkout.aspx" class="btn btn-success">Order Checkout</a>
                </div>
            </div>
        </div>
        <asp:Label ID="messages" runat="server"></asp:Label>
        <div class="row-fluid">
            <asp:GridView ID="gvCartLine" CssClass="table table-striped" GridLines="None" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ViewCart" OnRowCommand="gvCartLine_RowCommand" ShowHeaderWhenEmpty="True">
                <EmptyDataTemplate>Your cart is empty.</EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="ProductID" HeaderText="Product ID" SortExpression="ProductID">
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName">
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ProductPrice" HeaderText="Produc tPrice" SortExpression="ProductPrice">
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:TextBox ID="tbNewQuantity" runat="server" Width="100%" CssClass="form-control inline" Font-Size="Medium" Text='<%# Eval("Quantity") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Available Actions" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" CommandName="rowUpdate" Text="Update"></asp:Button>
                            <asp:Button ID="btnRemove" runat="server" CssClass="btn btn-danger" CommandName="rowRemove" Text="Remove"></asp:Button>
                        <ItemStyle Width="20%"></ItemStyle>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="row-fluid">
            <div class="col-md-6">
                &nbsp;
            </div>
            <div class="col-md-6">
                <div class="pull-right">
                    <label>Subtotal:</label>
                    <asp:Label ID="subtotal" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="ViewCart" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnection %>" ProviderName="<%$ ConnectionStrings:DatabaseConnection.ProviderName %>"></asp:SqlDataSource>
</asp:Content>