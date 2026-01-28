var num = 10;
function test() {
    var num = 20;
    if (true) {
        let num = 30;
        console.log("Inside if:", num);
    }
    console.log("Inside function:", num);
}
test();
console.log("Outside:", num);

//output
// Inside if: 30
// Inside function: 20
// Outside: 10


console.log(a);
console.log(b);
console.log(c);
var a = 5;
let b = 10;
const c = 15;


//output 
//a is undefined
//we cannot access b before initialization
//c is never reached because of the error with b



console.log("--- Testing var ---");
var x = "I am a var";
console.log("Initial value:", x);

x = "I was re-assigned";
console.log("After re-assignment:", x);


var x = "I was re-declared";
console.log("After re-declaration:", x);



console.log("--- Testing let ---");
let y = 100;
console.log("Initial value:", y);


y = 200;
console.log("After re-assignment:", y);


try {
  let y = 300; 
  console.log("After re-declaration:", y);
} catch (error) {
  console.error("Re-declaring 'let' ERROR:", error.message);
}



console.log("--- Testing const ---");
const z = "I am constant";
console.log("Initial value:", z);


try {
  z = "I cannot be changed"; 
} catch (error) {
  console.error("Re-assigning 'const' ERROR:", error.message);
}


try {
  const z = "I cannot be re-declared";
} catch (error) {
  console.error("Re-declaring 'const' ERROR:", error.message);
}


//for var 
// NO errors 
//The value of x was successfully changed by both re-assignment and re-declaration.

//for let
//Re-assignment (y = 200;) worked.
//let allows re-assignment.  the try { ... } created a new block scope, so let y = 300 created a new "shadow" variable y that only existed inside that block

//Re-assignment (z = "...") failed and threw a TypeError.



//4.

const multiply = (a, b) => a * b;

console.log(multiply(5, 4)); 

const greet = (name) => `Hello, ${name}!`;
console.log(greet("Alice"));    


const numbers = [2, 5, 8, 11, 14];


const doubled = numbers.map(num => num * 2);
console.log(doubled);
const evenNumbers = numbers.filter(num => num % 2 === 0);
console.log(evenNumbers);
const sum = numbers.reduce((acc, curr) => acc + curr, 0);
console.log(sum);