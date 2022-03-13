<%@ Page Title="" Language="C#" MasterPageFile="~/Front.Master" AutoEventWireup="true" CodeBehind="ViewUserForm.aspx.cs" Inherits="saral.ViewUserForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #dashbtn {
        box-shadow: 1px 1px black;
        }
        #example{
            border:1px solid black;
        }
        
  
        </style>

                 
    <asp:Label runat="server" ID="lblOutput"></asp:Label>
    <asp:HiddenField runat="server" ID="uid" Value="0" />
    <asp:HiddenField runat="server" ID="fid" Value="0" />
    <asp:Button runat="server" ID="backbtn" CssClass="btn btn-info" Text="Go Back" OnClick="backbtn_Click"></asp:Button>
    
</asp:Content>
