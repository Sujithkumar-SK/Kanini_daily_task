document.getElementById("tourform").addEventListener("submit", function (event) {
  let customerData = {
    name: document.getElementById("name").value,
    mobile: document.getElementById("mobileNumber").value,
    aadhar: document.getElementById("aadharNumber").value,
    checkindate: document.getElementById("dataOfCheckin").value,
    package: document.getElementById("packageType").value,
    adults: document.getElementById("adults").value,
    children: document.getElementById("children").value,
    payment: document.querySelector('input[name="paymentMode"]:checked').value,
    card: document.getElementById("cardNumber").value,
    cvv: document.getElementById("cvvNumber").value
  };
  localStorage.setItem("tourData",JSON.stringify(customerData));
  alert("Your data is saved!...")
});

function opendetails() {
  window.open("details.html","_blank");
}

document.getElementById("reset").addEventListener("click", function () {
  localStorage.removeItem("tourData");
});