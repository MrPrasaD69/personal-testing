<?php
	session_start();
	$sqli = mysqli_connect("localhost", "root", "", "students") or die("Could not connect database...");
	$update = false;
	$id = $name = $email = "";
	 if (isset($_REQUEST['edit'])) {
		$id = $_REQUEST['edit'];
		$update = true; 
		$record = mysqli_query($sqli, "SELECT * FROM table1 WHERE SubID=$id"); 

		if (($record) == 1 ) 
		{
			$num = mysqli_fetch_array($record);
			$id = $num['SubID'];
			$name = $num['Uname'];
			$email = $num['Email'];
		}

	}
	if(isset($_REQUEST['save'])){
		//$id = $_REQUEST['ID'];
		$name = $_REQUEST['name'];
		$email = $_REQUEST['email'];
		$phone = $_REQUEST['phone'];
		$address = $_REQUEST['address'];
		mysqli_query($sqli, "INSERT INTO `table1` (`Uname`, `Email`, `Phone`, `Address`) VALUES ('$name', '$email', '$phone','$address')");
		$_SESSION['msg'] = "Employee Saved";
		header("location:testpage.php");

	}
	if(isset($_REQUEST['update'])){
		//$id = $_REQUEST['id'];
		$name = $_REQUEST['name'];
		$email = $_REQUEST['email'];
		$phone = $_REQUEST['phone'];
		$address = $_REQUEST['address'];

		mysqli_query($sqli, "UPDATE table1 SET Uname = '$name', Email= '$email' , Phone = '$phone' , Address= '$address'  WHERE ID = $id");
		$_SESSION['msg']= "Data Updated.";
		header("location:testpage.php");
		
	}
	if(isset($_REQUEST['del'])){
		$id = $_REQUEST['del'];
		mysqli_query($sqli, "DELETE FROM table1 WHERE ID = $id");
		$_SESSION['msg'] = "Employee Data is deleted";
		header("location:testpage.php");
	}


?>

<style>
	.container-fluid .row .table td, th, td
	{
		border:  1px solid black;
	}
</style>

<!DOCTYPE html>
<html>
<head>
	<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
	<script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
	<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
	<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
	<title>ADMIN Dashboard</title>
	<link rel="stylesheet" type="text/css" href="style.css">
	<script type="text/javascript">

		function clear(){
			document.getElementsByTagName('ID').clear();
			document.getElementsByTagName('Uname').clear();
			document.getElementsByTagName('Email').clear();
		}
		function back(){
			 window.location.href = "https://www.google.com";
		}
	</script>
</head>
<body>	
		<?php

			if(isset($_SESSION['msg'])){
				echo "<div class='msg'>";
				echo $_SESSION['msg'];
				unset($_SESSION['msg']);
				echo "</div>";
			}
			$query = mysqli_query($sqli, "SELECT * FROM table1");
		?>
		<div class="container-fluid">
			<div class="row">
				<div class="col-md-7">
				<div class="table">
				<table>
				<tr>
					<td>SubID</td>
					<td>Name</td>
					<td>Email</td>
					<td>Phone</td>
					<td>Address</td>
					<td colspan="2">Action</td>
				</tr>
				<?php
					while($row = mysqli_fetch_array($query)){
						echo "<tr>";
						echo "<td>" .$row['SubID']. "</td>";
						echo "<td>".$row['Uname']."</td>";
						echo "<td>".$row['Email']."</td>";
						echo "<td>".$row['Phone']."</td>";
						echo "<td>".$row['Address']."</td>";
						
						echo "<td><a class='link' href=testpage.php?edit=".$row['SubID'].">Edit</a></td>";
						echo "<td><a class='link' href=testpage.php?del=".$row['SubID'].">Delete</a></td>";
						echo "</tr>";
					}
				?>
			</table>
			</div>
			<form action="testpage.php" method="POST">
			<!-- <div class="input-group">
				<label>ID</label>
				<input type="text" name="id" value=""> 
			</div> -->
			<div class="input-group">
				<label>Name</label>
				<input type="text" name="name" value="">
			</div>
			<div class="input-group">
				<label>Email</label>
				<input type="text" name="email" value="">
			</div>
			<div class="input-group">
				<label>Phone</label>
				<input type="text" name="phone" value="">
			</div>
			<div class="input-group">
				<label>Address</label>
				<input type="text" name="address" value="">
			</div>
			<div class="input-group">
				<?php if($update == true) { ?>
				 <button class="btn" type="submit" name="update" style="background: red;" onclick="back()"> Update </button>
				 <?php } elseif ($update == false) { ?>
				 	<button class="btn btn-primmary" type="submit" name="save" onclick="clear()"> Save </button>
				 <?php } ?>
			</div>
			</form>
			</div>
		</div>
</div>

<!--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// -->

