
Imports System.Web.Script.Serialization
Partial Class LC_pakLoginSelecter
  Inherits System.Web.UI.UserControl
  Public Event OKClicked()
  Public Sub ShowLoginSelecter()
    mPopup.Show()
  End Sub
  Public Sub HideLoginSelecter()
    mPopup.Hide()
  End Sub
  Public Property SearchText As String
    Get
      Return ODSpakBPLoginMap.SelectParameters("SearchText").DefaultValue
    End Get
    Set(value As String)
      F_Search.Text = value
      ODSpakBPLoginMap.SelectParameters("SearchText").DefaultValue = value
      GVpakBPLoginMap.DataBind()
    End Set
  End Property
  Public Property SupplierID As String
    Get
      If ViewState("SupplierID") IsNot Nothing Then
        Return Convert.ToString(ViewState("SupplierID"))
      End If
      Return ""
    End Get
    Set(value As String)
      ViewState.Add("SupplierID", value)
    End Set
  End Property
  Public Property Company As String
    Get
      If ViewState("Company") IsNot Nothing Then
        Return Convert.ToString(ViewState("Company"))
      End If
      Return ""
    End Get
    Set(value As String)
      ViewState.Add("Company", value)
      CurrentCompany = ""
      OtherCompany = ""
      Dim cmps As List(Of SIS.COM.comFinanceCompany) = SIS.COM.comFinanceCompany.comFinanceCompanySelectList("")
      For Each cmp As SIS.COM.comFinanceCompany In cmps
        If cmp.FinanceCompany = value Then
          CurrentCompany = cmp.CompanyName
        Else
          If OtherCompany = "" Then
            OtherCompany = cmp.CompanyName
          Else
            OtherCompany &= ", " & cmp.CompanyName
          End If
        End If
      Next
    End Set
  End Property

  Public Property OtherCompany As String
    Get
      If ViewState("OtherCompany") IsNot Nothing Then
        Return Convert.ToString(ViewState("OtherCompany"))
      End If
      Return ""
    End Get
    Set(value As String)
      ViewState.Add("OtherCompany", value)
    End Set
  End Property
  Public Property CurrentCompany As String
    Get
      If ViewState("CurrentCompany") IsNot Nothing Then
        Return Convert.ToString(ViewState("CurrentCompany"))
      End If
      Return ""
    End Get
    Set(value As String)
      ViewState.Add("CurrentCompany", value)
    End Set
  End Property
  Private Sub ODSpakBPLoginMap_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles ODSpakBPLoginMap.Selecting
    If ODSpakBPLoginMap.SelectParameters("SearchText").DefaultValue = "" Then
      e.Cancel = True
    End If
  End Sub

  Private Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
    If F_Search.Text <> "" Then
      SearchText = F_Search.Text
    End If
  End Sub

  Private Sub GVpakBPLoginMap_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GVpakBPLoginMap.RowCommand
    If e.CommandName.ToLower = "lgSelect".ToLower Then
      Try
        Dim LoginID As String = GVpakBPLoginMap.DataKeys(e.CommandArgument).Values("LoginID")
        Dim LoginFound As String = SIS.PAK.pakBPLoginMap.GetLoginID(SupplierID, Company)
        If LoginFound = "" Then
          Dim tmp As New SIS.PAK.pakBPLoginMap
          With tmp
            .LoginID = LoginID
            .Comp = Company
            .BPID = SupplierID
          End With
          SIS.PAK.pakBPLoginMap.InsertData(tmp)
        Else
          If LoginFound <> LoginID Then
            Dim tmp As SIS.PAK.pakBPLoginMap = SIS.PAK.pakBPLoginMap.pakBPLoginMapGetByID(LoginFound, SupplierID, Company)
            With tmp
              .LoginID = LoginID
            End With
            SIS.PAK.pakBPLoginMap.UpdateData(tmp)
            SIS.PAK.pakBPLoginMap.UpdateTransactions(tmp, LoginFound)
          End If
        End If
      Catch ex As Exception
        Dim message As String = New JavaScriptSerializer().Serialize(ex.Message.ToString())
        Dim script As String = String.Format("alert({0});", message)
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, True)
      End Try
    End If
  End Sub

  Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
    mPopup.Hide()
    RaiseEvent OKClicked()
  End Sub
End Class
