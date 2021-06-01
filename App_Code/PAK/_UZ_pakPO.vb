Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel

Namespace SIS.PAK
  Public Class EffectedPOByDrawings

    Public Property PONumber As String = ""
    Public Property POLine As Integer = 0


    Public Shared Function GetPOList(DocumentID As String, Comp As String) As List(Of EffectedPOByDrawings)
      Dim Sql As String = ""
      Sql &= " SELECT distinct t_orno as PONumber,t_pono As POLine FROM ttdisg002" & Comp
      Sql &= " WHERE t_docn='" & DocumentID & "' "
      Dim mRet As New List(Of EffectedPOByDrawings)
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            mRet.Add(New EffectedPOByDrawings(Reader))
          End While
          Reader.Close()
        End Using
      End Using
      Return mRet
    End Function
    Sub New(rd As SqlDataReader)
      SIS.SYS.SQLDatabase.DBCommon.NewObj(Me, rd)
    End Sub
  End Class


  Public Class PODocs
    Public Property DocumentID As String = ""
    Public Property DocumentRevision As String = ""
    Public Shared Function GetDocs(SerialNo As String) As List(Of SIS.PAK.PODocs)
      Dim mRet As New List(Of SIS.PAK.PODocs)
      Dim Sql As String = ""
      Sql &= "  select "
      Sql &= "  distinct doc.DocumentID, "
      Sql &= "  doc.DocumentRevision "
      Sql &= "  from pak_pobitems as itm "
      Sql &= "  inner join pak_documents as doc on itm.documentno=doc.documentno "
      Sql &= "  where itm.serialno= " & SerialNo
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Rd As SqlDataReader = Cmd.ExecuteReader
          While Rd.Read
            mRet.Add(New SIS.PAK.PODocs(Rd))
          End While
        End Using
      End Using
      Return mRet
    End Function
    Public Shared Function getLatestERPDoc(DocumentID As String, Comp As String) As SIS.PAK.PODocs
      Dim mRet As SIS.PAK.PODocs = Nothing
      Dim Sql As String = ""
      Sql &= " select "
      Sql &= "  dm.t_docn As DocumentID, "
      Sql &= "  dm.t_revn as DocumentRevision "
      Sql &= "  from tdmisg001" & Comp & " as dm "
      Sql &= "  where t_docn='" & DocumentID & "' "
      Sql &= "  and t_revn = (select max(xx.t_revn) from tdmisg001" & Comp & " as xx where xx.t_docn=dm.t_docn) "
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If (Reader.Read()) Then
            mRet = New SIS.PAK.PODocs(Reader)
          End If
          Reader.Close()
        End Using
      End Using
      Return mRet
    End Function
    Sub New(rD As SqlDataReader)
      SIS.SYS.SQLDatabase.DBCommon.NewObj(Me, rD)
    End Sub
  End Class
  Partial Public Class pakPO
    Public ReadOnly Property UpdFromDWGVisible() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          Select Case POStatusID
            Case pakPOStates.UnderDespatch
              If HttpContext.Current.Session("LoginID") = "0340" Then
                mRet = True
              End If
          End Select
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property

    Public ReadOnly Property ToAckCount As Integer
      Get
        Dim mRet As Integer = 0
        Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
          Using Cmd As SqlCommand = Con.CreateCommand()
            Cmd.CommandType = CommandType.Text
            If IsSupplier Then
              Cmd.CommandText = "select isnull(count(*),0) as cnt from PAK_POBItems where Changed=1 and serialno=" & SerialNo
            Else
              Cmd.CommandText = "select isnull(count(*),0) as cnt from PAK_POBItems where ChangedBySupplier=1 and serialno=" & SerialNo
            End If
            Con.Open()
            mRet = Cmd.ExecuteScalar
          End Using
        End Using
        Return mRet
      End Get
    End Property
    Public ReadOnly Property GetAckCount As Integer
      Get
        Dim mRet As Integer = 0
        Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
          Using Cmd As SqlCommand = Con.CreateCommand()
            Cmd.CommandType = CommandType.Text
            If IsSupplier Then
              Cmd.CommandText = "select isnull(count(*),0) as cnt from PAK_POBItems where ChangedBySupplier=1 and serialno=" & SerialNo
            Else
              Cmd.CommandText = "select isnull(count(*),0) as cnt from PAK_POBItems where Changed=1 and serialno=" & SerialNo
            End If
            Con.Open()
            mRet = Cmd.ExecuteScalar
          End Using
        End Using
        Return mRet
      End Get
    End Property

    Public ReadOnly Property QCOCount As Integer
      Get
        Dim mRet As Integer = 0
        Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
          Using Cmd As SqlCommand = Con.CreateCommand()
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = "select isnull(count(*),0) as cnt from PAK_QCListH where serialno=" & SerialNo
            Con.Open()
            mRet = Cmd.ExecuteScalar
          End Using
        End Using
        Return mRet
      End Get
    End Property
    Public ReadOnly Property PKGCount As Integer
      Get
        Dim mRet As Integer = 0
        Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
          Using Cmd As SqlCommand = Con.CreateCommand()
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = "select isnull(count(*),0) as cnt from PAK_PKGListH where serialno=" & SerialNo
            Con.Open()
            mRet = Cmd.ExecuteScalar
          End Using
        End Using
        Return mRet
      End Get
    End Property



    Public ReadOnly Property GetPrintLink() As String
      Get
        Dim UrlAuthority As String = HttpContext.Current.Request.Url.Authority
        If UrlAuthority.ToLower <> "cloud.isgec.co.in" Then
          UrlAuthority = "192.9.200.146"
        End If
        Dim mRet As String = HttpContext.Current.Request.Url.Scheme & Uri.SchemeDelimiter & UrlAuthority
        mRet &= "/PurchaseOrderReceipt/PurchaseOrder?PO=" & PONumber & "&Revision=" & PORevision
        mRet = "javascript:window.open('" & mRet & "', 'win" & PONumber & "', 'left=20,top=20,width=1100,height=600,toolbar=1,resizable=1,scrollbars=1,toolbar=no,status=no,menu=no, directories=no,titlebar=no,location=no,addressbar=no'); return false;"
        Return mRet
      End Get
    End Property

    Public ReadOnly Property GetNotesLink() As String
      Get
        Dim mRet As String = HttpContext.Current.Request.Url.Scheme & Uri.SchemeDelimiter & HttpContext.Current.Request.Url.Authority
        If HttpContext.Current.Request.Url.Authority.ToLower = "localhost" Then
          mRet = "http://192.9.200.146"
        End If
        mRet &= "/Attachment/Notes.aspx?handle=J_POISSUE"
        Dim Index As String = SerialNo
        Dim User As String = HttpContext.Current.Session("LoginID")
        Dim canEdit As String = "y"
        mRet &= "&Index=" & Index & "&user=" & User & "&ed=" & canEdit
        mRet = "javascript:window.open('" & mRet & "', 'win" & SerialNo & "', 'left=20,top=20,width=1000,height=700,toolbar=1,resizable=1,scrollbars=1'); return false;"
        Return mRet
      End Get
    End Property
    Public Function GetColor() As System.Drawing.Color
      Dim mRet As System.Drawing.Color = Drawing.Color.Black
      Select Case POStatusID
        Case pakPOStates.UnderSupplierVerification
          mRet = Drawing.Color.Green
        Case pakPOStates.UnderISGECApproval
          mRet = Drawing.Color.Crimson
        Case pakPOStates.IssuedtoSupplier
          mRet = Drawing.Color.LightSeaGreen
        Case pakPOStates.UnderDespatch
          mRet = Drawing.Color.MediumVioletRed
        Case pakPOStates.Closed
          mRet = Drawing.Color.Olive
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
    Private _IsSupplier As Boolean = False
    Public Property IsSupplier As Boolean
      Get
        Return _IsSupplier
      End Get
      Set(value As Boolean)
        _IsSupplier = value
      End Set
    End Property
    Public Function GetEditable() As Boolean
      Dim mRet As Boolean = False
      Select Case POStatusID
        Case pakPOStates.Free, pakPOStates.ImportedFromERP
          mRet = True
      End Select
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
        Dim mRet As Boolean = False
        Return mRet
      End Get
    End Property
    Public ReadOnly Property DeleteWFVisible() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          Select Case POStatusID
            Case pakPOStates.Free, pakPOStates.ImportedFromERP
              mRet = True
            Case pakPOStates.UnderISGECApproval, pakPOStates.UnderSupplierVerification, pakPOStates.UnderDespatch
              If HttpContext.Current.Session("LoginID") = "0340" Then
                mRet = True
              End If
          End Select
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property UpdateWFVisible() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          Select Case POTypeID
            Case pakErpPOTypes.Boughtout, pakErpPOTypes.ISGECEngineered
              mRet = True
            Case pakErpPOTypes.Package
              'Update PO is also Enabled for Package, If Not using Package Master, BOM can be added from PO
              If POStatusID = pakPOStates.Free Or POStatusID = pakPOStates.UnderISGECApproval Then
                mRet = True
              End If
          End Select
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property InitiateWFVisible() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          Select Case POStatusID
            Case pakPOStates.Free, pakPOStates.ImportedFromERP
              mRet = True
          End Select
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
        Dim mRet As Boolean = False
        Try
          Select Case POStatusID
            Case pakPOStates.UnderISGECApproval, pakPOStates.ImportedFromERP
              mRet = True
          End Select
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
    Public ReadOnly Property CompleteWFVisible() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetVisible()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property CompleteWFEnable() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetEnable()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property SubmitWFVisible() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          Select Case POStatusID
            Case pakPOStates.UnderISGECApproval
              If Not IsSupplier Then
                mRet = True
              End If
            Case pakPOStates.UnderSupplierVerification
              If IsSupplier Then
                mRet = True
              End If
          End Select
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property FreezeWFVisible() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          Select Case POStatusID
            Case pakPOStates.UnderISGECApproval
              If Not IsSupplier Then
                mRet = True
              End If
          End Select
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property UnFreezeWFVisible() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          Select Case POStatusID
            Case pakPOStates.UnderDespatch
              If Not IsSupplier Then
                mRet = True
              End If
          End Select
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public Shared Function CreateHistory(SerialNo As Integer) As Boolean
      Dim mRet As Boolean = False
      Try
        Dim Sql As String = ""
        Sql &= " declare @hid int, @sl int "
        Sql &= " set @sl = " & SerialNo
        Sql &= " select @hid=isnull(max(histid),0)+1 from pak_h_po "
        Sql &= " insert into PAK_H_PO select @hid as HistID, * from pak_po where serialno=@sl "
        Sql &= " insert into PAK_H_POBOM select @hid as HistID, * from PAK_POBOM where serialno=@sl "
        Sql &= " insert into PAK_H_POBItems select @hid as HistID, * from PAK_POBItems where serialno=@sl "
        Sql &= " insert into PAK_H_POBIDocuments select @hid as HistID, * from PAK_POBIDocuments where serialno=@sl "
        Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
          Using Cmd As SqlCommand = Con.CreateCommand()
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = Sql
            Con.Open()
            mRet = Cmd.ExecuteNonQuery
          End Using
        End Using
        mRet = True
      Catch ex As Exception
      End Try
      Return mRet
    End Function

    Public Shared Function FreezeBOMWF(ByVal SerialNo As Int32) As SIS.PAK.pakPO
      '===========BOM Freezing By Isgec==========
      Dim Results As SIS.PAK.pakPO = pakPOGetByID(SerialNo)
      Dim tmp As List(Of SIS.PAK.pakPOBOM) = SIS.PAK.pakPOBOM.UZ_pakPOBOMSelectList(0, 999, "", False, "", SerialNo)
      For Each ttmp As SIS.PAK.pakPOBOM In tmp
        Dim cnt As Integer = 0
        cnt = SIS.PAK.pakPOBItems.GetChangedBySupplierCount(ttmp.SerialNo, ttmp.BOMNo)
        If cnt > 0 Then
          Throw New Exception("There are Item(s) NOT Acknowledged, Pl. ACK them before Freezing.")
        End If
        cnt = SIS.PAK.pakPOBItems.GetChangedCount(ttmp.SerialNo, ttmp.BOMNo)
        If cnt > 0 Then
          Throw New Exception("There are changes done by you and NOT Acknowledged by Supplier, Pl. submit and get ACK them before Freezing.")
        End If
      Next
      For Each bom As SIS.PAK.pakPOBOM In tmp
        SIS.PAK.pakPOBItems.FreezeWF(bom.SerialNo, bom.BOMNo, bom.ItemNo)
      Next
      With Results
        .POStatusID = pakPOStates.UnderDespatch
        .ClosedBy = HttpContext.Current.Session("LoginID")
        .ClosedOn = Now
      End With
      Results = SIS.PAK.pakSPO.UpdateData(Results)
      SIS.PAK.pakPO.CreateHistory(SerialNo)
      '===========================================
      'Send BOM Freezed E-Mail=>Supplier may Start work TC, QC, Despatch
      'SIS.PAK.Alerts.Alert(SerialNo, pakAlertEvents.POApproval)
      '===========================================
      Return Results
    End Function
    Public Shared Function UnFreezeBOMWF(ByVal SerialNo As Int32) As SIS.PAK.pakPO
      '===========BOM Freezing By Isgec==========
      Dim Results As SIS.PAK.pakPO = pakPOGetByID(SerialNo)
      Dim tmp As List(Of SIS.PAK.pakPOBOM) = SIS.PAK.pakPOBOM.UZ_pakPOBOMSelectList(0, 999, "", False, "", SerialNo)
      For Each bom As SIS.PAK.pakPOBOM In tmp
        SIS.PAK.pakPOBItems.UnFreezeWF(bom.SerialNo, bom.BOMNo, bom.ItemNo)
      Next
      With Results
        .POStatusID = pakPOStates.UnderISGECApproval
        .ClosedBy = HttpContext.Current.Session("LoginID")
        .ClosedOn = Now
      End With
      Results = SIS.PAK.pakSPO.UpdateData(Results)
      SIS.PAK.pakPO.CreateHistory(SerialNo)
      Return Results
    End Function

    Public Shared Function SubmitWF(ByVal SerialNo As Int32) As SIS.PAK.pakPO
      '========BOM Prepareing Submit By Isgec and Supplier to Each Other=======
      Dim Results As SIS.PAK.pakPO = pakPOGetByID(SerialNo)
      Dim tmp As List(Of SIS.PAK.pakPOBOM) = SIS.PAK.pakPOBOM.UZ_pakPOBOMSelectList(0, 999, "", False, "", SerialNo)
      For Each ttmp As SIS.PAK.pakPOBOM In tmp
        Dim cnt As Integer = 0
        If Results.IsSupplier Then
          cnt = SIS.PAK.pakPOBItems.GetChangedCount(ttmp.SerialNo, ttmp.BOMNo)
        Else
          cnt = SIS.PAK.pakPOBItems.GetChangedBySupplierCount(ttmp.SerialNo, ttmp.BOMNo)
        End If
        If cnt > 0 Then
          Throw New Exception("There are Item(s) NOT Acknowledged by you, Pl. ACK them before submitting.")
        End If
      Next
      With Results
        'Closed by is used to store, BOM prepration action last submitted by login ID
        .ClosedBy = HttpContext.Current.Session("LoginID")
        .ClosedOn = Now
        If Results.IsSupplier Then
          .POStatusID = pakPOStates.UnderISGECApproval
        Else
          .POStatusID = pakPOStates.UnderSupplierVerification
        End If
      End With
      Results = SIS.PAK.pakSPO.UpdateData(Results)
      '===========================================
      SIS.PAK.Alerts.BomSubmitted(SerialNo)
      '===========================================
      Return Results
    End Function
    Public Shared Function DeleteWF(ByVal SerialNo As Int32) As SIS.PAK.pakPO
      Dim Results As SIS.PAK.pakPO = pakPOGetByID(SerialNo)
      If Results.QCOCount > 0 Or Results.PKGCount > 0 Then
        Throw New Exception("Quality Inspection or Packing List found, can not delete.")
      End If
      Dim BOMs As List(Of SIS.PAK.pakPOBOM) = SIS.PAK.pakPOBOM.UZ_pakPOBOMSelectList(0, 999, "", False, "", SerialNo)
      For Each bom As SIS.PAK.pakPOBOM In BOMs
        Dim tmp As SIS.PAK.pakPOBItems = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(bom.SerialNo, bom.BOMNo, bom.ItemNo)
        If tmp IsNot Nothing Then
          If Not SIS.PAK.pakPOBItems.CanBeDeleted(tmp) Then
            Throw New Exception("There are QC or Despatch in Item [tree from here], CAN Not perform Delete. You may try to delete individual Item.")
          End If
        End If
      Next
      For Each bom As SIS.PAK.pakPOBOM In BOMs
        SIS.PAK.pakPOBItems.AcceptDeleteWF(bom.SerialNo, bom.BOMNo, bom.ItemNo)
        SIS.PAK.pakPOBOM.pakPOBOMDelete(bom)
      Next
      SIS.PAK.pakPO.pakPODelete(Results)
      Return Results
    End Function
    Public Shared Function UpdateLatestReleasedDWG(SerialNo As String) As Boolean
      Dim Comp As String = HttpContext.Current.Session("FinanceCompany")
      Dim mRet As Boolean = True
      Dim tmpPODocs As List(Of SIS.PAK.PODocs) = SIS.PAK.PODocs.GetDocs(SerialNo)
      For Each doc As SIS.PAK.PODocs In tmpPODocs
        Dim eDoc As SIS.PAK.PODocs = SIS.PAK.PODocs.getLatestERPDoc(doc.DocumentID, Comp)
        If eDoc IsNot Nothing Then
          If eDoc.DocumentRevision > doc.DocumentRevision Then
            Dim xPOs As List(Of SIS.PAK.EffectedPOByDrawings) = SIS.PAK.EffectedPOByDrawings.GetPOList(doc.DocumentID, Comp)
            Dim MoreThanOnePO As Boolean = IIf(xPOs.Count > 1, True, False)
            Dim PO As SIS.PAK.pakPO = SIS.PAK.pakPO.pakPOGetByID(SerialNo)
            Dim ePO As SIS.PAK.EffectedPOByDrawings = xPOs.Find(Function(n) n.PONumber = PO.PONumber)
            'Update BOM
            UpdateIssuedPOFromDrawing(SerialNo, ePO, PO, doc.DocumentID, doc.DocumentRevision, eDoc.DocumentRevision, Comp, MoreThanOnePO)
          End If
        End If
      Next
      Return mRet
    End Function
    Private Shared Function GetNextItemNo(ByVal SerialNo As Integer, ByVal BomNo As Integer) As Integer
      Dim tmp As Integer = 0
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString)
        Using Cmd As SqlCommand = Con.CreateCommand()
          Dim mSql As String = "SELECT MAX(ISNULL(ItemNo,0)) FROM [PAK_POBitems] WHERE SerialNo = " & SerialNo & " and BOMNo=" & BomNo
          Cmd.CommandType = System.Data.CommandType.Text
          Cmd.CommandText = mSql
          Con.Open()
          Try
            tmp = Cmd.ExecuteScalar()
            tmp += 1
          Catch ex As Exception
            tmp = 1
          End Try
        End Using
      End Using
      Return tmp
    End Function

    Public Shared Sub UpdateIssuedPOFromDrawing(SerialNo As String, ePO As SIS.PAK.EffectedPOByDrawings, PO As SIS.PAK.pakPO, DocumentID As String, RevisionNo As String, eRevisionNo As String, Comp As String, MoreThanOnePO As Boolean)
      If PO IsNot Nothing Then
        Try
          Dim dmItems As List(Of dmisg002) = dmisg002.GetItems(DocumentID, eRevisionNo, Comp)
          Dim bItems As List(Of SIS.PAK.pakPOBItems) = SIS.PAK.pakPOBItems.GetByDocRev(PO.SerialNo, ePO.POLine, DocumentID, eRevisionNo)
          Dim Found As Boolean = False
          For Each dmItem As dmisg002 In dmItems
            Found = False
            Dim bItem As SIS.PAK.pakPOBItems = Nothing
            For Each bItem In bItems
              If bItem.ItemCode.Trim = dmItem.t_item.Trim Then
                Found = True
                Exit For
              End If
            Next
            If Not Found Then
              If MoreThanOnePO Then
                Continue For 'Do Not Insert New Item
              End If
              bItem = bItems(0)
              Dim mNextItemNo As Integer = GetNextItemNo(bItem.SerialNo, bItem.BOMNo)
              With bItem
                .ItemNo = mNextItemNo
                .ItemCode = dmItem.t_item.Trim
                .ItemDescription = dmItem.t_dsca
                .QuantityToPack = 0
                .TotalWeightToPack = 0
                .QuantityToDespatch = 0
                .TotalWeightToDespatch = 0
                .QuantityDespatched = 0
                .TotalWeightDespatched = 0
                .QuantityReceived = 0
                .TotalWeightReceived = 0
              End With
            End If
            With bItem
              Try
                .ItemDescription = dmItem.t_dsca
                If dmItem.t_cuni <> "" Then
                  .UOMQuantity = SIS.PAK.pakUnits.pakUnitsGetByDescription(dmItem.t_cuni).UnitID
                End If
                If dmItem.t_qnty < 0.0001 Then dmItem.t_qnty = 0
                If PO.QCRequired Then
                  If dmItem.t_qnty < bItem.QualityClearedQty Then
                    dmItem.t_qnty = bItem.QualityClearedQty
                  End If
                Else
                  If dmItem.t_qnty < bItem.QuantityDespatched Then
                    dmItem.t_qnty = bItem.QuantityDespatched
                  End If
                End If
                .Quantity = dmItem.t_qnty
                .UOMWeight = SIS.PAK.pakUnits.pakUnitsGetByDescription("kg").UnitID
                If dmItem.t_wght >= 0.0001 AndAlso dmItem.t_qnty >= 0.0001 Then
                  .WeightPerUnit = dmItem.t_wght / dmItem.t_qnty
                Else
                  .WeightPerUnit = 0
                End If
              Catch ex As Exception
                Throw New Exception("Err Updating: " & ex.Message)
              End Try
            End With
            If Found Then
              bItem = SIS.PAK.pakPOBItems.UpdateData(bItem)
            Else
              bItem = SIS.PAK.pakPOBItems.InsertData(bItem)
            End If
          Next
          'Update Document Revision
          SIS.PAK.pakPOBItems.CreateNewRevDocumentAndLink(PO.SerialNo, ePO.POLine, DocumentID, eRevisionNo)
          'Reverse check to Delete Items Removed from Drawing
          'Do Not Reverse Check to delete if more than One PO
          If Not MoreThanOnePO Then
            For Each bItem As SIS.PAK.pakPOBItems In bItems
              Found = False
              For Each dmItem As dmisg002 In dmItems
                If bItem.ItemCode.Trim = dmItem.t_item.Trim Then
                  Found = True
                  Exit For
                End If
              Next
              If Not Found Then
                If SIS.PAK.pakPOBItems.CanBeDeleted(bItem) Then
                  SIS.PAK.pakPOBItems.pakPOBItemsDeleteRecursive(bItem)
                Else
                  bItem.DeletedInERP = True
                  SIS.PAK.pakPOBItems.UpdateData(bItem)
                End If
              End If
            Next
          End If
        Catch ex As Exception
        End Try
      End If
    End Sub

    Public Shared Function InitiateWF(ByVal SerialNo As Int32) As SIS.PAK.pakPO
      '====================PO Issue By ISGEC==================
      Dim Results As SIS.PAK.pakPO = pakPOGetByID(SerialNo)
      If Results.FK_PAK_SupplierID.EMailID = "" Then
        Throw New Exception("Supplier E-Mail IDs is blank.")
      End If
      Dim tmp As List(Of SIS.PAK.pakPOBOM) = SIS.PAK.pakPOBOM.UZ_pakPOBOMSelectList(0, 999, "", False, "", SerialNo)
      If tmp.Count <= 0 Then
        Throw New Exception("NO PO Line found or Package Item NOT Linked to this PO, can NOT be issued.")
      End If
      '=====Update Released Drawings======
      If Convert.ToBoolean(ConfigurationManager.AppSettings("UpdateDWGOnPOIssue")) Then
        Select Case Results.POTypeID
          Case pakErpPOTypes.ISGECEngineered, pakErpPOTypes.Boughtout
            Try
              UpdateLatestReleasedDWG(SerialNo)
            Catch ex As Exception
              Throw New Exception("Error: Updating Latest Revision of PO Document.")
            End Try
        End Select
      End If
      '===================================
      If Results.PortRequired AndAlso Results.PortID = String.Empty Then Results.PortID = "1"
      With Results
        If Results.POTypeID = pakErpPOTypes.Package Then
          .POStatusID = pakPOStates.UnderSupplierVerification
        Else
          'Issued to Supplier status, when supplier acknowledge then Under Despatch status
          'But Hold this logic for time being
          '.POStatusID = pakPOStates.IssuedtoSupplier
          .POStatusID = pakPOStates.UnderDespatch
        End If
        .IssuedBy = HttpContext.Current.Session("LoginID")
        .IssuedOn = Now
      End With
      Results = SIS.PAK.pakPO.UpdateData(Results)
      'Create WebUser for supplier
      Dim owUsr As SIS.QCM.qcmUsers = SIS.PAK.pakSTCPO.CreateSupplierLogin(Results.SupplierID)
      '===========================================
      If Not Convert.ToBoolean(ConfigurationManager.AppSettings("Testing")) Then
        If Results.POTypeID = pakErpPOTypes.Package Then
          SIS.PAK.Alerts.Alert(SerialNo, pakAlertEvents.POVerification)
        Else
          SIS.PAK.Alerts.Alert(SerialNo, pakAlertEvents.POIssued)
        End If
      End If
      Try
        SIS.CT.ctUpdates.CT_POIssued(Results)
      Catch ex As Exception
        Throw New Exception(ex.Message)
      End Try
      Return Results
    End Function

    Public Shared Function UZ_pakPOSelectList(ByVal StartRowIndex As Integer, ByVal MaximumRows As Integer, ByVal OrderBy As String, ByVal SearchState As Boolean, ByVal SearchText As String, ByVal POTypeID As Int32, ByVal SupplierID As String, ByVal ProjectID As String, ByVal BuyerID As String, ByVal POStatusID As Int32, ByVal IssuedBy As String) As List(Of SIS.PAK.pakPO)
      Dim Results As List(Of SIS.PAK.pakPO) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          If SearchState Then
            Cmd.CommandText = "sppak_LG_POSelectListSearch"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@KeyWord", SqlDbType.NVarChar, 250, SearchText)
          Else
            Cmd.CommandText = "sppak_LG_POSelectListFilteres"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_POTypeID", SqlDbType.Int, 10, IIf(POTypeID = Nothing, 0, POTypeID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_SupplierID", SqlDbType.NVarChar, 9, IIf(SupplierID Is Nothing, String.Empty, SupplierID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_ProjectID", SqlDbType.NVarChar, 6, IIf(ProjectID Is Nothing, String.Empty, ProjectID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_BuyerID", SqlDbType.NVarChar, 8, IIf(BuyerID Is Nothing, String.Empty, BuyerID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_POStatusID", SqlDbType.Int, 10, IIf(POStatusID = Nothing, 0, POStatusID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_IssuedBy", SqlDbType.NVarChar, 8, IIf(IssuedBy Is Nothing, String.Empty, IssuedBy))
          End If
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@StartRowIndex", SqlDbType.Int, -1, StartRowIndex)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaximumRows", SqlDbType.Int, -1, MaximumRows)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NVarChar, 9, HttpContext.Current.Session("LoginID"))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@OrderBy", SqlDbType.NVarChar, 50, OrderBy)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@DivisionID", SqlDbType.Int, 10, Global.System.Web.HttpContext.Current.Session("DivisionID"))
          Cmd.Parameters.Add("@RecordCount", SqlDbType.Int)
          Cmd.Parameters("@RecordCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Results = New List(Of SIS.PAK.pakPO)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.PAK.pakPO(Reader))
          End While
          Reader.Close()
          _RecordCount = Cmd.Parameters("@RecordCount").Value
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function UZ_PackagePOSelectList(ByVal StartRowIndex As Integer, ByVal MaximumRows As Integer, ByVal OrderBy As String, ByVal SearchState As Boolean, ByVal SearchText As String, ByVal POTypeID As Int32, ByVal SupplierID As String, ByVal ProjectID As String, ByVal BuyerID As String, ByVal POStatusID As Int32, ByVal IssuedBy As String) As List(Of SIS.PAK.pakPO)
      Dim Results As List(Of SIS.PAK.pakPO) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          If SearchState Then
            Cmd.CommandText = "sppak_LG_PackagePOSelectListSearch"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@KeyWord", SqlDbType.NVarChar, 250, SearchText)
          Else
            Cmd.CommandText = "sppak_LG_PackagePOSelectListFilteres"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_POTypeID", SqlDbType.Int, 10, IIf(POTypeID = Nothing, 0, POTypeID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_SupplierID", SqlDbType.NVarChar, 9, IIf(SupplierID Is Nothing, String.Empty, SupplierID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_ProjectID", SqlDbType.NVarChar, 6, IIf(ProjectID Is Nothing, String.Empty, ProjectID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_BuyerID", SqlDbType.NVarChar, 8, IIf(BuyerID Is Nothing, String.Empty, BuyerID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_POStatusID", SqlDbType.Int, 10, IIf(POStatusID = Nothing, 0, POStatusID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Filter_IssuedBy", SqlDbType.NVarChar, 8, IIf(IssuedBy Is Nothing, String.Empty, IssuedBy))
          End If
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@StartRowIndex", SqlDbType.Int, -1, StartRowIndex)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaximumRows", SqlDbType.Int, -1, MaximumRows)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NVarChar, 9, HttpContext.Current.Session("LoginID"))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@OrderBy", SqlDbType.NVarChar, 50, OrderBy)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@DivisionID", SqlDbType.Int, 10, Global.System.Web.HttpContext.Current.Session("DivisionID"))
          Cmd.Parameters.Add("@RecordCount", SqlDbType.Int)
          Cmd.Parameters("@RecordCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Results = New List(Of SIS.PAK.pakPO)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.PAK.pakPO(Reader))
          End While
          Reader.Close()
          _RecordCount = Cmd.Parameters("@RecordCount").Value
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function UZ_pakPOInsert(ByVal Record As SIS.PAK.pakPO) As SIS.PAK.pakPO
      Dim _Result As SIS.PAK.pakPO = pakPOInsert(Record)
      Return _Result
    End Function
    Public Shared Function UZ_pakPOUpdate(ByVal Record As SIS.PAK.pakPO) As SIS.PAK.pakPO
      Dim _Rec As SIS.PAK.pakPO = SIS.PAK.pakPO.pakPOGetByID(Record.SerialNo)
      With _Rec
        .IssueReasonID = Record.IssueReasonID
        .PortID = Record.PortID
        .ISGECRemarks = Record.ISGECRemarks
      End With
      Return SIS.PAK.pakPO.UpdateData(_Rec)
    End Function
    Public Shared Function UZ_pakPODelete(ByVal Record As SIS.PAK.pakPO) As Integer
      Dim _Result As Integer = pakPODelete(Record)
      Return _Result
    End Function
    Public Shared Function SetDefaultValues(ByVal sender As System.Web.UI.WebControls.FormView, ByVal e As System.EventArgs) As System.Web.UI.WebControls.FormView
      With sender
        Try
          CType(.FindControl("F_SerialNo"), TextBox).Text = ""
          CType(.FindControl("F_POConsignee"), TextBox).Text = ""
          CType(.FindControl("F_POOtherDetails"), TextBox).Text = ""
          CType(.FindControl("F_IssueReasonID"), Object).SelectedValue = ""
          CType(.FindControl("F_PONumber"), TextBox).Text = ""
          CType(.FindControl("F_PORevision"), TextBox).Text = ""
          CType(.FindControl("F_PODate"), TextBox).Text = ""
          CType(.FindControl("F_PODescription"), TextBox).Text = ""
          CType(.FindControl("F_POTypeID"), Object).SelectedValue = ""
          CType(.FindControl("F_SupplierID"), TextBox).Text = ""
          CType(.FindControl("F_SupplierID_Display"), Label).Text = ""
          CType(.FindControl("F_ProjectID"), TextBox).Text = ""
          CType(.FindControl("F_ProjectID_Display"), Label).Text = ""
          CType(.FindControl("F_BuyerID"), TextBox).Text = ""
          CType(.FindControl("F_BuyerID_Display"), Label).Text = ""
          CType(.FindControl("F_POStatusID"), Object).SelectedValue = ""
          CType(.FindControl("F_ISGECRemarks"), TextBox).Text = ""
          CType(.FindControl("F_SupplierRemarks"), TextBox).Text = ""
          CType(.FindControl("F_IssuedBy"), TextBox).Text = ""
          CType(.FindControl("F_IssuedBy_Display"), Label).Text = ""
          CType(.FindControl("F_IssuedOn"), TextBox).Text = ""
          CType(.FindControl("F_ClosedBy"), TextBox).Text = ""
          CType(.FindControl("F_ClosedBy_Display"), Label).Text = ""
          CType(.FindControl("F_ClosedOn"), TextBox).Text = ""
        Catch ex As Exception
        End Try
      End With
      Return sender
    End Function
    Public Shared Function GetTotalWeight(ByVal Qty As String, ByVal UnitWt As String, ByVal UOMqty As String, ByVal UOMWt As String) As Decimal
      Dim mRet As Decimal = 0
      Dim mc As SIS.PAK.UnitMultiplicationFactor = Nothing
      Try
        Dim tmpUnit As SIS.PAK.pakUnits = SIS.PAK.pakUnits.pakUnitsGetByID(UOMqty)
        If tmpUnit.UnitSetID = 3 Then '3=>Weight Unit Set
          Try
            mc = New SIS.PAK.UnitMultiplicationFactor
            mc.Unit = tmpUnit
            mc = SIS.PAK.UnitMultiplicationFactor.GetMultiplicationFactorToBaseUnit(mc)
            mRet = Qty * mc.MF
          Catch ex As Exception
          End Try
        Else
          Try
            mc = New SIS.PAK.UnitMultiplicationFactor
            mc.Unit = SIS.PAK.pakUnits.pakUnitsGetByID(UOMWt)
            mc = SIS.PAK.UnitMultiplicationFactor.GetMultiplicationFactorToBaseUnit(mc)
            mRet = Qty * UnitWt * mc.MF
          Catch ex As Exception
          End Try
        End If
      Catch ex As Exception
        Try
          mc = New SIS.PAK.UnitMultiplicationFactor
          mc.Unit = SIS.PAK.pakUnits.pakUnitsGetByID(UOMWt)
          mc = SIS.PAK.UnitMultiplicationFactor.GetMultiplicationFactorToBaseUnit(mc)
          mRet = Qty * UnitWt * mc.MF
        Catch ex1 As Exception
        End Try
      End Try
      Return mRet
    End Function
    Public Shared Function pakPOGetByPONo(ByVal PONo As String) As SIS.PAK.pakPO
      Dim Results As SIS.PAK.pakPO = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppak_LG_POSelectByPONo"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@PONo", SqlDbType.NVarChar, PONo.Length, PONo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NVarChar, 9, HttpContext.Current.Session("LoginID"))
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Reader.Read() Then
            Results = New SIS.PAK.pakPO(Reader)
          End If
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
  End Class
  Public Class dmisg002
    Public Property t_docn As String = ""
    Public Property t_revn As String = ""
    Public Property t_srno As Integer = 0
    Public Property t_item As String = ""
    Public Property t_dsca As String = ""
    Public Property t_citg As String = ""
    Public Property t_qnty As Double = 0
    Public Property t_wght As Double = 0
    Public Property t_itmt As String = ""
    Public Property t_txta As Integer = 0
    Public Property t_txtb As Integer = 0
    Public Property t_cuni As String = ""
    Public Property t_oitm As String = ""
    Public Property t_elem As String = ""
    Public Property t_Refcntd As Integer = 0
    Public Property t_Refcntu As Integer = 0
    Public Property t_sitm As String = ""

    Public Shared Function GetItems(DocumentID As String, RevisionNo As String, comp As String) As List(Of dmisg002)
      Dim Ret As New List(Of dmisg002)
      Dim Sql As String = ""
      Sql &= "select *  "
      Sql &= "  from tdmisg002" & comp & "  "
      Sql &= "  where t_docn ='" & DocumentID & "'"
      Sql &= "    and t_revn ='" & RevisionNo & "'"
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          Do While (Reader.Read())
            Ret.Add(New dmisg002(Reader))
          Loop
          Reader.Close()
        End Using
      End Using
      Return Ret
    End Function

    Sub New(ByVal rd As SqlDataReader)
      SIS.SYS.SQLDatabase.DBCommon.NewObj(Me, rd)
    End Sub
    Sub New()
      MyBase.New()
    End Sub

  End Class

End Namespace
