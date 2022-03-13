<%@ Page Title="" Language="C#" MasterPageFile="~/Front.Master" AutoEventWireup="true" CodeBehind="FieldTable.aspx.cs" Inherits="saral.FieldTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
                     <asp:HiddenField runat="server" ID="HiddenField1" Value="0" />
                    <asp:HiddenField runat="server" ID="HiddenField2" Value="0" />
    
 <style>
     #namebox{
         width:200px;
     }
     #numbox{
         width:80px;
     }
        .addBtn {
            font-size:30px;
            margin-bottom: 5px;
            float:right;
            line-height: 15px;
            padding: 10px;
        }
         .btn {
        box-shadow: 1px 1px inset black;
        }
       
    </style>
    <div class="card">
                    <h2 style="display: inline;">Form:
                    <asp:Label runat="server" ID="formTitle"></asp:Label></h2>
                     
                <%--Hidden field to query string id--%>
                <asp:HiddenField runat="server" ID="form_id" />
                
        <div class="col-md-12">
            <div style="margin-bottom: 30px;">
                <%--<h2 style="display: inline;">My Forms</h2>
                <a href="AddEditForm" class="btn btn-danger" style="display: inline; float: right">Add New</a>--%>




               <asp:HiddenField ID="EditID" runat="server" Value="0" />








            </div>
            <div class="row">
                
                <div class="col-md-12">
                    <%--ShowHeaderWhenEmpty="true"--%>
                    <%--OnRowDataBound="FieldGridView_RowDataBound"--%> 
                   
                    <asp:Label runat="server" ID="lblOutput"></asp:Label>
                    <asp:HiddenField runat="server" ID="uid" Value="0" />
                    <asp:HiddenField runat="server" ID="fid" Value="0" />
                   
                    
                     <asp:GridView ID="FieldGridView" 
                     runat="server" BackColor="White"     BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                     CellPadding="3" Height="153px" OnSelectedIndexChanged="FieldGridView_SelectedIndexChanged" HorizontalAlign="Center"
                     AutoGenerateColumns="false" ShowFooter="true" 
                     OnRowCommand="FieldGridView_RowCommand" OnRowEditing="FieldGridView_RowEditing" 
                     OnRowCancelingEdit="FieldGridView_RowCancelingEdit"  OnRowDeleting="FieldGridView_RowDeleting"
                     OnRowUpdating="FieldGridView_RowUpdating"   
                         DataKeyNames  ="Id">
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />

                     <Columns>
                      
                         




                      <%----------------------------------------------------------------------------------------------%>

                         <%-- 1 FieldName--%>
                            <asp:TemplateField  HeaderText="Field Name" >

                                <ItemTemplate>
                                    <asp:Label ID="Label1" Text='<%# Bind("Label") %>'  runat="server"/>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox ID="Nametxt" Text='<%# Bind("Label") %>' runat="server"></asp:TextBox>
                                </EditItemTemplate>

                                <FooterTemplate>
                                    <asp:TextBox ID="FooterFieldNametxt" Placeholder="Enter Field Name" runat="server"></asp:TextBox>
                                </FooterTemplate>

                            </asp:TemplateField>
                          <%----------------------------------------------------------------------------------------------%>

                            <%-- 2 FieldType--%>
                         <asp:TemplateField HeaderText="Type" >
                                <ItemTemplate>  
                                    <asp:Label ID="Label2" Text='<%# Bind("FieldTypeLabel") %>' runat="server"/>
                                </ItemTemplate>
                                
                                 <EditItemTemplate>  
                                <asp:TextBox ID="Typetxt" Text='<%# Bind("FieldTypeLabel") %>' runat="server"></asp:TextBox>  
                                </EditItemTemplate>  

                                <EditItemTemplate>  
                                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("FieldType") %>'>  
                                        <asp:ListItem>--Select Field Type--</asp:ListItem>  
                                        <asp:ListItem Value="1">Open</asp:ListItem>  
                                        <asp:ListItem Value="2">Number</asp:ListItem> 
                                        <asp:ListItem Value="3">Date</asp:ListItem> 
                                        <asp:ListItem Value="4">Time</asp:ListItem> 
                                    </asp:DropDownList>  
                                </EditItemTemplate>

                                <FooterTemplate>
                                    <%--<asp:TextBox ID="FooterTypetxt"  runat="server"></asp:TextBox>--%>
                                    <asp:DropDownList ID="FooterTypetxt" runat="server" >  
                                        <asp:ListItem>--Select Field Type--</asp:ListItem>  
                                        
                                        <asp:ListItem Value="1">Open</asp:ListItem>  
                                        <asp:ListItem Value="2">Number</asp:ListItem> 
                                        <asp:ListItem Value="3">Date</asp:ListItem> 
                                        <asp:ListItem Value="4">Time</asp:ListItem> 
                                    </asp:DropDownList>  
                                </FooterTemplate>

                        </asp:TemplateField>



                               <%-- <EditItemTemplate>
                                    <asp:TextBox ID="Typetxt" Text='<%# Eval("FieldType") %>' runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="FooterTypetxt"  runat="server"></asp:TextBox>
                                </FooterTemplate>--%>
                                
                          <%----------------------------------------------------------------------------------------------%>

                            <%-- 3 Order--%>
                         <asp:TemplateField HeaderText="Order" >
                                <ItemTemplate>
                                    <asp:Label ID="Label3" Text='<%# Bind("FieldOrder") %>' runat="server"/>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox ID="OrderTxt" Text='<%# Bind("FieldOrder") %>' runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <EditItemTemplate>  
                                   <asp:TextBox runat="server" ID="fordertxt" Text='<%# Bind("FieldOrder") %>' ></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="FooterOrderTxt" Placeholder="Enter Field Order" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                          <%----------------------------------------------------------------------------------------------%>

                            <%-- 4 Required--%>
                         <asp:TemplateField HeaderText="Required" >
                                <ItemTemplate>
                                    <asp:Label ID="Label4" Text='<%# Bind("isRequired") %>' runat="server"/>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox ID="RequiredTxt" Text='<%# Bind("isRequired") %>' runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                
                                <EditItemTemplate>
                                        <asp:DropDownList runat="server" ID="reqDropDown" SelectedValue='<%# Bind("isRequired") %>' >
                                            <asp:ListItem>--Select Required Type--</asp:ListItem>
                                            <asp:ListItem name="yes" Value="True">Yes</asp:ListItem>
                                            <asp:ListItem name="no" Value="False" >No</asp:ListItem>
                                        </asp:DropDownList>
                                </EditItemTemplate>

                                <FooterTemplate>
                                    <%--<asp:TextBox ID="FooterRequiredTxt"  runat="server"></asp:TextBox>--%>
                                    <asp:DropDownList runat="server" ID="FooterRequiredTxt" >
                                            <asp:ListItem>--Select Required Type--</asp:ListItem>
                                            <asp:ListItem name="true" Value="True">Yes</asp:ListItem>
                                            <asp:ListItem name="false" Value="False" >No</asp:ListItem>
                                        </asp:DropDownList>
                                </FooterTemplate>
                          </asp:TemplateField>
                         
                         <%----------------------------------------------------------------------------------------------%>

                            <%-- 5 Action Menu--%> 
                         <asp:TemplateField HeaderText="Action">
                             <ItemTemplate>
                                 <asp:ImageButton ImageUrl="~/img/edit.jpg" CommandName="Edit" ToolTip="Edit" Width="50px" Height="50px" runat="server"></asp:ImageButton>
                                 <asp:ImageButton ImageUrl="~/img/trash.jpg" CommandName="Delete" ToolTip="Delete" Width="50px" Height="50px" runat="server"></asp:ImageButton>
                             </ItemTemplate>

                              <EditItemTemplate>
                                <asp:ImageButton ImageUrl="~/img/save.jpg" CommandName="Update" ToolTip="Update" Width="50px" Height="50px" runat="server"></asp:ImageButton>      
                                <asp:ImageButton ImageUrl="~/img/cancel.jpg" CommandName="Cancel" ToolTip="Cancel" Width="50px" Height="50px" runat="server"></asp:ImageButton>      
                              </EditItemTemplate>
                             <FooterTemplate>
                                 <asp:ImageButton ImageUrl="~/img/add.jpg" CommandName="AddNew" ToolTip="Add New" Width="50px" Height="50px" runat="server"></asp:ImageButton>         
                             </FooterTemplate>
                         </asp:TemplateField>

                     </Columns>




                </asp:GridView>
                <br/>
                <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
                <br />
                <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
                    
                    
                </div>
            </div>
            <asp:Button type="button" ID="dashbtn" CssClass="btn btn-info" runat="server" Text="DASHBOARD" OnClick="dashbtn_Click" ></asp:Button>
        </div>
    </div>




                            <%-- 2 FieldType--%>
                         <%--<asp:TemplateField HeaderText="Type" >
                                <ItemTemplate>
                                    <asp:Label Text='<%# Eval("FieldType") %>' runat="server"/>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox ID="Typetxt" Text='<%# Eval("FieldType") %>' runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="FooterTypetxt"  runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>--%>






   <%-- <script type="text/javascript">
        function addNewRow() {
            $("#MainContent_lblOutput table tbody").append('<tr><td></td></tr>');
        }
        function saveField(id, fld, thiss, fid, uid) {
            if (id == 0) {
                //alert("ID is 0");
                if (thiss.data("id") !== undefined) {
                    //alert("DATA ID is defined");
                    id = thiss.data("id");
                }
            }
            console.log(id + " => " + fld + " => " + fid);
            console.log(thiss[0].value);
            var fldVal = thiss[0].value;
            //debugger;
            if (fldVal.length > 0) {
                $.ajax({
                    type: "POST",
                    url: "FormView.aspx/saveField",
                    data: JSON.stringify({ id: id, field: fld, fldVal: fldVal, form: fid, user: uid }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        console.log(data.d);
                        if (data.d > 0) {
                            thiss.attr("data-id", data.d);
                            thiss.closest('tr').children().children().attr("data-id", data.d);
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        debugger;
                        console.log(xhr.responseText);
                        console.log(ajaxOptions);
                        console.log(thrownError);
                    }
                });
            }
            return false;
        }
    </script>--%>
</asp:Content>
