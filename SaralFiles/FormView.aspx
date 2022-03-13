<%@ Page Title="" Language="C#" MasterPageFile="~/Front.Master" AutoEventWireup="true" CodeBehind="FormView.aspx.cs" Inherits="saral.FormView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .addBtn {
            font-size:30px;
            margin-bottom: 5px;
            float:right;
            line-height: 15px;
            padding: 10px;
        }
    </style>
    <div class="card">
        <div class="col-md-12">
            <div style="margin-bottom: 30px;">
                <%--<h2 style="display: inline;">My Forms</h2>
                <a href="AddEditForm" class="btn btn-danger" style="display: inline; float: right">Add New</a>--%>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <%--<a href="#" onclick="addNewRow();" class="btn btn-danger" style="font-size:30px;margin-bottom: 5px;float:right;line-height: 15px;padding: 10px;"> + </a>--%>
                    <asp:Button ID="addBtn" OnClientClick="addNewRow(); return false;" class="btn btn-danger addBtn" Text="+" runat="server" />
                    <asp:Label runat="server" ID="lblOutput"></asp:Label>
                    <asp:HiddenField runat="server" ID="uid" Value="0" />
                    <asp:HiddenField runat="server" ID="fid" Value="0" />
                    <asp:Button runat="server" ID="forwardBtn" OnClick="forwardBtn_Click" Text="Forward" CssClass="btn btn-primary" />
                    <%--<asp:Button runat="server" ID="SubmitForAuthbtn" OnClientClick="confirmpopup();" OnClick="SubmitForAuthbtn_Click" Text="Submit For Authentication" CssClass="btn btn-info" />--%>
                    <a href="#"  ID="SubmitForAuthbtn" onClick="confirmpopup();"  Class="btn btn-info" >Submit For Authentication</a>
                    <a href="Inbox.aspx"  ID="backinboxbtn"  Class="btn btn-primary" >Back To Inbox </a>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
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
    </script>

    <!--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////-->
    <!--POP UP CONFIRMATION FUNC-->
    <script>
        var object = { status: false, ele: null };
        function confirmsubmit(ev) {

            if (object.status) { return true; };


            swal({
                title: "are you sure?",
                text: "you will not be able to make changes later!",
                type: "warning",
                showcancelbutton: true,
                confirmbuttonclass: "btn-warning",
                confirmbuttontext: "yes, i'm sure!",
                closeonconfirm: true
                
            },
                function ()
                {
                    object.status = true;
                    object.ele = ev;
                    object.ele.click();
                });
        };
    </script>

    <!--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////-->
    <!--POP UP CONFIRMATION FUNC-->

    <%--<script>

        function submitalert() {
            Swal.fire(
                'Good job!',
                'You clicked the button!',
                'success'
            )
        }

    </script>--%>

    <script>
        function confirmpopup(){
            var x = confirm("Are you Sure? You will not be able to make changes later!");
            //alert(x);
            var uid = $("#MainContent_uid").val();
            var fid = $("#MainContent_fid").val();
            if (x) {
                //alert("abc");
                $.ajax({
                    type: "POST",
                    url: "FormView.aspx/SubmitForAuthbtn_Click",
                    data: JSON.stringify({ form: fid, user: uid }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        console.log(data.d);

                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        debugger;
                        console.log(xhr.responseText);
                        console.log(ajaxOptions);
                        console.log(thrownError);
                    }
                });
            }
            //else if (x == false)
            //{
            //    alert(x);
            //}
            else {
                alert(x);
                return false;   
            }
            return false;
        }
        
    </script>

     <%--<script>
         function confirmpopup() {
             var x = confirm("Are you Sure? You will not be able to make changes later!");

             
             if (x == true) {
                 $.ajax({
                     type: "POST",
                     url: "FormView.aspx/SubmitForAuthbtn_Click",
                     //data: JSON.stringify({ id: id, field: fld, fldVal: fldVal, form: fid, user: uid }),
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (data) {
                         console.log(data);
                         console.log(data.d);

                     },
                     error: function (xhr, ajaxOptions, thrownError) {
                         debugger;
                         console.log(xhr.responseText);
                         console.log(ajaxOptions);
                         console.log(thrownError);
                     }
                 });
             }
             //else if (x == false)
             //{
             //    alert(x);
             //}
             else {
                 return false;
             }
         }

     </script>--%>



    <%--<script src="--https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
        <script>
            function submitalert() {
                swal({
                    title: "Are you sure?",
                    text: "You will not be able to recover this imaginary file!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Yes, delete it!",
                    cancelButtonText: "No, cancel plx!",
                    closeOnConfirm: false,
                    closeOnCancel: false
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            swal("Deleted!", "Your imaginary file has been deleted.", "success");
                        } else {
                            swal("Cancelled", "Your imaginary file is safe :)", "error");
                        }
                    });
            }
        </script>--%>





            <%--    //if (x == true) {
            //    document.write("You have Submitted the form");
            //    //window.location.href = "FormView?formid=".formid;
            //}
            //else {
            //    document.write("You have Not Submitted the form");
            //    //window.location.href = "FormView?formid="+ fid.Value;
            //}--%>


</asp:Content>
