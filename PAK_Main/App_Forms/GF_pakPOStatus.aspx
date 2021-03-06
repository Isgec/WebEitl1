<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="False" CodeFile="GF_pakPOStatus.aspx.vb" Inherits="GF_pakPOStatus" title="Maintain List: PO States" %>
<asp:Content ID="CPHpakPOStatus" ContentPlaceHolderID="cph1" Runat="Server">
<div class="ui-widget-content page">
<div class="caption">
    <asp:Label ID="LabelpakPOStatus" runat="server" Text="&nbsp;List: PO States"></asp:Label>
</div>
<div class="pagedata">
<asp:UpdatePanel ID="UPNLpakPOStatus" runat="server">
  <ContentTemplate>
    <table width="100%"><tr><td class="sis_formview"> 
    <LGM:ToolBar0 
      ID = "TBLpakPOStatus"
      ToolType = "lgNMGrid"
      EditUrl = "~/PAK_Main/App_Edit/EF_pakPOStatus.aspx"
      AddUrl = "~/PAK_Main/App_Create/AF_pakPOStatus.aspx"
      ValidationGroup = "pakPOStatus"
      runat = "server" />
    <asp:UpdateProgress ID="UPGSpakPOStatus" runat="server" AssociatedUpdatePanelID="UPNLpakPOStatus" DisplayAfter="100">
      <ProgressTemplate>
        <span style="color: #ff0033">Loading...</span>
      </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:GridView ID="GVpakPOStatus" SkinID="gv_silver" runat="server" DataSourceID="ODSpakPOStatus" DataKeyNames="StatusID">
      <Columns>
        <asp:TemplateField HeaderText="EDIT">
          <ItemTemplate>
            <asp:ImageButton ID="cmdEditPage" ValidationGroup="Edit" runat="server" Visible='<%# EVal("Visible") %>' Enabled='<%# EVal("Enable") %>' AlternateText="Edit" ToolTip="Edit the record." SkinID="Edit" CommandName="lgEdit" CommandArgument='<%# Container.DataItemIndex %>' />
          </ItemTemplate>
          <ItemStyle CssClass="alignCenter" />
          <HeaderStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status ID" SortExpression="StatusID">
          <ItemTemplate>
            <asp:Label ID="LabelStatusID" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("StatusID") %>'></asp:Label>
          </ItemTemplate>
          <ItemStyle CssClass="alignright" />
          <HeaderStyle CssClass="alignright" Width="40px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description" SortExpression="Description">
          <ItemTemplate>
            <asp:Label ID="LabelDescription" runat="server" ForeColor='<%# EVal("ForeColor") %>' Text='<%# Bind("Description") %>'></asp:Label>
          </ItemTemplate>
          <ItemStyle CssClass="" />
        <HeaderStyle CssClass="" Width="100px" />
        </asp:TemplateField>
      </Columns>
      <EmptyDataTemplate>
        <asp:Label ID="LabelEmpty" runat="server" Font-Size="Small" ForeColor="Red" Text="No record found !!!"></asp:Label>
      </EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource 
      ID = "ODSpakPOStatus"
      runat = "server"
      DataObjectTypeName = "SIS.PAK.pakPOStatus"
      OldValuesParameterFormatString = "original_{0}"
      SelectMethod = "pakPOStatusSelectList"
      TypeName = "SIS.PAK.pakPOStatus"
      SelectCountMethod = "pakPOStatusSelectCount"
      SortParameterName="OrderBy" EnablePaging="True">
      <SelectParameters >
        <asp:Parameter Name="SearchState" Type="Boolean" Direction="Input" DefaultValue="false" />
        <asp:Parameter Name="SearchText" Type="String" Direction="Input" DefaultValue="" />
      </SelectParameters>
    </asp:ObjectDataSource>
    <br />
  </td></tr></table>
  </ContentTemplate>
  <Triggers>
    <asp:AsyncPostBackTrigger ControlID="GVpakPOStatus" EventName="PageIndexChanged" />
  </Triggers>
</asp:UpdatePanel>
</div>
</div>
</asp:Content>
