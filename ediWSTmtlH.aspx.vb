Imports System.Web.Script.Serialization
Partial Class EF_ediWSTmtlH
  Inherits SIS.SYS.UpdateBase
  Partial Class gvBase
    Inherits SIS.SYS.GridBase
  End Class
  Private WithEvents gvediWTmtlDCC As New gvBase
  Protected Sub GVediWTmtlD_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVediWTmtlD.Init
    gvediWTmtlDCC.DataClassName = "GediWTmtlD"
    gvediWTmtlDCC.SetGridView = GVediWTmtlD
  End Sub
  Protected Sub TBLediWTmtlD_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles TBLediWTmtlD.Init
    gvediWTmtlDCC.SetToolBar = TBLediWTmtlD
  End Sub

  Private Sub EF_ediWTmtlH_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
    If HttpContext.Current.Session("LoginID") Is Nothing Then
      SIS.SYS.Utilities.SessionManager.InitializeEnvironment("")
    End If
  End Sub

End Class
