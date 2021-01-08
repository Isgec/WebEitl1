<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="AF_pakBPLoginMap.aspx.vb" Inherits="AF_pakBPLoginMap" title="Add: BP Login Map" %>
<asp:Content ID="CPHpakBPLoginMap" ContentPlaceHolderID="cph1" Runat="Server">
<div id="div1" class="ui-widget-content page">
<div id="div2" class="caption">
    <asp:Label ID="LabelpakBPLoginMap" runat="server" Text="&nbsp;Add: BP Login Map"></asp:Label>
</div>
<div id="div3" class="pagedata">
<asp:UpdatePanel ID="UPNLpakBPLoginMap" runat="server" >
  <ContentTemplate>
  <LGM:ToolBar0 
    ID = "TBLpakBPLoginMap"
    ToolType = "lgNMAdd"
    InsertAndStay = "False"
    ValidationGroup = "pakBPLoginMap"
    runat = "server" />
<asp:FormView ID="FVpakBPLoginMap"
  runat = "server"
  DataKeyNames = "LoginID,BPID,Comp"
  DataSourceID = "ODSpakBPLoginMap"
  DefaultMode = "Insert" CssClass="sis_formview">
  <InsertItemTemplate>
    <div id="frmdiv" class="ui-widget-content minipage">
    <asp:Label ID="L_ErrMsgpakBPLoginMap" runat="server" ForeColor="Red" Font-Bold="true" Text=""></asp:Label>
    <table style="margin:auto;border: solid 1pt lightgrey">
      <tr>
        <td class="alignright">
          <b><asp:Label ID="L_LoginID" ForeColor="#CC6633" runat="server" Text="LoginID :" /><span style="color:red">*</span></b>
        </td>
        <td colspan="3">
          <asp:TextBox
            ID = "F_LoginID"
            CssClass = "mypktxt"
            Width="72px"
            Text='<%# Bind("LoginID") %>'
            AutoCompleteType = "None"
            onfocus = "return this.select();"
            ToolTip="Enter value for LoginID."
            ValidationGroup = "pakBPLoginMap"
            onblur= "script_pakBPLoginMap.validate_LoginID(this);"
            Runat="Server" />
          <asp:RequiredFieldValidator 
            ID = "RFVLoginID"
            runat = "server"
            ControlToValidate = "F_LoginID"
            ErrorMessage = "<div class='errorLG'>Required!</div>"
            Display = "Dynamic"
            EnableClientScript = "true"
            ValidationGroup = "pakBPLoginMap"
            SetFocusOnError="true" />
          <asp:Label
            ID = "F_LoginID_Display"
            Text='<%# Eval("aspnet_users1_UserFullName") %>'
            CssClass="myLbl"
            Runat="Server" />
          <AJX:AutoCompleteExtender
            ID="ACELoginID"
            BehaviorID="B_ACELoginID"
            ContextKey=""
            UseContextKey="true"
            ServiceMethod="LoginIDCompletionList"
            TargetControlID="F_LoginID"
            EnableCaching="false"
            CompletionInterval="100"
            FirstRowSelected="true"
            MinimumPrefixLength="1"
            OnClientItemSelected="script_pakBPLoginMap.ACELoginID_Selected"
            OnClientPopulating="script_pakBPLoginMap.ACELoginID_Populating"
            OnClientPopulated="script_pakBPLoginMap.ACELoginID_Populated"
            CompletionSetCount="10"
            CompletionListCssClass = "autocomplete_completionListElement"
            CompletionListItemCssClass = "autocomplete_listItem"
            CompletionListHighlightedItemCssClass = "autocomplete_highlightedListItem"
            Runat="Server" />
        </td>
      </tr>
      <tr>
        <td class="alignright">
          <b><asp:Label ID="L_BPID" ForeColor="#CC6633" runat="server" Text="BPID :" /><span style="color:red">*</span></b>
        </td>
        <td colspan="3">
          <asp:TextBox ID="F_BPID"
            Text='<%# Bind("BPID") %>'
            CssClass = "mypktxt"
            onfocus = "return this.select();"
            ValidationGroup="pakBPLoginMap"
            onblur= "this.value=this.value.replace(/\'/g,'');"
            ToolTip="Enter value for BPID."
            MaxLength="9"
            Width="80px"
            runat="server" />
          <asp:RequiredFieldValidator 
            ID = "RFVBPID"
            runat = "server"
            ControlToValidate = "F_BPID"
            ErrorMessage = "<div class='errorLG'>Required!</div>"
            Display = "Dynamic"
            EnableClientScript = "true"
            ValidationGroup = "pakBPLoginMap"
            SetFocusOnError="true" />
        </td>
      </tr>
      <tr>
        <td class="alignright">
          <b><asp:Label ID="L_Comp" ForeColor="#CC6633" runat="server" Text="Comp :" /><span style="color:red">*</span></b>
        </td>
        <td colspan="3">
          <asp:DropDownList
            ID="F_Comp"
            SelectedValue='<%# Bind("Comp") %>'
            Width="200px"
            ValidationGroup = "pakBPLoginMap"
            CssClass = "myddl"
            Runat="Server" >
            <asp:ListItem Value="200">200</asp:ListItem>
            <asp:ListItem Value="700">700</asp:ListItem>
            <asp:ListItem Value="651">651</asp:ListItem>
          </asp:DropDownList>
          <asp:RequiredFieldValidator 
            ID = "RFVComp"
            runat = "server"
            ControlToValidate = "F_Comp"
            ErrorMessage = "<div class='errorLG'>Required!</div>"
            Display = "Dynamic"
            EnableClientScript = "true"
            ValidationGroup = "pakBPLoginMap"
            SetFocusOnError="true" />
         </td>
      </tr>
    </table>
    </div>
  </InsertItemTemplate>
</asp:FormView>
  </ContentTemplate>
</asp:UpdatePanel>
<asp:ObjectDataSource 
  ID = "ODSpakBPLoginMap"
  DataObjectTypeName = "SIS.PAK.pakBPLoginMap"
  InsertMethod="UZ_pakBPLoginMapInsert"
  OldValuesParameterFormatString = "original_{0}"
  TypeName = "SIS.PAK.pakBPLoginMap"
  SelectMethod = "GetNewRecord"
  runat = "server" >
</asp:ObjectDataSource>
</div>
</div>
</asp:Content>
