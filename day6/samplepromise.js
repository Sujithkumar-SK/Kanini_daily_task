const axiosreq = require('axios')
axiosreq.get('https://jsonplaceholder.typicode.com/todos')
.then(respone =>{
  console.log(respone.data)
})