AddDigits=(num)=>{
    return String(num).split('').reduce((acc,curr)=>{
        return acc + Number(curr);
    },0);
}
console.log(AddDigits(5259));