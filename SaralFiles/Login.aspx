<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="saral.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-control, .btn {
            margin-top: 5px;
            margin-bottom: 15px;
        }

        label {
            
        }

        .searchPno {
            margin-left:auto;
            margin-right:auto;
        }

    </style>
    <div class="row">

        <div class="col-md-5">
        </div>


        <div class="col-md-6">
            <div runat="server" ID="searchDiv">
                <h3><b>LOGIN</b> TO SARAL:</h3>
                <div class="row">
                    <div class="col-md-4">
                        <center><asp:Label runat="server">Personal Number</asp:Label></center>
                        <asp:TextBox runat="server" ID="pNo" CssClass="form-control" Text=""></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                       <center> <asp:Label runat="server">Paschim Password</asp:Label></center>
                        <asp:TextBox runat="server" TextMode="Password" ID="password" CssClass="form-control" Text=""></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <center> <asp:Button runat="server" ID="searchPno" CssClass="btn btn-primary btn-lg " Text="Login" OnClick="searchPno_Click" /></center>
                        <br />
                        <asp:Label runat="server" ID="regMsg" ForeColor="Red"></asp:Label>
                        <%--<asp:Button runat="server" ID="Button1" CssClass="btn btn-primary" Text="Login" OnClick="Button1_Click" />--%>
                    </div>




                   
                </div>
            </div>            
        </div>

         
    </div>
</asp:Content>
