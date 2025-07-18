/**const axios = require('axios');

async function fetchTodos() {
  try {
    const response = await axios.get('https://jsonplaceholder.typicode.com/todos');
    console.log(response.data);
  } 
  catch (error) {
    console.error('Error fetching todos:', error.message);
  }
}

fetchTodos(); **/
const axiosreq = require('axios');
async function getUsersInOrder() {
  try {
    const res1 = await axiosreq.get('https://jsonplaceholder.typicode.com/users/3');
    console.log("User 3:", res1.data.name);
    
    const res2 = await axiosreq.get('https://jsonplaceholder.typicode.com/users/30');
    console.log("User 30:", res2.data.name);
  } catch (err) {
    console.error("Error:", err.message);
  }
}

getUsersInOrder();

