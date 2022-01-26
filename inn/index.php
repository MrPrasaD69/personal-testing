<?php
session_start();
include('dbcon.php');
?>

<html>
<head>
	<title>HOME</title>
</head>
<body>
	<div class ="container-fluid">
	<h2>WELCOME TO HOME</h2>
	<h3 class="text-center text-success"> Logged in as <?php echo $_SESSION['username']; ?> </h3>
</div>
</body>
</html>