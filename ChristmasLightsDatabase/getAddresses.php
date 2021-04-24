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
	$result = $conn->query($sql);
	if($result === FALSE)
	{
		echo "Error: " . $sql . "<br>" . $conn->error;	
	}
	else
	{
		//array to hold our addresses
		$addressArray = array();	
		while($row = mysqli_fetch_array($result))
		{
			$address = array("addressLine" => $row['addressline'],
							 "city" => $row['city'],
							 "state" => $row['state'],
							 "zipCode" => $row['zipcode'],
							 "desc" => $row['desc'],
							 "image64" => $row['imageData']);
			//push to array
			array_push($addressArray, $address);
		}
		echo json_encode($addressArray);	
	}
CloseCon($conn);	
?>