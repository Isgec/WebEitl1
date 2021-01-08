Partial Class AF_pakBPLoginMap
  Inherits SIS.SYS.InsertBase
  Protected Sub FVpakBPLoginMap_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles FVpakBPLoginMap.Init
    DataClassName = "ApakBPLoginMap"
    SetFormView = FVpakBPLoginMap
  End Sub
  Protected Sub TBLpakBPLoginMap_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles TBLpakBPLoginMap.Init
    SetToolBar = TBLpakBPLoginMap
  End Sub
  Protected Sub FVpakBPLoginMap_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles FVpakBPLoginMap.DataBound
    SIS.PAK.pakBPLoginMap.SetDefaultValues(sender, e) 
  End Sub
  Protected Sub FVpakBPLoginMap_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles FVpakBPLoginMap.PreRender
    Dim oF_LoginID_Display As Label  = FVpakBPLoginMap.FindControl("F_LoginID_Display")
    Dim oF_LoginID As TextBox  = FVpakBPLoginMap.FindControl("F_LoginID")
    Dim mStr As String = ""
    Dim oTR As IO.StreamReader = New IO.StreamReader(HttpContext.Current.Server.MapPath("~/PAK_Main/App_Create") & "/AF_pakBPLoginMap.js")
    mStr = oTR.ReadToEnd
    oTR.Close()
    oTR.Dispose()
    If Not Page.ClientScript.IsClientScriptBlockRegistered("scriptpakBPLoginMap") Then
      Page.ClientScript.RegisterClientScriptBlock(GetType(System.String), "scriptpakBPLoginMap", mStr)
    End If
    If Request.QueryString("LoginID") IsNot Nothing Then
      CType(FVpakBPLoginMap.FindControl("F_LoginID"), TextBox).Text = Request.QueryString("LoginID")
      CType(FVpakBPLoginMap.FindControl("F_LoginID"), TextBox).Enabled = False
    End If
    If Request.QueryString("BPID") IsNot Nothing Then
      CType(FVpakBPLoginMap.FindControl("F_BPID"), TextBox).Text = Request.QueryString("BPID")
      CType(FVpakBPLoginMap.FindControl("F_BPID"), TextBox).Enabled = False
    End If
    If Request.QueryString("Comp") IsNot Nothing Then
      CType(FVpakBPLoginMap.FindControl("F_Comp"), TextBox).Text = Request.QueryString("Comp")
      CType(FVpakBPLoginMap.FindControl("F_Comp"), TextBox).Enabled = False
    End If
  End Sub
  <System.Web.Services.WebMethod()> _
  <System.Web.Script.Services.ScriptMethod()> _
  Public Shared Function LoginIDCompletionList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
    Return SIS.QCM.qcmUsers.SelectqcmUsersAutoCompleteList(prefixText, count, contextKey)
  End Function
  <System.Web.Services.WebMethod()> _
  Public Shared Function validatePK_pakBPLoginMap(ByVal value As String) As String
    Dim aVal() As String = value.Split(",".ToCharArray)
    Dim mRet As String="0|" & aVal(0)
    Dim LoginID As String = CType(aVal(1),String)
    Dim BPID As String = CType(aVal(2),String)
    Dim Comp As String = CType(aVal(3),String)
    Dim oVar As SIS.PAK.pakBPLoginMap = SIS.PAK.pakBPLoginMap.pakBPLoginMapGetByID(LoginID,BPID,Comp)
    If Not oVar Is Nothing Then
      mRet = "1|" & aVal(0) & "|Record allready exists." 
    End If
    Return mRet
  End Function
  <System.Web.Services.WebMethod()> _
  Public Shared Function validate_FK_SYS_BPLoginMap_LoginID(ByVal value As String) As String
    Dim aVal() As String = value.Split(",".ToCharArray)
    Dim mRet As String="0|" & aVal(0)
    Dim LoginID As String = CType(aVal(1),String)
    Dim oVar As SIS.QCM.qcmUsers = SIS.QCM.qcmUsers.qcmUsersGetByID(LoginID)
    If oVar Is Nothing Then
      mRet = "1|" & aVal(0) & "|Record not found." 
    Else
      mRet = "0|" & aVal(0) & "|" & oVar.DisplayField 
    End If
    Return mRet
  End Function

End Class
