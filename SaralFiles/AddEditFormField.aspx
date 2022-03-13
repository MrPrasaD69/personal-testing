<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditFormField.aspx.cs" Inherits="saral.AddEditFormField" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="col-md-12">
            <div style="margin-bottom: 30px;">
                <asp:Label runat="server" ID="pageTitleDisplay" Font-Size="Large"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label runat="server" ID="lblOutput"></asp:Label>
                    <div class="row">
                        <div class="col-md-6"><label class="form-label">Field Name:</label></div>
                        <div class="col-md-6"><asp:TextBox required runat="server" ID="fieldName" class="form-control"></asp:TextBox></div>
                    </div>
                    <div class="row">
                        <div class="col-md-6"><label class="form-label">Field Type:</label></div>
                        <div class="col-md-6"><asp:DropDownList runat="server" ID="ddlFieldType" CssClass="form-control"></asp:DropDownList></div>
                    </div>
                    <div class="row">
                        <div class="col-md-6"><label class="form-label">Required Field:</label></div>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="requiredField" CssClass="form-control">
                                <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="0" Selected Text="No"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6"><label class="form-label">Field Order:</label></div>
                        <div class="col-md-6"><asp:TextBox runat="server" ID="fieldOrder" TextMode="Number" class="form-control"></asp:TextBox></div>
                    </div>
                    <div class="row">
                        <div class="col-md-6"><label class="form-label">&nbsp;</label><asp:HiddenField ID="EditID" runat="server" Value="0" /></div>
                        <div class="col-md-6"><asp:Button runat="server" ID="submitField" CssClass="btn btn-primary" OnClick="submitField_Click" Text="Submit" /> </div>
                        <div class="col-md-6"><asp:Button runat="server" ID="backbtn" CssClass="btn btn-primary" OnClick="backbtn_Click" Text="Back" /> </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
