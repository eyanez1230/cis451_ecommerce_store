<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Checkout.aspx.vb" Inherits="Checkout" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <h4>Customer Information</h4>
                <asp:ValidationSummary ID="ValidationSummary1" HeaderText="The following fields have errors:" DisplayMode="BulletList" EnableClientScript="true" runat="server"></asp:ValidationSummary>
                <div class="row">
                    <div class="col-md-11">
                        <label for="FirstName">First Name</label>
                        <asp:TextBox ID="FirstName" runat="server" placeholder="First Name" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="FirstName" runat="server" ErrorMessage="First name is required." Text="*"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-11">
                        <label for="FirstName">Last Name</label>
                        <asp:TextBox ID="LastName" runat="server" placeholder="Last Name" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="LastName" runat="server" ErrorMessage="Last name is required." Text="*"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-11">
                        <label for="Email">Email</label>
                        <asp:TextBox ID="Email" runat="server" placeholder="Email" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="Email" runat="server" ErrorMessage="Email is required." Text="*"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-11">
                        <label for="StreetAddress">Street Address</label>
                        <asp:TextBox ID="StreetAddress" runat="server" placeholder="Street Address" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="StreetAddress" runat="server" ErrorMessage="Street address is required." Text="*"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-11">
                        <label for="City">City</label>
                        <asp:TextBox ID="City" runat="server" placeholder="City" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="City" runat="server" ErrorMessage="City is required." Text="*"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-11">
                        <label for="State">State</label>
                        <asp:DropDownList ID="State" runat="server" CssClass="form-control">
                            <asp:ListItem Value="AL">Alabama</asp:ListItem>
                            <asp:ListItem Value="AK">Alaska</asp:ListItem>
                            <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                            <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                            <asp:ListItem Value="CA">California</asp:ListItem>
                            <asp:ListItem Value="CO">Colorado</asp:ListItem>
                            <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                            <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
                            <asp:ListItem Value="DE">Delaware</asp:ListItem>
                            <asp:ListItem Value="FL">Florida</asp:ListItem>
                            <asp:ListItem Value="GA">Georgia</asp:ListItem>
                            <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                            <asp:ListItem Value="ID">Idaho</asp:ListItem>
                            <asp:ListItem Value="IL">Illinois</asp:ListItem>
                            <asp:ListItem Value="IN">Indiana</asp:ListItem>
                            <asp:ListItem Value="IA">Iowa</asp:ListItem>
                            <asp:ListItem Value="KS">Kansas</asp:ListItem>
                            <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                            <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                            <asp:ListItem Value="ME">Maine</asp:ListItem>
                            <asp:ListItem Value="MD">Maryland</asp:ListItem>
                            <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                            <asp:ListItem Value="MI">Michigan</asp:ListItem>
                            <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                            <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                            <asp:ListItem Value="MO">Missouri</asp:ListItem>
                            <asp:ListItem Value="MT">Montana</asp:ListItem>
                            <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                            <asp:ListItem Value="NV">Nevada</asp:ListItem>
                            <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                            <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                            <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                            <asp:ListItem Value="NY">New York</asp:ListItem>
                            <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                            <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                            <asp:ListItem Value="OH">Ohio</asp:ListItem>
                            <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                            <asp:ListItem Value="OR">Oregon</asp:ListItem>
                            <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                            <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                            <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                            <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                            <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                            <asp:ListItem Value="TX">Texas</asp:ListItem>
                            <asp:ListItem Value="UT">Utah</asp:ListItem>
                            <asp:ListItem Value="VT">Vermont</asp:ListItem>
                            <asp:ListItem Value="VA">Virginia</asp:ListItem>
                            <asp:ListItem Value="WA">Washington</asp:ListItem>
                            <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                            <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                            <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="State" runat="server" ErrorMessage="State is required." Text="*"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-11">
                        <label for="Zip">Zip</label>
                        <asp:TextBox ID="Zip" runat="server" placeholder="Zip" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="Zip" runat="server" ErrorMessage="Zip is required." Text="*"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="Zip" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Phone number must be an integer" Text="*"></asp:CompareValidator>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-11">
                        <label for="PhoneNumber">Phone Number</label>
                        <asp:TextBox ID="PhoneNumber" runat="server" placeholder="Phone Number" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="PhoneNumber" runat="server" ErrorMessage="Phone number is required." Text="*"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="PhoneNumber" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Phone number must be an integer" Text="*"></asp:CompareValidator>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-11">
                        <label for="CreditCardNumber">Credit Card Number</label>
                        <asp:TextBox ID="CreditCardNumber" runat="server" placeholder="Credit Card Number" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="CreditCardNumber" runat="server" ErrorMessage="Credit card number is required." Text="*"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="CreditCardNumber" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Credit card number must be an integer" Text="*"></asp:CompareValidator>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-11">
                        <label for="CreditCardType">Credit Card Type</label>
                        <asp:DropDownList ID="CreditCardType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="Visa">Visa</asp:ListItem>
                            <asp:ListItem Value="MasterCard">MasterCard</asp:ListItem>
                            <asp:ListItem Value="Discover">Discover</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="CreditCardType" runat="server" ErrorMessage="Credit type is required." Text="*"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-11">
                        <label for="CreditCardExpirationMonth">Credit Expiration Month</label>
                        <asp:DropDownList ID="CreditCardExpirationMonth" runat="server" CssClass="form-control">
                            <asp:ListItem Text="January" Value="1" />
                            <asp:ListItem Text="February" Value="2" />
                            <asp:ListItem Text="March" Value="3" />
                            <asp:ListItem Text="April" Value="4" />
                            <asp:ListItem Text="May" Value="5" />
                            <asp:ListItem Text="June" Value="6" />
                            <asp:ListItem Text="July" Value="7" />
                            <asp:ListItem Text="August" Value="8" />
                            <asp:ListItem Text="September" Value="9" />
                            <asp:ListItem Text="October" Value="10" />
                            <asp:ListItem Text="November" Value="11" />
                            <asp:ListItem Text="December" Value="12" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="CreditCardExpirationMonth" runat="server" ErrorMessage="Credit expiration month is required." Text="*"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-11">
                        <label for="CreditCardExpirationYear">Credit Expiration Year</label>
                        <asp:DropDownList ID="CreditCardExpirationYear" runat="server" CssClass="form-control">
                            <asp:ListItem Text="2013" Value="2013" />
                            <asp:ListItem Text="2014" Value="2014" />
                            <asp:ListItem Text="2015" Value="2015" />
                            <asp:ListItem Text="2016" Value="2016" />
                            <asp:ListItem Text="2017" Value="2017" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="CreditCardExpirationYear" runat="server" ErrorMessage="Credit expiration year is required." Text="*"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row">
                    <h4>Order Summary</h4>
                    <asp:GridView ID="gvCartLine" CssClass="table table-striped" GridLines="None" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ViewCart" ShowHeaderWhenEmpty="True">
                        <EmptyDataTemplate>Your cart is empty.</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="ProductID" HeaderText="Product ID" SortExpression="ProductID">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ProductPrice" HeaderText="Product Price" SortExpression="ProductPrice">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <div class="pull-right">
                        <label>Subtotal:</label>
                        <asp:Label ID="subtotal" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <asp:Button ID="SubmitCheckout" runat="server" CssClass="btn btn-primary" Text="Submit Order" />
        </div>
    </div>
    <asp:SqlDataSource ID="ViewCart" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnection %>" ProviderName="<%$ ConnectionStrings:DatabaseConnection.ProviderName %>"></asp:SqlDataSource>
</asp:Content>

