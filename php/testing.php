<?php
$budget = 500;
$items = array(
    array('name' => 'burger', 'price' => 100),
    array('name' => 'pizza', 'price' => 150),
    array('name' => 'milkshake', 'price' => 50)
);

function findCombinations($budget, $items, $combination = array(), $startIndex = 0) {
    if ($budget === 0) {
        echo implode(', ', $combination) . "\n";
        return;
    }

    for ($i = $startIndex; $i < count($items); $i++) {
        if ($items[$i]['price'] <= $budget) {
            $combination[] = $items[$i]['name'];
            findCombinations($budget - $items[$i]['price'], $items, $combination, $i);
            array_pop($combination);
        }
    }
}

echo "Possible combinations:\n";
findCombinations($budget, $items);

?>
