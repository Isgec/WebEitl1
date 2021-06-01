Imports System.Xml
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Net.Mail
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Text
Namespace SIS.PAK
  Public Class Alerts
    Public Shared Sub ResetAndSendPasswordToSupplier(ByVal SerialNo As Integer)
      Dim oPO As SIS.PAK.pakPO = SIS.PAK.pakPO.pakPOGetByID(SerialNo)
      Dim oClient As SmtpClient = New SmtpClient("192.9.200.214", 25)
      oClient.Credentials = New Net.NetworkCredential("adskvaultadmin", "isgec@123")
      Dim oMsg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()
      With oMsg
        Try
          Dim aIDs() As String = oPO.FK_PAK_SupplierID.EMailID.Split(",;".ToCharArray)
          For Each tmp As String In aIDs
            .To.Add(New MailAddress(tmp.Trim, tmp.Trim))
          Next
        Catch ex As Exception
          .To.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
        End Try
        .From = New MailAddress("baansupport@isgec.co.in", "BaaN Support")
        .Subject = "Authorization Details to access PO Issued from ISGEC"
        .IsBodyHtml = True


        Dim oTbl As New Table
        oTbl.GridLines = GridLines.Both
        oTbl.Width = 900
        oTbl.Style.Add("text-align", "left")
        oTbl.Style.Add("font", "Tahoma")

        Dim oCol As TableCell = Nothing
        Dim oRow As TableRow = Nothing
        '1.
        oRow = New TableRow
        oCol = New TableCell
        oCol.Text = "Login Detail"
        oCol.Style.Add("text-align", "center")
        oCol.Font.Size = "14"
        oRow.Cells.Add(oCol)
        oTbl.Rows.Add(oRow)

        oRow = New TableRow
        oCol = New TableCell
        Dim supplierID As String = SIS.PAK.pakBPLoginMap.GetLoginID(oPO.SupplierID, HttpContext.Current.Session("FinanceCompany"))
        SIS.QCM.qcmUsers.ChangePassword(supplierID, supplierID)
        oCol.Text = "Dear Supplier, <br /><br /> Your password has been reset."
        oCol.Text &= "<br /><b>URL:</b> http://cloud.isgec.co.in/WebEitl1"
        oCol.Text &= "<br /><b>User ID:</b> " & supplierID
        oCol.Text &= "<br /><b>Password:</b> " & supplierID
        oCol.Style.Add("text-align", "left")
        oCol.Font.Size = "10"
        oRow.Cells.Add(oCol)
        oTbl.Rows.Add(oRow)


        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim writer As HtmlTextWriter = New HtmlTextWriter(sw)
        Try
          oTbl.RenderControl(writer)
        Catch ex As Exception
        End Try
        Dim Header As String = ""
        Header = Header & "<html xmlns=""http://www.w3.org/1999/xhtml"">"
        Header = Header & "<head>"
        Header = Header & "<title></title>"
        Header = Header & "<style>"
        Header = Header & "body{margin: 10px auto auto 60px;}"
        Header = Header & ".tblHd, .tblHd td{font-size: 12px;font-weight: bold;height: 30px !important;background-color:lightgray;}"
        Header = Header & "table{"
        Header = Header & "border: solid 1pt black;"
        Header = Header & "border-collapse:collapse;"
        Header = Header & "font-family: Tahoma;}"

        Header = Header & "td{padding-left: 4px;"
        Header = Header & "border: solid 1pt black;"
        Header = Header & "font-family: Tahoma;"
        Header = Header & "font-size: 9px;"
        Header = Header & "vertical-align:top;}"

        Header = Header & "</style>"
        Header = Header & "</head>"
        Header = Header & "<body>"
        Header = Header & sb.ToString
        Header = Header & "</body></html>"
        .Body = Header
      End With
      Try
        If Not Convert.ToBoolean(ConfigurationManager.AppSettings("Testing")) Then
          oClient.Send(oMsg)
        End If
      Catch ex As Exception
      End Try
    End Sub

    Public Shared Function TCAlert(ByVal PONo As Integer, ByVal AlertEvent As pakTCAlertEvents, Optional ByVal ItemNo As Integer = 0, Optional ByVal UploadNo As Integer = 0) As Boolean
      Dim oPO As SIS.PAK.pakTCPO = Nothing
      Dim oItm As SIS.PAK.pakTCPOL = Nothing
      Dim oUpl As SIS.PAK.pakTCPOLR = Nothing
      Select Case AlertEvent
        Case pakTCAlertEvents.OpenPORequested, pakTCAlertEvents.OpenPORequestExecuted, pakTCAlertEvents.POClosed
          oPO = SIS.PAK.pakTCPO.pakTCPOGetByID(PONo)
        Case pakTCAlertEvents.TCPOIssued
          oPO = SIS.PAK.pakTCPO.pakTCPOGetByID(PONo)
        Case pakTCAlertEvents.DocumentsSubmitted
          oPO = SIS.PAK.pakTCPO.pakTCPOGetByID(PONo)
          oItm = SIS.PAK.pakTCPOL.pakTCPOLGetByID(PONo, ItemNo)
          oUpl = SIS.PAK.pakTCPOLR.pakTCPOLRGetByID(PONo, ItemNo, UploadNo)
      End Select
      If AlertEvent = pakTCAlertEvents.TCPOIssued Then
        SendPasswordToSupplier(PONo, True)
      End If
      Dim aErr As New ArrayList
      Dim mRet As String = ""
      Dim oClient As SmtpClient = New SmtpClient("192.9.200.214", 25)
      oClient.Credentials = New Net.NetworkCredential("adskvaultadmin", "isgec@123")
      Dim oMsg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()
      With oMsg
        Select Case AlertEvent
          Case pakTCAlertEvents.TCPOIssued, pakTCAlertEvents.OpenPORequestExecuted
            'FROM Issuer
            Try
              If oPO.FK_PAK_PO_IssuedBy.EMailID = String.Empty Then
                aErr.Add(oPO.IssuedBy & " " & oPO.FK_PAK_PO_IssuedBy.UserFullName)
                .From = New MailAddress("baansupport@isgec.co.in", "BaaN Support")
                .CC.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              Else
                .From = New MailAddress(oPO.FK_PAK_PO_IssuedBy.EMailID.Trim, oPO.FK_PAK_PO_IssuedBy.UserFullName)
                .CC.Add(New MailAddress(oPO.FK_PAK_PO_IssuedBy.EMailID.Trim, oPO.FK_PAK_PO_IssuedBy.UserFullName))
              End If
              'CC to Buyer
              If oPO.FK_PAK_PO_BuyerID.EMailID = String.Empty Then
                aErr.Add(oPO.BuyerID & " " & oPO.FK_PAK_PO_BuyerID.UserFullName)
                .CC.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              Else
                If Not Convert.ToBoolean(ConfigurationManager.AppSettings("Testing")) Then
                  .CC.Add(New MailAddress(oPO.FK_PAK_PO_BuyerID.EMailID.Trim, oPO.FK_PAK_PO_BuyerID.UserFullName))
                End If
              End If
              'Default CC Include
              .CC.Add(New MailAddress("harishkumar@isgec.co.in", "Harish Kumar"))
              .CC.Add(New MailAddress("lalit@isgec.co.in", "Lalit Gupta"))

            Catch ex As Exception
              .From = New MailAddress("baansupport@isgec.co.in", "BaaN Support")
              .CC.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              .CC.Add(New MailAddress("harishkumar@isgec.co.in", "Harish Kumar"))
            End Try
            'TO Supplier
            If oPO.FK_PAK_SupplierID.EMailID = String.Empty Then
              aErr.Add(oPO.SupplierID & " " & oPO.FK_PAK_SupplierID.BPName)
              .To.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
            Else
              Dim aIDs() As String = oPO.FK_PAK_SupplierID.EMailID.Split(";,".ToCharArray)
              For Each tmp As String In aIDs
                .To.Add(New MailAddress(tmp.Trim, tmp.Trim))
              Next
            End If
            'End of Supplier ID
            If AlertEvent = pakTCAlertEvents.TCPOIssued Then
              .Subject = "Issued Purchase Order No.: " & oPO.PONumber
            Else
              'If oRQ.ExecutedToOpen Then
              '  .Subject = "Re-Opened Purchase Order No.: " & oPO.PONumber
              'Else
              '  .Subject = "Purchase Order Not Opened.: " & oPO.PONumber
              'End If
            End If
          Case pakTCAlertEvents.DocumentsSubmitted, pakTCAlertEvents.POClosed, pakTCAlertEvents.OpenPORequested
            'From Supplier
            Try
              If oPO.FK_PAK_SupplierID.EMailID = String.Empty Then
                aErr.Add(oPO.SupplierID & " " & oPO.FK_PAK_SupplierID.BPName)
                .From = New MailAddress("baansupport@isgec.co.in", "BaaN Support")
                .CC.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              Else
                Dim aIDs() As String = oPO.FK_PAK_SupplierID.EMailID.Split(",;".ToCharArray)
                Dim First As Boolean = True
                For Each eid As String In aIDs
                  If First Then
                    .From = New MailAddress(eid.Trim, oPO.FK_PAK_SupplierID.BPName)
                    First = False
                  End If
                  .CC.Add(New MailAddress(eid.Trim, eid.Trim))
                Next
              End If
              'End of Supplier ID
            Catch ex As Exception
              .From = New MailAddress("baansupport@isgec.co.in", "BaaN Support")
              .CC.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              .CC.Add(New MailAddress("harishkumar@isgec.co.in", "Harish Kumar"))
            End Try
            'To Issuer
            Try
              If oPO.FK_PAK_PO_IssuedBy.EMailID = String.Empty Then
                aErr.Add(oPO.IssuedBy & " " & oPO.FK_PAK_PO_IssuedBy.UserFullName)
                .To.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              Else
                .To.Add(New MailAddress(oPO.FK_PAK_PO_IssuedBy.EMailID.Trim, oPO.FK_PAK_PO_IssuedBy.UserFullName))
              End If
              'Include Buyer
              If oPO.FK_PAK_PO_BuyerID.EMailID = String.Empty Then
                aErr.Add(oPO.BuyerID & " " & oPO.FK_PAK_PO_BuyerID.UserFullName)
                .CC.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              Else
                If Not Convert.ToBoolean(ConfigurationManager.AppSettings("Testing")) Then
                  .CC.Add(New MailAddress(oPO.FK_PAK_PO_BuyerID.EMailID.Trim, oPO.FK_PAK_PO_BuyerID.UserFullName))
                End If
              End If
            Catch ex As Exception
              .To.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              .CC.Add(New MailAddress("harishkumar@isgec.co.in", "Harish Kumar"))
            End Try
            'End of Buyer
            If AlertEvent = pakTCAlertEvents.POClosed Then
              .Subject = "Closed Purchase Order No.: " & oPO.PONumber
            ElseIf AlertEvent = pakTCAlertEvents.OpenPORequested Then
              .Subject = "Request to Re-Open Purchase Order No.: " & oPO.PONumber
            Else
              .Subject = "Document Submitted for Technical Clearance: " & oItm.ItemDescription
            End If
        End Select

        If AlertEvent = pakTCAlertEvents.DocumentsSubmitted Then
          'Add Project wise Alert Group in CC
          Dim eunt As String = SIS.PAK.erpData.erpProject.GetEnterpriseUnit(oPO.ProjectID)
          'Select Case oPO.PONumber.Substring(0, 4)
          '  Case "P101"
          '    eunt = "EU200"
          '  Case "P201"
          '    eunt = "EU230"
          '  Case "P301"
          '    eunt = "EU210"
          '  Case "P401"
          '    eunt = "EU220"
          '  Case "P501"
          '    eunt = "EU240"
          '  Case "P250"
          '    eunt = "EU250"
          'End Select

          Dim Users As List(Of SIS.PAK.erpData.erpEnggGroup) = SIS.PAK.erpData.erpEnggGroup.GetFromERP(eunt, oPO.ProjectID)
          For Each usr As SIS.PAK.erpData.erpEnggGroup In Users
            Try
              Dim ad As MailAddress = New MailAddress(usr.EMailID.Trim, usr.EmpName)
              If Not .CC.Contains(ad) Then
                .CC.Add(ad)
              End If
            Catch ex As Exception
              aErr.Add(usr.EmpID & " " & usr.EmpName)
            End Try
          Next
          'End of Add Project Users
        End If
      End With
      With oMsg
        If .To.Count <= 0 Then
          .To.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
        End If
        .IsBodyHtml = True
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim writer As HtmlTextWriter = New HtmlTextWriter(sw)

        Dim oTbl As Table = GetTCPOTable(oPO, AlertEvent)
        Try
          oTbl.RenderControl(writer)
          sb.Append("<br /><br />")
        Catch ex As Exception
        End Try
        Select Case AlertEvent
          Case pakTCAlertEvents.TCPOIssued
            oTbl = GetTCItemTable(oPO)
            Try
              oTbl.RenderControl(writer)
              sb.Append("<br /><br />")
            Catch ex As Exception
            End Try
          Case pakTCAlertEvents.DocumentsSubmitted
            Try
              oTbl = GetTCItemTable(oPO, ItemNo)
              oTbl.RenderControl(writer)
              sb.Append("<br /><br />")
            Catch ex As Exception
            End Try
            sb.Append("<b>Receipt No.: </b>" & oUpl.ReceiptNo)
            sb.Append("<br /><br />")
            oTbl = GetTCUploadTable(PONo, ItemNo, UploadNo)
            Try
              oTbl.RenderControl(writer)
              sb.Append("<br /><br />")
            Catch ex As Exception
            End Try
        End Select

        Dim Header As String = ""
        Header = Header & "<html xmlns=""http://www.w3.org/1999/xhtml"">"
        Header = Header & "<head>"
        Header = Header & "<title></title>"
        Header = Header & "<style>"
        Header = Header & "body{margin: 10px auto auto 60px;}"
        Header = Header & ".tblHd, .tblHd td{font-size: 12px;font-weight: bold;height: 30px !important;background-color:lightgray;}"
        Header = Header & "table{"
        Header = Header & "border: solid 1pt black;"
        Header = Header & "border-collapse:collapse;"
        Header = Header & "font-family: Tahoma;}"

        Header = Header & "td{padding-left: 4px;"
        Header = Header & "border: solid 1pt black;"
        Header = Header & "font-family: Tahoma;"
        Header = Header & "font-size: 9px;"
        Header = Header & "vertical-align:top;}"

        Header = Header & "</style>"
        Header = Header & "</head>"
        Header = Header & "<body>"
        If aErr.Count > 0 Then
          Header = Header & "<table>"
          Header = Header & "<tr><td style=""color: red""><i><b>"
          Header = Header & "NOTE: E-Mail Alert could not be delivered to following recipient(s), Please update their E-Mail ID in EITL/ERP Application."
          Header = Header & "</b></i></td></tr>"
          For Each Err As String In aErr
            Header = Header & "<tr><td color=""red""><i>"
            Header = Header & Err
            Header = Header & "</i></td></tr>"
          Next
          Header = Header & "</table>"
        End If
        If AlertEvent = pakTCAlertEvents.OpenPORequested Then
          'Header = Header & "<br /><table>"
          'Header = Header & "<tr><td><b>"
          'Header = Header & "REASON TO RE-OPEN"
          'Header = Header & "</b></td></tr>"
          'Header = Header & "<tr><td>"
          'Header = Header & oRQ.Remarks
          'Header = Header & "</td></tr>"
          'Header = Header & "</table>"
        ElseIf AlertEvent = pakTCAlertEvents.OpenPORequestExecuted Then
          ''If Not oRQ.ExecutedToOpen Then
          ''  Header = Header & "<br /><table>"
          ''  Header = Header & "<tr><td><b>"
          ''  Header = Header & "REASON TO RE-OPEN"
          ''  Header = Header & "</b></td></tr>"
          ''  Header = Header & "<tr><td>"
          ''  Header = Header & oRQ.ExecuterRemarks
          ''  Header = Header & "</td></tr>"
          ''  Header = Header & "</table>"
          ''End If
        End If
        Header = Header & sb.ToString
        Header = Header & "</body></html>"
        .Body = Header
      End With
      Try
        If Not Convert.ToBoolean(ConfigurationManager.AppSettings("Testing")) Then
          oClient.Send(oMsg)
        End If
      Catch ex As Exception
      End Try
      Return True
    End Function

    Public Shared Sub SendPasswordToSupplier(ByVal PONo As Integer, Optional ByVal IsTC As Boolean = False)
      Dim oPO As SIS.PAK.pakPO = SIS.PAK.pakPO.pakPOGetByID(PONo)
      Dim oClient As SmtpClient = New SmtpClient("192.9.200.214", 25)
      oClient.Credentials = New Net.NetworkCredential("adskvaultadmin", "isgec@123")
      Dim oMsg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()
      With oMsg
        Try
          Dim aIDs() As String = oPO.FK_PAK_SupplierID.EMailID.Split(";,".ToCharArray)
          For Each tmp As String In aIDs
            .To.Add(New MailAddress(tmp.Trim, tmp.Trim))
          Next

        Catch ex As Exception
          .To.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
        End Try
        .From = New MailAddress("baansupport@isgec.co.in", "BaaN Support")
        .Subject = "Authorization Details to access Purchase Order No.: " & oPO.PONumber
        .IsBodyHtml = True
        Dim oTbl As Table = Nothing
        If Not IsTC Then
          oTbl = GetPOTable(oPO, pakAlertEvents.POVerification, True)
        Else
          oTbl = GetTCPOTable(SIS.PAK.pakTCPO.pakTCPOGetByID(PONo), pakTCAlertEvents.TCPOIssued, True)
        End If
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim writer As HtmlTextWriter = New HtmlTextWriter(sw)
        Try
          oTbl.RenderControl(writer)
        Catch ex As Exception
        End Try
        Dim Header As String = ""
        Header = Header & "<html xmlns=""http://www.w3.org/1999/xhtml"">"
        Header = Header & "<head>"
        Header = Header & "<title></title>"
        Header = Header & "<style>"
        Header = Header & "body{margin: 10px auto auto 60px;}"
        Header = Header & ".tblHd, .tblHd td{font-size: 12px;font-weight: bold;height: 30px !important;background-color:lightgray;}"
        Header = Header & "table{"
        Header = Header & "border: solid 1pt black;"
        Header = Header & "border-collapse:collapse;"
        Header = Header & "font-family: Tahoma;}"

        Header = Header & "td{padding-left: 4px;"
        Header = Header & "border: solid 1pt black;"
        Header = Header & "font-family: Tahoma;"
        Header = Header & "font-size: 9px;"
        Header = Header & "vertical-align:top;}"

        Header = Header & "</style>"
        Header = Header & "</head>"
        Header = Header & "<body>"
        Header = Header & sb.ToString
        Header = Header & "</body></html>"
        .Body = Header
        If Not IsTC Then
          Try
            Dim oAt As New System.Net.Mail.Attachment("~/User_Mannual.pdf")
            oAt.Name = "User Mannual"
            .Attachments.Add(oAt)
          Catch ex As Exception
          End Try
        End If
      End With
      Try
        If Not Convert.ToBoolean(ConfigurationManager.AppSettings("Testing")) Then
          oClient.Send(oMsg)
        End If
      Catch ex As Exception
      End Try
    End Sub
    Public Shared Function Alert(ByVal PONo As Integer, ByVal AlertEvent As pakAlertEvents) As Boolean
      Dim oPO As SIS.PAK.pakPO = Nothing
      oPO = SIS.PAK.pakPO.pakPOGetByID(PONo)
      If oPO.POTypeID = pakErpPOTypes.Package Then
        If AlertEvent = pakAlertEvents.POVerification Then
          SendPasswordToSupplier(PONo)
        End If
      Else
        If AlertEvent = pakAlertEvents.POIssued Then
          SendPasswordToSupplier(PONo)
        End If
      End If

      Dim aErr As New ArrayList
      Dim mRet As String = ""
      Dim oClient As SmtpClient = New SmtpClient("192.9.200.214", 25)
      oClient.Credentials = New Net.NetworkCredential("adskvaultadmin", "isgec@123")
      Dim oMsg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()
      With oMsg
        Select Case AlertEvent
          Case pakAlertEvents.POVerification, pakAlertEvents.POIssued, pakAlertEvents.OpenPORequestExecuted
            'FROM Issuer
            Try
              If oPO.FK_PAK_PO_IssuedBy.EMailID = String.Empty Then
                aErr.Add(oPO.IssuedBy & " " & oPO.FK_PAK_PO_IssuedBy.UserFullName)
                .From = New MailAddress("baansupport@isgec.co.in", "BaaN Support")
                .CC.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              Else
                .From = New MailAddress(oPO.FK_PAK_PO_IssuedBy.EMailID.Trim, oPO.FK_PAK_PO_IssuedBy.UserFullName)
                .CC.Add(New MailAddress(oPO.FK_PAK_PO_IssuedBy.EMailID.Trim, oPO.FK_PAK_PO_IssuedBy.UserFullName))
              End If
              'CC to Buyer
              If oPO.FK_PAK_PO_BuyerID.EMailID = String.Empty Then
                aErr.Add(oPO.BuyerID & " " & oPO.FK_PAK_PO_BuyerID.UserFullName)
                .CC.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              Else
                If Not Convert.ToBoolean(ConfigurationManager.AppSettings("Testing")) Then
                  .CC.Add(New MailAddress(oPO.FK_PAK_PO_BuyerID.EMailID.Trim, oPO.FK_PAK_PO_BuyerID.UserFullName))
                End If
              End If
            Catch ex As Exception
              .From = New MailAddress("baansupport@isgec.co.in", "BaaN Support")
              .CC.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              .CC.Add(New MailAddress("lalit@isgec.co.in", "Lalit Gupta"))
            End Try
            'TO Supplier
            If oPO.FK_PAK_SupplierID.EMailID = String.Empty Then
              aErr.Add(oPO.SupplierID & " " & oPO.FK_PAK_SupplierID.BPName)
              .To.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
            Else
              Dim aIDs() As String = oPO.FK_PAK_SupplierID.EMailID.Split(";,".ToCharArray)
              For Each tmp As String In aIDs
                .To.Add(New MailAddress(tmp.Trim, "<" & tmp.Trim & ">"))
              Next
            End If
            'End of Supplier ID
            If AlertEvent = pakAlertEvents.POIssued Then
              .Subject = "Issued Purchase Order No.: " & oPO.PONumber
            ElseIf AlertEvent = pakAlertEvents.POVerification Then
              .Subject = "Verify Purchase Order No.: " & oPO.PONumber
            Else
              'If oRQ.ExecutedToOpen Then
              '  .Subject = "Re-Opened Purchase Order No.: " & oPO.PONumber
              'Else
              '  .Subject = "Purchase Order Not Opened.: " & oPO.PONumber
              'End If
            End If
          Case pakAlertEvents.POApproval, pakAlertEvents.MaterialDespatched, pakAlertEvents.DocumentsDespatched, pakAlertEvents.POClosed, pakAlertEvents.OpenPORequested
            'From Supplier
            Try
              If oPO.FK_PAK_SupplierID.EMailID = String.Empty Then
                aErr.Add(oPO.SupplierID & " " & oPO.FK_PAK_SupplierID.BPName)
                .From = New MailAddress("baansupport@isgec.co.in", "BaaN Support")
                .CC.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              Else
                Dim aIDs() As String = oPO.FK_PAK_SupplierID.EMailID.Split(",;".ToCharArray)
                Dim First As Boolean = True
                For Each eid As String In aIDs
                  If First Then
                    .From = New MailAddress(eid.Trim, oPO.FK_PAK_SupplierID.BPName)
                    First = False
                  End If
                  .CC.Add(New MailAddress(eid.Trim, eid.Trim))
                Next
              End If
              'End of Supplier ID
            Catch ex As Exception
              .From = New MailAddress("baansupport@isgec.co.in", "BaaN Support")
              .CC.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              .CC.Add(New MailAddress("harishkumar@isgec.co.in", "Harish Kumar"))
            End Try
            'To Issuer
            Try
              If oPO.FK_PAK_PO_IssuedBy.EMailID = String.Empty Then
                aErr.Add(oPO.IssuedBy & " " & oPO.FK_PAK_PO_IssuedBy.UserFullName)
                .To.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              Else
                .To.Add(New MailAddress(oPO.FK_PAK_PO_IssuedBy.EMailID.Trim, oPO.FK_PAK_PO_IssuedBy.UserFullName))
              End If
              'Include Buyer
              If oPO.FK_PAK_PO_BuyerID.EMailID = String.Empty Then
                aErr.Add(oPO.BuyerID & " " & oPO.FK_PAK_PO_BuyerID.UserFullName)
                .CC.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              Else
                If Not Convert.ToBoolean(ConfigurationManager.AppSettings("Testing")) Then
                  .CC.Add(New MailAddress(oPO.FK_PAK_PO_BuyerID.EMailID.Trim, oPO.FK_PAK_PO_BuyerID.UserFullName))
                End If
              End If
            Catch ex As Exception
              .To.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
              .CC.Add(New MailAddress("harishkumar@isgec.co.in", "Harish Kumar"))
            End Try
            'End of Buyer
            If AlertEvent = pakAlertEvents.POClosed Then
              .Subject = "Closed Purchase Order No.: " & oPO.PONumber
            ElseIf AlertEvent = pakAlertEvents.OpenPORequested Then
              .Subject = "Request to Re-Open Purchase Order No.: " & oPO.PONumber
            ElseIf AlertEvent = pakAlertEvents.POApproval Then
              .Subject = "Approve Purchase Order No.: " & oPO.PONumber
            Else
              .Subject = "Material / Document Despatched for Purchase Order No.: " & oPO.PONumber
            End If
        End Select

        'Add Project wise Alert Group in CC
        Dim Users As List(Of SIS.EITL.eitlProjectWiseUser) = SIS.EITL.eitlProjectWiseUser.GetByProjectID(oPO.ProjectID, "")
        For Each usr As SIS.EITL.eitlProjectWiseUser In Users
          Try
            Dim ad As MailAddress = New MailAddress(usr.FK_EITL_ProjectWiseUser_UserID.EMailID.Trim, usr.FK_EITL_ProjectWiseUser_UserID.UserFullName)
            If Not .CC.Contains(ad) Then
              .CC.Add(ad)
            End If
          Catch ex As Exception
            aErr.Add(usr.UserID & " " & usr.FK_EITL_ProjectWiseUser_UserID.UserFullName)
          End Try
        Next
        'End of Add Project Users
      End With
      With oMsg
        If .To.Count <= 0 Then
          .To.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
        End If
        .IsBodyHtml = True
        Dim oTbl As Table = GetPOTable(oPO, AlertEvent)
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim writer As HtmlTextWriter = New HtmlTextWriter(sw)
        Try
          oTbl.RenderControl(writer)
          sb.Append("<br /><br />")
        Catch ex As Exception

        End Try
        If AlertEvent = pakAlertEvents.DocumentsDespatched Or AlertEvent = pakAlertEvents.MaterialDespatched Then
          'oTbl = GetDocumentTable(oPO)
          'Try
          '  oTbl.RenderControl(writer)
          '  sb.Append("<br /><br />")
          'Catch ex As Exception
          'End Try
          oTbl = GetItemTable(oPO)
          Try
            oTbl.RenderControl(writer)
            sb.Append("<br /><br />")
          Catch ex As Exception
          End Try
        End If

        Dim Header As String = ""
        Header = Header & "<html xmlns=""http://www.w3.org/1999/xhtml"">"
        Header = Header & "<head>"
        Header = Header & "<title></title>"
        Header = Header & "<style>"
        Header = Header & "body{margin: 10px auto auto 60px;}"
        Header = Header & ".tblHd, .tblHd td{font-size: 12px;font-weight: bold;height: 30px !important;background-color:lightgray;}"
        Header = Header & "table{"
        Header = Header & "border: solid 1pt black;"
        Header = Header & "border-collapse:collapse;"
        Header = Header & "font-family: Tahoma;}"

        Header = Header & "td{padding-left: 4px;"
        Header = Header & "border: solid 1pt black;"
        Header = Header & "font-family: Tahoma;"
        Header = Header & "font-size: 9px;"
        Header = Header & "vertical-align:top;}"

        Header = Header & "</style>"
        Header = Header & "</head>"
        Header = Header & "<body>"
        If aErr.Count > 0 Then
          Header = Header & "<table>"
          Header = Header & "<tr><td style=""color: red""><i><b>"
          Header = Header & "NOTE: E-Mail Alert could not be delivered to following recipient(s), Please update their E-Mail ID in EITL Application."
          Header = Header & "</b></i></td></tr>"
          For Each Err As String In aErr
            Header = Header & "<tr><td color=""red""><i>"
            Header = Header & Err
            Header = Header & "</i></td></tr>"
          Next
          Header = Header & "</table>"
        End If
        If AlertEvent = pakAlertEvents.OpenPORequested Then
          'Header = Header & "<br /><table>"
          'Header = Header & "<tr><td><b>"
          'Header = Header & "REASON TO RE-OPEN"
          'Header = Header & "</b></td></tr>"
          'Header = Header & "<tr><td>"
          'Header = Header & oRQ.Remarks
          'Header = Header & "</td></tr>"
          'Header = Header & "</table>"
        ElseIf AlertEvent = pakAlertEvents.OpenPORequestExecuted Then
          ''If Not oRQ.ExecutedToOpen Then
          ''  Header = Header & "<br /><table>"
          ''  Header = Header & "<tr><td><b>"
          ''  Header = Header & "REASON TO RE-OPEN"
          ''  Header = Header & "</b></td></tr>"
          ''  Header = Header & "<tr><td>"
          ''  Header = Header & oRQ.ExecuterRemarks
          ''  Header = Header & "</td></tr>"
          ''  Header = Header & "</table>"
          ''End If
        End If
        Header = Header & sb.ToString
        Header = Header & "</body></html>"
        .Body = Header
      End With
      Try
        If Not Convert.ToBoolean(ConfigurationManager.AppSettings("Testing")) Then
          oClient.Send(oMsg)
        End If
      Catch ex As Exception
      End Try
      Return True
    End Function
    Public Shared Function GetItemTable(ByVal oPO As SIS.PAK.pakPO) As Table
      Dim oTbl As New Table
      oTbl.GridLines = GridLines.Both
      oTbl.Width = 900
      oTbl.Style.Add("text-align", "left")
      oTbl.Style.Add("font", "Tahoma")
      oTbl.Style("margin-top") = "10px"

      Dim oCol As TableCell = Nothing
      Dim oRow As TableRow = Nothing

      Dim pakPOBOMLists As List(Of SIS.PAK.pakPOBOM) = SIS.PAK.pakPOBOM.pakPOBOMSelectList(0, 9999, "", False, "", oPO.SerialNo)
      oRow = New TableRow
      oRow.CssClass = "tblHd"
      oCol = New TableCell
      oCol.Text = "Item Code"
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "Description"
      oRow.Cells.Add(oCol)
      'oCol = New TableCell
      'oCol.Text = "UOM"
      'oRow.Cells.Add(oCol)
      'oCol = New TableCell
      'oCol.Text = "Quantity"
      'oRow.Cells.Add(oCol)
      'oCol = New TableCell
      'oCol.Text = "Weight In KG"
      'oRow.Cells.Add(oCol)
      'oCol = New TableCell
      'oCol.Text = "Document ID"
      'oRow.Cells.Add(oCol)
      'oCol = New TableCell
      'oCol.Text = "Despatched"
      'oRow.Cells.Add(oCol)
      'oCol = New TableCell
      'oCol.Text = "Despatch Date"
      'oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      For Each pakpoBom As SIS.PAK.pakPOBOM In pakPOBOMLists
        With pakpoBom
          oRow = New TableRow
          oCol = New TableCell
          oCol.Text = .ItemCode
          oRow.Cells.Add(oCol)
          oCol = New TableCell
          oCol.Text = .ItemDescription
          oRow.Cells.Add(oCol)
          'oCol = New TableCell
          'oCol.Text = oeitlPOItemList.UOM
          'oRow.Cells.Add(oCol)
          'oCol = New TableCell
          'oCol.Text = oeitlPOItemList.Quantity
          'oRow.Cells.Add(oCol)
          'oCol = New TableCell
          'oCol.Text = oeitlPOItemList.WeightInKG
          'oRow.Cells.Add(oCol)
          'oCol = New TableCell
          'oCol.Text = oeitlPOItemList.EITL_PODocumentList3_DocumentID
          'oRow.Cells.Add(oCol)
          'oCol = New TableCell
          'oCol.Text = IIf(oeitlPOItemList.Despatched, "YES", "NO")
          'oRow.Cells.Add(oCol)
          'oCol = New TableCell
          'oCol.Text = oeitlPOItemList.DespatchDate
          'oRow.Cells.Add(oCol)
          oTbl.Rows.Add(oRow)
        End With
      Next

      Return oTbl
    End Function
    'Public Shared Function GetDocumentTable(ByVal opo As SIS.EITL.eitlPOList) As Table
    '  Dim oeitlPODocumentLists As List(Of SIS.EITL.eitlPODocumentList) = SIS.EITL.eitlPODocumentList.eitlPODocumentListSelectList(0, 9999, "", False, "", opo.SerialNo)
    '  Dim oTbl As New Table
    '  oTbl.GridLines = GridLines.Both
    '  oTbl.Width = 900
    '  oTbl.Style.Add("text-align", "left")
    '  oTbl.Style.Add("font", "Tahoma")
    '  oTbl.Style("margin-top") = "10px"

    '  Dim oCol As TableCell = Nothing
    '  Dim oRow As TableRow = Nothing

    '  oRow = New TableRow
    '  oRow.CssClass = "tblHd"
    '  oCol = New TableCell
    '  oCol.Text = "Document ID"
    '  oCol.Width = 300
    '  oRow.Cells.Add(oCol)
    '  oCol = New TableCell
    '  oCol.Text = "Revision No"
    '  oCol.Width = 100
    '  oRow.Cells.Add(oCol)
    '  oCol = New TableCell
    '  oCol.Text = "Description"
    '  oCol.Width = 450
    '  oRow.Cells.Add(oCol)
    '  oCol = New TableCell
    '  oCol.Text = "File Attached"
    '  oCol.Width = 50
    '  oRow.Cells.Add(oCol)
    '  oTbl.Rows.Add(oRow)
    '  For Each oeitlPODocumentList As SIS.EITL.eitlPODocumentList In oeitlPODocumentLists
    '    oRow = New TableRow
    '    oCol = New TableCell
    '    oCol.Text = oeitlPODocumentList.DocumentID
    '    oRow.Cells.Add(oCol)
    '    oCol = New TableCell
    '    oCol.Text = oeitlPODocumentList.RevisionNo
    '    oRow.Cells.Add(oCol)
    '    oCol = New TableCell
    '    oCol.Text = oeitlPODocumentList.Description
    '    oRow.Cells.Add(oCol)
    '    oCol = New TableCell
    '    Dim oFiles As List(Of SIS.EITL.eitlPODocumentFile) = SIS.EITL.eitlPODocumentFile.eitlPODocumentFileSelectList(0, 1, "", False, "", oeitlPODocumentList.SerialNo, oeitlPODocumentList.DocumentLineNo)
    '    If oFiles.Count > 0 Then
    '      If oFiles(0).DiskFile <> String.Empty Then
    '        oCol.Text = "YES"
    '      Else
    '        oCol.Text = "NO"
    '      End If
    '    Else
    '      oCol.Text = "NO"
    '    End If
    '    oRow.Cells.Add(oCol)
    '    oTbl.Rows.Add(oRow)
    '  Next
    '  Return oTbl
    'End Function
    Public Shared Function GetPOTable(ByVal oPO As SIS.PAK.pakPO, ByVal AlertEvent As pakAlertEvents, Optional ByVal IncludeAuthorization As Boolean = False) As Table

      Dim oTbl As New Table
      oTbl.GridLines = GridLines.Both
      oTbl.Width = 900
      oTbl.Style.Add("text-align", "left")
      oTbl.Style.Add("font", "Tahoma")

      Dim oCol As TableCell = Nothing
      Dim oRow As TableRow = Nothing
      '1.
      oRow = New TableRow
      oCol = New TableCell
      oCol.ColumnSpan = "6"
      oCol.Text = "Purchase Order Detail"
      oCol.Style.Add("text-align", "center")
      oCol.Style.Add("border-bottom", "none")
      oCol.Font.Size = "14"
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      '2.
      Select Case AlertEvent
        Case pakAlertEvents.POIssued, pakAlertEvents.POVerification
          oRow = New TableRow
          oCol = New TableCell
          oCol.ColumnSpan = "6"
          oCol.Text = "Dear Supplier, <br /><br /> Purchase Order No.: " & oPO.PONumber & " issued to you from ISGEC, is available online to update Items and Documents information."
          If IncludeAuthorization Then
            oCol.Text &= "<br /><b>URL:</b> http://cloud.isgec.co.in/WebEitl1"
            Dim SupplierLoginID As String = SIS.PAK.pakBPLoginMap.GetLoginID(oPO.SupplierID, HttpContext.Current.Session("FinanceCompany"))
            oCol.Text &= "<br /><b>User ID:</b> " & SupplierLoginID
            oCol.Text &= "<br /><b>Password:</b> " & SIS.QCM.qcmUsers.qcmUsersGetByID(SupplierLoginID).PW
          Else
            oCol.Text &= "<br />Login ID & Password has been sent separately."
          End If
          oCol.Style.Add("text-align", "left")
          oCol.Style.Add("border-bottom", "none")
          oCol.Font.Size = "10"
          oRow.Cells.Add(oCol)
          oTbl.Rows.Add(oRow)
        Case pakAlertEvents.POApproval
          oRow = New TableRow
          oCol = New TableCell
          oCol.ColumnSpan = "6"
          oCol.Text = "<br /><br /> Purchase Order No.: " & oPO.PONumber & " is submitted for BOM creation/verification."
          oCol.Style.Add("text-align", "left")
          oCol.Style.Add("border-bottom", "none")
          oCol.Font.Size = "10"
          oRow.Cells.Add(oCol)
          oTbl.Rows.Add(oRow)
      End Select
      '3.
      oTbl.Width = 900
      oTbl.BorderStyle = BorderStyle.Solid
      oTbl.BorderColor = Drawing.Color.Black
      oTbl.BorderWidth = 2
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = "Serial No"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.SerialNo
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "PO Number"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.PONumber
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = "PO Revision"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.PORevision
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "PO Date"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.PODate
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = "Supplier"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.SupplierID
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.VR_BusinessPartner9_BPName
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "Project"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.ProjectID
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.IDM_Projects4_Description
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = "Division"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.DivisionID
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "Buyer"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.BuyerID
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.aspnet_Users1_UserFullName
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = "POStatus"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.POStatusID
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.PAK_POStatus6_Description
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "Issued By"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.IssuedBy
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.aspnet_Users2_UserFullName
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = "Issued On"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.IssuedOn
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "Closed By"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.ClosedBy
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.aspnet_Users3_UserFullName
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = "Closed On"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.ClosedOn
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      Return oTbl
    End Function
    Public Shared Function GetTCPOTable(ByVal oPO As SIS.PAK.pakTCPO, ByVal AlertEvent As pakTCAlertEvents, Optional ByVal IncludeAuthorization As Boolean = False) As Table

      Dim oTbl As New Table
      oTbl.GridLines = GridLines.Both
      oTbl.Width = 900
      oTbl.Style.Add("text-align", "left")
      oTbl.Style.Add("font", "Tahoma")

      Dim oCol As TableCell = Nothing
      Dim oRow As TableRow = Nothing
      '1.
      oRow = New TableRow
      oCol = New TableCell
      oCol.ColumnSpan = "6"
      oCol.Text = "Purchase Order Detail"
      oCol.Style.Add("text-align", "center")
      oCol.Style.Add("border-bottom", "none")
      oCol.Font.Size = "14"
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      '2.
      Select Case AlertEvent
        Case pakTCAlertEvents.TCPOIssued
          oRow = New TableRow
          oCol = New TableCell
          oCol.ColumnSpan = "6"
          oCol.Text = "Dear Supplier, <br /><br /> Purchase Order No.: " & oPO.PONumber & " issued to you from ISGEC, is available online to submit documents for technical clearance."
          If IncludeAuthorization Then
            oCol.Text &= "<br /><b>URL:</b> http://cloud.isgec.co.in/WebEitl1"
            Dim SupplierLoginID As String = SIS.PAK.pakBPLoginMap.GetLoginID(oPO.SupplierID, HttpContext.Current.Session("FinanceCompany"))
            oCol.Text &= "<br /><b>User ID:</b> " & SupplierLoginID
            oCol.Text &= "<br /><b>Password:</b> " & SIS.QCM.qcmUsers.qcmUsersGetByID(SupplierLoginID).PW
          Else
            oCol.Text &= "<br />Login ID & Password has been sent separately."
          End If
          oCol.Style.Add("text-align", "left")
          oCol.Style.Add("border-bottom", "none")
          oCol.Font.Size = "10"
          oRow.Cells.Add(oCol)
          oTbl.Rows.Add(oRow)
        Case pakTCAlertEvents.DocumentsSubmitted
          oRow = New TableRow
          oCol = New TableCell
          oCol.ColumnSpan = "6"
          oCol.Text = "<br /><br /> Documents submitted by supplier for technical clearance."
          oCol.Style.Add("text-align", "left")
          oCol.Style.Add("border-bottom", "none")
          oCol.Font.Size = "10"
          oRow.Cells.Add(oCol)
          oTbl.Rows.Add(oRow)
      End Select
      '3.
      oTbl.Width = 900
      oTbl.BorderStyle = BorderStyle.Solid
      oTbl.BorderColor = Drawing.Color.Black
      oTbl.BorderWidth = 2
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = "Serial No"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.SerialNo
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "PO Number"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.PONumber
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = "PO Revision"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.PORevision
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "PO Date"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.PODate
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = "Supplier"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.SupplierID
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.VR_BusinessPartner9_BPName
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "Project"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.ProjectID
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.IDM_Projects4_Description
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = "Division"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.DivisionID
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "Buyer"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.BuyerID
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.aspnet_Users1_UserFullName
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = "POStatus"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.POStatusID
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.PAK_POStatus6_Description
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "Issued By"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.IssuedBy
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.aspnet_Users2_UserFullName
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = "Issued On"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.IssuedOn
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "Closed By"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.ClosedBy
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.aspnet_Users3_UserFullName
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      oRow = New TableRow
      oCol = New TableCell
      oCol.Text = "Closed On"
      oCol.Font.Bold = True
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = oPO.ClosedOn
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = ""
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      Return oTbl
    End Function
    Public Shared Function GetTCItemTable(ByVal oPO As SIS.PAK.pakTCPO, Optional ByVal ItemNo As Integer = 0) As Table
      Dim oTbl As New Table
      oTbl.GridLines = GridLines.Both
      oTbl.Width = 900
      oTbl.Style.Add("text-align", "left")
      oTbl.Style.Add("font", "Tahoma")
      oTbl.Style("margin-top") = "10px"

      Dim oCol As TableCell = Nothing
      Dim oRow As TableRow = Nothing

      Dim TCPOL As List(Of SIS.PAK.pakTCPOL) = SIS.PAK.pakTCPOL.pakTCPOLSelectList(0, 9999, "", False, "", oPO.SerialNo)
      oRow = New TableRow
      oRow.CssClass = "tblHd"
      oCol = New TableCell
      oCol.Text = "Item Code"
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "Description"
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "UOM"
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "Quantity"
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "Element"
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      For Each itm As SIS.PAK.pakTCPOL In TCPOL
        If ItemNo <> 0 Then
          If itm.ItemNo <> ItemNo Then
            Continue For
          End If
        End If
        With itm
          oRow = New TableRow
          oCol = New TableCell
          oCol.Text = .ItemCode
          oRow.Cells.Add(oCol)
          oCol = New TableCell
          oCol.Text = .ItemDescription
          oRow.Cells.Add(oCol)
          oCol = New TableCell
          oCol.Text = .ItemUnit
          oRow.Cells.Add(oCol)
          oCol = New TableCell
          oCol.Text = .ItemQuantity
          oRow.Cells.Add(oCol)
          oCol = New TableCell
          oCol.Text = .ItemElement
          oRow.Cells.Add(oCol)
          oTbl.Rows.Add(oRow)
        End With
      Next
      Return oTbl
    End Function
    Public Shared Function GetTCUploadTable(ByVal SerialNo As Integer, ByVal ItemNo As Integer, ByVal UploadNo As Integer) As Table
      Dim tmpDocs As List(Of SIS.PAK.pakSTCPOLRD) = SIS.PAK.pakSTCPOLRD.pakSTCPOLRDSelectList(0, 9999, "", False, "", SerialNo, ItemNo, UploadNo)
      Dim oTbl As New Table
      oTbl.GridLines = GridLines.Both
      oTbl.Width = 900
      oTbl.Style.Add("text-align", "left")
      oTbl.Style.Add("font", "Tahoma")
      oTbl.Style("margin-top") = "10px"

      Dim oCol As TableCell = Nothing
      Dim oRow As TableRow = Nothing

      oRow = New TableRow
      oRow.CssClass = "tblHd"
      oCol = New TableCell
      oCol.Text = "Document ID"
      oCol.Width = 300
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "Revision No"
      oCol.Width = 100
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "Description"
      oCol.Width = 450
      oRow.Cells.Add(oCol)
      oCol = New TableCell
      oCol.Text = "File Attached"
      oCol.Width = 50
      oRow.Cells.Add(oCol)
      oTbl.Rows.Add(oRow)
      For Each tmp As SIS.PAK.pakSTCPOLRD In tmpDocs
        oRow = New TableRow
        oCol = New TableCell
        oCol.Text = tmp.DocumentID
        oRow.Cells.Add(oCol)
        oCol = New TableCell
        oCol.Text = tmp.DocumentRev
        oRow.Cells.Add(oCol)
        oCol = New TableCell
        oCol.Text = tmp.DocumentDescription
        oRow.Cells.Add(oCol)
        oCol = New TableCell
        oRow.Cells.Add(oCol)
        oCol.Text = tmp.FileName
        oTbl.Rows.Add(oRow)
      Next
      Return oTbl
    End Function
    Public Shared Function BomSubmitted(ByVal SerialNo As Integer) As Boolean
      Dim peUsers As New List(Of SIS.PAK.dmisg133)
      Dim oPO As SIS.PAK.pakPO = SIS.PAK.pakPO.pakPOGetByID(SerialNo)
      Dim oBOMs As List(Of SIS.PAK.pakPOBOM) = SIS.PAK.pakPOBOM.pakPOBOMSelectList(0, 9999, "", False, "", SerialNo)
      For Each bom As SIS.PAK.pakPOBOM In oBOMs
        Dim tmpUsers As List(Of SIS.PAK.dmisg133) = SIS.PAK.dmisg133.GetEngFuncUsers(oPO.ProjectID, bom.ItemCode)
        peUsers.AddRange(tmpUsers)
      Next
      Dim peMUser As SIS.PAK.dmisg133 = Nothing
      If peUsers.Count > 0 Then
        peMUser = peUsers(0)
        For Each tmp As SIS.PAK.dmisg133 In peUsers
          If tmp.t_ownr Then
            peMUser = tmp
            Exit For
          End If
        Next
      End If

      Dim aErr As New ArrayList
      Dim mRet As String = ""
      Dim oClient As SmtpClient = New SmtpClient("192.9.200.214", 25)
      oClient.Credentials = New Net.NetworkCredential("adskvaultadmin", "isgec@123")
      Dim oMsg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()
      With oMsg
        'CC to Buyer
        If oPO.FK_PAK_PO_BuyerID.EMailID <> String.Empty Then
          .CC.Add(New MailAddress(oPO.FK_PAK_PO_BuyerID.EMailID.Trim, oPO.FK_PAK_PO_BuyerID.UserFullName))
        End If
        'CC to Issuer
        If oPO.FK_PAK_PO_IssuedBy.EMailID <> String.Empty Then
          .CC.Add(New MailAddress(oPO.FK_PAK_PO_IssuedBy.EMailID.Trim, oPO.FK_PAK_PO_IssuedBy.UserFullName))
        End If
        'Engg Team
        For Each tmp As SIS.PAK.dmisg133 In peUsers
          If tmp.t_logn <> peMUser.t_logn Then
            If tmp.t_mail <> "" Then
              .CC.Add(New MailAddress(tmp.t_mail.Trim, tmp.t_nama))
            End If
          End If
        Next
        'Add Project wise Alert Group in CC
        Dim Users As List(Of SIS.EITL.eitlProjectWiseUser) = SIS.EITL.eitlProjectWiseUser.GetByProjectID(oPO.ProjectID, "")
        For Each usr As SIS.EITL.eitlProjectWiseUser In Users
          Try
            Dim ad As MailAddress = New MailAddress(usr.FK_EITL_ProjectWiseUser_UserID.EMailID.Trim, usr.FK_EITL_ProjectWiseUser_UserID.UserFullName)
            If Not .CC.Contains(ad) Then
              .CC.Add(ad)
            End If
          Catch ex As Exception
          End Try
        Next
        Select Case oPO.POStatusID
          Case pakPOStates.UnderSupplierVerification
            .Subject = "BOM submitted by ISGEC for Order No.: " & oPO.PONumber
            '1. from Isgec
            If peMUser IsNot Nothing Then
              If peMUser.t_mail <> "" Then
                .From = New MailAddress(peMUser.t_mail.Trim, peMUser.t_nama)
              End If
            End If
            '2. TO Supplier
            If oPO.FK_PAK_SupplierID.EMailID <> String.Empty Then
              Dim aIDs() As String = oPO.FK_PAK_SupplierID.EMailID.Split(";,".ToCharArray)
              For Each tmp As String In aIDs
                .To.Add(New MailAddress(tmp.Trim, tmp.Trim))
              Next
            End If
          Case pakPOStates.UnderISGECApproval
            .Subject = "BOM submitted by Supplier for Order No.: " & oPO.PONumber
            '1. From Supplier
            Dim aIDs() As String = oPO.FK_PAK_SupplierID.EMailID.Split(",;".ToCharArray)
            Dim First As Boolean = True
            For Each eid As String In aIDs
              If First Then
                .From = New MailAddress(eid.Trim, oPO.FK_PAK_SupplierID.BPName)
                First = False
              End If
              .CC.Add(New MailAddress(eid.Trim, eid.Trim))
            Next
            '2. To ISGEC Engg
            If peMUser IsNot Nothing Then
              If peMUser.t_mail <> "" Then
                .From = New MailAddress(peMUser.t_mail.Trim, peMUser.t_nama)
              End If
            End If
        End Select
        If .To.Count <= 0 Then
          .To.Add(New MailAddress("baansupport@isgec.co.in", "BaaN Support"))
        End If
        If .From Is Nothing Then
          .From = New MailAddress("baansupport@isgec.co.in", "BaaN Support")
        End If

      End With
      With oMsg
        .IsBodyHtml = True
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim writer As HtmlTextWriter = New HtmlTextWriter(sw)
        Try
          Dim oTbl As Table = GetPOTable(oPO, pakAlertEvents.POApproval)
          oTbl.RenderControl(writer)
          sb.Append("<br /><br />")
          oTbl = GetItemTable(oPO)
          oTbl.RenderControl(writer)
          sb.Append("<br /><br />")
        Catch ex As Exception

        End Try

        Dim Header As String = ""
        Header = Header & "<html xmlns=""http://www.w3.org/1999/xhtml"">"
        Header = Header & "<head>"
        Header = Header & "<title></title>"
        Header = Header & "<style>"
        Header = Header & "body{margin: 10px auto auto 60px;}"
        Header = Header & ".tblHd, .tblHd td{font-size: 12px;font-weight: bold;height: 30px !important;background-color:lightgray;}"
        Header = Header & "table{"
        Header = Header & "border: solid 1pt black;"
        Header = Header & "border-collapse:collapse;"
        Header = Header & "font-family: Tahoma;}"

        Header = Header & "td{padding-left: 4px;"
        Header = Header & "border: solid 1pt black;"
        Header = Header & "font-family: Tahoma;"
        Header = Header & "font-size: 9px;"
        Header = Header & "vertical-align:top;}"

        Header = Header & "</style>"
        Header = Header & "</head>"
        Header = Header & "<body>"
        Header = Header & sb.ToString
        Header = Header & "</body></html>"
        .Body = Header
      End With
      Try
        If Not Convert.ToBoolean(ConfigurationManager.AppSettings("Testing")) Then
          oClient.Send(oMsg)
        End If
      Catch ex As Exception
      End Try
      Return True
    End Function
  End Class

  Public Class dmisg164
    'Item wise engg functions
    Public Property t_eunt As String = ""
    Public Property t_item As String = ""
    Public Property t_sent_1 As Integer = 0
    Public Property t_sent_2 As Integer = 0
    Public Property t_sent_3 As Integer = 0
    Public Property t_sent_4 As Integer = 0
    Public Property t_sent_5 As Integer = 0
    Public Property t_sent_6 As Integer = 0
    Public Property t_sent_7 As Integer = 0
    Public Property t_Refcntd As Integer = 0
    Public Property t_Refcntu As Integer = 0
    Public Property t_ownr As Integer = 0

    Public Shared Function GetEngFunc(cprj As String, item As String) As SIS.PAK.dmisg164
      Dim Comp As String = HttpContext.Current.Session("FinanceCompany")
      Dim Sql As String = ""
      Sql &= " select * from tdmisg164" & Comp & " as dm "
      Sql &= " inner Join ttppdm600" & Comp & " as tp on ltrim(dm.t_eunt) = 'EU'+ltrim(str(tp.t_ncmp)) "
      Sql &= " where LTrim(dm.t_item) ='" & item & "' "
      Sql &= " And tp.t_cprj ='" & cprj & "'"
      Dim Results As SIS.PAK.dmisg164 = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Reader.Read() Then
            Results = New SIS.PAK.dmisg164(Reader)
          End If
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Sub New(rd As SqlDataReader)
      SIS.SYS.SQLDatabase.DBCommon.NewObj(Me, rd)
    End Sub
    Sub New()

    End Sub
  End Class
  Public Class dmisg133
    'Project wise engg function users
    Public Property t_engi As Integer = 0
    Public Property t_logn As String = ""
    Public Property t_cprj As String = ""
    Public Property t_eunt As String = ""
    Public Property t_nama As String = ""
    Public Property t_mail As String = ""
    Public Property t_ownr As Boolean = False

    Public Shared Function GetUsers(cprj As String) As List(Of SIS.PAK.dmisg133)
      Dim Comp As String = HttpContext.Current.Session("FinanceCompany")
      Dim Sql As String = ""
      Sql &= " select "
      Sql &= " dm.t_engi, "
      Sql &= " dm.t_logn, "
      Sql &= " dm.t_cprj, "
      Sql &= " dm.t_eunt, "
      Sql &= " emp.t_nama, "
      Sql &= " bpe.t_mail  "
      Sql &= " from tdmisg133" & Comp & " as dm "
      Sql &= " inner Join ttccom001" & Comp & " as emp on emp.t_emno = dm.t_logn "
      Sql &= " left outer join tbpmdm001" & Comp & " as bpe on emp.t_emno=bpe.t_emno "
      Sql &= " where dm.t_cprj ='" & cprj & "'"
      Dim Results As New List(Of SIS.PAK.dmisg133)
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While Reader.Read()
            Results.Add(New SIS.PAK.dmisg133(Reader))
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function

    Public Shared Function GetEngFuncUsers(cprj As String, item As String) As List(Of SIS.PAK.dmisg133)
      Dim Results As New List(Of SIS.PAK.dmisg133)
      Dim Users As List(Of SIS.PAK.dmisg133) = SIS.PAK.dmisg133.GetUsers(cprj)
      If Users.Count > 0 Then
        Dim ef As SIS.PAK.dmisg164 = SIS.PAK.dmisg164.GetEngFunc(cprj, item)
        If ef IsNot Nothing Then
          For Each usr As SIS.PAK.dmisg133 In Users
            If ef.t_ownr = usr.t_engi Then usr.t_ownr = True
            Select Case usr.t_engi
              Case 1
                If ef.t_sent_1 = 1 Then Results.Add(usr)
              Case 2
                If ef.t_sent_2 = 1 Then Results.Add(usr)
              Case 3
                If ef.t_sent_3 = 1 Then Results.Add(usr)
              Case 4
                If ef.t_sent_4 = 1 Then Results.Add(usr)
              Case 5
                If ef.t_sent_5 = 1 Then Results.Add(usr)
              Case 6
                If ef.t_sent_6 = 1 Then Results.Add(usr)
              Case 7
                If ef.t_sent_7 = 1 Then Results.Add(usr)
            End Select
          Next
        Else
          For Each usr As SIS.PAK.dmisg133 In Users
            usr.t_ownr = True
          Next
          Results = Users
        End If
      End If
      Return Results
    End Function
    Sub New(rd As SqlDataReader)
      SIS.SYS.SQLDatabase.DBCommon.NewObj(Me, rd)
    End Sub
    Sub New()

    End Sub

  End Class
End Namespace
