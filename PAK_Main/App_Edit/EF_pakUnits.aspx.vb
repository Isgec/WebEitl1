Partial Class EF_pakUnits
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
  Protected Sub ODSpakUnits_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles ODSpakUnits.Selected
    Dim tmp As SIS.PAK.pakUnits = CType(e.ReturnValue, SIS.PAK.pakUnits)
    Editable = tmp.Editable
    Deleteable = tmp.Deleteable
    PrimaryKey = tmp.PrimaryKey
  End Sub
  Protected Sub FVpakUnits_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles FVpakUnits.Init
    DataClassName = "EpakUnits"
    SetFormView = FVpakUnits
  End Sub
  Protected Sub TBLpakUnits_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles TBLpakUnits.Init
    SetToolBar = TBLpakUnits
  End Sub
  Protected Sub FVpakUnits_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles FVpakUnits.PreRender
    TBLpakUnits.EnableSave = Editable
    TBLpakUnits.EnableDelete = Deleteable
    Dim mStr As String = ""
    Dim oTR As IO.StreamReader = New IO.StreamReader(HttpContext.Current.Server.MapPath("~/PAK_Main/App_Edit") & "/EF_pakUnits.js")
    mStr = oTR.ReadToEnd
    oTR.Close()
    oTR.Dispose()
    If Not Page.ClientScript.IsClientScriptBlockRegistered("scriptpakUnits") Then
      Page.ClientScript.RegisterClientScriptBlock(GetType(System.String), "scriptpakUnits", mStr)
    End If
  End Sub

End Class
