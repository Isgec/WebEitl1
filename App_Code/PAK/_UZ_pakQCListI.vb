Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.PAK
  Partial Public Class pakQCListI
    Public Shared Function GetNextMICN(ProjectID As String) As Integer
      Dim mRet As Integer = 0
      Dim Sql As String = ""
      Sql &= " Declare @prjFound NvarChar(6) "
      Sql &= " Set @prjFound = (Select ProjectID from PAK_QCListINo where projectID='" & ProjectID & "') "
      Sql &= " If (@prjFound Is null) "
      Sql &= "   insert into PAK_QCListINo (ProjectID) Values('" & ProjectID & "') "
      Sql &= " Update PAK_QCListINo Set LastMicn = LastMicn + 1 where Projectid='" & ProjectID & "' "
      Sql &= " Select LastMicn As LastNo from PAK_QCListINo where Projectid='" & ProjectID & "' "
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          mRet = Cmd.ExecuteScalar
        End Using
      End Using
      Return mRet
    End Function
    Public Shared Function GetNextIW(ProjectID As String) As Integer
      Dim mRet As Integer = 0
      Dim Sql As String = ""
      Sql &= " Declare @prjFound NvarChar(6) "
      Sql &= " Set @prjFound = (Select ProjectID from PAK_QCListINo where projectID='" & ProjectID & "') "
      Sql &= " If (@prjFound Is null) "
      Sql &= "   insert into PAK_QCListINo (ProjectID) Values('" & ProjectID & "') "
      Sql &= " Update PAK_QCListINo Set LastIW = LastIW + 1 where Projectid='" & ProjectID & "' "
      Sql &= " Select LastIW As LastNo from PAK_QCListINo where Projectid='" & ProjectID & "' "
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          mRet = Cmd.ExecuteScalar
        End Using
      End Using
      Return mRet
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
    Public Shared Function SetDefaultValues(ByVal sender As System.Web.UI.WebControls.FormView, ByVal e As System.EventArgs) As System.Web.UI.WebControls.FormView
      With sender
        Try
          CType(.FindControl("F_SerialNo"), TextBox).Text = 0
          CType(.FindControl("F_QCLNo"), TextBox).Text = 0
          CType(.FindControl("F_IsMICN"), CheckBox).Checked = False
          CType(.FindControl("F_SubSupplier"), TextBox).Text = ""
          CType(.FindControl("F_RefReportNo"), TextBox).Text = ""
          CType(.FindControl("F_MainItem"), TextBox).Text = ""
          CType(.FindControl("F_ComplianceReportNo"), TextBox).Text = ""
          CType(.FindControl("F_POClosed"), CheckBox).Checked = False
          CType(.FindControl("F_Remarks"), TextBox).Text = ""
          CType(.FindControl("F_InspectionReportPrefix"), TextBox).Text = ""
          CType(.FindControl("F_InspectionReportNo"), TextBox).Text = 0
        Catch ex As Exception
        End Try
      End With
      Return sender
    End Function
  End Class
End Namespace
