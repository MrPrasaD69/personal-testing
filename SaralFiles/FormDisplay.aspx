<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormDisplay.aspx.cs" Inherits="saral.FormDisplay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-6 col-sm-12">
                        <div id="divTable" runat="server">

                        </div>
                    </div>
                <//div>
            </div>
    
            <asp:GridView ID="GridView1" runat="server" Visible="false"></asp:GridView>
            <asp:Label runat="server" ID="lblOutput"></asp:Label>
            <asp:HiddenField runat="server" ID="form_id" />


           

     
</asp:Content>
