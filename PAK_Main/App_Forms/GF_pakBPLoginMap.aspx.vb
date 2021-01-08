Imports System.Web.Script.Serialization
Partial Class GF_pakBPLoginMap
  Inherits SIS.SYS.GridBase
  Private _InfoUrl As String = "~/PAK_Main/App_Display/DF_pakBPLoginMap.aspx"
  Protected Sub Info_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    Dim oBut As ImageButton = CType(sender, ImageButton)
    Dim aVal() As String = oBut.CommandArgument.ToString.Split(",".ToCharArray)
    Dim RedirectUrl As String = _InfoUrl  & "?LoginID=" & aVal(0) & "&BPID=" & aVal(1) & "&Comp=" & aVal(2)
    Response.Redirect(RedirectUrl)
  End Sub
  Protected Sub GVpakBPLoginMap_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GVpakBPLoginMap.RowCommand
    If e.CommandName.ToLower = "lgedit".ToLower Then
      Try
        Dim LoginID As String = GVpakBPLoginMap.DataKeys(e.CommandArgument).Values("LoginID")  
        Dim BPID As String = GVpakBPLoginMap.DataKeys(e.CommandArgument).Values("BPID")  
        Dim Comp As String = GVpakBPLoginMap.DataKeys(e.CommandArgument).Values("Comp")  
        Dim RedirectUrl As String = TBLpakBPLoginMap.EditUrl & "?LoginID=" & LoginID & "&BPID=" & BPID & "&Comp=" & Comp
        Response.Redirect(RedirectUrl)
      Catch ex As Exception
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize(ex.Message) & "');", True)
      End Try
    End If
    If e.CommandName.ToLower = "initiatewf".ToLower Then
      Try
        Dim LoginID As String = GVpakBPLoginMap.DataKeys(e.CommandArgument).Values("LoginID")  
        Dim BPID As String = GVpakBPLoginMap.DataKeys(e.CommandArgument).Values("BPID")  
        Dim Comp As String = GVpakBPLoginMap.DataKeys(e.CommandArgument).Values("Comp")  
        SIS.PAK.pakBPLoginMap.InitiateWF(LoginID, BPID, Comp)
        GVpakBPLoginMap.DataBind()
      Catch ex As Exception
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize(ex.Message) & "');", True)
      End Try
    End If
    If e.CommandName.ToLower = "approvewf".ToLower Then
      Try
        Dim LoginID As String = GVpakBPLoginMap.DataKeys(e.CommandArgument).Values("LoginID")  
        Dim BPID As String = GVpakBPLoginMap.DataKeys(e.CommandArgument).Values("BPID")  
        Dim Comp As String = GVpakBPLoginMap.DataKeys(e.CommandArgument).Values("Comp")  
        SIS.PAK.pakBPLoginMap.ApproveWF(LoginID, BPID, Comp)
        GVpakBPLoginMap.DataBind()
      Catch ex As Exception
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize(ex.Message) & "');", True)
      End Try
    End If
    If e.CommandName.ToLower = "rejectwf".ToLower Then
      Try
        Dim LoginID As String = GVpakBPLoginMap.DataKeys(e.CommandArgument).Values("LoginID")  
        Dim BPID As String = GVpakBPLoginMap.DataKeys(e.CommandArgument).Values("BPID")  
        Dim Comp As String = GVpakBPLoginMap.DataKeys(e.CommandArgument).Values("Comp")  
        SIS.PAK.pakBPLoginMap.RejectWF(LoginID, BPID, Comp)
        GVpakBPLoginMap.DataBind()
      Catch ex As Exception
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize(ex.Message) & "');", True)
      End Try
    End If
  End Sub
  Protected Sub GVpakBPLoginMap_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVpakBPLoginMap.Init
    DataClassName = "GpakBPLoginMap"
    SetGridView = GVpakBPLoginMap
  End Sub
  Protected Sub TBLpakBPLoginMap_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles TBLpakBPLoginMap.Init
    SetToolBar = TBLpakBPLoginMap
  End Sub
  Protected Sub F_BPID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles F_BPID.TextChanged
    Session("F_BPID") = F_BPID.Text
    InitGridPage()
  End Sub
  Protected Sub F_Comp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles F_Comp.TextChanged
    Session("F_Comp") = F_Comp.Text
    InitGridPage()
  End Sub
  Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
  End Sub
End Class
