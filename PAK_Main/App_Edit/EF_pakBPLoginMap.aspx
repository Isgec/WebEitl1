<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="EF_pakBPLoginMap.aspx.vb" Inherits="EF_pakBPLoginMap" title="Edit: BP Login Map" %>
<asp:Content ID="CPHpakBPLoginMap" ContentPlaceHolderID="cph1" Runat="Server">
<div id="div1" class="ui-widget-content page">
<div id="div2" class="caption">
    <asp:Label ID="LabelpakBPLoginMap" runat="server" Text="&nbsp;Edit: BP Login Map"></asp:Label>
</div>
<div id="div3" class="pagedata">
<asp:UpdatePanel ID="UPNLpakBPLoginMap" runat="server" >
<ContentTemplate>
  <LGM:ToolBar0 
    ID = "TBLpakBPLoginMap"
    ToolType = "lgNMEdit"
    UpdateAndStay = "False"
    ValidationGroup = "pakBPLoginMap"
    runat = "server" />
<asp:FormView ID="FVpakBPLoginMap"
  runat = "server"
  DataKeyNames = "LoginID,BPID,Comp"
  DataSourceID = "ODSpakBPLoginMap"
  DefaultMode = "Edit" CssClass="sis_formview">
  <EditItemTemplate>
    <div id="frmdiv" class="ui-widget-content minipage">
    <table style="margin:auto;border: solid 1pt lightgrey">
      <tr>
        <td class="alignright">
          <b><asp:Label ID="L_LoginID" runat="server" ForeColor="#CC6633" Text="LoginID :" /><span style="color:red">*</span></b>
        </td>
        <td colspan="3">
          <asp:TextBox
            ID = "F_LoginID"
            Width="72px"
            Text='<%# Bind("LoginID") %>'
            CssClass = "mypktxt"
            Enabled = "False"
            ToolTip="Value of LoginID."
            Runat="Server" />
          <asp:Label
            ID = "F_LoginID_Display"
            Text='<%# Eval("aspnet_users1_UserFullName") %>'
            CssClass="myLbl"
            Runat="Server" />
        </td>
      </tr>
      <tr><td colspan="4" style="border-top: solid 1pt LightGrey" ></td></tr>
      <tr>
        <td class="alignright">
          <b><asp:Label ID="L_BPID" runat="server" ForeColor="#CC6633" Text="BPID :" /><span style="color:red">*</span></b>
        </td>
        <td colspan="3">
          <asp:TextBox ID="F_BPID"
            Text='<%# Bind("BPID") %>'
            ToolTip="Value of BPID."
            Enabled = "False"
            CssClass = "mypktxt"
            Width="80px"
            runat="server" />
        </td>
      </tr>
      <tr><td colspan="4" style="border-top: solid 1pt LightGrey" ></td></tr>
      <tr>
        <td class="alignright">
          <b><asp:Label ID="L_Comp" runat="server" ForeColor="#CC6633" Text="Comp :" /><span style="color:red">*</span></b>
        </td>
        <td colspan="3">
          <asp:DropDownList
            ID="F_Comp"
            SelectedValue='<%# Bind("Comp") %>'
            Width="200px"
            Enabled = "False"
            CssClass = "mypktxt"
            Runat="Server" >
            <asp:ListItem Value="200">200</asp:ListItem>
            <asp:ListItem Value="700">700</asp:ListItem>
            <asp:ListItem Value="651">651</asp:ListItem>
          </asp:DropDownList>
         </td>
      </tr>
      <tr><td colspan="4" style="border-top: solid 1pt LightGrey" ></td></tr>
    </table>
  </div>
  </EditItemTemplate>
</asp:FormView>
  </ContentTemplate>
</asp:UpdatePanel>
<asp:ObjectDataSource 
  ID = "ODSpakBPLoginMap"
  DataObjectTypeName = "SIS.PAK.pakBPLoginMap"
  SelectMethod = "pakBPLoginMapGetByID"
  UpdateMethod="UZ_pakBPLoginMapUpdate"
  DeleteMethod="UZ_pakBPLoginMapDelete"
  OldValuesParameterFormatString = "original_{0}"
  TypeName = "SIS.PAK.pakBPLoginMap"
  runat = "server" >
<SelectParameters>
  <asp:QueryStringParameter DefaultValue="0" QueryStringField="LoginID" Name="LoginID" Type="String" />
  <asp:QueryStringParameter DefaultValue="0" QueryStringField="BPID" Name="BPID" Type="String" />
  <asp:QueryStringParameter DefaultValue="0" QueryStringField="Comp" Name="Comp" Type="String" />
</SelectParameters>
</asp:ObjectDataSource>
</div>
</div>
</asp:Content>
