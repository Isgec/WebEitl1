<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="False" CodeFile="GF_pakPO.aspx.vb" Inherits="GF_pakPO" title="Maintain List: Purchase Order" %>
<asp:Content ID="CPHpakPO" ContentPlaceHolderID="cph1" runat="Server">
  <div class="ui-widget-content page">
    <div class="caption">
      <asp:Label ID="LabelpakPO" runat="server" Text="&nbsp;List: Purchase Order"></asp:Label>
    </div>
    <div class="pagedata">
      <asp:UpdatePanel ID="UPNLpakPO" runat="server">
        <ContentTemplate>
          <table width="100%">
            <tr>
              <td class="sis_formview">
                <LGM:ToolBar0
                  ID="TBLpakPO"
                  ToolType="lgNMGrid"
                  EditUrl="~/PAK_Main/App_Edit/EF_pakPO.aspx"
                  EnableAdd="false"
                  ValidationGroup="pakPO"
                  runat="server" />
                <asp:UpdateProgress ID="UPGSpakPO" runat="server" AssociatedUpdatePanelID="UPNLpakPO" DisplayAfter="100">
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
                  <div id="frmdiv" style="width: 30%; float: left" class="ui-widget-content ui-corner-all">
                    <h3 class="ui-widget-header ui-corner-all" style="margin: 0; padding: 0.4em; text-align: center;">FILTER RECORDS</h3>
                    <table>
                      <tr>
                        <td class="alignright">
                          <b>
                            <asp:Label ID="L_SupplierID" runat="server" Text="Supplier :" /></b>
                        </td>
                        <td>
                          <asp:TextBox
                            ID="F_SupplierID"
                            CssClass="myfktxt"
                            Width="80px"
                            Text=""
                            onfocus="return this.select();"
                            AutoCompleteType="None"
                            onblur="validate_SupplierID(this);"
                            runat="Server" />
                          <asp:Label
                            ID="F_SupplierID_Display"
                            Text=""
                            runat="Server" />
                          <AJX:AutoCompleteExtender
                            ID="ACESupplierID"
                            BehaviorID="B_ACESupplierID"
                            ContextKey=""
                            UseContextKey="true"
                            ServiceMethod="SupplierIDCompletionList"
                            TargetControlID="F_SupplierID"
                            CompletionInterval="100"
                            FirstRowSelected="true"
                            MinimumPrefixLength="1"
                            OnClientItemSelected="ACESupplierID_Selected"
                            OnClientPopulating="ACESupplierID_Populating"
                            OnClientPopulated="ACESupplierID_Populated"
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
                            <asp:Label ID="L_ProjectID" runat="server" Text="Project :" /></b>
                        </td>
                        <td>
                          <asp:TextBox
                            ID="F_ProjectID"
                            CssClass="myfktxt"
                            Width="56px"
                            Text=""
                            onfocus="return this.select();"
                            AutoCompleteType="None"
                            onblur="validate_ProjectID(this);"
                            runat="Server" />
                          <asp:Label
                            ID="F_ProjectID_Display"
                            Text=""
                            runat="Server" />
                          <AJX:AutoCompleteExtender
                            ID="ACEProjectID"
                            BehaviorID="B_ACEProjectID"
                            ContextKey=""
                            UseContextKey="true"
                            ServiceMethod="ProjectIDCompletionList"
                            TargetControlID="F_ProjectID"
                            CompletionInterval="100"
                            FirstRowSelected="true"
                            MinimumPrefixLength="1"
                            OnClientItemSelected="ACEProjectID_Selected"
                            OnClientPopulating="ACEProjectID_Populating"
                            OnClientPopulated="ACEProjectID_Populated"
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
                            <asp:Label ID="L_POStatusID" runat="server" Text="Status :" /></b>
                        </td>
                        <td>
                          <LGM:LC_pakPOStatus
                            ID="F_POStatusID"
                            SelectedValue=""
                            OrderBy="Description"
                            DataTextField="Description"
                            DataValueField="StatusID"
                            IncludeDefault="true"
                            DefaultText="-- Select --"
                            Width="200px"
                            AutoPostBack="true"
                            RequiredFieldErrorMessage="<div class='errorLG'>Required!</div>"
                            CssClass="myddl"
                            runat="Server" />
                        </td>
                      </tr>
                      <tr>
                        <td class="alignright">
                          <b>
                            <asp:Label ID="L_IssuedBy" runat="server" Text="Issued By :" /></b>
                        </td>
                        <td>
                          <asp:TextBox
                            ID="F_IssuedBy"
                            CssClass="myfktxt"
                            Width="72px"
                            Text=""
                            onfocus="return this.select();"
                            AutoCompleteType="None"
                            onblur="validate_IssuedBy(this);"
                            runat="Server" />
                          <asp:Label
                            ID="F_IssuedBy_Display"
                            Text=""
                            runat="Server" />
                          <AJX:AutoCompleteExtender
                            ID="ACEIssuedBy"
                            BehaviorID="B_ACEIssuedBy"
                            ContextKey=""
                            UseContextKey="true"
                            ServiceMethod="IssuedByCompletionList"
                            TargetControlID="F_IssuedBy"
                            CompletionInterval="100"
                            FirstRowSelected="true"
                            MinimumPrefixLength="1"
                            OnClientItemSelected="ACEIssuedBy_Selected"
                            OnClientPopulating="ACEIssuedBy_Populating"
                            OnClientPopulated="ACEIssuedBy_Populated"
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
                            <asp:Label ID="L_POTypeID" runat="server" Text="PO Type :" /></b>
                        </td>
                        <td>
                          <LGM:LC_pakPOTypes
                            ID="F_POTypeID"
                            SelectedValue=""
                            OrderBy="Description"
                            DataTextField="Description"
                            DataValueField="POTypeID"
                            IncludeDefault="true"
                            DefaultText="-- Select --"
                            Width="200px"
                            AutoPostBack="true"
                            RequiredFieldErrorMessage="<div class='errorLG'>Required!</div>"
                            CssClass="myddl"
                            runat="Server" />
                        </td>
                      </tr>
                      <tr>
                        <td class="alignright">
                          <b>
                            <asp:Label ID="L_BuyerID" runat="server" Text="Buyer :" /></b>
                        </td>
                        <td>
                          <asp:TextBox
                            ID="F_BuyerID"
                            CssClass="myfktxt"
                            Width="72px"
                            Text=""
                            onfocus="return this.select();"
                            AutoCompleteType="None"
                            onblur="validate_BuyerID(this);"
                            runat="Server" />
                          <asp:Label
                            ID="F_BuyerID_Display"
                            Text=""
                            runat="Server" />
                          <AJX:AutoCompleteExtender
                            ID="ACEBuyerID"
                            BehaviorID="B_ACEBuyerID"
                            ContextKey=""
                            UseContextKey="true"
                            ServiceMethod="BuyerIDCompletionList"
                            TargetControlID="F_BuyerID"
                            CompletionInterval="100"
                            FirstRowSelected="true"
                            MinimumPrefixLength="1"
                            OnClientItemSelected="ACEBuyerID_Selected"
                            OnClientPopulating="ACEBuyerID_Populating"
                            OnClientPopulated="ACEBuyerID_Populated"
                            CompletionSetCount="10"
                            CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            runat="Server" />
                        </td>
                      </tr>
                    </table>
                  </div>
                  <div style="width: 60%; float: right" class="ui-widget-content ui-corner-all">
                    <h3 class="ui-widget-header ui-corner-all" style="margin: 0; padding: 0.4em; text-align: center;">IMPORT PO DETAILS FROM ERP</h3>
                    <table style="margin: 10px 10px 10px 10px">
                      <tr>
                        <td class="alignright">
                          <b>
                            <asp:Label ID="Label1" runat="server" Text="PO Number :" /></b>
                        </td>
                        <td style="padding-left: 10px">
                          <asp:TextBox
                            ID="F_PONumber"
                            CssClass="myfktxt"
                            Width="110px"
                            Text=""
                            MaxLength="9"
                            ToolTip="Enter PO Number to import"
                            onfocus="return this.select();"
                            AutoCompleteType="None"
                            runat="Server" />
                        </td>
                        <td style="padding-left: 10px">
                          <asp:Button ID="cmdBoughtout" runat="server" Text="BOUGHTOUT [STD]" ToolTip="BOM is defined by ISGEC and available in PO" OnClientClick="return confirm('Import PO as Boughtout [STD], BOM is available ERP-PO ?');" />
                        </td>
                        <td style="padding-left: 10px">
                          <asp:Button ID="cmdImport" runat="server" Text="BOUGHTOUT [PACKAGE]" ToolTip="BOM will be provided by Supplier and freezed by ISGEC" OnClientClick="return confirm('Import PO as Boughtout [Package], BOM will be provided by supplier and freezed by ISGEC ?');" />
                        </td>
                        <td style="padding-left: 10px">
                          <asp:Button ID="cmdIsgec" runat="server" Text="ISGEC Engineered" ToolTip="BOM is available in PO and auto updated when drawing is revised" OnClientClick="return confirm('Import PO as Isgec Engineered, BOM is available in ERP-PO ?');" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="5">
                          <asp:CheckBox ID="chkRevertConfig" runat="server" Text="USE Package Master" />
                        </td>
                      </tr>
                    </table>
                  </div>
                </asp:Panel>
                <AJX:CollapsiblePanelExtender ID="cpe1" runat="Server" TargetControlID="pnlD" ExpandControlID="pnlH" CollapseControlID="pnlH" Collapsed="True" TextLabelID="lblH" ImageControlID="imgH" ExpandedText="(Hide Filters...)" CollapsedText="(Show Filters...)" ExpandedImage="~/images/ua.png" CollapsedImage="~/images/da.png" SuppressPostBack="true" />
                <asp:GridView ID="GVpakPO" SkinID="gv_silver" runat="server" DataSourceID="ODSpakPO" DataKeyNames="SerialNo">
                  <Columns>
                    <asp:TemplateField HeaderText="EDIT">
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdEditPage" ValidationGroup="Edit" runat="server" Visible='<%# EVal("Visible") %>' Enabled='<%# EVal("Enable") %>' AlternateText="Edit" ToolTip="Edit the record." SkinID="Edit" CommandName="lgEdit" CommandArgument='<%# Container.DataItemIndex %>' />
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PRN">
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdPrint" runat="server" AlternateText='<%# Eval("PrimaryKey") %>' ToolTip="Print PO" SkinID="print" OnClientClick='<%# Eval("GetPrintLink") %>' CommandArgument='<%# Container.DataItemIndex %>' />
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
                    <asp:TemplateField HeaderText="QCO PKG">
                      <ItemTemplate>
                        <div style="display: flex; flex-direction: row;">
                          <div title="QC Offered" style='font-weight: bold; padding: 5px; border-radius: 10px; background-color: #ff0033; color: white;'><%# Eval("QCOCount") %></div>
                          <div title="Packing Lists" style='font-weight: bold; padding: 5px; border-radius: 10px; background-color: gold; color: black;'><%# Eval("PKGCount") %></div>
                        </div>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Serial No" SortExpression="SerialNo">
                      <ItemTemplate>
                        <asp:Label ID="LabelSerialNo" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("SerialNo") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle CssClass="alignCenter" Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PO Number" SortExpression="PONumber">
                      <ItemTemplate>
                        <asp:Label ID="LabelPONumber" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("PONumber") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="" />
                      <HeaderStyle CssClass="" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="REV." SortExpression="PORevision">
                      <ItemTemplate>
                        <asp:Label ID="LabelPORevision" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("PORevision") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle CssClass="alignCenter" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PO TYPE" SortExpression="PAK_POTypes7_Description">
                      <ItemTemplate>
                        <asp:Label ID="L_POTypeID" runat="server" ForeColor='<%# EVal("ForeColor") %>' Title='<%# EVal("POTypeID") %>' Text='<%# Eval("PAK_POTypes7_Description") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle CssClass="alignCenter" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PO Date" SortExpression="PODate">
                      <ItemTemplate>
                        <asp:Label ID="LabelPODate" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("PODate") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle CssClass="alignCenter" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supplier" SortExpression="VR_BusinessPartner9_BPName">
                      <ItemTemplate>
                        <asp:Button ID="L_SupplierID" runat="server" ForeColor='<%# EVal("ForeColor") %>' BorderStyle="None" BackColor="Transparent" Style="cursor: pointer;" Font-Underline="true" Title='<%# EVal("SupplierID") %>' Text='<%# Eval("VR_BusinessPartner9_BPName") %>' CommandName="lgEmailIDs" CommandArgument='<%# Container.DataItemIndex %>'></asp:Button>
                      </ItemTemplate>
                      <HeaderStyle Width="200px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project" SortExpression="IDM_Projects4_Description">
                      <ItemTemplate>
                        <asp:Label ID="L_ProjectID" runat="server" ForeColor='<%# EVal("ForeColor") %>' Title='<%# EVal("ProjectID") %>' Text='<%# Eval("IDM_Projects4_Description") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" SortExpression="PAK_POStatus6_Description">
                      <ItemTemplate>
                        <asp:Label ID="L_POStatusID" runat="server" ForeColor='<%# EVal("ForeColor") %>' Title='<%# EVal("POStatusID") %>' Text='<%# Eval("PAK_POStatus6_Description") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RESET PW">
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdResetWF" ValidationGroup='<%# "Reset" & Container.DataItemIndex %>' CausesValidation="true" runat="server" AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Reset Supplier Password and Send E-Mail" SkinID="reset" OnClientClick='<%# "return Page_ClientValidate(""Reset" & Container.DataItemIndex & """) && confirm(""Reset Supplier Password to same as Login ID and send E-Mail ?"");" %>' CommandName="ResetWF" CommandArgument='<%# Container.DataItemIndex %>' />
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UPD">
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdUpdateWF" Visible='<%# Eval("UpdateWFVisible") %>' ValidationGroup='<%# "Update" & Container.DataItemIndex %>' CausesValidation="true" runat="server" AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Update PO from ERP" SkinID="update" OnClientClick='<%# "return Page_ClientValidate(""Update" & Container.DataItemIndex & """) && confirm(""Update PO to Latest version in ERP ?"");" %>' CommandName="UpdateWF" CommandArgument='<%# Container.DataItemIndex %>' />
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ISS">
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdInitiateWF" runat="server" Visible='<%# Eval("InitiateWFVisible") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Send to supplier" SkinID="forward" OnClientClick="return confirm('Issue PO to supplier ?');" CommandName="InitiateWF" CommandArgument='<%# Container.DataItemIndex %>' />
                        <asp:ImageButton ID="cmdUpdDwg" runat="server" Visible='<%# Eval("updFromDwgVisible") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Update from released DWG" SkinID="dwg" OnClientClick="return confirm('Update from released DWG ?');" CommandName="UpdFromDwg" CommandArgument='<%# Container.DataItemIndex %>' />
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdDeleteWF" runat="server" Visible='<%# Eval("DeleteWFVisible") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Delete PO." SkinID="delete" OnClientClick="return confirm('Delete PO ?');" CommandName="DeleteWF" CommandArgument='<%# Container.DataItemIndex %>' />
                        </td></tr>
            <tr style="background-color: AntiqueWhite; color: DeepPink">
              <td></td>
              <td></td>
              <td></td>
              <td colspan="4">
                <asp:Label ID="LabelISGECRemarks" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("ISGECRemarks") %>'></asp:Label>
              </td>
              <td></td>
              <td colspan="4">
                <asp:Label ID="LabelSupplierRemarks" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("SupplierRemarks") %>'></asp:Label>
              </td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
                      </ItemTemplate>
                      <HeaderStyle Width="10px" />
                    </asp:TemplateField>
                  </Columns>
                  <EmptyDataTemplate>
                    <asp:Label ID="LabelEmpty" runat="server" Font-Size="Small" ForeColor="Red" Text="No record found !!!"></asp:Label>
                  </EmptyDataTemplate>
                </asp:GridView>
                <asp:ObjectDataSource
                  ID="ODSpakPO"
                  runat="server"
                  DataObjectTypeName="SIS.PAK.pakPO"
                  OldValuesParameterFormatString="original_{0}"
                  SelectMethod="UZ_pakPOSelectList"
                  TypeName="SIS.PAK.pakPO"
                  SelectCountMethod="pakPOSelectCount"
                  SortParameterName="OrderBy" EnablePaging="True">
                  <SelectParameters>
                    <asp:ControlParameter ControlID="F_SupplierID" PropertyName="Text" Name="SupplierID" Type="String" Size="9" />
                    <asp:ControlParameter ControlID="F_ProjectID" PropertyName="Text" Name="ProjectID" Type="String" Size="6" />
                    <asp:ControlParameter ControlID="F_POStatusID" PropertyName="SelectedValue" Name="POStatusID" Type="Int32" Size="10" />
                    <asp:ControlParameter ControlID="F_IssuedBy" PropertyName="Text" Name="IssuedBy" Type="String" Size="8" />
                    <asp:ControlParameter ControlID="F_BuyerID" PropertyName="Text" Name="BuyerID" Type="String" Size="8" />
                    <asp:ControlParameter ControlID="F_POTypeID" PropertyName="SelectedValue" Name="POTypeID" Type="Int32" Size="10" />
                    <asp:Parameter Name="SearchState" Type="Boolean" Direction="Input" DefaultValue="false" />
                    <asp:Parameter Name="SearchText" Type="String" Direction="Input" DefaultValue="" />
                  </SelectParameters>
                </asp:ObjectDataSource>
                <br />
              </td>
            </tr>
          </table>
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="GVpakPO" EventName="PageIndexChanged" />
          <asp:AsyncPostBackTrigger ControlID="F_SupplierID" />
          <asp:AsyncPostBackTrigger ControlID="F_ProjectID" />
          <asp:AsyncPostBackTrigger ControlID="F_POStatusID" />
          <asp:AsyncPostBackTrigger ControlID="F_IssuedBy" />
          <asp:AsyncPostBackTrigger ControlID="F_BuyerID" />
          <asp:AsyncPostBackTrigger ControlID="F_POTypeID" />
        </Triggers>

      </asp:UpdatePanel>
    </div>
  </div>
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <asp:Panel ID="pnl1" runat="server" Style="background-color: white; display: none; height: 226px" Width='400px'>
        <asp:Panel ID="pnlHeader" runat="server" Style="width: 100%; height: 33px; padding-top: 8px; text-align: center; border-bottom: 1pt solid lightgray;">
          <asp:Label ID="HeaderText" runat="server" Font-Size="16px" Font-Bold="true" Text='My Modal Text'></asp:Label>
        </asp:Panel>
        <asp:Panel ID="modalContent" runat="server" Style="width: 100%; height: 136px; padding: 4px;">
          <asp:Label ID="L_EMailID" runat="server" Text="Update Supplier E-Mail IDs:" Font-Bold="true" Width="392px"></asp:Label>
          <asp:TextBox ID="F_EMailIDs" runat="server" Width="386px" Height="100px" TextMode="MultiLine" onfocus="this.select();"></asp:TextBox>
        </asp:Panel>
        <asp:Panel ID="pnlFooter" runat="server" Style="width: 100%; height: 33px; padding-top: 8px; text-align: right; border-top: 1pt solid lightgray;">
          <asp:Label ID="L_PrimaryKey" runat="server" Style="display: none;"></asp:Label>
          <asp:Button ID="cmdOK" runat="server" Width="70px" Text="OK" Style="text-align: center; margin-right: 30px;" />
          <asp:Button ID="cmdCancel" runat="server" Width="70px" Text="Cancel" Style="text-align: center; margin-right: 30px;" />
        </asp:Panel>
      </asp:Panel>
      <asp:Button ID="dummy" runat="server" Style="display: none;" Text="show"></asp:Button>
      <AJX:ModalPopupExtender
        ID="mPopup"
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
  <LGM:LC_pakLoginSelecter ID="ls1" runat="server" />
</asp:Content>
