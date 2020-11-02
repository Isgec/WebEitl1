Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.PAK
  <DataObject()> _
  Partial Public Class pakQCListI
    Private Shared _RecordCount As Integer
    Private _SerialNo As Int32 = 0
    Private _QCLNo As Int32 = 0
    Private _IsMICN As Boolean = False
    Private _SubSupplier As String = ""
    Private _RefReportNo As String = ""
    Private _MainItem As String = ""
    Private _ComplianceReportNo As String = ""
    Private _POClosed As Boolean = False
    Private _Remarks As String = ""
    Private _InspectionReportPrefix As String = ""
    Private _InspectionReportNo As String = ""
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
    Public Property SerialNo() As Int32
      Get
        Return _SerialNo
      End Get
      Set(ByVal value As Int32)
        _SerialNo = value
      End Set
    End Property
    Public Property QCLNo() As Int32
      Get
        Return _QCLNo
      End Get
      Set(ByVal value As Int32)
        _QCLNo = value
      End Set
    End Property
    Public Property IsMICN() As Boolean
      Get
        Return _IsMICN
      End Get
      Set(ByVal value As Boolean)
        _IsMICN = value
      End Set
    End Property
    Public Property SubSupplier() As String
      Get
        Return _SubSupplier
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _SubSupplier = ""
         Else
           _SubSupplier = value
         End If
      End Set
    End Property
    Public Property RefReportNo() As String
      Get
        Return _RefReportNo
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _RefReportNo = ""
         Else
           _RefReportNo = value
         End If
      End Set
    End Property
    Public Property MainItem() As String
      Get
        Return _MainItem
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _MainItem = ""
         Else
           _MainItem = value
         End If
      End Set
    End Property
    Public Property ComplianceReportNo() As String
      Get
        Return _ComplianceReportNo
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _ComplianceReportNo = ""
         Else
           _ComplianceReportNo = value
         End If
      End Set
    End Property
    Public Property POClosed() As Boolean
      Get
        Return _POClosed
      End Get
      Set(ByVal value As Boolean)
        _POClosed = value
      End Set
    End Property
    Public Property Remarks() As String
      Get
        Return _Remarks
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _Remarks = ""
         Else
           _Remarks = value
         End If
      End Set
    End Property
    Public Property InspectionReportPrefix() As String
      Get
        Return _InspectionReportPrefix
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _InspectionReportPrefix = ""
         Else
           _InspectionReportPrefix = value
         End If
      End Set
    End Property
    Public Property InspectionReportNo() As String
      Get
        Return _InspectionReportNo
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _InspectionReportNo = ""
         Else
           _InspectionReportNo = value
         End If
      End Set
    End Property
    Public Readonly Property DisplayField() As String
      Get
        Return ""
      End Get
    End Property
    Public Readonly Property PrimaryKey() As String
      Get
        Return _SerialNo & "|" & _QCLNo
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
    Public Class PKpakQCListI
      Private _SerialNo As Int32 = 0
      Private _QCLNo As Int32 = 0
      Public Property SerialNo() As Int32
        Get
          Return _SerialNo
        End Get
        Set(ByVal value As Int32)
          _SerialNo = value
        End Set
      End Property
      Public Property QCLNo() As Int32
        Get
          Return _QCLNo
        End Get
        Set(ByVal value As Int32)
          _QCLNo = value
        End Set
      End Property
    End Class
    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function pakQCListIGetNewRecord() As SIS.PAK.pakQCListI
      Return New SIS.PAK.pakQCListI()
    End Function
    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function pakQCListIGetByID(ByVal SerialNo As Int32, ByVal QCLNo As Int32) As SIS.PAK.pakQCListI
      Dim Results As SIS.PAK.pakQCListI = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakQCListISelectByID"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SerialNo",SqlDbType.Int,SerialNo.ToString.Length, SerialNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@QCLNo",SqlDbType.Int,QCLNo.ToString.Length, QCLNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NvarChar, 9, HttpContext.Current.Session("LoginID"))
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Reader.Read() Then
            Results = New SIS.PAK.pakQCListI(Reader)
          End If
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function pakQCListISelectList(ByVal StartRowIndex As Integer, ByVal MaximumRows As Integer, ByVal OrderBy As String, ByVal SearchState As Boolean, ByVal SearchText As String) As List(Of SIS.PAK.pakQCListI)
      Dim Results As List(Of SIS.PAK.pakQCListI) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          If SearchState Then
            Cmd.CommandText = "sppakQCListISelectListSearch"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@KeyWord", SqlDbType.NVarChar, 250, SearchText)
          Else
            Cmd.CommandText = "sppakQCListISelectListFilteres"
          End If
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@StartRowIndex", SqlDbType.Int, -1, StartRowIndex)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaximumRows", SqlDbType.Int, -1, MaximumRows)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NvarChar, 9, HttpContext.Current.Session("LoginID"))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@OrderBy", SqlDbType.NVarChar, 50, OrderBy)
          Cmd.Parameters.Add("@RecordCount", SqlDbType.Int)
          Cmd.Parameters("@RecordCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Results = New List(Of SIS.PAK.pakQCListI)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.PAK.pakQCListI(Reader))
          End While
          Reader.Close()
          _RecordCount = Cmd.Parameters("@RecordCount").Value
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function pakQCListISelectCount(ByVal SearchState As Boolean, ByVal SearchText As String) As Integer
      Return _RecordCount
    End Function
      'Select By ID One Record Filtered Overloaded GetByID
    <DataObjectMethod(DataObjectMethodType.Insert, True)> _
    Public Shared Function pakQCListIInsert(ByVal Record As SIS.PAK.pakQCListI) As SIS.PAK.pakQCListI
      Dim _Rec As SIS.PAK.pakQCListI = SIS.PAK.pakQCListI.pakQCListIGetNewRecord()
      With _Rec
        .SerialNo = Record.SerialNo
        .QCLNo = Record.QCLNo
        .IsMICN = Record.IsMICN
        .SubSupplier = Record.SubSupplier
        .RefReportNo = Record.RefReportNo
        .MainItem = Record.MainItem
        .ComplianceReportNo = Record.ComplianceReportNo
        .POClosed = Record.POClosed
        .Remarks = Record.Remarks
        .InspectionReportPrefix = Record.InspectionReportPrefix
        .InspectionReportNo = Record.InspectionReportNo
      End With
      Return SIS.PAK.pakQCListI.InsertData(_Rec)
    End Function
    Public Shared Function InsertData(ByVal Record As SIS.PAK.pakQCListI) As SIS.PAK.pakQCListI
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakQCListIInsert"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SerialNo",SqlDbType.Int,11, Record.SerialNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@QCLNo",SqlDbType.Int,11, Record.QCLNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsMICN",SqlDbType.Bit,3, Record.IsMICN)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SubSupplier",SqlDbType.NVarChar,501, Iif(Record.SubSupplier= "" ,Convert.DBNull, Record.SubSupplier))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@RefReportNo",SqlDbType.NVarChar,501, Iif(Record.RefReportNo= "" ,Convert.DBNull, Record.RefReportNo))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MainItem",SqlDbType.NVarChar,501, Iif(Record.MainItem= "" ,Convert.DBNull, Record.MainItem))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ComplianceReportNo",SqlDbType.NVarChar,501, Iif(Record.ComplianceReportNo= "" ,Convert.DBNull, Record.ComplianceReportNo))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@POClosed",SqlDbType.Bit,3, Record.POClosed)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Remarks",SqlDbType.NVarChar,501, Iif(Record.Remarks= "" ,Convert.DBNull, Record.Remarks))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@InspectionReportPrefix",SqlDbType.NVarChar,251, Iif(Record.InspectionReportPrefix= "" ,Convert.DBNull, Record.InspectionReportPrefix))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@InspectionReportNo",SqlDbType.Int,11, Iif(Record.InspectionReportNo= "" ,Convert.DBNull, Record.InspectionReportNo))
          Cmd.Parameters.Add("@Return_SerialNo", SqlDbType.Int, 11)
          Cmd.Parameters("@Return_SerialNo").Direction = ParameterDirection.Output
          Cmd.Parameters.Add("@Return_QCLNo", SqlDbType.Int, 11)
          Cmd.Parameters("@Return_QCLNo").Direction = ParameterDirection.Output
          Con.Open()
          Cmd.ExecuteNonQuery()
          Record.SerialNo = Cmd.Parameters("@Return_SerialNo").Value
          Record.QCLNo = Cmd.Parameters("@Return_QCLNo").Value
        End Using
      End Using
      Return Record
    End Function
    <DataObjectMethod(DataObjectMethodType.Update, True)> _
    Public Shared Function pakQCListIUpdate(ByVal Record As SIS.PAK.pakQCListI) As SIS.PAK.pakQCListI
      Dim _Rec As SIS.PAK.pakQCListI = SIS.PAK.pakQCListI.pakQCListIGetByID(Record.SerialNo, Record.QCLNo)
      With _Rec
        .IsMICN = Record.IsMICN
        .SubSupplier = Record.SubSupplier
        .RefReportNo = Record.RefReportNo
        .MainItem = Record.MainItem
        .ComplianceReportNo = Record.ComplianceReportNo
        .POClosed = Record.POClosed
        .Remarks = Record.Remarks
        .InspectionReportPrefix = Record.InspectionReportPrefix
        .InspectionReportNo = Record.InspectionReportNo
      End With
      Return SIS.PAK.pakQCListI.UpdateData(_Rec)
    End Function
    Public Shared Function UpdateData(ByVal Record As SIS.PAK.pakQCListI) As SIS.PAK.pakQCListI
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakQCListIUpdate"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_SerialNo",SqlDbType.Int,11, Record.SerialNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_QCLNo",SqlDbType.Int,11, Record.QCLNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SerialNo",SqlDbType.Int,11, Record.SerialNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@QCLNo",SqlDbType.Int,11, Record.QCLNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsMICN",SqlDbType.Bit,3, Record.IsMICN)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SubSupplier",SqlDbType.NVarChar,501, Iif(Record.SubSupplier= "" ,Convert.DBNull, Record.SubSupplier))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@RefReportNo",SqlDbType.NVarChar,501, Iif(Record.RefReportNo= "" ,Convert.DBNull, Record.RefReportNo))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MainItem",SqlDbType.NVarChar,501, Iif(Record.MainItem= "" ,Convert.DBNull, Record.MainItem))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ComplianceReportNo",SqlDbType.NVarChar,501, Iif(Record.ComplianceReportNo= "" ,Convert.DBNull, Record.ComplianceReportNo))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@POClosed",SqlDbType.Bit,3, Record.POClosed)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Remarks",SqlDbType.NVarChar,501, Iif(Record.Remarks= "" ,Convert.DBNull, Record.Remarks))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@InspectionReportPrefix",SqlDbType.NVarChar,251, Iif(Record.InspectionReportPrefix= "" ,Convert.DBNull, Record.InspectionReportPrefix))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@InspectionReportNo",SqlDbType.Int,11, Iif(Record.InspectionReportNo= "" ,Convert.DBNull, Record.InspectionReportNo))
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
    Public Shared Function pakQCListIDelete(ByVal Record As SIS.PAK.pakQCListI) As Int32
      Dim _Result as Integer = 0
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakQCListIDelete"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_SerialNo",SqlDbType.Int,Record.SerialNo.ToString.Length, Record.SerialNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_QCLNo",SqlDbType.Int,Record.QCLNo.ToString.Length, Record.QCLNo)
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
      Try
        For Each pi As System.Reflection.PropertyInfo In Me.GetType.GetProperties
          If pi.MemberType = Reflection.MemberTypes.Property Then
            Try
              Dim Found As Boolean = False
              For I As Integer = 0 To Reader.FieldCount - 1
                If Reader.GetName(I).ToLower = pi.Name.ToLower Then
                  Found = True
                  Exit For
                End If
              Next
              If Found Then
                If Convert.IsDBNull(Reader(pi.Name)) Then
                  Select Case Reader.GetDataTypeName(Reader.GetOrdinal(pi.Name))
                    Case "decimal"
                      CallByName(Me, pi.Name, CallType.Let, "0.00")
                    Case "bit"
                      CallByName(Me, pi.Name, CallType.Let, Boolean.FalseString)
                    Case Else
                      CallByName(Me, pi.Name, CallType.Let, String.Empty)
                  End Select
                Else
                  CallByName(Me, pi.Name, CallType.Let, Reader(pi.Name))
                End If
              End If
            Catch ex As Exception
            End Try
          End If
        Next
      Catch ex As Exception
      End Try
    End Sub
    Public Sub New()
    End Sub
  End Class
End Namespace
