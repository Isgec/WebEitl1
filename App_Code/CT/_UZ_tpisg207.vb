Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.TPISG
  Partial Public Class tpisg207
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
    Public Shared Function GetInspectionInBlackRecord(ByVal RequestNo As Integer) As SIS.TPISG.tpisg207
      Dim Sql As String = ""
      Sql &= " select top 1 * from ttpisg207200 "
      Sql &= " where t_bohd='CT_INSPECTIONBLACKCONDITION' "
      Sql &= "   and t_mode=1 "
      Sql &= "   and t_inid=" & RequestNo
      Dim Results As SIS.TPISG.tpisg207 = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Reader.Read() Then
            Results = New SIS.TPISG.tpisg207(Reader)
          End If
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function

    Public Shared Function UZ_tpisg207SelectList(ByVal StartRowIndex As Integer, ByVal MaximumRows As Integer, ByVal OrderBy As String, ByVal SearchState As Boolean, ByVal SearchText As String) As List(Of SIS.TPISG.tpisg207)
      Dim Results As List(Of SIS.TPISG.tpisg207) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          If SearchState Then
            Cmd.CommandText = "sptpisg_LG_207SelectListSearch"
            Cmd.CommandText = "sptpisg207SelectListSearch"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@KeyWord", SqlDbType.NVarChar, 250, SearchText)
          Else
            Cmd.CommandText = "sptpisg_LG_207SelectListFilteres"
            Cmd.CommandText = "sptpisg207SelectListFilteres"
          End If
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@StartRowIndex", SqlDbType.Int, -1, StartRowIndex)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaximumRows", SqlDbType.Int, -1, MaximumRows)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NvarChar, 9, HttpContext.Current.Session("LoginID"))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@OrderBy", SqlDbType.NVarChar, 50, OrderBy)
          Cmd.Parameters.Add("@RecordCount", SqlDbType.Int)
          Cmd.Parameters("@RecordCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Results = New List(Of SIS.TPISG.tpisg207)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.TPISG.tpisg207(Reader))
          End While
          Reader.Close()
          _RecordCount = Cmd.Parameters("@RecordCount").Value
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function UZ_tpisg207Insert(ByVal Record As SIS.TPISG.tpisg207) As SIS.TPISG.tpisg207
      Dim _Result As SIS.TPISG.tpisg207 = tpisg207Insert(Record)
      Return _Result
    End Function
    Public Shared Function UZ_tpisg207Update(ByVal Record As SIS.TPISG.tpisg207) As SIS.TPISG.tpisg207
      Dim _Result As SIS.TPISG.tpisg207 = tpisg207Update(Record)
      Return _Result
    End Function
    Public Shared Function UZ_tpisg207Delete(ByVal Record As SIS.TPISG.tpisg207) As Integer
      Dim _Result as Integer = tpisg207Delete(Record)
      Return _Result
    End Function
    Public Shared Function SetDefaultValues(ByVal sender As System.Web.UI.WebControls.FormView, ByVal e As System.EventArgs) As System.Web.UI.WebControls.FormView
      With sender
        Try
        CType(.FindControl("F_t_inid"), TextBox).Text = 0
        CType(.FindControl("F_t_date"), TextBox).Text = ""
        CType(.FindControl("F_t_pono"), TextBox).Text = ""
        CType(.FindControl("F_t_iref"), TextBox).Text = ""
        CType(.FindControl("F_t_sitm"), TextBox).Text = ""
        CType(.FindControl("F_t_prpo"), TextBox).Text = 0
        CType(.FindControl("F_t_mode"), TextBox).Text = 0
        CType(.FindControl("F_t_bohd"), TextBox).Text = ""
        CType(.FindControl("F_t_Refcntd"), TextBox).Text = 0
        CType(.FindControl("F_t_Refcntu"), TextBox).Text = 0
        Catch ex As Exception
        End Try
      End With
      Return sender
    End Function
  End Class
End Namespace
