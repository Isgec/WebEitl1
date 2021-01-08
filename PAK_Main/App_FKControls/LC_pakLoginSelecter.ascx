<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LC_pakLoginSelecter.ascx.vb" Inherits="LC_pakLoginSelecter" %>
<style>
  .m-box{
    border:1pt solid #808080;
    background-color:white;
    padding:2px;
    border-radius:6px;
  }
  .m-box-h{
    width:100%;
    height:70px;
    padding-top:4px;
    text-align:center;
    border-bottom:1pt solid #aca9a9;
  }
  .m-box-b{
    min-height:300px;
    width:100%;
  }
  .m-box-f{
    width:100%;
    height:33px;
    padding-top:8px;
    text-align:right;
    border-top:1pt solid #aca9a9;
  }
</style>
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
  <asp:Panel ID="pnl1" runat="server" CssClass="m-box" Style="display:none;" Width="700px">
    <asp:Panel ID="pnlHeader" runat="server" CssClass="m-box-h" >
      <asp:Label runat="server" ForeColor="Red">Supplier Login ID for <%= CurrentCompany %>  not found.</asp:Label><br />
      <asp:Label runat="server" ForeColor="#0099ff">Please select the MATCHING supplier name from <%= OtherCompany %> so that supplier can use same Login ID</asp:Label><br />
      <asp:Label runat="server"><b>NOTE:</b> If matching supplier NOT found/selected, a New Login-ID for this company will be issued to supplier.</asp:Label><br />
      <asp:Label ID="HeaderText" runat="server" Font-Size="14px" Font-Bold="true" ForeColor="#cc0099" Font-Underline="true" Text='Select Matching Login ID for Supplier'></asp:Label>
    </asp:Panel>
    <asp:Panel ID="modalContent" runat="server" CssClass="m-box-b">
      <div style="float:right;">
        <asp:TextBox ID="F_Search" runat="server" CssClass="mypktxt" Width="200px" MaxLength="100"></asp:TextBox>
        <asp:Button ID="cmdFind" runat="server" CssClass="nt-but-primary" Text="Find" />
      </div>
    <asp:GridView ID="GVpakBPLoginMap" SkinID="gv_silver" runat="server" DataSourceID="ODSpakBPLoginMap" DataKeyNames="LoginID,BPID,Comp">
      <Columns>
        <asp:TemplateField HeaderText="Select">
          <ItemTemplate>
            <asp:Button ID="cmdSelect" runat="server" CssClass="nt-but-danger" Text="Select" ToolTip="Select Login to link with Supplier." CommandName="lgSelect" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Select the Login ID ?');" />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle CssClass="alignCenter" Width="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Login ID">
          <ItemTemplate>
             <asp:Label ID="L_LoginID" runat="server" ForeColor='<%# Eval("ForeColor") %>' Title='<%# EVal("aspnet_users1_UserFullName") %>' Text='<%# Eval("LoginID") %>'></asp:Label>
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle CssClass="alignCenter" Width="60px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Supplier Name" SortExpression="[aspnet_users1].[UserFullName]">
          <ItemTemplate>
             <asp:Label ID="L_SupplierName" runat="server" ForeColor='<%# Eval("ForeColor") %>' Title='<%# EVal("LoginID") %>' Text='<%# Eval("aspnet_users1_UserFullName") %>'></asp:Label>
          </ItemTemplate>
          <ItemStyle CssClass="alignLeft" />
          <HeaderStyle CssClass="alignLeft" Width="200px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Address">
          <ItemTemplate>
             <asp:Label ID="L_AddressLine" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text='<%# Eval("AddressLine") %>'></asp:Label>
          </ItemTemplate>
          <HeaderStyle Width="100px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Comp" SortExpression="[SYS_BPLoginMap].[Comp]">
          <ItemTemplate>
            <asp:Label ID="LabelComp" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("Comp") %>'></asp:Label>
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
        <HeaderStyle CssClass="alignCenter" Width="30px" />
        </asp:TemplateField>
      </Columns>
      <EmptyDataTemplate>
        <asp:Label ID="LabelEmpty" runat="server" Font-Size="Small" ForeColor="Red" Text="No record found !!!"></asp:Label>
      </EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource 
      ID = "ODSpakBPLoginMap"
      runat = "server"
      DataObjectTypeName = "SIS.PAK.pakBPLoginMap"
      OldValuesParameterFormatString = "original_{0}"
      SelectMethod = "UZ_LoginSelecterSelectList"
      TypeName = "SIS.PAK.pakBPLoginMap"
      SelectCountMethod = "LoginSelecterSelectCount"
      SortParameterName="OrderBy" EnablePaging="True">
      <SelectParameters >
        <asp:Parameter Name="SearchState" Type="Boolean" Direction="Input" DefaultValue="true" />
        <asp:Parameter Name="SearchText" Type="String" Direction="Input" DefaultValue="" />
      </SelectParameters>
    </asp:ObjectDataSource>

    </asp:Panel>
    <asp:Panel ID="pnlFooter" runat="server" CssClass="m-box-f">
      <asp:Button ID="cmdOK" runat="server" CssClass="nt-but-success" Width="70px" Text="OK" style="text-align:center;margin-right:30px;" />
      <asp:Button ID="cmdCancel" runat="server" CssClass="nt-but-danger" Width="70px" Text="Cancle" style="text-align:center;margin-right:30px;" />
    </asp:Panel>
    <asp:Label runat="server" ForeColor="#999999"><b>Remember:</b> If supplier will issued separate Login ID for different company, he would not be able to view PO of other company by changing company from dropdown. He will have to logout and login with different credential.</asp:Label>
  </asp:Panel>
<asp:Button ID="dummy" runat="server" style="display:none;" Text="show"></asp:Button>
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
    <asp:AsyncPostBackTrigger ControlID="GVpakBPLoginMap" EventName="PageIndexChanged" />
    <asp:AsyncPostBackTrigger ControlID="cmdOK" EventName="Click" />
    <asp:AsyncPostBackTrigger ControlID="cmdFind" EventName="Click" />
  </Triggers>
  </asp:UpdatePanel>
