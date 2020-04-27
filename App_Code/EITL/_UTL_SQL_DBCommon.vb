Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Namespace SIS.SYS.SQLDatabase
  Public Class DBCommon
    Implements IDisposable
    Public Shared Property BaaNLive As Boolean = False
    Public Shared Property JoomlaLive As Boolean = False
    Public Shared Function GetBaaNConnectionString() As String
      BaaNLive = Convert.ToBoolean(ConfigurationManager.AppSettings("BaaNLive"))

      If BaaNLive Then
        Return "Data Source=192.9.200.129;Initial Catalog=inforerpdb;Integrated Security=False;User Instance=False;Persist Security Info=True;User ID=lalit;Password=scorpions"
      Else
        Return "Data Source=192.9.200.45;Initial Catalog=inforerpdb;Integrated Security=False;User Instance=False;Persist Security Info=True;User ID=lalit;Password=scorpions"
      End If
    End Function
    Public Shared Function GetConnectionString() As String
      JoomlaLive = Convert.ToBoolean(ConfigurationManager.AppSettings("JoomlaLive"))
      If JoomlaLive Then
        Return "Data Source=192.9.200.150;Initial Catalog=IJTPerks;Integrated Security=False;User Instance=False;Persist Security Info=True;User ID=sa;Password=isgec12345"
      Else
        Return "Data Source=.\LGSQL;Initial Catalog=IJTPerks;Integrated Security=False;User Instance=False;User ID=sa;Password=isgec12345"
      End If
    End Function
    Shared Sub New()
      BaaNLive = Convert.ToBoolean(ConfigurationManager.AppSettings("BaaNLive"))
      JoomlaLive = Convert.ToBoolean(ConfigurationManager.AppSettings("JoomlaLive"))
    End Sub
    Public Shared Sub AddDBParameter(ByRef Cmd As SqlCommand, ByVal name As String, ByVal type As SqlDbType, ByVal size As Integer, ByVal value As Object)
      Dim Parm As SqlParameter = Cmd.CreateParameter()
      Parm.ParameterName = name
      Parm.SqlDbType = type
      Parm.Size = size
      Parm.Value = value
      Cmd.Parameters.Add(Parm)
    End Sub
    Private disposedValue As Boolean = False    ' To detect redundant calls
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
      If Not Me.disposedValue Then
        If disposing Then
          ' TODO: free unmanaged resources when explicitly called
        End If

        ' TODO: free shared unmanaged resources
      End If
      Me.disposedValue = True
    End Sub
#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
      ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
      Dispose(True)
      GC.SuppressFinalize(Me)
    End Sub
#End Region

  End Class
End Namespace
