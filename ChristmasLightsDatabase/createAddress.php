<?php
	include 'db_connection.php';
//if (isset($_POST['date']) && isset($_POST['addressLine']) && isset($_POST['city']) && isset($_POST['state']) && isset($_POST['zipCode']) && isset($_POST['description']))
//{
	function ExtendedAddslash(&$params){
    foreach($params as &$var){
        // check if $var is an array. If yes, it will start another ExtendedAddslash() function to loop to each key inside.
        is_array($var) ? ExtendedAddslash($var) : $var=addslashes($var);
		}
	}

	// Initialize ExtendedAddslash() function for every $_POST variable
	ExtendedAddslash($_POST);
	//Set up variables
	$pDate = $_POST['date'];
	$pAddressLine = $_POST['addressLine'];
	$pCity = $_POST['city'];
	$pState = $_POST['state'];
	$pZipCode = $_POST['zipCode'];
	$pDesc = $_POST['description'];
	$pImage = $_POST['image'];
	//Decode image
	$imageData = base64_decode($pImage);
	//Set up connection
	$conn = OpenCon();
	if ($conn->connect_error) 
	{
		die("Connection failed: " . $conn->connect_error);
	}
	$sql = "INSERT INTO `maintable` (`date`, `addressline`, `city`, `state`, `zipcode`, `desc`, `imageData`)
			VALUES ('$pDate', '$pAddressLine', '$pCity', '$pState', $pZipCode, '$pDesc', '$imageData')";
	if($conn->query($sql) === TRUE)
	{
		echo "Record Created Successfully";	
	}
	else
	{
		echo "Error: " . $sql . "<br>" . $conn->error;	
	}
CloseCon($conn);	
//}
file_put_contents( 'debug' . time() . '.log', var_export( $_POST, true));
?>