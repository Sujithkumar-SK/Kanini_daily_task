function result()
{
  let name = document.getElementById('name').value;
  let phone = document.getElementById('phoneNumber').value;
  let mail = document.getElementById('emailid').value;
  let address = document.getElementById('address').value;


  let check = document.querySelectorAll("input[type='checkbox']:checked");
  let tot=0;
  //console.log(check[0]);
  for (var i=0;i<check.length;i++) {
    tot = tot + parseInt(check[i].value);
  }

  let discount=0;

  if (tot >=2000) {
    discount = tot *0.2
  }

  let finalamount = Math.round(tot - discount);

  document.getElementById("result").innerText=`Order has been placed sucessfully. You are requested to pay Rs.${finalamount} on delivery.`;

  return false;
}