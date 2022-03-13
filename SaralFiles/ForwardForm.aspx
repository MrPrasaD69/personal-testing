<%@ Page Title="" Language="C#" MasterPageFile="~/Front.Master" AutoEventWireup="true" CodeBehind="ForwardForm.aspx.cs" Inherits="saral.ForwardForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        table input, table select, table textarea {
            border: 0;
            width: 100%;
        }

        td, th {
            border: 3px solid #cacaca;
            padding: 5px;
        }

        input[type="checkbox"] {
            width: 14px;
            margin: 5px auto;
        }
    </style>
    <script>
        function checkMe(v) {
            var cnt = $("#tblCnt").val();
            $("#tblRouteOrder tbody").append("<tr id='c" + cnt + "'><td id='td" + v + "'><input type='hidden' name='tr" + cnt + "' id='tr" + cnt + "' value='" + v + "' /><input type='hidden' id='ct" + v + "' value='" + cnt + "' /> X </td><td>" + $("#val" + v).val() + "</td><td><a href='#' onclick='moveup(" + v + ", " + cnt + ", \"" + $("#val" + v).val() + "\")'>Move Up</a> &nbsp; | &nbsp; <a href='#' disabled onclick='movedown(" + v + ")'>Move Down</a></td></tr>");
            $("#tblCnt").val(+cnt + 1);
        }

        function moveup(v, num, name) {
            //get values of num in temp
            //copy values to num - 1
            //copy values of temp to num
            //var temp = 
            var cnt = num;
            var currCnt = num;
            var prevCnt = num - 1;
            var parentID = $("#tr" + prevCnt).val();
            //parentID = $("#td" + v).parent().prev().html();
            console.log($("#td" + v).parent().prev().html());
            var prevHTML = $("#c" + prevCnt).html();
            var currHTML = $("#c" + currCnt).html();

            $("#c" + prevCnt).html(currHTML);
            $("#c" + currCnt).html(prevHTML);
            return;

            $("#td" + v).parent().html(prevHTML);
            $("#td" + parentID).parent.html();

            var temp = $("#td" + v).parent().prev().html();
            console.log(temp);
            $("#td" + v).parent().html("<tr id=''><td id='td" + v + "'><input type='hidden' id='tr" + v + "' value='" + cnt + "' /> X </td><td>" + $("#val" + v).val() + "</td><td><a href='#' onclick='moveup(" + v + ", " + cnt + ", \"" + $("#val" + v).val() + "\")'>Move Up</a> &nbsp; | &nbsp; <a href='#' disabled onclick='movedown(" + v + ")'>Move Down</a></td></tr>");
            $("#td" + v).parent().html(temp);
            //var temp = $("#td" + v).parent().html();
            //var pr = $("#td" + v).parent();//.child("td:first");
            //console.log(pr);
            //$("#td" + v).parent().remove();
            //pr[0].append(temp);
        }
        function movedown(v) {
            var temp = $("#tr" + v).html();
            $("#tr" + v).remove();
        }
    </script>
    <asp:Label runat="server" ID="formTitle"></asp:Label>
    <br />
    <%--<asp:DropDownList runat="server" ID="ddlDept">
        <asp:ListItem Text="" Value=""></asp:ListItem>
    </asp:DropDownList>--%>
    <div class="row">
        <div class="col-md-5">
            <asp:Label runat="server" ID="lblOrdering" Visible="false">
                <input type="hidden" id="tblCnt" value="1" />
                <table id="tblRouteOrder">
                    <thead>
                        <tr>
                            <th>Remove</th>
                            <th>Name</th>
                            <th>Re-order</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
                <div style="margin: 20px;">
                    <asp:Button runat="server" ID="btnCreateRoute" CssClass="btn btn-danger" Text="Create Route" OnClick="btnCreateRoute_Click" />
                </div>
            </asp:Label>
        </div>
        <div class="col-md-7">
            <select class="form-control">
                <option>Filter by Department</option>
                <option>Filter by Designation</option>
                <option>Filter by Name</option>
                <option>Filter by Personal No</option>
            </select>
            <br />
            <asp:Label runat="server" ID="lblOutput"></asp:Label>
            <asp:HiddenField runat="server" ID="uid" Value="0" />
            <asp:HiddenField runat="server" ID="fid" Value="0" />
            <a id="btnForwardForm" class="btn btn-primary" href="#" onclick="btnClick();">Forward</a>
            <a id="dashbtn" class="btn btn-warning" href="Default.aspx" >Dashboard</a>
            <%--<a href="#" id="addToRoute" onclick="addToRoute();" class="btn btn-primary">Add To Route</a>--%>
            <script>
                function btnClick() {
                    var val = [];
                    $(':checkbox:checked').each(function (i) {
                        val[i] = $(this).val();
                    });
                    var uid = $("#MainContent_uid").val();
                    var fid = $("#MainContent_fid").val();
                    console.log(val);
                    $.ajax({
                        type: "POST",
                        url: "ForwardForm.aspx/saveField",
                        data: JSON.stringify({ users: val, form: fid, sentby: uid }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            console.log(data);
                            window.location.href = "Inbox";
                            //console.log(data.d);
                            //if (data.d > 0) {
                            //    thiss.attr("data-id", data.d);
                            //    thiss.closest('tr').children().children().attr("data-id", data.d);
                            //}
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            debugger;
                            console.log(xhr.responseText);
                            console.log(ajaxOptions);
                            console.log(thrownError);
                        }
                    });
                }
            </script>
        </div>
        
    </div>
    <asp:Label runat="server" ID="otherScripts"></asp:Label>
</asp:Content>
