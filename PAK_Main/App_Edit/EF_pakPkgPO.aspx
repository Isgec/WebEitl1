<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="EF_pakPkgPO.aspx.vb" Inherits="EF_pakPkgPO" title="Edit: Create Packing List & Despatch" %>
<asp:Content ID="CPHpakPkgPO" ContentPlaceHolderID="cph1" Runat="Server">
<div id="div1" class="ui-widget-content page">
<div id="div2" class="caption">
    <asp:Label ID="LabelpakPkgPO" runat="server" Text="&nbsp;Edit: Create Packing List & Despatch"></asp:Label>
</div>
<div id="div3" class="pagedata">
<asp:UpdatePanel ID="UPNLpakPkgPO" runat="server" >
<ContentTemplate>
  <LGM:ToolBar0 
    ID = "TBLpakPkgPO"
    ToolType = "lgNMEdit"
    UpdateAndStay = "False"
    EnableDelete = "False"
    ValidationGroup = "pakPkgPO"
    runat = "server" />
<asp:FormView ID="FVpakPkgPO"
  runat = "server"
  DataKeyNames = "SerialNo"
  DataSourceID = "ODSpakPkgPO"
  DefaultMode = "Edit" CssClass="sis_formview">
  <EditItemTemplate>
    <div id="frmdiv" class="ui-widget-content minipage">
    <table style="margin:auto;border: solid 1pt lightgrey">
      <tr>
        <td class="alignright">
          <b><asp:Label ID="L_SerialNo" runat="server" ForeColor="#CC6633" Text="Serial No :" /><span style="color:red">*</span></b>
        </td>
        <td colspan="3">
          <asp:TextBox ID="F_SerialNo"
            Text='<%# Bind("SerialNo") %>'
            ToolTip="Value of Serial No."
            Enabled = "False"
            CssClass = "mypktxt"
            Width="88px"
            style="text-align: right"
            runat="server" />
        </td>
      </tr>
      <tr><td colspan="4" style="border-top: solid 1pt LightGrey" ></td></tr>
      <tr>
        <td class="alignright">
          <asp:Label ID="L_PONumber" runat="server" Text="PO Number :" />&nbsp;
        </td>
        <td>
          <asp:TextBox ID="F_PONumber"
            Text='<%# Bind("PONumber") %>'
            ToolTip="Value of PO Number."
            Enabled = "False"
            Width="88px"
            CssClass = "dmytxt"
            runat="server" />
        </td>
        <td class="alignright">
          <asp:Label ID="L_PORevision" runat="server" Text="PO Revision :" />&nbsp;
        </td>
        <td>
          <asp:TextBox ID="F_PORevision"
            Text='<%# Bind("PORevision") %>'
            ToolTip="Value of PO Revision."
            Enabled = "False"
            Width="88px"
            CssClass = "dmytxt"
            runat="server" />
        </td>
      </tr>
      <tr>
        <td class="alignright">
          <asp:Label ID="L_PODate" runat="server" Text="PO Date :" />&nbsp;
        </td>
        <td>
          <asp:TextBox ID="F_PODate"
            Text='<%# Bind("PODate") %>'
            ToolTip="Value of PO Date."
            Enabled = "False"
            Width="168px"
            CssClass = "dmytxt"
            runat="server" />
        </td>
        <td class="alignright">
          <asp:Label ID="L_POStatusID" runat="server" Text="Status :" />&nbsp;
        </td>
        <td>
          <asp:TextBox
            ID = "F_POStatusID"
            Width="88px"
            Text='<%# Bind("POStatusID") %>'
            Enabled = "False"
            ToolTip="Value of Status."
            CssClass = "dmyfktxt"
            Runat="Server" />
          <asp:Label
            ID = "F_POStatusID_Display"
            Text='<%# Eval("PAK_POStatus6_Description") %>'
            CssClass="myLbl"
            Runat="Server" />
        </td>
      </tr>
      <tr><td colspan="4" style="border-top: solid 1pt LightGrey" ></td></tr>
      <tr>
        <td class="alignright">
          <asp:Label ID="L_PODescription" runat="server" Text="PO Description :" />&nbsp;
        </td>
        <td colspan="3">
          <asp:TextBox ID="F_PODescription"
            Text='<%# Bind("PODescription") %>'
            ToolTip="Value of PO Description."
            Enabled = "False"
            Width="350px"
            CssClass = "dmytxt"
            runat="server" />
        </td>
      </tr>
      <tr><td colspan="4" style="border-top: solid 1pt LightGrey" ></td></tr>
      <tr>
        <td class="alignright">
          <asp:Label ID="L_SupplierID" runat="server" Text="Supplier :" />&nbsp;
        </td>
        <td>
          <asp:TextBox
            ID = "F_SupplierID"
            Width="80px"
            Text='<%# Bind("SupplierID") %>'
            Enabled = "False"
            ToolTip="Value of Supplier."
            CssClass = "dmyfktxt"
            Runat="Server" />
          <asp:Label
            ID = "F_SupplierID_Display"
            Text='<%# Eval("VR_BusinessPartner9_BPName") %>'
            CssClass="myLbl"
            Runat="Server" />
        </td>
        <td class="alignright">
          <asp:Label ID="L_ProjectID" runat="server" Text="Project :" />&nbsp;
        </td>
        <td>
          <asp:TextBox
            ID = "F_ProjectID"
            Width="56px"
            Text='<%# Bind("ProjectID") %>'
            Enabled = "False"
            ToolTip="Value of Project."
            CssClass = "dmyfktxt"
            Runat="Server" />
          <asp:Label
            ID = "F_ProjectID_Display"
            Text='<%# Eval("IDM_Projects4_Description") %>'
            CssClass="myLbl"
            Runat="Server" />
        </td>
      </tr>
      <tr>
        <td class="alignright">
          <asp:Label ID="L_BuyerID" runat="server" Text="Buyer :" />&nbsp;
        </td>
        <td>
          <asp:TextBox
            ID = "F_BuyerID"
            Width="72px"
            Text='<%# Bind("BuyerID") %>'
            Enabled = "False"
            ToolTip="Value of Buyer."
            CssClass = "dmyfktxt"
            Runat="Server" />
          <asp:Label
            ID = "F_BuyerID_Display"
            Text='<%# Eval("aspnet_users1_UserFullName") %>'
            CssClass="myLbl"
            Runat="Server" />
        </td>
      <td></td><td></td></tr>
      <tr><td colspan="4" style="border-top: solid 1pt LightGrey" ></td></tr>
      <tr>
        <td class="alignright">
          <asp:Label ID="L_IssuedBy" runat="server" Text="Issued By :" />&nbsp;
        </td>
        <td>
          <asp:TextBox
            ID = "F_IssuedBy"
            Width="72px"
            Text='<%# Bind("IssuedBy") %>'
            Enabled = "False"
            ToolTip="Value of Issued By."
            CssClass = "dmyfktxt"
            Runat="Server" />
          <asp:Label
            ID = "F_IssuedBy_Display"
            Text='<%# Eval("aspnet_users2_UserFullName") %>'
            CssClass="myLbl"
            Runat="Server" />
        </td>
        <td class="alignright">
          <asp:Label ID="L_IssuedOn" runat="server" Text="Issued On :" />&nbsp;
        </td>
        <td>
          <asp:TextBox ID="F_IssuedOn"
            Text='<%# Bind("IssuedOn") %>'
            ToolTip="Value of Issued On."
            Enabled = "False"
            Width="168px"
            CssClass = "dmytxt"
            runat="server" />
        </td>
      </tr>
      <tr>
        <td class="alignright">
          <asp:Label ID="L_ClosedBy" runat="server" Text="Closed By :" />&nbsp;
        </td>
        <td>
          <asp:TextBox
            ID = "F_ClosedBy"
            Width="72px"
            Text='<%# Bind("ClosedBy") %>'
            Enabled = "False"
            ToolTip="Value of Closed By."
            CssClass = "dmyfktxt"
            Runat="Server" />
          <asp:Label
            ID = "F_ClosedBy_Display"
            Text='<%# Eval("aspnet_users3_UserFullName") %>'
            CssClass="myLbl"
            Runat="Server" />
        </td>
        <td class="alignright">
          <asp:Label ID="L_ClosedOn" runat="server" Text="Closed On :" />&nbsp;
        </td>
        <td>
          <asp:TextBox ID="F_ClosedOn"
            Text='<%# Bind("ClosedOn") %>'
            ToolTip="Value of Closed On."
            Enabled = "False"
            Width="168px"
            CssClass = "dmytxt"
            runat="server" />
        </td>
      </tr>
      <tr><td colspan="4" style="border-top: solid 1pt LightGrey" ></td></tr>
    </table>
  </div>
