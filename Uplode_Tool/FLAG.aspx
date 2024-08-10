<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FLAG.aspx.cs" Inherits="FLAG" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
  <tr>
  <td>
      <asp:Label ID="Label1" runat="server" Text="Upload Data" ></asp:Label>
  </td>
  </tr>
  <tr>
  <td>
      <asp:FileUpload ID="FileUpload1" runat="server" />
  </td>
  <td>
      <asp:Button ID="btnUpload" runat="server" Text="Upload" 
          onclick="btnUpload_Click" />
  
  </td>
  <td>
      <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
          onclick="btnCancel_Click" />
  </td>
  <td>
      <u ><asp:HyperLink ID="hlpricingdocmay" runat="server" NavigateUrl="~/UploadFormat/UploadFlag.xls" Target="_blank">Please Download_Upload_Format</asp:HyperLink></u>
      
  </td>
  </tr>
  <tr>
  <td class="auto-style1">
      <asp:Label ID="lblErrMsg" runat="server" Visible="true"></asp:Label>
  </td>
  </tr>
  <tr>
  <td>
      <asp:Label ID="lblTotal" runat="server" Text="Total Count" Visible="False" ></asp:Label>
      <asp:Label ID="Label3" runat="server" Text=":"  Visible="False"></asp:Label>
      <asp:Label ID="lblTotal1" runat="server" Text=""  Visible="False"></asp:Label>

  </td>
   </tr>
    <tr>
  <td>
      <asp:Label ID="lblUpload" runat="server" Text="Upload Count" Visible="False"></asp:Label>
       <asp:Label ID="Label4" runat="server" Text=":"  Visible="False"></asp:Label>
      <asp:Label ID="lblUpload1" runat="server" Text=""  Visible="False"></asp:Label>
  </td>
   </tr>
    <tr>
  <td>
      <asp:Label ID="lblDuplicate" runat="server" Text="Internal Duplicate Count" Visible="False"></asp:Label>
       <asp:Label ID="Label6" runat="server" Text=":" Visible="False"></asp:Label>
      <asp:Label ID="lblDuplicate1" runat="server" Text=""  Visible="False"></asp:Label>
  </td>
   </tr>
    <tr>
   <td>
      <asp:Label ID="Label2" runat="server" Text="BatchID" Visible="False"></asp:Label>
       <asp:Label ID="Label5" runat="server" Text=":" Visible="False"></asp:Label>
      <asp:Label ID="lblBatchID" runat="server" Text=""></asp:Label>
  </td>

  </tr>
    <tr>
  <td>
      <asp:Label ID="lbl" runat="server" Text="External Duplicate Count" Visible="False"></asp:Label>
       <asp:Label ID="Label8" runat="server" Text=":" Visible="False"></asp:Label>
      <asp:Label ID="lblExternal" runat="server" Text=""></asp:Label>
  </td>
   </tr>
  </table>
</asp:Content>
