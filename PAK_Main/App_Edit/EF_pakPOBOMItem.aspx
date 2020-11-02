<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="EF_pakPOBOMItem.aspx.vb" Inherits="EF_pakPOBOMItem" title="Edit: PO BOM Item" %>
<asp:Content ID="CPHpakPOBOM" ContentPlaceHolderID="cph1" Runat="Server">
<div id="div1" class="ui-widget-content page">
<div id="div2" class="caption">
    <asp:Label ID="LabelpakPOBOM" runat="server" Text="&nbsp;Edit: PO BOM Item"></asp:Label>
</div>
<div id="div3" class="pagedata">
<asp:UpdatePanel ID="UPNLpakPOBOM" runat="server" >
<ContentTemplate>
  <LGM:ToolBar0 
    ID = "TBLpakPOBOM"
    ToolType = "lgNMEdit"
    UpdateAndStay = "False"
    ValidationGroup = "pakPOBOM"
    runat = "server" />
<asp:FormView ID="FVpakPOBOM"
  runat = "server"
  DataKeyNames = "SerialNo,BOMNo"
  DataSourceID = "ODSpakPOBOM"
  DefaultMode = "Edit" CssClass="sis_formview">
  <EditItemTemplate>
    <div id="frmdiv" class="ui-widget-content minipage">
    <table style="margin:auto;border: solid 1pt lightgrey">
      <tr>
        <td class="alignright">
          <b><asp:Label ID="L_SerialNo" runat="server" ForeColor="#CC6633" Text="Serial No :" /><span style="color:red">*</span></b>
        </td>
        <td>
          <asp:TextBox
            ID = "F_SerialNo"
            Width="88px"
            Text='<%# Bind("SerialNo") %>'
            CssClass = "mypktxt"
            Enabled = "False"
            ToolTip="Value of Serial No."
            Runat="Server" />
          <asp:Label
            ID = "F_SerialNo_Display"
            Text='<%# Eval("PAK_PO6_PODescription") %>'
            CssClass="myLbl"
            Runat="Server" />
        </td>
        <td class="alignright">
          <b><asp:Label ID="L_BOMNo" runat="server" ForeColor="#CC6633" Text="BOM No :" /><span style="color:red">*</span></b>
        </td>
        <td>
          <asp:TextBox ID="F_BOMNo"
            Text='<%# Bind("BOMNo") %>'
            ToolTip="Value of BOM No."
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
          <asp:Label ID="L_ItemNo" runat="server" Text="Item No :" />&nbsp;
        </td>
        <td>
          <asp:TextBox ID="F_ItemNo"
            Text='<%# Bind("ItemNo") %>'
            ToolTip="Value of Item No."
            Enabled = "False"
            Width="88px"
            CssClass = "dmytxt"
            style="text-align: right"
            runat="server" />
        </td>
        <td class="alignright">
          <asp:Label ID="L_ItemDescription" runat="server" Text="Item Description :" />&nbsp;
        </td>
        <td>
          <asp:TextBox ID="F_ItemDescription"
            Text='<%# Bind("ItemDescription") %>'
            ToolTip="Value of Item Description."
            Enabled = "False"
            Width="350px" 
            CssClass = "dmytxt"
            runat="server" />
        </td>
      </tr>
      <tr><td colspan="4" style="border-top: solid 1pt LightGrey" ></td></tr>
      <tr>
        <td class="alignright">
          <asp:Label ID="L_ISGECRemarks" runat="server" Text="ISGEC Remarks :" />&nbsp;
        </td>
        <td>
          <asp:TextBox ID="F_ISGECRemarks"
            Text='<%# Bind("ISGECRemarks") %>'
            Width="350px" Height="40px" TextMode="MultiLine"
            CssClass = "mytxt"
            onfocus = "return this.select();"
            onblur= "this.value=this.value.replace(/\'/g,'');"
            MaxLength="500"
            runat="server" />
        </td>
        <td class="alignright">
          <asp:Label ID="L_SupplierRemarks" runat="server" Text="Supplier Remarks :" />&nbsp;
        </td>
        <td>
          <asp:TextBox ID="F_SupplierRemarks"
            Text='<%# Bind("SupplierRemarks") %>'
            Width="350px" Height="40px" TextMode="MultiLine"
            CssClass = "mytxt"
            onfocus = "return this.select();"
            onblur= "this.value=this.value.replace(/\'/g,'');"
            MaxLength="500"
            runat="server" />
        </td>
      </tr>
    </table>
  </div>
