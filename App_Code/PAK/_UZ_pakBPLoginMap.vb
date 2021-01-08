Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.PAK
  Partial Public Class pakBPLoginMap
    Public Shared Function UpdateTransactions(map As SIS.PAK.pakBPLoginMap, OldLogin As String) As Boolean
      Dim Sql As String = ""
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Con.Open()
        Sql = ""
        Sql &= " update pak_qclistH set createdby='" & map.LoginID & "' "
        Sql &= " where createdby='" & OldLogin & "' "
        Sql &= " and SerialNo in (select serialno from PAK_PO where supplierid='" & map.BPID & "') "
        Sql &= " "
        Sql &= " update pak_pkglistH set createdby='" & map.LoginID & "' "
        Sql &= " where createdby='" & OldLogin & "' "
        Sql &= " and SerialNo in (select serialno from PAK_PO where supplierid='" & map.BPID & "') "
        Sql &= " "
        Sql &= " update pak_poLineRec set createdby='" & map.LoginID & "' "
        Sql &= " where createdby='" & OldLogin & "' "
        Sql &= " and SerialNo in (select serialno from PAK_PO where supplierid='" & map.BPID & "') "
        Sql &= " "
        Sql &= " update qcm_requests set createdby='" & map.LoginID & "' "
        Sql &= " where createdby='" & OldLogin & "' "
        Sql &= " and supplierid='" & map.BPID & "' "
        Sql &= " "
        Sql &= " update vr_vehiclerequest set requestedby='" & map.LoginID & "' "
        Sql &= " where requestedby='" & OldLogin & "' "
        Sql &= " and supplierid='" & map.BPID & "' "
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Cmd.ExecuteNonQuery()
        End Using
      End Using
      Return True
    End Function
    Public Shared Function UZ_LoginSelecterSelectList(ByVal StartRowIndex As Integer, ByVal MaximumRows As Integer, ByVal OrderBy As String, ByVal SearchState As Boolean, ByVal SearchText As String) As List(Of SIS.PAK.pakBPLoginMap)
      Dim Results As List(Of SIS.PAK.pakBPLoginMap) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetToolsConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppak_LG_BPLoginMapSearch"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@KeyWord", SqlDbType.NVarChar, 250, SearchText)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@StartRowIndex", SqlDbType.Int, -1, StartRowIndex)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaximumRows", SqlDbType.Int, -1, MaximumRows)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NVarChar, 9, "")
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@OrderBy", SqlDbType.NVarChar, 50, "")
          Cmd.Parameters.Add("@RecordCount", SqlDbType.Int)
          Cmd.Parameters("@RecordCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Results = New List(Of SIS.PAK.pakBPLoginMap)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.PAK.pakBPLoginMap(Reader))
          End While
          Reader.Close()
          _RecordCount = Cmd.Parameters("@RecordCount").Value
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function LoginSelecterSelectCount(ByVal SearchState As Boolean, ByVal SearchText As String) As Integer
      Return _RecordCount
    End Function

    Public Shared Function GetSupplierID(LoginID As String, FinComp As String) As String
      Dim SupplierID As String = ""
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetToolsConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "select isnull(bpid,'') from sys_bploginmap where loginid='" & LoginID & "' and comp='" & FinComp & "'"
          Con.Open()
          SupplierID = Cmd.ExecuteScalar
        End Using
      End Using
      Return SupplierID
    End Function
    Public Shared Function GetLoginID(SupplierID As String, FinComp As String) As String
      Dim LoginID As String = ""
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetToolsConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "select isnull(LoginID,'') from sys_bploginmap where bpid='" & SupplierID & "' and comp='" & FinComp & "'"
          Con.Open()
          LoginID = Cmd.ExecuteScalar
        End Using
      End Using
      Return LoginID
    End Function
    Public Function GetColor() As System.Drawing.Color
      Dim mRet As System.Drawing.Color = Drawing.Color.Blue
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
      Dim mRet As Boolean = True
      Return mRet
    End Function
    Public Function GetDeleteable() As Boolean
      Dim mRet As Boolean = True
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
    Public ReadOnly Property InitiateWFVisible() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetVisible()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property InitiateWFEnable() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetEnable()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property ApproveWFVisible() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetVisible()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property ApproveWFEnable() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetEnable()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property RejectWFVisible() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetVisible()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property RejectWFEnable() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetEnable()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public Shared Function InitiateWF(ByVal LoginID As String, ByVal BPID As String, ByVal Comp As String) As SIS.PAK.pakBPLoginMap
      Dim Results As SIS.PAK.pakBPLoginMap = pakBPLoginMapGetByID(LoginID, BPID, Comp)
      Return Results
    End Function
    Public Shared Function ApproveWF(ByVal LoginID As String, ByVal BPID As String, ByVal Comp As String) As SIS.PAK.pakBPLoginMap
      Dim Results As SIS.PAK.pakBPLoginMap = pakBPLoginMapGetByID(LoginID, BPID, Comp)
      Return Results
    End Function
    Public Shared Function RejectWF(ByVal LoginID As String, ByVal BPID As String, ByVal Comp As String) As SIS.PAK.pakBPLoginMap
      Dim Results As SIS.PAK.pakBPLoginMap = pakBPLoginMapGetByID(LoginID, BPID, Comp)
      Return Results
    End Function
    Public Shared Function UZ_pakBPLoginMapSelectList(ByVal StartRowIndex As Integer, ByVal MaximumRows As Integer, ByVal OrderBy As String, ByVal SearchState As Boolean, ByVal SearchText As String, ByVal BPID As String, ByVal Comp As String) As List(Of SIS.PAK.pakBPLoginMap)
      Dim Results As List(Of SIS.PAK.pakBPLoginMap) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetToolsConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          If OrderBy = String.Empty Then OrderBy = "LoginID"
          Cmd.CommandType = CommandType.StoredProcedure
          If SearchState Then
            Cmd.CommandText = "sppak_LG_BPLoginMapSelectListSearch"
            Cmd.CommandText = "sppakBPLoginMapSelectListSearch"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@KeyWord", SqlDbType.NVarChar, 250, SearchText)
          Else
            Cmd.CommandText = "sppak_LG_BPLoginMapSelectListFilteres"
            Cmd.CommandText = "sppakBPLoginMapSelectListFilteres"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_BPID", SqlDbType.NVarChar, 9, IIf(BPID Is Nothing, String.Empty, BPID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_Comp", SqlDbType.NVarChar, 10, IIf(Comp Is Nothing, String.Empty, Comp))
          End If
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@StartRowIndex", SqlDbType.Int, -1, StartRowIndex)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaximumRows", SqlDbType.Int, -1, MaximumRows)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NVarChar, 9, HttpContext.Current.Session("LoginID"))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@OrderBy", SqlDbType.NVarChar, 50, OrderBy)
          Cmd.Parameters.Add("@RecordCount", SqlDbType.Int)
          Cmd.Parameters("@RecordCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Results = New List(Of SIS.PAK.pakBPLoginMap)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.PAK.pakBPLoginMap(Reader))
          End While
          Reader.Close()
          _RecordCount = Cmd.Parameters("@RecordCount").Value
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function UZ_pakBPLoginMapInsert(ByVal Record As SIS.PAK.pakBPLoginMap) As SIS.PAK.pakBPLoginMap
      Try
        Dim LoginID As String = Record.LoginID
        Dim LoginFound As String = SIS.PAK.pakBPLoginMap.GetLoginID(Record.BPID, Record.Comp)
        If LoginFound = "" Then
          SIS.PAK.pakBPLoginMap.InsertData(Record)
        Else
          If LoginFound <> LoginID Then
            Dim tmp As SIS.PAK.pakBPLoginMap = SIS.PAK.pakBPLoginMap.pakBPLoginMapGetByID(LoginFound, Record.BPID, Record.Comp)
            With tmp
              .LoginID = LoginID
            End With
            SIS.PAK.pakBPLoginMap.UpdateData(tmp)
            SIS.PAK.pakBPLoginMap.UpdateTransactions(tmp, LoginFound)
          End If
        End If
      Catch ex As Exception
      End Try
      Return Record
    End Function
    Public Shared Function UZ_pakBPLoginMapUpdate(ByVal Record As SIS.PAK.pakBPLoginMap) As SIS.PAK.pakBPLoginMap
      Dim _Result As SIS.PAK.pakBPLoginMap = pakBPLoginMapUpdate(Record)
      Return _Result
    End Function
    Public Shared Function UZ_pakBPLoginMapDelete(ByVal Record As SIS.PAK.pakBPLoginMap) As Integer
      Dim _Result As Integer = pakBPLoginMapDelete(Record)
      Return _Result
    End Function
    Public Shared Function SetDefaultValues(ByVal sender As System.Web.UI.WebControls.FormView, ByVal e As System.EventArgs) As System.Web.UI.WebControls.FormView
      With sender
        Try
          CType(.FindControl("F_LoginID"), TextBox).Text = ""
          CType(.FindControl("F_LoginID_Display"), Label).Text = ""
          CType(.FindControl("F_BPID"), TextBox).Text = ""
          CType(.FindControl("F_Comp"), DropDownList).SelectedValue = ""
        Catch ex As Exception
        End Try
      End With
      Return sender
    End Function
  End Class
End Namespace
