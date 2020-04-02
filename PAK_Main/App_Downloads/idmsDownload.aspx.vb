Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports OfficeOpenXml
Imports System.Web.Script.Serialization

Partial Class idmsDkgdownload
  Inherits System.Web.UI.Page
  Private st As Long = HttpContext.Current.Server.ScriptTimeout
  Dim ProjectID As String = ""
  Dim SupplierID As String = ""
  Dim UploadStatusID As String = ""
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    HttpContext.Current.Server.ScriptTimeout = Integer.MaxValue
    If Request.QueryString("ProjectID") IsNot Nothing Then
      ProjectID = Request.QueryString("ProjectID")
    End If
    If Request.QueryString("SupplierID") IsNot Nothing Then
      SupplierID = Request.QueryString("SupplierID")
    End If
    If Request.QueryString("UploadStatusID") IsNot Nothing Then
      UploadStatusID = Request.QueryString("UploadStatusID")
    End If
    DownloadXL()
  End Sub

  Private Sub DownloadXL()

    Dim TemplateName As String = "IDMS_TEMPLATE.xlsx"

    Dim tmpFile As String = Server.MapPath("~/App_Templates/" & TemplateName)
    If IO.File.Exists(tmpFile) Then
      Dim FileName As String = Server.MapPath("~/..") & "App_Temp/" & Guid.NewGuid().ToString()
      IO.File.Copy(tmpFile, FileName)
      Dim FileInfo As IO.FileInfo = New IO.FileInfo(FileName)
      Dim xlPk As ExcelPackage = New ExcelPackage(FileInfo)

      Dim xlWS As ExcelWorksheet = xlPk.Workbook.Worksheets("Data")
      Dim r As Integer = 2
      Dim c As Integer = 1
      Dim cnt As Integer = 1


      Dim Recs As List(Of SIS.PAK.pakTCPOLR) = GetIDMSReceipts()

      For Each tmp As SIS.PAK.pakTCPOLR In Recs
        With xlWS
          c = 1
          .Cells(r, c).Value = tmp.ProjectID
          c += 1
          .Cells(r, c).Value = tmp.ItemDescription
          c += 1
          .Cells(r, c).Value = tmp.PONumber
          c += 1
          .Cells(r, c).Value = tmp.BPName
          c += 1
          .Cells(r, c).Value = tmp.ReceiptNo
          c += 1
          .Cells(r, c).Value = tmp.CreatedOn
          c += 1
          .Cells(r, c).Value = tmp.aspnet_Users1_UserFullName
          c += 1
          .Cells(r, c).Value = tmp.PAK_POLineRecStatus5_Description
          c += 1
          .Cells(r, c).Value = ""
          c += 1
          .Cells(r, c).Value = ""
          r += 1
        End With

      Next

      xlPk.Save()
      xlPk.Dispose()

      Response.Clear()
      Response.AppendHeader("content-disposition", "attachment; filename=TCReceipt_" & Now.ToString("yyyyMMddHHmmss") & ".xlsx")
      Response.ContentType = SIS.SYS.Utilities.ApplicationSpacific.ContentType(TemplateName)
      Response.WriteFile(FileName)
      HttpContext.Current.Server.ScriptTimeout = st
      Response.End()
    End If
  End Sub
  Public Function GetIDMSReceipts() As List(Of SIS.PAK.pakTCPOLR)
    Dim Results As New List(Of SIS.PAK.pakTCPOLR)
    Dim Sql As String = ""
    Sql &= "   SELECT "
    Sql &= "     [PAK_POLineRec].* , "
    Sql &= "     [PAK_PO2].[PONumber] AS PONumber, "
    Sql &= "     [PAK_PO2].[SupplierID] AS SupplierID, "
    Sql &= "     [PAK_PO2].[ProjectID] AS ProjectID, "
    Sql &= "     [VR_BusinessPartner].[BPName] As BPName, "
    Sql &= "     [IDM_Projects].[Description] As ProjectName, "
    Sql &= "     [PAK_POLine3].[ItemCode] As ItemCode, "
    Sql &= "     [PAK_POLine3].[ItemDescription] As ItemDescription, "
    Sql &= "     [aspnet_Users1].[UserFullName] AS aspnet_Users1_UserFullName, "
    Sql &= "     [PAK_PO2].[PODescription] AS PAK_PO2_PODescription, "
    Sql &= "     [PAK_POLine3].[ItemCode] AS PAK_POLine3_ItemCode, "
    Sql &= "     [PAK_POLineRecCategory4].[Description] AS PAK_POLineRecCategory4_Description, "
    Sql &= "     [PAK_POLineRecStatus5].[Description] AS PAK_POLineRecStatus5_Description  "
    Sql &= "   FROM [PAK_POLineRec]  "
    Sql &= "   LEFT OUTER JOIN [aspnet_users] AS [aspnet_users1] "
    Sql &= "     ON [PAK_POLineRec].[CreatedBy] = [aspnet_users1].[LoginID] "
    Sql &= "   INNER JOIN [PAK_PO] AS [PAK_PO2] "
    Sql &= "     ON [PAK_POLineRec].[SerialNo] = [PAK_PO2].[SerialNo] "
    Sql &= "   INNER JOIN [PAK_POLine] AS [PAK_POLine3] "
    Sql &= "     ON [PAK_POLineRec].[SerialNo] = [PAK_POLine3].[SerialNo] "
    Sql &= "     AND [PAK_POLineRec].[ItemNo] = [PAK_POLine3].[ItemNo] "
    Sql &= "   LEFT OUTER JOIN [PAK_POLineRecCategory] AS [PAK_POLineRecCategory4] "
    Sql &= "     ON [PAK_POLineRec].[DocumentCategoryID] = [PAK_POLineRecCategory4].[DocumentCategoryID] "
    Sql &= "   LEFT OUTER JOIN [PAK_POLineRecStatus] AS [PAK_POLineRecStatus5] "
    Sql &= "     ON [PAK_POLineRec].[UploadStatusID] = [PAK_POLineRecStatus5].[StatusID] "
    Sql &= "   LEFT OUTER JOIN [IDM_Projects] AS [IDM_Projects] "
    Sql &= "     ON [PAK_PO2].[ProjectID] = [IDM_Projects].[ProjectID] "
    Sql &= "   LEFT OUTER JOIN [VR_BusinessPartner] AS [VR_BusinessPartner] "
    Sql &= "     ON [PAK_PO2].[SupplierID] = [VR_BusinessPartner].[BPID] "
    Sql &= "   WHERE 1 = 1 "
    If ProjectID <> "" Then
      Sql &= "    AND [PAK_PO2].[ProjectID] ='" & ProjectID & "'"
    End If
    If SupplierID <> "" Then
      Sql &= "    AND [PAK_PO2].[SupplierID] ='" & SupplierID & "'"
    End If
    If UploadStatusID <> "" Then
      Sql &= "    AND [PAK_POLineRec].[UploadStatusID] = " & UploadStatusID
    End If
    Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
      Con.Open()
      Using Cmd As SqlCommand = Con.CreateCommand()
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = Sql
        Dim Reader As SqlDataReader = Cmd.ExecuteReader()
        While (Reader.Read())
          Results.Add(New SIS.PAK.pakTCPOLR(Reader))
        End While
        Reader.Close()
      End Using
    End Using
    Return Results
  End Function

End Class
