<%@ Page Title="" Language="C#" MasterPageFile="~/Front.Master" AutoEventWireup="true" CodeBehind="Outbox.aspx.cs" Inherits="saral.Outbox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        table, td,th{
            border: 2px solid grey;
        }
        #ViewBtn{
           
            float:right;            
        }
          .btn {
        box-shadow: 1px 1px inset black;
        }
          td,th{
              text-align:center;
          }
        </style>
    <asp:HiddenField runat="server" ID="uid" Value="0" />
    <asp:HiddenField runat="server" ID="fid" Value="0" />
     <asp:HiddenField runat="server" ID="form_id" />
        <div class="card">          
        <div class="col-md-12">          
            <div style="margin-bottom: 30px;">               
                <h2 style="display: inline;">OUTBOX</h2>                             
            </div>
            <div class="row">               
                <div class="col-md-12">
                    <asp:Button runat="server" ID="ViewBtn" Text="View All Data" CssClass="btn btn-info" OnClick="ViewBtn_Click"></asp:Button>                  
                    <asp:Label runat="server" ID="lblOutput"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
