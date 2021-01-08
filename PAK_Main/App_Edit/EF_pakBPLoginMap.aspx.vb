Imports System.Web.Script.Serialization
Partial Class EF_pakBPLoginMap
  Inherits SIS.SYS.UpdateBase
  Public Property Editable() As Boolean
    Get
      If ViewState("Editable") IsNot Nothing Then
        Return CType(ViewState("Editable"), Boolean)
      End If
      Return True
    End Get
    Set(ByVal value As Boolean)
      ViewState.Add("Editable", value)
    End Set
  End Property
  Public Property Deleteable() As Boolean
    Get
      If ViewState("Deleteable") IsNot Nothing Then
        Return CType(ViewState("Deleteable"), Boolean)
      End If
      Return True
    End Get
    Set(ByVal value As Boolean)
      ViewState.Add("Deleteable", value)
    End Set
  End Property
  Public Property PrimaryKey() As String
    Get
      If ViewState("PrimaryKey") IsNot Nothing Then
        Return CType(ViewState("PrimaryKey"), String)
      End If
      Return True
    End Get
    Set(ByVal value As String)
      ViewState.Add("PrimaryKey", value)
    End Set
  End Property
  Protected Sub ODSpakBPLoginMap_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles ODSpakBPLoginMap.Selected
    Dim tmp As SIS.PAK.pakBPLoginMap = CType(e.ReturnValue, SIS.PAK.pakBPLoginMap)
    Editable = tmp.Editable
    Deleteable = tmp.Deleteable
    PrimaryKey = tmp.PrimaryKey
  End Sub
  Protected Sub FVpakBPLoginMap_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles FVpakBPLoginMap.Init
    DataClassName = "EpakBPLoginMap"
    SetFormView = FVpakBPLoginMap
  End Sub
  Protected Sub TBLpakBPLoginMap_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles TBLpakBPLoginMap.Init
    SetToolBar = TBLpakBPLoginMap
  End Sub
  Protected Sub FVpakBPLoginMap_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles FVpakBPLoginMap.PreRender
    TBLpakBPLoginMap.EnableSave = Editable
    TBLpakBPLoginMap.EnableDelete = Deleteable
    Dim mStr As String = ""
    Dim oTR As IO.StreamReader = New IO.StreamReader(HttpContext.Current.Server.MapPath("~/PAK_Main/App_Edit") & "/EF_pakBPLoginMap.js")
    mStr = oTR.ReadToEnd
    oTR.Close()
    oTR.Dispose()
    If Not Page.ClientScript.IsClientScriptBlockRegistered("scriptpakBPLoginMap") Then
      Page.ClientScript.RegisterClientScriptBlock(GetType(System.String), "scriptpakBPLoginMap", mStr)
    End If
  End Sub

End Class
