<?php
	include 'db_connection.php';
	//Set up connection
	$conn = OpenCon();
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	//Select entire table
	$sql = "SELECT * FROM `maintable` ORDER BY `date`";
	if($conn->query($sql) === FALSE)
	{
		echo "Error: " . $sql . "<br>" . $conn->error;	
	}
	else
	{
		//array to hold our addresses
		$address = array();	
		while()
		
	}
CloseCon($conn);	
?>