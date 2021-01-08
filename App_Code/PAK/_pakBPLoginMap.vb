Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.PAK
  <DataObject()> _
  Partial Public Class pakBPLoginMap
    Private Shared _RecordCount As Integer
    Public Property LoginID As String = ""
    Public Property BPID As String = ""
    Public Property Comp As String = ""
    Public Property AddressLine As String = ""
    Public Property aspnet_Users1_UserFullName As String = ""
    Private _FK_SYS_BPLoginMap_LoginID As SIS.QCM.qcmUsers = Nothing
    Public ReadOnly Property ForeColor() As System.Drawing.Color
      Get
        Dim mRet As System.Drawing.Color = Drawing.Color.Blue
        Try
          mRet = GetColor()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property Visible() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetVisible()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property Enable() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetEnable()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public Readonly Property DisplayField() As String
      Get
        Return ""
      End Get
    End Property
    Public Readonly Property PrimaryKey() As String
      Get
        Return _LoginID & "|" & _BPID & "|" & _Comp
      End Get
    End Property
    Public Shared Property RecordCount() As Integer
      Get
        Return _RecordCount
      End Get
      Set(ByVal value As Integer)
        _RecordCount = value
      End Set
    End Property
    Public Class PKpakBPLoginMap
      Private _LoginID As String = ""
      Private _BPID As String = ""
      Private _Comp As String = ""
      Public Property LoginID() As String
        Get
          Return _LoginID
        End Get
        Set(ByVal value As String)
          _LoginID = value
        End Set
      End Property
      Public Property BPID() As String
        Get
          Return _BPID
        End Get
        Set(ByVal value As String)
          _BPID = value
        End Set
      End Property
      Public Property Comp() As String
        Get
          Return _Comp
        End Get
        Set(ByVal value As String)
          _Comp = value
        End Set
      End Property
    End Class
    Public ReadOnly Property FK_SYS_BPLoginMap_LoginID() As SIS.QCM.qcmUsers
      Get
        If _FK_SYS_BPLoginMap_LoginID Is Nothing Then
          _FK_SYS_BPLoginMap_LoginID = SIS.QCM.qcmUsers.qcmUsersGetByID(_LoginID)
        End If
        Return _FK_SYS_BPLoginMap_LoginID
      End Get
    End Property
    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function pakBPLoginMapGetNewRecord() As SIS.PAK.pakBPLoginMap
      Return New SIS.PAK.pakBPLoginMap()
    End Function
    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function pakBPLoginMapGetByID(ByVal LoginID As String, ByVal BPID As String, ByVal Comp As String) As SIS.PAK.pakBPLoginMap
      Dim Results As SIS.PAK.pakBPLoginMap = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetToolsConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakBPLoginMapSelectByID"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NVarChar, LoginID.ToString.Length, LoginID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@BPID", SqlDbType.NVarChar, BPID.ToString.Length, BPID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Comp", SqlDbType.NVarChar, Comp.ToString.Length, Comp)
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Reader.Read() Then
            Results = New SIS.PAK.pakBPLoginMap(Reader)
          End If
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function pakBPLoginMapSelectList(ByVal StartRowIndex As Integer, ByVal MaximumRows As Integer, ByVal OrderBy As String, ByVal SearchState As Boolean, ByVal SearchText As String, ByVal BPID As String, ByVal Comp As String) As List(Of SIS.PAK.pakBPLoginMap)
      Dim Results As List(Of SIS.PAK.pakBPLoginMap) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetToolsConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          If OrderBy = String.Empty Then OrderBy = "LoginID"
          Cmd.CommandType = CommandType.StoredProcedure
          If SearchState Then
            Cmd.CommandText = "sppakBPLoginMapSelectListSearch"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@KeyWord", SqlDbType.NVarChar, 250, SearchText)
          Else
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
    Public Shared Function pakBPLoginMapSelectCount(ByVal SearchState As Boolean, ByVal SearchText As String, ByVal BPID As String, ByVal Comp As String) As Integer
      Return _RecordCount
    End Function
      'Select By ID One Record Filtered Overloaded GetByID
    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function pakBPLoginMapGetByID(ByVal LoginID As String, ByVal BPID As String, ByVal Comp As String, ByVal Filter_BPID As String, ByVal Filter_Comp As String) As SIS.PAK.pakBPLoginMap
      Return pakBPLoginMapGetByID(LoginID, BPID, Comp)
    End Function
    <DataObjectMethod(DataObjectMethodType.Insert, True)> _
    Public Shared Function pakBPLoginMapInsert(ByVal Record As SIS.PAK.pakBPLoginMap) As SIS.PAK.pakBPLoginMap
      Dim _Rec As SIS.PAK.pakBPLoginMap = SIS.PAK.pakBPLoginMap.pakBPLoginMapGetNewRecord()
      With _Rec
        .LoginID = Record.LoginID
        .BPID = Record.BPID
        .Comp = Record.Comp
      End With
      Return SIS.PAK.pakBPLoginMap.InsertData(_Rec)
    End Function
    Public Shared Function InsertData(ByVal Record As SIS.PAK.pakBPLoginMap) As SIS.PAK.pakBPLoginMap
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetToolsConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakBPLoginMapInsert"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NVarChar, 9, Record.LoginID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@BPID", SqlDbType.NVarChar, 10, Record.BPID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Comp", SqlDbType.NVarChar, 11, Record.Comp)
          Cmd.Parameters.Add("@Return_LoginID", SqlDbType.NVarChar, 9)
          Cmd.Parameters("@Return_LoginID").Direction = ParameterDirection.Output
          Cmd.Parameters.Add("@Return_BPID", SqlDbType.NVarChar, 10)
          Cmd.Parameters("@Return_BPID").Direction = ParameterDirection.Output
          Cmd.Parameters.Add("@Return_Comp", SqlDbType.NVarChar, 11)
          Cmd.Parameters("@Return_Comp").Direction = ParameterDirection.Output
          Con.Open()
          Cmd.ExecuteNonQuery()
          Record.LoginID = Cmd.Parameters("@Return_LoginID").Value
          Record.BPID = Cmd.Parameters("@Return_BPID").Value
          Record.Comp = Cmd.Parameters("@Return_Comp").Value
        End Using
      End Using
      Return Record
    End Function
    <DataObjectMethod(DataObjectMethodType.Update, True)> _
    Public Shared Function pakBPLoginMapUpdate(ByVal Record As SIS.PAK.pakBPLoginMap) As SIS.PAK.pakBPLoginMap
      Dim _Rec As SIS.PAK.pakBPLoginMap = SIS.PAK.pakBPLoginMap.pakBPLoginMapGetByID(Record.LoginID, Record.BPID, Record.Comp)
      With _Rec
      End With
      Return SIS.PAK.pakBPLoginMap.UpdateData(_Rec)
    End Function
    Public Shared Function UpdateData(ByVal Record As SIS.PAK.pakBPLoginMap) As SIS.PAK.pakBPLoginMap
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetToolsConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakBPLoginMapUpdate"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_LoginID", SqlDbType.NVarChar, 9, Record.LoginID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_BPID", SqlDbType.NVarChar, 10, Record.BPID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_Comp", SqlDbType.NVarChar, 11, Record.Comp)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NVarChar, 9, Record.LoginID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@BPID", SqlDbType.NVarChar, 10, Record.BPID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Comp", SqlDbType.NVarChar, 11, Record.Comp)
          Cmd.Parameters.Add("@RowCount", SqlDbType.Int)
          Cmd.Parameters("@RowCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Con.Open()
          Cmd.ExecuteNonQuery()
          _RecordCount = Cmd.Parameters("@RowCount").Value
        End Using
      End Using
      Return Record
    End Function
    <DataObjectMethod(DataObjectMethodType.Delete, True)> _
    Public Shared Function pakBPLoginMapDelete(ByVal Record As SIS.PAK.pakBPLoginMap) As Int32
      Dim _Result as Integer = 0
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetToolsConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakBPLoginMapDelete"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_LoginID", SqlDbType.NVarChar, Record.LoginID.ToString.Length, Record.LoginID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_BPID", SqlDbType.NVarChar, Record.BPID.ToString.Length, Record.BPID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_Comp", SqlDbType.NVarChar, Record.Comp.ToString.Length, Record.Comp)
          Cmd.Parameters.Add("@RowCount", SqlDbType.Int)
          Cmd.Parameters("@RowCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Con.Open()
          Cmd.ExecuteNonQuery()
          _RecordCount = Cmd.Parameters("@RowCount").Value
        End Using
      End Using
      Return _RecordCount
    End Function
    Public Sub New(ByVal Reader As SqlDataReader)
      SIS.SYS.SQLDatabase.DBCommon.NewObj(Me, Reader)
    End Sub
    Public Sub New()
    End Sub
  End Class
End Namespace
