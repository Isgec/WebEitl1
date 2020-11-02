Imports System.Web.Script.Serialization
Partial Class EF_pakPOBOMItem
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
  Protected Sub ODSpakPOBOM_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles ODSpakPOBOM.Selected
    Dim tmp As SIS.PAK.pakPOBOM = CType(e.ReturnValue, SIS.PAK.pakPOBOM)
    Editable = tmp.Editable
    Deleteable = tmp.Deleteable
    PrimaryKey = tmp.PrimaryKey
  End Sub
  Protected Sub FVpakPOBOM_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles FVpakPOBOM.Init
    DataClassName = "EpakPOBOM"
    SetFormView = FVpakPOBOM
  End Sub
  Protected Sub TBLpakPOBOM_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles TBLpakPOBOM.Init
    SetToolBar = TBLpakPOBOM
  End Sub
  Private ShowPopup As Boolean = False
  Protected Sub FVpakPOBOM_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles FVpakPOBOM.PreRender
    TBLpakPOBOM.EnableSave = Editable
    TBLpakPOBOM.EnableDelete = Deleteable
    Dim iR As TextBox = CType(FVpakPOBOM.FindControl("F_ISGECRemarks"), TextBox)
    Dim sR As TextBox = CType(FVpakPOBOM.FindControl("F_SupplierRemarks"), TextBox)

    iR.CssClass = IIf(IsSupplier, "dmytxt", "mytxt")
    iR.Enabled = IIf(IsSupplier, False, True)
    sR.CssClass = IIf(IsSupplier, "mytxt", "dmytxt")
    sR.Enabled = IIf(IsSupplier, True, False)

    Dim mStr As String = ""
    Dim oTR As IO.StreamReader = New IO.StreamReader(HttpContext.Current.Server.MapPath("~/PAK_Main/App_Edit") & "/EF_pakPOBOM.js")
    mStr = oTR.ReadToEnd
    oTR.Close()
    oTR.Dispose()
    If Not Page.ClientScript.IsClientScriptBlockRegistered("scriptpakPOBOM") Then
      Page.ClientScript.RegisterClientScriptBlock(GetType(System.String), "scriptpakPOBOM", mStr)
    End If
    If ShowPopup Then
      mPopup.Show()
    End If

  End Sub
  Partial Class gvBase
    Inherits SIS.SYS.GridBase
  End Class
  Private WithEvents gvpakPOBItemsCC As New gvBase
  Protected Sub GVpakPOBItems_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVpakPOBItems.Init
    gvpakPOBItemsCC.DataClassName = "GpakPOBItems"
    gvpakPOBItemsCC.SetGridView = GVpakPOBItems
  End Sub
  Protected Sub TBLpakPOBItems_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles TBLpakPOBItems.Init
    gvpakPOBItemsCC.SetToolBar = TBLpakPOBItems
  End Sub
  Protected Sub GVpakPOBItems_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GVpakPOBItems.RowCommand
    If e.CommandName.ToLower = "lgedit".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim BOMNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("BOMNo")
        Dim ItemNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("ItemNo")
        Dim RedirectUrl As String = TBLpakPOBItems.EditUrl & "?SerialNo=" & SerialNo & "&BOMNo=" & BOMNo & "&ItemNo=" & ItemNo
        Response.Redirect(RedirectUrl)
      Catch ex As Exception
      End Try
    End If
    If e.CommandName.ToLower = "CopyWF".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim BOMNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("BOMNo")
        Dim ItemNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("ItemNo")
        SIS.PAK.pakPOBItems.CopyCWF(SerialNo, BOMNo, ItemNo)
        GVpakPOBItems.DataBind()
      Catch ex As Exception
      End Try
    End If
    If e.CommandName.ToLower = "DeleteWF".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim BOMNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("BOMNo")
        Dim ItemNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("ItemNo")
        SIS.PAK.pakPOBItems.DeleteWF(SerialNo, BOMNo, ItemNo)
        GVpakPOBItems.DataBind()
      Catch ex As Exception
      End Try
    End If
    If e.CommandName.ToLower = "UnDeleteWF".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim BOMNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("BOMNo")
        Dim ItemNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("ItemNo")
        SIS.PAK.pakPOBItems.UnDeleteWF(SerialNo, BOMNo, ItemNo)
        GVpakPOBItems.DataBind()
      Catch ex As Exception
      End Try
    End If
    If e.CommandName.ToLower = "FreezeWF".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim BOMNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("BOMNo")
        Dim ItemNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("ItemNo")
        SIS.PAK.pakPOBItems.FreezeWF(SerialNo, BOMNo, ItemNo)
        GVpakPOBItems.DataBind()
      Catch ex As Exception
      End Try
    End If
    If e.CommandName.ToLower = "UnFreezeWF".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim BOMNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("BOMNo")
        Dim ItemNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("ItemNo")
        SIS.PAK.pakPOBItems.UnFreezeWF(SerialNo, BOMNo, ItemNo)
        GVpakPOBItems.DataBind()
      Catch ex As Exception
      End Try
    End If
    If e.CommandName.ToLower = "AcceptWF".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim BOMNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("BOMNo")
        Dim ItemNo As Int32 = GVpakPOBItems.DataKeys(e.CommandArgument).Values("ItemNo")
        Dim pItm As SIS.PAK.pakPOBItems = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(SerialNo, BOMNo, ItemNo)
        Select Case pItm.StatusID
          Case pakItemStates.DeleteRequiredBySupplier, pakItemStates.DeleteRequiredByISGEC
            SIS.PAK.pakPOBItems.AcceptDeleteWF(SerialNo, BOMNo, ItemNo)
          Case Else
            'pakItemStates.ChangedByIsgec, pakItemStates.ChangedBySupplier, pakItemStates.CreatedByISGEC, pakItemStates.CreatedBySupplier
            With pItm
              .Changed = False
              .ChangedBySupplier = False
              .StatusID = IIf(IsSupplier, pakItemStates.AcceptedbySupplier, pakItemStates.AcceptedbyISGEC)
              .Accepted = True
              .AcceptedBySupplier = True
              .AcceptedBy = HttpContext.Current.Session("LoginID")
              .AcceptedOn = Now
            End With
            pItm = SIS.PAK.pakPOBItems.UpdateData(pItm)
        End Select
        GVpakPOBItems.DataBind()
      Catch ex As Exception
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", String.Format("alert({0});", New JavaScriptSerializer().Serialize(ex.Message.ToString())), True)
      End Try
    End If

    'Add New Item
    If e.CommandName.ToLower = "NewWF".ToLower Then
      Try
        SerialNo = GVpakPOBItems.DataKeys(e.CommandArgument).Values("SerialNo")
        BOMNo = GVpakPOBItems.DataKeys(e.CommandArgument).Values("BOMNo")
        ItemNo = GVpakPOBItems.DataKeys(e.CommandArgument).Values("ItemNo")
        Dim pItm As SIS.PAK.pakPOBItems = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(SerialNo, BOMNo, ItemNo)
        If pItm.StatusID = pakItemStates.DeleteRequiredBySupplier Or pItm.StatusID = pakItemStates.DeleteRequiredByISGEC Then
          ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", String.Format("alert({0});", New JavaScriptSerializer().Serialize("Record is marked for deletion")), True)
          Exit Sub
        End If
        HeaderText.Text = "Add in: " & pItm.ItemDescription
        AssignVal(New SIS.PAK.pakPOBItems)
        F_SerialNo.Text = pItm.SerialNo
        F_BOMNo.Text = pItm.BOMNo
        F_ItemNo.Text = 0
        RelevantEntry()
        opt1.Visible = True
        opt2.Visible = True
        opt3.Visible = True
        cmdOK.CssClass = "nt-but-success"
        cmdOK.Enabled = True
        ShowPopup = True
      Catch ex As Exception
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", String.Format("alert({0});", New JavaScriptSerializer().Serialize(ex.Message.ToString())), True)
      End Try
    End If
    If e.CommandName.ToLower = "EditWF".ToLower Then
      Try
        SerialNo = GVpakPOBItems.DataKeys(e.CommandArgument).Values("SerialNo")
        BOMNo = GVpakPOBItems.DataKeys(e.CommandArgument).Values("BOMNo")
        ItemNo = GVpakPOBItems.DataKeys(e.CommandArgument).Values("ItemNo")
        Dim pItm As SIS.PAK.pakPOBItems = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(SerialNo, BOMNo, ItemNo)
        If pItm.StatusID = pakItemStates.DeleteRequiredBySupplier Or pItm.StatusID = pakItemStates.DeleteRequiredByISGEC Then
          ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", String.Format("alert({0});", New JavaScriptSerializer().Serialize("Record is marked for deletion")), True)
          Exit Sub
        End If
        Bottom = pItm.Bottom
        HeaderText.Text = "Edit: " & pItm.ItemDescription
        AssignVal(pItm)
        RelevantEntry()
        If Not pItm.Bottom Then
          opt1.Visible = False
          opt2.Visible = False
          opt3.Visible = False
        Else
          opt1.Visible = True
          opt2.Visible = True
          opt3.Visible = True
        End If
        If pItm.Editable Then
          cmdOK.CssClass = "nt-but-success"
          cmdOK.Enabled = True
        Else
          cmdOK.CssClass = "nt-but-grey"
          cmdOK.Enabled = False
        End If
        ShowPopup = True
      Catch ex As Exception
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", String.Format("alert({0});", New JavaScriptSerializer().Serialize(ex.Message.ToString())), True)
      End Try
    End If

  End Sub
  Public ReadOnly Property IsSupplier As Boolean
    Get
      Return HttpContext.Current.Session("IsSupplier")
    End Get
  End Property
  Private Sub RelevantEntry()
    F_ItemCode.CssClass = "mytxt"
    F_ItemCode.Enabled = True
    F_ISGECRemarks.CssClass = "mytxt"
    F_ISGECRemarks.Enabled = True
    F_SupplierItemCode.CssClass = "mytxt"
    F_SupplierItemCode.Enabled = True
    F_SupplierRemarks.CssClass = "mytxt"
    F_SupplierRemarks.Enabled = True
    If IsSupplier Then
      F_ItemCode.CssClass = "dmytxt"
      F_ItemCode.Enabled = False
      F_ISGECRemarks.CssClass = "dmytxt"
      F_ISGECRemarks.Enabled = False
    Else
      F_SupplierItemCode.CssClass = "dmytxt"
      F_SupplierItemCode.Enabled = False
      F_SupplierRemarks.CssClass = "dmytxt"
      F_SupplierRemarks.Enabled = False
    End If
  End Sub
  Private Sub AssignVal(x As SIS.PAK.pakPOBItems)
    F_SerialNo.Text = x.SerialNo
    F_BOMNo.Text = x.BOMNo
    F_ItemNo.Text = x.ItemNo
    F_ItemCode.Text = x.ItemCode
    F_SupplierItemCode.Text = x.SupplierItemCode
    F_ItemDescription.Text = x.ItemDescription
    F_UOMQuantity.SelectedValue = IIf(x.UOMQuantity = "", "8", x.UOMQuantity)
    F_Quantity.Text = x.Quantity
    F_UOMWeight.SelectedValue = IIf(x.UOMWeight = "", "6", x.UOMWeight)
    F_WeightPerUnit.Text = x.WeightPerUnit
    F_ISGECRemarks.Text = x.ISGECRemarks
    F_SupplierRemarks.Text = x.SupplierRemarks
  End Sub
  Private Function ExtractVal() As SIS.PAK.pakPOBItems
    Dim x As New SIS.PAK.pakPOBItems
    x.SerialNo = F_SerialNo.Text
    x.BOMNo = F_BOMNo.Text
    x.ItemNo = F_ItemNo.Text
    x.ItemCode = F_ItemCode.Text
    x.SupplierItemCode = F_SupplierItemCode.Text
    x.ItemDescription = F_ItemDescription.Text
    x.UOMQuantity = F_UOMQuantity.SelectedValue
    x.Quantity = F_Quantity.Text
    x.UOMWeight = F_UOMWeight.SelectedValue
    x.WeightPerUnit = F_WeightPerUnit.Text
    x.ISGECRemarks = F_ISGECRemarks.Text
    x.SupplierRemarks = F_SupplierRemarks.Text
    Return x
  End Function

  Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
    Dim x As SIS.PAK.pakPOBItems = ExtractVal()
    If x.WeightPerUnit < 0 Or x.Quantity < 0 Then
      ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", String.Format("alert({0});", New JavaScriptSerializer().Serialize("Invalid Quantity or Weight per unit")), True)
      ShowPopup = True
      Exit Sub
    End If
    Dim isNew As Boolean = IIf(x.ItemNo = 0, True, False)
    If isNew Then
      Dim pItm As SIS.PAK.pakPOBItems = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(SerialNo, BOMNo, ItemNo)
      With x
        If IsSupplier Then
          .CreatedBySupplier = True
          .StatusID = pakItemStates.CreatedBySupplier
          .ChangedBySupplier = True
        Else
          .Free = True
          .StatusID = pakItemStates.CreatedByISGEC
          .Changed = True
        End If
        .Active = True
        .DeletedInERP = False
        .ItemNo = SIS.PAK.pakPOBItems.GetMaxItemNo(SerialNo, BOMNo) + 1
        .ParentItemNo = ItemNo
        .Root = False
        .Middle = False
        .Bottom = True
        .ItemLevel = pItm.ItemLevel + 1
        .Prefix = .Prefix.PadLeft(.ItemLevel, Chr(187))
        .DivisionID = pItm.DivisionID
        .ElementID = pItm.ElementID
        .TotalWeight = SIS.PAK.pakPO.GetTotalWeight(.Quantity, .WeightPerUnit, .UOMQuantity, .UOMWeight)
      End With
      x = SIS.PAK.pakPOBItems.InsertData(x)
      With pItm
        If Not .Root Then
          .Middle = True
          .Bottom = False
        Else
          .Middle = False
          .Bottom = False
        End If
      End With
      pItm = SIS.PAK.pakPOBItems.UpdateData(pItm)
      GVpakPOBItems.DataBind()
    Else
      Dim pItm As SIS.PAK.pakPOBItems = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(SerialNo, BOMNo, ItemNo)
      Dim isChanged As Boolean = False
      If Not isChanged Then If pItm.ItemCode <> x.ItemCode Then isChanged = True
      If Not isChanged Then If pItm.SupplierItemCode <> x.SupplierItemCode Then isChanged = True
      If Not isChanged Then If pItm.ItemDescription <> x.ItemDescription Then isChanged = True
      If Not isChanged Then If pItm.UOMQuantity <> x.UOMQuantity Then isChanged = True
      If Not isChanged Then If pItm.Quantity <> x.Quantity Then isChanged = True
      If Not isChanged Then If pItm.UOMWeight <> x.UOMWeight Then isChanged = True
      If Not isChanged Then If pItm.WeightPerUnit <> x.WeightPerUnit Then isChanged = True
      If Not isChanged Then If pItm.ISGECRemarks <> x.ISGECRemarks Then isChanged = True
      If Not isChanged Then If pItm.SupplierRemarks <> x.SupplierRemarks Then isChanged = True
      If isChanged Then
        '===========================
        If pItm.FK_PAK_POBItems_SerialNo.QCRequired Then
          If x.Quantity < pItm.QualityClearedQty Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", String.Format("alert({0});", New JavaScriptSerializer().Serialize("Quantity CAN not be less than Quality Cleared Qty.:" & pItm.QualityClearedQty)), True)
            ShowPopup = True
            Exit Sub
          End If
        Else
          If pItm.FK_PAK_POBItems_SerialNo.PortRequired Then
            If x.Quantity < pItm.QuantityDespatchedToPort Then
              ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", String.Format("alert({0});", New JavaScriptSerializer().Serialize("Quantity CAN not be less than Despatched to Port Qty.:" & pItm.QuantityDespatched)), True)
              ShowPopup = True
              Exit Sub
            End If
          Else
            If x.Quantity < pItm.QuantityDespatched Then
              ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", String.Format("alert({0});", New JavaScriptSerializer().Serialize("Quantity CAN not be less than Despatched Qty.:" & pItm.QuantityDespatched)), True)
              ShowPopup = True
              Exit Sub
            End If
          End If

        End If
        '===========================
        With pItm
          .Changed = False
          .ChangedBySupplier = False
          .Accepted = False
          .AcceptedBySupplier = False
          .AcceptedBy = HttpContext.Current.Session("LoginID")
          .AcceptedOn = Now
          If IsSupplier Then
            If .StatusID <> pakItemStates.CreatedBySupplier Then .StatusID = pakItemStates.ChangedBySupplier
            .SupplierItemCode = x.SupplierItemCode
            .SupplierRemarks = x.SupplierRemarks
            .ChangedBySupplier = True
          Else
            If .StatusID <> pakItemStates.CreatedByISGEC Then .StatusID = pakItemStates.ChangedByIsgec
            .ItemCode = x.ItemCode
            .ISGECRemarks = x.ISGECRemarks
            .Changed = True
          End If
          .ItemDescription = x.ItemDescription
          .UOMQuantity = x.UOMQuantity
          .Quantity = x.Quantity
          .UOMWeight = x.UOMWeight
          .WeightPerUnit = x.WeightPerUnit
          .TotalWeight = SIS.PAK.pakPO.GetTotalWeight(.Quantity, .WeightPerUnit, .UOMQuantity, .UOMWeight)
        End With
        pItm = SIS.PAK.pakPOBItems.UpdateData(pItm)
        GVpakPOBItems.DataBind()
      End If
    End If
  End Sub

  Public Property Bottom() As Boolean
    Get
      If ViewState("Bottom") IsNot Nothing Then
        Return CType(ViewState("Bottom"), Boolean)
      End If
      Return True
    End Get
    Set(ByVal value As Boolean)
      ViewState.Add("Bottom", value)
    End Set
  End Property

  Public Property SerialNo() As String
    Get
      If ViewState("SerialNo") IsNot Nothing Then
        Return CType(ViewState("SerialNo"), String)
      End If
      Return "0"
    End Get
    Set(ByVal value As String)
      ViewState.Add("SerialNo", value)
    End Set
  End Property
  Public Property BOMNo() As String
    Get
      If ViewState("BOMNo") IsNot Nothing Then
        Return CType(ViewState("BOMNo"), String)
      End If
      Return "0"
    End Get
    Set(ByVal value As String)
      ViewState.Add("BOMNo", value)
    End Set
  End Property
  Public Property ItemNo() As String
    Get
      If ViewState("ItemNo") IsNot Nothing Then
        Return CType(ViewState("ItemNo"), String)
      End If
      Return "0"
    End Get
    Set(ByVal value As String)
      ViewState.Add("ItemNo", value)
    End Set
  End Property

End Class
