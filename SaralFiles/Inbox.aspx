<%@ Page Title="" Language="C#" MasterPageFile="~/Front.Master" AutoEventWireup="true" CodeBehind="Inbox.aspx.cs" Inherits="saral.Inbox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        table input, table select, table textarea {
            border: 0;
            width: 100%;
        }

        td, th {
            border: 1px solid #cacaca;
            padding: 5px;
        }

        input[type="checkbox"] {
            width: 14px;
            margin: 5px auto;
        }
        body {
            background-color:#f8f8f8;
        }
        table tbody {
        border: 2px solid black;
        }
        table td {
        border:2px solid black;
        }
        .btn {
        box-shadow: 1px 1px inset black;
        }
        h3 {
        font-size:30px;
        color: #000;
        text-shadow: 1px 1px black;
        }
        table{
            table-layout:auto; 
        }
    </style>

    <asp:Label runat="server" ID="formTitle"><h3>INBOX</h3></asp:Label>
            <asp:HiddenField runat="server" ID="uid" Value="0" />
            <asp:HiddenField runat="server" ID="fid" Value="0" />
    <br />
    <div class="row">
        <div class="col-md-6">
            <asp:Label runat="server" ID="lblOrdering"></asp:Label>
        </div>
      
        <div class="col-md-12">
            <asp:Label runat="server" ID="lblOutput"></asp:Label>
            <%--<asp:Button runat="server" ID="checkstatus" OnClick="checkstatus_Click" Text="Check" CssClass="btn btn-info"></asp:Button>--%>
            
        </div>
    </div>
    <asp:Label runat="server" ID="otherScripts"></asp:Label>
    <%--<a href="Default" class="btn btn-danger">Admin Panel</a>--%>
</asp:Content>