<fieldset class="ui-widget-content page">
<legend>
    <asp:Label ID="LabelpakPkgListH" runat="server" Text="&nbsp;List: Packing List"></asp:Label>
</legend>
<div class="pagedata">
<asp:UpdatePanel ID="UPNLpakPkgListH" runat="server">
  <ContentTemplate>
    <table>
      <tr>
        <td>
          <asp:Label runat="server" Font-Bold="true" ForeColor="BlueViolet" style="margin: 10px 10px auto 10px" Text="Upload Packing List template file." ></asp:Label>
        </td>
      </tr>
      <tr>
        <td style="text-align:center">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              <asp:HiddenField ID="IsUploaded" runat="server" ClientIDMode="Static" ></asp:HiddenField>
              <asp:FileUpload ID="F_FileUpload" runat="server" Width="250px" ToolTip="Browse Packing List Template" />
              <asp:Button ID="cmdTmplUpload" Text="Upload" OnClientClick="$get('IsUploaded').value='YES';" runat="server" ToolTip="Click to upload & process template file." CommandName="tmplUpload" CommandArgument='<%# Eval("PrimaryKey") %>' />
            </ContentTemplate>
            <Triggers>
              <asp:PostBackTrigger ControlID="cmdTmplUpload" />
            </Triggers>
          </asp:UpdatePanel>
        </td>
      </tr>
    </table>
    <script type="text/javascript">
      var pcnt = 0;
      function print_report(o) {
        pcnt = pcnt + 1;
        var nam = 'wTask' + pcnt;
        var url = self.location.href.replace('App_Edit/EF_','App_Print/RP_');
        url = url + '&pkg=' + o.alt;
        window.open(url, nam, 'left=20,top=20,width=110,height=60,toolbar=1,resizable=1,scrollbars=1');
        return false;
      }
    </script>
    <table width="100%"><tr><td class="sis_formview"> 
    <LGM:ToolBar0 
      ID = "TBLpakPkgListH"
      ToolType = "lgNMGrid"
      EditUrl = "~/PAK_Main/App_Edit/EF_pakPkgListH.aspx"
      AddUrl = "~/PAK_Main/App_Edit/EF_pakPkgPO.aspx"
      AddPostBack = "True"
      EnableExit = "false"
      ValidationGroup = "pakPkgListH"
      runat = "server" />
    <asp:UpdateProgress ID="UPGSpakPkgListH" runat="server" AssociatedUpdatePanelID="UPNLpakPkgListH" DisplayAfter="100">
      <ProgressTemplate>
        <span style="color: #ff0033">Loading...</span>
      </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:GridView ID="GVpakPkgListH" SkinID="gv_silver" runat="server" DataSourceID="ODSpakPkgListH" DataKeyNames="SerialNo,PkgNo">
      <Columns>
        <asp:TemplateField HeaderText="EDIT">
          <ItemTemplate>
            <asp:ImageButton ID="cmdEditPage" ValidationGroup="Edit" runat="server" Visible='<%# Eval("Visible") %>' Enabled='<%# EVal("Enable") %>' AlternateText="Edit" ToolTip="Edit the record." SkinID="Edit" CommandName="lgEdit" CommandArgument='<%# Container.DataItemIndex %>' />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Get Tmpl.">
          <ItemTemplate>
            <asp:ImageButton ID="cmdDownload" runat="server" Visible='<%# EVal("Visible") %>' Enabled='<%# EVal("Enable") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Download Template File." SkinID="download" OnClientClick='<%# Eval("GetDownloadLink") %>' />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="PRINT">
          <ItemTemplate>
            <asp:ImageButton ID="cmdPrintPage" runat="server" Visible='<%# EVal("Visible") %>' Enabled='<%# EVal("Enable") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Print the record." SkinID="Print" OnClientClick="return print_report(this);" />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pkg. No" SortExpression="PkgNo">
          <ItemTemplate>
            <asp:Label ID="LabelPkgNo" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("PkgNo") %>'></asp:Label>
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="40px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Supplier Ref. No" SortExpression="SupplierRefNo">
          <ItemTemplate>
            <asp:Label ID="LabelSupplierRefNo" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("SupplierRefNo") %>'></asp:Label>
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="100px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total Weight" SortExpression="TotalWeight">
          <ItemTemplate>
            <asp:Label ID="LabelTotalWeight" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("TotalWeight") %>'></asp:Label>
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="80px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Transporter Name" SortExpression="TransporterName">
          <ItemTemplate>
            <asp:Label ID="LabelTransporterName" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("TransporterName") %>'></asp:Label>
          </ItemTemplate>
          <ItemStyle CssClass="" />
        <HeaderStyle CssClass="" Width="200px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Created On" SortExpression="CreatedOn">
          <ItemTemplate>
            <asp:Label ID="LabelCreatedOn" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("CreatedOn") %>'></asp:Label>
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="90px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status" SortExpression="PAK_PkgStatus6_Description">
          <ItemTemplate>
             <asp:Label ID="L_StatusID" runat="server" ForeColor='<%# EVal("ForeColor") %>' Title='<%# EVal("StatusID") %>' Text='<%# Eval("PAK_PkgStatus6_Description") %>'></asp:Label>
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="100px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="RAISE VEHICLE REQUEST">
          <ItemTemplate>
            <asp:ImageButton ID="cmdVRRequest" ValidationGroup="VRRequest" runat="server" Visible='<%# Eval("VRVisible") %>' AlternateText="VRRequest" ToolTip="Raise Vehicle Request." SkinID="Vehicle" OnClientClick="return confirm('Raise Vehicle Request, Packing list can not be modified once Vehicle Request is Raised.');" CommandName="VRRequestWF" CommandArgument='<%# Container.DataItemIndex %>' />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Despatch Details">
          <ItemTemplate>
            <asp:ImageButton ID="cmdDespatchDetails" runat="server" Visible='<%# Eval("InitiateWFVisible") %>' Enabled='<%# EVal("InitiateWFEnable") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Enter Despatch Details" SkinID="upddet" CommandName="DespatchDetails" CommandArgument='<%# Container.DataItemIndex %>' />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="DESPATCH">
          <ItemTemplate>
            <asp:ImageButton ID="cmdInitiateWF" ValidationGroup='<%# "Initiate" & Container.DataItemIndex %>' CausesValidation="true" runat="server" Visible='<%# EVal("InitiateWFVisible") %>' Enabled='<%# EVal("InitiateWFEnable") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Despatch" SkinID="forward" OnClientClick='<%# "return Page_ClientValidate(""Initiate" & Container.DataItemIndex & """) && confirm(""Forward record ?"");" %>' CommandName="InitiateWF" CommandArgument='<%# Container.DataItemIndex %>' />
            <asp:CheckBox ID="F_UnProtected" runat="server" Visible='<%# Eval("RejectWFVisible") %>' ToolTip="Check to Un Protected [No Check for RECEIPT in ERP] Return to Vendor" />
            <asp:ImageButton ID="cmdRejectWF" ValidationGroup='<%# "Reject" & Container.DataItemIndex %>' CausesValidation="true" runat="server" Visible='<%# EVal("GetBackVisible") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Return to Vendor" SkinID="reject" OnClientClick='<%# "return Page_ClientValidate(""Reject" & Container.DataItemIndex & """) && confirm(""Do you want to get packing list back ?"");" %>' CommandName="RejectWF" CommandArgument='<%# Container.DataItemIndex %>' />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
      </Columns>
      <EmptyDataTemplate>
        <asp:Label ID="LabelEmpty" runat="server" Font-Size="Small" ForeColor="Red" Text="No record found !!!"></asp:Label>
      </EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource 
      ID = "ODSpakPkgListH"
      runat = "server"
      DataObjectTypeName = "SIS.PAK.pakPkgListH"
      OldValuesParameterFormatString = "original_{0}"
      SelectMethod = "UZ_pakPkgListHSelectList"
      TypeName = "SIS.PAK.pakPkgListH"
      SelectCountMethod = "pakPkgListHSelectCount"
      SortParameterName="OrderBy" EnablePaging="True">
      <SelectParameters >
        <asp:ControlParameter ControlID="F_SerialNo" PropertyName="Text" Name="SerialNo" Type="Int32" Size="10" />
        <asp:Parameter Name="SearchState" Type="Boolean" Direction="Input" DefaultValue="false" />
        <asp:Parameter Name="SearchText" Type="String" Direction="Input" DefaultValue="" />
      </SelectParameters>
    </asp:ObjectDataSource>
    <br />
  </td></tr></table>
  </ContentTemplate>
  <Triggers>
    <asp:AsyncPostBackTrigger ControlID="GVpakPkgListH" EventName="PageIndexChanged" />
  </Triggers>
