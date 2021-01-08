Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports OfficeOpenXml
Imports System.Web.Script.Serialization

Partial Class qcldownload
  Inherits System.Web.UI.Page
  Private st As Long = HttpContext.Current.Server.ScriptTimeout
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    HttpContext.Current.Server.ScriptTimeout = Integer.MaxValue
    Dim docPK As String = ""
    Dim filePK As String = ""
    Dim downloadType As Integer = 0
    '0=Template
    '1=Attachement
    Dim val() As String = Nothing
    Dim Value As String = ""
    If Request.QueryString("qcl") IsNot Nothing Then
      Value = Request.QueryString("qcl")
      If Request.QueryString("typ") IsNot Nothing Then
        DownloadOfferedQCL(Value)
      Else
        DownloadTmplForQCL(Value)
      End If
    End If
  End Sub

#Region " OFFERED QCL "

  Private Sub DownloadOfferedQCL(ByVal Value As String)

    Dim aVal() As String = Value.Split("|".ToCharArray)

    Dim SerialNo As String = ""
    Dim QCLNo As String = ""
    Dim BOMNo As String = ""

    Try
      SerialNo = aVal(0)
      QCLNo = aVal(1)
      BOMNo = aVal(2)
    Catch ex As Exception
    End Try

    If QCLNo = String.Empty Then
      ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize("Quality Clearance No is required for Template download.") & "');", True)
      HttpContext.Current.Server.ScriptTimeout = st
      Exit Sub
    End If

    Dim TemplateName As String = "QCL_Offered.xlsx"

    Dim tmpFile As String = Server.MapPath("~/App_Templates/" & TemplateName)
    If IO.File.Exists(tmpFile) Then
      Dim FileName As String = Server.MapPath("~/..") & "App_Temp/" & Guid.NewGuid().ToString()
      IO.File.Copy(tmpFile, FileName)
      Dim FileInfo As IO.FileInfo = New IO.FileInfo(FileName)
      Dim xlPk As ExcelPackage = New ExcelPackage(FileInfo)

      '1.
      Dim xlWS As ExcelWorksheet = xlPk.Workbook.Worksheets("QC List")
      Dim r As Integer = 1
      Dim c As Integer = 1
      Dim cnt As Integer = 1

      Dim tmpPO As SIS.PAK.pakQCPO = SIS.PAK.pakQCPO.pakQCPOGetByID(SerialNo)
      With xlWS
        .Cells(1, 3).Value = SerialNo
        .Cells(2, 3).Value = QCLNo
        .Cells(3, 3).Value = BOMNo
        .Cells(4, 3).Value = tmpPO.PONumber
        .Cells(5, 3).Value = tmpPO.VR_BusinessPartner9_BPName
        .Cells(4, 10).Value = tmpPO.ProjectID
        .Cells(5, 10).Value = tmpPO.IDM_Projects4_Description
      End With

      r = 9
      c = 1

      Dim qclItems As List(Of SIS.PAK.pakQCListD) = SIS.PAK.pakQCListD.pakQCListDSelectList(0, 99999, "", False, "", SerialNo, QCLNo)

      For Each tmp As SIS.PAK.pakQCListD In qclItems
        If BOMNo <> String.Empty Then If tmp.BOMNo <> BOMNo Then Continue For
        With xlWS
          c = 1
          .Cells(r, c).Value = cnt
          c += 1
          .Cells(r, c).Value = tmp.BOMNo
          c += 1
          .Cells(r, c).Value = tmp.ItemNo
          c += 1
          .Cells(r, c).Value = tmp.FK_PAK_QCListD_ItemNo.ItemCode
          c += 1
          .Cells(r, c).Value = tmp.FK_PAK_QCListD_ItemNo.Prefix & tmp.FK_PAK_QCListD_ItemNo.ItemDescription
          c += 1
          .Cells(r, c).Value = tmp.FK_PAK_QCListD_ItemNo.PAK_Documents4_cmba
          c += 1
          .Cells(r, c).Value = "*"
          c += 1
          If tmp.FK_PAK_QCListD_ItemNo.UOMQuantity <> "" Then .Cells(r, c).Value = tmp.FK_PAK_QCListD_ItemNo.PAK_Units10_Description
          c += 1
          .Cells(r, c).Value = tmp.FK_PAK_QCListD_ItemNo.Quantity
          c += 1
          If tmp.UOMWeight <> "" Then .Cells(r, c).Value = tmp.FK_PAK_QCListD_ItemNo.PAK_Units11_Description
          c += 1
          .Cells(r, c).Value = tmp.FK_PAK_QCListD_ItemNo.WeightPerUnit
          c += 1
          .Cells(r, c).Value = (tmp.FK_PAK_QCListD_ItemNo.WeightPerUnit * tmp.FK_PAK_QCListD_ItemNo.Quantity).ToString("n")
          c += 1
          .Cells(r, c).Value = tmp.FK_PAK_QCListD_ItemNo.Quantity - tmp.FK_PAK_QCListD_ItemNo.QualityClearedQty
          c += 1
          .Cells(r, c).Value = ((tmp.FK_PAK_QCListD_ItemNo.Quantity - tmp.FK_PAK_QCListD_ItemNo.QualityClearedQty) * tmp.FK_PAK_QCListD_ItemNo.WeightPerUnit)
          c += 1
          'Offered Quantity
          .Cells(r, c).Value = tmp.Quantity
          c += 1
          .Cells(r, c).Value = tmp.QCM_InspectionStages8_Description
          c += 1
          'Cleared Qty
          Dim tmpQty As Decimal = tmp.Quantity
          If tmp.QualityClearedQty <> "" Then
            If Convert.ToDecimal(tmp.QualityClearedQty) > 0 Then
              tmpQty = tmp.QualityClearedQty
            End If
          End If
          .Cells(r, c).Value = tmpQty
          c += 1
          .Cells(r, c).Value = tmp.Remarks
          cnt += 1
          r += 1
        End With

      Next

      xlPk.Save()
      xlPk.Dispose()

      Response.Clear()
      Response.AppendHeader("content-disposition", "attachment; filename=QCPrint_" & SerialNo & "_" & QCLNo & ".xlsx")
      Response.ContentType = SIS.SYS.Utilities.ApplicationSpacific.ContentType(TemplateName)
      Response.WriteFile(FileName)
      HttpContext.Current.Server.ScriptTimeout = st
      Response.End()
    End If
  End Sub

