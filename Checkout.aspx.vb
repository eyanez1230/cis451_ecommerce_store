Imports System.Data
Imports System.Data.SqlClient
Imports System.Net
Imports System.IO
Imports System.Net.Mail

Partial Class Checkout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim strCartID As String
            ' Retrieve cookie
            Dim CookieBack As HttpCookie
            CookieBack = HttpContext.Current.Request.Cookies("CartID")
            ' Set cookie value
            strCartID = CookieBack.Value

        ' Retrieve cart items from the Cartline table by CartID
        ViewCart.SelectCommand = "SELECT * FROM Cartline WHERE CartID = '" + strCartID + "'"
        ' Bind the cart table so that it refreshes it everytime this is processed
        ViewCart.DataBind()

        ' Open SQL Data Reader
        Dim dr As SqlDataReader
        Dim strSQLStatement As String
        Dim strSQL As SqlCommand
        ' Get the connection configuration from the Web.Config file
        Dim strConnectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True"

        strSQLStatement = "SELECT SUM(ProductPrice * Quantity) AS Subtotal FROM Cartline WHERE CartID = '" + strCartID + "'"

        ' Open connection and execute the SQL statement
        Dim conn As New SqlConnection(strConnectionString)
        strSQL = New SqlCommand(strSQLStatement, conn)
        conn.Open()
        dr = strSQL.ExecuteReader()

        If dr.Read() Then
            ' Set subtotal labels
            subtotal.Text = dr.Item("Subtotal")
            subtotal.DataBind()
        End If
        conn.Close()
    End Sub

    Protected Sub SubmitCheckout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SubmitCheckout.Click
        ' Get cookie ID
        Dim strCartID As String
        Dim CookieBack As HttpCookie
        CookieBack = HttpContext.Current.Request.Cookies("CartID")
        strCartID = CookieBack.Value

        Dim dr As SqlDataReader
        Dim strSQLStatement As String
        Dim cmdSQL As SqlCommand
        ' Get connection from Web.Config
        Dim strConnectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True"

        Dim CreditCardDate As String = CStr(CreditCardExpirationMonth.Text + "/" + CreditCardExpirationYear.Text)

        strSQLStatement = "INSERT INTO Customer (OrderLineID, FirstName, Email, LastName, StreetAddress, City, State, Zip, PhoneNumber, CreditCardNumber, CreditCardType, CreditCardExpirationDate) VALUES ('" + strCartID + "', '" + FirstName.Text + "', '" + LastName.Text + "', '" + Email.Text + "', '" + StreetAddress.Text + "', '" + City.Text + "', '" + State.Text + "', '" + Zip.Text + "', '" + PhoneNumber.Text + "', '" + CreditCardNumber.Text + "', '" + CreditCardType.Text + "', '" + CStr(CreditCardDate) + "'); SELECT @@Identity;"

        Dim conn As New SqlConnection(strConnectionString)
        cmdSQL = New SqlCommand(strSQLStatement, conn)
        conn.Open()
        dr = cmdSQL.ExecuteReader()
        conn.Close()

        ' Use the cookie CartID to transfer items from cart to the orerline table and store orderlineID as CartID

        strSQLStatement = "SELECT * FROM Cartline WHERE CartID = '" + strCartID + "'"

        cmdSQL = New SqlCommand(strSQLStatement, conn)
        conn.Open()
        dr = cmdSQL.ExecuteReader()


        Dim strProductCode As String = ""
        Dim strProductName As String = ""
        Dim decProductPrice As Decimal
        Dim intQuantity As Integer

        Dim products As String = ""

        Dim myList As New List(Of String)

        While dr.Read()
            strProductCode = dr.Item("ProductID")
            strProductName = dr.Item("ProductName")
            decProductPrice = dr.Item("ProductPrice")
            intQuantity = dr.Item("Quantity")

            products += dr.Item("ProductName") + " <br />"

            myList.Add("INSERT INTO Orderline (OrderlineID, ProductCode, ProductName, ProductPrice, Quantity) VALUES ('" & strCartID & "', '" & CStr(strProductCode) & "', '" & CStr(strProductName) & "', " & CDec(decProductPrice) & ", " & CInt(intQuantity) & ")")

        End While
        conn.Close()

        For Each myListing In myList
            cmdSQL = New SqlCommand(myListing, conn)
            conn.Open()
            dr = cmdSQL.ExecuteReader()
            conn.Close()
        Next

        ' Possibly create another table to hold order info and have columns, ID, OrderID, Subtotal, Total And CustomerID
        ' OrderID will be the OrderlineID that is the same from the cookie of cartId
        ' If so, calculate subtotal, total and if california is state, calculate 8.75% tax

        strSQLStatement = "SELECT SUM(ProductPrice * Quantity) AS Subtotal FROM Cartline WHERE CartID = '" & strCartID & "'"
        cmdSQL = New SqlCommand(strSQLStatement, conn)
        conn.Open()
        dr = cmdSQL.ExecuteReader()

        Dim subtotal As Decimal
        Dim totalBeforeRound As Decimal
        Dim total As Decimal
        Dim tax As Decimal = 0.0875
        Dim totalTax As Decimal

        If dr.Read() Then
            If State.Text = "CA" Then
                subtotal = dr.Item("Subtotal")
                totalTax = subtotal * tax
                totalBeforeRound = totalTax + subtotal
                total = totalBeforeRound
            Else
                subtotal = dr.Item("Subtotal")
                total = subtotal
            End If
        End If
        conn.Close()

        ' Get Customer ID By OrderLINE ID

        strSQLStatement = "SELECT * FROM Customer WHERE OrderlineID = '" & strCartID & "'"
        cmdSQL = New SqlCommand(strSQLStatement, conn)
        conn.Open()
        dr = cmdSQL.ExecuteReader()

        Dim customerID As Integer

        If dr.Read() Then
            customerID = dr.Item("ID")
        End If
        conn.Close()

        strSQLStatement = "INSERT INTO OrderInfo (OrderlineID, Subtotal, Total, CustomerID) VALUES ('" & strCartID & "', " & subtotal & ", " & total & ", " & customerID & ")"
        cmdSQL = New SqlCommand(strSQLStatement, conn)
        conn.Open()
        dr = cmdSQL.ExecuteReader()
        conn.Close()

        ' By default, this sample code is designed to post to our test server for
        ' developer accounts: https://test.authorize.net/gateway/transact.dll
        ' for real accounts (even in test mode), please make sure that you are
        ' posting to: https://secure.authorize.net/gateway/transact.dll
        Dim post_url As String
        post_url = "https://test.authorize.net/gateway/transact.dll"

        Dim post_values As New Dictionary(Of String, String)

        'the API Login ID and Transaction Key must be replaced with valid values
        post_values.Add("x_login", "55UhxX87")
        post_values.Add("x_tran_key", "66NvERFwq5k9753g")

        post_values.Add("x_delim_data", "TRUE")
        post_values.Add("x_delim_char", "|")
        post_values.Add("x_relay_response", "FALSE")

        post_values.Add("x_type", "AUTH_CAPTURE")
        post_values.Add("x_method", "CC")
        post_values.Add("x_card_num", CreditCardNumber.Text)
        post_values.Add("x_exp_date", "0115")

        post_values.Add("x_amount", total)
        post_values.Add("x_description", "CIS 451 Transaction")

        post_values.Add("x_first_name", FirstName.Text)
        post_values.Add("x_last_name", LastName.Text)
        post_values.Add("x_address", StreetAddress.Text)
        post_values.Add("x_state", State.Text)
        post_values.Add("x_zip", Zip.Text)
        ' Additional fields can be added here as outlined in the AIM integration
        ' guide at: http://developer.authorize.net

        ' This section takes the input fields and converts them to the proper format
        ' for an http post.  For example: "x_login=username&x_tran_key=a1B2c3D4"
        Dim post_string As String = ""
        For Each field As KeyValuePair(Of String, String) In post_values
            post_string &= field.Key & "=" & HttpUtility.UrlEncode(field.Value) & "&"
        Next
        post_string = Left(post_string, Len(post_string) - 1)

        ' The following section provides an example of how to add line item details to
        ' the post string.  Because line items may consist of multiple values with the
        ' same key/name, they cannot be simply added into the above array.
        '
        ' This section is commented out by default.
        'Dim line_items() As String = { _
        '    "item1<|>golf balls<|><|>2<|>18.95<|>Y", _
        '    "item2<|>golf bag<|>Wilson golf carry bag, red<|>1<|>39.99<|>Y", _
        '    "item3<|>book<|>Golf for Dummies<|>1<|>21.99<|>Y"}
        '
        'For Each value As String In line_items
        '   post_string += "&x_line_item=" + HttpUtility.UrlEncode(value)
        'Next

        ' create an HttpWebRequest object to communicate with Authorize.net
        Dim objRequest As HttpWebRequest = CType(WebRequest.Create(post_url), HttpWebRequest)
        objRequest.Method = "POST"
        objRequest.ContentLength = post_string.Length
        objRequest.ContentType = "application/x-www-form-urlencoded"

        ' post data is sent as a stream
        Dim myWriter As StreamWriter = Nothing
        myWriter = New StreamWriter(objRequest.GetRequestStream())
        myWriter.Write(post_string)
        myWriter.Close()

        ' returned values are returned as a stream, then read into a string
        Dim objResponse As HttpWebResponse = CType(objRequest.GetResponse(), HttpWebResponse)
        Dim responseStream As New StreamReader(objResponse.GetResponseStream())
        Dim post_response As String = responseStream.ReadToEnd()
        responseStream.Close()

        ' the response string is broken into an array
        Dim response_array As Array = Split(post_response, post_values("x_delim_char"), -1)

        'resultSpan.InnerHtml += "<OL>" & vbCrLf
        'For Each value In response_array
        'resultSpan.InnerHtml += "<LI>" & value & "&nbsp;</LI>" & vbCrLf
        'Next
        'resultSpan.InnerHtml += "</OL>" & vbCrLf

        ' individual elements of the array could be accessed to read certain response
        ' fields.  For example, response_array(0) would return the Response Code,
        ' response_array(2) would return the Response Reason Code.
        ' for a list of response fields, please review the AIM Implementation Guide

        strSQLStatement = "UPDATE OrderInfo SET AuthCode = '" & response_array(4) & "' WHERE CustomerID = " & customerID & " AND OrderlineID = '" & strCartID & "'"
        cmdSQL = New SqlCommand(strSQLStatement, conn)
        conn.Open()
        dr = cmdSQL.ExecuteReader()
        conn.Close()

        'Emailing a receipt 

        Dim MyMailMessage As New MailMessage()
        MyMailMessage.IsBodyHtml = True
        MyMailMessage.From = New MailAddress("CIS451OnlineStore@gmail.com")
        ' strEmail is from getting user
        Dim strEmail As String
        strEmail = Email.Text
        MyMailMessage.To.Add(strEmail)
        MyMailMessage.Subject = "Order Confirmation"
        'MyMailMessage.Body = "<html>" & strOrderline & "Total: $" & strTotal & "<br/> " & "Tax: $" & strTaxAmount & "<br/> " & "SubTotal: $" & strSubTotal & "<br/> " & "</html>"

        MyMailMessage.Body = "<html>Thank you for placing an order with us! <br/> Hello " & FirstName.Text & ", <br/><br/> Confirmation ID: " & strCartID & "<br/> Items: " & products & "Order Total: " & total & "<br/><br/> Have any questions? <br/> Email us at CIS451OnlineStore@gmail.com </html>"

        'Create the SMTPClient object and specify the SMTP GMail server
        Dim SMTPServer As New SmtpClient("smtp.gmail.com")
        SMTPServer.Port = 587
        SMTPServer.Credentials = New System.Net.NetworkCredential("CIS451OnlineStore@gmail.com", "6PvzrZV5")
        SMTPServer.EnableSsl = True

        Try
            SMTPServer.Send(MyMailMessage)
            'MessageBox.Show("Email Sent")
        Catch ex As SmtpException
            'MessageBox.Show(ex.Message)
        End Try

        Response.Redirect("Receipt.aspx")
    End Sub
End Class