</asp:UpdatePanel>
</div>
</fieldset>
  </EditItemTemplate>
</asp:FormView>
  </ContentTemplate>
</asp:UpdatePanel>
<asp:ObjectDataSource 
  ID = "ODSpakPkgPO"
  DataObjectTypeName = "SIS.PAK.pakPkgPO"
  SelectMethod = "pakPkgPOGetByID"
  UpdateMethod="UZ_pakPkgPOUpdate"
  OldValuesParameterFormatString = "original_{0}"
  TypeName = "SIS.PAK.pakPkgPO"
  runat = "server" >
<SelectParameters>
  <asp:QueryStringParameter DefaultValue="0" QueryStringField="SerialNo" Name="SerialNo" Type="Int32" />
</SelectParameters>
</asp:ObjectDataSource>
</div>
</div>
  <script src="AF_pakPkgListH.js"></script>
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <asp:Panel ID="pnl1" runat="server" Style="background-color: white; display: none; height:350px;border-radius:6px;" Width='800px'>
        <asp:Panel ID="pnlHeader" runat="server" Style="width: 100%; height: 33px; padding-top: 8px; text-align: center; border-bottom: 1pt solid lightgray;">
          <asp:Label ID="HeaderText" runat="server" Font-Size="16px" Font-Bold="true" Text='My Modal Text'></asp:Label>
        </asp:Panel>
        <asp:Panel ID="modalContent" runat="server" Style="width: 100%; height: 250px; padding: 4px;">
          <table style="margin:auto;border: solid 1pt lightgrey">
            <tr>
              <td class="alignright">
                <b><asp:Label ID="L_SerialNo" ForeColor="#CC6633" runat="server" Text="Serial No :" /><span style="color:red">*</span></b>
              </td>
              <td colspan="3">
                <asp:TextBox
                  ID = "F_SerialNo"
                  CssClass = "mypktxt"
                  Width="88px"
                  Text='<%# Bind("SerialNo") %>'
                  AutoCompleteType = "None"
                  onfocus = "return this.select();"
                  ToolTip="Enter value for Serial No."
                  ValidationGroup = "pakPkgListH"
                  onblur= "script_pakPkgListH.validate_SerialNo(this);"
                  Runat="Server" />
                <asp:Label
                  ID = "F_SerialNo_Display"
                  Text='<%# Eval("PAK_PO2_PODescription") %>'
                  CssClass="myLbl"
                  Runat="Server" />
              </td>
            </tr>
            <tr>
              <td class="alignright">
                <asp:Label ID="L_SupplierRefNo" runat="server" Text="Supplier Ref. No :" />&nbsp;
              </td>
              <td>
                <asp:TextBox ID="F_SupplierRefNo"
                  Text='<%# Bind("SupplierRefNo") %>'
                  CssClass = "mytxt"
                  onfocus = "return this.select();"
                  onblur= "this.value=this.value.replace(/\'/g,'');"
                  ToolTip="Enter value for Supplier Ref. No."
                  MaxLength="50"
                  Width="300px"
                  runat="server" />
              </td>
              <td class="alignright">
                <asp:Label ID="L_PortID" runat="server" Text="Destination Port :" />
              </td>
              <td>
                <LGM:LC_elogPorts
                  ID="F_PortID"
                  SelectedValue='<%# Bind("PortID") %>'
                  OrderBy="DisplayField"
                  DataTextField="DisplayField"
                  DataValueField="PrimaryKey"
                  IncludeDefault="true"
                  DefaultText="-- Select --"
                  Width="200px"
                  CssClass="myddl"
                  Runat="Server" />
                </td>
            </tr>
            <tr>
              <td class="alignright">
                <asp:Label ID="L_VRExecutionNo" runat="server" Text="Vehicle Execution No. :" />&nbsp;
              </td>
              <td colspan="3">
                <asp:TextBox
                  ID = "F_VRExecutionNo"
                  CssClass = "myfktxt"
                  Width="88px"
                  Text='<%# Bind("VRExecutionNo") %>'
                  AutoCompleteType = "None"
                  onfocus = "return this.select();"
                  ToolTip="Enter value for Vehicle Execution No.."
                  onblur= "script_pakPkgListH.validate_VRExecutionNo(this);"
                  Runat="Server" />
                <asp:Label
                  ID = "F_VRExecutionNo_Display"
                  Text='<%# Eval("VR_RequestExecution5_ExecutionDescription") %>'
                  CssClass="myLbl"
                  Runat="Server" />
                <AJX:AutoCompleteExtender
                  ID="ACEVRExecutionNo"
                  BehaviorID="B_ACEVRExecutionNo"
                  ContextKey=""
                  UseContextKey="true"
                  ServiceMethod="VRExecutionNoCompletionList"
                  TargetControlID="F_VRExecutionNo"
                  EnableCaching="false"
                  CompletionInterval="100"
                  FirstRowSelected="true"
                  MinimumPrefixLength="1"
                  OnClientItemSelected="script_pakPkgListH.ACEVRExecutionNo_Selected"
                  OnClientPopulating="script_pakPkgListH.ACEVRExecutionNo_Populating"
                  OnClientPopulated="script_pakPkgListH.ACEVRExecutionNo_Populated"
                  CompletionSetCount="10"
                  CompletionListCssClass = "autocomplete_completionListElement"
                  CompletionListItemCssClass = "autocomplete_listItem"
                  CompletionListHighlightedItemCssClass = "autocomplete_highlightedListItem"
                  Runat="Server" />
              </td>
            </tr>
            <tr>
              <td class="alignright">
                <asp:Label ID="L_TransporterID" runat="server" Text="Transporter :" />&nbsp;
              </td>
              <td colspan="3">
                <asp:TextBox
                  ID = "F_TransporterID"
                  CssClass = "myfktxt"
                  Width="80px"
                  Text='<%# Bind("TransporterID") %>'
                  AutoCompleteType = "None"
                  onfocus = "return this.select();"
                  ToolTip="Enter value for Transporter."
                  onblur= "script_pakPkgListH.validate_TransporterID(this);"
                  Runat="Server" />
                <asp:Label
                  ID = "F_TransporterID_Display"
                  Text='<%# Eval("VR_BusinessPartner4_BPName") %>'
                  CssClass="myLbl"
                  Runat="Server" />
                <AJX:AutoCompleteExtender
                  ID="ACETransporterID"
                  BehaviorID="B_ACETransporterID"
                  ContextKey=""
                  UseContextKey="true"
                  ServiceMethod="TransporterIDCompletionList"
                  TargetControlID="F_TransporterID"
                  EnableCaching="false"
                  CompletionInterval="100"
                  FirstRowSelected="true"
                  MinimumPrefixLength="1"
                  OnClientItemSelected="script_pakPkgListH.ACETransporterID_Selected"
                  OnClientPopulating="script_pakPkgListH.ACETransporterID_Populating"
                  OnClientPopulated="script_pakPkgListH.ACETransporterID_Populated"
                  CompletionSetCount="10"
                  CompletionListCssClass = "autocomplete_completionListElement"
                  CompletionListItemCssClass = "autocomplete_listItem"
                  CompletionListHighlightedItemCssClass = "autocomplete_highlightedListItem"
                  Runat="Server" />
              </td>
            </tr>
            <tr>
              <td class="alignright">
                <asp:Label ID="L_TransporterName" runat="server" Text="Transporter Name :" />&nbsp;
              </td>
              <td colspan="3">
                <asp:TextBox ID="F_TransporterName"
                  Text='<%# Bind("TransporterName") %>'
                  CssClass = "mytxt"
                  onfocus = "return this.select();"
                  onblur= "this.value=this.value.replace(/\'/g,'');"
                  ToolTip="Enter value for Transporter Name."
                  MaxLength="50"
                  Width="408px"
                  runat="server" />
              </td>
            </tr>
            <tr>
              <td class="alignright">
                <asp:Label ID="L_VehicleNo" runat="server" Text="Vehicle No :" /><span style="color:red">*</span>
              </td>
              <td colspan="3">
                <asp:TextBox ID="F_VehicleNo"
                  Text='<%# Bind("VehicleNo") %>'
                  CssClass = "mytxt"
                  onfocus = "return this.select();"
                  ValidationGroup="pakPkgListH"
                  onblur= "this.value=this.value.replace(/\'/g,'');"
                  ToolTip="Enter value for Vehicle No."
                  MaxLength="50"
                  Width="300px"
                  runat="server" />
                <asp:RequiredFieldValidator 
                  ID = "RFVVehicleNo"
                  runat = "server"
                  ControlToValidate = "F_VehicleNo"
                  ErrorMessage = "<div class='errorLG'>Required!</div>"
                  Display = "Dynamic"
                  EnableClientScript = "true"
                  ValidationGroup = "pakPkgListH"
                  SetFocusOnError="true" />
              </td>
            </tr>
            <tr>
              <td class="alignright">
                <asp:Label ID="L_GRNo" runat="server" Text="GR No :" /><span style="color:red">*</span>
              </td>
              <td>
                <asp:TextBox ID="F_GRNo"
                  Text='<%# Bind("GRNo") %>'
                  CssClass = "mytxt"
                  onfocus = "return this.select();"
                  ValidationGroup="pakPkgListH"
                  onblur= "this.value=this.value.replace(/\'/g,'');"
                  ToolTip="Enter value for GR No."
                  MaxLength="50"
                  Width="300px"
                  runat="server" />
                <asp:RequiredFieldValidator 
                  ID = "RFVGRNo"
                  runat = "server"
                  ControlToValidate = "F_GRNo"
                  ErrorMessage = "<div class='errorLG'>Required!</div>"
                  Display = "Dynamic"
                  EnableClientScript = "true"
                  ValidationGroup = "pakPkgListH"
                  SetFocusOnError="true" />
              </td>
              <td class="alignright">
                <asp:Label ID="L_GRDate" runat="server" Text="GR Date :" /><span style="color:red">*</span>
              </td>
              <td>
                <asp:TextBox ID="F_GRDate"
                  Text='<%# Bind("GRDate") %>'
                  Width="80px"
                  CssClass = "mytxt"
                  ValidationGroup="pakPkgListH"
                  onfocus = "return this.select();"
                  runat="server" />
                <asp:Image ID="ImageButtonGRDate" runat="server" ToolTip="Click to open calendar" style="cursor: pointer; vertical-align:bottom" ImageUrl="~/Images/cal.png" />
                <AJX:CalendarExtender 
                  ID = "CEGRDate"
                  TargetControlID="F_GRDate"
                  Format="dd/MM/yyyy"
                  runat = "server" CssClass="MyCalendar" PopupButtonID="ImageButtonGRDate" />
                <AJX:MaskedEditExtender 
                  ID = "MEEGRDate"
                  runat = "server"
                  mask = "99/99/9999"
                  MaskType="Date"
                  CultureName = "en-GB"
                  MessageValidatorTip="true"
                  InputDirection="LeftToRight"
                  ErrorTooltipEnabled="true"
                  TargetControlID="F_GRDate" />
                <AJX:MaskedEditValidator 
                  ID = "MEVGRDate"
                  runat = "server"
                  ControlToValidate = "F_GRDate"
                  ControlExtender = "MEEGRDate"
                  EmptyValueBlurredText = "<div class='errorLG'>Required!</div>"
                  Display = "Dynamic"
                  EnableClientScript = "true"
                  ValidationGroup = "pakPkgListH"
                  IsValidEmpty = "false"
                  SetFocusOnError="true" />
              </td>
            </tr>
            <tr><td colspan="4" style="border-top: solid 1pt LightGrey" ></td></tr>
            <tr>
              <td class="alignright">
                <asp:Label ID="L_SupplierBillNo" runat="server" Text="Supplier Bill No :" />
              </td>
              <td>
                <asp:TextBox ID="F_SupplierBillNo"
                  Text='<%# Bind("SupplierBillNo") %>'
                  CssClass = "mytxt"
                  onfocus = "return this.select();"
                  ValidationGroup="pakPkgListH"
                  onblur= "this.value=this.value.replace(/\'/g,'');"
                  ToolTip="Enter Supplier Bill No."
                  MaxLength="50"
                  Width="300px"
                  runat="server" />
              </td>
              <td class="alignright">
                <asp:Label ID="L_SupplierBillDate" runat="server" Text="Supplier Bill Date :" />
              </td>
              <td>
                <asp:TextBox ID="F_SupplierBillDate"
                  Text='<%# Bind("SupplierBillDate") %>'
                  Width="80px"
                  CssClass = "mytxt"
                  ValidationGroup="pakPkgListH"
                  onfocus = "return this.select();"
                  runat="server" />
                <asp:Image ID="ImageButtonSupplierBillDate" runat="server" ToolTip="Click to open calendar" style="cursor: pointer; vertical-align:bottom" ImageUrl="~/Images/cal.png" />
                <AJX:CalendarExtender 
                  ID = "CESupplierBillDate"
                  TargetControlID="F_SupplierBillDate"
                  Format="dd/MM/yyyy"
                  runat = "server" CssClass="MyCalendar" PopupButtonID="ImageButtonSupplierBillDate" />
                <AJX:MaskedEditExtender 
                  ID = "MEESupplierBillDate"
                  runat = "server"
                  mask = "99/99/9999"
                  MaskType="Date"
                  CultureName = "en-GB"
                  MessageValidatorTip="true"
                  InputDirection="LeftToRight"
                  ErrorTooltipEnabled="true"
                  TargetControlID="F_SupplierBillDate" />
                <AJX:MaskedEditValidator 
                  ID = "MEVSupplierBillDate"
                  runat = "server"
                  ControlToValidate = "F_SupplierBillDate"
                  ControlExtender = "MEESupplierBillDate"
                  EmptyValueBlurredText = "<div class='errorLG'>Required!</div>"
                  Display = "Dynamic"
                  EnableClientScript = "true"
                  ValidationGroup = "pakPkgListH"
                  IsValidEmpty = "true"
                  SetFocusOnError="true" />
              </td>
            </tr>
            <tr>
              <td class="alignright">
                <asp:Label ID="L_SupplierBillAmount" runat="server" Text="Supplier Bill Amount :" />&nbsp;
              </td>
              <td>
                <asp:TextBox ID="F_SupplierBillAmount"
                  Text='<%# Bind("SupplierBillAmount") %>'
                  style="text-align: right"
                  Width="168px"
                  CssClass = "mytxt"
                  MaxLength="20"
                  onfocus = "return this.select();"
                  onblur="return dc(this,2);"
                  runat="server" />
              </td>
              <td class="alignright">
              </td>
              <td>
            </tr>

            <tr><td colspan="4" style="border-top: solid 1pt LightGrey" ></td></tr>
            <tr>
              <td class="alignright">
                <asp:Label ID="L_Remarks" runat="server" Text="Remarks :" />&nbsp;
              </td>
              <td colspan="3">
                <asp:TextBox ID="F_Remarks"
                  Text='<%# Bind("Remarks") %>'
                  CssClass = "mytxt"
                  onfocus = "return this.select();"
                  onblur= "this.value=this.value.replace(/\'/g,'');"
                  ToolTip="Enter value for Remarks."
                  MaxLength="500"
                  Width="300px"
                  runat="server" />
              </td>
            </tr>
          </table>
        </asp:Panel>
        <asp:Panel ID="pnlFooter" runat="server" Style="width: 100%; height: 33px; padding-top: 8px; text-align: right; border-top: 1pt solid lightgray;">
          <asp:Label ID="L_PrimaryKey" runat="server" Style="display: none;"></asp:Label>
          <asp:Button ID="cmdOK" runat="server" Width="70px" Text="OK" Style="text-align: center; margin-right: 30px;" />
          <asp:Button ID="cmdCancel" runat="server" Width="70px" Text="Cancle" Style="text-align: center; margin-right: 30px;" />
        </asp:Panel>
      </asp:Panel>
      <asp:Button ID="dummy" runat="server" Style="display: none;" Text="show"></asp:Button>
      <AJX:ModalPopupExtender
        ID="mPopup"
        BehaviorID="myMPE1"
        TargetControlID="dummy"
        BackgroundCssClass="modalBackground"
        CancelControlID="cmdCancel"
        OkControlID="cmdCancel"
        PopupControlID="pnl1"
        PopupDragHandleControlID="pnlHeader"
        DropShadow="true"
        runat="server">
      </AJX:ModalPopupExtender>
    </ContentTemplate>
    <Triggers>
      <asp:AsyncPostBackTrigger ControlID="cmdOK" EventName="Click" />
    </Triggers>
  </asp:UpdatePanel>

</asp:Content>
