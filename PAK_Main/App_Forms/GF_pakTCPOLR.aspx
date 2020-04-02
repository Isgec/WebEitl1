<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="False" CodeFile="GF_pakTCPOLR.aspx.vb" Inherits="GF_pakTCPOLR" title="Maintain List: Submitted for TC" %>
<asp:Content ID="CPHpakTCPOLR" ContentPlaceHolderID="cph1" runat="Server">
  <div class="ui-widget-content page">
    <div class="caption">
      <asp:Label ID="LabelpakTCPOLR" runat="server" Text="&nbsp;Display: IDMS Receipt"></asp:Label>
    </div>
    <div class="pagedata">
      <asp:UpdatePanel ID="UPNLpakTCPOLR" runat="server">
        <ContentTemplate>
          <table width="100%">
            <tr>
              <td class="sis_formview">
                <LGM:ToolBar0
                  ID="TBLpakTCPOLR"
                  ToolType="lgNMGrid"
                  EditUrl="~/PAK_Main/App_Edit/EF_pakTCPOLR.aspx"
                  EnableAdd="False"
                  ValidationGroup="pakTCPOLR"
                  runat="server" />
                <asp:UpdateProgress ID="UPGSpakTCPOLR" runat="server" AssociatedUpdatePanelID="UPNLpakTCPOLR" DisplayAfter="100">
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
                <script type="text/javascript">
                  function v_print() {
                    var p = $get('F_ProjectID').value;
                    var s = $get('F_SupplierID').value;
                    var u = $get('<%= UClientID %>').value;
                    if(p == '' && s == '') {
                      alert('Supplier ID or Project ID atleast one value is required.')
                      return false;
                    }
                    var l = '<%= GetUrl %>';
                    var w = window.open( l + '?projectid=' + p + '&supplierid=' + s + '&uploadstatusid=' + u , 'win_'+u , 'left=20,top=20,width=100,height=100,toolbar=1,resizable=1,scrollbars=1');
                    return false;
                  }
                </script>
                <asp:Panel ID="pnlD" runat="server" CssClass="cp_filter" Height="0">

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
                          ClientIDMode="Static"
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
                          ClientIDMode="Static"
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
                          <asp:Label ID="L_UploadStatusID" runat="server" Text="IDMS Receipt Status :" /></b>
                      </td>
                      <td>
                        <LGM:LC_pakTCPOLRStatus
                          ID="F_UploadStatusID"
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
                          <asp:Label ID="Label1" runat="server" Text="Export to Excel :" /></b>
                      </td>
                      <td>
                        <asp:Button ID="cmdPrint" runat="server" Text="PRINT" OnClientClick="return v_print();" />
                      </td>
                    </tr>
                  </table>
                </asp:Panel>
                <AJX:CollapsiblePanelExtender ID="cpe1" runat="Server" TargetControlID="pnlD" ExpandControlID="pnlH" CollapseControlID="pnlH" Collapsed="True" TextLabelID="lblH" ImageControlID="imgH" ExpandedText="(Hide Filters...)" CollapsedText="(Show Filters...)" ExpandedImage="~/images/ua.png" CollapsedImage="~/images/da.png" SuppressPostBack="true" />
                <asp:GridView ID="GVpakTCPOLR" SkinID="gv_silver" runat="server" DataSourceID="ODSpakTCPOLR" DataKeyNames="SerialNo,ItemNo,UploadNo">
                  <Columns>
                    <asp:TemplateField HeaderText="EDIT">
                      <ItemTemplate>
                        <asp:ImageButton ID="cmdEditPage" ValidationGroup="Edit" runat="server" Visible='<%# Eval("Visible") %>' Enabled='<%# EVal("Enable") %>' AlternateText="Edit" ToolTip="Edit the record." SkinID="Edit" CommandName="lgEdit" CommandArgument='<%# Container.DataItemIndex %>' />
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ID" SortExpression="UploadNo">
                      <ItemTemplate>
                        <asp:Label ID="LabelUploadNo" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text='<%# Bind("UploadNo") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle CssClass="alignCenter" Width="30px" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Document Category" SortExpression="PAK_POLineRecCategory4_Description">
                      <ItemTemplate>
                         <asp:Label ID="L_DocumentCategoryID" runat="server" ForeColor='<%# EVal("ForeColor") %>' Title='<%# EVal("DocumentCategoryID") %>' Text='<%# Eval("PAK_POLineRecCategory4_Description") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle Width="100px" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Project" SortExpression="ProjectName">
                      <ItemTemplate>
                        <asp:Label ID="L_ProjectID" runat="server" ForeColor='<%# Eval("ForeColor") %>' Title='<%# EVal("ProjectID") %>' Text='<%# Eval("ProjectName") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supplier" SortExpression="BPName">
                      <ItemTemplate>
                        <asp:Label ID="L_SupplierID" runat="server" ForeColor='<%# Eval("ForeColor") %>' Title='<%# EVal("SupplierID") %>' Text='<%# Eval("BPName") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PONumber" SortExpression="PONumber">
                      <ItemTemplate>
                        <asp:Label ID="L_PONumber" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text='<%# Eval("PONumber") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle CssClass="alignCenter" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item" SortExpression="ItemDescription">
                      <ItemTemplate>
                        <asp:Label ID="L_ItemCode" runat="server" ForeColor='<%# Eval("ForeColor") %>' Title='<%# EVal("ItemCode") %>' Text='<%# Eval("ItemDescription") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignleft" />
                      <HeaderStyle CssClass="alignleft" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Submitted On" SortExpression="CreatedOn">
                      <ItemTemplate>
                        <asp:Label ID="LabelCreatedOn" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text='<%# Bind("CreatedOn") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle CssClass="alignCenter" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IDMS Receipt" SortExpression="ReceiptNo">
                      <ItemTemplate>
                        <asp:Label ID="LabelReceiptNo" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text='<%# Bind("ReceiptNo") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle CssClass="alignCenter" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="REV." SortExpression="RevisionNo">
                      <ItemTemplate>
                        <asp:Label ID="LabelRevisionNo" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text='<%# Bind("RevisionNo") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle CssClass="alignCenter" />
                      <HeaderStyle CssClass="alignCenter" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" SortExpression="PAK_POLineRecStatus5_Description">
                      <ItemTemplate>
                        <asp:Label ID="L_UploadStatusID" runat="server" ForeColor='<%# Eval("ForeColor") %>' Title='<%# EVal("UploadStatusID") %>' Text='<%# Eval("PAK_POLineRecStatus5_Description") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle Width="100px" />
                    </asp:TemplateField>
                  </Columns>
                  <EmptyDataTemplate>
                    <asp:Label ID="LabelEmpty" runat="server" Font-Size="Small" ForeColor="Red" Text="No record found !!!"></asp:Label>
                  </EmptyDataTemplate>
                </asp:GridView>
                <asp:ObjectDataSource
                  ID="ODSpakTCPOLR"
                  runat="server"
                  DataObjectTypeName="SIS.PAK.pakTCPOLR"
                  OldValuesParameterFormatString="original_{0}"
                  SelectMethod="pakDisplayReceipt"
                  TypeName="SIS.PAK.pakTCPOLR"
                  SelectCountMethod="pakDisplayReceiptCount"
                  SortParameterName="OrderBy" EnablePaging="True">
                  <SelectParameters>
                    <asp:ControlParameter ControlID="F_UploadStatusID" PropertyName="SelectedValue" Name="UploadStatusID" Type="Int32" Size="10" />
                    <asp:ControlParameter ControlID="F_SupplierID" PropertyName="Text" Name="SupplierID" Type="String" Size="9" />
                    <asp:ControlParameter ControlID="F_ProjectID" PropertyName="Text" Name="ProjectID" Type="String" Size="6" />
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
          <asp:AsyncPostBackTrigger ControlID="GVpakTCPOLR" EventName="PageIndexChanged" />
          <asp:AsyncPostBackTrigger ControlID="F_SupplierID" />
          <asp:AsyncPostBackTrigger ControlID="F_ProjectID" />
          <asp:AsyncPostBackTrigger ControlID="F_UploadStatusID" />
        </Triggers>
      </asp:UpdatePanel>
    </div>
  </div>
</asp:Content>
