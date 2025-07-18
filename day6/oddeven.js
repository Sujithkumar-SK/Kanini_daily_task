function isEvenNumber(num) {
  return new Promise((res,rej) => {
    if (num % 2 == 0) {
      res(`${num} is even`);
    }
    else {
      rej(`${num} is odd`);
    }
  });
}

isEvenNumber(4)
.then(res => console.log('Success:', res))
.catch(error => console.error('Error:', error));

isEvenNumber(5)
.then(res => console.log('Success:', res))
.catch(error => console.error('Error:', error));