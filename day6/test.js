function makeJuice(callback) {
  console.log("Juice is being prepared...");
  setTimeout(() => {
    console.log("Juice is ready! -->2 sec delay");
    callback(); 
  }, 2000);
}

function bringStraw() {
  console.log("Here is your straw!");
}

makeJuice(bringStraw);
console.log("checking something");
