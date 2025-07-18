const axiosreq = require('axios');

axiosreq.get('https://jsonplaceholder.typicode.com/users/1')
.then(respone => {
  const user = respone.data;
  console.log('User:', user.name);
  return axiosreq.get(`https://jsonplaceholder.typicode.com/posts?userId=${user.id}`)
})
.then(post => {
  const respost = post.data;

  if (respost.length > 0) {
    console.log('First Post Title:',respost[0].title);
  }
  else {
    console.log('No posts found for this user.');
  }
})
.catch(error => {
  console.log('something went wrong:', error.message);
});