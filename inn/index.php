<?php
session_start();
include('dbcon.php');


?>

<html>
<head>
	<title>HOME</title>
</head>
<body>
	<link rel="stylesheet" href="style.css">
	<div class ="container-fluid">
	<h2>WELCOME TO HOME</h2>


	 <ul>
  	<li style="float:left" ><a class="active" href="index.php">Home</a></li>
  	<li><a href="login.php">Login</a></li>
 	 <li><a href="#contact">Contact</a></li>
 	 <li style="float:right"><a href="#about">About</a></li>
	</ul> 

	 
	<!-- <h3 class="text-center text-success"> Logged in as <?php echo $_SESSION['username']; ?> </h3> -->



	<?php

	if($_SESSION["username"]) 
	{
	?>
	Welcome <?php echo $_SESSION['username']; ?>. Click here to <a href="logout.php" tite="Logout">Logout.
	<?php
	}
	else echo "<h1>Please login first .</h1>";
	?>


	<div class ="row">
		<div class="col-md-4">
			
			<!-- <button type="submit" href="logout.php" class="btn btn-primary" name = "logout" id="logout">Log Out</button> -->
			<!--<h3><a href="logout.php">Log Out</a></h3>-->

		</div>
	</div>
</div>
</body>
</html>