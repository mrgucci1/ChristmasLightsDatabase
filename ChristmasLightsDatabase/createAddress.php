<?php
	require_once(dirname(__FILE__).'/db_connection.php');

if (isset($_POST['date']) && isset($_POST['addressLine']) && isset($_POST['city']) && isset($_POST['state']) && isset($_POST['zipCode']) && isset($_POST['description'])
{
	//Set up variables
	$pDate = $_POST['date'];
	$pAddressLine = $_POST['addressLine'];
	$pCity = $_POST['city'];
	$pState = $_POST['state'];
	$pZipCode = $_POST['zipCode'];
	$pDesc = $_POST['description'];
	
	//Set up connection
	$local_db_conn = new db_connection();
	$local_db_conn->OpenCon();
	if ($local_db_conn->connect_error) 
	{
		die("Connection failed: " . $local_db_conn->connect_error);
	}
	$sql = "INSERT INTO maintable (date, addressline, city, state, zipcode, desc)
			VALUES ($pDate, $pAddressLine, $pCity, $pState, $pZipCode, $pDesc)";
	if($local_db_conn->query($sql) === TRUE)
	{
		echo "Record Created Successfully";	
	}
	else
	{
		echo "Error: " . $sql . "<br>" . $local_db_conn->error;	
	}
$local_db_conn->close();	
}
?>