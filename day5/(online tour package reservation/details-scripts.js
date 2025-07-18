// details-script.js

// Get the saved data from local storage
let data = JSON.parse(localStorage.getItem("tourData"));

// Get the table body where we want to show the details
let tableBody = document.querySelector("#detailsTable tbody");

// Check if we have any saved data
if (data) {
  // Loop through each key-value pair and create a table row
  for (let key in data) {
    let row = document.createElement("tr");

    // Field name cell
    let fieldCell = document.createElement("td");
    fieldCell.textContent = formatFieldName(key);
    row.appendChild(fieldCell);

    // Field value cell
    let valueCell = document.createElement("td");
    valueCell.textContent = data[key];
    row.appendChild(valueCell);

    // Add the row to the table
    tableBody.appendChild(row);
  }
} else {
  // If no data is found
  let row = document.createElement("tr");
  let cell = document.createElement("td");
  cell.colSpan = 2;
  cell.textContent = "No reservation data found.";
  cell.style.textAlign = "center";
  row.appendChild(cell);
  tableBody.appendChild(row);
}

// Helper function to make field names readable
function formatFieldName(key) {
  switch (key) {
    case "name": return "Customer Name";
    case "mobile": return "Mobile Number";
    case "aadhar": return "Aadhar Number";
    case "checkinDate": return "Check-in Date";
    case "package": return "Package Type";
    case "adults": return "Number of Adults";
    case "children": return "Number of Children";
    case "payment": return "Payment Mode";
    case "card": return "Card Number";
    case "cvv": return "CVV Number";
    default: return key;
  }
}
