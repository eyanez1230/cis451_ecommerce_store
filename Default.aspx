<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row-fluid">
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
            <div class="col-md-10">
                <asp:Label ID="adTitleLabel" Runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                <asp:GridView CssClass="table table-striped" GridLines="None" ID="GridView1" runat="server" DataSourceID="ProductInventory" AutoGenerateColumns="False" DataKeyNames="id">
                    <Columns>
                        <asp:BoundField DataField="ProductCode" HeaderText="Product Code" SortExpression="ProductCode" />
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName" />
                        <asp:BoundField DataField="ProductCost" HeaderText="Product Cost" SortExpression="ProductCost" />
                        <asp:BoundField DataField="StockQTY" HeaderText="Stock Quantity" SortExpression="StockQTY" />
                        <asp:TemplateField HeaderText="Available Actions">
                            <ItemTemplate>
                                <asp:Hyperlink ID="ViewProduct" runat="server" Text="Product Details" NavigateUrl='<%# "ProductView.aspx?category=" + Request.QueryString("category") + "&product_code=" + CStr(Eval("ProductCode"))%>'  Font-Size="Small"></asp:Hyperlink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="ProductCategorySubmenu" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnection %>" ProviderName="<%$ ConnectionStrings:DatabaseConnection.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ProductInventory" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnection %>" ProviderName="<%$ ConnectionStrings:DatabaseConnection.ProviderName %>"></asp:SqlDataSource>
</asp:Content>