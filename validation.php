<?php

session_start();
$con = mysqli_connect('localhost','root');
if($con)
{
    echo "Connected";
}
else
{
    echo "Not Connected";
}
mysqli_select_db($con, 'students' );

$name=$_POST['name'];
$pass=$_POST['pass'];

//checking table Uname/Upassword 
$q="select * from table1 where Uname = '$name' && Upassword= '$pass' ";

$result= mysqli_query($con, $q);
$num=mysqli_num_rows($result);
if($num==1)
{
        echo "Log in success";
}
else
{
    $qy= "insert into table1(Uname,Upassword) values('$name',$pass)";
    mysqli_query($con, $qy);
}

?>