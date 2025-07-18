function validate() {
  var name = document.getElementById("name").value.trim();
  var email = document.getElementById("email").value.trim();
  var password = document.getElementById("password").value;
  var confirmPassword = document.getElementById("confirmpassword").value;

  document.getElementById("namelocation").innerHTML = "";
  document.getElementById("emaillocation").innerHTML = "";
  document.getElementById("passwordlocation").innerHTML = "";
  document.getElementById("confirmlocation").innerHTML = "";

  var valid = true;

  if (name === "") {
    document.getElementById("namelocation").innerHTML = "Name field cannot be empty";
    valid = false;
  }

  if (email === "") {
    document.getElementById("emaillocation").innerHTML = "Email field cannot be empty";
    valid = false;
  }

  if (password === "") {
    document.getElementById("passwordlocation").innerHTML = "Password field cannot be empty";
    valid = false;
  } else if (password.length < 5) {
    document.getElementById("passwordlocation").innerHTML = "Password length must be greater than 5";
    valid = false;
  }

  if (confirmPassword === "") {
    document.getElementById("confirmlocation").innerHTML = "Please enter the password again";
    valid = false;
  } else if (confirmPassword !== password) {
    document.getElementById("confirmlocation").innerHTML = "Confirm password does not match";
    valid = false;
  }

  return valid;
}
