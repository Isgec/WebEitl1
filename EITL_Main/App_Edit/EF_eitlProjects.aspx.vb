Partial Class EF_eitlProjects
  Inherits SIS.SYS.UpdateBase
  Protected Sub FVeitlProjects_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles FVeitlProjects.Init
    DataClassName = "EeitlProjects"
    SetFormView = FVeitlProjects
  End Sub
  Protected Sub TBLeitlProjects_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles TBLeitlProjects.Init
    SetToolBar = TBLeitlProjects
  End Sub
  Protected Sub FVeitlProjects_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles FVeitlProjects.PreRender
		TBLeitlProjects.PrintUrl &= Request.QueryString("ProjectID") & "|"
		Dim mStr As String = ""
		Dim oTR As IO.StreamReader = New IO.StreamReader(HttpContext.Current.Server.MapPath("~/EITL_Main/App_Edit") & "/EF_eitlProjects.js")
    mStr = oTR.ReadToEnd
    oTR.Close()
    oTR.Dispose()
    If Not Page.ClientScript.IsClientScriptBlockRegistered("scripteitlProjects") Then
      Page.ClientScript.RegisterClientScriptBlock(GetType(System.String), "scripteitlProjects", mStr)
    End If
  End Sub
	<System.Web.Services.WebMethod()> _
	<System.Web.Script.Services.ScriptMethod()> _
  Public Shared Function BusinessPartnerIDCompletionList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
    Return SIS.EITL.eitlSuppliers.SelecteitlSuppliersAutoCompleteList(prefixText, count, contextKey)
  End Function
	<System.Web.Services.WebMethod()> _
  Public Shared Function validate_FK_IDM_Projects_BusinessPartnerID(ByVal value As String) As String
    Dim aVal() As String = value.Split(",".ToCharArray)
    Dim mRet As String="0|" & aVal(0)
		Dim BusinessPartnerID As String = CType(aVal(1),String)
		Dim oVar As SIS.EITL.eitlSuppliers = SIS.EITL.eitlSuppliers.eitlSuppliersGetByID(BusinessPartnerID)
    If oVar Is Nothing Then
			mRet = "1|" & aVal(0) & "|Record not found." 
    Else
			mRet = "0|" & aVal(0) & "|" & oVar.DisplayField 
    End If
    Return mRet
  End Function

End Class
