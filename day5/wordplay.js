function wordplay(num){
  if (num > 30) {
    return "Input exceeds the range";
  }
  if (num < 1) {
    return "Input falls short of the range";
  }
  var result = [];
  for (let i = 1; i <=num; i++) {
    if (i % 2 == 0 && i % 5 == 0) {
      result[i] = "Jump";
    }
    else if (i % 2 ==0) {
      result[i] = "Tap";
    }
    else if (i % 5 == 0) {
      result[i] = " Stomp";
    }
    else {
      result[i] = i;
    }
  }
  return result.join(" ");
}
console.log("Result of wordPlay function which lies within the specified range:\n"+ wordplay(12));
console.log("\nResult of wordPlay function which exceeds the range:\n"+ wordplay(35));
console.log("\nResult of wordPlay function which falls short of the range:\n"+ wordplay(-19));