#End Region


#Region " TMPL FOR QCL "

  Private Function WriteBItemXL(ByVal xlWS As ExcelWorksheet, ByVal r As Integer, ByVal SerialNo As Integer, ByVal BOMNo As Integer, ByVal pItemNo As Integer, ByRef cnt As Integer, ByVal qcDs As List(Of SIS.PAK.pakQCListD)) As Integer
    Dim Items As List(Of SIS.PAK.pakPOBItems) = SIS.PAK.pakPOBItems.GetByParentPOBItemNo(SerialNo, BOMNo, pItemNo, "")
    If Items.Count > 0 Then
      For Each tmp As SIS.PAK.pakPOBItems In Items
        With xlWS
          Dim c As Integer = 1
          .Cells(r, c).Value = cnt
          c += 1
          .Cells(r, c).Value = tmp.BOMNo
          c += 1
          .Cells(r, c).Value = tmp.ItemNo
          c += 1
          .Cells(r, c).Value = tmp.ItemCode
          c += 1
          .Cells(r, c).Value = tmp.Prefix & tmp.ItemDescription
          If Not tmp.Bottom Then
            .Cells(r, c).Style.Font.Bold = True
            .Cells(r, c).Style.Font.Color.SetColor(tmp.GetColor)
            .Cells(r, c, r, c + 10).Style.Locked = True
          End If
          c += 1
          .Cells(r, c).Value = tmp.PAK_Documents4_cmba
          c += 1
          If tmp.Bottom Then
            Dim OfferedQuantity As Decimal = 0
            Dim InspectionStage As String = ""
            For Each qcD As SIS.PAK.pakQCListD In qcDs
              If qcD.ItemNo = tmp.ItemNo And qcD.BOMNo = tmp.BOMNo Then
                OfferedQuantity = qcD.Quantity
                Select Case qcD.InspectionStageID
                  Case "1"
                    InspectionStage = "S"
                  Case "2"
                    InspectionStage = "F"
                  Case "3"
                    InspectionStage = "SF"
                  Case ""
                    InspectionStage = ""
                End Select
                Exit For
              End If
            Next
            .Cells(r, c).Value = "*"
            c += 1
            If tmp.UOMQuantity <> "" Then .Cells(r, c).Value = tmp.PAK_Units10_Description
            c += 1
            .Cells(r, c).Value = tmp.Quantity
            c += 1
            If tmp.UOMWeight <> "" Then .Cells(r, c).Value = tmp.PAK_Units11_Description
            c += 1
            .Cells(r, c).Value = tmp.WeightPerUnit
            c += 1
            .Cells(r, c).Value = Math.Round(tmp.WeightPerUnit * tmp.Quantity, 4)
            c += 1
            .Cells(r, c).Value = tmp.Quantity - tmp.QualityClearedQty
            c += 1
            .Cells(r, c).Value = Math.Round((tmp.Quantity - tmp.QualityClearedQty) * tmp.WeightPerUnit, 4)
            c += 1
            If Not AllowNegativeBalance Then
              If tmp.Quantity - tmp.QualityClearedQty <= 0 Then
                .Cells(r, c, r, c + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                .Cells(r, c, r, c + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)
                .Cells(r, c, r, c + 1).Style.Locked = True
              End If
            End If
            .Cells(r, c).Value = IIf(OfferedQuantity = 0, "", OfferedQuantity)
            c += 1
            .Cells(r, c).Value = InspectionStage
            c += 1
          End If
          cnt += 1
          r += 1
          If Not tmp.Bottom Then
            r = WriteBItemXL(xlWS, r, tmp.SerialNo, tmp.BOMNo, tmp.ItemNo, cnt, qcDs)
          End If
        End With
      Next
    End If
    Return r
  End Function
  Dim AllowNegativeBalance As Boolean = False
  Dim PORTRequired As Boolean = False
  Private Sub DownloadTmplForQCL(ByVal Value As String)
    AllowNegativeBalance = Convert.ToBoolean(ConfigurationManager.AppSettings("AllowNegativeBalance"))

    Dim aVal() As String = Value.Split("|".ToCharArray)

    Dim SerialNo As String = ""
    Dim QCLNo As String = ""
    Dim BOMNo As String = ""

    Try
      SerialNo = aVal(0)
      QCLNo = aVal(1)
      BOMNo = aVal(2)
    Catch ex As Exception
    End Try

    If QCLNo = String.Empty Then
      ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize("Quality Clearance No is required for Template download.") & "');", True)
      HttpContext.Current.Server.ScriptTimeout = st
      Exit Sub
    End If

    Dim TemplateName As String = "QCL_TEMPLATE.xlsx"

    Dim tmpFile As String = Server.MapPath("~/App_Templates/" & TemplateName)
    If IO.File.Exists(tmpFile) Then
      Dim FileName As String = Server.MapPath("~/..") & "App_Temp/" & Guid.NewGuid().ToString()
      IO.File.Copy(tmpFile, FileName)
      Dim FileInfo As IO.FileInfo = New IO.FileInfo(FileName)
      Dim xlPk As ExcelPackage = New ExcelPackage(FileInfo)

      '1.
      Dim xlWS As ExcelWorksheet = xlPk.Workbook.Worksheets("Data")
      Dim r As Integer = 1
      Dim c As Integer = 1
      Dim cnt As Integer = 1


      Dim tmpPO As SIS.PAK.pakPO = SIS.PAK.pakPO.pakPOGetByID(SerialNo)
      With xlWS
        .Cells(1, 3).Value = SerialNo
        .Cells(2, 3).Value = QCLNo
        .Cells(3, 3).Value = BOMNo
        .Cells(4, 3).Value = tmpPO.PONumber
        .Cells(5, 3).Value = tmpPO.VR_BusinessPartner9_BPName
        .Cells(4, 10).Value = tmpPO.ProjectID
        .Cells(5, 10).Value = tmpPO.IDM_Projects4_Description
      End With

      r = 9
      c = 1
      Try
        PORTRequired = tmpPO.PortRequired
      Catch ex As Exception
      End Try

      Dim POBOMs As List(Of SIS.PAK.pakPOBOM) = SIS.PAK.pakPOBOM.pakPOBOMSelectList(0, 99999, "", False, "", SerialNo)

      Dim qcDs As List(Of SIS.PAK.pakQCListD) = SIS.PAK.pakQCListD.pakQCListDSelectList(0, 99999, "", False, "", SerialNo, QCLNo)

      For Each tmp As SIS.PAK.pakPOBOM In POBOMs
        If BOMNo <> String.Empty Then If tmp.BOMNo <> BOMNo Then Continue For
        With xlWS
          c = 1
          .Cells(r, c).Value = cnt
          c += 1
          .Cells(r, c).Value = tmp.BOMNo
          c += 1
          .Cells(r, c).Value = tmp.ItemNo
          c += 1
          .Cells(r, c).Value = tmp.ItemCode
          c += 1
          .Cells(r, c).Value = tmp.Prefix & tmp.ItemDescription
          If Not tmp.Bottom Then
            .Cells(r, c).Style.Font.Bold = True
            .Cells(r, c).Style.Font.Color.SetColor(tmp.GetColor)
            .Cells(r, c, r, c + 10).Style.Locked = True
          End If
          c += 1
          .Cells(r, c).Value = tmp.PAK_Documents4_cmba
          cnt += 1
          r += 1
        End With
        r = WriteBItemXL(xlWS, r, SerialNo, tmp.BOMNo, tmp.ItemNo, cnt, qcDs)

      Next

      xlPk.Save()
      xlPk.Dispose()

      Response.Clear()
      Response.AppendHeader("content-disposition", "attachment; filename=QCList_" & SerialNo & "_" & QCLNo & ".xlsx")
      Response.ContentType = SIS.SYS.Utilities.ApplicationSpacific.ContentType(TemplateName)
      Response.WriteFile(FileName)
      HttpContext.Current.Server.ScriptTimeout = st
      Response.End()
    End If
  End Sub

#End Region


End Class
