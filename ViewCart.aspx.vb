Imports System.Data
Imports System.Data.SqlClient

Partial Class ViewCart
    Inherits System.Web.UI.Page

    ' Handles process when page is first loaded.
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim strCartID As String

        ' If there is no cookie, create one and name it CartID. This creates a cookie when poage is loaded
        If HttpContext.Current.Request.Cookies("CartID") Is Nothing Then
            ' Generate the password for the cookie using the function
            strCartID = GetRandomPasswordUsingGUID(10)
            ' Create cookie
            Dim CookieTo As New HttpCookie("CartID", strCartID)
            HttpContext.Current.Response.AppendCookie(CookieTo)
        Else
            ' Retrieve cookie
            Dim CookieBack As HttpCookie
            CookieBack = HttpContext.Current.Request.Cookies("CartID")
            ' Set cookie value
            strCartID = CookieBack.Value
        End If

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

    Public Function GetRandomPasswordUsingGUID(ByVal length As Integer) As String
        'Get the GUID
        Dim guidResult As String = System.Guid.NewGuid().ToString()
        'Remove the hyphens
        guidResult = guidResult.Replace("-", String.Empty)
        'Make sure length is valid
        If length <= 0 OrElse length > guidResult.Length Then
            Throw New ArgumentException("Length must be between 1 and " & guidResult.Length)
        End If

        'Return the first length bytes
        Return guidResult.Substring(0, length)
    End Function

    ' Execute function when the EmptyCart button is clicked
    Sub EmptyCart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EmptyCart.Click
        Dim strCartID As String
        ' Retrieve cookie
        Dim CookieBack As HttpCookie
        CookieBack = HttpContext.Current.Request.Cookies("CartID")
        ' Set cookie value
        strCartID = CookieBack.Value

        ' Open SQL Data Reader
        Dim dr As SqlDataReader
        Dim strSQLStatement As String
        Dim strSQL As SqlCommand
        ' Get the connection configuration from the Web.Config file
        Dim strConnectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True"

        strSQLStatement = "DELETE FROM Cartline WHERE CartID = '" + strCartID + "'"

        ' Open connection and execute the SQL statement
        Dim conn As New SqlConnection(strConnectionString)
        strSQL = New SqlCommand(strSQLStatement, conn)
        conn.Open()
        ' This executes the statement
        dr = strSQL.ExecuteReader()
        conn.Close()
        conn.Open()
        dr = strSQL.ExecuteReader(CommandBehavior.CloseConnection)
        ' Bind the cart table so that it refreshes it everytime this is processed
        gvCartLine.DataBind()

        ' Display message in the "Messages" div
        messages.Text = "<div class='alert alert-success'>The cart has been emptied.</div>"
    End Sub

    Sub gvCartLine_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Dim gvrCartLine As GridViewRow = DirectCast(DirectCast(e.CommandSource, Button).NamingContainer, GridViewRow)
        ' Gets the row index of the GridView
        Dim intRowIndex As Integer = gvrCartLine.RowIndex
        ' Gets the selected row from the GridView
        Dim selectedRow As GridViewRow = gvCartLine.Rows(intRowIndex)
        Dim tablecellQuantity As TableCell = selectedRow.Cells(2)
        ' Gets the value from the textfield inside the GridView
        Dim tbNewQuantity As TextBox = CType(gvCartLine.Rows(intRowIndex).Cells(2).FindControl("tbNewQuantity"), TextBox)

        ' Perform validation on the textfield inside the GridView
        If (e.CommandName = "rowUpdate" And IsNumeric(tbNewQuantity.Text)) Or (e.CommandName = "rowRemove") Then
            If e.CommandName = "rowRemove" And Not IsNumeric(tbNewQuantity) Then
                Dim intNewQuantity As String = CStr(tbNewQuantity.Text)
            Else
                Dim intNewQuantity As Integer = CInt(tbNewQuantity.Text)
            End If

            ' Get the ProductCode from the selected cell
            Dim tablecellProductCode As TableCell = selectedRow.Cells(0)
            Dim strProductCode As String = tablecellProductCode.Text

            ' Get cookie
            Dim strCartID As String
            Dim CookieBack As HttpCookie
            CookieBack = HttpContext.Current.Request.Cookies("CartID")
            strCartID = CookieBack.Value

            ' Open SQL Reader
            Dim dr As SqlDataReader
            Dim strSQLStatement As String
            Dim strSQL As SqlCommand
            Dim strConnectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True"

            ' If the "Update" button was clicked, perform the function
            If e.CommandName = "rowUpdate" Then
                strSQLStatement = "UPDATE CartLine SET Quantity = " & tbNewQuantity.Text & " WHERE CartID = '" & strCartID & "' AND ProductID = '" & strProductCode & "'; SELECT SUM(ProductPrice * Quantity) AS Subtotal FROM Cartline WHERE CartID = '" + strCartID + "'"
                messages.Text = "<div class='alert alert-success'>Quantity has been updated.</div>"

                ' If the "Remove" button was clicked, perform the function
            ElseIf e.CommandName = "rowRemove" Then
                strSQLStatement = "DELETE FROM Cartline WHERE CartID = '" & strCartID & "' AND ProductID = '" & strProductCode & "'"
                messages.Text = "<div class='alert alert-success'>Item has been removed from cart.</div>"
            End If

            ' Establish connection to database using the connection string from Web.Config declared on top
            Dim conn As New SqlConnection(strConnectionString)
            strSQL = New SqlCommand(strSQLStatement, conn)
            conn.Open()
            ' Execute the reader
            dr = strSQL.ExecuteReader()

            If dr.Read() Then
                subtotal.Text = dr.Item("Subtotal")
                subtotal.DataBind()
            End If

            conn.Close()
            conn.Open()
            ' Close connection
            dr = strSQL.ExecuteReader(CommandBehavior.CloseConnection)
            ' Bind the cart table so that it refreshes it everytime this is processed
            gvCartLine.DataBind()
        Else
            ' Show message in the "Messages" div
            messages.Text = "<div class='alert alert-danger'>Quantity must be an integer.</div>"
        End If
    End Sub
End Class