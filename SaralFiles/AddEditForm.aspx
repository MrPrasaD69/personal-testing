<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditForm.aspx.cs" Inherits="saral.AddEditForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="card">
        <div class="col-md-12">
            <div style="margin-bottom: 30px;">
                
                <h2 style="display: inline;">Form:
                    <asp:Label runat="server" ID="formTitle"></asp:Label></h2>
                <%--<a href="AddEditFormField" id="AddFieldBtn" runat="server" class="btn btn-danger" style="display: inline; float: right">Add New Field</a>--%>

                <asp:Button   id="AddFieldBtn" runat="server" Text="AddNewField" CssClass="btn btn-primary" style="display: inline; float: right" OnClick="AddFieldBtn_Click" />
            
                
                <%--Hidden field to query string id--%>
                <asp:HiddenField runat="server" ID="form_id" />


            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label runat="server" ID="lblOutput"></asp:Label>
                    <div class="col-md-6" runat="server" id="addDiv" visible="false">
                        <div class="row">
                            <div class="col-md-6">
                                <label class="form-label">Form Title:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="FTitle" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label class="form-label">Form Subtitle:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="FSubtitle" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label class="form-label">Description:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="FDescription" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label class="form-label">&nbsp;</label>
                            </div>
                            <div class="col-md-6">
                                <asp:Button runat="server" ID="submitForm" CssClass="btn btn-primary" OnClick="submitForm_Click" Text="Submit" />
                                <asp:Button   id="Button2" runat="server" Text="Go Back" CssClass="btn btn-primary" style="display: inline; float: right" OnClick="Button2_Click" />
                            </div>
                        </div>
                    </div>
                     <%--<asp:Button runat="server" CssClass="btn btn-primary" Text="Dashboard" ID="dashbtn" OnClick="dashbtn_Click" ></asp:Button>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
