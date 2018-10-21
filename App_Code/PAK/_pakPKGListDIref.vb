Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.PAK
  <DataObject()>
  Partial Public Class pakPKGListDIRef

    Public Property SerialNo As Integer = 0
    Public Property PKGNo As Integer = 0
    Public Property ItemReference As String = ""
    Public Property SubItem As String = ""
    Public Property TotalWeight As Decimal = 0
    Public Property Quantity As Decimal = 0
    Public Property IRefTotalWeight As Decimal = 0
    Public Property IRefQuantity As Decimal = 0
    Public Property ProgressPercent As Decimal = 0
    Public Property ProgressPercentByQuantity As Decimal = 0
    'Add Total Weight Field and Update It When Received or Quantity Updated
    'Why not pushed
    Public Shared Function GetReceivedPKGSiteDIref(ByVal RecNo As Integer) As List(Of SIS.PAK.pakPKGListDIRef)
      Dim Results As New List(Of SIS.PAK.pakPKGListDIRef)
      Dim Sql As String = ""
      Sql &= " select "
      Sql &= "   pkg.SerialNo, "
      Sql &= "   pkg.RecNo As PkgNo, "
      Sql &= "   sum(pkg.TotalWeight) as TotalWeight, "
      Sql &= "   sum(pkg.Quantity) as Quantity, "
      Sql &= "   (select sum(isnull(aa.TotalWeight,0)) from pak_pobitems as aa where aa.serialno=pkg.serialno and aa.ItemReference=itm.ItemReference and 1 = case when itm.subitem is null then case when aa.subitem is null then 1 else 0 end else case when aa.subitem=itm.subitem then 1 else 0 end end) as IRefTotalWeight, "
      Sql &= "   (select sum(isnull(aa.Quantity,0)) from pak_pobitems as aa where aa.serialno=pkg.serialno and aa.ItemReference=itm.ItemReference and 1 = case when itm.subitem is null then case when aa.subitem is null then 1 else 0 end else case when aa.subitem=itm.subitem then 1 else 0 end end ) as IRefQuantity, "
      Sql &= "   itm.ItemReference, "
      Sql &= "   itm.SubItem "
      Sql &= " from PAK_SitePkgD as pkg "
      Sql &= "   inner join PAK_POBItems as itm "
      Sql &= "      on pkg.SerialNo = itm.SerialNo "
      Sql &= " 	   and pkg.BOMNo = itm.BOMNo "
      Sql &= " 	   and pkg.ItemNo = itm.ItemNo "
      Sql &= " where "
      Sql &= "   pkg.RecNo = " & RecNo
      Sql &= " group by "
      Sql &= "   pkg.SerialNo, "
      Sql &= "   pkg.RecNo, "
      Sql &= "   itm.ItemReference, "
      Sql &= "   itm.SubItem "
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Dim tmp As New SIS.PAK.pakPKGListDIRef(Reader)
            Try
              tmp.ProgressPercent = (tmp.TotalWeight / tmp.IRefTotalWeight) * 100
            Catch ex As Exception
              tmp.ProgressPercent = 0
            End Try
            Try
              tmp.ProgressPercentByQuantity = (tmp.Quantity / tmp.IRefQuantity) * 100
            Catch ex As Exception
              tmp.ProgressPercentByQuantity = 0
            End Try
            Results.Add(tmp)
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function


    Public Shared Function GetDespatchedPKGPortDIref(ByVal PkgNo As Integer) As List(Of SIS.PAK.pakPKGListDIRef)
      Dim Results As New List(Of SIS.PAK.pakPKGListDIRef)
      Dim Sql As String = ""
      Sql &= " select "
      Sql &= "   pkg.SerialNo, "
      Sql &= "   pkg.PKGNo, "
      Sql &= "   sum(pkg.TotalWeight) as TotalWeight, "
      Sql &= "   sum(pkg.Quantity) as Quantity, "
      Sql &= "   (select sum(isnull(aa.TotalWeight,0)) from pak_pobitems as aa where aa.serialno=pkg.serialno and aa.ItemReference=itm.ItemReference and 1 = case when itm.subitem is null then case when aa.subitem is null then 1 else 0 end else case when aa.subitem=itm.subitem then 1 else 0 end end) as IRefTotalWeight, "
      Sql &= "   (select sum(isnull(aa.Quantity,0)) from pak_pobitems as aa where aa.serialno=pkg.serialno and aa.ItemReference=itm.ItemReference and 1 = case when itm.subitem is null then case when aa.subitem is null then 1 else 0 end else case when aa.subitem=itm.subitem then 1 else 0 end end ) as IRefQuantity, "
      Sql &= "   itm.ItemReference, "
      Sql &= "   itm.SubItem "
      Sql &= " from PAK_PkgListD as pkg "
      Sql &= "   inner join PAK_POBItems as itm "
      Sql &= "      on pkg.SerialNo = itm.SerialNo "
      Sql &= " 	   and pkg.BOMNo = itm.BOMNo "
      Sql &= " 	   and pkg.ItemNo = itm.ItemNo "
      Sql &= " where "
      Sql &= "   pkg.PKGNo = " & PkgNo
      Sql &= " group by "
      Sql &= "   pkg.SerialNo, "
      Sql &= "   pkg.PKGNo, "
      Sql &= "   itm.ItemReference, "
      Sql &= "   itm.SubItem "
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Dim tmp As New SIS.PAK.pakPKGListDIRef(Reader)
            Try
              tmp.ProgressPercent = (tmp.TotalWeight / tmp.IRefTotalWeight) * 100
            Catch ex As Exception
              tmp.ProgressPercent = 0
            End Try
            Try
              tmp.ProgressPercentByQuantity = (tmp.Quantity / tmp.IRefQuantity) * 100
            Catch ex As Exception
              tmp.ProgressPercentByQuantity = 0
            End Try
            Results.Add(tmp)
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function

    Public Shared Function GetDespatchedPKGListDIref(ByVal hPKG As SIS.PAK.pakPkgListH) As List(Of SIS.PAK.pakPKGListDIRef)
      Dim Results As New List(Of SIS.PAK.pakPKGListDIRef)
      Dim Sql As String = ""
      Sql &= " select "
      Sql &= "   pkg.SerialNo, "
      Sql &= "   pkg.PKGNo, "
      Sql &= "   sum(pkg.TotalWeight) as TotalWeight, "
      Sql &= "   sum(pkg.Quantity) as Quantity, "
      Sql &= "   (select sum(isnull(aa.TotalWeight,0)) from pak_pobitems as aa where aa.serialno=pkg.serialno and aa.ItemReference=itm.ItemReference and 1 = case when itm.subitem is null then case when aa.subitem is null then 1 else 0 end else case when aa.subitem=itm.subitem then 1 else 0 end end) as IRefTotalWeight, "
      Sql &= "   (select sum(isnull(aa.Quantity,0)) from pak_pobitems as aa where aa.serialno=pkg.serialno and aa.ItemReference=itm.ItemReference and 1 = case when itm.subitem is null then case when aa.subitem is null then 1 else 0 end else case when aa.subitem=itm.subitem then 1 else 0 end end ) as IRefQuantity, "
      Sql &= "   itm.ItemReference, "
      Sql &= "   itm.SubItem "
      Sql &= " from PAK_PkgListD as pkg "
      Sql &= "   inner join PAK_POBItems as itm "
      Sql &= "      on pkg.SerialNo = itm.SerialNo "
      Sql &= " 	   and pkg.BOMNo = itm.BOMNo "
      Sql &= " 	   and pkg.ItemNo = itm.ItemNo "
      Sql &= " where "
      Sql &= "       pkg.SerialNo = " & hPKG.SerialNo
      Sql &= "   and pkg.PKGNo = " & hPKG.PkgNo
      Sql &= " group by "
      Sql &= "   pkg.SerialNo, "
      Sql &= "   pkg.PKGNo, "
      Sql &= "   itm.ItemReference, "
      Sql &= "   itm.SubItem "
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Dim tmp As New SIS.PAK.pakPKGListDIRef(Reader)
            Try
              tmp.ProgressPercent = (tmp.TotalWeight / tmp.IRefTotalWeight) * 100
            Catch ex As Exception
              tmp.ProgressPercent = 0
            End Try
            Try
              tmp.ProgressPercentByQuantity = (tmp.Quantity / tmp.IRefQuantity) * 100
            Catch ex As Exception
              tmp.ProgressPercentByQuantity = 0
            End Try
            Results.Add(tmp)
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
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
