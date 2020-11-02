Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports OfficeOpenXml
Imports System.Web.Script.Serialization

Partial Class SiteMrn
  Inherits System.Web.UI.Page
  Private st As Long = HttpContext.Current.Server.ScriptTimeout
  Private QCRequired As Boolean = False
  Private PortRequired As Boolean = False
  Private AllowNegativeBalance As Boolean = False
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    HttpContext.Current.Server.ScriptTimeout = Integer.MaxValue
    Try
      If Request.QueryString("SiteRec") IsNot Nothing Then
        DownloadSiteMrn(Request.QueryString("SiteRec"))
      End If
    Catch ex As Exception
    End Try
    HttpContext.Current.Server.ScriptTimeout = st
  End Sub


#Region " SITE MRN "

  Private Sub DownloadSiteMrn(ByVal Value As String)

    Dim aVal() As String = Value.Split("|".ToCharArray)

    Dim ProjectID As String = ""
    Dim RecNo As String = ""
    Dim SerialNo As String = ""
    Dim PkgNo As String = ""

    Try
      ProjectID = aVal(0)
      RecNo = aVal(1)
    Catch ex As Exception
    End Try

    Dim TemplateName As String = "SiteMRN_Template.xlsx"
    Dim FileName As String = ""

    Dim tmpFile As String = HttpContext.Current.Server.MapPath("~/App_Templates/" & TemplateName)
    If IO.File.Exists(tmpFile) Then
      FileName = HttpContext.Current.Server.MapPath("~/..") & "App_Temp/" & Guid.NewGuid().ToString()
      IO.File.Copy(tmpFile, FileName)
      Dim FileInfo As IO.FileInfo = New IO.FileInfo(FileName)
      Dim xlPk As ExcelPackage = New ExcelPackage(FileInfo)

      '1.
      Dim xlWS As ExcelWorksheet = xlPk.Workbook.Worksheets("Site MRN")
      Dim r As Integer = 1
      Dim c As Integer = 1
      Dim cnt As Integer = 1

      '1. Header
      Dim tmpSRec As SIS.PAK.pakSitePkgH = SIS.PAK.pakSitePkgH.pakSitePkgHGetByID(ProjectID, RecNo)
      SerialNo = tmpSRec.SerialNo
      PkgNo = tmpSRec.PkgNo
      Dim tmpPO As SIS.PAK.pakPkgPO = SIS.PAK.pakPkgPO.pakPkgPOGetByID(SerialNo)
      Dim tmpPkg As SIS.PAK.pakPkgListH = SIS.PAK.pakPkgListH.pakPkgListHGetByID(SerialNo, PkgNo)
      With xlWS
        .Cells(1, 9).Value &= " - " & ProjectID
        .Cells(2, 4).Value = RecNo
        .Cells(3, 4).Value = SerialNo
        .Cells(4, 4).Value = PkgNo
        .Cells(5, 4).Value = "" & Convert.ToDateTime(tmpPkg.CreatedOn).ToString("dd/MM/yyyy")
        .Cells(6, 4).Value = tmpPO.ProjectID & " - " & tmpPO.IDM_Projects4_Description
        .Cells(7, 4).Value = tmpPO.PONumber
        .Cells(8, 4).Value = tmpSRec.TotalWeight

        .Cells(2, 12).Value = tmpSRec.CreatedBy & " - " & tmpSRec.aspnet_Users1_UserFullName
        .Cells(2, 18).Value = tmpSRec.CreatedOn
        .Cells(3, 12).Value = tmpSRec.SupplierID & " - " & tmpSRec.FK_PAK_SitePkgH_SupplierID.BPName
        .Cells(4, 12).Value = tmpPkg.SupplierRefNo
        .Cells(5, 12).Value = IIf(tmpSRec.TransporterID <> "", tmpSRec.VR_LorryReceipts9_TransporterName, tmpSRec.TransporterName)
        .Cells(6, 12).Value = tmpSRec.VehicleNo
        .Cells(7, 12).Value = tmpSRec.GRNo
        .Cells(8, 12).Value = "" & Convert.ToDateTime(tmpSRec.GRDate).ToString("dd/MM/yyyy")
      End With

      '2. Data
      r = 12
      c = 1

      Dim PkgItems As List(Of SIS.PAK.pakPkgListD) = SIS.PAK.pakPkgListD.pakPkgListDSelectList(0, 99999, "", False, "", PkgNo, SerialNo)

      For Each tmp As SIS.PAK.pakPkgListD In PkgItems
        With xlWS
          c = 1
          .Cells(r, c).Value = cnt
          c += 1
          .Cells(r, c).Value = tmp.ItemNo
          c += 1
          .Cells(r, c).Value = tmp.FK_PAK_PkgListD_ItemNo.ItemCode
          c += 1
          .Cells(r, c).Value = tmp.FK_PAK_PkgListD_ItemNo.ItemDescription
          c += 1
          If tmp.UOMQuantity <> "" Then .Cells(r, c).Value = tmp.FK_PAK_PkgListD_UOMQuantity.Description
          c += 1
          .Cells(r, c).Value = tmp.Quantity
          c += 1
          If tmp.UOMWeight <> "" Then .Cells(r, c).Value = tmp.FK_PAK_PkgListD_UOMWeight.Description
          c += 1
          .Cells(r, c).Value = tmp.WeightPerUnit
          c += 1
          .Cells(r, c).Value = (tmp.Quantity * tmp.WeightPerUnit).ToString("n")
          c += 1
          .Cells(r, c).Value = tmp.DocumentNo
          c += 1
          .Cells(r, c).Value = tmp.DocumentRevision
          c += 1
          .Cells(r, c).Value = tmp.PAK_PakTypes1_Description
          c += 1
          .Cells(r, c).Value = tmp.PackingMark
          c += 1
          .Cells(r, c).Value = tmp.PackLength
          c += 1
          .Cells(r, c).Value = tmp.PackWidth
          c += 1
          .Cells(r, c).Value = tmp.PackHeight
          c += 1
          .Cells(r, c).Value = tmp.PAK_Units8_Description
          c += 1

          cnt += 1
          r += 1
        End With
      Next


      xlPk.Save()
      xlPk.Dispose()

      Dim DownloadName As String = "SiteMRN_" & SerialNo & "_" & PkgNo
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

      Response.Clear()
      Response.AppendHeader("content-disposition", "attachment; filename=" & DownloadName)
      Response.ContentType = SIS.SYS.Utilities.ApplicationSpacific.ContentType(DownloadName)
      Response.WriteFile(FileName)
      Response.End()

    End If
  End Sub

#End Region


End Class
