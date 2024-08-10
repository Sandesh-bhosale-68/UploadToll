<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Visual_IVR_Blaster.aspx.cs" Inherits="Visual_IVR_Blaster" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            height: 21px;
        }
        .auto-style2 {
            width: 199px;
        }
        .auto-style3 {
            width: 112px;
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
  <td class="auto-style3">
      <asp:Button ID="btnUpload" runat="server" Text="Upload" 
          onclick="btnUpload_Click" style="height: 26px" />
  
  </td>
 <%-- <td>
      <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
          onclick="btnCancel_Click" />
  </td>--%>
  <td>
      <u ><asp:HyperLink ID="hlpricingdocmay" runat="server" NavigateUrl="~/UploadFormat/IVR_Bluster.xls" Target="_blank">Please Download_Upload_Format</asp:HyperLink></u>
      
  </td>
  </tr>
  <tr>
  <td class="auto-style1">
      <asp:Label ID="lblErrMsg" runat="server" Visible="true"></asp:Label>
  </td>
     
   <td class="auto-style1">
      <asp:Label ID="Label2" runat="server" Visible="true" ForeColor="green"></asp:Label>
  </td>
  </tr>
        <tr>
    <td class="auto-style1">
  <asp:Label ID="lbltransfer" runat="server" class="auto-style2" Visible="false" Font-Bold="true" Text="Transfer To Calling List"></asp:Label>

  </td>
   </tr>
  <tr>
      <td>
      

  </td>
 <td>
      
  </td>
   </tr>
    <tr>
     <td>
      <%--
     <asp:Label ID="lblbatchid" runat="server" Visible="true" Text="Batchid" Font-Bold="true"></asp:Label>
  </td>
  <td class="auto-style3">

     <asp:DropDownList ID="ddlbatchname" runat="server" Visible="true" Width="200"></asp:DropDownList>
  </td>
   </tr>

    <tr>
     <td>

     <asp:Label ID="Label3" runat="server"  Text="Select Table Tranfer to  calling " Font-Bold="true"></asp:Label>
  </td>
  <td class="auto-style3">

     <asp:DropDownList ID="ddl_Campaign_Name" runat="server"  Width="200">
         <asp:ListItem>--Select---</asp:ListItem>
           <asp:ListItem>TMFLC_Early_stage</asp:ListItem>
             <asp:ListItem>TMFL_COLLECTION_WC</asp:ListItem>
     </asp:DropDownList>
  </td>
   </tr>

    <tr>
        
  <td>
      <asp:Button ID="btntransfer" runat="server" Text="TRANSFER" Visible="true"
          onclick="btntransfer_Click" />
      
  </td>
   </tr>
    <tr>
   <td>
      <asp:Label runat="server" ID="lbltrans" Font-Bold="true" ForeColor="Yellow" Font-Size="Small" Visible="false"></asp:Label>
  </td>

  </tr>
    <tr>
  <td>
     
  </td>
   </tr>--%>
  </table>
</asp:Content>
