
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Request.QueryString("category") <> "" Then
            ProductCategorySubmenu.SelectCommand = "SELECT * FROM Category WHERE parent != 0 AND MenuGroup = '" + CStr(Request.QueryString("category") + "'")
            ProductCategorySubmenu.DataBind()
            Session("category") = Request.QueryString("category")
        End If

        If Request.QueryString("category_id") <> "" Then
            ProductInventory.SelectCommand = "SELECT * FROM Product WHERE CategoryID = '" + CStr(Request.QueryString("category_id") + "'")
            ProductInventory.DataBind()
            Session("category_id") = Request.QueryString("category_id")
        ElseIf Request.QueryString("category_id") = "" Then
            adTitleLabel.Text = "<h4>Please select a brand category from the menu on the left.</h4>"
        End If
    End Sub
End Class