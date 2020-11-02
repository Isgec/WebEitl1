<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="False" CodeFile="GF_pakIQCListH.aspx.vb" Inherits="GF_pakIQCListH" title="Maintain List: Offered QC List" %>
<asp:Content ID="CPHpakIQCListH" ContentPlaceHolderID="cph1" runat="Server">
  <div class="ui-widget-content">
    <div class="caption">
      <asp:Label ID="LabelpakIQCListH" runat="server" Text="&nbsp;List: Offered QC List"></asp:Label>
    </div>
    <div class="pagedata">
      <asp:UpdatePanel ID="UPNLpakIQCListH" runat="server">
        <ContentTemplate>
          <table>
            <tr>
              <td>
                <asp:Label runat="server" Font-Bold="true" ForeColor="BlueViolet" Style="margin: 10px 10px auto 10px" Text="Upload Quality Cleared File."></asp:Label>
              </td>
            </tr>
            <tr>
              <td style="text-align: center">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                  <ContentTemplate>
                    <asp:HiddenField ID="IsUploaded" runat="server" ClientIDMode="Static"></asp:HiddenField>
                    <asp:FileUpload ID="F_FileUpload" runat="server" Width="250px" ToolTip="Browse QC List" />
                    <asp:Button ID="cmdTmplUpload" Text="Upload" OnClientClick="$get('IsUploaded').value='YES';" runat="server" ToolTip="Click to upload & process template file." CommandName="tmplUpload" />
                  </ContentTemplate>
                  <Triggers>
                    <asp:PostBackTrigger ControlID="cmdTmplUpload" />
                  </Triggers>
                </asp:UpdatePanel>
              </td>
            </tr>
          </table>
          <table width="100%">
            <tr>
              <td class="sis_formview">
                <LGM:ToolBar0
                  ID="TBLpakIQCListH"
                  ToolType="lgNMGrid"
                  EditUrl="~/PAK_Main/App_Edit/EF_pakIQCListH.aspx"
                  EnableAdd="False"
                  ValidationGroup="pakIQCListH"
                  runat="server" />
                <asp:UpdateProgress ID="UPGSpakIQCListH" runat="server" AssociatedUpdatePanelID="UPNLpakIQCListH" DisplayAfter="100">
                  <ProgressTemplate>
                    <span style="color: #ff0033">Loading...</span>
                  </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:Panel ID="pnlH" runat="server" CssClass="cph_filter">
                  <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                    <div style="float: left;">Filter Records </div>
                    <div style="float: left; margin-left: 20px;">
                      <asp:Label ID="lblH" runat="server">(Show Filters...)</asp:Label>
                    </div>
                    <div style="float: right; vertical-align: middle;">
                      <asp:ImageButton ID="imgH" runat="server" ImageUrl="~/images/ua.png" AlternateText="(Show Filters...)" />
                    </div>
                  </div>
                </asp:Panel>
                <asp:Panel ID="pnlD" runat="server" CssClass="cp_filter" Height="0">
                  <table>
                    <tr>
                      <td class="alignright">
                        <b>
                          <asp:Label ID="L_SerialNo" runat="server" Text="Serial No :" /></b>
                      </td>
                      <td>
                        <asp:TextBox
                          ID="F_SerialNo"
                          CssClass="mypktxt"
                          Width="88px"
                          Text=""
                          onfocus="return this.select();"
                          AutoCompleteType="None"
                          onblur="validate_SerialNo(this);"
                          runat="Server" />
                        <asp:Label
                          ID="F_SerialNo_Display"
                          Text=""
                          runat="Server" />
                        <AJX:AutoCompleteExtender
                          ID="ACESerialNo"
                          BehaviorID="B_ACESerialNo"
                          ContextKey=""
                          UseContextKey="true"
                          ServiceMethod="SerialNoCompletionList"
                          TargetControlID="F_SerialNo"
                          CompletionInterval="100"
                          FirstRowSelected="true"
                          MinimumPrefixLength="1"
                          OnClientItemSelected="ACESerialNo_Selected"
                          OnClientPopulating="ACESerialNo_Populating"
                          OnClientPopulated="ACESerialNo_Populated"
                          CompletionSetCount="10"
                          CompletionListCssClass="autocomplete_completionListElement"
                          CompletionListItemCssClass="autocomplete_listItem"
                          CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                          runat="Server" />
                      </td>
                    </tr>
                    <tr>
                      <td class="alignright">
                        <b>
                          <asp:Label ID="L_QCLNo" runat="server" Text="QC List No :" /></b>
                      </td>
                      <td>
                        <asp:TextBox ID="F_QCLNo"
                          Text=""
                          Width="88px"
                          Style="text-align: right"
                          CssClass="mytxt"
                          MaxLength="10"
                          onfocus="return this.select();"
                          runat="server" />
                        <AJX:MaskedEditExtender
                          ID="MEEQCLNo"
                          runat="server"
                          Mask="9999999999"
                          AcceptNegative="Left"
                          MaskType="Number"
                          MessageValidatorTip="true"
                          InputDirection="RightToLeft"
                          ErrorTooltipEnabled="true"
                          TargetControlID="F_QCLNo" />
                        <AJX:MaskedEditValidator
                          ID="MEVQCLNo"
                          runat="server"
                          ControlToValidate="F_QCLNo"
                          ControlExtender="MEEQCLNo"
                          InvalidValueMessage="*"
                          EmptyValueMessage=""
                          EmptyValueBlurredText=""
                          Display="Dynamic"
                          EnableClientScript="true"
                          IsValidEmpty="True"
                          SetFocusOnError="true" />
                      </td>
                    </tr>
                    <tr>
                      <td class="alignright">
                        <b>
                          <asp:Label ID="L_CreatedBy" runat="server" Text="Supplier :" /></b>
                      </td>
                      <td>
                        <asp:TextBox
                          ID="F_CreatedBy"
                          CssClass="myfktxt"
                          Width="72px"
                          Text=""
                          onfocus="return this.select();"
                          AutoCompleteType="None"
                          onblur="validate_CreatedBy(this);"
                          runat="Server" />
                        <asp:Label
                          ID="F_CreatedBy_Display"
                          Text=""
                          runat="Server" />
                        <AJX:AutoCompleteExtender
                          ID="ACECreatedBy"
                          BehaviorID="B_ACECreatedBy"
                          ContextKey=""
                          UseContextKey="true"
                          ServiceMethod="CreatedByCompletionList"
                          TargetControlID="F_CreatedBy"
                          CompletionInterval="100"
                          FirstRowSelected="true"
                          MinimumPrefixLength="1"
                          OnClientItemSelected="ACECreatedBy_Selected"
                          OnClientPopulating="ACECreatedBy_Populating"
                          OnClientPopulated="ACECreatedBy_Populated"
                          CompletionSetCount="10"
                          CompletionListCssClass="autocomplete_completionListElement"
                          CompletionListItemCssClass="autocomplete_listItem"
                          CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                          runat="Server" />
                      </td>
                    </tr>
                    <tr>
                      <td class="alignright">
                        <b>
                          <asp:Label ID="L_StatusID" runat="server" Text="Status :" /></b>
                      </td>
                      <td>
                        <asp:TextBox
                          ID="F_StatusID"
                          CssClass="myfktxt"
                          Width="88px"
                          Text=""
                          onfocus="return this.select();"
                          AutoCompleteType="None"
                          onblur="validate_StatusID(this);"
                          runat="Server" />
                        <asp:Label
                          ID="F_StatusID_Display"
                          Text=""
                          runat="Server" />
                        <AJX:AutoCompleteExtender
                          ID="ACEStatusID"
                          BehaviorID="B_ACEStatusID"
                          ContextKey=""
                          UseContextKey="true"
                          ServiceMethod="StatusIDCompletionList"
                          TargetControlID="F_StatusID"
                          CompletionInterval="100"
                          FirstRowSelected="true"
                          MinimumPrefixLength="1"
                          OnClientItemSelected="ACEStatusID_Selected"
                          OnClientPopulating="ACEStatusID_Populating"
                          OnClientPopulated="ACEStatusID_Populated"
                          CompletionSetCount="10"
                          CompletionListCssClass="autocomplete_completionListElement"
                          CompletionListItemCssClass="autocomplete_listItem"
                          CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                          runat="Server" />
                      </td>
                    </tr>
                    <tr>
                      <td class="alignright">
                        <b>
                          <asp:Label ID="L_ClearedBy" runat="server" Text="Inspected By :" /></b>
                      </td>
                      <td>
                        <asp:TextBox
                          ID="F_ClearedBy"
                          CssClass="myfktxt"
                          Width="72px"
                          Text=""
                          onfocus="return this.select();"
                          AutoCompleteType="None"
                          onblur="validate_ClearedBy(this);"
                          runat="Server" />
                        <asp:Label
                          ID="F_ClearedBy_Display"
                          Text=""
                          runat="Server" />
                        <AJX:AutoCompleteExtender
                          ID="ACEClearedBy"
                          BehaviorID="B_ACEClearedBy"
                          ContextKey=""
                          UseContextKey="true"
                          ServiceMethod="ClearedByCompletionList"
                          TargetControlID="F_ClearedBy"
                          CompletionInterval="100"
                          FirstRowSelected="true"
                          MinimumPrefixLength="1"
                          OnClientItemSelected="ACEClearedBy_Selected"
                          OnClientPopulating="ACEClearedBy_Populating"
                          OnClientPopulated="ACEClearedBy_Populated"
                          CompletionSetCount="10"
                          CompletionListCssClass="autocomplete_completionListElement"
                          CompletionListItemCssClass="autocomplete_listItem"
                          CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                          runat="Server" />
                      </td>
                    </tr>
                  </table>
                </asp:Panel>
                <AJX:CollapsiblePanelExtender ID="cpe1" runat="Server" TargetControlID="pnlD" ExpandControlID="pnlH" CollapseControlID="pnlH" Collapsed="True" TextLabelID="lblH" ImageControlID="imgH" ExpandedText="(Hide Filters...)" CollapsedText="(Show Filters...)" ExpandedImage="~/images/ua.png" CollapsedImage="~/images/da.png" SuppressPostBack="true" />
                <script type="text/javascript">
                  var pcnt = 0;
                  function print_report(o) {
                    pcnt = pcnt + 1;
                    var nam = 'wTask' + pcnt;
                    var url = self.location.href.replace('App_Forms/GF_', 'App_Print/RP_');
                    url = url + '?pk=' + o.alt;
                    window.open(url, nam, 'left=20,top=20,width=1000,height=600,toolbar=1,resizable=1,scrollbars=1');
                    return false;
                  }
                </script>
                <style type="text/css">
                  .lg_tb_n {
                    border: 1pt solid silver;
                    background-color: gainsboro;
                    color: gray;
                  }

                  lg_tb_n:hover {
                    background-color: white;
                    color: black;
                  }

                  .lg_pb_w,
                  .lg_pb_p {
                    border-radius: 5px;
                    padding: 2px;
                    color: darkgray;
                    background-color: gainsboro;
                    border: 1pt solid silver;
                    width: 100%;
                  }

                    .lg_pb_w:hover {
                      color: white;
                      background-color: crimson;
                    }

                    .lg_pb_p:hover {
                      color: white;
                      background-color: royalblue;
                    }
                </style>
                <style>
                  .nt-modal-container {
                    display: none;
                    position: fixed;
                    z-index: 9000;
                    left: 0;
                    top: 0;
                    width: 100%;
                    height: 100%;
                    overflow: hidden;
                    background-color: rgb(0,0,0);
                    background-color: rgba(0,0,0,0.4);
                  }

                  .nt-newNote {
                    border: 1pt solid #b7b5b5;
                    border-radius: 6px;
                    background-color: #dddbdb;
                    margin: 10% auto;
                    padding: 10px;
                    width: 80%;
                  }

                  .nt-input-box {
                    width: 100%;
                    border: 1pt solid gray;
                    border-radius: 4px;
                    font-family: 'Courier New';
                    font-size: 12px;
                  }

                  .nt-newNote-but {
                    display: flex;
                    flex-direction: row;
                    justify-content: flex-end;
                    flex-wrap: wrap;
                    margin: 10px;
                  }

                  .nt-but-danger {
                    border-radius: 4px;
                    border: 1pt solid #ff0000;
                    background-color: crimson;
                    color: white;
                    font-family: 'Courier New';
                    font-size: 14px;
                  }

                    .nt-but-danger:hover {
                      border-radius: 4px;
                      border: 1pt solid #ff0000;
                      background-color: #fa7d7d;
                      color: white;
                    }

                  .nt-but-primary {
                    border-radius: 4px;
                    border: 1pt solid #1f336d;
                    background-color: #2f5fe9;
                    color: white;
                    font-family: 'Courier New';
                    font-size: 14px;
                  }

                    .nt-but-primary:hover {
                      border-radius: 4px;
                      border: 1pt solid #1f336d;
                      background-color: #698bed;
                      color: white;
                    }

                  .nt-but-success {
                    border-radius: 4px;
                    border: 1pt solid #049317;
                    background-color: #06bf1e;
                    color: white;
                    font-family: 'Courier New';
                    font-size: 14px;
                  }

                    .nt-but-success:hover {
                      border-radius: 4px;
                      background-color: #05fa25;
                      color: black;
                    }

                  .nt-err-msg {
                    color: red;
                    font-weight: bold;
                    font-family: Tahoma;
                    font-size: 12px;
                  }
                </style>
                <script>
                  function $get(str) {
                    return document.getElementById(str);
                  }
                  var qci_script = {
                    newShown: false,
                    show_qci: function (o) {
                      $get('newNote').style.display = 'block';
                      return false;
                    },
                    hide_qci: function () {
                      $get('newNote').style.display = 'none';
                      return false;
                    },

                    add_err: function (x) {
                      var de = $get('divErr');
                      de.innerHTML = de.innerHTML + '<li class="nt-err-msg">' + x + '</li>';
                    },
                    clear_err: function () {
                      var de = $get('divErr');
                      de.innerHTML = '';
                      de.style.display = 'none';
                    },
                    show_err: function () {
                      var de = $get('divErr');
                      de.style.display = 'block';
                    },
                    validate_qci: function () {
                      this.clear_err();
                      var err = false;
                      var mainItem = $get('F_MainItem');
                      if (mainItem.value == '') {
                        this.add_err('Main Item is required.');
                        err = true;
                      }
                      if (err) {
                        this.show_err();
                        return false;
                      }
                      return true;
                    },
                    submit_qci: function () {
                      return this.validate_qci();
                    }
                  }
                </script>



                <asp:GridView ID="GVpakIQCListH" SkinID="gv_silver" runat="server" DataSourceID="ODSpakIQCListH" DataKeyNames="SerialNo,QCLNo">
                  <Columns>
                    <asp:TemplateField HeaderText="EDIT">
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdEditPage" ValidationGroup="Edit" runat="server" Visible='<%# EVal("Visible") %>' Enabled='<%# EVal("Enable") %>' AlternateText="Edit" ToolTip="Edit the record." SkinID="Edit" CommandName="lgEdit" CommandArgument='<%# Container.DataItemIndex %>' />
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Get Tmpl.">
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdDownload" runat="server" Visible='<%# EVal("Visible") %>' Enabled='<%# EVal("Enable") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Download QC List Offered." SkinID="download" OnClientClick='<%# Eval("GetPrintLink") %>' />
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PRN">
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdPrintPage" runat="server" Visible='<%# EVal("Visible") %>' Enabled='<%# EVal("Enable") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Print the record." SkinID="Print" OnClientClick="return print_report(this);" />
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NT">
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdNotes" runat="server" AlternateText='<%# Eval("PrimaryKey") %>' ToolTip="View/reply Notes" SkinID="notes" OnClientClick='<%# Eval("GetNotesLink") %>' CommandArgument='<%# Container.DataItemIndex %>' />
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OFR DOC">
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdShowDoc" runat="server" AlternateText='<%# Eval("PrimaryKey") %>' ToolTip="Show Attached offer documents." SkinID="attach" OnClientClick='<%# Eval("ShowOfferAttachLink") %>' />
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PO Number" SortExpression="PAK_PO2_PODescription">
                      <ItemTemplate>
                        <asp:Label ID="L_SerialNo" runat="server" ForeColor='<%# EVal("ForeColor") %>' Title='<%# EVal("SerialNo") %>' Text='<%# Eval("PAK_PO2_PODescription") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Offer List No" SortExpression="QCLNo">
                      <ItemTemplate>
                        <asp:Label ID="LabelQCLNo" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("QCLNo") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle CssClass="alignCenter" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM" SortExpression="PAK_Units4_Description">
                      <ItemTemplate>
                        <asp:Label ID="L_UOMTotalWeight" runat="server" ForeColor='<%# EVal("ForeColor") %>' Title='<%# EVal("UOMTotalWeight") %>' Text='<%# Eval("PAK_Units4_Description") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ofrd.Wt" SortExpression="TotalWeight">
                      <ItemTemplate>
                        <asp:Label ID="LabelTotalWeight" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text='<%# Bind("TotalWeight") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle CssClass="alignCenter" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ofrd.Qty">
                      <ItemTemplate>
                        <asp:Label ID="LabelOfferedQty" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text='<%# EVal("SumOfQty") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle CssClass="alignCenter" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supplier" SortExpression="aspnet_users1_UserFullName">
                      <ItemTemplate>
                        <asp:Label ID="L_CreatedBy" runat="server" ForeColor='<%# EVal("ForeColor") %>' Title='<%# EVal("CreatedBy") %>' Text='<%# Eval("aspnet_users1_UserFullName") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle Width="200px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ofrd.On" SortExpression="CreatedOn">
                      <ItemTemplate>
                        <asp:Label ID="LabelCreatedOn" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text='<%# Bind("CreatedOn") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle CssClass="alignCenter" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" SortExpression="PAK_QCListStatus3_Description">
                      <ItemTemplate>
                        <asp:Label ID="L_StatusID" runat="server" ForeColor='<%# EVal("ForeColor") %>' Title='<%# EVal("StatusID") %>' Text='<%# Eval("PAK_QCListStatus3_Description") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Alloted Request">
                      <ItemTemplate>
                        <LGM:LC_qcmSRequests
                          ID="F_QCRequestNo"
                          SelectedValue='<%# Bind("QCRequestNo") %>'
                          OrderBy='<%# Eval("PrimaryKey") %>'
                          DataTextField="DisplayField"
                          DataValueField="PrimaryKey"
                          IncludeDefault="true"
                          DefaultText="-- Select --"
                          Width="100px"
                          CssClass="myddl"
                          runat="Server" />
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ins. Done">
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdApproveWF" ValidationGroup='<%# "Approve" & Container.DataItemIndex %>' CausesValidation="true" runat="server" Visible='<%# EVal("ApproveWFVisible") %>' Enabled='<%# EVal("ApproveWFEnable") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="After uploading Cleared Quantity List, click to complete." SkinID="approve" OnClientClick='<%# "return Page_ClientValidate(""Approve" & Container.DataItemIndex & """) && confirm(""Update and close cleared quantity ?"");" %>' CommandName="ApproveWF" CommandArgument='<%# Container.DataItemIndex %>' />
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ACTION">
                      <ItemTemplate>
                        <%--       Enabled='<%# Eval("EnableInput") %>'--%>
                        <div style="display: flex; flex-direction: column;">
                          <div runat="server" visible='<%# Eval("RejectWFVisible") %>'>
                            <div style="display:flex; flex-direction: row;">
                              <div>
                                <asp:TextBox ID="F_ReturnRemarks"
                                  Text='<%# Bind("Remarks") %>'
                                  Width="100px"
                                  CssClass="lg_tb_n"
                                  onfocus="return this.select();"
                                  ValidationGroup='<%# "Reject" & Container.DataItemIndex %>'
                                  onblur="this.value=this.value.replace(/\'/g,'');"
                                  ToolTip="Remarks is mandatory if returning."
                                  MaxLength="500"
                                  placeholder="Reason for returning"
                                  runat="server" />
                                <asp:RequiredFieldValidator
                                  ID="RFVReturnRemarks"
                                  runat="server"
                                  ControlToValidate="F_ReturnRemarks"
                                  Text="Return Remarks is required."
                                  ErrorMessage="[Required!]"
                                  Display="Dynamic"
                                  EnableClientScript="true"
                                  ValidationGroup='<%# "Reject" & Container.DataItemIndex %>'
                                  SetFocusOnError="true" />
                              </div>
                              <div style="padding-left: 2px;">
                                <asp:Button ID="cmdRejectWF" CssClass="lg_pb_w" Text="Retn" ValidationGroup='<%# "Reject" & Container.DataItemIndex %>' CausesValidation="true" runat="server" Visible='<%# EVal("RejectWFVisible") %>' Enabled='<%# EVal("RejectWFEnable") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Return" OnClientClick='<%# "return Page_ClientValidate(""Reject" & Container.DataItemIndex & """) && confirm(""Return record ?"");" %>' CommandName="RejectWF" CommandArgument='<%# Container.DataItemIndex %>' />
                              </div>
                            </div>
                          </div>
                          <div runat="server" visible='<%# Eval("MICNVisible") %>'>
                          <div style="display: flex; flex-direction: row;">
                            <div>
                              <asp:Button ID="cmdMicn" CssClass="lg_pb_p" Width="107px" Text="MICN Form" runat="server" ToolTip="Open MICN Form" CommandName="cmdMicn" CommandArgument='<%# Container.DataItemIndex %>' />
                            </div>
                            <div style="padding-left: 2px;">
                              <asp:Button ID="prnMicn" CssClass="lg_pb_p" Text="Print" runat="server" OnClientClick='<%# Eval("GetPrintQCILink") %>' ToolTip="Print MICN" CommandName="prnMicn" CommandArgument='<%# Container.DataItemIndex %>' />
                            </div>
                          </div>
                          <div style="display: flex; flex-direction: row;">
                            <div>
                              <asp:Button ID="cmdIW" CssClass="lg_pb_p" Width="107px" Text="IW Form" runat="server" AlternateText='<%# Eval("PrimaryKey") %>' ToolTip="Open Inspection Waiver Form" CommandName="cmdIW" CommandArgument='<%# Container.DataItemIndex %>'></asp:Button>
                            </div>
                            <div style="padding-left: 2px;">
                              <asp:Button ID="PrnIW" CssClass="lg_pb_p" Text="Print" runat="server" OnClientClick='<%# Eval("GetPrintQCILink") %>' ToolTip="Print IW" CommandName="prnIW" CommandArgument='<%# Container.DataItemIndex %>' />
                            </div>
                          </div>
                          </div>
                        </div>
                      </ItemTemplate>
                      <HeaderStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MICN DOC">
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdAttach" runat="server" AlternateText='<%# Eval("PrimaryKey") %>' ToolTip="Attach MICN/IW documents." SkinID="attach" OnClientClick='<%# Eval("GetMICNIWAttachLink") %>' />
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CLOSE">
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdClose" runat="server" Visible='<%# Eval("MICNVisible") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Close Offered list." SkinID="hold" OnClientClick="return confirm('Close offer list ? MICN or IW cannot be issued once closed.');" CommandName="closeQC" CommandArgument='<%# Container.DataItemIndex %>' />
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle CssClass="alignCenter" Width="40px" />
                    </asp:TemplateField>
                  </Columns>
                  <EmptyDataTemplate>
                    <asp:Label ID="LabelEmpty" runat="server" Font-Size="Small" ForeColor="Red" Text="No record found !!!"></asp:Label>
                  </EmptyDataTemplate>
                </asp:GridView>
                <asp:ObjectDataSource
                  ID="ODSpakIQCListH"
                  runat="server"
                  DataObjectTypeName="SIS.PAK.pakIQCListH"
                  OldValuesParameterFormatString="original_{0}"
                  SelectMethod="UZ_pakIQCListHSelectList"
                  TypeName="SIS.PAK.pakIQCListH"
                  SelectCountMethod="pakIQCListHSelectCount"
                  SortParameterName="OrderBy" EnablePaging="True">
                  <SelectParameters>
                    <asp:ControlParameter ControlID="F_SerialNo" PropertyName="Text" Name="SerialNo" Type="Int32" Size="10" />
                    <asp:ControlParameter ControlID="F_QCLNo" PropertyName="Text" Name="QCLNo" Type="Int32" Size="10" />
                    <asp:ControlParameter ControlID="F_CreatedBy" PropertyName="Text" Name="CreatedBy" Type="String" Size="8" />
                    <asp:ControlParameter ControlID="F_StatusID" PropertyName="Text" Name="StatusID" Type="Int32" Size="10" />
                    <asp:ControlParameter ControlID="F_ClearedBy" PropertyName="Text" Name="ClearedBy" Type="String" Size="8" />
                    <asp:Parameter Name="SearchState" Type="Boolean" Direction="Input" DefaultValue="false" />
                    <asp:Parameter Name="SearchText" Type="String" Direction="Input" DefaultValue="" />
                  </SelectParameters>
                </asp:ObjectDataSource>
                <br />
              </td>
            </tr>
          </table>
          <%--MICN Form--%>
          <div id="newNote" class="nt-modal-container">
            <div class="nt-newNote">
              <div id="qcHeader" runat="server" clientidmode="Static" style="font-size: 14px; font-weight: bold; margin: 6px;">
                MICN
              </div>
              <div id="divErr" runat="server" clientidmode="Static" style="display: none; padding: 10px; text-align: center;">
              </div>
              <div style="padding-right: 2px;">
                <asp:TextBox ID="F_SubSupplier" runat="server" ClientIDMode="Static" CssClass="nt-input-box" Style="min-height: 50px; resize: vertical;" placeholder="Sub Supplier" TextMode="MultiLine"></asp:TextBox>
              </div>
              <div style="padding-right: 2px;">
                <asp:TextBox ID="F_RefReportNo" runat="server" ClientIDMode="Static" CssClass="nt-input-box" Style="min-height: 50px; resize: vertical;" placeholder="Reference Report No." TextMode="MultiLine"></asp:TextBox>
              </div>
              <div style="padding-right: 2px;">
                <asp:TextBox ID="F_MainItem" runat="server" ClientIDMode="Static" CssClass="nt-input-box" Style="min-height: 50px; resize: vertical;" placeholder="Main Item" TextMode="MultiLine"></asp:TextBox>
              </div>
              <div style="padding-right: 2px;">
                <asp:TextBox ID="F_ComplianceReportNo" runat="server" ClientIDMode="Static" CssClass="nt-input-box" Style="min-height: 50px; resize: vertical;" placeholder="Compliance Report No." TextMode="MultiLine"></asp:TextBox>
              </div>
              <div class="nt-newNote-but">
                <div style="margin: 5px;">
                  <asp:Button ID="cmdClose" runat="server" ClientIDMode="Static" CssClass="nt-but-danger" Text="Close" OnClientClick="return qci_script.hide_qci(this);" />
                </div>
                <div style="margin: 5px;">
                  <asp:Button ID="cmdSubmit" runat="server" ClientIDMode="Static" CssClass="nt-but-success" Text="Submit" OnClientClick="return qci_script.submit_qci();" />
                </div>
              </div>
            </div>
          </div>

        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="GVpakIQCListH" EventName="PageIndexChanged" />
          <asp:AsyncPostBackTrigger ControlID="F_SerialNo" />
          <asp:AsyncPostBackTrigger ControlID="F_QCLNo" />
          <asp:AsyncPostBackTrigger ControlID="F_CreatedBy" />
          <asp:AsyncPostBackTrigger ControlID="F_StatusID" />
          <asp:AsyncPostBackTrigger ControlID="F_ClearedBy" />
        </Triggers>
      </asp:UpdatePanel>
    </div>
  </div>


</asp:Content>
