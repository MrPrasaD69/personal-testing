//Stack DataStructure Start

var Letter = [];
var word="racecar";
var rword="";

for(var i = 0; i<word.length;i++){  //take each element from word and push it inside Letter array
    Letter.push(word[i]);
}

for(var i=0; i<word.length; i++){  //take each element out from Letter array and assign it to rword using pop in reverse
    rword+=Letter.pop();
}

if(word === rword){
    console.log(word+" is a palindrome");
}
else{
    console.log(word+" is not a palindrome");
}

//Stack DataStructure End