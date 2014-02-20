<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ProductView.aspx.vb" Inherits="ProductView" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="container">
            <div class="col-md-2">
                <ul class="nav nav-list bs-docs-sidenav affix">
                    <asp:Repeater ID="Repeater2" runat="server" DataSourceID="ProductCategorySubmenu">
                        <ItemTemplate>
                            <li>
                                <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("CategoryName")%>' NavigateUrl='<%# "Default.aspx?category=" + CStr(Eval("MenuGroup")) + "&category_id=" + CStr(Eval("CategoryID"))%>'></asp:HyperLink>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <asp:Panel ID="Repeater1" runat="server" DataSourceID="ProductViewDetails">
                <div class="col-md-4">
                    <asp:Image ID="Image1" runat="server" />
                </div>
                <div class="col-md-6">
                    <asp:GridView CssClass="table table-striped" GridLines="None" ID="ItemDetails" runat="server" DataSourceID="ProductViewDetails" AutoGenerateColumns="False" DataKeyNames="id">
                        <Columns>
                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName" />
                            <asp:BoundField DataField="ProductCost" HeaderText="Product Cost" SortExpression="ProductCost" />
                            <asp:BoundField DataField="StockQTY" HeaderText="Stock Quantity" SortExpression="StockQTY" />
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="errors" runat="server"></asp:Label>
                    <div class="pull-right">
                        <asp:Panel ID="pnlProductDetail" runat="server" Visible="True">
                            <asp:TextBox ID="txtQuantity" runat="server" Width="250" CssClass="form-control" PlaceHolder="Order Quantity"></asp:TextBox>
                            <br />
                            <asp:Button ID="addToCart" CssClass="btn btn-primary" runat="server" Text="Add to Cart" />
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <asp:SqlDataSource ID="ProductCategorySubmenu" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnection %>" ProviderName="<%$ ConnectionStrings:DatabaseConnection.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ProductViewDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnection %>" ProviderName="<%$ ConnectionStrings:DatabaseConnection.ProviderName %>"></asp:SqlDataSource>
</asp:Content>