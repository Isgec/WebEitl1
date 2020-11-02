Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports OfficeOpenXml
Imports System.Web.Script.Serialization

Partial Class qciPrint
  Inherits System.Web.UI.Page
  Private st As Long = HttpContext.Current.Server.ScriptTimeout
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    HttpContext.Current.Server.ScriptTimeout = Integer.MaxValue
    Response.Clear()
    Try
      If Request.QueryString("qcl") IsNot Nothing Then
        PrintInspection(Request.QueryString("qcl"))
      End If
    Catch ex As Exception
      ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize(ex.Message) & "');", True)
    End Try
    HttpContext.Current.Server.ScriptTimeout = st
    Response.End()
  End Sub

#Region " Print Inspection MICM / IW "

  Private Sub PrintInspection(ByVal Value As String)

    Dim aVal() As String = Value.Split("|".ToCharArray)

    Dim SerialNo As String = ""
    Dim QCLNo As String = ""

    Try
      SerialNo = aVal(0)
      QCLNo = aVal(1)
    Catch ex As Exception
    End Try

    If QCLNo = String.Empty Then
      Throw New Exception("Quality Clearance No is required for Template download.")
      Exit Sub
    End If

    Dim tmpQci As SIS.PAK.pakQCListI = SIS.PAK.pakQCListI.pakQCListIGetByID(SerialNo, QCLNo)
    If tmpQci Is Nothing Then
      Throw New Exception("Report not prepared.")
      Exit Sub
    End If

    Dim TemplateName As String = "MICN_Template.xlsx"
    Dim DownloadName As String = "MICN_" & tmpQci.InspectionReportPrefix & "-" & tmpQci.InspectionReportNo
    If Not tmpQci.IsMICN Then
      TemplateName = "IW_Template.xlsx"
      DownloadName = "IW_" & tmpQci.InspectionReportPrefix & "-" & tmpQci.InspectionReportNo
    End If

    Dim tmpFile As String = Server.MapPath("~/App_Templates/" & TemplateName)
    If IO.File.Exists(tmpFile) Then
      Dim FileName As String = Server.MapPath("~/..") & "App_Temp/" & Guid.NewGuid().ToString()
      IO.File.Copy(tmpFile, FileName)
      Dim FileInfo As IO.FileInfo = New IO.FileInfo(FileName)
      Dim xlPk As ExcelPackage = New ExcelPackage(FileInfo)

      '1.
      Dim xlWS As ExcelWorksheet = xlPk.Workbook.Worksheets("Report")
      Dim r As Integer = 1
      Dim c As Integer = 1
      Dim cnt As Integer = 1

      Dim tmpQcH As SIS.PAK.pakQCListH = SIS.PAK.pakQCListH.pakQCListHGetByID(SerialNo, QCLNo)
      Dim tmpPO As SIS.PAK.pakQCPO = SIS.PAK.pakQCPO.pakQCPOGetByID(SerialNo)
      With xlWS
        .Cells(3, 3).Value = tmpQci.InspectionReportNo
        .Cells(3, 7).Value = tmpQcH.ClearedOn
        .Cells(4, 3).Value = tmpPO.ProjectID
        .Cells(4, 7).Value = tmpPO.IDM_Projects4_Description
        .Cells(5, 3).Value = tmpPO.PONumber
        .Cells(5, 7).Value = tmpPO.PORevision
        .Cells(6, 3).Value = tmpQcH.QCRequestNo
        .Cells(7, 3).Value = tmpPO.SupplierID
        .Cells(7, 7).Value = tmpQci.SubSupplier

        .Cells(8, 3).Value = tmpPO.VR_BusinessPartner9_BPName
        .Cells(8, 7).Value = tmpQci.RefReportNo
        .Cells(9, 3).Value = tmpQci.MainItem
        .Cells(9, 7).Value = tmpQci.ComplianceReportNo

      End With

      r = 13
      c = 1
      cnt = 0
      Dim qclItems As List(Of SIS.PAK.pakQCListD) = SIS.PAK.pakQCListD.pakQCListDSelectList(0, 99999, "", False, "", SerialNo, QCLNo)

      For Each tmp As SIS.PAK.pakQCListD In qclItems
        If tmp.InspectionStageID = 1 Then Continue For
        If tmp.QualityClearedQty <= 0 Then Continue For
        With xlWS
          If r > 12 Then xlWS.InsertRow(r, 1, r + 1)
          cnt += 1
          c = 1
          .Cells(r, c).Value = cnt
          c += 1
          .Cells(r, c).Value = tmp.FK_PAK_QCListD_ItemNo.ItemCode
          c += 1
          .Cells(r, c).Value = tmp.FK_PAK_QCListD_ItemNo.ItemDescription
          c += 1
          Dim d As SIS.PAK.pakDocuments = Nothing
          Try
            d = tmp.FK_PAK_QCListD_ItemNo.FK_PAK_POBItems_DocumentNo
          Catch ex As Exception
          End Try
          If d IsNot Nothing Then .Cells(r, c).Value = d.DocumentID
          c += 1
          If d IsNot Nothing Then .Cells(r, c).Value = d.DocumentRevision
          c += 1
          'Offered Quantity
          .Cells(r, c).Value = tmp.Quantity
          c += 1
          'Cleared Qty
          .Cells(r, c).Value = tmp.QualityClearedQty
          c += 1
          r += 1
        End With

      Next
      If tmpQcH.ClearedBy = "" Then
        With xlWS
          .Cells(2, 3).Value = "Not For Use - " & .Cells(2, 3).Text
          .Cells(r + 10, 2).Value = "PROVISIONAL"
          .Cells(r + 10, 3).Value = "INSPECTION NOT COMPLETED"
        End With
      Else
        With xlWS
          .Cells(r + 10, 2).Value = tmpQcH.ClearedBy
          .Cells(r + 10, 3).Value = tmpQcH.FK_PAK_QCListH_ClearedBy.UserFullName
        End With
      End If

      xlPk.Save()
      xlPk.Dispose()

      If Convert.ToBoolean(ConfigurationManager.AppSettings("PDFReport")) Then
        If pdfWriter.generateXLPDF(FileName) Then
          DownloadName &= ".PDF"
          FileName &= ".PDF"
        Else
          DownloadName &= ".xlsx"
        End If
      Else
        DownloadName &= ".xlsx"
      End If


      Response.AppendHeader("content-disposition", "attachment; filename=" & DownloadName)
      Response.ContentType = SIS.SYS.Utilities.ApplicationSpacific.ContentType(DownloadName)
      Response.WriteFile(FileName)
    End If
  End Sub

#End Region




End Class
