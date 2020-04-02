Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.PAK
  Partial Public Class pakTCPOLR
    'Public Shared Function GetCopyLink(ByVal SerialNo As Int32, ByVal ItemNo As Int32, ByVal UploadNo As Int32) As String
    '  Dim xUpd As SIS.PAK.pakSTCPOLR = SIS.PAK.pakSTCPOLR.pakSTCPOLRGetByID(SerialNo, ItemNo, UploadNo)
    '  Dim mRet As String = HttpContext.Current.Request.Url.Scheme & Uri.SchemeDelimiter & HttpContext.Current.Request.Url.Authority & "/ProjectApi/AttachmentApi.svc/Attachments"
    '  Dim AthHandleS As String = "J_IDMSPOSTORDERREC"
    '  Dim AthHandleT As String = "IDMS_POSTORDER"
    '  Dim IndexS As String = SerialNo & "_" & ItemNo & "_" & UploadNo
    '  Dim IndexT As String = xUpd.ReceiptNo & "_" & xUpd.RevisionNo
    '  Return mRet & "/" & AthHandleS & "/" & AthHandleT & "/" & IndexS & "/" & IndexT
    'End Function
    Public Shared Function GetCopyLink() As String
      Dim UrlAuthority As String = HttpContext.Current.Request.Url.Authority
      If UrlAuthority.ToLower <> "cloud.isgec.co.in" Then
        UrlAuthority = "192.9.200.146"
      End If
      Dim mRet As String = HttpContext.Current.Request.Url.Scheme & Uri.SchemeDelimiter & UrlAuthority & "/ProjectApi/AttachmentApi.svc/Attachments"
      Dim AthHandleS As String = "J_IDMSPOSTORDERREC"
      Dim AthHandleT As String = "IDMSRECEIPTS_200"
      Return mRet & "/" & AthHandleS & "/" & AthHandleT
    End Function
    Public Function GetColor() As System.Drawing.Color
      Dim mRet As System.Drawing.Color = Drawing.Color.Black
      Select Case UploadStatusID
        Case pakTCUploadStates.UnderEvaluation
          mRet = Drawing.Color.Blue
        Case pakTCUploadStates.TechnicallyCleared
          mRet = Drawing.Color.Green
        Case pakTCUploadStates.CommentSubmitted
          mRet = Drawing.Color.Red
        Case pakTCUploadStates.Closed, pakTCUploadStates.Superseded
          mRet = Drawing.Color.Olive
        Case pakTCUploadStates.Free
          If ReceiptNo <> "" Then
            mRet = Drawing.Color.DeepPink
          End If
      End Select
      Return mRet
    End Function
    Public Function GetVisible() As Boolean
      Dim mRet As Boolean = True
      Return mRet
    End Function
    Public Function GetEnable() As Boolean
      Dim mRet As Boolean = True
      Return mRet
    End Function
    Public Function GetEditable() As Boolean
      Dim mRet As Boolean = False
      Return mRet
    End Function
    Public Function GetDeleteable() As Boolean
      Dim mRet As Boolean = False
      Return mRet
    End Function
    Public ReadOnly Property Editable() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetEditable()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property Deleteable() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetDeleteable()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public Shared Function SetDefaultValues(ByVal sender As System.Web.UI.WebControls.FormView, ByVal e As System.EventArgs) As System.Web.UI.WebControls.FormView
      With sender
        Try
          CType(.FindControl("F_UploadNo"), TextBox).Text = ""
          CType(.FindControl("F_DocumentCategoryID"), Object).SelectedValue = ""
          CType(.FindControl("F_CreatedOn"), TextBox).Text = ""
          CType(.FindControl("F_ReceiptNo"), TextBox).Text = ""
          CType(.FindControl("F_RevisionNo"), TextBox).Text = ""
          CType(.FindControl("F_UploadStatusID"), TextBox).Text = ""
          CType(.FindControl("F_UploadStatusID_Display"), Label).Text = ""
          CType(.FindControl("F_SerialNo"), TextBox).Text = ""
          CType(.FindControl("F_SerialNo_Display"), Label).Text = ""
          CType(.FindControl("F_ItemNo"), TextBox).Text = ""
          CType(.FindControl("F_ItemNo_Display"), Label).Text = ""
          CType(.FindControl("F_UploadRemarks"), TextBox).Text = ""
          CType(.FindControl("F_CreatedBy"), TextBox).Text = ""
          CType(.FindControl("F_CreatedBy_Display"), Label).Text = ""
        Catch ex As Exception
        End Try
      End With
      Return sender
    End Function
    Public Property PONumber As String = ""
    Public Property ProjectID As String = ""
    Public Property SupplierID As String = ""
    Public Property BPName As String = ""
    Public Property ProjectName As String = ""
    Public Property ItemCode As String = ""
    Public Property ItemDescription As String = ""

    <DataObjectMethod(DataObjectMethodType.Select)>
    Public Shared Function pakDisplayReceipt(ByVal StartRowIndex As Integer, ByVal MaximumRows As Integer, ByVal OrderBy As String, ByVal SearchState As Boolean, ByVal SearchText As String, ByVal SupplierID As String, ByVal ProjectID As String, ByVal UploadStatusID As Int32) As List(Of SIS.PAK.pakTCPOLR)
      Dim Results As List(Of SIS.PAK.pakTCPOLR) = Nothing
      If OrderBy = "" Then OrderBy = "UploadNo DESC"
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          If SearchState Then
            Cmd.CommandText = "sppak_LG_DisplayReceiptSearch"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@KeyWord", SqlDbType.NVarChar, 250, SearchText)
          Else
            Cmd.CommandText = "sppak_LG_DisplayReceiptFilteres"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_SupplierID", SqlDbType.NVarChar, 9, IIf(SupplierID Is Nothing, String.Empty, SupplierID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_ProjectID", SqlDbType.NVarChar, 6, IIf(ProjectID Is Nothing, String.Empty, ProjectID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_UploadStatusID", SqlDbType.Int, 10, IIf(UploadStatusID = Nothing, 0, UploadStatusID))
          End If
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@StartRowIndex", SqlDbType.Int, -1, StartRowIndex)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaximumRows", SqlDbType.Int, -1, MaximumRows)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NVarChar, 9, HttpContext.Current.Session("LoginID"))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@OrderBy", SqlDbType.NVarChar, 50, OrderBy)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@DivisionID", SqlDbType.Int, 10, Global.System.Web.HttpContext.Current.Session("DivisionID"))
          Cmd.Parameters.Add("@RecordCount", SqlDbType.Int)
          Cmd.Parameters("@RecordCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Results = New List(Of SIS.PAK.pakTCPOLR)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.PAK.pakTCPOLR(Reader))
          End While
          Reader.Close()
          _RecordCount = Cmd.Parameters("@RecordCount").Value
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function pakDisplayReceiptCount(ByVal SearchState As Boolean, ByVal SearchText As String, ByVal SupplierID As String, ByVal ProjectID As String, ByVal UploadStatusID As Int32) As Integer
      Return _RecordCount
    End Function

  End Class
End Namespace