<fieldset class="ui-widget-content page">
<legend>
    <asp:Label ID="LabelpakPOBItems" runat="server" Text="&nbsp;List: PO BOM Item Details"></asp:Label>
</legend>
<div class="pagedata">
<asp:UpdatePanel ID="UPNLpakPOBItems" runat="server">
  <ContentTemplate>
    <table width="100%"><tr><td class="sis_formview"> 
    <LGM:ToolBar0 
      ID = "TBLpakPOBItems"
      ToolType = "lgNMGrid"
      EditUrl = "~/PAK_Main/App_Edit/EF_pakPOBIEngg.aspx"
      EnableAdd = "False"
      EnableExit = "false"
      ValidationGroup = "pakPOBItems"
      runat = "server" />
    <asp:UpdateProgress ID="UPGSpakPOBItems" runat="server" AssociatedUpdatePanelID="UPNLpakPOBItems" DisplayAfter="100">
      <ProgressTemplate>
        <span style="color: #ff0033">Loading...</span>
      </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">
      var pcnt = 0;
      function print_report(o) {
        pcnt = pcnt + 1;
        var nam = 'wTask' + pcnt;
        var url = self.location.href.replace('App_Edit/EF_','App_Print/RP_');
        url = url + '&pk=' + o.alt;
        window.open(url, nam, 'left=20,top=20,width=1100,height=600,toolbar=1,resizable=1,scrollbars=1');
        return false;
      }
    </script>
    <asp:GridView ID="GVpakPOBItems" SkinID="gv_silver" runat="server" DataSourceID="ODSpakPOBItems" DataKeyNames="SerialNo,BOMNo,ItemNo">
      <Columns>
        <asp:TemplateField HeaderText="EDIT">
          <ItemTemplate>
            <asp:ImageButton ID="cmdEditPage" ValidationGroup="Edit" runat="server" Visible='<%# Eval("Visible") %>' Enabled='<%# EVal("Enable") %>' AlternateText="Edit" ToolTip="Edit the record." SkinID="Edit" CommandName="lgEdit" CommandArgument='<%# Container.DataItemIndex %>' />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
