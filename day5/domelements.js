var obj_array = [];

function instantiateObject(name, age, height, weight) {
  return {
    Name: name,
    Age: age,
    Height: height,
    Weight: weight
  };
}

function addToArray(obj) {
  obj_array.push(obj);
  return obj_array;
}

function display() {
  var name = document.getElementById("name").value;
  var age = document.getElementById("age").value;
  var height = document.getElementById("height").value;
  var weight = document.getElementById("weight").value;
  var obj = instantiateObject(name, age, height, weight);
  var updatedArray = addToArray(obj);
  var tableHTML = "<table class='table'><tr><th>Name</th><th>Age</th><th>Height</th><th>Weight</th></tr>";
  for (var i = 0; i < updatedArray.length; i++) {
    tableHTML += "<tr><td>" + updatedArray[i].Name + "</td><td>" + updatedArray[i].Age + "</td><td>" + updatedArray[i].Height + "</td><td>" + updatedArray[i].Weight + "</td></tr>";
  }

  tableHTML += "</table>";

  document.getElementById("result").innerHTML = tableHTML;
}
