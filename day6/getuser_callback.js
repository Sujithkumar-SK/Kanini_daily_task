const axiosreq = require('axios');

function getUser(id,callback) {
  axiosreq.get(`https://jsonplaceholder.typicode.com/users/${id}`)
  .then(respone => {
    const user = respone.data;
    callback(null, user.name);
  })
  .catch(error => {
    callback(error, null);
  });
}

function againback(error, data) {
  if (data) {
    console.log("success! data is:", data);
  }
  else {
    console.log("something went wrong:",error.message);

  }
}

getUser(3,againback);

setTimeout(() => {
  getUser(30, againback);
}, 3000);
//getUser(30,againback);