<?php
	//session_start();
	$sqli = mysqli_connect("localhost", "root", "", "students") or die("Could not connect database...");
	$update = false;
	$id = $name = $email = "";
	 if (isset($_REQUEST['edit'])) {
		$id = $_REQUEST['edit'];
		$update = true; 
		$record = mysqli_query($sqli, "SELECT * FROM teachers WHERE SubID=$id"); 

		if (($record) == 1 ) 
		{
			$num = mysqli_fetch_array($record);
			$id = $num['SubID'];
			$name = $num['Tname'];
			$email = $num['Email'];
		}

	}
	if(isset($_REQUEST['save'])){
		//$id = $_REQUEST['SubID'];
		$name = $_REQUEST['tname'];
		$email = $_REQUEST['temail'];
		$phone = $_REQUEST['tphone'];
		
		mysqli_query($sqli, "INSERT INTO `teachers` (`Tname`, `Email`, `Phone`) VALUES ('$name', '$email', '$phone')");
		$_SESSION['msg'] = "Employee Saved";
		header("location:testpage.php");

	}
	if(isset($_REQUEST['update'])){
		//$id = $_REQUEST['id'];
		$name = $_REQUEST['tname'];
		$email = $_REQUEST['temail'];
		$phone = $_REQUEST['tphone'];
		

		mysqli_query($sqli, "UPDATE teachers SET Tname = '$name', Email= '$email' , Phone = '$phone'  WHERE ID = $id");
		$_SESSION['msg']= "Data Updated.";
		header("location:testpage.php");
		
	}
	if(isset($_REQUEST['del'])){
		$id = $_REQUEST['del'];
		mysqli_query($sqli, "DELETE FROM teachers WHERE ID = $id");
		$_SESSION['msg'] = "Employee Data is deleted";
		header("location:testpage.php");
	}


?>

		<?php

			if(isset($_SESSION['msg'])){
				echo "<div class='msg'>";
				echo $_SESSION['msg'];
				unset($_SESSION['msg']);
				echo "</div>";
			}
			$query = mysqli_query($sqli, "SELECT * FROM teachers");
		?>


	<div class="container-fluid">
			<div class="row">
				<div class="col-md-5">
				<div class="table1">
			<table>
				<tr>
					<td>SubID</td>
					<td>Name</td>
					<td>Email</td>
					<td>Phone</td>					
					<td colspan="2">Action</td>
				</tr>
				<?php
					while($row = mysqli_fetch_array($query)){
						echo "<tr>";
						echo "<td>" .$row['SubID']. "</td>";
						echo "<td>".$row['Tname']."</td>";
						echo "<td>".$row['Email']."</td>";
						echo "<td>".$row['Phone']."</td>";
						
						
						echo "<td><a class='link' href=testpage.php?edit=".$row['SubID'].">Edit</a></td>";
						echo "<td><a class='link' href=testpage.php?del=".$row['SubID'].">Delete</a></td>";
						echo "</tr>";
					}
				?>
		</table>
	</div>
	</div>
	</div>

	<form action="testpage.php" method="POST">
			<!-- <div class="input-group">
				<label>ID</label>
				<input type="text" name="id" value=""> 
			</div> -->
			<div class="input-group">
				<label>Tname</label>
				<input type="text" name="tname" value="">
			</div>
			<div class="input-group">
				<label>Email</label>
				<input type="text" name="temail" value="">
			</div>
			<div class="input-group">
				<label>Phone</label>
				<input type="text" name="tphone" value="">
			</div>
			
			<div class="input-group">
				<?php if($update == true) { ?>
				 <button class="btn" type="submit" name="update" style="background: red;" onclick="back()"> Update </button>
				 <?php } elseif ($update == false) { ?>
				 	<button class="btn btn-primmary" type="submit" name="save" onclick="clear()"> Save </button>
				 <?php } ?>
			</div>
			</form>
</div>


<!--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// -->

<?php
	//session_start();
	$sqli = mysqli_connect("localhost", "root", "", "students") or die("Could not connect database...");
	$update = false;
	$id = $name = $email = "";
	 /*if (isset($_REQUEST['edit'])) {
		$id = $_REQUEST['edit'];
		$update = true; 
		$record = mysqli_query($sqli, "SELECT students.SubID,teachers.SubID  FROM table1 LEFT JOIN teachers ON table1.SubID = teachers.SubID WHERE ORDER BY table1.SubID;"); 

		if (($record) == 1 ) 
		{
			$num = mysqli_fetch_array($record);
			$id = $num['SubID'];
			$name = $num['Uname'];
			$tname= $num['Tname'];
			$email = $num['email'];
			$phone = $num['phone'];
		}

	} */
	



?>

		<?php

			if(isset($_SESSION['msg'])){
				echo "<div class='msg'>";
				echo $_SESSION['msg'];
				unset($_SESSION['msg']);
				echo "</div>";
			}
			$query = mysqli_query($sqli, "SELECT students.SubID,teachers.SubID  FROM table1 LEFT JOIN teachers ON table1.SubID = teachers.SubID WHERE ORDER BY table1.SubID;");
		?>





	<div class="container-fluid">
			<div class="row">
				<label>COMMON TABLE</label>
				<div class="col-md-5">
				<div class="table1">
			<table>
				<tr>
					<td>SubID</td>
					<td>Uname</td>
					<td>Tname</td>
					<td>Email</td>
					<td>Phone</td>					
					<td colspan="2">Action</td>
				</tr>
				<?php
					while($row = mysqli_fetch_array($query)){
						echo "<tr>";
						echo "<td>" .$row['SubID']. "</td>";
						echo "<td>".$row['Uname']."</td>";
						echo "<td>".$row['Tname']."</td>";
						echo "<td>".$row['Email']."</td>";
						echo "<td>".$row['Phone']."</td>";
						
						
						echo "<td><a class='link' href=testpage.php?edit=".$row['SubID'].">Edit</a></td>";
						echo "<td><a class='link' href=testpage.php?del=".$row['SubID'].">Delete</a></td>";
						echo "</tr>";
					}
				?>
		</table>
	</div>
	</div>
	</div>

	
</div>











		
</body>
</html>