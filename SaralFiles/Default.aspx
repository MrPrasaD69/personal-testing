<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Front.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="saral._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .addbtn {
        box-shadow: 1px 1px black;
        }
         .btn {
        box-shadow: 1px 1px inset black;
        }

       
       
        </style>

    <div class="card">
        <div class="col-md-12">
            <div style="margin-bottom: 30px;">
                <h2 style="display: inline;">MY FORMS</h2>
                <%--<asp:Button ID="btnaddnew" runat="server" class="btn btn-danger" OnClick="btnaddnew_Click" style="display: inline; float: right" Text="Add New" />--%>

                <a href="AddEditForm.aspx" id="addbtn" class="btn btn-danger" style="display: inline; float: right">Add New Form</a>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label runat="server" ID="lblOutput"></asp:Label>
                </div>
            </div>

            <%--<h2 style="display: inline;">Logged IN User:
                    <asp:Label runat="server" ID="sesTitle"></asp:Label></h2>--%>
        </div>
    </div>
</asp:Content>

