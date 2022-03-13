<%@ Page Title="" Language="C#" MasterPageFile="~/Front.Master" AutoEventWireup="true" CodeBehind="SendFormPage.aspx.cs" Inherits="saral.SendFormPage" %>

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

        .container-Officers {
            border: 1px solid black;
        }

        #remoffbtn {
            width: 150px;
            position: relative;
            left: 138px;
        }

        #sendbtn {
            width: 150px;
            position: relative;
            left: 138px;
        }

        #l1 {
            font-size: 50px;
        }

        #offtxt {
            position: relative;
            margin-left: 170px;
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
        <div class="col-md-2">
            <asp:Label runat="server" ID="lblordering" Visible="false">
                <input type="hidden" id="tblcnt" value="1" />
                <table id="tblrouteorder">
                    <thead>
                        <tr>
                            <th>remove</th>
                            <th>name</th>
                            <th>re-order</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
                <%--<div style="margin: 20px;">
                    <asp:button runat="server" id="btncreateroute" cssclass="btn btn-danger" text="create route" onclick="btncreateroute_click" />
                </div>--%>
            </asp:Label>
        </div>
        <div class="col-md-5">
            <%--<select class="form-control">
                <option>Filter by Department</option>
                <option>Filter by Designation</option>
                <option>Filter by Name</option>
                <option>Filter by Personal No</option>
            </select>--%>

            <asp:Label runat="server">FILTER BY</asp:Label>
            <input type="search" id="search" name="search" placeholder="Search here...">
            <%--<asp:TextBox type="text" id="searchbox" class="form-control search-input" runat="server" placeholder="Search Here..."></asp:TextBox>--%>
            <br />
            <a href ="#" class="btn btn-primary" OnClick="searchusers();">Search</a>
            <%--<asp:Button ID="searchbtn" Text="SEARCH" runat="server" OnClick="searchbtn_Click" CssClass="btn btn-info" Style="margin-bottom: 20px;"></asp:Button>--%>
            <asp:Label runat="server" ID="lblOutput"></asp:Label>
            <asp:HiddenField runat="server" ID="uid" Value="0" />
            <asp:HiddenField runat="server" ID="fid" Value="0" />
            <%--<a id="btnForwardForm" class="btn btn-primary" href="#" onclick="btnClick();">Forward</a>--%>
            <a id="addoffbtn" class="btn btn-primary" href="#">Add Officers</a>
            <a id="dashbtn" class="btn btn-warning" href="Default.aspx">Dashboard</a>
            <%--<a href="#" id="addToRoute" onclick="addToRoute();" class="btn btn-primary">Add To Route</a>--%>



            <!--Forward Form Function-->
            <script>
                function btnClick() {
                    var val = [];
                    $('.container-Officers input:checkbox').each(function (i) {
                        val[i] = $(this).val();
                    });
                    var uid = $("#MainContent_uid").val();
                    var fid = $("#MainContent_fid").val();
                    console.log(val);
                    //return;
                    $.ajax({
                        type: "POST",
                        url: "SendFormPage.aspx/saveField",
                        data: JSON.stringify({ users: val, form: fid, sentby: uid }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            console.log(data);
                            window.location.href = "Inbox";
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



            <!--Test Func to Search Members-->
            <%--    <script>
                    function myFunc() {
                        var input, filter, table, tr, td, i, TxtValue;
                        input = document.getElementById("myInput");
                        filter = input.value.ToUpperCase();
                        table = document.getElementById("myTable");
                        tr = table.getElementsByTagName("tr");
                        for(i=0; i<tr.length;i++)
                        {
                            td = tr[i].getElementsByTagName("td"[0]);
                            if (td) {
                                txtValue = td.TextContent || td.innerText;
                                if(txtValue.ToUpperCase().indexOf(filter)>-1)
                                {
                                    tr[i].style.display="";
                                }
                                else {
                                    tr[i].stye.display = "";
                                }
                            }
                        }
                    }
                </script>--%>



            <!--test Func to search & filter users in Table-->
            <%--<script>
                (function(document) {
                    'use strict';
                    var TableFilter=(function(MyArray){
                        var search_input;

                        function _onInputSearch(e){
                            search_input=e.target;
                            var tables = document.getElementsByClassName(search_input.getAttribute('data-table'));
                            MyArray.forEach.call(table.tbodies,function(tbody){
                                MyArray.forEach.call(tbody.rows,function(row){
                                    var text_content = row.textContent.toLowerCase();
                                    var search_val =search_input.value.toLowerCase();
                                    row.style.display= text_content.indexOf(search_val) > -1 ? '':'none';
                                });
                            });
                        });
                    }

                    return {
                    init: function() {
                        var inputs = document.getElementsByClassName('search-input');
                        MyArray.forEach.call(inputs,function(input){
                            input.oninput = _onInputSearch;
                        });
                    }
                });
                })(Array.prototype);
                document.addEventListener('readystatechange',function()
                {
                if(document.readyState ==='complete') {
                    TableFilter.init();
                    }
                });
                    
                    
                })(document);
            </script>--%>

            <%--<script>
                var $rows = $('#table tr');
                $('#searchbox').keyup(function () {
                    var val = $.trim($(this).val()).replace(/t/g, '').toLowerCase();
                    $rows.show()().filter(function () {
                        var text = $(this).text().replace(/\st+/g, '').toLowerCase();
                        return !~text.indexOf(val);
                    }).hide();
                });
                </script>--%>



            <!-- Test func to add officers-->
            <script>
                $(document).ready(function () {
                    $('#addoffbtn').click(function () {
                        $('#MainContent_lblOutput input[type="checkbox"]:checked').each(function () {
                            //alert($(this).val());
                            $(this).prop('checked', false).closest('tr').appendTo($('.container-Officers'));
                        });
                    });

                    $('#remoffbtn').click(function () {
                        $('.container-Officers input[type="checkbox"]:checked').each(function () {
                            $(this).
                                prop('checked', false).
                                closest('tr').appendTo($('#MainContent_lblOutput table tbody'));
                        });
                    });
                });

            </script>



            <!--Function sends to officers-->


        </div>




        <%----------------------------------------------------------------------------------------------------------------------------------------%>
        <%--officers list after adding--%>

        <div class="col-md-5">
            <div class="container-Officers">
                <label id="offtxt">OFFICERS LIST</label>
                <div class="card">
                    <a id="sendbtn" class="btn btn-info" href="#" onclick="btnClick();">Send TO Officers</a>
                    <a id="remoffbtn" class="btn btn-danger" href="#">Remove Officers</a>
                </div>
            </div>
        </div>
        <asp:Label runat="server" ID="otherScripts"></asp:Label>
    </div>




    <div class="row">
        <div class="col-md-6">
        </div>
    </div>
    <%-- <div class="row">
            <div class="col-md-6">
                <label id="l1">OUTBOX</label>
                <asp:Label runat="server" ID="OutboxLabel"></asp:Label>

            </div>
        </div>--%>




    <script>
        function searchusers() {
            var searchText = $("#search").val();
            //alert(searchText);
            var val = [];
            $.ajax({
                type: "GET",
                url: "SendFormPage.aspx/searchUsers?searchText=",
                data: JSON.stringify({searchText: searchText}),
                contentType: "application/json; charset=utf-8",
                //dataType: "json",
                success: function (data) {
                    console.log(data);
                    //window.location.href = "Inbox";
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    //debugger;
                    console.log(xhr.responseText);
                    console.log(ajaxOptions);
                    console.log(thrownError);
                }
            });
            return false;
            $.ajax({
                type: "post",
                url: "SendFormPage.aspx/saveField",
                data: JSON.stringify({ searchText: searchText, formid:1 }),
                contenttype: "application/json; charset=utf-8",
                datatype: "json",
                success: function (data) {
                    console.log(data);
                    console.log(data.d);

                },
                error: function (xhr, ajaxoptions, thrownerror) {
                    debugger;
                    console.log(xhr.responsetext);
                    console.log(ajaxoptions);
                    console.log(thrownerror);
                }
            });
            return false;
        }

    </script>

    <%--<script>
        

   
        function getUniqueValuesFromColumn() {

            var unique_col_values_dict = {}

            allFilters = document.querySelectorAll(".table-filter")
            allFilters.forEach((filter_i) => {
                col_index = filter_i.parentElement.getAttribute("col-index");
                // alert(col_index)
                const rows = document.querySelectorAll("#offtable > tbody > tr")

                rows.forEach((row) => {
                    cell_value = row.querySelector("td:nth-child(" + col_index + ")").innerHTML;
                    // if the col index is already present in the dict
                    if (col_index in unique_col_values_dict) {

                        // if the cell value is already present in the array
                        if (unique_col_values_dict[col_index].includes(cell_value)) {
                            // alert(cell_value + " is already present in the array : " + unique_col_values_dict[col_index])

                        } else {
                            unique_col_values_dict[col_index].push(cell_value)
                            // alert("Array after adding the cell value : " + unique_col_values_dict[col_index])

                        }


                    } else {
                        unique_col_values_dict[col_index] = new Array(cell_value)
                    }
                });


            });

            for (i in unique_col_values_dict) {
                alert("Column index : " + i + " has Unique values : \n" + unique_col_values_dict[i]);
            }

            updateSelectOptions(unique_col_values_dict)

        };

        // Add <option> tags to the desired columns based on the unique values

        function updateSelectOptions(unique_col_values_dict) {
            allFilters = document.querySelectorAll(".table-filter")

            allFilters.forEach((filter_i) => {
                col_index = filter_i.parentElement.getAttribute('col-index')

                unique_col_values_dict[col_index].forEach((i) => {
                    filter_i.innerHTML = filter_i.innerHTML + `\n<option value="${i}">${i}</option>`
                });

            });
        };


        // Create filter_rows() function

        // filter_value_dict {2 : Value selected, 4:value, 5: value}

        function filter_rows() {
            allFilters = document.querySelectorAll(".table-filter")
            var filter_value_dict = {}

            allFilters.forEach((filter_i) => {
                col_index = filter_i.parentElement.getAttribute('col-index')

                value = filter_i.value
                if (value != "all") {
                    filter_value_dict[col_index] = value;
                }
            });

            var col_cell_value_dict = {};

            const rows = document.querySelectorAll("#offtable tbody tr");
            rows.forEach((row) => {
                var display_row = true;

                allFilters.forEach((filter_i) => {
                    col_index = filter_i.parentElement.getAttribute('col-index')
                    col_cell_value_dict[col_index] = row.querySelector("td:nth-child(" + col_index + ")").innerHTML
                })

                for (var col_i in filter_value_dict) {
                    filter_value = filter_value_dict[col_i]
                    row_cell_value = col_cell_value_dict[col_i]

                    if (row_cell_value.indexOf(filter_value) == -1 && filter_value != "all") {
                        display_row = false;
                        break;
                    }


                }

                if (display_row == true) {
                    row.style.display = "table-row"

                } else {
                    row.style.display = "none"

                }





            })

        }


        window.onload = () => {
            console.log(document.querySelector("#offtable > tbody > tr:nth-child(1) > td:nth-child(2) ").innerHTML);
        };

        getUniqueValuesFromColumn()

    </script>--%>

<%--
    <script>
        const searchInput = document.getElementById("search");
        const rows = document.querySelectorAll("tbody tr");
        //console.log(rows);
        searchInput.addEventListener('keyup', function (event) {
            //console.log(event);
            const q = event.target.value.toLowerCase();
            rows.forEach(row => {
                //console.log(row);
                row.querySelector('td').textContent.toLowerCase().startsWith(q) ? null : row.style.display = 'none';
            })
        })


    </script>--%>

</asp:Content>
