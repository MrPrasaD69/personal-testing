<?php
include('dbcon.php');
$stu_id = $_GET['id'];
//echo "\n $stu_id";
$sqli = mysqli_connect("localhost", "root", "", "students") or die("Could not connect database...");
$q="delete from table1 where ID = '$stu_id' ";
$result=mysqli_query($sqli,$q);
if($result)
{
	echo "DELETED";
}
else
{
	echo "Error : ".mysqli_error($con);
}


?>