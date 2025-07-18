const alphabetData = [
  { letter: "A", word: "Apple" },
  { letter: "B", word: "Ball" },
  { letter: "C", word: "Cat" },
  { letter: "D", word: "Dog" },
  { letter: "E", word: "Elephant" },
  { letter: "F", word: "Fish" },
  { letter: "G", word: "Goat" },
  { letter: "H", word: "Hat" },
  { letter: "I", word: "Ice cream" },
  { letter: "J", word: "Jug" },
  { letter: "K", word: "Kite" },
  { letter: "L", word: "Lion" },
  { letter: "M", word: "Monkey" },
  { letter: "N", word: "Nest" },
  { letter: "O", word: "Orange" },
  { letter: "P", word: "Parrot" },
  { letter: "Q", word: "Queen" },
  { letter: "R", word: "Rabbit" },
  { letter: "S", word: "Sun" },
  { letter: "T", word: "Tiger" },
  { letter: "U", word: "Umbrella" },
  { letter: "V", word: "Van" },
  { letter: "W", word: "Watch" },
  { letter: "X", word: "Xylophone" },
  { letter: "Y", word: "Yak" },
  { letter: "Z", word: "Zebra" }
];


const numberData = [
  { number: 1, word: "One" },
  { number: 2, word: "Two" },
  { number: 3, word: "Three" },
  { number: 4, word: "Four" },
  { number: 5, word: "Five" },
  { number: 6, word: "Six" },
  { number: 7, word: "Seven" },
  { number: 8, word: "Eight" },
  { number: 9, word: "Nine" },
  { number: 10, word: "Ten" }
];



const API_KEY = "ruqiuJKIpiJhvWgk8kwYfnUCnhrBYeCFltPBFxp3VqXcZnL7Sd4IKRsc";

$(document).ready(function () {

  function showSection(sectionId) {
    $("#mainContainer section").each(function () {
      $(this).removeClass("d-flex").hide();
    });
    if (sectionId === "#homeSection") {
      $(sectionId).addClass("d-flex").fadeIn();
    } else {
      $(sectionId).fadeIn();
    }
  }


  $("#navHome").click(function () {
    showSection("#homeSection");
  });

  $("#startLearningBtn").click(function () {
    showSection("#alphabetSection");
  });

  $("#navAlphabets").click(function () {
    showSection("#alphabetSection");
  });

  $("#navNumbers").click(function () {
    showSection("#numberSection");
  });

  $("#navDictionary").click(function () {
    showSection("#dictionarySection");
  });



  loadAlphabetCards();
  loadNumberCards()

  const backToTopBtn = document.getElementById("backToTopBtn");

  window.onscroll = function () {
    if (document.body.scrollTop > 100 || document.documentElement.scrollTop > 100) {
      backToTopBtn.style.display = "block";
    } else {
      backToTopBtn.style.display = "none";
    }
  };

  backToTopBtn.addEventListener("click", function () {
    window.scrollTo({
      top: 0,
      behavior: "smooth"
    });
  });


  $("#dictionarySearchBtn").click(async function () {
    const word = $("#dictionaryInput").val().trim();
    const $result = $("#dictionaryResult");

    if (!word) {
      $result.html('<p class="text-danger text-center">Please enter a word to search.</p>');
      return;
    }

    $result.html('<p class="text-center">Searching...</p>');

    try {
      const response = await fetch(`https://api.dictionaryapi.dev/api/v2/entries/en/${encodeURIComponent(word)}`);
      if (!response.ok) throw new Error("Word not found");

      const data = await response.json();

      let html = `<h5>Definitions for "<strong>${word}</strong>":</h5><ul>`;
      data[0].meanings.forEach(meaning => {
        meaning.definitions.forEach(def => {
          html += `<li><em>${meaning.partOfSpeech}</em>: ${def.definition}</li>`;
        });
      });
      html += '</ul>';

      $result.html(html);

    } catch (error) {
      $result.html('<p class="text-danger text-center">No results found for "' + word + '".</p>');
    }
  });

  $("#dictionaryClearBtn").click(function () {
    $("#dictionaryInput").val('');
    $("#dictionaryResult").html('<p class="text-muted text-center">Enter a word above and click Search.</p>');
  });



});

async function loadAlphabetCards() {
  const $grid = $("#alphabetGrid");

  for (let i = 0; i < alphabetData.length; i++) {
    const item = alphabetData[i];

    let imgUrl = "images/number/default.jpg";

    try {
      const response = await fetch(`https://api.pexels.com/v1/search?query=${encodeURIComponent(item.word)}&per_page=1`, {
        headers: { Authorization: API_KEY }
      });

      const data = await response.json();
      if (data.photos.length > 0) {
        imgUrl = data.photos[0].src.medium;
      }
    } catch (error) {
      console.log(`Error fetching for ${item.word}:`, error);
    }

    const card = `
      <div class="col">
        <div class="card text-center p-3 shadow-sm border-0" style="cursor:pointer;">
          <div class="display-4 fw-bold text-warning">${item.letter}</div>
          <img src="${imgUrl}" alt="${item.word}" class="img-fluid mx-auto" style="max-height: 70px;">
          <div class="mt-2 fw-semibold">${item.word}</div>
        </div>
      </div>
    `;
    $grid.append(card);
  }
}



function loadNumberCards() {
  const $grid = $("#numberGrid");
  $grid.empty();

  numberData.forEach(item => {
    const imgUrl = `images/number/${item.number}.jpg`;

    const card = `
      <div class="col">
        <div class="card text-center p-3 shadow-sm border-0" style="cursor:pointer;">
          <div class="display-4 fw-bold text-success">${item.number}</div>
          <img src="${imgUrl}" alt="${item.word}" class="img-fluid mx-auto" style="max-height: 70px;">
          <div class="mt-2 fw-semibold">${item.word}</div>
        </div>
      </div>
    `;
    $grid.append(card);
  });
}




