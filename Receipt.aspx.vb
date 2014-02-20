Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail

Partial Class Receipt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Get cookie ID
        Dim strCartID As String
        Dim CookieBack As HttpCookie
        CookieBack = HttpContext.Current.Request.Cookies("CartID")
        strCartID = CookieBack.Value


        ' Open SQL Data Reader
        Dim dr As SqlDataReader
        Dim strSQLStatement As String
        Dim cmdSQL As SqlCommand
        ' Get connection from Web.Config
        Dim strConnectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True"

        strSQLStatement = "SELECT * FROM Customer WHERE OrderlineID = '" + strCartID + "'"

        ' Open connection and execute the SQL statement
        Dim conn As New SqlConnection(strConnectionString)
        cmdSQL = New SqlCommand(strSQLStatement, conn)
        conn.Open()
        dr = cmdSQL.ExecuteReader()

        Dim email As String = ""

        If dr.Read() Then
            ' Set values to the labels
            FirstName.Text = dr.Item("FirstName")
            LastName.Text = dr.Item("LastName")
            email = dr.Item("Email")
        End If
        conn.Close()

        ' Retrieve cart items from the Cartline table by CartID
        ViewOrderline.SelectCommand = "SELECT * FROM Orderline WHERE OrderlineID = '" + strCartID + "'"
        ' Bind the cart table so that it refreshes it everytime this is processed
        ViewOrderline.DataBind()

        strSQLStatement = "SELECT * FROM OrderInfo WHERE OrderlineID = '" + strCartID + "'"

        ' Open connection and execute the SQL statement
        cmdSQL = New SqlCommand(strSQLStatement, conn)
        conn.Open()
        dr = cmdSQL.ExecuteReader()

        If dr.Read() Then
            ' Set values to the labels
            Subtotal.Text = dr.Item("Subtotal")
            Total.Text = dr.Item("Total")
        End If
        conn.Close()

        strSQLStatement = "SELECT * FROM Orderline WHERE OrderlineID = '" + strCartID + "'"

        ' Open connection and execute the SQL statement
        cmdSQL = New SqlCommand(strSQLStatement, conn)
        conn.Open()
        dr = cmdSQL.ExecuteReader()

        If dr.Read() Then

        End If
        conn.Close()

        ' Send email receipt

        strSQLStatement = "DELETE FROM Cartline WHERE CartID = '" + strCartID + "'"

        ' Open connection and execute the SQL statement
        cmdSQL = New SqlCommand(strSQLStatement, conn)
        conn.Open()
        dr = cmdSQL.ExecuteReader()
        conn.Close()

        ' Clear cookies
        CookieBack.Expires = DateTime.Now.AddDays(-1D)
        Response.Cookies.Add(CookieBack)
    End Sub
End Class
