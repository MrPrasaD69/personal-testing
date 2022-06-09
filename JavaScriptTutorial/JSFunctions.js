// var cars = [
//   {
//     name: "honda",
//     price: 100,
//   },
//   {
//     name: "ford",
//     price: 200,
//   },
//   {
//     name: "nissan",
//     price: 300,
//   },
// ];

// var newcars = cars.map(x=>{
//     return x.name;
// })
// console.log(newcars);

// items=[100,200,30,5,0,69,212];
// var newitems=items.filter((item)=>{
//     return item < 50;
// })
// console.log(newitems);

// const a = [
//  {price: 100},
//  {price: 20},
//  {price:53},
//  {price:87},
//  {price:205}
// ];

// const b= a.reduce((total, a1)=>{
//     return a1.price + total;
// },0)
// console.log(b);

const a = [
  {
    name: "Prasad",
    age: 22,
  },
  {
    name: "Neha",
    age: 22,
  },
];

const b= a.map(a1=>{
  return (a1.name);
})
console.log(b);
