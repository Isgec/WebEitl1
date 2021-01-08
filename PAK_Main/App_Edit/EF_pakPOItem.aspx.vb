Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports OfficeOpenXml
Imports System.Web.Script.Serialization
Partial Class EF_pakPOItem
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
  Public Property UploadVisible() As Boolean
    Get
      If ViewState("UploadVisible") IsNot Nothing Then
        Return CType(ViewState("UploadVisible"), Boolean)
      End If
      Return True
    End Get
    Set(ByVal value As Boolean)
      ViewState.Add("UploadVisible", value)
    End Set
  End Property
  Public Property DUListVisible() As Boolean
    Get
      If ViewState("DUListVisible") IsNot Nothing Then
        Return CType(ViewState("DUListVisible"), Boolean)
      End If
      Return True
    End Get
    Set(ByVal value As Boolean)
      ViewState.Add("DUListVisible", value)
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
  Protected Sub ODSpakPO_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles ODSpakPO.Selected
    Dim tmp As SIS.PAK.pakPO = CType(e.ReturnValue, SIS.PAK.pakPO)
    Editable = tmp.Editable
    Deleteable = tmp.Deleteable
    PrimaryKey = tmp.PrimaryKey
    DUListVisible = False
    UploadVisible = False
    If tmp.UsePackageMaster Then
      If tmp.POStatusID = pakPOStates.Free Then
        DUListVisible = True
      ElseIf tmp.POStatusID = pakPOStates.UnderISGECApproval AndAlso Not tmp.IsSupplier Then
        UploadVisible = True
      ElseIf tmp.POStatusID = pakPOStates.UnderSupplierVerification AndAlso tmp.IsSupplier Then
        UploadVisible = True
      End If
    Else
      If tmp.POStatusID = pakPOStates.UnderISGECApproval AndAlso Not tmp.IsSupplier Then
        UploadVisible = True
      ElseIf tmp.POStatusID = pakPOStates.UnderSupplierVerification AndAlso tmp.IsSupplier Then
        UploadVisible = True
      End If
    End If
  End Sub
  Protected Sub FVpakPO_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles FVpakPO.Init
    DataClassName = "EpakPO"
    SetFormView = FVpakPO
  End Sub
  Protected Sub TBLpakPO_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles TBLpakPO.Init
    SetToolBar = TBLpakPO
  End Sub
  Protected Sub FVpakPO_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles FVpakPO.PreRender
    TBLpakPO.EnableSave = False
    TBLpakPO.EnableDelete = Deleteable
    Dim mStr As String = ""
    Dim oTR As IO.StreamReader = New IO.StreamReader(HttpContext.Current.Server.MapPath("~/PAK_Main/App_Edit") & "/EF_pakPO.js")
    mStr = oTR.ReadToEnd
    oTR.Close()
    oTR.Dispose()
    If Not Page.ClientScript.IsClientScriptBlockRegistered("scriptpakPO") Then
      Page.ClientScript.RegisterClientScriptBlock(GetType(System.String), "scriptpakPO", mStr)
    End If
  End Sub
  Partial Class gvBase
    Inherits SIS.SYS.GridBase
  End Class
  Private WithEvents gvpakPOBOMCC As New gvBase
  Protected Sub GVpakPOBOM_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVpakPOBOM.Init
    gvpakPOBOMCC.DataClassName = "GpakPOBOM"
    gvpakPOBOMCC.SetGridView = GVpakPOBOM
  End Sub
  Protected Sub TBLpakPOBOM_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles TBLpakPOBOM.Init
    gvpakPOBOMCC.SetToolBar = TBLpakPOBOM
  End Sub
  Protected Sub GVpakPOBOM_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GVpakPOBOM.RowCommand
    If e.CommandName.ToLower = "lgedit".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPOBOM.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim BOMNo As Int32 = GVpakPOBOM.DataKeys(e.CommandArgument).Values("BOMNo")
        Dim RedirectUrl As String = TBLpakPOBOM.EditUrl & "?SerialNo=" & SerialNo & "&BOMNo=" & BOMNo
        Response.Redirect(RedirectUrl)
      Catch ex As Exception
      End Try
    End If
    If e.CommandName.ToLower = "Deletewf".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPOBOM.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim BOMNo As Int32 = GVpakPOBOM.DataKeys(e.CommandArgument).Values("BOMNo")
        SIS.PAK.pakPOBOM.DeleteWF(SerialNo, BOMNo)
        GVpakPOBOM.DataBind()
        GVpakUItems.DataBind()
      Catch ex As Exception
        Dim message As String = New JavaScriptSerializer().Serialize(ex.Message)
        Dim script As String = String.Format("alert({0});", message)
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, True)
      End Try
    End If
    If e.CommandName.ToLower = "initiatewf".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPOBOM.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim BOMNo As Int32 = GVpakPOBOM.DataKeys(e.CommandArgument).Values("BOMNo")
        SIS.PAK.pakPOBOM.InitiateWF(SerialNo, BOMNo)
        GVpakPOBOM.DataBind()
      Catch ex As Exception
      End Try
    End If
    If e.CommandName.ToLower = "approvewf".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPOBOM.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim BOMNo As Int32 = GVpakPOBOM.DataKeys(e.CommandArgument).Values("BOMNo")
        SIS.PAK.pakPOBOM.ApproveWF(SerialNo, BOMNo)
        GVpakPOBOM.DataBind()
      Catch ex As Exception
      End Try
    End If
    If e.CommandName.ToLower = "rejectwf".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPOBOM.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim BOMNo As Int32 = GVpakPOBOM.DataKeys(e.CommandArgument).Values("BOMNo")
        SIS.PAK.pakPOBOM.RejectWF(SerialNo, BOMNo)
        GVpakPOBOM.DataBind()
      Catch ex As Exception
      End Try
    End If
    If e.CommandName.ToLower = "completewf".ToLower Then
      Try
        Dim SerialNo As Int32 = GVpakPOBOM.DataKeys(e.CommandArgument).Values("SerialNo")
        Dim BOMNo As Int32 = GVpakPOBOM.DataKeys(e.CommandArgument).Values("BOMNo")
        SIS.PAK.pakPOBOM.CompleteWF(SerialNo, BOMNo)
        GVpakPOBOM.DataBind()
      Catch ex As Exception
      End Try
    End If
  End Sub
  Private WithEvents gvpakUItemsCC As New gvBase
  Protected Sub GVpakUItems_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles GVpakUItems.Init
    gvpakUItemsCC.DataClassName = "GpakUItems"
    gvpakUItemsCC.SetGridView = GVpakUItems
  End Sub
  Protected Sub TBLpakUItems_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles TBLpakUItems.Init
    gvpakUItemsCC.SetToolBar = TBLpakUItems
  End Sub
  Protected Sub GVpakUItems_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GVpakUItems.RowCommand
    If e.CommandName.ToLower = "lgedit".ToLower Then
      Try
        Dim ItemNo As Int32 = GVpakUItems.DataKeys(e.CommandArgument).Values("ItemNo")
        Dim RedirectUrl As String = TBLpakUItems.EditUrl & "?ItemNo=" & ItemNo
        Response.Redirect(RedirectUrl)
      Catch ex As Exception
      End Try
    End If
    If e.CommandName.ToLower = "completewf".ToLower Then
      Try
        Dim ItemNo As Int32 = GVpakUItems.DataKeys(e.CommandArgument).Values("ItemNo")
        SIS.PAK.pakUItems.CompleteWF(ItemNo, PrimaryKey)
        GVpakUItems.DataBind()
        GVpakPOBOM.DataBind()
      Catch ex As Exception
        Dim message As String = New JavaScriptSerializer().Serialize(ex.Message)
        Dim script As String = String.Format("alert({0});", message)
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, True)
      End Try
    End If
  End Sub
  Protected Sub cmdFileUpload_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles cmdFileUpload.Command
    Dim st As Long = HttpContext.Current.Server.ScriptTimeout
    HttpContext.Current.Server.ScriptTimeout = Integer.MaxValue
    Try
      With F_FileUpload
        If .HasFile Then
          Dim tmpPath As String = Server.MapPath("~/../App_Temp")
          Dim tmpName As String = IO.Path.GetRandomFileName()
          Dim tmpFile As String = tmpPath & "\\" & tmpName
          .SaveAs(tmpFile)
          Dim fi As FileInfo = New FileInfo(tmpFile)
          Using xlP As ExcelPackage = New ExcelPackage(fi)
            Dim wsD As ExcelWorksheet = Nothing
            Try
              Try
                wsD = xlP.Workbook.Worksheets("Data")
              Catch ex As Exception
                wsD = Nothing
              End Try
              If wsD Is Nothing Then
                errMsg.Text = "XL File does not have DATA sheet, Invalid MS-EXCEL file."
                HttpContext.Current.Server.ScriptTimeout = st
                Dim message As String = New JavaScriptSerializer().Serialize(errMsg.Text)
                Dim script As String = String.Format("alert({0});", message)
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, True)
                xlP.Dispose()
                Exit Sub
              End If
              Dim SerialNo As Integer = wsD.Cells(3, 2).Text
              Dim BOMNo As Integer = wsD.Cells(4, 2).Text
              Dim PItemNo As Integer = wsD.Cells(5, 2).Text
              Dim POStatusID As String = wsD.Cells(5, 6).Text.Split("-".ToCharArray)(0)

              Dim tmpPO As SIS.PAK.pakPO = SIS.PAK.pakPO.pakPOGetByID(SerialNo)
              If POStatusID <> tmpPO.POStatusID Then
                errMsg.Text = "PO Status does not match with uploaded file."
                HttpContext.Current.Server.ScriptTimeout = st
                Dim message As String = New JavaScriptSerializer().Serialize(errMsg.Text)
                Dim script As String = String.Format("alert({0});", message)
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, True)
                xlP.Dispose()
                Exit Sub
              End If
              If POStatusID = pakPOStates.UnderSupplierVerification Then
                If Not tmpPO.IsSupplier Then
                  errMsg.Text = "File can be uploaded by Supplier Login."
                  HttpContext.Current.Server.ScriptTimeout = st
                  Dim message As String = New JavaScriptSerializer().Serialize(errMsg.Text)
                  Dim script As String = String.Format("alert({0});", message)
                  ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, True)
                  xlP.Dispose()
                  Exit Sub
                End If
              End If
              If POStatusID = pakPOStates.UnderISGECApproval Then
                If tmpPO.IsSupplier Then
                  errMsg.Text = "File can be uploaded by ISGEC."
                  HttpContext.Current.Server.ScriptTimeout = st
                  Dim message As String = New JavaScriptSerializer().Serialize(errMsg.Text)
                  Dim script As String = String.Format("alert({0});", message)
                  ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, True)
                  xlP.Dispose()
                  Exit Sub
                End If
              End If

              Dim tmpBOM As SIS.PAK.pakPOBOM = SIS.PAK.pakPOBOM.pakPOBOMGetByID(SerialNo, BOMNo)
              Dim tmpBItem As SIS.PAK.pakPOBItems = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(SerialNo, BOMNo, PItemNo)

              Dim ItemNo As String = ""
              Dim ParentRecordCode As String = ""
              Dim ItemCode As String = ""
              Dim ItemDesc As String = ""
              Dim UOMQty As String = ""
              Dim Qty As String = ""
              Dim UOMWt As String = ""
              Dim Wt As String = ""
              Dim Remarks As String = ""
              Dim q As Decimal = 0
              Dim w As Decimal = 0
              For I As Integer = 9 To 99000
                ItemNo = wsD.Cells(I, 1).Text
                If ItemNo = "EOR" Then Exit For
                'Read Rest of the data
                ParentRecordCode = wsD.Cells(I, 2).Text
                ItemCode = wsD.Cells(I, 3).Text
                ItemDesc = wsD.Cells(I, 4).Text
                UOMQty = wsD.Cells(I, 5).Text
                Qty = wsD.Cells(I, 6).Text
                UOMWt = wsD.Cells(I, 7).Text
                Wt = wsD.Cells(I, 8).Text
                Remarks = wsD.Cells(I, 9).Text
                'Setup Safe Values
                Try
                  q = Convert.ToDecimal(Qty)
                Catch ex As Exception
                  q = 0
                End Try
                Try
                  w = Convert.ToDecimal(Wt)
                Catch ex As Exception
                  w = 0
                End Try
                If q > 0 Then
                  If UOMQty.ToUpper.Trim = "LOT" Then Continue For
                  If UOMQty <> "" Then
                    Dim tmpUnit As SIS.PAK.pakUnits = SIS.PAK.pakUnits.pakUnitsGetByDescription(UOMQty)
                    If tmpUnit IsNot Nothing Then
                      UOMQty = tmpUnit.UnitID
                    Else
                      Continue For 'As ESGP Comments [Keep UOM Mandatory to be entered by User]
                      UOMQty = 8 'Nos
                    End If
                  Else
                    Continue For 'As ESGP Comments [Keep UOM Mandatory to be entered by User]
                    UOMQty = 8 'Nos
                  End If
                End If
                If w > 0 Then
                  If UOMWt <> "" Then
                    Dim tmpUnit As SIS.PAK.pakUnits = SIS.PAK.pakUnits.pakUnitsGetByDescription(UOMWt)
                    If tmpUnit IsNot Nothing Then
                      UOMWt = tmpUnit.UnitID
                    Else
                      Continue For 'As ESGP Comments [Keep UOM Mandatory to be entered by User]
                      UOMWt = 6 'Kg
                    End If
                  Else
                    Continue For 'As ESGP Comments [Keep UOM Mandatory to be entered by User]
                    UOMWt = 6 'Kg
                  End If
                End If
                If ItemNo <> String.Empty Then
                  'Existing Item
                  Dim tmp As SIS.PAK.pakPOBItems = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(SerialNo, BOMNo, ItemNo)
                  If tmp Is Nothing Then Continue For
                  If tmp.DeletedInERP Then Continue For
                  If tmp.AcceptWFVisible Then Continue For
                  Select Case tmp.StatusID
                    Case pakItemStates.FreezedbyISGEC
                      Continue For
                    Case pakItemStates.DeleteRequiredByISGEC, pakItemStates.DeleteRequiredBySupplier
                      Continue For
                  End Select
                  'Identify what is to be performed Modify or Delete or No Change
                  Dim IsDeleted As Boolean = False
                  Dim IsModified As Boolean = False
                  If ItemDesc = "" Then
                    IsDeleted = True
                  Else
                    If tmpPO.POStatusID = pakPOStates.UnderSupplierVerification Then If ItemCode <> tmp.SupplierItemCode Then IsModified = True
                    If tmpPO.POStatusID = pakPOStates.UnderISGECApproval Then If ItemCode <> tmp.ItemCode Then IsModified = True
                    If ItemDesc <> tmp.ItemDescription Then IsModified = True
                    If tmp.Bottom Then
                      If Convert.ToInt32(UOMQty) <> tmp.UOMQuantity Then IsModified = True
                      If q <> tmp.Quantity Then IsModified = True
                      If Convert.ToInt32(UOMWt) <> tmp.UOMWeight Then IsModified = True
                      If w <> tmp.WeightPerUnit Then IsModified = True
                    End If
                    If tmpPO.POStatusID = pakPOStates.UnderSupplierVerification Then If Remarks <> tmp.SupplierRemarks Then IsModified = True
                    If tmpPO.POStatusID = pakPOStates.UnderISGECApproval Then If Remarks <> tmp.ISGECRemarks Then IsModified = True
                  End If
                  If Not IsDeleted AndAlso Not IsModified Then Continue For
                  If IsModified Then
                    If tmpPO.QCRequired Then
                      If q < tmp.QualityClearedQty Then
                        Qty = tmp.QualityClearedQty
                      End If
                    Else
                      If tmpPO.PortRequired Then
                        If q < tmp.QuantityDespatchedToPort Then
                          q = tmp.QuantityDespatchedToPort
                        End If
                      Else
                        If q < tmp.QuantityDespatched Then
                          q = tmp.QuantityDespatched
                        End If
                      End If
                    End If
                    With tmp
                      .Changed = False
                      .ChangedBySupplier = False
                      .Accepted = False
                      .AcceptedBySupplier = False
                      .AcceptedBy = HttpContext.Current.Session("LoginID")
                      .AcceptedOn = Now
                      If tmp.IsSupplier Then
                        If .StatusID <> pakItemStates.CreatedBySupplier Then .StatusID = pakItemStates.ChangedBySupplier
                        .SupplierItemCode = ItemCode
                        .SupplierRemarks = Remarks
                        .ChangedBySupplier = True
                      Else
                        If .StatusID <> pakItemStates.CreatedByISGEC Then .StatusID = pakItemStates.ChangedByIsgec
                        .ItemCode = ItemCode
                        .ISGECRemarks = Remarks
                        .Changed = True
                      End If
                      .ItemDescription = ItemDesc
                      .UOMQuantity = UOMQty
                      .Quantity = q
                      .UOMWeight = UOMWt
                      .WeightPerUnit = w
                      .TotalWeight = SIS.PAK.pakPO.GetTotalWeight(.Quantity, .WeightPerUnit, .UOMQuantity, .UOMWeight)
                    End With
                    tmp = SIS.PAK.pakPOBItems.UpdateData(tmp)
                  ElseIf IsDeleted Then
                    If SIS.PAK.pakPOBItems.CanBeDeleted(tmp) Then
                      SIS.PAK.pakPOBItems.DeleteWF(tmp.SerialNo, tmp.BOMNo, tmp.ItemNo)
                    End If
                  End If
                Else
                  '===========
                  'New Item
                  '===========
                  Dim pItm As SIS.PAK.pakPOBItems = Nothing
                  If ParentRecordCode <> "" Then
                    Try
                      pItm = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(SerialNo, BOMNo, ParentRecordCode)
                    Catch ex As Exception
                    End Try
                    If pItm Is Nothing Then
                      If tmpPO.POStatusID = pakPOStates.UnderSupplierVerification Then
                        pItm = SIS.PAK.pakPOBItems.pakPOBItemsGetBySupplierItemCode(SerialNo, BOMNo, ParentRecordCode)
                      Else
                        pItm = SIS.PAK.pakPOBItems.pakPOBItemsGetByItemCode(SerialNo, BOMNo, ParentRecordCode)
                      End If
                    End If
                    If pItm IsNot Nothing Then
                      'Parent Item Qty. Should be zero
                      If pItm.Quantity > 0 Then
                        pItm = Nothing
                      End If
                    End If
                  End If
                  If pItm Is Nothing Then
                    pItm = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(SerialNo, BOMNo, tmpBOM.ItemNo)
                  End If
                  Dim tmp As New SIS.PAK.pakPOBItems
                  With tmp
                    .SerialNo = SerialNo
                    .BOMNo = BOMNo
                    If tmpPO.IsSupplier Then
                      .CreatedBySupplier = True
                      .StatusID = pakItemStates.CreatedBySupplier
                      .ChangedBySupplier = True
                      .SupplierItemCode = ItemCode
                      .SupplierRemarks = Remarks
                    Else
                      .Free = True
                      .StatusID = pakItemStates.CreatedByISGEC
                      .Changed = True
                      .ItemCode = ItemCode
                      .ISGECRemarks = Remarks
                    End If
                    .Active = True
                    .DeletedInERP = False
                    .ItemNo = SIS.PAK.pakPOBItems.GetMaxItemNo(SerialNo, BOMNo) + 1
                    .ItemDescription = ItemDesc
                    .UOMQuantity = UOMQty
                    .Quantity = q
                    .UOMWeight = UOMWt
                    .WeightPerUnit = w
                    .ParentItemNo = pItm.ItemNo
                    .Root = False
                    .Middle = False
                    .Bottom = True
                    .ItemLevel = pItm.ItemLevel + 1
                    .Prefix = .Prefix.PadLeft(.ItemLevel, Chr(187))
                    .DivisionID = pItm.DivisionID
                    .ElementID = pItm.ElementID
                    .TotalWeight = SIS.PAK.pakPO.GetTotalWeight(.Quantity, .WeightPerUnit, .UOMQuantity, .UOMWeight)
                  End With
                  tmp = SIS.PAK.pakPOBItems.InsertData(tmp)
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
                End If
              Next
            Catch ex As Exception
              Dim message As String = New JavaScriptSerializer().Serialize(ex.Message.ToString())
              Dim script As String = String.Format("alert({0});", message)
              ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, True)
            End Try
            xlP.Save()
            wsD.Dispose()
            xlP.Dispose()
          End Using
          HttpContext.Current.Server.ScriptTimeout = st
          '======================
          'Dim FileNameForUser As String = F_FileUpload.FileName
          'Response.Clear()
          'Response.AppendHeader("content-disposition", "attachment; filename=" & FileNameForUser)
          'Response.ContentType = SIS.SYS.Utilities.ApplicationSpacific.ContentType(FileNameForUser)
          'Response.WriteFile(tmpFile)
          'Response.End()
        End If
      End With
    Catch ex As Exception
      HttpContext.Current.Server.ScriptTimeout = st
      Dim message As String = New JavaScriptSerializer().Serialize(ex.Message.ToString())
      Dim script As String = String.Format("alert({0});", message)
      ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, True)
    End Try
  End Sub

End Class