<%--        <asp:TemplateField HeaderText="PRINT">
          <ItemTemplate>
            <asp:ImageButton ID="cmdPrintPage" runat="server" Visible='<%# EVal("Visible") %>' Enabled='<%# EVal("Enable") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Print the record." SkinID="Print" OnClientClick="return print_report(this);" />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Get XL">
          <ItemTemplate>
            <asp:ImageButton ID="cmdDownload" runat="server" Visible='<%# EVal("Visible") %>' Enabled='<%# EVal("Enable") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Download Template File." SkinID="download" OnClientClick='<%# Eval("GetDownloadLink") %>' />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="Item No" SortExpression="ItemNo">
          <ItemTemplate>
            <asp:Label ID="LabelItemNo" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("ItemNo") %>'></asp:Label>
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle CssClass="alignCenter" Width="80px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="B">
          <ItemTemplate>
            <asp:Label ID="L_Free" runat="server" Visible='<%# EVal("Visible") %>' Font-Bold="true" ForeColor="Red" ToolTip="Not From Standard BOM" Text='<%# Eval("ShowCreated") %>' />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="10px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Item Description" SortExpression="ItemDescription">
          <ItemTemplate>
            <asp:Linkbutton ID="LabelItemDescription" runat="server" Font-Bold='<%# Eval("FontBold") %>' ForeColor='<%# Eval("ForeColor") %>' Text='<%# Eval("pItemDescription") %>' CommandName="EditWF" CommandArgument='<%#  Container.DataItemIndex  %>' />
          </ItemTemplate>
          <ItemStyle CssClass="alignleft" />
        <HeaderStyle CssClass="alignleft" Width="500px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity">
          <ItemTemplate>
            <asp:Label ID="LabelQuantity" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Eval("PQuantity") %>'></asp:Label>
          </ItemTemplate>
          <ItemStyle CssClass="alignright" />
          <HeaderStyle CssClass="alignright" Width="80px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Wt Per Unit" SortExpression="WeightPerUnit">
          <ItemTemplate>
            <asp:Label ID="LabelWeightPerUnit" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Eval("PWeightPerUnit") %>'></asp:Label>
          </ItemTemplate>
          <ItemStyle CssClass="alignright" />
          <HeaderStyle CssClass="alignright" Width="80px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ACK">
          <ItemTemplate>
            <asp:ImageButton ID="cmdAccept" runat="server" Visible='<%# Eval("AcceptWFVisible") %>'  AlternateText='<%# Eval("PrimaryKey") %>' ToolTip='<%# Eval("ACKToolTip") %>' SkinID="approve" OnClientClick='<%# "return confirm(""" & Eval("ACKConfirm") & """);" %>' CommandName="AcceptWF" CommandArgument='<%#  Container.DataItemIndex  %>' />
            <asp:Label ID="L_Modified" runat="server" Visible='<%# Eval("ModifiedWFVisible") %>'  ToolTip="Record Modified, requires ACK." Font-Bold="true" ForeColor="#ff0000" Font-Size="12px" Text="*" />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ADD">
          <ItemTemplate>
            <asp:ImageButton ID="cmdAddC" runat="server" Visible='<%# Eval("AddChildWFVisible") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Add new child item." SkinID="add"  CommandName="NewWF" CommandArgument='<%#  Container.DataItemIndex  %>' />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="CPY">
          <ItemTemplate>
            <asp:ImageButton ID="cmdCopyC" ValidationGroup='<%# "CopyC" & Container.DataItemIndex %>' CausesValidation="true" runat="server" Visible='<%# EVal("DeleteWFVisible") %>' Enabled='<%# EVal("DeleteWFEnable") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Copy Complete [Tree from here]" SkinID="copy" OnClientClick='<%# "return Page_ClientValidate(""CopyC" & Container.DataItemIndex & """) && confirm(""It will also COPY all CHILD ITEMs below this item ?"");" %>' CommandName="CopyWF" CommandArgument='<%#  Container.DataItemIndex  %>' />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="DEL">
          <ItemTemplate>
            <asp:ImageButton ID="cmdDelete" runat="server" Visible='<%# Eval("DeleteWFVisible") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Delete Complete [Tree from here]" SkinID="Delete" OnClientClick="return confirm('It will also DELETE all CHILD ITEMs below this item ?');" CommandName="DeleteWF" CommandArgument='<%# Container.DataItemIndex %>' />
            <asp:ImageButton ID="cmdUnDelete" runat="server" Visible='<%# Eval("UnDeleteWFVisible") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Un Delete Complete [Tree from here]" SkinID="warn" OnClientClick="return confirm('It will also UN-DELETE all CHILD ITEMs below this item ?');" CommandName="UnDeleteWF" CommandArgument='<%# Container.DataItemIndex %>' />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="FRZ">
          <ItemTemplate>
            <asp:ImageButton ID="cmdFreezeWF" runat="server" Visible='<%# Eval("FreezeWFVisible") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Freeze Item." SkinID="freez" OnClientClick="return confirm('Freeze Item [Tree from here]. Once Item is freezed, NO modification can be done ?');" CommandName="FreezeWF" CommandArgument='<%# Container.DataItemIndex %>' />
            <asp:ImageButton ID="cmdUnFreez" runat="server" Visible='<%# Eval("UnFreezeWFVisible") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Unfreeze Item." SkinID="unfreez" OnClientClick="return confirm('Unfreeze Item [Tree from here] for modification ?');" CommandName="UnFreezeWF" CommandArgument='<%# Container.DataItemIndex %>' />
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
      ID = "ODSpakPOBItems"
      runat = "server"
      DataObjectTypeName = "SIS.PAK.pakPOBItems"
      OldValuesParameterFormatString = "original_{0}"
      SelectMethod = "UZ_pakPOBItemsSelectList"
      TypeName = "SIS.PAK.pakPOBItems"
      SelectCountMethod = "pakPOBItemsSelectCount"
      SortParameterName="OrderBy" EnablePaging="True">
      <SelectParameters >
        <asp:ControlParameter ControlID="F_SerialNo" PropertyName="Text" Name="SerialNo" Type="Int32" Size="10" />
        <asp:ControlParameter ControlID="F_BOMNo" PropertyName="Text" Name="BOMNo" Type="Int32" Size="10" />
        <asp:Parameter Name="SearchState" Type="Boolean" Direction="Input" DefaultValue="false" />
        <asp:Parameter Name="SearchText" Type="String" Direction="Input" DefaultValue="" />
      </SelectParameters>
    </asp:ObjectDataSource>
    <br />
  </td></tr></table>
  </ContentTemplate>
  <Triggers>
    <asp:AsyncPostBackTrigger ControlID="GVpakPOBItems" EventName="PageIndexChanged" />
  </Triggers>
