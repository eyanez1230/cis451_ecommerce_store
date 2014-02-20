<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="SearchResults.aspx.vb" Inherits="SearchResults" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <h4>Search Results:</h4>
        <br />
        <div class="row-fluid">
            <asp:GridView CssClass="table table-striped" GridLines="None" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="ProductCode" HeaderText="ProductCode" SortExpression="ProductCode" />
                    <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                    <asp:BoundField DataField="ProductCost" HeaderText="ProductCost" SortExpression="ProductCost" />
                    <asp:BoundField DataField="StockQty" HeaderText="StockQty" SortExpression="StockQty" />
                    <asp:BoundField DataField="CategoryID" HeaderText="CategoryID" SortExpression="CategoryID" />
                    <asp:TemplateField HeaderText="Available Actions">
                        <ItemTemplate>
                            <asp:HyperLink ID="ViewProduct" runat="server" Text="Product Details" NavigateUrl='<%# "ProductView.aspx?category=" + CStr(Eval("MenuGroup")) + "&product_code=" + CStr(Eval("ProductCode"))%>' PostBackUrl='' Font-Size="Small"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnection %>" ProviderName="<%$ ConnectionStrings:DatabaseConnection.ProviderName %>" SelectCommand="SELECT Product.ID, Product.ProductCode, Product.ProductName, Product.ProductCost, Product.StockQty, Product.CategoryID, Product.ProductFeatured, Product.ProductOnSale, Category.ID AS Expr1, Category.CategoryID AS Expr2, Category.CategoryName, Category.Parent, Category.MenuGroup FROM Product LEFT OUTER JOIN Category ON Product.CategoryID = Category.CategoryID WHERE (Product.ProductName LIKE '%' + @ProductName + '%')">
                <SelectParameters>
                    <asp:QueryStringParameter Name="ProductName" QueryStringField="product_name" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>