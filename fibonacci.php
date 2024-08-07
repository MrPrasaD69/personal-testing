<?php 

$one = 0;
$two = 1;

for($i=0; $i<20; $i++){
    $new = $one + $two;
    echo $new."\n";
    $one = $two;
    $two = $new;

}

?>
