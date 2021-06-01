Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel

Partial Class MISView
  Inherits System.Web.UI.Page
  Private Sub RenderDS(ds As DataSet, BaseName As String, Optional InExcel As Boolean = False, Optional Index As Integer = 0)
    Dim cnt As Integer = 0
    For Each dt As DataTable In ds.Tables
      Dim hDiv As New HtmlGenericControl("div")
      Dim gv As New GridView
      cnt += 1
      gv.ID = BaseName & "Rep_" & cnt
      gv.CssClass = "mis-gv"
      With gv
        .HeaderStyle.BackColor = Drawing.ColorTranslator.FromHtml("#3AC0F2")
        .HeaderStyle.ForeColor = Drawing.Color.White
        .RowStyle.BackColor = Drawing.ColorTranslator.FromHtml("#dcf5f5")  '#A1DCF2
        .AlternatingRowStyle.BackColor = Drawing.Color.White
        .AlternatingRowStyle.ForeColor = Drawing.ColorTranslator.FromHtml("#000")
        .ShowFooter = True
        '.AllowPaging = True
        '.PageSize = 20
        '.PageIndex = 1
        'With .PagerSettings
        '  .Visible = True
        '  .Mode = PagerButtons.Numeric
        'End With
      End With
      'If dt.TableName = "Table" And BaseName <> "BaaN" Then gv.CssClass = "mis-gv mis-count"
      gv.DataSource = dt
      gv.DataBind()
      If Not InExcel Then
        Dim bt As New Button
        With bt
          .Text = "Export to Excel"
          .CommandArgument = BaseName & "_" & cnt
          .CssClass = "nt-but-danger"
          AddHandler .Click, AddressOf abc
        End With
        gv.FooterRow.Cells(0).Controls.Add(bt)
        hDiv.Controls.Add(gv)
        mainContainer.Controls.Add(hDiv)
      Else
        If cnt = Index Then
          ExportToExcel(gv)
        End If
      End If
    Next
    If InExcel Then
    End If

  End Sub
  Private Sub LoadErpdata(Projects As String, Optional InExcel As Boolean = False, Optional Index As Integer = 0)
    Dim Comp As String = "200"
    Dim ds As New DataSet
    Dim Sql As String = ""
    Sql &= " select  "
    Sql &= "  ir.t_ninv as IRNo, "
    Sql &= "  ir.t_ifbp as SupplierID, "
    Sql &= "  ir.t_refr as SupplierName, "
    Sql &= "  ir.t_isup as BillNo, "
    Sql &= "  ir.t_invd as BillDate, "
    Sql &= "  lgr.t_grno as GRNo, "
    Sql &= "  lgr.t_grdt as GRDate, "
    Sql &= "  lgr.t_grbp as TransporterID, "
    Sql &= "  gbp.t_nama as TransporterName, "
    Sql &= " 	ir.t_cdf_pono as PONumber, "
    Sql &= " 	ir.t_cdf_cprj as ProjectID "
    Sql &= " from ttfacp100" & Comp & " as ir "
    Sql &= " left outer join ttfisg002" & Comp & " as lgr on ir.t_ninv = lgr.t_irno "
    Sql &= " left outer join ttccom100" & Comp & " as gbp on gbp.t_bpid = lgr.t_grbp "
    Sql &= " where ir.t_cdf_cprj in ('ES0184','EC0030')"

    Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
      Using Cmd As SqlCommand = Con.CreateCommand()
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = Sql
        Con.Open()
        Dim da As New SqlDataAdapter
        da.SelectCommand = Cmd
        da.Fill(ds)
      End Using
    End Using
    RenderDS(ds, "BaaN", InExcel, Index)
  End Sub

  Private Sub Loaddata(Projects As String, Optional InExcel As Boolean = False, Optional Index As Integer = 0)
    Dim ds As New DataSet
    Dim Sql As String = ""
    Sql &= " SELECT "
    Sql &= "   [ProjectID] "
    Sql &= "  ,[MRNNo] "
    Sql &= "  ,[MRNDate] "
    Sql &= "  ,[SupplierID] "
    Sql &= "  ,[SupplierName] "
    Sql &= "  ,[SupplierInvoiceNo] "
    Sql &= "  ,[SupplierInvoiceDate] "
    Sql &= "  ,[GRorLRNo] "
    Sql &= "  ,[GRorLRDate] "
    Sql &= "  ,[TransporterID] "
    Sql &= "  ,[TransporterName] "
    Sql &= "  ,[VehicleRegistrationNo] "
    Sql &= " FROM [IJTPerks].[dbo].[VR_MRN_Report] where ProjectID IN ('ES0184','EC0030') "
    Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
      Using Cmd As SqlCommand = Con.CreateCommand()
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = Sql
        Con.Open()
        Dim da As New SqlDataAdapter
        da.SelectCommand = Cmd
        da.Fill(ds)
      End Using
    End Using
    RenderDS(ds, "Joomla", InExcel, Index)
  End Sub
  Protected Sub abc(s As Object, e As EventArgs)
    Dim ca() As String = CType(s, Button).CommandArgument.Split("_".ToCharArray)
    Select Case ca(0)
      Case "Joomla"
        Loaddata(F_Projects.Text, True, ca(1))
      Case "BaaN"
        LoadErpdata(F_Projects.Text, True, ca(1))
    End Select
  End Sub
  Protected Sub ExportToExcel(GridView1 As GridView)
    Response.Clear()
    Response.Buffer = True
    Response.AddHeader("content-disposition", "attachment;filename=" & GridView1.ID & ".xls")
    Response.Charset = ""
    Response.ContentType = "application/vnd.ms-excel"
    Using sw As New IO.StringWriter()
      Dim hw As New HtmlTextWriter(sw)
      GridView1.HeaderRow.BackColor = Drawing.Color.White
      For Each cell As TableCell In GridView1.HeaderRow.Cells
        cell.BackColor = GridView1.HeaderStyle.BackColor
      Next
      For Each row As GridViewRow In GridView1.Rows
        row.BackColor = Drawing.Color.White
        For Each cell As TableCell In row.Cells
          If row.RowIndex Mod 2 = 0 Then
            cell.BackColor = GridView1.AlternatingRowStyle.BackColor
          Else
            cell.BackColor = GridView1.RowStyle.BackColor
          End If
          cell.CssClass = "textmode"
        Next
      Next
      GridView1.RenderControl(hw)
      'style to format numbers to string
      Dim style As String = "<style> .textmode { } </style>"
      Response.Write(style)
      Response.Output.Write(sw.ToString())
      Response.Flush()
      Response.[End]()
    End Using
  End Sub

  Public Overrides Sub VerifyRenderingInServerForm(control As Control)
    ' Verifies that the control is rendered
  End Sub

  Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
    If F_Projects.Text = "" Then Exit Sub
    Loaddata(F_Projects.Text)
    LoadErpdata(F_Projects.Text)
  End Sub
End Class
