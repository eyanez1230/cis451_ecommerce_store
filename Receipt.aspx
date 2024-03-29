﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Receipt.aspx.vb" Inherits="Receipt" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row-fluid">
            <h4>Order Receipt</h4>
        </div>
        <div class="row-fluid">
            <label>First Name:</label>
            <asp:Label ID="FirstName" runat="server" Text="Label"></asp:Label>
        </div>
        <div class="row-fluid">
            <label>Last Name:</label>
            <asp:Label ID="LastName" runat="server" Text="Label"></asp:Label>
        </div>
        <div class="row-fluid">
            <h4>Orderline Items:</h4>
            <asp:GridView ID="gvOrderline" CssClass="table table-striped" GridLines="None" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ViewOrderline" ShowHeaderWhenEmpty="True">
                <EmptyDataTemplate>Your cart is empty.</EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="ProductCode" HeaderText="ProductCode" SortExpression="ProductCode">
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName">
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ProductPrice" HeaderText="ProductPrice" SortExpression="ProductPrice">
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity">
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="row-fluid">
            <label>Subtotal:</label>
            <asp:Label ID="Subtotal" runat="server" Text="Label"></asp:Label>
        </div>
        <div class="row-fluid">
            <label>Total:</label>
            <asp:Label ID="Total" runat="server" Text="Label"></asp:Label>
        </div>
    </div>
    <asp:SqlDataSource ID="ViewOrderline" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnection %>" ProviderName="<%$ ConnectionStrings:DatabaseConnection.ProviderName %>"></asp:SqlDataSource>
</asp:Content>