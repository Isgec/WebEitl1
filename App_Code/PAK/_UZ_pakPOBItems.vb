Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.PAK
  Partial Public Class pakPOBItems
    Public ReadOnly Property GetDownloadLink() As String
      Get
        Return "javascript:window.open('" & HttpContext.Current.Request.Url.Scheme & Uri.SchemeDelimiter & HttpContext.Current.Request.Url.Authority & HttpContext.Current.Request.ApplicationPath & "/PAK_Main/App_Downloads/filedownload.aspx?bitem=" & PrimaryKey & "', 'win" & _ItemNo & "', 'left=20,top=20,width=100,height=100,toolbar=1,resizable=1,scrollbars=1'); return false;"
      End Get
    End Property
    Public Function GetColor() As System.Drawing.Color
      Dim mRet As System.Drawing.Color = Drawing.Color.Black
      If Root Then
        mRet = Drawing.Color.Red
      ElseIf Middle Then
        Select Case ItemLevel
          Case 1
            mRet = Drawing.Color.Blue
          Case 2
            mRet = Drawing.Color.Green
          Case 3
            mRet = Drawing.Color.Crimson
          Case 4
            mRet = Drawing.Color.LightSeaGreen
          Case 5
            mRet = Drawing.Color.MediumVioletRed
          Case Else
            mRet = Drawing.Color.Olive
        End Select
      ElseIf Bottom Then
        If Not Freezed Then
          mRet = Drawing.Color.Black
        Else
          mRet = Drawing.Color.DarkCyan
        End If
        If StatusID = pakItemStates.DeleteRequiredByISGEC Or StatusID = pakItemStates.DeleteRequiredBySupplier Then
          mRet = Drawing.Color.Fuchsia
        End If
      End If
      Return mRet
    End Function
    Public ReadOnly Property pFree As String
      Get
        If Free Then
          Return "*"
        End If
        Return ""
      End Get
    End Property
    Public ReadOnly Property ShowCreated As String
      Get
        If Free Then
          Return "I"
        ElseIf CreatedBySupplier Then
          Return "S"
        End If
        Return ""
      End Get
    End Property
    Public ReadOnly Property PWeightPerUnit As String
      Get
        If WeightPerUnit <= 0 Then
          Return ""
        Else
          Return WeightPerUnit
        End If
      End Get
    End Property
    Public ReadOnly Property PQuantity As String
      Get
        If Quantity <= 0 Then
          Return ""
        Else
          Return Quantity
        End If
      End Get
    End Property
    Public ReadOnly Property PItemDescription As String
      Get
        Return Prefix & " " & ItemDescription
      End Get
    End Property
    Public ReadOnly Property FontBold As Boolean
      Get
        Dim mRet As Boolean = False
        If Not Bottom Then
          mRet = True
        End If
        Return mRet
      End Get
    End Property
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
    Public ReadOnly Property IsSupplier As Boolean
      Get
        Return HttpContext.Current.Session("IsSupplier")
      End Get
    End Property
    Public ReadOnly Property ACKToolTip() As String
      Get
        Dim mRet As String = "Click to acknowledge/accept changes."
        Select Case StatusID
          Case pakItemStates.DeleteRequiredBySupplier, pakItemStates.DeleteRequiredByISGEC
            mRet = "Click to acknowledge/accept DELETION of Item [Tree form here]."
          Case pakItemStates.ChangedByIsgec, pakItemStates.ChangedBySupplier
            mRet = "Click to acknowledge/accept CHANGES."
          Case pakItemStates.CreatedByISGEC, pakItemStates.CreatedBySupplier
            mRet = "Click to acknowledge/accept NEW ITEM."
        End Select
        Return mRet
      End Get
    End Property
    Public ReadOnly Property ACKConfirm() As String
      Get
        Dim mRet As String = "Acknowledge/accept changes."
        Select Case StatusID
          Case pakItemStates.DeleteRequiredBySupplier, pakItemStates.DeleteRequiredByISGEC
            mRet = "Acknowledge/accept DELETION of Item [Tree form here]."
          Case pakItemStates.ChangedByIsgec, pakItemStates.ChangedBySupplier
            mRet = "Acknowledge/accept CHANGES."
          Case pakItemStates.CreatedByISGEC, pakItemStates.CreatedBySupplier
            mRet = "Acknowledge/accept NEW ITEM."
        End Select
        Return mRet
      End Get
    End Property

    Public ReadOnly Property Editable() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          If Freezed Then Return False
          If StatusID = pakItemStates.DeleteRequiredBySupplier Then Return False
          If StatusID = pakItemStates.DeleteRequiredByISGEC Then Return False
          If FK_PAK_POBItems_SerialNo.POStatusID = pakPOStates.UnderISGECApproval And Not IsSupplier Then Return True
          If FK_PAK_POBItems_SerialNo.POStatusID = pakPOStates.UnderSupplierVerification And IsSupplier Then Return True
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public Property Deleteable As Boolean = False
    Public ReadOnly Property ModifiedWFVisible() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          If Freezed Then Return False
          If FK_PAK_POBItems_SerialNo.POStatusID = pakPOStates.UnderISGECApproval And Not IsSupplier And Changed Then Return True
          If FK_PAK_POBItems_SerialNo.POStatusID = pakPOStates.UnderSupplierVerification And IsSupplier And ChangedBySupplier Then Return True
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property

    Public ReadOnly Property AcceptWFVisible() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          If Freezed Then Return False
          If FK_PAK_POBItems_SerialNo.POStatusID = pakPOStates.UnderISGECApproval And Not IsSupplier And ChangedBySupplier Then Return True
          If FK_PAK_POBItems_SerialNo.POStatusID = pakPOStates.UnderSupplierVerification And IsSupplier And Changed Then Return True
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property FreezeWFVisible() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          If Freezed Then Return False
          If StatusID = pakItemStates.DeleteRequiredBySupplier Then Return False
          If StatusID = pakItemStates.DeleteRequiredByISGEC Then Return False
          If FK_PAK_POBItems_SerialNo.POStatusID = pakPOStates.UnderISGECApproval And Not IsSupplier Then Return True
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property UnFreezeWFVisible() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          If Not Freezed Then Return False
          If FK_PAK_POBItems_SerialNo.POStatusID = pakPOStates.UnderISGECApproval And Not IsSupplier Then Return True
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property

    Public ReadOnly Property AddChildWFVisible() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          If Freezed Then Return False
          If Quantity > 0 Then Return False
          If StatusID = pakItemStates.DeleteRequiredBySupplier Then Return False
          If StatusID = pakItemStates.DeleteRequiredByISGEC Then Return False
          If FK_PAK_POBItems_SerialNo.POStatusID = pakPOStates.UnderISGECApproval And Not IsSupplier Then Return True
          If FK_PAK_POBItems_SerialNo.POStatusID = pakPOStates.UnderSupplierVerification And IsSupplier Then Return True
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property

    Public ReadOnly Property DeleteWFVisible() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          If Root Then Return False
          If Freezed Then Return False
          If StatusID = pakItemStates.DeleteRequiredBySupplier Then Return False
          If StatusID = pakItemStates.DeleteRequiredByISGEC Then Return False
          If FK_PAK_POBItems_SerialNo.POStatusID = pakPOStates.UnderISGECApproval And Not IsSupplier Then Return True
          If FK_PAK_POBItems_SerialNo.POStatusID = pakPOStates.UnderSupplierVerification And IsSupplier Then Return True
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property UnDeleteWFVisible() As Boolean
      Get
        Dim mRet As Boolean = False
        Try
          If Root Then Return mRet
          If Freezed Then Return mRet
          If FK_PAK_POBItems_SerialNo.POStatusID = pakPOStates.UnderISGECApproval And Not IsSupplier Then
            If StatusID = pakItemStates.DeleteRequiredBySupplier Then Return True
            If StatusID = pakItemStates.DeleteRequiredByISGEC Then Return True
          End If
          If FK_PAK_POBItems_SerialNo.POStatusID = pakPOStates.UnderSupplierVerification And IsSupplier Then
            If StatusID = pakItemStates.DeleteRequiredBySupplier Then Return True
            'To disable supplier to undelete, delete marked by ISGEC, => comment the following line
            If StatusID = pakItemStates.DeleteRequiredByISGEC Then Return True
          End If
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property

    Public ReadOnly Property DeleteWFEnable() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
          mRet = GetEnable()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Private Shared Sub rDeleteWF(ByVal pItm As SIS.PAK.pakPOBItems)
      If pItm IsNot Nothing Then
        Dim Items As List(Of SIS.PAK.pakPOBItems) = SIS.PAK.pakPOBItems.GetByParentPOBItemNo(pItm.SerialNo, pItm.BOMNo, pItm.ItemNo, "")
        If Items.Count > 0 Then
          For Each Itm As SIS.PAK.pakPOBItems In Items
            rDeleteWF(Itm)
          Next
        End If
        SIS.PAK.pakPOBItems.pakPOBItemsDelete(pItm)
      End If
    End Sub
    Private Shared Sub rMarkDeleteWF(ByVal pItm As SIS.PAK.pakPOBItems, isSupplier As Boolean, LoginID As String)
      Dim Items As List(Of SIS.PAK.pakPOBItems) = SIS.PAK.pakPOBItems.GetByParentPOBItemNo(pItm.SerialNo, pItm.BOMNo, pItm.ItemNo, "")
      If Items.Count > 0 Then
        For Each Itm As SIS.PAK.pakPOBItems In Items
          rMarkDeleteWF(Itm, isSupplier, LoginID)
        Next
      End If
      With pItm
        .ChangedBySupplier = False
        .Changed = False
        .Accepted = False
        .AcceptedBySupplier = False
        .AcceptedBy = LoginID
        .AcceptedOn = Now
        If isSupplier Then
          .ChangedBySupplier = True
          .StatusID = pakItemStates.DeleteRequiredBySupplier
        Else
          .Changed = True
          .StatusID = pakItemStates.DeleteRequiredByISGEC
        End If
      End With
      SIS.PAK.pakPOBItems.UpdateData(pItm)
    End Sub
    Public Shared Function AcceptDeleteWF(ByVal SerialNo As Int32, ByVal BOMNo As Int32, ByVal ItemNo As Int32) As SIS.PAK.pakPOBItems
      Dim Results As SIS.PAK.pakPOBItems = pakPOBItemsGetByID(SerialNo, BOMNo, ItemNo)
      rDeleteWF(Results)
      Return Results
    End Function
    Public Shared Function CanBeDeleted(ByVal pItm As SIS.PAK.pakPOBItems) As Boolean
      If pItm Is Nothing Then Return True
      If pItm.QualityClearedQty > 0 Or pItm.QualityClearedQtyStage > 0 Or pItm.QuantityDespatched > 0 Or pItm.QuantityDespatchedToPort > 0 Then
        Return False
      End If
      Dim Items As List(Of SIS.PAK.pakPOBItems) = SIS.PAK.pakPOBItems.GetByParentPOBItemNo(pItm.SerialNo, pItm.BOMNo, pItm.ItemNo, "")
      If Items.Count > 0 Then
        For Each Itm As SIS.PAK.pakPOBItems In Items
          If Not CanBeDeleted(Itm) Then
            Return False
          End If
        Next
      End If
      Return True
    End Function
    Public Shared Function DeleteWF(ByVal SerialNo As Int32, ByVal BOMNo As Int32, ByVal ItemNo As Int32) As SIS.PAK.pakPOBItems
      Dim Results As SIS.PAK.pakPOBItems = pakPOBItemsGetByID(SerialNo, BOMNo, ItemNo)
      '===================================
      'Check QC or Despatch, If Not found then only can initiate or delete
      If Not CanBeDeleted(Results) Then
        Throw New Exception("There are QC or Despatch in Item [tree from here], CAN Not perform Delete. You may try to delete individual Item.")
      End If
      '===================================
      Dim isMarking As Boolean = False
      Dim isSupplier As Boolean = HttpContext.Current.Session("IsSupplier")
      Dim LoginID As String = HttpContext.Current.Session("LoginID")
      If isSupplier Then
        If Not Results.CreatedBySupplier Then
          rMarkDeleteWF(Results, isSupplier, LoginID)
          isMarking = True
        ElseIf Results.CreatedBySupplier AndAlso Results.StatusID <> pakItemStates.CreatedBySupplier Then
          rMarkDeleteWF(Results, isSupplier, LoginID)
          isMarking = True
        ElseIf Results.CreatedBySupplier AndAlso Results.StatusID = pakItemStates.CreatedBySupplier Then
          'Status is NOT changed, status will be changed by ISGEC after acceptance
          rDeleteWF(Results)
        End If
      Else
        If Results.CreatedBySupplier Then
          rMarkDeleteWF(Results, isSupplier, LoginID)
          isMarking = True
        ElseIf Not Results.CreatedBySupplier AndAlso Results.StatusID <> pakItemStates.CreatedByISGEC Then
          rMarkDeleteWF(Results, isSupplier, LoginID)
          isMarking = True
        ElseIf Results.StatusID = pakItemStates.CreatedByISGEC Then
          rDeleteWF(Results)
        End If
      End If
      If Not isMarking Then
        If Not Results.Root Then
          Dim tmpP As SIS.PAK.pakPOBItems = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(Results.SerialNo, Results.BOMNo, Results.ParentItemNo)
          If tmpP IsNot Nothing Then
            If Not tmpP.Root Then
              Dim tmpPs As List(Of SIS.PAK.pakPOBItems) = SIS.PAK.pakPOBItems.GetByParentPOBItemNo(Results.SerialNo, Results.BOMNo, Results.ParentItemNo, "")
              If tmpPs.Count <= 0 Then
                tmpP.Middle = False
                tmpP.Bottom = True
                tmpP = UpdateData(tmpP)
              End If
            End If
          End If
        End If
      End If
      Return Results
    End Function
    Public Shared Function GetMaxItemNo(ByVal SerialNo As Int32, ByVal BOMNo As Int32) As Integer
      Dim mRet As Integer = 1
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "select MAX(ISNULL(ItemNo,1)) as cnt from pak_POBItems where serialno=" & SerialNo & " and bomno=" & BOMNo
          Con.Open()
          mRet = Cmd.ExecuteScalar()
          mRet = mRet + 1
        End Using
      End Using
      Return mRet
    End Function
    Private Shared Sub rUnDelete(ByVal pItm As SIS.PAK.pakPOBItems, isSupplier As Boolean, LoginID As String)
      Dim Items As List(Of SIS.PAK.pakPOBItems) = SIS.PAK.pakPOBItems.GetByParentPOBItemNo(pItm.SerialNo, pItm.BOMNo, pItm.ItemNo, "")
      If Items.Count > 0 Then
        For Each Itm As SIS.PAK.pakPOBItems In Items
          rUnDelete(Itm, isSupplier, LoginID)
        Next
      End If
      With pItm
        .Changed = False
        .ChangedBySupplier = False
        If isSupplier Then
          .StatusID = pakItemStates.UndeleteBySupplier
        Else
          .StatusID = pakItemStates.UndeleteByIsgec
        End If
        .AcceptedBy = LoginID
        .AcceptedOn = Now
      End With
      SIS.PAK.pakPOBItems.UpdateData(pItm)
    End Sub
    Public Shared Function UnDeleteWF(ByVal SerialNo As Int32, ByVal BOMNo As Int32, ByVal ItemNo As Int32) As SIS.PAK.pakPOBItems
      Dim Results As SIS.PAK.pakPOBItems = pakPOBItemsGetByID(SerialNo, BOMNo, ItemNo)
      Dim isSupplier As Boolean = HttpContext.Current.Session("IsSupplier")
      Dim LoginID As String = HttpContext.Current.Session("LoginID")
      If isSupplier Then
        'To Restrict Supplier, He can not undelete ISGEC request for Deletion
        'Un comment following condition
        'If Results.StatusID = pakItemStates.DeleteRequiredBySupplier Then
        rUnDelete(Results, isSupplier, LoginID)
        'End If
      Else
        'Isgec Person can unmark any delete request
        rUnDelete(Results, isSupplier, LoginID)
      End If
      Return Results
    End Function

    Private Shared Sub rCopyCWF(ByVal pItm As SIS.PAK.pakPOBItems, ByVal ParentItemNo As Integer)
      Dim Items As List(Of SIS.PAK.pakPOBItems) = SIS.PAK.pakPOBItems.GetByParentPOBItemNo(pItm.SerialNo, pItm.BOMNo, ParentItemNo, "")
      If Items.Count > 0 Then
        For Each Itm As SIS.PAK.pakPOBItems In Items
          Dim ItemNo As Integer = Itm.ItemNo
          Itm.ItemNo = GetMaxItemNo(Itm.SerialNo, Itm.BOMNo)
          Itm.ParentItemNo = pItm.ItemNo
          Itm.Free = True
          Itm.StatusID = pakItemStates.CreatedByISGEC
          Itm = pakPOBItems.pakPOBItemsInsert(Itm)
          rCopyCWF(Itm, ItemNo)
        Next
      End If
    End Sub

    Public Shared Function CopyCWF(ByVal SerialNo As Int32, ByVal BOMNo As Int32, ByVal ItemNo As Int32) As SIS.PAK.pakPOBItems
      Dim Results As SIS.PAK.pakPOBItems = pakPOBItemsGetByID(SerialNo, BOMNo, ItemNo)
      With Results
        .ItemNo = GetMaxItemNo(SerialNo, BOMNo)
        .Free = True
        .StatusID = pakItemStates.CreatedByISGEC
      End With
      Results = pakPOBItems.pakPOBItemsInsert(Results)
      rCopyCWF(Results, ItemNo)
      Return Results
    End Function
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
        Dim mRet As Boolean = False
        Try
          Select Case Me.FK_PAK_POBItems_SerialNo.POStatusID
            Case pakPOStates.UnderISGECApproval
              If Not Freezed Then
                mRet = True
              End If
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
        Dim mRet As Boolean = False
        Try
          Select Case Me.FK_PAK_POBItems_SerialNo.POStatusID
            Case pakPOStates.UnderISGECApproval
              If Freezed Then
                mRet = True
              End If
          End Select
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
    'Public Shared Function InitiateWF(ByVal SerialNo As Int32, ByVal BOMNo As Int32, ByVal ItemNo As Int32) As SIS.PAK.pakPOBItems
    '  Dim Results As SIS.PAK.pakPOBItems = pakPOBItemsGetByID(SerialNo, BOMNo, ItemNo)
    '  Return Results
    'End Function
    Private Shared Sub rFreezeWF(ByVal pItm As SIS.PAK.pakPOBItems, ElementID As String)
      If Not pItm.Freezed Then
        With pItm
          .StatusID = pakItemStates.FreezedbyISGEC
          .FreezedBy = HttpContext.Current.Session("LoginID")
          .FreezedOn = Now
          .Freezed = True
          If ElementID <> "" Then .ElementID = ElementID
          If .ItemCode = "" Then .ItemCode = .ItemNo
          If .UOMQuantity = "" Then .UOMQuantity = 8 'Nos
          If .UOMWeight = "" Then .UOMWeight = 6 'Kg
        End With
        pItm = pakPOBItems.UpdateData(pItm)
      End If
      Dim Items As List(Of SIS.PAK.pakPOBItems) = SIS.PAK.pakPOBItems.GetByParentPOBItemNo(pItm.SerialNo, pItm.BOMNo, pItm.ItemNo, "")
      If Items.Count > 0 Then
        For Each Itm As SIS.PAK.pakPOBItems In Items
          rFreezeWF(Itm, ElementID)
        Next
      End If
    End Sub
    Public Shared Function FreezeWF(ByVal SerialNo As Int32, ByVal BOMNo As Int32, ByVal ItemNo As Int32) As SIS.PAK.pakPOBItems
      Dim Results As SIS.PAK.pakPOBItems = pakPOBItemsGetByID(SerialNo, BOMNo, ItemNo)
      rFreezeWF(Results, Results.FK_PAK_POBItems_PAK_POBOM.ElementID)
      Return Results
    End Function
    Private Shared Sub rUnFreezeWF(ByVal pItm As SIS.PAK.pakPOBItems)
      If pItm.Freezed Then
        With pItm
          .StatusID = pakItemStates.UnfreezedByISGEC
          .FreezedBy = HttpContext.Current.Session("LoginID")
          .FreezedOn = Now
          .Freezed = False
        End With
        pItm = pakPOBItems.UpdateData(pItm)
      End If
      Dim Items As List(Of SIS.PAK.pakPOBItems) = SIS.PAK.pakPOBItems.GetByParentPOBItemNo(pItm.SerialNo, pItm.BOMNo, pItm.ItemNo, "")
      If Items.Count > 0 Then
        For Each Itm As SIS.PAK.pakPOBItems In Items
          rUnFreezeWF(Itm)
        Next
      End If
    End Sub
    Public Shared Function UnFreezeWF(ByVal SerialNo As Int32, ByVal BOMNo As Int32, ByVal ItemNo As Int32) As SIS.PAK.pakPOBItems
      Dim Results As SIS.PAK.pakPOBItems = pakPOBItemsGetByID(SerialNo, BOMNo, ItemNo)
      rUnFreezeWF(Results)
      Return Results
    End Function
    'Public Shared Function CompleteWF(ByVal SerialNo As Int32, ByVal BOMNo As Int32, ByVal ItemNo As Int32) As SIS.PAK.pakPOBItems
    '  Dim Results As SIS.PAK.pakPOBItems = pakPOBItemsGetByID(SerialNo, BOMNo, ItemNo)
    '  Return Results
    'End Function
    Public Shared Function GetChangedCount(ByVal SerialNo As Int32, ByVal BOMNo As Int32) As Integer
      Dim Results As Integer = 0
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "Select isnull(Count(ItemNo),0) as cnt from pak_pobItems where serialno=" & SerialNo & " and bomno=" & BOMNo & " and changed=1 "
          Con.Open()
          Results = Cmd.ExecuteScalar()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function GetChangedBySupplierCount(ByVal SerialNo As Int32, ByVal BOMNo As Int32) As Integer
      Dim Results As Integer = 0
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "Select isnull(Count(ItemNo),0) as cnt from pak_pobItems where serialno=" & SerialNo & " and bomno=" & BOMNo & " and ChangedBySupplier=1 "
          Con.Open()
          Results = Cmd.ExecuteScalar()
        End Using
      End Using
      Return Results
    End Function

    Public Shared Function GetpakPOBItemsCreatedByISGECCount(ByVal SerialNo As Int32, ByVal BOMNo As Int32) As Integer
      Dim Results As Integer = 0
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "Select Count(ItemNo) as cnt from pak_pobItems where serialno=" & SerialNo & " and bomno=" & BOMNo & " and bottom=1 and StatusID=" & pakItemStates.CreatedByISGEC
          Con.Open()
          Results = Cmd.ExecuteScalar()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function GetpakPOBItemsUnFreezedCount(ByVal SerialNo As Int32, ByVal BOMNo As Int32) As Integer
      Dim Results As Integer = 0
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "Select Count(ItemNo) as cnt from pak_pobItems where serialno=" & SerialNo & " and bomno=" & BOMNo & " and bottom=1 and Freezed=0"
          Con.Open()
          Results = Cmd.ExecuteScalar()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function GetpakPOBItemsUnCheckedISGECCount(ByVal SerialNo As Int32, ByVal BOMNo As Int32) As Integer
      Dim Results As Integer = 0
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "Select Count(ItemNo) as cnt from pak_pobItems where serialno=" & SerialNo & " and bomno=" & BOMNo & " and bottom=1 and Changed=1 and StatusID = " & pakItemStates.ChangedBySupplier
          Con.Open()
          Results = Cmd.ExecuteScalar()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function pakPOBItemsDeleteAll(ByVal SerialNo As Int32, ByVal BOMNo As Int32) As Integer
      Dim Results As Integer = 0
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "Delete from pak_pobItems where serialno=" & SerialNo & " and bomno=" & BOMNo
          Con.Open()
          Results = Cmd.ExecuteNonQuery()
        End Using
      End Using
      Return Results
    End Function

    Public Shared Function GetByParentPOBItemNo(ByVal SerialNo As Int32, ByVal BOMNo As Int32, ByVal ParentItemNo As Int32, ByVal OrderBy As String) As List(Of SIS.PAK.pakPOBItems)
      Dim Results As List(Of SIS.PAK.pakPOBItems) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppakPOBItemsSelectByParentItemNo"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@BOMNo", SqlDbType.Int, 10, IIf(BOMNo = Nothing, 0, BOMNo))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SerialNo", SqlDbType.Int, 10, IIf(SerialNo = Nothing, 0, SerialNo))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ParentItemNo", SqlDbType.Int, ParentItemNo.ToString.Length, ParentItemNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NVarChar, 9, HttpContext.Current.Session("LoginID"))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@OrderBy", SqlDbType.NVarChar, 50, OrderBy)
          Cmd.Parameters.Add("@RecordCount", SqlDbType.Int)
          Cmd.Parameters("@RecordCount").Direction = ParameterDirection.Output
          RecordCount = -1
          Results = New List(Of SIS.PAK.pakPOBItems)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.PAK.pakPOBItems(Reader))
          End While
          Reader.Close()
          RecordCount = Cmd.Parameters("@RecordCount").Value
        End Using
      End Using
      Return Results
    End Function
    Private Shared Sub getpakHBItems(ByVal SerialNo As Int32, ByVal BOMNo As Int32, ByVal pItem As SIS.PAK.pakPOBItems, ByRef cList As List(Of SIS.PAK.pakPOBItems))
      cList.Add(pItem)
      Dim Results As List(Of SIS.PAK.pakPOBItems) = SIS.PAK.pakPOBItems.GetByParentPOBItemNo(SerialNo, BOMNo, pItem.ItemNo, "")
      For Each tmp As SIS.PAK.pakPOBItems In Results
        getpakHBItems(SerialNo, BOMNo, tmp, cList)
      Next
    End Sub
    Public Shared Function UZ_pakPOBItemsSelectList(ByVal StartRowIndex As Integer, ByVal MaximumRows As Integer, ByVal OrderBy As String, ByVal SearchState As Boolean, ByVal SearchText As String, ByVal BOMNo As Int32, ByVal SerialNo As Int32) As List(Of SIS.PAK.pakPOBItems)
      Dim tmp As SIS.PAK.pakPOBOM = SIS.PAK.pakPOBOM.pakPOBOMGetByID(SerialNo, BOMNo)
      Dim Results As New List(Of SIS.PAK.pakPOBItems)
      Dim rItm As SIS.PAK.pakPOBItems = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(SerialNo, BOMNo, tmp.ItemNo)
      getpakHBItems(SerialNo, BOMNo, rItm, Results)
      RecordCount = Results.Count
      Return Results
    End Function
    'Public Shared Function UZ_pakPOBItemsInsert(ByVal Record As SIS.PAK.pakPOBItems) As SIS.PAK.pakPOBItems
    '  Dim _Result As SIS.PAK.pakPOBItems = pakPOBItemsInsert(Record)
    '  Return _Result
    'End Function
    'Public Shared Function UZ_pakPOBItemsUpdate(ByVal Record As SIS.PAK.pakPOBItems) As SIS.PAK.pakPOBItems
    '  Dim _Rec As SIS.PAK.pakPOBItems = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(Record.SerialNo, Record.BOMNo, Record.ItemNo)
    '  With _Rec
    '    .Quantity = Record.Quantity
    '    .WeightPerUnit = Record.WeightPerUnit
    '    .UOMWeight = Record.UOMWeight
    '    .ISGECRemarks = Record.ISGECRemarks
    '    .UOMQuantity = Record.UOMQuantity
    '    .TotalWeight = SIS.PAK.pakPO.GetTotalWeight(.Quantity, .WeightPerUnit, .UOMQuantity, .UOMWeight)
    '  End With
    '  Return SIS.PAK.pakPOBItems.UpdateData(_Rec)
    'End Function
    'Public Shared Function UZ_pakPOBIEnggUpdate(ByVal Record As SIS.PAK.pakPOBItems) As SIS.PAK.pakPOBItems
    '  Dim _Rec As SIS.PAK.pakPOBItems = SIS.PAK.pakPOBItems.pakPOBItemsGetByID(Record.SerialNo, Record.BOMNo, Record.ItemNo)
    '  With _Rec
    '    .ItemCode = Record.ItemCode
    '    .SupplierItemCode = Record.SupplierItemCode
    '    .ItemDescription = Record.ItemDescription
    '    .ElementID = Record.ElementID
    '    .Quantity = Record.Quantity
    '    .WeightPerUnit = Record.WeightPerUnit
    '    .UOMWeight = Record.UOMWeight
    '    .ISGECRemarks = Record.ISGECRemarks
    '    .UOMQuantity = Record.UOMQuantity
    '    .TotalWeight = SIS.PAK.pakPO.GetTotalWeight(.Quantity, .WeightPerUnit, .UOMQuantity, .UOMWeight)
    '  End With
    '  Return SIS.PAK.pakPOBItems.UpdateData(_Rec)
    'End Function
    Public Shared Function UZ_pakPOBItemsDelete(ByVal Record As SIS.PAK.pakPOBItems) As Integer
      Dim _Result As Integer = pakPOBItemsDelete(Record)
      Return _Result
    End Function
    Public Shared Function pakPOBItemsGetByItemCode(ByVal SerialNo As Int32, ByVal BOMNo As Int32, ByVal ItemCode As String) As SIS.PAK.pakPOBItems
      Dim Results As SIS.PAK.pakPOBItems = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppak_LG_POBItemsSelectByItemCode"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SerialNo", SqlDbType.Int, SerialNo.ToString.Length, SerialNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@BOMNo", SqlDbType.Int, BOMNo.ToString.Length, BOMNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ItemCode", SqlDbType.NVarChar, ItemCode.Length, ItemCode)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NVarChar, 9, HttpContext.Current.Session("LoginID"))
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Reader.Read() Then
            Results = New SIS.PAK.pakPOBItems(Reader)
          End If
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function pakPOBItemsGetBySupplierItemCode(ByVal SerialNo As Int32, ByVal BOMNo As Int32, ByVal SupplierItemCode As String) As SIS.PAK.pakPOBItems
      Dim Results As SIS.PAK.pakPOBItems = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sppak_LG_POBItemsSelectBySupplierItemCode"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SerialNo", SqlDbType.Int, SerialNo.ToString.Length, SerialNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@BOMNo", SqlDbType.Int, BOMNo.ToString.Length, BOMNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SupplierItemCode", SqlDbType.NVarChar, SupplierItemCode.Length, SupplierItemCode)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NVarChar, 9, HttpContext.Current.Session("LoginID"))
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Reader.Read() Then
            Results = New SIS.PAK.pakPOBItems(Reader)
          End If
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function SetDefaultValues(ByVal sender As System.Web.UI.WebControls.FormView, ByVal e As System.EventArgs) As System.Web.UI.WebControls.FormView
      With sender
        Try
          CType(.FindControl("F_ItemNo"), TextBox).Text = 0
          CType(.FindControl("F_ItemCode"), TextBox).Text = ""
          CType(.FindControl("F_ItemDescription"), TextBox).Text = ""
          CType(.FindControl("F_ElementID"), TextBox).Text = ""
          CType(.FindControl("F_ElementID_Display"), Label).Text = ""
          CType(.FindControl("F_Quantity"), TextBox).Text = "0.0000"
          CType(.FindControl("F_WeightPerUnit"), TextBox).Text = "0.0000"
          CType(.FindControl("F_StatusID"), TextBox).Text = ""
          CType(.FindControl("F_StatusID_Display"), Label).Text = ""
          CType(.FindControl("F_Bottom"), CheckBox).Checked = False
          CType(.FindControl("F_Free"), CheckBox).Checked = False
          CType(.FindControl("F_Middle"), CheckBox).Checked = False
          CType(.FindControl("F_Root"), CheckBox).Checked = False
          CType(.FindControl("F_ChangedBySupplier"), CheckBox).Checked = False
          CType(.FindControl("F_CreatedBySupplier"), CheckBox).Checked = False
          CType(.FindControl("F_Changed"), CheckBox).Checked = False
          CType(.FindControl("F_Active"), CheckBox).Checked = False
          CType(.FindControl("F_FreezedBySupplier"), CheckBox).Checked = False
          CType(.FindControl("F_AcceptedBySupplier"), CheckBox).Checked = False
          CType(.FindControl("F_QuantityDespatched"), TextBox).Text = "0.0000"
          CType(.FindControl("F_TotalWeightToDespatch"), TextBox).Text = "0.0000"
          CType(.FindControl("F_TotalWeightDespatched"), TextBox).Text = "0.0000"
          CType(.FindControl("F_TotalWeightReceived"), TextBox).Text = "0.0000"
          CType(.FindControl("F_QuantityReceived"), TextBox).Text = "0.0000"
          CType(.FindControl("F_Prefix"), TextBox).Text = ""
          CType(.FindControl("F_ItemLevel"), TextBox).Text = 0
          CType(.FindControl("F_QuantityToPack"), TextBox).Text = "0.0000"
          CType(.FindControl("F_QuantityToDespatch"), TextBox).Text = "0.0000"
          CType(.FindControl("F_TotalWeightToPack"), TextBox).Text = "0.0000"
          CType(.FindControl("F_DocumentNo"), TextBox).Text = ""
          CType(.FindControl("F_DocumentNo_Display"), Label).Text = ""
          CType(.FindControl("F_UOMWeight"), Object).SelectedValue = ""
          CType(.FindControl("F_ParentItemNo"), TextBox).Text = ""
          CType(.FindControl("F_ParentItemNo_Display"), Label).Text = ""
          CType(.FindControl("F_SupplierRemarks"), TextBox).Text = ""
          CType(.FindControl("F_ISGECRemarks"), TextBox).Text = ""
          CType(.FindControl("F_BOMNo"), TextBox).Text = ""
          CType(.FindControl("F_SerialNo"), TextBox).Text = ""
          CType(.FindControl("F_SerialNo_Display"), Label).Text = ""
          CType(.FindControl("F_SupplierItemCode"), TextBox).Text = ""
          CType(.FindControl("F_UOMQuantity"), Object).SelectedValue = ""
          CType(.FindControl("F_DivisionID"), TextBox).Text = ""
          CType(.FindControl("F_DivisionID_Display"), Label).Text = ""
          CType(.FindControl("F_AcceptedOn"), TextBox).Text = ""
          CType(.FindControl("F_AcceptedBy"), TextBox).Text = ""
          CType(.FindControl("F_AcceptedBy_Display"), Label).Text = ""
          CType(.FindControl("F_Freezed"), CheckBox).Checked = False
          CType(.FindControl("F_FreezedOn"), TextBox).Text = ""
          CType(.FindControl("F_FreezedBy"), TextBox).Text = ""
          CType(.FindControl("F_FreezedBy_Display"), Label).Text = ""
          CType(.FindControl("F_ISGECWeightPerUnit"), TextBox).Text = "0.0000"
          CType(.FindControl("F_ISGECQuantity"), TextBox).Text = "0.0000"
          CType(.FindControl("F_SupplierQuantity"), TextBox).Text = "0.0000"
          CType(.FindControl("F_Accepted"), CheckBox).Checked = False
          CType(.FindControl("F_SupplierWeightPerUnit"), TextBox).Text = "0.0000"
        Catch ex As Exception
        End Try
      End With
      Return sender
    End Function
  End Class
End Namespace
