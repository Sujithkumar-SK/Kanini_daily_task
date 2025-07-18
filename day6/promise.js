const axiosreq = require('axios')
function fetchtodo() {
    return new Promise((resolve, reject) => {
        axiosreq.get('https://jsonplaceholder.typicode.com/todos')
            .then(response => {
                resolve(response.data)                
            })
            .catch(error => {
                reject(`Error:${error}`)
            })
    })
}
fetchtodo()
  .then( todos =>
  {
    console.log("Todo List");
    todos.forEach(todo =>
        {
        console.log(`ID:${todo.id},Title:${todo.title}`)
    });
  }
  )
 