</asp:UpdatePanel>
</div>
</fieldset>
  </EditItemTemplate>
</asp:FormView>
  </ContentTemplate>
</asp:UpdatePanel>
<asp:ObjectDataSource 
  ID = "ODSpakPOBOM"
  DataObjectTypeName = "SIS.PAK.pakPOBOM"
  SelectMethod = "pakPOBOMGetByID"
  UpdateMethod="UZ_pakPOBOMUpdate"
  DeleteMethod="UZ_pakPOBOMDelete"
  OldValuesParameterFormatString = "original_{0}"
  TypeName = "SIS.PAK.pakPOBOM"
  runat = "server" >
<SelectParameters>
  <asp:QueryStringParameter DefaultValue="0" QueryStringField="SerialNo" Name="SerialNo" Type="Int32" />
  <asp:QueryStringParameter DefaultValue="0" QueryStringField="BOMNo" Name="BOMNo" Type="Int32" />
</SelectParameters>
</asp:ObjectDataSource>
</div>
</div>
  <style>
    .nt-but-danger {
      border: 1pt solid #960825;
      background-color: #d1062f;
      color: white;
    }
    .nt-but-primary {
      border: 1pt solid #5780f8;
      background-color:#2196F3;
      color:black;
    }
    .nt-but-grey {
      border: 1pt solid gray;
      background-color:#bdbcbc;
      color: black;
    }
    .nt-but-success {
      border: 1pt solid #049317;
      background-color: #06bf1e;
      color: white;
    }
    .nt-but-danger,
    .nt-but-grey,
    .nt-but-primary,
    .nt-but-success {
      border-radius: 4px;
      height: 20px;
      font-size: 10px;
      font-weight:bold;
    }
    .nt-but-danger:hover,
    .nt-but-grey:hover,
    .nt-but-primary:hover,
    .nt-but-success:hover {
      border:1pt solid orange;
      opacity:0.7;
    }

  </style>
  <script>
    function isValidItem() {
      var x = document.getElementById('F_ItemDescription');
      if (x.value == '') {
        alert('Item Description is required.');
        x.focus();
        return false;
      }
      return true;
    }
  </script>
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <asp:Panel ID="pnl1" runat="server" Style="background-color: white; display: none; height:350px;border-radius:6px;" Width='800px'>
        <asp:Panel ID="pnlHeader" runat="server" Style="width: 100%; height: 33px; padding-top: 8px; text-align: center; border-bottom: 1pt solid lightgray;background-color:#034e83;color:white;">
          <asp:Label ID="HeaderText" runat="server" Font-Size="16px" Font-Bold="true" Text='My Modal Text'></asp:Label>
        </asp:Panel>
        <asp:Panel ID="modalContent" runat="server" Style="width: 100%; height: 250px; padding: 4px;">
          <table style="margin: auto; border: solid 1pt lightgrey">
            <tr>
              <td class="alignright">
                <b>
                  <asp:Label ID="L_SerialNo" runat="server" ForeColor="#CC6633" Text="Serial No :" /></b>
              </td>
              <td>
                <asp:TextBox
                  ID="F_SerialNo"
                  Width="88px"
                  Text='<%# Bind("SerialNo") %>'
                  CssClass="mypktxt"
                  Enabled="False"
                  runat="Server" />
              </td>
              <td class="alignright">
                <b>
                  <asp:Label ID="L_BOMNo" runat="server" ForeColor="#CC6633" Text="BOM No :" /></b>
              </td>
              <td>
                <asp:TextBox
                  ID="F_BOMNo"
                  Width="88px"
                  Text='<%# Bind("BOMNo") %>'
                  CssClass="mypktxt"
                  Enabled="False"
                  runat="Server" />
              </td>
            </tr>
            <tr>
              <td class="alignright">
                <b>
                  <asp:Label ID="L_ItemNo" runat="server" ForeColor="#CC6633" Text="Item No :" /></b>
              </td>
              <td colspan="3">
                <asp:TextBox ID="F_ItemNo"
                  Text='<%# Bind("ItemNo") %>'
                  Enabled="False"
                  CssClass="mypktxt"
                  Width="88px"
                  Style="text-align: right"
                  runat="server" />
              </td>
            </tr>
            <tr>
              <td colspan="4" style="border-top: solid 1pt LightGrey"></td>
            </tr>
            <tr>
              <td class="alignright">
                <asp:Label ID="L_ItemCode" runat="server" Text="ISGEC Item Code :" />&nbsp;
              </td>
              <td colspan="3">
                <asp:TextBox ID="F_ItemCode"
                  Text='<%# Bind("ItemCode") %>'
                  Width="200px"
                  MaxLength="50"
                  CssClass="mytxt"
                  runat="server" />
              </td>
            </tr>
            <tr>
              <td class="alignright">
                <asp:Label ID="L_SupplierItemCode" runat="server" Text="Supplier Item Code :" />&nbsp;
              </td>
              <td colspan="3">
                <asp:TextBox ID="F_SupplierItemCode"
                  Text='<%# Bind("SupplierItemCode") %>'
                  ToolTip="Supplier Item Code."
                  Width="200px"
                  MaxLength="50"
                  CssClass="mytxt"
                  runat="server" />
              </td>
            </tr>
            <tr>
              <td class="alignright">
                <asp:Label ID="L_ItemDescription" runat="server" Text="Item Description :" />&nbsp;
              </td>
              <td colspan="3">
                <asp:TextBox ID="F_ItemDescription"
                  Text='<%# Bind("ItemDescription") %>'
                  ToolTip="Item Description."
                  Width="450px"
                  MaxLength="100"
                  CssClass="mytxt"
                  ClientIDMode="Static"
                  runat="server" />
              </td>
            </tr>
            <tr>
              <td colspan="4" style="border-top: solid 1pt LightGrey"></td>
            </tr>
            <tr id="opt1" runat="server" clientidmode="static">
              <td class="alignright">
                <asp:Label ID="L_UOMQuantity" runat="server" Text="UOM Quantity :" />&nbsp;
              </td>
              <td>
                <LGM:LC_pakUnits
                  ID="F_UOMQuantity"
                  SelectedValue='<%# Bind("UOMQuantity") %>'
                  OrderBy="DisplayField"
                  DataTextField="DisplayField"
                  DataValueField="PrimaryKey"
                  IncludeDefault="true"
                  DefaultText="-- Select --"
                  Width="200px"
                  CssClass="myddl"
                  runat="Server" />
              </td>
              <td class="alignright">
                <asp:Label ID="L_Quantity" runat="server" Text="Quantity :" />&nbsp;
              </td>
              <td>
                <asp:TextBox ID="F_Quantity"
                  Text='<%# Bind("Quantity") %>'
                  Style="text-align: right"
                  Width="168px"
                  CssClass="mytxt"
                  MaxLength="14"
                  onfocus="return this.select();"
                  onblur="return dc(this,4);"
                  runat="server" />
              </td>
            </tr>
            <tr id="opt2" runat="server" clientidmode="static">
              <td class="alignright">
                <asp:Label ID="L_UOMWeight" runat="server" Text="UOM Weight :" />&nbsp;
              </td>
              <td>
                <LGM:LC_pakUnits
                  ID="F_UOMWeight"
                  SelectedValue='<%# Bind("UOMWeight") %>'
                  OrderBy="DisplayField"
                  DataTextField="DisplayField"
                  DataValueField="PrimaryKey"
                  IncludeDefault="true"
                  DefaultText="-- Select --"
                  Width="200px"
                  CssClass="myddl"
                  runat="Server" />
              </td>
              <td class="alignright">
                <asp:Label ID="L_WeightPerUnit" runat="server" Text="Weight Per Unit :" />&nbsp;
              </td>
              <td>
                <asp:TextBox ID="F_WeightPerUnit"
                  Text='<%# Bind("WeightPerUnit") %>'
                  Style="text-align: right"
                  Width="168px"
                  CssClass="mytxt"
                  MaxLength="14"
                  onfocus="return this.select();"
                  onblur="return dc(this,4);"
                  runat="server" />
              </td>
            </tr>
            <tr>
              <td colspan="4" style="border-top: solid 1pt LightGrey"></td>
            </tr>
            <tr id="opt3" runat="server" clientidmode="static">
              <td class="alignright">
                <asp:Label ID="L_ISGECRemarks" runat="server" Text="ISGEC Remarks :" />&nbsp;
              </td>
              <td>
                <asp:TextBox ID="F_ISGECRemarks"
                  Text='<%# Bind("ISGECRemarks") %>'
                  Width="200px" Height="40px" TextMode="MultiLine"
                  CssClass="mytxt"
                  onfocus="return this.select();"
                  onblur="this.value=this.value.replace(/\'/g,'');"
                  ToolTip="Enter ISGEC Remarks."
                  MaxLength="500"
                  runat="server" />
              </td>
              <td class="alignright">
                <asp:Label ID="L_SupplierRemarks" runat="server" Text="Supplier Remarks :" />&nbsp;
              </td>
              <td>
                <asp:TextBox ID="F_SupplierRemarks"
                  Text='<%# Bind("SupplierRemarks") %>'
                  ToolTip="Enter Supplier Remarks."
                  Width="200px" Height="40px" TextMode="MultiLine"
                  CssClass="dmytxt"
                  onfocus="return this.select();"
                  onblur="this.value=this.value.replace(/\'/g,'');"
                  MaxLength="500"
                  runat="server" />
              </td>

            </tr>
          </table>
        </asp:Panel>
        <asp:Panel ID="pnlFooter" runat="server" Style="width: 100%; height: 33px; padding-top: 8px; text-align: right; border-top: 1pt solid lightgray;">
          <asp:Label ID="L_PrimaryKey" runat="server" Style="display: none;"></asp:Label>
          <asp:Button ID="cmdOK" CssClass="nt-but-success" runat="server" Width="70px" Text="OK" Style="text-align: center; margin-right: 30px;" OnClientClick="return isValidItem();" />
          <asp:Button ID="cmdCancel" CssClass="nt-but-danger" runat="server" Width="70px" Text="Cancle" Style="text-align: center; margin-right: 30px;" />
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
