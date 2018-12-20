Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.CT
  Public Class ctUpdates
    Public Shared Sub CT_POAccepted(ByVal pp As SIS.PAK.pakPO)
      Dim hndl As String = "CT_POISSUED"
      Dim trdt As String = Now.ToString("dd/MM/yyyy HH:mm:ss")

      Dim DSrn As Integer = 0
      Dim PODocs As List(Of SIS.DOCS.Document) = SIS.DOCS.Document.DocumentsSelectList(pp.PONumber)
      If PODocs.Count > 0 Then
        For Each PODoc As SIS.DOCS.Document In PODocs
          SIS.DMISG.dmisg140.UpdatePOAD(PODoc.t_docn, trdt)
        Next
      End If
      Dim tmp208 As SIS.TPISG.tpisg208 = SIS.TPISG.tpisg208.tpisg208GetByID(pp.SerialNo)
      If tmp208 IsNot Nothing Then
        With tmp208
          .t_accp = 1 'YES
          .t_accd = trdt
          .t_acby = HttpContext.Current.Session("LoginID")
          .t_Refcntd = 0
          .t_Refcntu = 0
        End With
        SIS.TPISG.tpisg208.UpdateData(tmp208)
      Else
        Throw New Exception("CT-PO Issue record Not found.")
      End If
    End Sub
    Public Shared Sub CT_POIssued(ByVal pp As SIS.PAK.pakPO)
      Dim hndl As String = "CT_POISSUED"
      Dim trdt As String = Now.ToString("dd/MM/yyyy HH:mm:ss")
      Dim PODocs As List(Of SIS.DOCS.Document) = SIS.DOCS.Document.DocumentsSelectList(pp.PONumber)
      If PODocs.Count > 0 Then
        For Each PODoc As SIS.DOCS.Document In PODocs
          SIS.DMISG.dmisg140.UpdatePOIS(PODoc.t_docn, trdt)
        Next
      End If
      Dim Found As Boolean = True
      Dim tmp208 As SIS.TPISG.tpisg208 = SIS.TPISG.tpisg208.tpisg208GetByID(pp.SerialNo)
      If tmp208 Is Nothing Then
        tmp208 = New SIS.TPISG.tpisg208
        Found = False
      End If
      With tmp208
        .t_idno = pp.SerialNo
        .t_pono = pp.PONumber
        .t_issu = 1 'YES
        .t_issd = trdt
        .t_isby = HttpContext.Current.Session("LoginID")
        .t_accp = 2 'NO
        .t_accd = "01/01/1970"
        .t_acby = ""
        .t_cprj = pp.ProjectID
        .t_Refcntd = 0
        .t_Refcntu = 0
      End With
      If Found Then
        SIS.TPISG.tpisg208.UpdateData(tmp208)
      Else
        SIS.TPISG.tpisg208.InsertData(tmp208)
      End If
    End Sub

    Public Shared Sub CT_QCOffered(ByVal pp As SIS.PAK.pakQCListH)
      Dim ppPO As SIS.PAK.pakPO = pp.FK_PAK_QCListH_SerialNo
      Dim hndl As String = "CT_INSPECTIONCALLRAISED"
      Dim trdt As String = Now.ToString("dd/MM/yyyy HH:mm:ss")
      Dim qcDs As List(Of SIS.PAK.pakQCListDIRef) = SIS.PAK.pakQCListDIRef.GetOfferedQCListDIref(pp)
      For Each qcD As SIS.PAK.pakQCListDIRef In qcDs
        Dim tmp207 As New SIS.TPISG.tpisg207
        With tmp207
          .t_bohd = hndl
          .t_date = trdt
          If pp.QCRequestNo = "" Then
            .t_inid = pp.QCLNo
          Else
            .t_inid = pp.QCRequestNo
          End If
          .t_iref = qcD.ItemReference
          .t_mode = 2  '=>Manual, 2=>Packing List
          .t_pono = pp.FK_PAK_QCListH_SerialNo.PONumber
          Select Case pp.FK_PAK_QCListH_SerialNo.POTypeID
            Case pakErpPOTypes.ISGECEngineered
              .t_prpo = qcD.ProgressPercent
            Case pakErpPOTypes.Boughtout, pakErpPOTypes.Package
              .t_prpo = qcD.ProgressPercentByQuantity
          End Select
          .t_powt = qcD.TotalWeight
          .t_Refcntd = 0
          .t_Refcntu = 0
          .t_cprj = pp.FK_PAK_QCListH_SerialNo.ProjectID
          .t_sitm = qcD.SubItem
        End With
        SIS.TPISG.tpisg207.InsertData(tmp207)
      Next
      'Get Inspection In Black Condition, i.e. Inspection Started Record
      'If found Get Date from that record and Insert With Mode 2, Other wise don't do any thing
      'Mode 2 record will be created by Inspection start
      Dim Started_207 As SIS.TPISG.tpisg207 = SIS.TPISG.tpisg207.GetInspectionInBlackRecord(pp.QCRequestNo)
      If Started_207 IsNot Nothing Then
        Dim StartDate As String = Started_207.t_date
        For Each qcD As SIS.PAK.pakQCListDIRef In qcDs
          Dim tmp207 As New SIS.TPISG.tpisg207
          With tmp207
            .t_bohd = "CT_INSPECTIONBLACKCONDITION"
            .t_date = StartDate
            .t_inid = pp.QCRequestNo
            .t_iref = qcD.ItemReference
            .t_mode = 2  '=>Manual, 2=>Packing List
            .t_pono = pp.FK_PAK_QCListH_SerialNo.PONumber
            Select Case pp.FK_PAK_QCListH_SerialNo.POTypeID
              Case pakErpPOTypes.ISGECEngineered
                .t_prpo = qcD.ProgressPercent
              Case pakErpPOTypes.Boughtout, pakErpPOTypes.Package
                .t_prpo = qcD.ProgressPercentByQuantity
            End Select
            .t_powt = qcD.TotalWeight
            .t_Refcntd = 0
            .t_Refcntu = 0
            .t_cprj = pp.FK_PAK_QCListH_SerialNo.ProjectID
            .t_sitm = qcD.SubItem
          End With
          SIS.TPISG.tpisg207.InsertData(tmp207)
        Next
      End If
    End Sub
    Public Shared Sub CT_QCOfferedReturn(ByVal pp As SIS.PAK.pakQCListH)
      Dim comp As String = "200"
      Dim ppPO As SIS.PAK.pakPO = pp.FK_PAK_QCListH_SerialNo
      Dim hndl As String = "CT_INSPECTIONCALLRAISED"
      Dim Sql As String = ""
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Con.Open()
        Sql = " delete ttpisg207" & comp
        Sql &= " where t_bohd='" & hndl & "'"
        Sql &= "   and t_cprj='" & pp.FK_PAK_QCListH_SerialNo.ProjectID & "'"
        Sql &= "   and t_pono='" & pp.FK_PAK_QCListH_SerialNo.PONumber & "'"
        Sql &= "   and t_mode= 2 "
        If pp.QCRequestNo = "" Then
          Sql &= " and t_indi=" & pp.QCLNo
        Else
          Sql &= " and t_indi=" & pp.QCRequestNo
        End If
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Cmd.ExecuteNonQuery()
        End Using
        If pp.QCRequestNo <> "" Then
          Sql = " delete ttpisg207" & comp
          Sql &= " where t_bohd='CT_INSPECTIONBLACKCONDITION'"
          Sql &= "   and t_cprj='" & pp.FK_PAK_QCListH_SerialNo.ProjectID & "'"
          Sql &= "   and t_pono='" & pp.FK_PAK_QCListH_SerialNo.PONumber & "'"
          Sql &= "   and t_mode= 2 "
          Sql &= "   and t_indi=" & pp.QCRequestNo
          Using Cmd As SqlCommand = Con.CreateCommand()
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = Sql
            Cmd.ExecuteNonQuery()
          End Using
        End If
      End Using
    End Sub

    Public Shared Sub CT_QCCleared(ByVal pp As SIS.PAK.pakQCListH)
      Dim ppPO As SIS.PAK.pakPO = pp.FK_PAK_QCListH_SerialNo
      Dim hndl As String = "CT_FINALMICN"
      Dim trdt As String = Now.ToString("dd/MM/yyyy HH:mm:ss")
      Dim qcDs As List(Of SIS.PAK.pakQCListDIRef) = SIS.PAK.pakQCListDIRef.GetClearedQCListDIref(pp)
      For Each qcD As SIS.PAK.pakQCListDIRef In qcDs
        Dim tmp207 As New SIS.TPISG.tpisg207
        With tmp207
          .t_bohd = hndl
          .t_date = trdt
          If pp.QCRequestNo = "" Then
            .t_inid = pp.QCLNo
          Else
            .t_inid = pp.QCRequestNo
          End If
          .t_iref = qcD.ItemReference
          .t_mode = 2  '=>Manual, 2=>Packing List
          .t_pono = pp.FK_PAK_QCListH_SerialNo.PONumber
          Select Case pp.FK_PAK_QCListH_SerialNo.POTypeID
            Case pakErpPOTypes.ISGECEngineered
              .t_prpo = qcD.ProgressPercent
            Case pakErpPOTypes.Boughtout, pakErpPOTypes.Package
              .t_prpo = qcD.ProgressPercentByQuantity
          End Select
          .t_powt = qcD.TotalWeight
          .t_Refcntd = 0
          .t_Refcntu = 0
          .t_cprj = pp.FK_PAK_QCListH_SerialNo.ProjectID
          .t_sitm = qcD.SubItem
        End With
        SIS.TPISG.tpisg207.InsertData(tmp207)
        'Update Same Progress
        tmp207.t_bohd = "CT_COMPLIANCEOFINSPECTION"
        SIS.TPISG.tpisg207.InsertData(tmp207)
      Next
    End Sub

    Public Shared Sub CT_DespatchedReturn(ByVal pp As SIS.PAK.pakPkgListH)
      Dim comp As String = "200"
      Dim ppPO As SIS.PAK.pakPO = pp.FK_PAK_PkgListH_SerialNo
      Dim hndl As String = "CT_MATERIALDISPATCHED"

      Dim Sql As String = ""
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Con.Open()
        Sql = " delete ttpisg207" & comp
        Sql &= " where t_bohd='" & hndl & "'"
        Sql &= "   and t_cprj='" & pp.FK_PAK_PkgListH_SerialNo.ProjectID & "'"
        Sql &= "   and t_pono='" & pp.FK_PAK_PkgListH_SerialNo.PONumber & "'"
        Sql &= "   and t_mode= 2 "
        Sql &= "   and t_indi=" & pp.PkgNo
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Cmd.ExecuteNonQuery()
        End Using
      End Using
    End Sub
    Public Shared Sub CT_Despatched(ByVal pp As SIS.PAK.pakPkgListH)
      Dim ppPO As SIS.PAK.pakPO = pp.FK_PAK_PkgListH_SerialNo
      Dim hndl As String = "CT_MATERIALDISPATCHED"
      Dim trdt As String = Now.ToString("dd/MM/yyyy HH:mm:ss")
      Dim pkgDs As List(Of SIS.PAK.pakPKGListDIRef) = SIS.PAK.pakPKGListDIRef.GetDespatchedPKGListDIref(pp)
      For Each pkgD As SIS.PAK.pakPKGListDIRef In pkgDs
        Dim tmp207 As New SIS.TPISG.tpisg207
        With tmp207
          .t_bohd = hndl
          .t_date = trdt
          .t_inid = pp.PkgNo
          .t_iref = pkgD.ItemReference
          .t_mode = 2  '=>Manual, 2=>Packing List
          .t_pono = pp.FK_PAK_PkgListH_SerialNo.PONumber
          Select Case pp.FK_PAK_PkgListH_SerialNo.POTypeID
            Case pakErpPOTypes.ISGECEngineered
              .t_prpo = pkgD.ProgressPercent
            Case pakErpPOTypes.Boughtout, pakErpPOTypes.Package
              .t_prpo = pkgD.ProgressPercentByQuantity
          End Select
          .t_powt = pkgD.TotalWeight
          .t_Refcntd = 0
          .t_Refcntu = 0
          .t_cprj = pp.FK_PAK_PkgListH_SerialNo.ProjectID
          .t_sitm = pkgD.SubItem
        End With
        SIS.TPISG.tpisg207.InsertData(tmp207)
        'Update Same Progress
        tmp207.t_bohd = "CT_RECEIPTPIIRN"
        SIS.TPISG.tpisg207.InsertData(tmp207)
      Next
    End Sub

    Public Shared Sub CT_ReceivedAtPort(ByVal pp As SIS.PAK.pakHPending)
      Dim ppPO As SIS.PAK.pakPO = pp.FK_PAK_PkgListH_SerialNo
      Dim hndl As String = "CT_RECEIPTATPORT"
      Dim trdt As String = Now.ToString("dd/MM/yyyy HH:mm:ss")
      Dim pkgDs As List(Of SIS.PAK.pakPKGListDIRef) = SIS.PAK.pakPKGListDIRef.GetDespatchedPKGListDIref(pp)
      For Each pkgD As SIS.PAK.pakPKGListDIRef In pkgDs
        Dim tmp207 As New SIS.TPISG.tpisg207
        With tmp207
          .t_bohd = hndl
          .t_date = trdt
          .t_inid = pp.PkgNo
          .t_iref = pkgD.ItemReference
          .t_mode = 2  '=>Manual, 2=>Packing List
          .t_pono = pp.FK_PAK_PkgListH_SerialNo.PONumber
          Select Case pp.FK_PAK_PkgListH_SerialNo.POTypeID
            Case pakErpPOTypes.ISGECEngineered
              .t_prpo = pkgD.ProgressPercent
            Case pakErpPOTypes.Boughtout, pakErpPOTypes.Package
              .t_prpo = pkgD.ProgressPercentByQuantity
          End Select
          .t_powt = pkgD.TotalWeight
          .t_Refcntd = 0
          .t_Refcntu = 0
          .t_cprj = pp.FK_PAK_PkgListH_SerialNo.ProjectID
          .t_sitm = pkgD.SubItem
        End With
        SIS.TPISG.tpisg207.InsertData(tmp207)
      Next
    End Sub
    Public Shared Sub CT_Shipped(ByVal pp As SIS.PAK.pakPkgListH)
      Dim errMsg As String = ""
      Dim hndl As String = "CT_SHIPMENT"
      Dim trdt As String = Now.ToString("dd/MM/yyyy HH:mm:ss")
      Dim pkgDs As List(Of SIS.PAK.pakPKGListDIRef) = SIS.PAK.pakPKGListDIRef.GetDespatchedPKGPortDIref(pp.PkgNo)
      For Each pkgD As SIS.PAK.pakPKGListDIRef In pkgDs
        Dim pakPO As SIS.PAK.pakPO = SIS.PAK.pakPO.pakPOGetByID(pkgD.SerialNo)
        Dim tmp207 As New SIS.TPISG.tpisg207
        With tmp207
          .t_bohd = hndl
          .t_date = trdt
          .t_inid = pkgD.PKGNo
          .t_iref = pkgD.ItemReference
          .t_mode = 2  '=>Manual, 2=>Packing List
          .t_pono = pakPO.PONumber
          Select Case pakPO.POTypeID
            Case pakErpPOTypes.ISGECEngineered
              .t_prpo = pkgD.ProgressPercent
            Case pakErpPOTypes.Boughtout, pakErpPOTypes.Package
              .t_prpo = pkgD.ProgressPercentByQuantity
          End Select
          .t_powt = pkgD.TotalWeight
          .t_Refcntd = 0
          .t_Refcntu = 0
          .t_cprj = pakPO.ProjectID
          .t_sitm = pkgD.SubItem
        End With
        Try
          SIS.TPISG.tpisg207.InsertData(tmp207)
        Catch ex As Exception
          Throw New Exception(ex.Message)
        End Try
      Next
    End Sub

    Public Shared Sub CT_ShippedReturn(ByVal pp As SIS.PAK.pakPkgListH)
      Dim comp As String = "200"
      Dim hndl As String = "CT_SHIPMENT"
      Dim pkgDs As List(Of SIS.PAK.pakPKGListDIRef) = SIS.PAK.pakPKGListDIRef.GetDespatchedPKGPortDIref(pp.PkgNo)

      Dim Sql As String = ""
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Con.Open()

        For Each pkgD As SIS.PAK.pakPKGListDIRef In pkgDs
          Dim pakPO As SIS.PAK.pakPO = SIS.PAK.pakPO.pakPOGetByID(pkgD.SerialNo)

          Sql = " delete ttpisg207" & comp
          Sql &= " where t_bohd='" & hndl & "'"
          Sql &= "   and t_cprj='" & pakPO.ProjectID & "'"
          Sql &= "   and t_pono='" & pakPO.PONumber & "'"
          Sql &= "   and t_mode= 2 "
          Sql &= "   and t_indi=" & pkgD.PKGNo
          Using Cmd As SqlCommand = Con.CreateCommand()
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = Sql
            Cmd.ExecuteNonQuery()
          End Using
        Next
      End Using
    End Sub

    Public Shared Sub CT_ReceivedAtSite(ByVal pp As SIS.PAK.pakSitePkgH)
      Dim errMsg As String = ""
      Dim hndl As String = "CT_RECEIPTATSITE"
      Dim trdt As String = Now.ToString("dd/MM/yyyy HH:mm:ss")
      Dim pkgDs As List(Of SIS.PAK.pakPKGListDIRef) = SIS.PAK.pakPKGListDIRef.GetReceivedPKGSiteDIref(pp.RecNo)
      For Each pkgD As SIS.PAK.pakPKGListDIRef In pkgDs
        Dim pakPO As SIS.PAK.pakPO = SIS.PAK.pakPO.pakPOGetByID(pkgD.SerialNo)
        Dim tmp207 As New SIS.TPISG.tpisg207
        With tmp207
          .t_bohd = hndl
          .t_date = trdt
          .t_inid = pkgD.PKGNo
          .t_iref = pkgD.ItemReference
          .t_mode = 2  '=>Manual, 2=>Packing List
          .t_pono = pakPO.PONumber
          Select Case pakPO.POTypeID
            Case pakErpPOTypes.ISGECEngineered
              .t_prpo = pkgD.ProgressPercent
            Case pakErpPOTypes.Boughtout, pakErpPOTypes.Package
              .t_prpo = pkgD.ProgressPercentByQuantity
          End Select
          .t_powt = pkgD.TotalWeight
          .t_Refcntd = 0
          .t_Refcntu = 0
          .t_cprj = pakPO.ProjectID
          .t_sitm = pkgD.SubItem
        End With
        Try
          SIS.TPISG.tpisg207.InsertData(tmp207)
        Catch ex As Exception
          Throw New Exception(ex.Message)
        End Try
      Next
    End Sub

  End Class
