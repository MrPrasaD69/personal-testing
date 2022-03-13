<%@ Page Title="" Language="C#" MasterPageFile="~/Front.Master" AutoEventWireup="true" CodeBehind="FormViewData.aspx.cs" Inherits="saral.FormViewData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <style>
        #dashbtn {
        box-shadow: 1px 1px black;
        }
        #example{
            border:1px solid black;
        }
         .btn {
        box-shadow: 1px 1px inset black;
        }
               
        </style>

    <%--<a href="EXPORT.xlsx" download style="float:right" id="exp" class="btn btn-primary" >Export to Excel</a>--%>
    <asp:button runat="server" id="Excelbtn" text="Export to Excel" onclick="Excelbtn_Click" cssclass="btn btn-primary" style="float:right" ></asp:button>
    <asp:button runat="server" id="PDFbtn" text="Export to PDF" onclick="PDFbtn_Click" cssclass="btn btn-info" style="float:right" ></asp:button>
    <asp:Label runat="server" ID="lblOutput"></asp:Label>
    <asp:HiddenField runat="server" ID="uid" Value="0" />
    <asp:HiddenField runat="server" ID="fid" Value="0" />
    <asp:Button type="button" ID="dashbtn" CssClass="btn btn-info" runat="server" Text="DASHBOARD" OnClick="dashbtn_Click" ></asp:Button>
</asp:Content>
