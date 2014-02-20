Imports System.Data
Imports System.Data.SqlClient

Partial Class ProductView
    Inherits System.Web.UI.Page

    ' Handles process when page is first loaded.
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Get image from images folder using the product code from the URL variable
        Image1.ImageUrl = "images/" + CStr(Request.QueryString("product_code")) + ".jpg"

        If Request.QueryString("category") <> "" Then
            ProductCategorySubmenu.SelectCommand = "SELECT * FROM Category WHERE parent != 0 AND MenuGroup = '" + CStr(Request.QueryString("category") + "'")
            ProductCategorySubmenu.DataBind()
            Session("category") = Request.QueryString("category")
        End If

        If Request.QueryString("product_code") <> "" Then
            ProductViewDetails.SelectCommand = "SELECT * FROM Product WHERE ProductCode = '" + CStr(Request.QueryString("product_code") + "'")
            ProductViewDetails.DataBind()
            Session("product_code") = Request.QueryString("product_code")
        End If
    End Sub

    ' Handles process when the Add To Cart button is clicked
    Protected Sub addToCart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addToCart.Click
        ' Validate numeric
        If IsNumeric(txtQuantity.Text) Then
            ' Establish SQL Data Reader
            Dim dr As SqlDataReader
            Dim strSQLStatement As String
            Dim cmdSQL As SqlCommand
            ' Get connection from Web.Config
            Dim strConnectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True"

            strSQLStatement = "SELECT * FROM Product WHERE ProductCode = '" & CStr(Request.QueryString("product_code") & "'")

            ' Execute the SQL Statement
            Dim conn As New SqlConnection(strConnectionString)
            cmdSQL = New SqlCommand(strSQLStatement, conn)
            conn.Open()
            dr = cmdSQL.ExecuteReader()

            ' Declare variables
            Dim intStockQty As Integer
            Dim strProductName As String = ""
            Dim decPrice As Decimal

            ' If the reader was executed
            If dr.Read() Then
                ' Set values from the object executed by the Data Reader
                intStockQty = dr.Item("StockQty")
                strProductName = dr.Item("ProductName")
                decPrice = dr.Item("ProductCost")
            End If

            ' If the stock quantity cannot fullfil the requested quantity then display error
            If txtQuantity.Text > intStockQty Then
                errors.Text = "<div class='alert alert-danger'>There is not enough quantity to supply your request.</div>"
            Else
                Dim strCartID As String

                'If cookie is empty, create a new cookie
                If HttpContext.Current.Request.Cookies("CartID") Is Nothing Then
                    ' Get cookie password from the function
                    strCartID = GetRandomPasswordUsingGUID(10)
                    ' Create cookie
                    Dim CookieTo As New HttpCookie("CartID", strCartID)
                    HttpContext.Current.Response.AppendCookie(CookieTo)
                Else
                    ' Retrieve cookie if the cookie was already created
                    Dim CookieBack As HttpCookie
                    CookieBack = HttpContext.Current.Request.Cookies("CartID")
                    strCartID = CookieBack.Value
                End If
                conn.Close()

                strSQLStatement = "SELECT * FROM Cartline WHERE CartID = '" & strCartID & "' AND ProductID = '" & Request.QueryString("product_code") & "'"

                ' Execute SQL Statement
                cmdSQL = New SqlCommand(strSQLStatement, conn)
                conn.Open()
                dr = cmdSQL.ExecuteReader()

                ' If the item is already in the cart, update the entry, otherwise add a new entry to the database
                If dr.Read() Then
                    conn.Close()
                    strSQLStatement = "UPDATE Cartline set Quantity = '" & txtQuantity.Text & "' WHERE CartID = '" & strCartID & "' AND ProductID = '" & Request.QueryString("product_code") & "'"
                    cmdSQL = New SqlCommand(strSQLStatement, conn)
                    conn.Open()
                    dr = cmdSQL.ExecuteReader(CommandBehavior.CloseConnection)
                Else
                    conn.Close()
                    strSQLStatement = "INSERT INTO Cartline (CartID, ProductID, ProductName, ProductPrice, Quantity) values('" & strCartID & "', '" & Request.QueryString("product_code") & "', '" & strProductName & "', " & decPrice & ", " & txtQuantity.Text & ")"
                    cmdSQL = New SqlCommand(strSQLStatement, conn)
                    conn.Open()
                    dr = cmdSQL.ExecuteReader(CommandBehavior.CloseConnection)
                End If
                Response.Redirect("ViewCart.aspx")
            End If
        Else
            ' Display errors in the "Errors" div
            errors.Text = "<div class='alert alert-danger'>Quantity must be an integer.</div>"
        End If
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
End Class