End Namespace

'Dim POs As List(Of SIS.PAK.pakPkgListD) = SIS.PAK.pakPkgListD.PortDespatchedPO(pp.PkgNo)
'For Each po As SIS.PAK.pakPkgListD In POs
'  Dim ppPO As SIS.PAK.pakPO = po.FK_PAK_PkgListD_SerialNo
'  '1. Insert In tpisg229
'  Dim ct229 As New SIS.TPISG.tpisg229
'  With ct229
'    .t_trdt = trdt
'    .t_bohd = hndl
'    .t_indv = ppPO.SerialNo & "_" & ppPO.PONumber
'    .t_srno = 1
'    .t_proj = ppPO.ProjectID
'    .t_elem = ""
'    .t_user = pp.CreatedBy
'    .t_stat = ""
'    .t_Refcntd = 0
'    .t_Refcntu = 0
'  End With
'  ct229 = SIS.TPISG.tpisg229.InsertData(ct229)
'  '2. Insert In tpisg230
'  Dim DSrn As Integer = 0
'  Dim PODocs As List(Of SIS.DOCS.Document) = SIS.DOCS.Document.DocumentsSelectList(ppPO.PONumber)
'  If PODocs.Count > 0 Then
'    For Each PODoc As SIS.DOCS.Document In PODocs
'      DSrn += 1
'      Dim ct230 As New SIS.TPISG.tpisg230
'      With ct230
'        .t_trdt = ct229.t_trdt
'        .t_bohd = hndl
'        .t_indv = ppPO.SerialNo & "_" & ppPO.PONumber
'        .t_srno = 1
'        .t_dsno = DSrn
'        .t_dwno = PODoc.t_docn
'        .t_elem = ""
'        .t_proj = ppPO.ProjectID
'        .t_wght = 0
'        .t_pitc = 0
'        .t_stat = ""
'        .t_atcd = ""
'        .t_scup = 0
'        .t_acdt = "01/01/1970"
'        .t_acfh = "01/01/1970"
'        .t_pper = 0
'        .t_lupd = "01/01/1970"
'        .t_Refcntd = 0
'        .t_Refcntu = 0
'        .t_numo = 0
'        .t_numq = 0
'        .t_numt = 0
'        .t_numv = 0
'        .t_nutc = 0
'        .t_cuni = ""
'        .t_iref = ""
'        .t_quan = 0
'        .t_iuom = ""
'      End With
'      ct230 = SIS.TPISG.tpisg230.InsertData(ct230)
'    Next
'  End If
'Next
