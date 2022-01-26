<?php

include('dbcon.php');

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

$name=$_POST['username1'];
$email=$_POST['email'];
$phone=$_POST['phone'];
$address=$_POST['address'];
$pass=$_POST['password1'];


//checking table Uname/Upassword 
$q="select * from table1 where Uname = '$name' && Upassword= '$pass' ";

$result= mysqli_query($con, $q);
$num=mysqli_num_rows($result);
if($num==1)
{
	//$_SESSION['Susername']= $name;
        //echo "User Exists";
        
        echo '<script>alert("User Already Exists.")</script>';

       // header('location:register.php');
}
else
{
    $qy= "insert into table1(Uname,email, phone, address,Upassword) values('$name','$email','$phone','$address','$pass')";
    mysqli_query($con,$qy);
    echo '<script>alert("Data Inserted Successfully")</script>' ;

}

?>