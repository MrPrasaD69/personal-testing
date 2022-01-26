<?php

include('dbcon.php');
session_start();


$con = mysqli_connect('localhost','root','');
if($con)
{
    echo "Connected";
}
else
{
    echo "Not Connected";
}
mysqli_select_db($con, 'students' );

$name=$_POST['username'];
$pass=$_POST['password'];

//checking table Uname/Upassword 
$q="select * from table1 where Uname = '$name' && Upassword= '$pass' ";

$result= mysqli_query($con, $q);
$num=mysqli_num_rows($result);
if($num==1)
{
	$_SESSION['username']= $name;
        echo "Log in success";
        header('location:index.php');
}
else
{
    echo '<script>alert("Invalid Credentials")</script>';
}

?>